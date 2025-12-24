using Client.Services.Network;
using Common.Constracts;
using Common.Constracts.Room;
using Common.Contracts.Room;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Views.Forms
{
    public partial class RoomWaitingForm : Form
    {
        private System.Windows.Forms.Timer _reloadTimer;
        private bool _isLeaving = false;


        private Dictionary<int, string> _charNames = new()
        {
            { 1, "Nhân vật 1" },
            { 2, "Nhân vật 2" },
            { 3, "Nhân vật 3" },
            { 4, "Nhân vật 4" }
        };

        public RoomWaitingForm(int idmatch)
        {
            InitializeComponent();
            lblRoomId.Text = $"ID: {idmatch}";

            this.Load += RoomWaitingForm_Load;

        }



        private async void RoomWaitingForm_Load(object sender, EventArgs e)
        {
            lblRoomId.Text = $"ID: {ClientSession.MatchID}";
            btnStart.Enabled = false;

            ClientSession.Tcp.OnEvent -= HandleServerEvent;
            ClientSession.Tcp.OnEvent += HandleServerEvent;

            await ReloadPlayers();
        }




        private async Task ReloadPlayers()
        {
            var resp = await ClientSession.Tcp.SearchRoomAsync(ClientSession.MatchID);
            if (!resp.Success) return;

            lstPlayers.Items.Clear();

            foreach (var p in resp.Players.OrderBy(x => x.PlayerId))
            {
                if (p.CharacterIndex <= 0) continue;

                lstPlayers.Items.Add($"{p.DisplayName} - {_charNames[p.CharacterIndex]}");
            }

        }

        private async void btnLeave_Click(object sender, EventArgs e)
        {
            _isLeaving = true;

            await LeaveRoomAndBack();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            if (_isLeaving) return;

            // fire & forget – đúng chuẩn WinForms
            _ = LeaveRoomAndBack();
        }


        private async Task LeaveRoomAndBack()
        {
            try
            {
                if (ClientSession.MatchID != 0 && ClientSession.PlayerID != 0)
                {
                    await ClientSession.Tcp.LeaveRoomAsync(
                        ClientSession.MatchID,
                        ClientSession.PlayerID
                    );
                }
            }
            catch { }

            ClientSession.MatchID = 0;
            ClientSession.PlayerID = 0;

            ClientSession.Tcp.OnEvent -= HandleServerEvent;

            Hide();
            new RoomHubForm().Show();
        }



        private async void btnStart_Click(object sender, EventArgs e)
        {
            await ClientSession.Tcp.StartMatchAsync(
                ClientSession.MatchID,
                ClientSession.PlayerID
            );

            MessageBox.Show("Bắt đầu game!");
        }

        private int _currentHostPlayerId = 0;


        private void HandleServerEvent(MessageEnvelope env)
        {
            switch (env.Type)
            {
                case MessageType.RoomUpdatedEvent:
                    {
                        var ev = JsonSerializer.Deserialize<RoomUpdatedEvent>(
                            env.Payload,
                            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                        );
                        if (ev == null) return;

                        BeginInvoke(new Action(() =>
                        {
                            _currentHostPlayerId = ev.HostPlayerId;
                            btnStart.Enabled = ClientSession.PlayerID == _currentHostPlayerId;
                            _ = ReloadPlayers();
                        }));

                        break;
                    }



                case MessageType.StartMatchResponse:
                    BeginInvoke(new Action(() =>
                    {
                        GoToMainForm();
                    }));
                    break;
            }
        }

        private void GoToMainForm()
        {
            ClientSession.Tcp.OnEvent -= HandleServerEvent;

            Hide();

            var main = new MainForm(ClientSession.MatchID, ClientSession.PlayerID);

            main.Show();
        }

        public class MatchHistoryItem
        {
            public int MatchId { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime? EndTime { get; set; }
            public int? Rank { get; set; }
            public string Status { get; set; }   // Win / Lose / Bankrupt
        }
    }
}

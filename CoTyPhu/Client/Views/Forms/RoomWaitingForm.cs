using Client.Services.Network;
using Common.Constracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Views.Forms
{
    public partial class RoomWaitingForm : Form
    {
        private System.Windows.Forms.Timer _reloadTimer;

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
            btnStart.Enabled = ClientSession.PlayerID == 1;

            ClientSession.Tcp.OnEvent -= HandleServerEvent;
            ClientSession.Tcp.OnEvent += HandleServerEvent;

            await ReloadPlayers();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            if (ClientSession.Tcp != null)
                ClientSession.Tcp.OnEvent -= HandleServerEvent;
        }



        private async Task ReloadPlayers()
        {
            var resp = await ClientSession.Tcp.SearchRoomAsync(ClientSession.MatchID);
            if (!resp.Success) return;

            lstPlayers.Items.Clear();

            foreach (var p in resp.Players.OrderBy(x => x.PlayerId))
            {
                if (p.CharacterIndex <= 0) continue;

                lstPlayers.Items.Add($"Player {p.PlayerId} - {_charNames[p.CharacterIndex]}");
            }

            btnStart.Enabled = ClientSession.PlayerID == 1;
        }


        private async void btnLeave_Click(object sender, EventArgs e)
        {
            _reloadTimer?.Stop();

            await ClientSession.Tcp.LeaveRoomAsync(
                ClientSession.MatchID,
                ClientSession.PlayerID
            );

            ClientSession.MatchID = 0;
            ClientSession.PlayerID = 0;

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

        private void HandleServerEvent(MessageEnvelope env)
        {
            switch (env.Type)
            {
                case MessageType.RoomUpdatedEvent:
                    BeginInvoke(new Action(async () =>
                    {
                        await ReloadPlayers();
                    }));
                    break;

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



    }
}

using Client.Services.Network;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Views.Forms
{
    public partial class RoomWaitingForm : Form
    {
        public RoomWaitingForm()
        {
            InitializeComponent();
        }

        private Dictionary<int, string> _charNames = new()
    {
        { 1, "Nhân vật 1" },
        { 2, "Nhân vật 2" },
        { 3, "Nhân vật 3" },
        { 4, "Nhân vật 4" }
    };

        private async void RoomWaitingForm_Load(object sender, EventArgs e)
        {
            lblRoomId.Text = $"ID: {ClientSession.MatchID}";

            btnStart.Enabled = ClientSession.PlayerID == 1;

            await ReloadPlayers();
        }

        private async Task ReloadPlayers()
        {
            lstPlayers.Items.Clear();

            var resp = await ClientSession.Tcp.SearchRoomAsync(ClientSession.MatchID);
            if (!resp.Success) return;

            int idx = 1;
            foreach (var charIndex in resp.PlayerRooms)
            {
                lstPlayers.Items.Add(
                    $"Player {idx++} - {_charNames[charIndex]}"
                );
            }
        }


        private async void btnLeave_Click(object sender, EventArgs e)
        {
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

    }
}

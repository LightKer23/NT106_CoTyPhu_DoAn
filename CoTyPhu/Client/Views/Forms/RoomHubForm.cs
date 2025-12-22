using Client.Services.Network;
using Common.Contracts.Room;
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
    public partial class RoomHubForm : Form
    {
        public RoomHubForm()
        {
            InitializeComponent();
        }

        private async void btnCreateRoom_Click(object sender, EventArgs e)
        {
            var resp = await ClientSession.Tcp.CreateRoomAsync(ClientSession.AccountID);

            ClientSession.MatchID = resp.RoomID;
            //ClientSession.PlayerID = 1; // host

            Hide();
            new ChooseCharacterForm().Show();
        }

        private async void btnJoinRoom_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtRoomId.Text, out int roomId))
            {
                MessageBox.Show("ID Phòng không hợp lệ");
                return;
            }

            SearchRoomResponse resp;
            try
            {
                resp = await ClientSession.Tcp.SearchRoomAsync(roomId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kiểm tra phòng: " + ex.Message);
                return;
            }

            if (!resp.Success)
            {
                MessageBox.Show("Phòng không tồn tại hoặc đã bị đóng.");
                return;
            }

            int currentPlayers = resp.Players.Count(p => p.CharacterIndex > 0);

            if (currentPlayers >= 4)
            {
                MessageBox.Show("Phòng đã đầy (tối đa 4 người).");
                return;
            }

            ClientSession.MatchID = roomId;

            Hide();
            new ChooseCharacterForm().Show();
        }

    }
}

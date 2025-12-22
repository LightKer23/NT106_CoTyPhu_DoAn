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
            ClientSession.PlayerID = 1; // host

            Hide();
            new ChooseCharacterForm().Show();
        }

        private void btnJoinRoom_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtRoomId.Text, out int roomId))
            {
                MessageBox.Show("Room ID không hợp lệ");
                return;
            }

            ClientSession.MatchID = roomId;

            Hide();
            new ChooseCharacterForm().Show();
        }
    }
}

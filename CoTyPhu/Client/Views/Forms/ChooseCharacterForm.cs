using Client.Services.Network;
using Common.Constracts;
using Common.Constracts.Room;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Views.Forms
{
    public partial class ChooseCharacterForm : Form
    {
        private readonly TcpClientService _tcp;
        private readonly int _accountId;

        public int RoomId { get; set; }

        public ChooseCharacterForm(TcpClientService tcp, int accountId)
        {
            InitializeComponent();
            _tcp = tcp;
            _accountId = accountId;


            btnChar1.Click += btnChar1_Click;
            btnChar2.Click += btnChar2_Click;
            btnChar3.Click += btnChar3_Click;
            btnChar4.Click += btnChar4_Click;
        }

        private async void ChooseCharacterForm_Load(object sender, EventArgs e)
        {
            // 1. Tạo request tạo phòng
            var req = new CreateRoomRequest
            {
                AccountID = _accountId
            };

            var env = new MessageEnvelope(
                MessageType.CreateRoomRequest,
                JsonSerializer.Serialize(req),
                playerId: _accountId
            );

            // 2. Gửi lên server
            //await _tcp.
        }

        private void SelectCharacter(int index)
        {
            _selectedChar = index;

            Button[] chars = { btnChar1, btnChar2, btnChar3, btnChar4 };

            for (int i = 0; i < chars.Length; i++)
            {
                chars[i].BackColor = i == index
                    ? Color.DarkGray
                    : SystemColors.Control;
            }
        }

        private void btnChar1_Click(object s, EventArgs e) => SelectCharacter(0);
        private void btnChar2_Click(object s, EventArgs e) => SelectCharacter(1);
        private void btnChar3_Click(object s, EventArgs e) => SelectCharacter(2);
        private void btnChar4_Click(object s, EventArgs e) => SelectCharacter(3);


        private int _selectedChar = -1;

        private async void button1_Click(object sender, EventArgs e)
        {
            if (_selectedChar == -1)
            {
                MessageBox.Show("Vui lòng chọn nhân vật");
                return;
            }

            try
            {
                MessageBox.Show($"{RoomId}, {_accountId}");

                await ClientSession.ConnectAsync();

                var res = await _tcp.JoinRoomAsync(RoomId, _accountId, _selectedChar);

                if (res == null || !res.Success)
                {
                    MessageBox.Show("Vào phòng thất bại");
                    return;
                }

                // disable nhân vật mình chọn
                DisableCharacter(_selectedChar);

                // server trả playerId
                //ClientSession.PlayerID = res.;

                bool isHost = (ClientSession.PlayerID == 0);

                // UI
                button1.Visible = isHost;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi vào phòng: " + ex.Message);
            }
        }




        private void DisableCharacter(int index)
        {
            Button[] chars = { btnChar1, btnChar2, btnChar3, btnChar4 };

            if (index < 0 || index >= chars.Length)
                return;

            chars[index].Enabled = false;
            chars[index].BackColor = Color.Gray;
        }

    }
}

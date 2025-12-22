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
    public partial class ChooseCharacterForm : Form
    {

        private int _selectedChar = -1;

        private Dictionary<int, Button> _charButtons;

        public ChooseCharacterForm()
        {
            InitializeComponent();
        }

        private async void ChooseCharacterForm_Load(object sender, EventArgs e)
        {
            _charButtons = new() { { 1, btnChar1 }, { 2, btnChar2 }, { 3, btnChar3 }, { 4, btnChar4 } };

            foreach (var kv in _charButtons)
            {
                int charIndex = kv.Key;
                kv.Value.Click += (_, __) => SelectCharacter(charIndex);
            }

            // Hỏi server: phòng này đã chọn những char nào
            var resp = await ClientSession.Tcp.SearchRoomAsync(ClientSession.MatchID);

            if (resp.Success)
            {
                DisableTakenCharacters(resp.PlayerRooms);
            }
        }


        private void DisableTakenCharacters(List<int> takenChars)
        {
            foreach (var charIndex in takenChars)
            {
                if (_charButtons.TryGetValue(charIndex, out var btn))
                {
                    btn.Enabled = false;
                    btn.BackColor = Color.Gray;
                }
            }
        }

        private void SelectCharacter(int charIndex)
        {
            _selectedChar = charIndex;

            foreach (var btn in _charButtons.Values)
                btn.FlatStyle = FlatStyle.Standard;

            _charButtons[charIndex].FlatStyle = FlatStyle.Popup;
        }



        private async void btnChoose_Click(object sender, EventArgs e)
        {
            if (_selectedChar == -1)
            {
                MessageBox.Show("Vui lòng chọn nhân vật");
                return;
            }

            var resp = await ClientSession.Tcp.JoinRoomAsync(
                ClientSession.MatchID,
                ClientSession.AccountID,
                _selectedChar);

            if (!resp.Success)
            {
                MessageBox.Show("Nhân vật đã bị chọn!");
                return;
            }

            ClientSession.PlayerID = resp.IDPlayer;

            Hide();
            new RoomWaitingForm(ClientSession.MatchID).Show();
        }
    }
}

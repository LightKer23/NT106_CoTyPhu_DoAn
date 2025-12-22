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
    public partial class MainForm : Form
    {
        public MainForm(int matchId, int playerId)
        {
            InitializeComponent();

            ClientSession.MatchID = matchId;
            ClientSession.PlayerID = playerId;

            //InitGame();
        }


        private void pbTile9_Click(object sender, EventArgs e)
        {

        }

        private void pbTile11_Click(object sender, EventArgs e)
        {

        }

        private void pbTile24_Click(object sender, EventArgs e)
        {

        }
    }
}

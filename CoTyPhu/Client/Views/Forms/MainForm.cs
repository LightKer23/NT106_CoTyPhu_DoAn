using Client.Services.Network;
using Common.Constracts;
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

            ClientSession.Tcp.OnEvent += HandleServerEvent;

            btnRollDice.Click += BtnRollDice_Click;
            btnEndTurn.Click += BtnEndTurn_Click;
            btnBuy.Click += BtnBuy_Click;

            btnEndTurn.Enabled = false;
        }


        private async void BtnRollDice_Click(object? sender, EventArgs e)
        {
            btnRollDice.Enabled = false;

            await ClientSession.Tcp.RollDiceAsync(
                ClientSession.MatchID,
                ClientSession.PlayerID
            );
        }


        private async void BtnBuy_Click(object? sender, EventArgs e)
        {
            await ClientSession.Tcp.BuyDecisionAsync(
                ClientSession.MatchID,
                ClientSession.PlayerID,
                tileIndex: 5,
                accept: true
            );

            btnEndTurn.Enabled = true;
        }


        private void BtnEndTurn_Click(object? sender, EventArgs e)
        {
            btnEndTurn.Enabled = false;
            btnRollDice.Enabled = false;
        }



        private void HandleServerEvent(MessageEnvelope env)
        {
            Invoke(() =>
            {
                switch (env.Type)
                {
                    case MessageType.PlayerMovedEvent:
                        lbHistory.Items.Add("Player di chuyển");
                        break;

                    case MessageType.AskBuyPropertyEvent:
                        lbHistory.Items.Add("Server hỏi mua đất");
                        btnBuy.Enabled = true;
                        break;

                    case MessageType.PropertyUpdatedEvent:
                        lbHistory.Items.Add("Property đã cập nhật");
                        btnEndTurn.Enabled = true;
                        break;

                    case MessageType.PlayerLeftEvent:
                        lbHistory.Items.Add("Đổi lượt chơi");

                        btnRollDice.Enabled =
                            env.Payload.Contains(ClientSession.PlayerID.ToString());

                        break;
                }
            });
        }

    }
}

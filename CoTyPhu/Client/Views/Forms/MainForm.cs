using Client.Services.Network;
using Common.Constracts;
using Common.Contracts.Game;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
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
            btnUpgrade.Click += BtnUpgrade_Click;

            btnEndTurn.Enabled = false;
            btnBuy.Visible = false;
            btnUpgrade.Visible = false;
        }


        private async void BtnRollDice_Click(object? sender, EventArgs e)
        {
            //btnRollDice.Enabled = false;

            await ClientSession.Tcp.RollDiceAsync(
                ClientSession.MatchID,
                ClientSession.PlayerID
            );
        }

        private async void BtnUpgrade_Click(object? sender, EventArgs e)
        {
            await ClientSession.Tcp.BuyDecisionAsync(
                ClientSession.MatchID,
                ClientSession.PlayerID,
                tileIndex: 5,
                accept: true
            );
            btnUpgrade.Visible = false;
        }


        private async void BtnBuy_Click(object? sender, EventArgs e)
        {
            await ClientSession.Tcp.BuyDecisionAsync(
                ClientSession.MatchID,
                ClientSession.PlayerID,
                tileIndex: 5,
                accept: true
            );

            btnBuy.Visible = false;
        }

        private static readonly JsonSerializerOptions JsonOpt = new()
        {
            PropertyNameCaseInsensitive = true
        };


        private async void BtnEndTurn_Click(object sender, EventArgs e)
        {
            btnBuy.Enabled = false;
            btnUpgrade.Enabled = false;

            await ClientSession.Tcp.EndTurnAsync(
                ClientSession.MatchID,
                ClientSession.PlayerID
            );
        }


        private void HandleServerEvent(MessageEnvelope env)
        {

            var data = JsonSerializer.Deserialize<AskBuyPropertyEvent>(env.Payload, JsonOpt);
            Invoke(() =>
            {
                switch (env.Type)
                {

                    case MessageType.PlayerMovedEvent:
                        lbHistory.Items.Add("Player di chuyển");
                        break;

                    case MessageType.AskBuyPropertyEvent:
                        {
                            if (data.IsAuction == false)
                            {
                                lbHistory.Items.Add($"Server hỏi mua đất {data.Name} : {data.TileIndex} : {data.Price}");
                                btnBuy.Visible = true;
                                break;
                            }
                            else
                            {                                 
                                lbHistory.Items.Add($"Server hỏi nâng cấp đất {data.Name} : {data.TileIndex} : {data.Price}");
                                btnUpgrade.Visible = true;
                                break;
                            }


                        }

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

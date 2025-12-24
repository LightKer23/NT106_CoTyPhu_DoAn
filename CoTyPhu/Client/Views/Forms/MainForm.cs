using Client.Services.Network;
using Common.Constracts;
using Common.Contracts.Game;
using System;
using System.Collections.Generic;
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
        private static readonly HashSet<int> PropertyTiles = new HashSet<int>
        {
            1, 3, 5, 6, 8, 9, 11, 12, 13, 14, 15, 16, 18, 19, 
            21, 23, 24, 25, 26, 27, 28, 29, 31, 32, 34, 35, 37, 39
        };

        private Dictionary<int, PictureBox> _tileMap;           
        private Dictionary<int, Panel> _tileTokenPanels;         
        private Dictionary<int, PictureBox> _playerTokens;      
        private Dictionary<int, int> _playerTile;

        // ✅ TRACKING PLAYER MONEY
        private Dictionary<int, int> _playerMoney = new Dictionary<int, int>();
        private Dictionary<int, string> _playerNames = new Dictionary<int, string>();

        // ✅ ANIMATION STATE
        private bool _isRollingDice = false;
        private System.Windows.Forms.Timer _diceAnimationTimer;
        private Random _random = new Random();
        private int _animationTicks = 0;
        
        // ✅ LƯU KẾT QUẢ XÚC XẮC TỪ SERVER
        private int _finalDice1 = 0;
        private int _finalDice2 = 0;
        private int _currentPlayerId = 0;
        
        private const int TokenAreaW = 70;
        private const int TokenAreaH = 84;

        private static readonly Point[] TokenSlots =
        {
            new Point(4, 6),   
            new Point(38, 6), 
            new Point(4, 44), 
            new Point(38, 44),
        };

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
            btnSend.Click += BtnSend_Click;

            textBox2.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    BtnSend_Click(s, e);
                }
            };

            btnEndTurn.Enabled = false;
            btnBuy.Visible = false;
            btnUpgrade.Visible = false;

            _diceAnimationTimer = new System.Windows.Forms.Timer();
            _diceAnimationTimer.Interval = 100;
            _diceAnimationTimer.Tick += DiceAnimationTimer_Tick;

            pbDie1.SizeMode = PictureBoxSizeMode.CenterImage;
            pbDie2.SizeMode = PictureBoxSizeMode.CenterImage;
            pbDie1.BackColor = Color.White;
            pbDie2.BackColor = Color.White;
            pbDie1.BorderStyle = BorderStyle.FixedSingle;
            pbDie2.BorderStyle = BorderStyle.FixedSingle;
            
            this.Shown += async (_, __) => await InitTokensFromRoomAsync();
        }

        //Hoạt họa xúc xắc
        private void DiceAnimationTimer_Tick(object sender, EventArgs e)
        {
            _animationTicks++;
            
            if (_animationTicks < 15)
            {
                ShowRandomDiceFaces();
            }
            else
            {
                _diceAnimationTimer.Stop();
                _isRollingDice = false;
                _animationTicks = 0;
                
                ShowDiceValue(pbDie1, _finalDice1);
                ShowDiceValue(pbDie2, _finalDice2);
                
                lbHistory.Items.Add($"Player {_currentPlayerId} tung được {_finalDice1} và {_finalDice2}");
                
                if (_playerTile.TryGetValue(_currentPlayerId, out int fromTile))
                {
                    int steps = _finalDice1 + _finalDice2;
                    int toTile = (fromTile + steps) % 40;
                    _ = AnimateTokenMovement(_currentPlayerId, fromTile, toTile, steps);
                }
            }
        }

        private void ShowRandomDiceFaces()
        {
            int random1 = _random.Next(1, 7);
            int random2 = _random.Next(1, 7);
            
            ShowDiceValue(pbDie1, random1);
            ShowDiceValue(pbDie2, random2);
        }

        private void ShowDiceValue(PictureBox pb, int value)
        {
            pb.Image = value switch
            {
                1 => Properties.Resources.num_1,
                2 => Properties.Resources.num_2,
                3 => Properties.Resources.num_3,
                4 => Properties.Resources.num_4,
                5 => Properties.Resources.num_5,
                6 => Properties.Resources.num_6,
                _ => null
            };
        }

        private async Task InitTokensFromRoomAsync()
        {
            try
            {
                _tileMap = BuildTileMap();

                _tileTokenPanels = BuildTokenAreas(_tileMap);

                var resp = await ClientSession.Tcp.SearchRoomAsync(ClientSession.MatchID);
                if (!resp.Success)
                {
                    MessageBox.Show("Không lấy được danh sách người chơi trong phòng.");
                    return;
                }

                var players = resp.Players
                    .Where(p => p.CharacterIndex > 0)
                    .OrderBy(p => p.PlayerId)
                    .ToList();

                _playerTokens = new Dictionary<int, PictureBox>();
                _playerTile = new Dictionary<int, int>();

                foreach (var pnl in _tileTokenPanels.Values)
                    pnl.Controls.Clear();

                lvPlayerInfo.View = View.Details;
                lvPlayerInfo.Items.Clear();

                for (int i = 0; i < players.Count && i < 4; i++)
                {
                    var p = players[i];
                    var token = CreatePlayerToken(p.PlayerId, p.CharacterIndex);

                    _playerTokens[p.PlayerId] = token;
                    _playerTile[p.PlayerId] = 0;
                    
                    _playerMoney[p.PlayerId] = 1500;
                    _playerNames[p.PlayerId] = p.DisplayName ?? $"Player {p.PlayerId}";

                    PlaceTokenOnTile(p.PlayerId, 0, slotIndex: i);
                    
                    var item = new ListViewItem(_playerNames[p.PlayerId]);
                    item.SubItems.Add($"${_playerMoney[p.PlayerId]}");
                    item.Tag = p.PlayerId; 
                    lvPlayerInfo.Items.Add(item);
                }
                
                colName.Width = 120;
                colCurrentMoney.Width = 130;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private Dictionary<int, PictureBox> BuildTileMap()
        {
            var map = new Dictionary<int, PictureBox>();

            for (int i = 0; i < 40; i++)
            {
                var found = pnlBoard.Controls.Find($"pbTile{i}", true).FirstOrDefault();
                if (found is PictureBox pb)
                    map[i] = pb;
                else
                    throw new Exception($"Không tìm thấy pbTile{i} trong pnlBoard");
            }

            return map;
        }

        private async void BtnRollDice_Click(object? sender, EventArgs e)
        {
            btnEndTurn.Enabled = true;
            btnRollDice.Enabled = false;

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

        private async void BtnSend_Click(object? sender, EventArgs e)
        {
            string message = textBox2.Text.Trim();
            
            if (string.IsNullOrEmpty(message))
                return;

            try
            {
                await ClientSession.Tcp.SendChatMessageAsync(
                    ClientSession.MatchID,
                    ClientSession.PlayerID,
                    message
                );

                lbChat.Items.Add($"Tôi: {message}");
                lbChat.TopIndex = lbChat.Items.Count - 1;

                textBox2.Clear();
            }
            catch (Exception ex)
            {
                lbHistory.Items.Add($"[Lỗi] Không thể gửi tin nhắn: {ex.Message}");
            }
        }

        private static readonly JsonSerializerOptions JsonOpt = new()
        {
            PropertyNameCaseInsensitive = true
        };

        private async void BtnEndTurn_Click(object sender, EventArgs e)
        {
            btnBuy.Visible = false;
            btnUpgrade.Visible = false;

            btnEndTurn.Enabled = false;

            await ClientSession.Tcp.EndTurnAsync(
                ClientSession.MatchID,
                ClientSession.PlayerID
            );
        }
        
        private Dictionary<int, Panel> BuildTokenAreas(Dictionary<int, PictureBox> tiles)
        {
            var dict = new Dictionary<int, Panel>();

            foreach (var kv in tiles)
            {
                int tileIndex = kv.Key;
                var tilePb = kv.Value;

                var existed = tilePb.Controls.OfType<Panel>().FirstOrDefault(p => p.Name == $"pnlTokens_{tileIndex}");
                if (existed != null)
                {
                    dict[tileIndex] = existed;
                    continue;
                }

                var pnl = new Panel
                {
                    Name = $"pnlTokens_{tileIndex}",
                    Size = new Size(TokenAreaW, TokenAreaH),
                    BackColor = Color.Transparent
                };

                int x = Math.Max(0, (tilePb.Width - TokenAreaW) / 2);
                int y = Math.Max(0, tilePb.Height - TokenAreaH);
                pnl.Location = new Point(x, y);

                pnl.Parent = tilePb;
                pnl.BringToFront();

                tilePb.Controls.Add(pnl);

                dict[tileIndex] = pnl;
            }

            return dict;
        }

        private PictureBox CreatePlayerToken(int playerId, int characterIndex)
        {
            var pb = new PictureBox
            {
                Name = $"pbToken_{playerId}",
                Size = new Size(28, 28),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent,
                Tag = playerId
            };

            pb.Image = characterIndex switch
            {
                1 => Properties.Resources.icon_1,
                2 => Properties.Resources.icon_2,
                3 => Properties.Resources.icon_3,
                4 => Properties.Resources.icon_4,
                _ => Properties.Resources.icon_1
            };

            return pb;
        }

        private void PlaceTokenOnTile(int playerId, int tileIndex, int slotIndex)
        {
            if (!_playerTokens.TryGetValue(playerId, out var token))
                return;

            if (!_tileTokenPanels.TryGetValue(tileIndex, out var tokenPanel))
                return;

            if (slotIndex < 0 || slotIndex >= TokenSlots.Length)
                slotIndex = 0;

            if (token.Parent is Control oldParent)
                oldParent.Controls.Remove(token);

            token.Location = TokenSlots[slotIndex];
            tokenPanel.Controls.Add(token);
            token.BringToFront();
            
            _playerTile[playerId] = tileIndex;
            
            if (playerId == ClientSession.PlayerID)
            {
                ShowPropertyCard(tileIndex);
            }
        }

        private void ShowPropertyCard(int tileIndex)
        {
            if (PropertyTiles.Contains(tileIndex))
            {
                string resourceName = $"TD_{tileIndex}";
                
                try
                {
                    var resourceManager = Properties.Resources.ResourceManager;
                    var image = resourceManager.GetObject(resourceName) as System.Drawing.Image;
                    
                    if (image != null)
                    {
                        pbTile.Image = image;
                        pbTile.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    else
                    {
                        pbTile.Image = null;
                    }
                }
                catch
                {
                    pbTile.Image = null;
                }
            }
            else
            {
                pbTile.Image = null;
            }
        }

        private async Task AnimateTokenMovement(int playerId, int fromTile, int toTile, int steps)
        {
            if (!_playerTokens.ContainsKey(playerId))
                return;

            int currentTile = fromTile;
            
            for (int i = 0; i < steps; i++)
            {
                currentTile = (currentTile + 1) % 40;
                
                int slot = FindAvailableSlot(currentTile, playerId);
                PlaceTokenOnTile(playerId, currentTile, slot);
                
                await Task.Delay(200);
            }
            
            if (playerId == ClientSession.PlayerID)
            {
                ShowPropertyCard(toTile);
            }
        }

        private int FindAvailableSlot(int tileIndex, int currentPlayerId)
        {
            if (!_tileTokenPanels.TryGetValue(tileIndex, out var panel))
                return 0;

            var occupiedSlots = new HashSet<int>();

            foreach (var kvp in _playerTokens)
            {
                int pid = kvp.Key;
                var token = kvp.Value;
                
                if (pid == currentPlayerId)
                    continue;
                
                if (token.Parent == panel)
                {
                    for (int i = 0; i < TokenSlots.Length; i++)
                    {
                        if (token.Location == TokenSlots[i])
                        {
                            occupiedSlots.Add(i);
                            break;
                        }
                    }
                }
            }

            for (int i = 0; i < TokenSlots.Length; i++)
            {
                if (!occupiedSlots.Contains(i))
                    return i;
            }

            return 0;
        }

        private void UpdatePlayerMoney(int playerId, int currentMoney)
        {
            _playerMoney[playerId] = currentMoney;
            
            foreach (ListViewItem item in lvPlayerInfo.Items)
            {
                if (item.Tag is int pid && pid == playerId)
                {
                    item.SubItems[1].Text = $"${currentMoney}";
                    
                    if (playerId == ClientSession.PlayerID)
                    {
                        item.BackColor = Color.LightYellow;
                    }
                    
                    break;
                }
            }
        }

        private void HandleServerEvent(MessageEnvelope env)
        {
            Invoke(() =>
            {
                switch (env.Type)
                {
                    case MessageType.PlayerMovedEvent:
                        {
                            var data = JsonSerializer.Deserialize<PlayerMoveEvent>(env.Payload, JsonOpt);
                            
                            _finalDice1 = data.Roll1;
                            _finalDice2 = data.Roll2;
                            _currentPlayerId = data.PlayerId;
                            
                            lbHistory.Items.Add($"Player {data.PlayerId} đang tung xúc xắc...");
                            
                            _isRollingDice = true;
                            _animationTicks = 0;
                            _diceAnimationTimer.Start();
                            
                            break;
                        }

                    case MessageType.MoneyChangedEvent:
                        {
                            var data = JsonSerializer.Deserialize<MoneyChangedEvent>(env.Payload, JsonOpt);
                            
                            UpdatePlayerMoney(data.PlayerId, data.CurrentMoney);
                            
                            string changeText = data.MoneyChange > 0 
                                ? $"+${data.MoneyChange}" 
                                : $"-${Math.Abs(data.MoneyChange)}";
                            
                            lbHistory.Items.Add($"Player {data.PlayerId} {changeText} (Còn lại: ${data.CurrentMoney})");
                            
                            break;
                        }

                    case MessageType.AskBuyPropertyEvent:
                        {
                            var data = JsonSerializer.Deserialize<AskBuyPropertyEvent>(env.Payload, JsonOpt);
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

                    case MessageType.PlayerSurrenderEvent:
                        {
                            var ev = JsonSerializer.Deserialize<PlayerSurrenderEvent>(env.Payload, JsonOpt);

                            lbHistory.Items.Add($"Player {ev.PlayerId} đã chịu thua");

                            if (ev.PlayerId == ClientSession.PlayerID)
                            {
                                MessageBox.Show("Bạn đã thua!");
                            }

                            break;
                        }

                    case MessageType.PropertyUpdatedEvent:
                        {
                            var data = JsonSerializer.Deserialize<PropertyUpdatedEvent>(env.Payload, JsonOpt);

                            if (data.PropertyTileIndex == -1)
                            {
                                MessageBox.Show("Số tiền hiện tại không đủ!");
                                btnEndTurn.Enabled = true;
                                break;
                            }
                            
                            lbHistory.Items.Add("Property đã cập nhật");
                            btnEndTurn.Enabled = true;
                            break;
                        }

                    case MessageType.PlayerLeftEvent:
                        {
                            var data = JsonSerializer.Deserialize<PlayerLeftEvent>(env.Payload, JsonOpt);
                            lbHistory.Items.Add($"Đến lượt Player {data.PlayerId}");

                            btnRollDice.Enabled = (data.PlayerId == ClientSession.PlayerID);
                            break;
                        }

                    case MessageType.ChatMessageEvent:
                        {
                            var data = JsonSerializer.Deserialize<ChatMessageEvent>(env.Payload, JsonOpt);
                            
                            if (data.PlayerId != ClientSession.PlayerID)
                            {
                                lbChat.Items.Add($"{data.PlayerName}: {data.Message}");
                                lbChat.TopIndex = lbChat.Items.Count - 1;
                            }
                            
                            break;
                        }
                }
            });
        }

    }
}

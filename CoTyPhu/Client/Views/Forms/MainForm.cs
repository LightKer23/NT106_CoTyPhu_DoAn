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

        private static readonly HashSet<int> ChanceTiles = new HashSet<int> { 7, 22, 36 };
        private static readonly HashSet<int> CommunityChestTiles = new HashSet<int> { 2, 17, 33 };

        private int _nextChanceCardIndex = -1; 
        private int _nextCommunityChestCardIndex = -1;

        private Dictionary<int, PictureBox> _tileMap;           
        private Dictionary<int, Panel> _tileTokenPanels;         
        private Dictionary<int, PictureBox> _playerTokens;      
        private Dictionary<int, int> _playerTile;

        // ✅ TRACKING OWNERSHIP INDICATORS
        private Dictionary<int, Label> _propertyOwnershipLabels = new Dictionary<int, Label>();

        // ✅ TRACKING JAIL STATUS & INDICATORS
        private Dictionary<int, bool> _playerInJail = new Dictionary<int, bool>();
        private Dictionary<int, Label> _jailIndicators = new Dictionary<int, Label>();

        private Dictionary<int, int> _playerMoney = new Dictionary<int, int>();
        private Dictionary<int, string> _playerNames = new Dictionary<int, string>();
        private bool _isShowingCard = false;

        private bool _isRollingDice = false;
        private System.Windows.Forms.Timer _diceAnimationTimer;
        private Random _random = new Random();
        private int _animationTicks = 0;
        
        private int _finalDice1 = 0;
        private int _finalDice2 = 0;
        private int _currentPlayerId = 0;
        
        // ✅ TRACKING CARD MOVEMENT
        private int _cardMovementFromTile = -1;
        private int _cardMovementToTile = -1;
        
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
<<<<<<< Updated upstream
            btnSend.Click += BtnSend_Click;

            textBox2.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    BtnSend_Click(s, e);
                }
            };
=======
            this.FormClosing += MainForm_FormClosing;

>>>>>>> Stashed changes

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
                
                // ✅ KIỂM TRA: Nếu Roll1=0 và Roll2=0 → Di chuyển từ card
                if (_finalDice1 == 0 && _finalDice2 == 0)
                {
                    lbHistory.Items.Add($"🎴 Player {_currentPlayerId} di chuyển theo hiệu ứng thẻ...");
                }
                else
                {
                    lbHistory.Items.Add($"Player {_currentPlayerId} tung được {_finalDice1} và {_finalDice2}");
                }
                
                if (_playerTile.TryGetValue(_currentPlayerId, out int fromTile))
                {
                    int steps;
                    int toTile;
                    
                    if (_finalDice1 == 0 && _finalDice2 == 0)
                    {
                        // ✅ Di chuyển từ card: Sử dụng _cardMovementFromTile và _cardMovementToTile
                        if (_cardMovementToTile >= 0)
                        {
                            int actualFrom = _cardMovementFromTile >= 0 ? _cardMovementFromTile : fromTile;
                            toTile = _cardMovementToTile;
                            
                            // Tính số bước để animate
                            if (toTile >= actualFrom)
                            {
                                steps = toTile - actualFrom;
                            }
                            else
                            {
                                // Đi qua GO (vòng bàn cờ)
                                steps = (40 - actualFrom) + toTile;
                            }
                            
                            _ = AnimateTokenMovement(_currentPlayerId, actualFrom, toTile, steps);
                        }
                        return;
                    }
                    else
                    {
                        // Di chuyển bình thường từ xúc xắc
                        steps = _finalDice1 + _finalDice2;
                        toTile = (fromTile + steps) % 40;
                        _ = AnimateTokenMovement(_currentPlayerId, fromTile, toTile, steps);
                    }
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

                // ✅ TẠO OWNERSHIP INDICATORS
                CreateOwnershipIndicators();

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
                    _playerInJail[p.PlayerId] = false; // ✅ Initialize jail status
                    
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
            lbHistory.Items.Add($"[DEBUG] BtnRollDice_Click: MyID={ClientSession.PlayerID}, CurrentTurn=?");
            
            btnEndTurn.Enabled = true;
            btnRollDice.Enabled = false;

            try
            {
                await ClientSession.Tcp.RollDiceAsync(
                    ClientSession.MatchID,
                    ClientSession.PlayerID
                );
            }
            catch (Exception ex)
            {
                lbHistory.Items.Add($"[ERROR] RollDice failed: {ex.Message}");
                btnRollDice.Enabled = true; // Re-enable để user thử lại
                btnEndTurn.Enabled = false;
            }
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

<<<<<<< HEAD
        // ✅ TẠO OWNERSHIP INDICATOR CHO MỖI Ô ĐẤT
        private void CreateOwnershipIndicators()
        {
            foreach (int tileIndex in PropertyTiles)
            {
                if (!_tileMap.TryGetValue(tileIndex, out var tilePb))
                    continue;

                // Tạo label để hiển thị ownership
                var label = new Label
                {
                    Name = $"lblOwner_{tileIndex}",
                    Size = new Size(60, 20),
                    BackColor = Color.Transparent,
                    ForeColor = Color.White,
                    Font = new Font("Arial", 8, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Visible = false, // Ẩn khi chưa có chủ
                    Location = new Point(5, 5) // Góc trên bên trái
                };

                tilePb.Controls.Add(label);
                label.BringToFront();

                _propertyOwnershipLabels[tileIndex] = label;
            }
        }

        // ✅ CẬP NHẬT HIỂN THỊ OWNERSHIP
        private void UpdatePropertyOwnership(int tileIndex, int? ownerId, int houseCount, bool hasHotel, string propertyType)
        {
            if (!_propertyOwnershipLabels.TryGetValue(tileIndex, out var label))
                return;

            if (ownerId == null)
            {
                // Chưa có chủ
                label.Visible = false;
                return;
            }

            // Xác định màu theo player
            Color ownerColor = ownerId.Value switch
            {
                1 => Color.FromArgb(255, 68, 68),    // Đỏ
                2 => Color.FromArgb(68, 138, 255),   // Xanh dương
                3 => Color.FromArgb(76, 175, 80),    // Xanh lá
                4 => Color.FromArgb(255, 193, 7),    // Vàng
                _ => Color.Gray
            };

            label.BackColor = ownerColor;
            label.Visible = true;

            // Hiển thị text dựa vào loại property
            if (propertyType == "Property")
            {
                if (hasHotel)
                {
                    label.Text = "🏨 H"; // Khách sạn
                }
                else if (houseCount > 0)
                {
                    label.Text = $"🏠 {houseCount}"; // Số nhà
                }
                else
                {
                    label.Text = $"P{ownerId}"; // Chỉ đất trống
                }
            }
            else if (propertyType == "RailRoad")
            {
                label.Text = $"🚂 P{ownerId}";
            }
            else if (propertyType == "Utility")
            {
                label.Text = $"⚡ P{ownerId}";
            }
        }

        // ✅ TẠO JAIL INDICATOR CHO PLAYER TOKEN
        private Label CreateJailIndicator(int playerId)
        {
            var label = new Label
            {
                Size = new Size(20, 20),
                BackColor = Color.Red,
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold),
                Text = "🔒",
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false, // Ẩn ban đầu
                Location = new Point(0, 0) // Sẽ được set khi hiển thị
            };

            return label;
        }

        // ✅ CẬP NHẬT JAIL INDICATOR
        private void UpdateJailIndicator(int playerId, bool inJail)
        {
            _playerInJail[playerId] = inJail;

            if (!_playerTokens.TryGetValue(playerId, out var token))
                return;

            // Tạo hoặc lấy jail indicator
            if (!_jailIndicators.TryGetValue(playerId, out var indicator))
            {
                indicator = CreateJailIndicator(playerId);
                _jailIndicators[playerId] = indicator;
                
                // Add vào parent của token
                if (token.Parent != null)
                {
                    token.Parent.Controls.Add(indicator);
                    indicator.BringToFront();
                }
            }

            if (inJail)
            {
                // Hiển thị icon tù trên token
                indicator.Location = new Point(token.Left + token.Width - 20, token.Top);
                indicator.Visible = true;
                indicator.BringToFront();
            }
            else
            {
                // Ẩn icon tù
                indicator.Visible = false;
            }
        }

        private async void ShowCardWithDelay(string cardType, int cardIndex, int delayMs = 300)
        {
            try
            {
                _isShowingCard = true;

                lbHistory.Items.Add($"[ShowCard] {cardType}_{cardIndex}");
                await Task.Delay(delayMs);

                string prefix = cardType.Equals("Chance", StringComparison.OrdinalIgnoreCase) ? "CH" : "KV";

                // nếu Resources bắt đầu từ 1 mà server gửi 0-based thì mở dòng này:
                // cardIndex += 1;

                string resourceName = $"{prefix}_{cardIndex}";
                lbHistory.Items.Add($"[ShowCard] Load {resourceName}");

                var obj = Properties.Resources.ResourceManager.GetObject(resourceName);
                if (obj is not Image img)
                {
                    lbHistory.Items.Add($"[Warning] Không tìm thấy resource: {resourceName}");
                    return;
                }

                // UI thread
                if (pbTile.InvokeRequired)
                {
                    pbTile.BeginInvoke(new Action(() =>
                    {
                        pbTile.Visible = true;
                        pbTile.BringToFront();
                        pbTile.Image = img;
                        pbTile.SizeMode = PictureBoxSizeMode.StretchImage;
                        pbTile.Refresh();
                    }));
                }
                else
                {
                    pbTile.Visible = true;
                    pbTile.BringToFront();
                    pbTile.Image = img;
                    pbTile.SizeMode = PictureBoxSizeMode.StretchImage;
                    pbTile.Refresh();
                }

                lbHistory.Items.Add($"[ShowCard] OK: {resourceName}");

                // giữ ảnh đủ lâu để thấy
                await Task.Delay(2500);
            }
            catch (Exception ex)
            {
                lbHistory.Items.Add($"[ERROR] ShowCard: {ex.Message}");
            }
            finally
            {
                _isShowingCard = false;

                // nếu bạn có ShowPropertyCard thì refresh lại ô hiện tại
                // if (_playerTile.TryGetValue(ClientSession.PlayerID, out var cur))
                //     ShowPropertyCard(cur);
            }
        }



=======
<<<<<<< Updated upstream
=======
        private bool _surrenderSent = false;

        private async void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // Nếu đã gửi rồi thì thôi
            if (_surrenderSent)
                return;

            // Nếu socket chưa kết nối thì thôi
            if (ClientSession.Tcp == null || !ClientSession.Tcp.IsConnected)
                return;

            _surrenderSent = true;

            try
            {
                // ❗ KHÔNG cancel Close
                // chỉ gửi 1 gói tin nhanh
                await ClientSession.Tcp.PlayerSurrenderAsync(ClientSession.MatchID, ClientSession.PlayerID);
            }
            catch
            {  }
        }


        // ✅ DI CHUYỂN TOKEN VỚI ANIMATION
>>>>>>> Stashed changes
>>>>>>> 15183d361ab428d73147b1f1c205831606163cce
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
                // Kiểm tra xem ô đích có phải là Chance/CommunityChest không
                if (!ChanceTiles.Contains(toTile) && !CommunityChestTiles.Contains(toTile))
                {
                    ShowPropertyCard(toTile);
                }
                // ✅ KHÔNG LÀM GÌ VỚI pbTile cho ô Chance/CommunityChest
                // Để DrawCardEvent xử lý việc hiển thị thẻ
            }
        }

        // ✅ ANIMATION ĐI VÀO TÙ (THẲNG, KHÔNG ĐI TỪNG BƯỚC)
        private async Task AnimateToJail(int playerId, int fromTile, int jailTile, string reason)
        {
            if (!_playerTokens.ContainsKey(playerId))
                return;

            lbHistory.Items.Add($"🚔 Player {playerId} bị bắt vào tù! Lý do: {GetJailReasonText(reason)}");

            // Hiệu ứng nhấp nháy trước khi đi
            var token = _playerTokens[playerId];
            for (int i = 0; i < 3; i++)
            {
                token.Visible = false;
                await Task.Delay(150);
                token.Visible = true;
                await Task.Delay(150);
            }

            // Di chuyển thẳng đến tù (không đi từng bước)
            int slot = FindAvailableSlot(jailTile, playerId);
            PlaceTokenOnTile(playerId, jailTile, slot);

            // Hiển thị jail indicator
            UpdateJailIndicator(playerId, true);

            // Hiệu ứng khi đến tù
            for (int i = 0; i < 2; i++)
            {
                token.BackColor = Color.Red;
                await Task.Delay(200);
                token.BackColor = Color.Transparent;
                await Task.Delay(200);
            }

            lbHistory.Items.Add($"🔒 Player {playerId} đã vào tù tại ô {jailTile}");
        }

        private string GetJailReasonText(string reason)
        {
            return reason switch
            {
                "GoToJail" => "Đi trúng ô 'Vào Tù'",
                "ThreeDoubles" => "Tung 3 xúc xắc đôi liên tiếp",
                "ChanceCard" => "Rút thẻ Cơ Hội 'Vào Tù'",
                "CommunityChestCard" => "Rút thẻ Khí Vận 'Vào Tù'",
                _ => reason
            };
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
                            
                            // ✅ LƯU FROM/TO TILE NẾU CÓ (từ card movement)
                            _cardMovementFromTile = data.FromTile ?? -1;
                            _cardMovementToTile = data.ToTile ?? -1;
                            
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
<<<<<<< Updated upstream
                            var data = JsonSerializer.Deserialize<PropertyUpdatedEvent>(env.Payload, JsonOpt);

<<<<<<< HEAD
                            // ✅ HIỂN THỊ MESSAGEBOX NẾU CÓ LỖI
                            if (!data.Success)
=======
                            if (data.PropertyTileIndex == -1)
=======
                            var data = JsonSerializer.Deserialize<AskBuyPropertyEvent>(env.Payload, JsonOpt);


                            if (data.TileIndex == -1)
>>>>>>> Stashed changes
>>>>>>> 15183d361ab428d73147b1f1c205831606163cce
                            {
                                MessageBox.Show(data.Message ?? "Có lỗi xảy ra!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                btnEndTurn.Enabled = true;
                                break;
                            }
                            
                            // ✅ THÀNH CÔNG
                            if (!string.IsNullOrEmpty(data.Message))
                            {
                                lbHistory.Items.Add(data.Message);
                            }
                            
                            btnEndTurn.Enabled = true;
                            break;
                        }

                    case MessageType.PlayerLeftEvent:
                        {
                            var data = JsonSerializer.Deserialize<PlayerLeftEvent>(env.Payload, JsonOpt);
                            lbHistory.Items.Add($"Đến lượt Player {data.PlayerId}");
                            lbHistory.Items.Add($"[DEBUG] PlayerLeftEvent: TurnPlayerId={data.PlayerId}, MyId={ClientSession.PlayerID}");

                            bool isMyTurn = (data.PlayerId == ClientSession.PlayerID);
                            btnRollDice.Enabled = isMyTurn;
                            
                            lbHistory.Items.Add($"[DEBUG] btnRollDice.Enabled = {btnRollDice.Enabled}");
                            
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

                    // ✅ XỬ LÝ RÚT THẺ CƠ HỘI/KHÍ VẬN
                    case MessageType.DrawCardEvent:
                        {
                            var data = JsonSerializer.Deserialize<DrawCardEvent>(env.Payload, JsonOpt);
                            
                            lbHistory.Items.Add($"[DEBUG] Received DrawCardEvent: Player={data.PlayerId}, Type={data.CardType}, Index={data.CardIndex}");
                            lbHistory.Items.Add($"Player {data.PlayerId} rút thẻ {(data.CardType == "Chance" ? "Cơ Hội" : "Khí Vận")}: {data.Description}");
                            
                            // ✅ CHỈ HIỂN THỊ THẺ CHO NGƯỜI CHƠI HIỆN TẠI
                            if (data.PlayerId == ClientSession.PlayerID)
                            {
                                lbHistory.Items.Add($"[DEBUG] This is MY card, showing image with 500ms delay...");
                                ShowCardWithDelay(data.CardType, data.CardIndex, delayMs: 500);
                            }
                            else
                            {
                                lbHistory.Items.Add($"[DEBUG] This is OTHER player's card, not showing image");
                            }
                            

                            break;
                        }

                    // ✅ XỬ LÝ THAY ĐỔI QUYỀN SỞ HỮU ĐẤT
                    case MessageType.PropertyOwnershipChangedEvent:
                        {
                            var data = JsonSerializer.Deserialize<PropertyOwnershipChangedEvent>(env.Payload, JsonOpt);
                            

                            UpdatePropertyOwnership(
                                data.TileIndex, 
                                data.OwnerId, 
                                data.HouseCount, 
                                data.HasHotel, 
                                data.PropertyType
                            );
                            
                            // Log để biết ownership đã thay đổi
                            if (data.OwnerId.HasValue)
                            {
                                string status = data.HasHotel ? "Khách sạn" : 
                                               data.HouseCount > 0 ? $"{data.HouseCount} nhà" : "Đất trống";
                                lbHistory.Items.Add($"Ô {data.TileIndex}: Player {data.OwnerId} - {status}");
                            }
                            else
                            {
                                lbHistory.Items.Add($"Ô {data.TileIndex}: Không còn chủ");
                            }
                            

                            break;
                        }

                    // ✅ XỬ LÝ PLAYER VÀO TÙ
                    case MessageType.PlayerJailedEvent:
                        {
                            var data = JsonSerializer.Deserialize<PlayerJailedEvent>(env.Payload, JsonOpt);
                            
                            lbHistory.Items.Add($"[DEBUG] PlayerJailedEvent: Player={data.PlayerId}, Reason={data.Reason}, From={data.FromTile} To={data.ToTile}");
                            
                            // Animation đi vào tù
                            _ = AnimateToJail(data.PlayerId, data.FromTile, data.ToTile, data.Reason);
                            
                            break;
                        }
                }
            });
        }

    }
}

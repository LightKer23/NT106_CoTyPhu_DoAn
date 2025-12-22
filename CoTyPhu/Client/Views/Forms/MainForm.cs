using Client.Services.Network;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Views.Forms
{
    public partial class MainForm : Form
    {
        private Dictionary<int, PictureBox> _tileMap;           
        private Dictionary<int, Panel> _tileTokenPanels;         
        private Dictionary<int, PictureBox> _playerTokens;      
        private Dictionary<int, int> _playerTile;                

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

            this.Shown += async (_, __) => await InitTokensFromRoomAsync();
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

                for (int i = 0; i < players.Count && i < 4; i++)
                {
                    var p = players[i];
                    var token = CreatePlayerToken(p.PlayerId, p.CharacterIndex);

                    _playerTokens[p.PlayerId] = token;
                    _playerTile[p.PlayerId] = 0;

                    PlaceTokenOnTile(p.PlayerId, 0, slotIndex: i);
                }
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
        }
    }
}

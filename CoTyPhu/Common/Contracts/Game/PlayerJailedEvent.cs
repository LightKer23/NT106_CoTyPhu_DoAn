using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Contracts.Game
{
    public class PlayerJailedEvent
    {
        public int PlayerId { get; set; }           // Player bị vào tù
        public string Reason { get; set; }          // "GoToJail", "ThreeDoubles", "ChanceCard", "CommunityChestCard"
        public int FromTile { get; set; }           // Vị trí trước khi vào tù
        public int ToTile { get; set; }             // Vị trí tù (thường là 10)
    }
}

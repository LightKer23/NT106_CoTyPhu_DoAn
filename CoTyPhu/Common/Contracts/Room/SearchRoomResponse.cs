using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Contracts.Room
{
    public class SearchRoomResponse
    {
        public bool Success { get; set; }
        public int RoomId { get; set; }

        public List<int> PlayerRooms { get; set; } = new(); 

        public List<RoomPlayerInfo> Players { get; set; } = new();
    }

    public class RoomPlayerInfo
    {
        public int PlayerId { get; set; }        
        public int CharacterIndex { get; set; }  
        public string DisplayName { get; set; } = string.Empty;
    }
}


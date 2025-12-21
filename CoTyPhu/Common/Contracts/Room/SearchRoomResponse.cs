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
    }
}

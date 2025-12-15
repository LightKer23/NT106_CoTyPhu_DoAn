using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Constracts.Room
{
    public class CreateRoomResponse
    {
        public bool Success { get; set; }
        public int RoomID { get; set; }
        public string Message { get; set; }
    }
}

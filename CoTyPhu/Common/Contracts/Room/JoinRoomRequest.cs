using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Constracts.Room
{
    public class JoinRoomRequest
    {
        public int RoomID { get; set; }
        public int AccountID { get; set; }
        public int CharacterIndex { get; set; }
    }
}

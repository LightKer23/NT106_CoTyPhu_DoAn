using Common.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Constracts.Room
{
    public class JoinRoomResponse
    {
        public bool Success { get; set; }
        public int PlayerID { get; set; }          // IDPlayer trong trận
        public int MatchID { get; set; }           // trận tương ứng
        public List<Player> Players { get; set; }  // danh sách player trong phòng
        public string Message { get; set; }
    }
}

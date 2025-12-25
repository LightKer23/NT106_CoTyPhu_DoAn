using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Models.Entities
{
    public class Player
    {
        public int IDPlayer { get; set; }
        public int IDMatch { get; set; }
        public int IDAccount { get; set; }

        public int? Rank { get; set; }
        public DateTime? CrashTime { get; set; }

        public string Status { get; set; }   // Waiting | Playing | Bankrupt | Crash
    }
}

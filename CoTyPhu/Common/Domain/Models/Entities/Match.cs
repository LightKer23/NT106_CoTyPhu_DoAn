using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Models.Entities
{
    public class Match
    {
        public int IDMatch { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public int NumberPlayer { get; set; }   // số người trong phòng
        public int Turn { get; set; }           // IDPlayer đang tới lượt
        public string Status { get; set; }      // Waiting | Playing | End
    }
}

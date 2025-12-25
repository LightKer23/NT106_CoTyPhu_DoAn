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
        public int NumberPlayer { get; set; }   // số người trong trận
        public int Turn { get; set; }           // IDPlayer đang tới lượt
        public string Status { get; set; }      // Waiting / Playing / End
    }
}

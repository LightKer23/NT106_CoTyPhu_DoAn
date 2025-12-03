using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Infrastructure.Database.Models
{
    public class Match
    {
        public int IDMatch { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int NumberPlayer { get; set; }
        public string Status { get; set; }
    }
}

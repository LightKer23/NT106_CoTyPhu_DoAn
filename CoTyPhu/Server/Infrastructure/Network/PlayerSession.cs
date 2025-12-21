using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Infrastructure.Network
{
    public class PlayerSession
    {
        public int AccountId { get; set; }
        public int PlayerId { get; set; }
        public int? MatchId { get; set; }

        public ClientConnection Connection { get; set; }

        public bool IsOnline => Connection != null;
    }
}

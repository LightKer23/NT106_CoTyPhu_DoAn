using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Contracts.Game
{
    public class ChatMessageEvent
    {
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public string Message { get; set; }
    }
}

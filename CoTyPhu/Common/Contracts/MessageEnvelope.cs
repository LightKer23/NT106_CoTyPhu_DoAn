using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Constracts
{
    public class MessageEnvelope
    {
        public MessageType Type { get; set; }
        public string Payload { get; set; }   // JSON của body

        public MessageEnvelope() { }

        public MessageEnvelope(MessageType type, string payload)
        {
            Type = type;
            Payload = payload;
        }

    }
}

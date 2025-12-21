using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Constracts
{
    public class MessageEnvelope
    {
        public Guid MessageId { get; set; } // Dùng để ghép Request, Response

        public MessageType Type { get; set; } // Phân biệt loại message

        public int? MatchId { get; set; }
        public int? PlayerId { get; set; }

        public string Payload { get; set; }   // Nội dung chính (JSON)


        // Constructor tiện dùng
        public MessageEnvelope()
        {
            MessageId = Guid.NewGuid();
        }

        public MessageEnvelope(
            MessageType type,
            string payload,
            int? matchId = null,
            int? playerId = null)
        {
            MessageId = Guid.NewGuid();
            Type = type;
            Payload = payload;
            MatchId = matchId;
            PlayerId = playerId;
        }
    }
}

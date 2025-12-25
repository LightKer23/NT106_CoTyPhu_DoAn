using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Domain.Models.Entities;

namespace Common.Contracts.Auth
{
    public class GetMatchHistoryResponse
    {
        public bool Success { get; set; }
        public List<MatchHistoryItem> History { get; set; } = new();
    }

    public class MatchHistoryItem
    {
        public int MatchId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? Rank { get; set; }
        public string Status { get; set; }   // Win / Lose / Bankrupt
    }
}

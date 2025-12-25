using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Contracts.Game
{
    public class GetOutOfJailResponse
    {
        public bool Success { get; set; }
        public string Method { get; set; }      // PayMoney | UseCard | RollDice
        public string Message { get; set; }     // Thông báo cho user
        public int? MoneyPaid { get; set; }     // Số tiền đã trả (nếu Method = PayMoney)
        public bool UsedCard { get; set; }      // Đã dùng thẻ hay chưa
    }
}

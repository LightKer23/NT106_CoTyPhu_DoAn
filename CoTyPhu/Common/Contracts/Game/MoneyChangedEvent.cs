using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Contracts.Game
{
    public class MoneyChangedEvent
    {
        public int PlayerId { get; set; }
        public int CurrentMoney { get; set; }  // Số tiền hiện tại sau khi thay đổi
        public int MoneyChange { get; set; }   // Số tiền thay đổi (+/-)
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Constracts.Game
{
    public class BuyDecisionRequest
    {
        public int PropertyID { get; set; }
        public bool Accept { get; set; }   // true = đồng ý mua, false = từ chối
    }
}

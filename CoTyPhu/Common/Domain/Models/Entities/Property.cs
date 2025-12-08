using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Models.Entities
{
    public class Property
    {
        public int IDProperty { get; set; }
        public int IDMatch { get; set; }          // Property thuộc trận nào
        public string Name { get; set; }
        public int Value { get; set; }
        public int Level { get; set; }
        public string TypeProperty { get; set; }    //Property, Railroad, Utility
        public int? PlayerID { get; set; }        // Chủ sở hữu, null = không ai sở hữu
    }
}

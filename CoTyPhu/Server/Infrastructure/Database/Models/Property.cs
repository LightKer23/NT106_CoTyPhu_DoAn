using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Infrastructure.Database.Models
{
    public class Property
    {
        public int IDProperty { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public int Level { get; set; }
    }
}

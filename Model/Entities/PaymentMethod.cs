using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class PaymentMethod
    {
        public int CodeMethod { get; set; } 
        public string NameMethod { get; set; }
        public bool IsActive { get; set; }
    }
}

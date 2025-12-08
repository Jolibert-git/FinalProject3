using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class PaymentStatus
    {
        public string StatusCode { get; set; } 
        public string StatusName { get; set; }
        public bool IsFinal { get; set; }
    }
}

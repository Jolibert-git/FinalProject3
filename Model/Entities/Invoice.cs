using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class Invoice
    {
        public InvoiceHeader Header { get; set; }
        public List<InvoiceDetail> Details { get; set; } = new List<InvoiceDetail>();
    }
}

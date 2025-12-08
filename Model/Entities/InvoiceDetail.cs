using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class InvoiceDetail
    {
        public int CodeDetailInvoice { get; set; }
        public int CodeInvoiceHeader { get; set; }
        public string CodeProduct { get; set; }
        public string NameProduct { get; set; }
        public string UnitOfMeasure { get; set; } 
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal CostProduct { get; set; }
        public decimal TaxRate { get; set; }
        public decimal LastPrice { get; set; } 
        public decimal TotalLine { get; set; }
        public string StatusDetail { get; set; } = "ACTIVA"; 

        public InvoiceDetail()
        {
        }
    }
}

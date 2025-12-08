using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Entities
{
    public class InvoiceHeader
    {
        public int CodeInvoiceHeader { get; set; }
        public DateTime DateHeader { get; set; }
        public string CodeCustomer { get; set; }
        public DateTime? DueDateHeader { get; set; }
        public decimal TotalTaxHeader { get; set; }
        public decimal SubtotalHeader { get; set; }
        public decimal TotalAmountHeader { get; set; }
        public string StatusCode { get; set; }
        public int PaymentMethodCode { get; set; }
        public decimal DiscountRate { get; set; }
        public string NCF { get; set; }
        public string RNC { get; set; }
        public string CashRegisterNumber { get; set; }
    }
}

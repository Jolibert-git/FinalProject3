using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Model.Entities
{
    public class Payment
    {
        public int CodePayment { get; set; }
        [Required(ErrorMessage = "El código de la factura es obligatorio.")]
        public int CodeInvoiceHeader { get; set; }
        [Required(ErrorMessage = "La fecha de pago es obligatoria.")]
        public DateTime PaymentDate { get; set; }
        [Required(ErrorMessage = "El monto pagado es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor que cero.")]
        public decimal AmountPaid { get; set; }
        [Required(ErrorMessage = "El código del método de pago es obligatorio.")]
        public int CodeMethod { get; set; }
        [StringLength(50)]
        public string ReferenceNumber { get; set; }
        [StringLength(255)]
        public string PaymentNote { get; set; }
        public string PaymentStatus { get; set; } = "ACTIVO";
        public string MethodName { get; set; }
    }
}

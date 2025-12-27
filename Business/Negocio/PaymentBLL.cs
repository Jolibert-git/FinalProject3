using DataAccess.Data;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Negocio
{
    public class PaymentBLL
    {
        //private readonly PaymentDAL _paymentDAL;
        private readonly PaymentDAL _paymentDAL;
        public PaymentBLL(PaymentDAL _paymentDAL)
        {
            // Inicializa la capa de acceso a datos (DAL)
            this._paymentDAL = _paymentDAL;
        }

        // -------------------------------------------------------------------
        // 1. INSERTAR PAGO ➕

        public int InsertPayment(Payment payment)
        {
            try
            {
                if (payment.CodeInvoiceHeader <= 0)
                {
                    throw new ArgumentException("Debe especificar el código de la factura (CodeInvoiceHeader).");
                }
                if (payment.AmountPaid <= 0)
                {
                    throw new ArgumentException("El monto pagado debe ser mayor que cero.");
                }
                if (payment.CodeMethod <= 0)
                {
                    throw new ArgumentException("Debe especificar un método de pago válido (CodeMethod).");
                }

                return _paymentDAL.InsertPayment(payment);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error BLL al insertar pago: {ex.Message}");
                // Se relanza una excepción de capa superior para que la interfaz de usuario la maneje.
                throw new Exception("Error al procesar el pago. Por favor, verifique los datos. Detalle: " + ex.Message);
            }
        }

        // -------------------------------------------------------------------
        // 2. ACTUALIZAR PAGO 🔄
        public bool UpdatePayment(Payment payment)
        {
            try
            {
                if (payment.CodePayment <= 0)
                {
                    throw new ArgumentException("El código de pago es inválido para la actualización.");
                }
                if (payment.AmountPaid <= 0)
                {
                    throw new ArgumentException("El monto pagado debe ser mayor que cero.");
                }
                if (payment.CodeMethod <= 0)
                {
                    throw new ArgumentException("Debe especificar un método de pago válido.");
                }

                return _paymentDAL.UpdatePayment(payment);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error BLL al actualizar pago: {ex.Message}");
                throw new Exception("Error al actualizar el pago. Detalle: " + ex.Message);
            }
        }

        // -------------------------------------------------------------------
        // 3. ANULAR PAGO ❌
        public bool AnularPayment(int codePayment)
        {
            try
            {
                // **Validación de Negocio**
                if (codePayment <= 0)
                {
                    throw new ArgumentException("El código de pago es inválido para la anulación.");
                }

                // El DAL se encarga de llamar al SP 'AnularPayment'
                return _paymentDAL.AnularPayment(codePayment);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error BLL al anular pago: {ex.Message}");
                throw new Exception("Error al anular el pago. Detalle: " + ex.Message);
            }
        }

        // -------------------------------------------------------------------
        // 4. LEER PAGOS POR ID DE FACTURA 📋
        public List<Payment> GetPaymentsByInvoice(int codeInvoiceHeader)
        {
            try
            {
                // **Validación de Negocio**
                if (codeInvoiceHeader <= 0)
                {
                    throw new ArgumentException("El código de factura es inválido.");
                }

                return _paymentDAL.ReadPaymentsByInvoice(codeInvoiceHeader);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error BLL al leer pagos: {ex.Message}");
                throw new Exception("Error al obtener la lista de pagos. Detalle: " + ex.Message);
            }
        }

        // -------------------------------------------------------------------
        // 5. OBTENER PAGO POR ID (INDIVIDUAL) 🔍

        public Payment GetPaymentById(int codePayment)
        {
            try
            {
                // **Validación de Negocio**
                if (codePayment <= 0)
                {
                    throw new ArgumentException("El código de pago es inválido.");
                }
                return _paymentDAL.GetPaymentById(codePayment);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error BLL al obtener pago por ID: {ex.Message}");
                throw new Exception("Error al obtener el pago individual. Detalle: " + ex.Message);
            }
        }

        public List<PaymentMethod> GetPaymentMethods()
        {
            // Simplemente llama al método de la capa de datos
            return _paymentDAL.GetPaymentMethods();
        }

    }
}

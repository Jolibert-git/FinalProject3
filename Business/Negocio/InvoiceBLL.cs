using DataAccess.Data;
using Microsoft.Data.SqlClient;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Negocio
{
    public class InvoiceBLL
    {
        private readonly InvoiceDAL invoiceDAL = new InvoiceDAL();
        private readonly CustomerBLL customerBLL = new CustomerBLL();
        private readonly InvoiceDetailDAL invoiceDetailDAL = new InvoiceDetailDAL();
        private readonly StockMovementBLL stockMovementBLL = new StockMovementBLL();

        private readonly ProductBLL productBLL = new ProductBLL();
        private readonly DBHelper dbHelper = new DBHelper();

        //private readonly string connectionString = "Data Source=DESKTOP-OA67FE6\\SQLEXPRESS;DATABASE=GcompleteQuery;Integrated Security=True;TrustServerCertificate=True;";

        // -------------------------------------------------------------------
        // 1. CREAR FACTURA COMPLETA (TRANSACCIONAL) ➕

        public int CreateInvoice(Invoice invoice)
        {

            using (SqlConnection connection = dbHelper.OpenConnection())
            {
                SqlTransaction transaction = dbHelper.GetTrasation();
                int newHeaderID = 0;

                try
                {
                    newHeaderID = invoiceDAL.InsertInvoiceHeader(invoice.Header, connection, transaction);

                    if (newHeaderID <= 0)
                    {
                        throw new Exception("Error interno: El ID de la factura no fue generado.");
                    }

                    foreach (var detail in invoice.Details)
                    {
                        detail.CodeInvoiceHeader = newHeaderID;
                        invoiceDetailDAL.InsertInvoiceDetail(detail, connection, transaction);

                        StockMovement movement = new StockMovement
                        {
                            CodeProduct = detail.CodeProduct,
                            MovementQuantity = detail.Quantity,
                            MovementType = "VENTA",
                            NameMovement = $"Salida por Factura No. {newHeaderID}",
                            Operation = "S"
                        };

                        stockMovementBLL.InsertStockMovement(movement, connection, transaction);
                    }
                    transaction.Commit();
                    return newHeaderID;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new ApplicationException($"Error crítico al crear la factura. Transacción revertida. Detalle: {ex.Message}", ex);
                }
            }
        }


        // -------------------------------------------------------------------
        // 2. OBTENER FACTURA POR CÓDIGO 🔍

        public InvoiceHeader GetInvoiceHeader(int code)
        {
            if (code <= 0)
            {
                throw new ArgumentException("El código de factura es inválido.");
            }
            try
            {
                return invoiceDAL.ReadInvoiceHeaders(code, null, null).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al buscar la cabecera de la factura.", ex);
            }
        }

        // -------------------------------------------------------------------
        // 3. OBTENER FACTURAS POR FILTRO 📋

        public List<InvoiceHeader> ReadInvoiceHeaders(int? codeInvoiceHeader = null, string codeCustomer = null, string invoiceStatus = null)
        {
            try
            {
                return invoiceDAL.ReadInvoiceHeaders(codeInvoiceHeader, codeCustomer, invoiceStatus);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al cargar la lista de facturas.", ex);
            }
        }

        // -------------------------------------------------------------------
        // 4. ANULAR FACTURA ❌

        public bool CancelInvoice(int codeInvoiceHeader)
        {
            if (codeInvoiceHeader <= 0)
            {
                throw new ArgumentException("El código de factura es inválido para anular.");
            }


            try
            {
                return invoiceDAL.CancelInvoiceHeader(codeInvoiceHeader);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al intentar anular la factura.", ex);
            }
        }




        // -------------------------------------------------------------------
        // 5. MÉTODOS PRIVADOS DE LÓGICA DE NEGOCIO Y VALIDACIÓN

        private void CalculateInvoiceTotals(Invoice invoice)
        {
            if (invoice.Details == null || !invoice.Details.Any())
            {
                // Si no hay detalles, se asume que los totales son cero
                invoice.Header.SubtotalHeader = 0.00m;
                invoice.Header.TotalTaxHeader = 0.00m;
                invoice.Header.TotalAmountHeader = 0.00m;
                return;
            }

            decimal subtotal = invoice.Details.Sum(d => d.Quantity * d.Price);
            decimal taxRate = 0.18m;

            invoice.Header.SubtotalHeader = subtotal;
            invoice.Header.TotalTaxHeader = subtotal * taxRate;
            invoice.Header.TotalAmountHeader = subtotal + invoice.Header.TotalTaxHeader;
        }


        private void ValidateInvoice(Invoice invoice)
        {
            if (invoice == null || invoice.Header == null)
            {
                throw new ArgumentNullException(nameof(invoice), "El objeto Factura o su cabecera no puede ser nulo.");
            }

            // 1. Validar Cabecera
            if (invoice.Header.DateHeader == default(DateTime))
            {
                throw new ArgumentException("La fecha de la factura es obligatoria.");
            }
            if (!string.IsNullOrEmpty(invoice.Header.CodeCustomer))
            {
                if (customerBLL.GetCustomerByCode(invoice.Header.CodeCustomer) == null)
                {
                    throw new KeyNotFoundException($"El cliente con código '{invoice.Header.CodeCustomer}' no existe.");
                }
            }

            if (invoice.Details == null || !invoice.Details.Any())
            {
                throw new InvalidOperationException("La factura debe contener al menos un artículo de detalle.");
            }

            foreach (var detail in invoice.Details)
            {
                if (detail.Quantity <= 0)
                {
                    throw new ArgumentException($"La cantidad del producto '{detail.CodeProduct}' debe ser mayor a cero.");
                }
                if (detail.Price <= 0)
                {
                    throw new ArgumentException($"El precio del producto '{detail.CodeProduct}' debe ser mayor a cero.");
                }
            }

            if (invoice.Header.TotalAmountHeader <= 0.00m)
            {
                throw new InvalidOperationException("El monto total de la factura debe ser positivo.");
            }
        }

        public List<InvoiceHeader> GetInvoicesBeforeDate(DateTime limitDate)
        {

            if (limitDate == default(DateTime))
            {
                throw new ArgumentException("La fecha límite para la búsqueda es inválida.");
            }

            try
            {

                // Se trunca la hora para buscar hasta el final del día.
                DateTime searchLimit = limitDate.Date.AddDays(1).AddSeconds(-1);

                List<InvoiceHeader> headers = invoiceDAL.ReadInvoiceHeadersByDate(searchLimit);

                return headers.OrderByDescending(h => h.CodeInvoiceHeader).ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error BLL: No se pudo obtener el historial de facturas por fecha.", ex);
            }
        }


        public Invoice GetInvoiceById(int invoiceId)
        {
            if (invoiceId <= 0)
            {
                throw new ArgumentException("El ID de factura es inválido.");
            }


            InvoiceHeader header = GetInvoiceHeader(invoiceId);

            if (header == null)
            {
                return null;
            }

            // 2. Obtener los Detalles
            try
            {
                List<InvoiceDetail> details = invoiceDetailDAL.ReadDetailsByHeaderId(invoiceId);

                foreach (var detail in details)
                {
                    Product product = productBLL.GetProductById(detail.CodeProduct);

                    // Opcional: Solo si el código no es nulo/vacío
                    if (product != null)
                    {
                        detail.NameProduct = product.NameProduct;
                    }
                }


                return new Invoice
                {
                    Header = header,
                    Details = details
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error BLL: No se pudieron cargar los detalles de la factura ID {invoiceId}.", ex);
            }
        }
    }
}

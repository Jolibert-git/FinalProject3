using Microsoft.Data.SqlClient;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class InvoiceDetailDAL
    {
        private readonly DBHelper dbHelper = new DBHelper();
        // 1. INSERTAR DETALLE DE FACTURA

        public void InsertInvoiceDetail(InvoiceDetail detail, SqlConnection connection, SqlTransaction transaction)
        {
            string commandText = "InsertInvoiceDetail";

            using (SqlCommand command = new SqlCommand(commandText, connection, transaction))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Parámetros
                command.Parameters.AddWithValue("@CodeInvoiceHeader", detail.CodeInvoiceHeader);
                command.Parameters.AddWithValue("@CodeProduct", detail.CodeProduct);
                command.Parameters.AddWithValue("@NameProduct", (object)detail.NameProduct ?? DBNull.Value);
                command.Parameters.AddWithValue("@UnitOfMeasure", (object)detail.UnitOfMeasure ?? DBNull.Value);
                command.Parameters.AddWithValue("@Quantity", detail.Quantity);
                command.Parameters.AddWithValue("@Price", detail.Price);
                command.Parameters.AddWithValue("@CostProduct", detail.CostProduct);
                command.Parameters.AddWithValue("@TaxRate", detail.TaxRate);
                command.Parameters.AddWithValue("@TotalLine", detail.TotalLine);
                command.Parameters.AddWithValue("@StatusDetail", detail.StatusDetail);

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception($"DAL Error al insertar el detalle para el producto {detail.CodeProduct}.", ex);
                }
            }
        }



        // 2. ACTUALIZAR DETALLE DE FACTURA

        public bool UpdateInvoiceDetail(InvoiceDetail detail)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@codeDetailInvoice", detail.CodeDetailInvoice),
                    new SqlParameter("@quantitySold", detail.Quantity),
                    new SqlParameter("@unitPrice", detail.Price),
                    new SqlParameter("@taxRate", detail.TaxRate)
                };

                dbHelper.ExecuteNonQuery("UpdateInvoiceDetail", parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar detalle de factura: {ex.Message}");
                throw;
            }
        }

        // 3. ANULAR DETALLE DE FACTURA (Soft Delete)

        public bool AnularInvoiceDetail(int codeDetailInvoice)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@codeDetailInvoice", codeDetailInvoice)
                };

                // Llama al procedimiento que realiza la lógica de reversión de stock y totales.
                dbHelper.ExecuteNonQuery("AnularInvoiceDetail", parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al anular detalle de factura: {ex.Message}");
                throw;
            }
        }

        // 4. LEER DETALLES POR CABECERA

        public List<InvoiceDetail> ReadInvoiceDetails(int codeInvoiceHeader)
        {
            List<InvoiceDetail> details = new List<InvoiceDetail>();
            SqlDataReader reader = null;
            SqlConnection connection = null;

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@codeInvoiceHeader", codeInvoiceHeader)
                };

                connection = dbHelper.OpenConnection();
                reader = dbHelper.ExecuteReader(connection, "ReadInvoiceDetail", parameters);

                while (reader.Read())
                {
                    details.Add(MapInvoiceDetailFromReader(reader));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer detalles de factura: {ex.Message}");
                throw;
            }
            finally
            {
                if (reader != null && !reader.IsClosed) reader.Close();
            }

            return details;
        }

        // 5. MÉTODO DE MAPEO
        private InvoiceDetail MapInvoiceDetailFromReader(SqlDataReader reader)
        {
            object GetValueOrDefault(string columnName)
            {
                return reader[columnName] is DBNull ? null : reader[columnName];
            }

            InvoiceDetail detail = new InvoiceDetail
            {
                CodeDetailInvoice = Convert.ToInt32(reader["codeDetailInvoice"]),
                CodeInvoiceHeader = Convert.ToInt32(reader["codeInvoiceHeader"]),
                CodeProduct = GetValueOrDefault("codeProduct")?.ToString(),
                StatusDetail = GetValueOrDefault("statusDetail")?.ToString(),
                Quantity = Convert.ToDecimal(GetValueOrDefault("quantitySold") ?? 0.00m),
                Price = Convert.ToDecimal(GetValueOrDefault("unitPrice") ?? 0.00m),
                TaxRate = Convert.ToDecimal(GetValueOrDefault("taxRate") ?? 0.00m),
                TotalLine = Convert.ToDecimal(GetValueOrDefault("totalLine") ?? 0.00m)
            };

            return detail;
        }

        /// Recupera todos los detalles (productos) asociados a una factura.
        public List<InvoiceDetail> ReadDetailsByHeaderId(int invoiceHeaderId)
        {
            List<InvoiceDetail> details = new List<InvoiceDetail>();
            SqlDataReader reader = null;
            SqlConnection connection = null;

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@codeInvoiceHeader", invoiceHeaderId)
                };

                connection = dbHelper.OpenConnection();

                reader = dbHelper.ExecuteReader(connection, "ReadInvoiceDetailsByHeaderId", parameters);

                while (reader.Read())
                {
                    details.Add(MapInvoiceDetailFromReader(reader));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer detalles de factura: {ex.Message}");
                throw;
            }
            finally
            {
                if (reader != null && !reader.IsClosed) reader.Close();
            }

            return details;
        }

    }
}

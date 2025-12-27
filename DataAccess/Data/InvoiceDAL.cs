using Microsoft.Data.SqlClient;
using Model;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class InvoiceDAL
    {
        private readonly IDBHelper dbHelper;
        private readonly StockMovementDAL stockDAL;

        public InvoiceDAL(IDBHelper _dbHelper, StockMovementDAL _stockDAL)
        {
            this.dbHelper = _dbHelper;
            this.stockDAL = _stockDAL;
        }                                                                                     //private readonly DBHelper dbHelper = new DBHelper();
        
        //private readonly StockMovementDAL _stockDAL = new StockMovementDAL();



        // -------------------------------------------------------------------
        // 1. INSERTAR CABECERA DE FACTURA (CORREGIDO)

        public int InsertInvoiceHeader(InvoiceHeader header, SqlConnection connection, SqlTransaction transaction)
        {
            int newHeaderID = 0;
            string commandText = "InsertInvoiceHeader";

            using (SqlCommand command = new SqlCommand(commandText, connection, transaction))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@DateHeader", header.DateHeader);
                command.Parameters.AddWithValue("@CodeCustomer", (object)header.CodeCustomer ?? DBNull.Value);
                command.Parameters.AddWithValue("@DueDateHeader", (object)header.DueDateHeader ?? DBNull.Value);
                command.Parameters.AddWithValue("@PaymentMethodCode", header.PaymentMethodCode);
                command.Parameters.AddWithValue("@DiscountRate", header.DiscountRate); // DECIMAL(6, 4)
                command.Parameters.AddWithValue("@NCF", (object)header.NCF ?? DBNull.Value);
                command.Parameters.AddWithValue("@RNC", (object)header.RNC ?? DBNull.Value);
                command.Parameters.AddWithValue("@CashRegisterNumber", (object)header.CashRegisterNumber ?? DBNull.Value);
                command.Parameters.AddWithValue("@TotalTaxHeader", header.TotalTaxHeader);
                command.Parameters.AddWithValue("@SubtotalHeader", header.SubtotalHeader);
                command.Parameters.AddWithValue("@TotalAmountHeader", header.TotalAmountHeader);
                command.Parameters.AddWithValue("@StatusCode", header.StatusCode);
                SqlParameter outputParam = command.Parameters.Add("@NewInvoiceID", SqlDbType.Int);
                outputParam.Direction = ParameterDirection.Output;

                try
                {
                    command.ExecuteNonQuery();

                    if (outputParam.Value != DBNull.Value)
                    {
                        newHeaderID = Convert.ToInt32(outputParam.Value);
                    }
                }
                catch (Exception ex)
                {
                    // Lanza la excepción para que la BLL haga el Rollback
                    throw new Exception("DAL Error al insertar la cabecera.", ex);
                }
            }
            return newHeaderID;
        }



        // -------------------------------------------------------------------
        // 2. ANULAR CABECERA DE FACTURA

        public bool CancelInvoiceHeader(int codeInvoiceHeader)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@codeInvoiceHeader", codeInvoiceHeader)
                };

                dbHelper.ExecuteNonQuery("CancelInvoiceHeader", parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al anular cabecera de factura: {ex.Message}");
                throw;
            }
        }

        // -------------------------------------------------------------------
        // 3. LEER CABECERAS DE FACTURA

        public List<InvoiceHeader> ReadInvoiceHeaders(int? codeInvoiceHeader = null, string codeCustomer = null, string invoiceStatus = null)
        {
            List<InvoiceHeader> headers = new List<InvoiceHeader>();
            SqlDataReader reader = null;
            SqlConnection connection = null;

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@codeInvoiceHeader", codeInvoiceHeader.HasValue ? (object)codeInvoiceHeader.Value : DBNull.Value),
                    //new SqlParameter("@codeCustomer", string.IsNullOrEmpty(codeCustomer) ? (object)DBNull.Value : codeCustomer),
                    //new SqlParameter("@invoiceStatus", string.IsNullOrEmpty(invoiceStatus) ? (object)DBNull.Value : invoiceStatus)
                };

                connection = dbHelper.OpenConnection();
                reader = dbHelper.ExecuteReader(connection, "ReadInvoiceHeaderById", parameters);//ReadInvoiceHeader

                while (reader.Read())
                {
                    headers.Add(MapInvoiceHeaderFromReader(reader));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer cabeceras de factura: {ex.Message}");
                throw;
            }
            finally
            {
                if (reader != null && !reader.IsClosed) reader.Close();
            }

            return headers;
        }

        // -------------------------------------------------------------------
        // 3.1. LEER CABECERA POR ID ESPECÍFICO
        public InvoiceHeader ReadInvoiceHeaderById(int codeInvoiceHeader)
        {
            InvoiceHeader header = null;
            SqlDataReader reader = null;
            SqlConnection connection = null;

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@CodeInvoiceHeader", codeInvoiceHeader)
                };

                connection = dbHelper.OpenConnection();
                reader = dbHelper.ExecuteReader(connection, "ReadInvoiceHeaderById", parameters);

                if (reader.Read())
                {
                    header = MapInvoiceHeaderFromReader(reader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer cabecera de factura por ID: {ex.Message}");
                throw;
            }
            finally
            {
                if (reader != null && !reader.IsClosed) reader.Close();
            }

            return header;
        }


        // 4. MÉTODO DE MAPEO


        private InvoiceHeader MapInvoiceHeaderFromReader(SqlDataReader reader)
        {
            object GetValueOrDefault(string columnName)
            {
                try
                {
                    int ordinal = reader.GetOrdinal(columnName);
                    return reader.IsDBNull(ordinal) ? null : reader.GetValue(ordinal);
                }
                catch (IndexOutOfRangeException ex)
                {
                    System.Diagnostics.Debug.WriteLine($"ADVERTENCIA DAL: Columna '{columnName}' no encontrada. Error: {ex.Message}");
                    return null;
                }
            }

            InvoiceHeader header = new InvoiceHeader
            {
                CodeInvoiceHeader = Convert.ToInt32(reader["CodeInvoiceHeader"]),
                DateHeader = Convert.ToDateTime(reader["DateHeader"]),
                CodeCustomer = GetValueOrDefault("CodeCustomer")?.ToString(),
                DueDateHeader = GetValueOrDefault("DueDateHeader") as DateTime?,
                NCF = GetValueOrDefault("NCF")?.ToString(),
                RNC = GetValueOrDefault("RNC")?.ToString(),
                CashRegisterNumber = GetValueOrDefault("CashRegisterNumber")?.ToString(),
                PaymentMethodCode = Convert.ToInt32(GetValueOrDefault("PaymentMethodCode") ?? 0),
                DiscountRate = Convert.ToDecimal(GetValueOrDefault("DiscountRate") ?? 0.00m),
                TotalTaxHeader = Convert.ToDecimal(GetValueOrDefault("TotalTaxHeader") ?? 0.00m),
                SubtotalHeader = Convert.ToDecimal(GetValueOrDefault("SubtotalHeader") ?? 0.00m),
                TotalAmountHeader = Convert.ToDecimal(GetValueOrDefault("TotalAmountHeader") ?? 0.00m),

                StatusCode = GetValueOrDefault("StatusCode")?.ToString()
            };

            return header;
        }




        public List<InvoiceHeader> ReadInvoiceHeadersByDate(DateTime limitDate)
        {
            List<InvoiceHeader> headers = new List<InvoiceHeader>();
            SqlDataReader reader = null;
            SqlConnection connection = null;

            try
            {
                DateTime effectiveLimitDate = limitDate.Date.AddDays(1).AddSeconds(-1);

                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@limitDate", effectiveLimitDate)
                };

                connection = dbHelper.OpenConnection();
                reader = dbHelper.ExecuteReader(connection, "ReadInvoiceHeaderByDate", parameters);

                while (reader.Read())
                {
                    // Reutilizamos el método de mapeo existente
                    headers.Add(MapInvoiceHeaderFromReader(reader));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer cabeceras de factura por fecha: {ex.Message}");
                throw;
            }
            finally
            {
                if (reader != null && !reader.IsClosed) reader.Close();
            }

            return headers;
        }




    }
}


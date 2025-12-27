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
    public class PaymentDAL
    {
        //private readonly DBHelper dbHelper = new DBHelper();
        private readonly IDBHelper dbHelper;

        public PaymentDAL(IDBHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }


        // 1. INSERTAR PAGO ➕
        public int InsertPayment(Payment payment)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@codeInvoiceHeader", payment.CodeInvoiceHeader),
                    new SqlParameter("@paymentDate", payment.PaymentDate),
                    new SqlParameter("@amountPaid", payment.AmountPaid),
                    new SqlParameter("@codeMethod", payment.CodeMethod),
                    new SqlParameter("@referenceNumber", (object)payment.ReferenceNumber ?? DBNull.Value),
                    new SqlParameter("@paymentNote", (object)payment.PaymentNote ?? DBNull.Value),
                };

                object result = dbHelper.ExecuteScalar("InsertPayment", parameters);

                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }

                throw new InvalidOperationException("La inserción del pago falló.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error DAL al insertar pago: {ex.Message}");
                throw;
            }
        }
        // 2. ACTUALIZAR PAGO 🔄
        public bool UpdatePayment(Payment payment)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@codePayment", payment.CodePayment),
                    new SqlParameter("@paymentDate", payment.PaymentDate),
                    new SqlParameter("@amountPaid", payment.AmountPaid),
                    new SqlParameter("@codeMethod", payment.CodeMethod),
                    new SqlParameter("@referenceNumber", (object)payment.ReferenceNumber ?? DBNull.Value),
                    new SqlParameter("@paymentNote", (object)payment.PaymentNote ?? DBNull.Value)
                };

                int rowsAffected = dbHelper.ExecuteNonQuery("UpdatePayment", parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error DAL al actualizar pago: {ex.Message}");
                throw;
            }
        }

        // 3. ANULAR PAGO ❌
        public bool AnularPayment(int codePayment)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@codePayment", codePayment)
                };

                int rowsAffected = dbHelper.ExecuteNonQuery("AnularPayment", parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error DAL al anular pago: {ex.Message}");
                throw;
            }
        }

        // 4. LEER PAGOS POR ID DE FACTURA 📋
        public List<Payment> ReadPaymentsByInvoice(int codeInvoiceHeader)
        {
            List<Payment> payments = new List<Payment>();
            SqlDataReader reader = null;
            SqlConnection connection = null;

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@codeInvoiceHeader", codeInvoiceHeader)
                };

                connection = dbHelper.OpenConnection();
                reader = dbHelper.ExecuteReader(connection, "ReadPayment", parameters);

                while (reader.Read())
                {
                    payments.Add(MapPaymentFromReader(reader));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error DAL al leer pagos por factura {codeInvoiceHeader}: {ex.Message}");
                throw;
            }
            finally
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                dbHelper.CloseConnection();
            }

            return payments;
        }

        // 5. OBTENER PAGO POR ID (INDIVIDUAL) 🔍
        public Payment GetPaymentById(int codePayment)
        {
            Payment payment = null;
            SqlDataReader reader = null;
            SqlConnection connection = null;

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@codePayment", codePayment)
                };

                connection = dbHelper.OpenConnection();
                reader = dbHelper.ExecuteReader(connection, "GetPaymentById", parameters);

                if (reader.Read())
                {
                    payment = MapPaymentFromReader(reader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error DAL al obtener pago por ID: {ex.Message}");
                throw;
            }
            finally
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                dbHelper.CloseConnection();
            }

            return payment;
        }

        // 6. MÉTODO DE MAPEO 🗺️
        private Payment MapPaymentFromReader(SqlDataReader reader)
        {
            object GetValueOrDefault(string columnName)
            {
                int colIndex = reader.GetOrdinal(columnName);
                return reader.IsDBNull(colIndex) ? null : reader.GetValue(colIndex);
            }

            // Mapeamos las columnas de la BD a las propiedades de la entidad Payment
            Payment payment = new Payment
            {
                CodePayment = Convert.ToInt32(GetValueOrDefault("codePayment")),
                CodeInvoiceHeader = Convert.ToInt32(GetValueOrDefault("codeInvoiceHeader")),
                PaymentDate = Convert.ToDateTime(GetValueOrDefault("paymentDate")),
                AmountPaid = Convert.ToDecimal(GetValueOrDefault("amountPaid") ?? 0.00m),
                CodeMethod = Convert.ToInt32(GetValueOrDefault("codeMethod")),
                ReferenceNumber = GetValueOrDefault("referenceNumber")?.ToString(),
                PaymentNote = GetValueOrDefault("paymentNote")?.ToString(),
                PaymentStatus = GetValueOrDefault("paymentStatus")?.ToString() ?? "ACTIVO"
            };

            return payment;
        }


        public List<PaymentMethod> GetPaymentMethods()
        {
            List<PaymentMethod> methods = new List<PaymentMethod>();
            string query = "SELECT CodeMethod, NameMethod FROM PaymentMethod ORDER BY NameMethod";

            try
            {
                using (SqlConnection con = dbHelper.OpenConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PaymentMethod method = new PaymentMethod
                                {
                                    CodeMethod = reader.IsDBNull(reader.GetOrdinal("CodeMethod")) ? 0 : reader.GetInt32(reader.GetOrdinal("CodeMethod")),
                                    NameMethod = reader.IsDBNull(reader.GetOrdinal("NameMethod")) ? string.Empty : reader.GetString(reader.GetOrdinal("NameMethod"))
                                };
                                methods.Add(method);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error en la capa DAL al obtener los métodos de pago desde la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado al obtener los métodos de pago.", ex);
            }
            return methods;
        }

    }
}
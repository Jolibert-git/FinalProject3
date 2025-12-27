using Microsoft.Data.SqlClient;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class PaymentMethodDAL
    {
        private readonly IDBHelper dbHelper;

        public PaymentMethodDAL(IDBHelper _dbHelper)
        {
            this.dbHelper = _dbHelper;
        }
        //private readonly DBHelper dbHelper = new DBHelper();


        // 1. LEER MÉTODOS DE PAGO
        public List<PaymentMethod> ReadPaymentMethods(bool? isActive = true)
        {
            List<PaymentMethod> methods = new List<PaymentMethod>();
            SqlDataReader reader = null;
            SqlConnection connection = null;

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    // Enviamos el filtro de activo, si se especifica
                    new SqlParameter("@isActive", isActive.HasValue ? (object)isActive.Value : DBNull.Value)
                };

                connection = dbHelper.OpenConnection();
                reader = dbHelper.ExecuteReader(connection, "ReadPaymentMethod", parameters);

                while (reader.Read())
                {
                    methods.Add(MapPaymentMethodFromReader(reader));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer métodos de pago: {ex.Message}");
                throw;
            }
            finally
            {
                if (reader != null && !reader.IsClosed) reader.Close();
            }

            return methods;
        }

        // 2. MÉTODO DE MAPEO
        private PaymentMethod MapPaymentMethodFromReader(SqlDataReader reader)
        {
            object GetValueOrDefault(string columnName)
            {
                return reader[columnName] is DBNull ? null : reader[columnName];
            }

            PaymentMethod method = new PaymentMethod
            {
                CodeMethod = Convert.ToInt32(reader["codeMethod"]),
                NameMethod = GetValueOrDefault("methodName")?.ToString(),
                IsActive = Convert.ToBoolean(GetValueOrDefault("isActive") ?? false)
            };

            return method;
        }

    }
}

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
    public class CustomerDAL
    {
        //private readonly DBHelper dbHelper = new DBHelper();

        private readonly IDBHelper dbHelper;

        public CustomerDAL(IDBHelper _dbHelper)
        {
            this.dbHelper = _dbHelper;
        }


        public bool ValidateCustomer(string code)
        { 
            try
            {
                SqlConnection conn = dbHelper.OpenConnection();

                SqlParameter[] parameter = new SqlParameter[]
                {
                    new SqlParameter("CodeCustomer",code)
                };

                string storeProcedure = "ValidateCustomer";

                return dbHelper.ValidateExisten(conn,storeProcedure,parameter);


            }
            catch (Exception Ex)
            {
                Console.WriteLine($"Error in ValidateExisten>>CustomerDAL{Ex}");
                throw;
            }
            
        }

        // -------------------------------------------------------------------
        // 1. OBTENER CLIENTE POR CÓDIGO (MÉTODO CRÍTICO)

        public Customer GetCustomerByCode(string codeCustomer)
        {
            Customer customer = null;
            SqlDataReader reader = null;
            SqlConnection connection = null;

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                   new SqlParameter("@CodeCustomer", codeCustomer)
                };
                connection = dbHelper.OpenConnection();
                reader = dbHelper.ExecuteReader(connection, "ReadCustomer", parameters);

                if (reader.Read())
                {
                    customer = MapCustomerFromReader(reader);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                dbHelper.CloseConnection();
            }

            return customer;
        }

        // -------------------------------------------------------------------
        // 2. INSERTAR CLIENTE
        public bool InsertCustomer(Customer customer)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@codeCustomer", customer.CodeCustomer),
                    new SqlParameter("@fullNameCustomer", customer.FullNameCustomer),
                    new SqlParameter("@idCustomer", (object)customer.IdCustomer ?? DBNull.Value),
                    new SqlParameter("@phoneCustomer", (object)customer.PhoneCustomer ?? DBNull.Value),
                    new SqlParameter("@locationCustomer", (object)customer.LocationCustomer ?? DBNull.Value),
                    new SqlParameter("@cityCustomer", (object)customer.CityCustomer ?? DBNull.Value),
                    new SqlParameter("@emailCustomer", (object)customer.EmailCustomer ?? DBNull.Value),
                    new SqlParameter("@rncCustomer", (object)customer.RncCustomer ?? DBNull.Value),
                    new SqlParameter("@areaCustomer", (object)customer.AreaCustomer ?? DBNull.Value),
                    new SqlParameter("@autDescuentoCustomer", customer.AutDescuentoCustomer == 0 ? (object)DBNull.Value : customer.AutDescuentoCustomer),
                    new SqlParameter("@totalDescontadoCustomer", customer.TotalDescontadoCustomer == 0 ? (object)DBNull.Value : customer.TotalDescontadoCustomer),
                    new SqlParameter("@totalGastadoCustomer", customer.TotalGastadoCustomer == 0 ? (object)DBNull.Value : customer.TotalGastadoCustomer),
                    new SqlParameter("@totalGananciasCustomer", customer.TotalGananciasCustomer == 0 ? (object)DBNull.Value : customer.TotalGananciasCustomer)
                };

                int rowsAffected = dbHelper.ExecuteNonQuery("InsertCustomer", parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al insertar cliente: {ex.Message}");
                throw;
            }
        }

        // -------------------------------------------------------------------
        // 3. ACTUALIZAR CLIENTE

        public bool UpdateCustomer(Customer customer)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@codeCustomer", customer.CodeCustomer),
                    new SqlParameter("@fullNameCustomer", customer.FullNameCustomer),
                    new SqlParameter("@idCustomer", (object)customer.IdCustomer ?? DBNull.Value),
                    new SqlParameter("@phoneCustomer", (object)customer.PhoneCustomer ?? DBNull.Value),
                    new SqlParameter("@locationCustomer", (object)customer.LocationCustomer ?? DBNull.Value),
                    new SqlParameter("@cityCustomer", (object)customer.CityCustomer ?? DBNull.Value),
                    new SqlParameter("@emailCustomer", (object)customer.EmailCustomer ?? DBNull.Value),
                    new SqlParameter("@rncCustomer", (object)customer.RncCustomer ?? DBNull.Value),
                    new SqlParameter("@areaCustomer", (object)customer.AreaCustomer ?? DBNull.Value),
                    new SqlParameter("@autDescuentoCustomer", customer.AutDescuentoCustomer == 0 ? (object)DBNull.Value : customer.AutDescuentoCustomer),
                    new SqlParameter("@totalDescontadoCustomer", customer.TotalDescontadoCustomer == 0 ? (object)DBNull.Value : customer.TotalDescontadoCustomer),
                    new SqlParameter("@totalGastadoCustomer", customer.TotalGastadoCustomer == 0 ? (object)DBNull.Value : customer.TotalGastadoCustomer),
                    new SqlParameter("@totalGananciasCustomer", customer.TotalGananciasCustomer == 0 ? (object)DBNull.Value : customer.TotalGananciasCustomer)
                };

                int rowsAffected = dbHelper.ExecuteNonQuery("UpdateCustomer", parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar cliente: {ex.Message}");
                throw;
            }
        }

        // -------------------------------------------------------------------
        // 4. LEER CLIENTES
        public List<Customer> ReadCustomers(string codeCustomer = null, string fullNameCustomer = null)
        {
            List<Customer> customers = new List<Customer>();
            SqlDataReader reader = null;
            SqlConnection connection = null;

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@codeCustomer", string.IsNullOrEmpty(codeCustomer) ? (object)DBNull.Value : codeCustomer),
                    new SqlParameter("@fullNameCustomer", string.IsNullOrEmpty(fullNameCustomer) ? (object)DBNull.Value : fullNameCustomer)
                };

                connection = dbHelper.OpenConnection();
                reader = dbHelper.ExecuteReader(connection, "ReadCustomer", parameters);

                while (reader.Read())
                {
                    customers.Add(MapCustomerFromReader(reader));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer clientes: {ex.Message}");
                throw;
            }
            finally
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                dbHelper.CloseConnection();
            }

            return customers;
        }

        // -------------------------------------------------------------------
        // 5. MÉTODO DE MAPEO
        private Customer MapCustomerFromReader(SqlDataReader reader)
        {
            // Función auxiliar para manejar DBNull en campos opcionales
            object GetValueOrDefault(string columnName)
            {
                int colIndex = reader.GetOrdinal(columnName);
                return reader.IsDBNull(colIndex) ? null : reader.GetValue(colIndex);
            }

            Customer customer = new Customer
            {
                CodeCustomer = GetValueOrDefault("codeCustomer")?.ToString(),
                FullNameCustomer = GetValueOrDefault("fullNameCustomer")?.ToString(),
                IdCustomer = GetValueOrDefault("idCustomer")?.ToString(),
                PhoneCustomer = GetValueOrDefault("phoneCustomer")?.ToString(),
                LocationCustomer = GetValueOrDefault("locationCustomer")?.ToString(),
                CityCustomer = GetValueOrDefault("cityCustomer")?.ToString(),
                EmailCustomer = GetValueOrDefault("emailCustomer")?.ToString(),
                RncCustomer = GetValueOrDefault("rncCustomer")?.ToString(),
                AreaCustomer = GetValueOrDefault("areaCustomer")?.ToString(),
                AutDescuentoCustomer = Convert.ToDecimal(GetValueOrDefault("autDescuentoCustomer") ?? 0.00m),
                TotalDescontadoCustomer = Convert.ToDecimal(GetValueOrDefault("totalDescontadoCustomer") ?? 0.00m),
                TotalGastadoCustomer = Convert.ToDecimal(GetValueOrDefault("totalGastadoCustomer") ?? 0.00m),
                TotalGananciasCustomer = Convert.ToDecimal(GetValueOrDefault("totalGananciasCustomer") ?? 0.00m)
            };

            return customer;
        }
    }
}

using Microsoft.Data.SqlClient;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class StockMovementDAL
    {
        private readonly DBHelper dbHelper = new DBHelper();



        public int InsertStockMovement(StockMovement movement, SqlConnection connection, SqlTransaction transaction)
        {
            string storedProcedureName = "InsertStockMovement"; 
            int newMovementID = 0;

            using (SqlCommand command = new SqlCommand(storedProcedureName, connection, transaction))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CodeProduct", movement.CodeProduct);
                command.Parameters.AddWithValue("@MovementQuantity", movement.MovementQuantity);
                command.Parameters.AddWithValue("@MovementType", movement.MovementType);
                command.Parameters.AddWithValue("@NameMovement", movement.NameMovement);
                command.Parameters.AddWithValue("@Operation", movement.Operation);

                object result = command.ExecuteScalar(); 

                if (result != null && result != DBNull.Value)
                {
                    newMovementID = Convert.ToInt32(result);
                }
            }
            return newMovementID;
        }


  
        // 1. INSERTAR MOVIMIENTO DE STOCK (Transaccional)
        public int InsertStockMovement(StockMovement movement)
        {
            int newMovementID = 0;
            SqlConnection connection = null;
            SqlDataReader reader = null;

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                   
                    new SqlParameter("@codeProduct", movement.CodeProduct),
                    new SqlParameter("@movementQuantity", movement.MovementQuantity),
                    new SqlParameter("@movementType", movement.MovementType),
                    new SqlParameter("@movementReason", (object)movement.MovementReason ?? DBNull.Value),
                    new SqlParameter("@nameMovement", movement.NameMovement),
                    new SqlParameter("@operation", movement.Operation)
                };

               
                connection = dbHelper.OpenConnection();
                reader = dbHelper.ExecuteReader(connection, "InsertStockMovement", parameters);

                if (reader.Read())
                {
                   
                    if (reader["NewMovementID"] != DBNull.Value)
                    {
                        newMovementID = Convert.ToInt32(reader["NewMovementID"]);
                    }
                }

                return newMovementID;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error DAL al insertar movimiento de stock: {ex.Message}");
                throw;
            }
            finally
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                if (connection != null) dbHelper.CloseConnection();
            }
        }

        // 2. ACTUALIZAR MOVIMIENTO DE STOCK 🔄
        public bool UpdateStockMovement(StockMovement movement)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@codeMovement", movement.CodeMovement),
                    new SqlParameter("@nameMovement", movement.NameMovement),
                    new SqlParameter("@operation", movement.Operation),
                    new SqlParameter("@movementReason", (object)movement.MovementReason ?? DBNull.Value)
                };

                int rowsAffected = dbHelper.ExecuteNonQuery("UpdateStockMovement", parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error DAL al actualizar movimiento de stock: {ex.Message}");
                throw;
            }
        }

        // 3. LEER HISTORIAL DE MOVIMIENTOS CON FILTROS DE FECHA Y PRODUCTO 📋
        public List<StockMovement> ReadStockMovements(DateTime? startDate, DateTime? endDate, string codeProduct = null)
        {
            List<StockMovement> movements = new List<StockMovement>();
            SqlDataReader reader = null;
            SqlConnection connection = null;

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@startDate", startDate.HasValue ? (object)startDate.Value : DBNull.Value),
                    new SqlParameter("@endDate", endDate.HasValue ? (object)endDate.Value : DBNull.Value),
                    new SqlParameter("@codeProduct", string.IsNullOrEmpty(codeProduct) ? (object)DBNull.Value : codeProduct)
                };

                connection = dbHelper.OpenConnection();
                reader = dbHelper.ExecuteReader(connection, "ReadStockMovement", parameters);

                while (reader.Read())
                {
                    movements.Add(MapStockMovementFromReader(reader));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error DAL al leer movimientos de stock: {ex.Message}");
                throw;
            }
            finally
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                if (connection != null) dbHelper.CloseConnection();
            }

            return movements;
        }

        // 4. OBTENER MOVIMIENTO POR ID (INDIVIDUAL) 🔍

        public StockMovement GetMovementById(int codeMovement)
        {
            
            /*
            SqlDataReader reader = null;
            try
            {
                SqlParameter parameter = new SqlParameter("@codeMovement", codeMovement);
                reader = dbHelper.ExecuteReader("ReadStockMovementById", new SqlParameter[] { parameter });
                if (reader.Read())
                {
                    return MapStockMovementFromReader(reader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error DAL al obtener movimiento por ID: {ex.Message}");
                throw;
            }
            finally
            {
                if (reader != null && !reader.IsClosed) reader.Close();
            }
            */

            // Si no se implementa el SP de lectura por ID, devolvemos null por ahora.
            return null;
        }

        // 5. MÉTODO DE MAPEO 🗺️
        private StockMovement MapStockMovementFromReader(SqlDataReader reader)
        {
            object GetValueOrDefault(string columnName)
            {
                int colIndex = reader.GetOrdinal(columnName);
                return reader.IsDBNull(colIndex) ? null : reader.GetValue(colIndex);
            }

            StockMovement movement = new StockMovement
            {
                CodeMovement = Convert.ToInt32(GetValueOrDefault("codeMovement")),
                CodeProduct = GetValueOrDefault("codeProduct")?.ToString(),
                MovementDate = Convert.ToDateTime(GetValueOrDefault("MovementDate")),
                MovementQuantity = Convert.ToDecimal(GetValueOrDefault("MovementQuantity") ?? 0.00m),
                MovementType = GetValueOrDefault("MovementType")?.ToString(),
                MovementReason = GetValueOrDefault("MovementReason")?.ToString(),
                NameMovement = GetValueOrDefault("nameMovement")?.ToString(),
                Operation = GetValueOrDefault("operation")?.ToString()
            };

            return movement;
        }
    }
}

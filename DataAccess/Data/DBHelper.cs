using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class DBHelper
    {
        SqlConnection Connection = new SqlConnection("Data Source=DESKTOP-OA67FE6\\SQLEXPRESS;DATABASE=GcompleteQuery;Integrated Security=True;TrustServerCertificate=True;");
        private readonly string _connectionString = "Data Source=DESKTOP-OA67FE6\\SQLEXPRESS;DATABASE=GcompleteQuery;Integrated Security=True;TrustServerCertificate=True;";


        // -------------------------------------------------------------------
        // 1. ABRIR CONEXIÓN

        public SqlConnection OpenConnection()
        {
            try
            {
                SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al abrir conexion: {ex.Message}");
                throw;
            }
        }


        // -------------------------------------------------------------------
        // 2. CERRAR CONEXIÓN (CORREGIDO DE CloseConnectio a CloseConnection)
 
        public void CloseConnection()
        {
            try
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al Cerrar Conexion: {ex.Message}");
                throw;
            }
        }


        // -------------------------------------------------------------------
        // 3. MÉTODO PARA INSERTAR/ACTUALIZAR/ELIMINAR (ExecuteNonQuery)



        public int ExecuteNonQuery(string storedProcedureName, SqlParameter[] parameters = null)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = OpenConnection())
            {
                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    try
                    {
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error en ExecuteNonQuery para {storedProcedureName}: {ex.Message}");
                        throw;
                    }
                } 
            } 

            return rowsAffected;
        }








        // -------------------------------------------------------------------
        // 4. MÉTODO PARA LECTURA (ExecuteReader)

        public SqlDataReader ExecuteReader(SqlConnection connection, string storedProcedureName, SqlParameter[] parameters = null)
        {
            try
            {

                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    return command.ExecuteReader(CommandBehavior.CloseConnection);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ExecuteReader para {storedProcedureName}: {ex.Message}");
                CloseConnection();
                throw;
            }
        }

        // -------------------------------------------------------------------
        // 4. MÉTODO REQUERIDO: ExecuteScalar

        public object ExecuteScalar(string storedProcedureName, SqlParameter[] parameters)
        {
            object result = null;
            SqlConnection connection = null;

            try
            {
                connection = OpenConnection();

                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    result = command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error SQL al ejecutar ExecuteScalar ({storedProcedureName}): {ex.Message}");
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return result;
        }

        
    }
}

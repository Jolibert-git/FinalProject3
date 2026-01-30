using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class DBHelper: IDBHelper
    {
        public readonly string _StringConnection = "Data Source=DESKTOP-OA67FE6\\SQLEXPRESS;DATABASE=GcompleteQuery;Integrated Security=True;TrustServerCertificate=True;";
        SqlConnection Connection = new SqlConnection("Data Source=DESKTOP-OA67FE6\\SQLEXPRESS;DATABASE=GcompleteQuery;Integrated Security=True;TrustServerCertificate=True;");
        
        //
        //validate existen
        public bool ValidateExisten(SqlConnection conn, string procedure, SqlParameter[] parameter)
        {
            try
            {
                using (SqlCommand command = new SqlCommand(procedure, conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameter);

                    SqlParameter answersParam = new SqlParameter("@Answer", SqlDbType.Int);
                    answersParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(answersParam);

                    command.ExecuteNonQuery();

                    int answers = (int)answersParam.Value;
                    return answers == 1;
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine($"Error al ejecutae comando en la clase DBHelper{Ex.Message}");
                throw;
            }
        }




        // -------------------------------------------------------------------
        // 1. ABRIR CONEXIÓN

        public SqlConnection OpenConnection()
        {
            //SqlConnection connection = new SqlConnection(_StringConnection);
            try
            {
                if (Connection == null)
                {
                    Connection = new SqlConnection(_StringConnection);
                }

                // 2. Si la conexión está en un estado "zombi" o dañado (Broken o Connecting)
                // la cerramos a la fuerza para poder reabrirla.
                if (Connection.State == ConnectionState.Broken || Connection.State == ConnectionState.Connecting)
                {
                    Connection.Close();
                }

                // 3. Solo si está cerrada intentamos abrirla
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }




                /*

                if (Connection.State != ConnectionState.Open)
                {
                    Connection.Open();
                    return Connection;
                }
                */

                //SqlConnection connection = new SqlConnection(Connection);
                return Connection;
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



        public int ExecuteNonQuery(string storedProcedureName, SqlParameter[] parameters )
        {
            int rowsAffected = 0;
            SqlConnection connection = OpenConnection();
            //using (SqlConnection connection = OpenConnection())
            //{
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
            //}

            return rowsAffected;
        }








        // -------------------------------------------------------------------
        // 4. MÉTODO PARA LECTURA (ExecuteReader)

        public SqlDataReader ExecuteReader(SqlConnection connection, string storedProcedureName, SqlParameter[] parameters = null)//receive two/thre parameters (Open connetion,Procedure and pareameter) for executet data base 
        {
            try
            {

                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;//define type of cammand

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


        //---------------------------------------
        // 5. Validar transaccion
        public SqlTransaction GetTrasation()
        {
            try
            {

                var Trasaction = Connection.BeginTransaction();
                return Trasaction;

            }
            catch (Exception Ex)
            {
                Console.WriteLine($"Error en la capa DataAccess en la clase DBHelper: {Ex.Message}");
                return null;
            }

        }




    }
}

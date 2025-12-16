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
    public class DistributorDAL
    {
        private readonly DBHelper dbHelper = new DBHelper();

        // -------------------------------------------------------------------
        // 1. OBTENER DISTRIBUIDOR POR CÓDIGO (Para uso en BLL) 🔍

        public Distributor GetDistributorByCode(string code)
        {
            Distributor distributor = null;
            SqlDataReader reader = null;
            SqlConnection connection = null;

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@codeDistributor", code),
                    new SqlParameter("@nameDistributor", DBNull.Value)
                };

                connection = dbHelper.OpenConnection();
                reader = dbHelper.ExecuteReader(connection, "ReadDistributor", parameters);

                if (reader.Read())
                {
                    distributor = MapDistributorFromReader(reader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error DAL al obtener distribuidor por código: {ex.Message}");
                throw;
            }
            finally
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                dbHelper.CloseConnection();
            }

            return distributor;
        }


        // -------------------------------------------------------------------
        // 2. INSERTAR DISTRIBUIDOR ➕
        public bool InsertDistributor(Distributor distributor)
        {
            try
            {
                // Mapeo de propiedades camelCase de la entidad a parámetros del SP
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@codeDistributor", distributor.codeDistributor),
                    new SqlParameter("@nameDistributor", distributor.nameDistributor),
                    new SqlParameter("@rncDistributor", (object)distributor.rncDistributor ?? DBNull.Value),
                    new SqlParameter("@phoneDistributor", distributor.phoneDistributor),
                    new SqlParameter("@locationDistributor", (object)distributor.locationDistributor ?? DBNull.Value),
                    new SqlParameter("@emailDistributor", (object)distributor.emailDistributor ?? DBNull.Value),
                    new SqlParameter("@bancoDistributor", (object)distributor.bancoDistributor ?? DBNull.Value),
                    new SqlParameter("@cuentaDistributor", (object)distributor.cuentaDistributor ?? DBNull.Value),
                    new SqlParameter("@numCuentaDistrubutor", (object)distributor.numCuentaDistrubutor ?? DBNull.Value)
                };

                int rowsAffected = dbHelper.ExecuteNonQuery("InsertDistributor", parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error DAL al insertar distribuidor: {ex.Message}");
                throw;
            }
        }

        // -------------------------------------------------------------------
        // 3. ACTUALIZAR DISTRIBUIDOR 🔄
        public bool UpdateDistributor(Distributor distributor)
        {
            try
            {
                // Mapeo de propiedades camelCase de la entidad a parámetros del SP
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@codeDistributor", distributor.codeDistributor),
                    new SqlParameter("@nameDistributor", distributor.nameDistributor),
                    new SqlParameter("@rncDistributor", (object)distributor.rncDistributor ?? DBNull.Value),
                    new SqlParameter("@phoneDistributor", distributor.phoneDistributor),
                    new SqlParameter("@locationDistributor", (object)distributor.locationDistributor ?? DBNull.Value),
                    new SqlParameter("@emailDistributor", (object)distributor.emailDistributor ?? DBNull.Value),
                    new SqlParameter("@bancoDistributor", (object)distributor.bancoDistributor ?? DBNull.Value),
                    new SqlParameter("@cuentaDistributor", (object)distributor.cuentaDistributor ?? DBNull.Value),
                    new SqlParameter("@numCuentaDistrubutor", (object)distributor.numCuentaDistrubutor ?? DBNull.Value)
                };

                int rowsAffected = dbHelper.ExecuteNonQuery("UpdateDistributor", parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error DAL al actualizar distribuidor: {ex.Message}");
                throw;
            }
        }

        // -------------------------------------------------------------------
        // 4. LEER DISTRIBUIDORES (LISTA) 📋
        public List<Distributor> ReadDistributors(string codeDistributor = null, string nameDistributor = null)
        {
            List<Distributor> distributors = new List<Distributor>();
            SqlDataReader reader = null;
            SqlConnection connection = null;

            try
            {
                // Configuramos los parámetros, permitiendo DBNull si no se proporcionan filtros
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@codeDistributor", string.IsNullOrEmpty(codeDistributor) ? (object)DBNull.Value : codeDistributor),
                    new SqlParameter("@nameDistributor", string.IsNullOrEmpty(nameDistributor) ? (object)DBNull.Value : nameDistributor)
                };

                connection = dbHelper.OpenConnection();
                reader = dbHelper.ExecuteReader(connection, "ReadDistributor", parameters);

                while (reader.Read())
                {
                    distributors.Add(MapDistributorFromReader(reader));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error DAL al leer distribuidores: {ex.Message}");
                throw;
            }
            finally
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                dbHelper.CloseConnection();
            }

            return distributors;
        }

        // -------------------------------------------------------------------
        // 5. MÉTODO DE MAPEO 🗺️

        private Distributor MapDistributorFromReader(SqlDataReader reader)
        {
            // Función auxiliar para manejar DBNull en campos opcionales
            object GetValueOrDefault(string columnName)
            {
                int colIndex = reader.GetOrdinal(columnName);
                return reader.IsDBNull(colIndex) ? null : reader.GetValue(colIndex);
            }

            // Mapeamos las columnas de la BD a las propiedades camelCase de la entida
            Distributor distributor = new Distributor
            {
                codeDistributor = GetValueOrDefault("codeDistributor")?.ToString(),
                nameDistributor = GetValueOrDefault("nameDistributor")?.ToString(),
                rncDistributor = GetValueOrDefault("rncDistributor")?.ToString(),
                phoneDistributor = GetValueOrDefault("phoneDistributor")?.ToString(),
                locationDistributor = GetValueOrDefault("locationDistributor")?.ToString(),
                emailDistributor = GetValueOrDefault("emailDistributor")?.ToString(),
                bancoDistributor = GetValueOrDefault("bancoDistributor")?.ToString(),
                cuentaDistributor = GetValueOrDefault("cuentaDistributor")?.ToString(),
                numCuentaDistrubutor = GetValueOrDefault("numCuentaDistrubutor")?.ToString()
            };

            return distributor;
        }
    }
}

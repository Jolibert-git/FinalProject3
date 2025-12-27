using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public interface IDBHelper
    {
        bool ValidateExisten(SqlConnection conn, string procedure, SqlParameter[] parameter);
        SqlConnection OpenConnection();
        void CloseConnection();
        int ExecuteNonQuery(string storedProcedureName, SqlParameter[] parameters);
        SqlDataReader ExecuteReader(SqlConnection connection, string storedProcedureName, SqlParameter[] parameters = null);
        object ExecuteScalar(string storedProcedureName, SqlParameter[] parameters);
        SqlTransaction GetTrasation();

    }
}

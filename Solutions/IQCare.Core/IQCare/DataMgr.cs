using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace IQCare
{
    public class DataMgr
    {
        public static object GetConnection(string connectionString)
        {

            
            string constr = connectionString;
            constr += ";connect timeout=30";
            constr += ";packet size=4128;Min Pool Size=3;Max Pool Size=200;";
            SqlConnection connection = new SqlConnection(constr);
            connection.Open();
            OpenDecryptedSession(connection);
            return connection;

        }

        private static void OpenDecryptedSession(SqlConnection connection)
        {
            SqlCommand theCmd = new SqlCommand();
            theCmd.CommandText = "pr_OpenDecryptedSession";
            //theCmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = ApplicationAccess.DBSecurity;
            theCmd.CommandType = CommandType.StoredProcedure;
            theCmd.Connection = (SqlConnection)connection;


            theCmd.ExecuteNonQuery();
        }
    }
}

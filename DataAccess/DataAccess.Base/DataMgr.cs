using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Application.Common;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DataAccess.Base
{
    /// <summary>
    /// Data Connection Mode available
    /// </summary>
    public enum ConnectionMode
    {
        EMR = 1,
        REPORT = 2
    };
    public class DataMgr //:IDisposable
    {
        #region "Constructor"

       

        protected string emrdatabase;
        public static object _connec;
        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns></returns>
        protected string reportsdatabase;
        /// <summary>
        /// Initializes a new instance of the <see cref="DataMgr"/> class.
        /// </summary>
        public DataMgr()
        {
        }
        ~DataMgr()
        {

        }
        #endregion

        #region "Custom Properties"
        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns></returns>
        public static object GetConnection()
        {
            Console.Write("GetConnection()");
            Utility objUtil = new Utility();
            //  string constr = objUtil.Decrypt(((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["ConnectionString"]);
            string constr = objUtil.Decrypt(ConfigurationManager.AppSettings.Get("ConnectionString"));
            constr += ";connect timeout=" + CommandTimeOut().ToString();
            // constr += ";connect timeout=" + ((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["SessionTimeOut"].ToString();
            constr += ";packet size=4128;Min Pool Size=3;Max Pool Size=500;Pooling=true;MultipleActiveResultSets = True; ";
            //using (SqlConnection connection = new SqlConnection(constr))
            //{
            //    connection.Open();
            //    OpenDecryptedSession(connection);
            //    return connection;
            //}
            SqlConnection connection = new SqlConnection(constr);
            connection.Open();
            OpenDecryptedSession(connection);
            return connection;

        }

        public static object GetConnection(string connectionName)
        {
            Utility objUtil = new Utility();
            string constr = objUtil.Decrypt(ConfigurationManager.AppSettings.Get(connectionName));
            SqlConnection connection = new SqlConnection(constr);
            connection.Open();
            OpenDecryptedSession(connection);
            return connection;
        }
        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <param name="connectionMode">The connection mode.</param>
        /// <returns></returns>
        public static object GetConnection(ConnectionMode connectionMode)
        {
            Utility objUtil = new Utility();
            string key = "ConnectionString";
            if(connectionMode == ConnectionMode.REPORT){
                key = "IQToolsConnectionString";
            }
            //if (Mode == "Report")
            //{
            //    key = "IQToolsConnectionString";
            //}
            string constr = objUtil.Decrypt(ConfigurationManager.AppSettings.Get(key));
            //string constr = objUtil.Decrypt(((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["ConnectionString"]);
            constr += ";connect timeout=" + CommandTimeOut().ToString();
            // constr += ";connect timeout=" + ConfigurationManager.AppSettings.Get("SessionTimeOut");//((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["SessionTimeOut"].ToString();
            constr += ";packet size=4128;Min Pool Size=3;Max Pool Size=200;";
            SqlConnection connection = new SqlConnection(constr);
            connection.Open();
            if (connectionMode == ConnectionMode.EMR)
            {
                OpenDecryptedSession( connection);
            }
            return connection;
        }
        /// <summary>
        /// Gets the connection for ORM use.
        /// </summary>
        /// <returns></returns>
        public static DbConnection GetOrmConnectionString()
        {
            Console.WriteLine("GetOrmConnectionString()");
            Utility objUtil = new Utility();
            //  string constr = objUtil.Decrypt(((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["ConnectionString"]);
            string constr = objUtil.Decrypt(ConfigurationManager.AppSettings.Get("ConnectionString"));
            constr += ";connect timeout=" + CommandTimeOut().ToString();
            // constr += ";connect timeout=" + ((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["SessionTimeOut"].ToString();
            constr += ";MultipleActiveResultSets = True; Pooling = True;";
            SqlConnection connection = new SqlConnection(constr);
           connection.Open();
            OpenDecryptedSession(connection);
            return connection;
        }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <param name="connectionMode">The connection mode.</param>
        /// <returns></returns>
        static string GetConnectionString(ConnectionMode connectionMode)
        {
            Utility objUtil = new Utility();
            string key = "ConnectionString";
            if (connectionMode == ConnectionMode.REPORT)
            {
                key = "IQToolsConnectionString";
            }
            string constr = objUtil.Decrypt(ConfigurationManager.AppSettings.Get(key));
            return constr;
        }
        /// <summary>
        /// Tests the connection.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="enncrypted">if set to <c>true</c> [enncrypted].</param>
        /// <returns></returns>
        public static bool TestConnection(string connectionString,bool enncrypted=true)
        {
            bool success = false;
            Utility objUtil = new Utility();
             string constr  ="";
             if (enncrypted)
                 constr = objUtil.Decrypt(connectionString);
             else
             {
                 constr = connectionString;
             }
             try
             {
                 SqlConnection connection = new SqlConnection(constr);
                 connection.Open();
                 success = true;
             }
             catch { }
            
            return success;
        }
        /// <summary>
        /// Gets the connection_ master.
        /// </summary>
        /// <returns></returns>
        public static object GetConnection_Master()
        {
            Utility objUtil = new Utility();
           // string constr = objUtil.Decrypt(((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["ConnectionString"]);
            string constr = objUtil.Decrypt(ConfigurationManager.AppSettings.Get("appSettings"));
            constr = constr.Substring(0, constr.Length - 6);
            constr += "master";
            //constr += ";connect timeout=" + ((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["SessionTimeOut"].ToString();
            constr += ";connect timeout=" + CommandTimeOut().ToString();
            constr += ";packet size=4128;Min Pool Size=3;Max Pool Size=200;";
            //set nocount off;set arithabort on;set concat_null_yields_null on;set ansi_nulls on;";
            //constr += "set cursor_close_on_commit off;set ansi_null_dflt_on on;set implicit_transactions off;set ansi_padding on;set ansi_warnings on;";
            //constr += "set quoted_identifier on;";
            SqlConnection connection = new SqlConnection(constr);
            connection.Open();
            return connection;
            
        }

         static void OpenDecryptedSession( object connection)
        {
            SqlCommand theCmd = new SqlCommand();
            theCmd.CommandText = "pr_OpenDecryptedSession";
           // theCmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = ApplicationAccess.DBSecurity;
            theCmd.CommandType = CommandType.StoredProcedure;
            theCmd.Connection = (SqlConnection)connection;

            
            theCmd.ExecuteNonQuery();
        }
         static void CloseDecryptedSession(object connection)
        {

            try
            {
                SqlCommand theCmd = new SqlCommand("pr_CloseDecryptedSession", (SqlConnection)connection);
                theCmd.ExecuteNonQuery();
            }
            catch { }
        }
        /// <summary>
        /// Commands the time out.
        /// </summary>
        /// <returns></returns>
        public static int CommandTimeOut()
        {
            int timeOut = 30;
            int.TryParse(ConfigurationManager.AppSettings.Get("CommandTimeOut"), out timeOut);
          //  Convert.ToInt32(ConfigurationManager.AppSettings.Get("CommandTimeOut"));//Convert.ToInt32(((NameValueCollection)ConfigurationSettings.GetConfig("appSettings"))["CommandTimeOut"]);
            return timeOut;
        }

        /// <summary>
        /// Releases the connection.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public static void ReleaseConnection(object connection)
        {
            SqlConnection cnn = (SqlConnection)connection;
            if (cnn != null)
            {
                if (cnn.State != ConnectionState.Closed)
                {
                    CloseDecryptedSession(connection);
                    cnn.Close();
                }
                cnn.Dispose();
            }
        }

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        /// <param name="Connection">The connection.</param>
        /// <returns></returns>
        public static object BeginTransaction(object Connection)
        {
            return ((SqlConnection)Connection).BeginTransaction();
        }

        /// <summary>
        /// Commits the transaction.
        /// </summary>
        /// <param name="Transation">The transation.</param>
        public static void CommitTransaction(object Transation)
        {
            ((SqlTransaction)Transation).Commit();
        }

        /// <summary>
        /// Rolls the back transation.
        /// </summary>
        /// <param name="Transaction">The transaction.</param>
        public static void RollBackTransation(object Transaction)
        {
            ((SqlTransaction)Transaction).Rollback();
        }

        #endregion



        public void Dispose()
        {
            
        }
    }
}

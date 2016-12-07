using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DataAccess.Base;

namespace Common.Data.Util
{
    public class ConnectionManager
    {
        public static string GetConnection()
        {

            var connection = DataMgr.GetOrmConnectionString();
            return connection.EndsWith(";")
                ? $"{connection}MultipleActiveResultSets=True"
                : $"{connection};MultipleActiveResultSets=True";
            /*
        name="IQCareDatabase" 
        connectionString="Data Source=127.0.0.1\KOSKE14;Initial Catalog=IQCareMakadara;Persist Security Info=True;User ID=sa;Password=maun;MultipleActiveResultSets=True"
        providerName="System.Data.SqlClient" 
             */
        }
    }
}

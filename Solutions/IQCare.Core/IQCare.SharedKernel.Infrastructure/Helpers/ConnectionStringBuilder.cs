using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace IQCare.SharedKernel.Infrastructure.Helpers
{
    public class ConnectionStringBuilder
    {
        public static string BuildConnectionString(IConfiguration configuration, IConnectionString connectionString)
        {
            string _connectionString;
            var dbConnectionString = configuration.GetConnectionString("IQCareConnection");
            var iqcareuri = configuration.GetSection("IQCareUri").Value;
            bool useLocal = Convert.ToBoolean(configuration.GetSection("UseLocal").Value);
           
            if (useLocal)
            {
                _connectionString = dbConnectionString;
            }
            else
            {
                var db = connectionString.GetConnectionString(iqcareuri);

                _connectionString = db.Result.Replace("\"", "").Replace("Application Name=IQCare_EMR;", "")
                    .Replace("Server", "Data Source").Replace("Type System Version=SQL Data Source 2005;", "")
                    .Replace("Database", "Initial Catalog")
                    .Replace("Integrated Security=false;", "")
                    .Replace("packet size=4128;Min Pool Size=3;Max Pool Size=200;", "");

                _connectionString = _connectionString.Replace(@"\\", @"\");
                StringBuilder conn = new StringBuilder();
                conn.Append(_connectionString);
                conn.Append("MultipleActiveResultSets=True;");

                _connectionString = conn.ToString();
            }

            Log.Debug(_connectionString);
            return _connectionString;
        }
    }
}

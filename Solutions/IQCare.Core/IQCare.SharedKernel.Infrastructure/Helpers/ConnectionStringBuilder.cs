using System.Text;
using Microsoft.Extensions.Configuration;

namespace IQCare.SharedKernel.Infrastructure.Helpers
{
    public class ConnectionStringBuilder
    {
        public static string BuildConnectionString(IConfiguration configuration, IConnectionString connectionString)
        {
            var iqcareuri = configuration.GetSection("IQCareUri").Value;
            var db = connectionString.GetConnectionString(iqcareuri);

            string _connectionString = db.Result.Replace("\"", "").Replace("Application Name=IQCare_EMR;", "").Replace("Server", "Data Source").
                Replace("Type System Version=SQL Data Source 2005;", "").Replace("Database", "Initial Catalog")
                .Replace("Integrated Security=false;", "").Replace("packet size=4128;Min Pool Size=3;Max Pool Size=200;", "");

            _connectionString = _connectionString.Replace(@"\\", @"\");
            StringBuilder conn = new StringBuilder();
            conn.Append(_connectionString);
            conn.Append("MultipleActiveResultSets=True;");

            _connectionString = conn.ToString();

            return _connectionString;
        }
    }
}
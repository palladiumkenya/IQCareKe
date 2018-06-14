using System.Net.Http;
using System.Threading.Tasks;

namespace IQCare.Helpers
{
    public class ConnectionString : IConnectionString
    {
        static HttpClient client = new HttpClient();
        public async Task<string> GetConnectionString()
        {
            string connectionString = string.Empty;
            HttpResponseMessage response = await client.GetAsync("/");
            if (response.IsSuccessStatusCode)
            {
                connectionString = await response.Content.ReadAsStringAsync();
            }
            return connectionString;
        }
    }
}
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace IQCare.Helpers
{
    public class ConnectionString : IConnectionString
    {
        static HttpClient client = new HttpClient();
        public async Task<string> GetConnectionString(string iqcareUri)
        {
            string connectionString = string.Empty;
            using (client)
            {
                client.BaseAddress = new Uri(iqcareUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("TOKEN", "31LgBzXbDSPj28AwWars8Q==");

                HttpResponseMessage response = await client.GetAsync("/api/config/connection");
                if (response.IsSuccessStatusCode)
                {
                    connectionString = await response.Content.ReadAsStringAsync();
                }

                return connectionString;
            }
        }
    }
}
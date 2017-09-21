
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace IQCare.Events
{
    public class Publisher
    {
       public Publisher()
        {
           
        }

        public static async Task RaiseEventAsync(object sender, MessageEventArgs e)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string absoluteUrl = HttpContext.Current.Request.Url.AbsoluteUri;
                string absolutePath = HttpContext.Current.Request.Url.AbsolutePath;
                string uri = absoluteUrl.Replace(absolutePath, "/");

                // httpClient.BaseAddress = new Uri(uri);
                httpClient.BaseAddress = new Uri("http://localhost:1155/");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string content = JsonConvert.SerializeObject(e);

                var jsoncontent = new StringContent(content, Encoding.UTF8, "application/json");
                // HTTP POST
                HttpResponseMessage response = await httpClient.PostAsync("api/interop/dispatch/", jsoncontent);
                if (response.IsSuccessStatusCode)
                {
                    //todo
                    //update the outbox that message was sent successfully
                }
            }
        }
        public void RaiseEvent(object sender, MessageEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:18315/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string content = JsonConvert.SerializeObject(e);

            var jsoncontent = new StringContent(content, Encoding.ASCII, "application/json");
            var result = Task.Run(() => client.PostAsync("api/interop/dispatch/", jsoncontent).Result);


            //  return Convert.ToInt32(result.StatusCode);


        }
        public delegate void m_eventHandler(object sender, MessageEventArgs args);

        public event m_eventHandler DataExchangeEvent;

        protected void OnDataExchange(object sender, MessageEventArgs args)
        {
            DataExchangeEvent?.Invoke(this,args);
        }
        //public void NotifyListeners()
        //{
        //    m_event?.Invoke(this);
        //}

        //public void RegisterListener(IDataExchangeListener listener)
        //{
        //    m_event += new m_eventHandler(listener.OnNotification);
        //}

        //public void UnregisterListener(IDataExchangeListener listener)
        //{
        //    m_event -= new m_eventHandler(listener.OnNotification);
        //}
    }
}

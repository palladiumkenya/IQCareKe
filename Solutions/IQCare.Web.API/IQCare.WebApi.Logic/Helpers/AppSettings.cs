using System.Configuration;

namespace IQCare.WebApi.Logic.Helpers
{
    public class AppSettings
    {
        public static string IlServer()
        {
            return ConfigurationManager.AppSettings.Get("ilServer");
        }
    }
}

using System.ComponentModel;
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

    public enum Senders
    {
        ADT = 1,
        T4A,
        MLAB,
        [Description("MLAB SMS APP")]
        MLAB_SMS_APP,
        KENYAEMR,
        IL
    }
}

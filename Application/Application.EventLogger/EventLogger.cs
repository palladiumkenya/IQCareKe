using log4net;
using log4net.Config;
using log4net.Core;
using System;
using System.Web;
namespace Application.Logger
{
    public class EventLogger : IEventLogger
    {
        private static ILog _log;

        /// <summary>
        /// Logs the debug.
        /// </summary>
        /// <param name="message">The message.</param>
        public void LogDebug(string message)
        {
            if (!string.IsNullOrEmpty(message) && (message.Trim() != ""))
            {
                Log.Debug(message);
            }
        }

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="ex">The executable.</param>
        public void LogError(Exception ex)
        {
            if (ex != null)
            {
                string errStack = "";
                string errMsg = "";
                for (Exception exception = ex; exception != null; exception = exception.InnerException)
                {
                    if (errStack.Length > 0)
                    {
                        errStack = errStack + "\r\n";
                    }
                    errStack = errStack + (exception.StackTrace ?? "");
                }
                if (errStack.Length > 0)
                {
                    errStack = errStack + "\r\n";
                }
                errStack = errStack + (Environment.StackTrace ?? "");
                errMsg = ex.Message ?? "";
                string errPageURL = "<no page>";
                try
                {
                    errPageURL = HttpContext.Current.Request.Url.ToString();
                }
                catch (Exception)
                {
                }
                this.LogError(errPageURL, errMsg, errStack);
            }
        }

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="errPageURL">The error page URL.</param>
        /// <param name="errMsg">The error MSG.</param>
        /// <param name="errStack">The error stack.</param>
        public void LogError(string errPageURL, string errMsg, string errStack)
        {
            if (errStack == null)
            {
                errStack = "";
            }
            if (errMsg == null)
            {
                errMsg = "";
            }
            if (errPageURL == null)
            {
                errPageURL = "<no page>";
            }
            string str = "<no user>";
            string str2 = "no user";
            try
            {
                if ((HttpContext.Current != null) && (HttpContext.Current.Session != null))
                {
                    object obj2 = HttpContext.Current.Session["AppUserName"];
                    if (obj2 != null)
                    {
                        str = obj2.ToString();                     
                    }
                    object obj3 = HttpContext.Current.Session["AppLocation"];
                    if (obj3 != null)
                    {
                    str2 = obj3.ToString();
                    }
                       
                  
                }
            }
            catch
            {
            }
            string str3 = "";
            try
            {
                foreach (string str4 in HttpContext.Current.Request.Form)
                {
                    if (str4 != null)
                    {
                        string str5 = HttpContext.Current.Request.Form[str4];
                        if (str4.ToLower().Contains("password") && !string.IsNullOrEmpty(str5))
                        {
                            str5 = "<hidden>";
                        }
                        str3 = str3 + string.Format("{0}: {1}\r\n", str4, str5);
                    }
                }
            }
            catch
            {
            }
            LoggingEvent logEvent = new LoggingEvent(typeof(EventLogger), Log.Logger.Repository, Log.Logger.Name, Level.Error, errMsg, null);
            logEvent.Properties["pageurl"] = errPageURL;
            logEvent.Properties["message"] = errMsg;
            logEvent.Properties["exceptionstacktrace"] = errStack;
            logEvent.Properties["user"] = str;
            logEvent.Properties["userid"] = str2;
           // logEvent.Properties["formdata"] = str3;
            Log.Logger.Log(logEvent);
        }

        /// <summary>
        /// Logs the information.
        /// </summary>
        /// <param name="message">The message.</param>
        public void LogInfo(string message)
        {
            if (!string.IsNullOrEmpty(message) && (message.Trim() != ""))
            {
                Log.Info(message);
            }
        }

        // Properties
        /// <summary>
        /// Gets the log.
        /// </summary>
        /// <value>
        /// The log.
        /// </value>
        private static ILog Log
        {
            get
            {
                if (_log == null)
                {
                    XmlConfigurator.Configure();
                    _log = LogManager.GetLogger(typeof(EventLogger));
                }
                return _log;
            }
        }

    }
}

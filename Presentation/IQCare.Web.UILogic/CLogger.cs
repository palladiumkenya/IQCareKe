using System;
using System.Linq;
using log4net;
using log4net.Config;
using log4net.Appender;
using System.IO;
using System.Web;
using log4net.Core;
using System.Reflection;


namespace Application.Common
{
    public static class CLogger
    {
        #region Members
        //private static readonly ILog logger = LogManager.GetLogger(typeof(CLogger));
        //private readonly static Type ThisDeclaringType = typeof(CLogger);
        private static readonly ILogger logger;
        #endregion


        #region Constructors
        static CLogger()
        {
            logger = LoggerManager.GetLogger(Assembly.GetCallingAssembly(), "CLogger");
            XmlConfigurator.Configure();
        }
        //CLogger(object className)
        //{
        //    logger = LoggerManager.GetLogger(Assembly.GetCallingAssembly(), "CLogger");
        //    XmlConfigurator.Configure();
        //}
        #endregion


        #region Methods
        public static void WriteLog(ELogLevel logLevel, String log)
        {
            WriteLog(logLevel, log, null);
        }

        public static void WriteLog(String raisedFrom, String commandText, String parameters, String exception)
        {
            WriteLog(ELogLevel.ERROR, raisedFrom, null);
            if (!string.IsNullOrEmpty(commandText))
            {
                WriteLog(ELogLevel.ERROR, "CommandText: " + commandText, null);
            }
            string strParamData = "Parameters: " + Environment.NewLine + parameters.ToString();
            if (!string.IsNullOrEmpty(strParamData))
            {
                WriteLog(ELogLevel.ERROR, strParamData, null);
            }
            WriteLog(ELogLevel.ERROR, "Exception Message: " + exception.ToString(), null);
            WriteLog(ELogLevel.ERROR, "---------------------------------------------------------------------------------------");
        }

        public static void WriteLog(ELogLevel logLevel, String log, Exception exception)
        {
            if (logLevel.Equals(ELogLevel.DEBUG))
            {
                logger.Log(typeof(CLogger), log4net.Core.Level.Debug, log, null);
            }
            else if (logLevel.Equals(ELogLevel.ERROR))
            {
                //logger.Error(log, exception);
                logger.Log(typeof(CLogger), log4net.Core.Level.Error, log, null);
            }
            else if (logLevel.Equals(ELogLevel.FATAL))
            {
                //logger.Fatal(log, exception);
                logger.Log(typeof(CLogger), log4net.Core.Level.Fatal, log, null);
            }
            else if (logLevel.Equals(ELogLevel.INFO))
            {
                //logger.Info(log);
                logger.Log(typeof(CLogger), log4net.Core.Level.Info, log, null);
            }
            else if (logLevel.Equals(ELogLevel.WARN))
            {
                //logger.Warn(log);
                logger.Log(typeof(CLogger), log4net.Core.Level.Warn, log, null);
            }
        }
        #endregion


        #region Clean Up Methods
        /// <summary>
        /// Cleans up. Auto configures the cleanup based on the log4net configuration
        /// </summary>
        public static void CleanUp()
        {
            if (!object.Equals(System.Configuration.ConfigurationManager.AppSettings["DaysToKeepLogFile"], null))
            {
                int daysToKeep = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DaysToKeepLogFile"].ToString());
                if (daysToKeep > 0)
                {
                    DateTime date = DateTime.Now.AddDays(-daysToKeep);
                    CleanUp(date);
                }

            }
        }

        /// <summary>
        /// Cleans up. Auto configures the cleanup based on the log4net configuration
        /// </summary>
        /// <param name="date">Anything prior will not be kept.</param>
        private static void CleanUp(DateTime date)
        {
            string directory = string.Empty;
            string filePrefix = string.Empty;

            var repo = LogManager.GetAllRepositories().FirstOrDefault(); ;
            if (repo == null)
            {
                //throw new NotSupportedException("Log4Net has not been configured yet.");
                return;
            }
            else
            {
                var app = repo.GetAppenders().Where(x => x.GetType() == typeof(RollingFileAppender)).FirstOrDefault();
                if (app != null)
                {
                    var appender = app as RollingFileAppender;

                    directory = Path.GetDirectoryName(appender.File);
                    filePrefix = Path.GetFileName(appender.File);

                    CleanUp(directory, filePrefix, date);
                }
            }
        }

        /// <summary>
        /// Cleans up.
        /// </summary>
        /// <param name="logDirectory">The log directory.</param>
        /// <param name="logPrefix">The log prefix. Example: logfile dont include the file extension.</param>
        /// <param name="date">Anything prior will not be kept.</param>
        private static void CleanUp(string logDirectory, string logPrefix, DateTime date)
        {
            if (string.IsNullOrEmpty(logDirectory))
                return;
            //throw new ArgumentException("logDirectory is missing");

            if (string.IsNullOrEmpty(logDirectory))
                return;
            //throw new ArgumentException("logPrefix is missing");

            var dirInfo = new DirectoryInfo(logDirectory);
            if (!dirInfo.Exists)
                return;

            //var fileInfos = dirInfo.GetFiles("{0}*.*".Sub(logPrefix));
            var fileInfos = dirInfo.GetFiles();
            if (fileInfos.Length == 0)
                return;

            foreach (var info in fileInfos)
            {
                if (string.Compare(info.Name, logPrefix, true) != 0)
                {
                    if (info.CreationTime < date)
                    {
                        info.Delete();
                    }
                }
            }

        }
        #endregion

        #region "BillingModule"
        private static ILog _log;

        /// Logs the debug.        
        public static void LogDebug(string message)
        {
            if (!string.IsNullOrEmpty(message) && (message.Trim() != ""))
            {
                Log.Debug(message);
            }
        }

        /// Logs the error.        
        public static void LogError(Exception ex)
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
                LogError(errPageURL, errMsg, errStack);
            }
        }

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="errPageURL">The error page URL.</param>
        /// <param name="errMsg">The error MSG.</param>
        /// <param name="errStack">The error stack.</param>
        public static void LogError(string errPageURL, string errMsg, string errStack)
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
            LoggingEvent logEvent = new LoggingEvent(typeof(CLogger), Log.Logger.Repository, Log.Logger.Name, Level.Error, errMsg, null);
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
        public static void LogInfo(string message)
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
                    _log = LogManager.GetLogger(typeof(CLogger));
                }
                return _log;
            }
        }

        #endregion
    }

    public enum ELogLevel
    {
        DEBUG = 1,
        ERROR,
        FATAL,
        INFO,
        WARN
    }
}



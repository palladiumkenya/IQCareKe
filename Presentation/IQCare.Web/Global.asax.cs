using IQCare.Web.UILogic;
using System;
using System.IO;
using System.IO.Compression;
using System.Web;
using System.Web.Http;
using System.Web.SessionState;
using System.Web.UI;

namespace IQCare.Web
{
    public class Global : HttpApplication
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            log.Debug("iq started");
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, EventArgs e)
        {
            try
            {
                Exception lastError = base.Server.GetLastError();
                SystemSetting.LogError(lastError);
                //if (lastError != null)
                //{
                //    Exception baseException = lastError.GetBaseException();
                //    if (baseException != null)
                //    {
                //        lastError = baseException;
                //    }
                //}
                //EventLogger logger = new EventLogger();
                //logger.LogError(lastError);

                //if (ConfigurationManager.AppSettings.Get("DEBUG").ToUpper() == "TRUE")
                //{
                //    HttpContext.Current.Session["IQCARE_ERROR"] = lastError.Message + lastError.StackTrace;
                //}
                //else
                //{
                //    HttpContext.Current.Session.Remove("IQCARE_ERROR");
                //}
            }
            catch { }
            base.Server.ClearError();
            base.Response.Clear();
            //handle endless loop ERR_TOO_MANY_REDIRECTS
            if (!HttpContext.Current.Request.Path.EndsWith("Error.aspx", StringComparison.InvariantCultureIgnoreCase))
            {
                base.Response.Redirect("~/Error.aspx",false);
                Context.ApplicationInstance.CompleteRequest();
            }


        }
        //protected void Application_PreRequestHandlerExecute(Object sender, EventArgs e)
        //{
        // here it checks if session is reuired, as
        // .aspx requires session, and session should be available there
        // .jpg, or .css doesn't require session so session will be null
        // as .jpg, or .css are also http request in any case
        // even if you implemented URL Rewritter, or custom IHttp Module

        //}
        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                if (CurrentSession.Current == null || !Request.IsAuthenticated)
                {
                    //string f = Request.RequestContext.ToString();
                }
            }
        }
        protected void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {

            HttpApplication app = sender as HttpApplication;
            string acceptEncoding = app.Request.Headers["Accept-Encoding"];
            Stream prevUncompressedStream = app.Response.Filter;

            if (Context.Handler is IRequiresSessionState || Context.Handler is IReadOnlySessionState)
            {
                if (app.Context.CurrentHandler is Page)
                {
                    if (CurrentSession.Current == null || Session["AppUserId"] == null)
                    {

                        if (!Context.Request.Url.AbsoluteUri.ToLower().Contains("frmlogin.aspx"))
                        {
                            // redirect to your login page
                            Context.Response.Redirect("~/frmLogin.aspx",false);
                            Context.ApplicationInstance.CompleteRequest();
                        }
                    }
                }
            }

            if (!(app.Context.CurrentHandler is Page) ||
                app.Request["HTTP_X_MICROSOFTAJAX"] != null)
                return;

            if (acceptEncoding == null || acceptEncoding.Length == 0)
                return;

            acceptEncoding = acceptEncoding.ToLower();

            if (acceptEncoding.Contains("deflate") || acceptEncoding == "*")
            {
                // defalte
                app.Response.Filter = new DeflateStream(prevUncompressedStream, CompressionMode.Compress);
                app.Response.AppendHeader("Content-Encoding", "deflate");
            }
            else if (acceptEncoding.Contains("gzip"))
            {
                // gzip
                app.Response.Filter = new GZipStream(prevUncompressedStream,  CompressionMode.Compress);
                app.Response.AppendHeader("Content-Encoding", "gzip");
            }
        }
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        
        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.IO;
using System.IO.Compression;


    namespace Application.Common
    {
    public static class GlobalVars
    {
        public static LoggedInUser LoggedInUser
        {
            get
            {
                 object o = HttpContext.Current.Session["_LoggedInUser"];
                if (o != null)
                {
                    return (LoggedInUser)o;
                }
                return null;
            }

        }
    }
    }

using System.Data;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.UI;
namespace IQCare.Web
{

    public static class ControlExtensions
    {
        public static IEnumerable<T> GetAllControlsOfType<T>(this Control parent) where T : Control
        {
            var result = new List<T>();
            foreach (Control control in parent.Controls)
            {
                if (control is T)
                {
                    result.Add((T)control);
                }
                if (control.HasControls())
                {
                    result.AddRange(control.GetAllControlsOfType<T>());
                }
            }
            return result;
        }
    }
}
namespace IQCare.Web
{

    /// <summary>
    /// Summary description for IQWebUtils
    /// </summary>
    public class IQWebUtils
    {
        #region "Constructor"
        public IQWebUtils()
        {
        }
        #endregion

        public void ExporttoExcel(DataTable theDT, HttpResponse theRes)
        {
            DataGrid theDG = new DataGrid();
            theDG.DataSource = theDT;
            theDG.DataBind();
            theRes.Clear();
            theRes.Buffer = true;
            theRes.AddHeader("Content-Disposition", "attachment; filename=" + theDT.TableName + ".xls");
            theRes.ContentType = "application/vnd.ms-excel";
            theRes.Charset = "";
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            theDG.RenderControl(oHtmlTextWriter);
            theRes.Write(oStringWriter.ToString());
            theRes.End();
        }
        public void ExportDocument(byte[] fileData, string contentType, string fileName, HttpResponse response)
        {
            response.Clear();
            response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
            response.ContentType = contentType;
            BinaryWriter bw = new BinaryWriter(response.OutputStream);
            bw.Write(fileData);
            bw.Close();
            response.End();
        }
        public void ShowExcelFile(string theFile, HttpResponse theRes)
        {
            theRes.Clear();
            theRes.Buffer = true;
            theRes.ContentType = "application/vnd.ms-excel";
            theRes.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", theFile));
            theRes.WriteFile(theFile);
            theRes.End();

        }
        public void ShowFile(string theFile, HttpResponse theRes)
        {
            theRes.Clear();
            theRes.Buffer = true;
            theRes.ContentType = "application/vnd.ms-htmlhelp";
            theRes.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", theFile));
            theRes.WriteFile(theFile);
            theRes.End();

        }

        //public void ResetSession()
        //{
        //    Session["PatientVisitId"] = 0;
        //    Session["ServiceLocationId"] = 0;
        //}
        //public object CreateSessionObject(string theSession)
        //{
        //    //if(System.Web.HttpContext.Current.Session.Contents[theSession.ToString() + "GBLUserInstance"])
        //    //{
        //    //    LoggedInUser theUser = new LoggedInUser();
        //    //    System.Web.HttpContext.Current.Session.Add(theSession.ToString() + "GBLUserInstance", theUser);
        //    //}
        //    //return System.Web.HttpContext.Current.Session[theSession.ToString() + "GBLUserInstance"];
        //}
    }
}
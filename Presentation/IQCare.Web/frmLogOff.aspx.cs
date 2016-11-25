using System;
using IQCare.Web.UILogic;
namespace IQCare.Web
{
    public partial class LogOffWeb : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CurrentSession.Logout();
            Session.Remove(Session.SessionID);
            #region "Session Clear"
            Session.RemoveAll();
            Application.Remove("AppCurrentDate");
            Application.Remove("AppEmployee");
            Session["SelectedData"] = null;
            Session.Remove("SelectedData");


            #endregion
            Response.Cookies.Clear();
            Response.Redirect("frmLogin.aspx");
        }
    }
}
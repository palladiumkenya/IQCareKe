using System;
using System.Web;

public partial class Error : System.Web.UI.Page
{
    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (base.Request.UrlReferrer != null)
            {
                string referrer = base.Request.UrlReferrer.ToString();
                this.lnkReferer.NavigateUrl = referrer;
                this.lnkReferer.Visible = true;
            }
            else
            {
                this.lnkReferer.Visible = false;
            }
            this.pnlErrorDetails.Visible = false;
            if (HttpContext.Current.Session["IQCARE_ERROR"] != null)
            {
                string errorText = base.Server.HtmlEncode(HttpContext.Current.Session["IQCARE_ERROR"].ToString()).Replace("\n", "<br />");
                this.txtErrorDetails.Text = errorText;
                this.pnlErrorDetails.Visible = true;
                HttpContext.Current.Session["IQCARE_ERROR"] = null;
            }

        }
    }
}
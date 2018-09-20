using System;

public partial class _Default : System.Web.UI.Page
{

    //IReportIQTools IQEw;
    protected void Page_Load(object sender, EventArgs e)
    {
     Response.Redirect("~/frmLogin.aspx",false);
        Context.ApplicationInstance.CompleteRequest();
    }
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
       
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

       
    }
}
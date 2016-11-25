using System;
using Application.Common;
using Application.Presentation;


public partial class frmRefreshReportFiles : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           // IIQCareSystem ReportingTables = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem,BusinessProcess.Security");
            //ReportingTables.RefreshReportingTables(1);
            Response.Redirect("frmFacilityHome.aspx");
        }
        catch (Exception err)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            theBuilder.DataElements["MessageText"] = err.Message.ToString();
            IQCareMsgBox.Show("#C1", theBuilder, this);
        }
    }
}

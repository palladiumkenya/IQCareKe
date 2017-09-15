using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Interface.Clinical;
using Application.Common;
using Application.Presentation;
namespace IQCare.Web.Clinical
{
    public partial class CalculateAgeCTC : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["regDate"] = Request.QueryString["regDate"].ToString().Substring(1, 11);
        }
        protected void btnCalculate_ServerClick(object sender, EventArgs e)
        {
            if (txtAge.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Age";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtAge.Focus();
            }
            else
            {
                DateTime theregdt = Convert.ToDateTime(ViewState["regDate"].ToString());
                DateTime theSysdt = Convert.ToDateTime(Application["AppCurrentDate"].ToString());
                double Regyear = theregdt.Year - Math.Round(Convert.ToDouble(txtAge.Text));
                txtDOB.Text = ViewState["regDate"].ToString().Substring(0, 7) + Regyear.ToString();
                txtRegAge.Value = Convert.ToString(theSysdt.Year - Convert.ToInt32(txtDOB.Text.Substring(7, 4)));
            }
        }

    }
}
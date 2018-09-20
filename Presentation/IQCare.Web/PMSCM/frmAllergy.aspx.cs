using Application.Presentation;
using Interface.SCM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IQCare.Web.PMSCM
{
    public partial class frmAllergy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IDrug theAllergyData = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
            DataSet theDS = theAllergyData.GetAllergyData(Convert.ToInt32(Session["PatientID"]));

            GridView1.DataSource = theDS.Tables[0];
            GridView1.DataBind();
        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            TableHeaderCell cell = new TableHeaderCell();
            cell.Text = "";
            cell.ColumnSpan = 3;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.ColumnSpan = 4;
            cell.Text = "Patient Allergy";
            cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#3AC0F2");
            cell.HorizontalAlign = HorizontalAlign.Center;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.ColumnSpan = 7;
            cell.Text = "";
            row.Controls.Add(cell);

            //row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
            GridView1.HeaderRow.Parent.Controls.AddAt(0, row);
        }
    }
}
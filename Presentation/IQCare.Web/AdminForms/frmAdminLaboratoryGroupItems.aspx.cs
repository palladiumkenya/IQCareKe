using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Interface.Administration;
using Application.Presentation;
using System.Data;

namespace IQCare.Web.Admin
{
    public partial class LaboratoryGroupItems : System.Web.UI.Page
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            BindGrid();
            lblH2.Text = "Edit " + Request.QueryString["Name"];
        }
        /// <summary>
        /// Binds the grid.
        /// </summary>
        private void BindGrid()
        {
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Customize Lists >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Laboratory Group Items";
            ILabMst LabManager = (ILabMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BLabMst, BusinessProcess.Administration");
            DataTable theMainDt = LabManager.GetLabGroupTests(int.Parse(Request.QueryString["LabId"]));
            DataView theDV = new DataView(theMainDt);
            theDV.RowFilter = "DataType <>'Group' OR DataType IS NULL ";
            DataTable theDt = theDV.ToTable();
            grdLabs.DataSource = theDt;
            grdLabs.DataBind();
          
        }

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (DataTable theDT = new DataTable())
            {
                theDT.Columns.AddRange(new DataColumn[2] { new DataColumn("LabgroupID", typeof(int)), new DataColumn("LabTestID", typeof(int)) });
                foreach (GridViewRow row in grdLabs.Rows)
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[1].FindControl("chkLabTest") as CheckBox);
                        if (chkRow.Checked)
                        {
                            string LabID = (row.Cells[0].FindControl("lblLabTestID") as Label).Text;
                            string LabGroupID = Request.QueryString["LabId"];
                            theDT.Rows.Add(LabGroupID, LabID);
                        }
                    }
                ILabMst LabManager = (ILabMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BLabMst, BusinessProcess.Administration");
              LabManager.SaveLabGroupItems(Convert.ToInt32(Session["AppUserId"]),theDT,int.Parse(Request.QueryString["LabId"]));

              string theUrl = "frmAdmin_LaboratoryGroups.aspx?Fid=" + Request.QueryString["Fid"];

              string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
              script += "window.alert('Saved successfully.');\n";
              script += "window.location.href='" + theUrl + "';\n";
              script += "</script>\n";
              ClientScript.RegisterStartupScript(this.GetType(), "success", script);
            }
            

        }

        /// <summary>
        /// Handles the Click event of the btnExit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmAdmin_LaboratoryGroups.aspx?Fid=" + Request.QueryString["Fid"], false);
        }

    }
}
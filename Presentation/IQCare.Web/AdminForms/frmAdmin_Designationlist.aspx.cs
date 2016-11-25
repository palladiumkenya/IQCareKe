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
using Interface.Administration;
using Application.Presentation;
using Application.Common;
namespace IQCare.Web.Admin
{
    public partial class Designationlist : System.Web.UI.Page
    {   
        private void BindGrid()
        {

            BoundField theCol0 = new BoundField();
            theCol0.HeaderText = "Designation_pk";
            theCol0.DataField = "Designation_Id";
            theCol0.ItemStyle.CssClass = "textstyle";
            theCol0.ReadOnly = true;

            BoundField theCol1 = new BoundField();
            theCol1.HeaderText = "DesignationName";
            theCol1.ItemStyle.CssClass = "textstyle";
            theCol1.DataField = "Designation_Name";
            theCol1.SortExpression = "Designation_Name";
            theCol1.ItemStyle.Font.Underline = true;
            theCol1.ReadOnly = true;

            BoundField theCol2 = new BoundField();
            theCol2.HeaderText = "Status";
            theCol2.DataField = "Status";
            theCol2.SortExpression = "Status";
            theCol2.ItemStyle.CssClass = "textstyle";
            theCol2.ReadOnly = true;

            BoundField theCol3 = new BoundField();
            theCol3.HeaderText = "Priority";
            theCol3.DataField = "Sequence";
            theCol3.ItemStyle.CssClass = "textstyle";
            theCol3.SortExpression = "Sequence";
            theCol3.ReadOnly = true;

            ButtonField theBtn = new ButtonField();
            theBtn.ButtonType = ButtonType.Link;
            theBtn.CommandName = "Select";
            theBtn.HeaderStyle.CssClass = "textstylehidden";
            theBtn.ItemStyle.CssClass = "textstylehidden";

            grdDesignation.Columns.Add(theCol0);
            grdDesignation.Columns.Add(theCol3);
            grdDesignation.Columns.Add(theCol1);
            grdDesignation.Columns.Add(theCol2);


            grdDesignation.Columns.Add(theBtn);

            grdDesignation.DataBind();
            grdDesignation.Columns[0].Visible = false;

        }
       
        protected void Page_Load(object sender, EventArgs e)
        {
            IDesignation DesignationManager;
            try
            {
                if (!IsPostBack)
                {
                    DesignationManager = (IDesignation)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDesignation, BusinessProcess.Administration");
                    DataSet theDS = DesignationManager.GetDesignation();
                    this.grdDesignation.DataSource = theDS.Tables[0];
                    this.grdDesignation.DataBind();
                    if (ViewState["grdDataSource"] == null)
                        ViewState["grdDataSource"] = theDS.Tables[0];
                    ViewState["SortDirection"] = "Desc";
                    BindGrid();
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
                return;
            }
            finally
            {
                DesignationManager = null;
            }
        }
        protected void grdDesignation_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int thePage = grdDesignation.PageIndex;
            int thePageSize = grdDesignation.PageSize;

            GridViewRow theRow = grdDesignation.Rows[e.NewSelectedIndex];

            int DesignationId = Convert.ToInt32(theRow.Cells[0].Text.ToString());
            string theUrl = string.Format("{0}designationid={1}", "frmAdmin_Designation.aspx?name=" + "Edit" + "&", DesignationId);
            Response.Redirect(theUrl);

        }
        protected void grdDesignation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(grdDesignation, "Select$" + e.Row.RowIndex.ToString()));
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string url;
            url = "frmAdmin_Designation.aspx?name=Add";
            Response.Redirect(url);
        }
        protected void grdDesignation_Sorting(object sender, GridViewSortEventArgs e)
        {
            IQCareUtils clsUtil = new IQCareUtils();
            DataView theDV;
            if (ViewState["SortDirection"].ToString() == "Asc")
            {
                theDV = clsUtil.GridSort((DataTable)ViewState["grdDataSource"], e.SortExpression, ViewState["SortDirection"].ToString());
                ViewState["SortDirection"] = "Desc";
            }
            else
            {
                theDV = clsUtil.GridSort((DataTable)ViewState["grdDataSource"], e.SortExpression, ViewState["SortDirection"].ToString());
                ViewState["SortDirection"] = "Asc";
            }
            grdDesignation.Columns.Clear();
            grdDesignation.DataSource = theDV;
            BindGrid();
        }
    }
}
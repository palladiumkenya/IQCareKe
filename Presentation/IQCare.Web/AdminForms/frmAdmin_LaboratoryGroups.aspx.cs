using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;

namespace IQCare.Web.Admin
{
    public partial class LaboratoryGroups : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ILabMst LabManager;
            try
            {
                if (!IsPostBack)
                {
                    (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Customize Lists >> ";
                    (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Laboratory";
                    LabManager = (ILabMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BLabMst, BusinessProcess.Administration");
                    DataSet theDS = LabManager.GetLabs();
                    DataView theDV = new DataView(theDS.Tables[0]);
                    theDV.RowFilter = "DataType='Group'";
                    DataTable theDt = theDV.ToTable();
                    this.grdLab.DataSource = theDt;
                    this.grdLab.DataBind();
                    ViewState["gridSortDirection"] = "Desc";
                    ViewState["FID"] = Request.QueryString["Fid"].ToString();
                    if (ViewState["grdDataSource"] == null)
                        ViewState["grdDataSource"] = theDt;
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
                LabManager = null;
            }
        }

        protected void grdLab_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //So that when the user clicks on the row - the corresponding row is edited
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(grdLab, "Select$" + e.Row.RowIndex.ToString()));
            }
        }

        protected void grdLab_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            //Edit the selected row
            int thePage = grdLab.PageIndex;
            int thePageSize = grdLab.PageSize;

            GridViewRow theRow = grdLab.Rows[e.NewSelectedIndex];
            int theIndex = thePageSize * thePage + theRow.RowIndex;

            int LabId = Convert.ToInt32(theRow.Cells[0].Text.ToString());
            String LabName = theRow.Cells[3].Text;
            string theUrl = string.Format("{0}?Name={1}&LabId={2}&Fid={3}", "frmAdminLaboratoryGroupItems.aspx", LabName, LabId, ViewState["FID"].ToString());
            // string theUrl = string.Format("./frmAdminLaboratoryGroupItems.aspx");
            Response.Redirect(theUrl, false);
        }

        #region User functions

        private void BindGrid()
        {
            //Bind the fields of the gridview
            BoundField theCol0 = new BoundField();
            theCol0.HeaderText = "Lab Test ID";
            theCol0.DataField = "LabTestID";
            theCol0.ItemStyle.CssClass = "textstyle";
            theCol0.ReadOnly = true;

            BoundField theCol1 = new BoundField();
            theCol1.HeaderText = "Priority";
            theCol1.DataField = "Sequence";
            theCol1.ItemStyle.CssClass = "textstyle";
            theCol1.SortExpression = "Sequence";
            theCol1.ItemStyle.Font.Underline = true;
            theCol1.ReadOnly = true;

            BoundField theCol2 = new BoundField();
            theCol2.HeaderText = "Lab Type";
            theCol2.ItemStyle.CssClass = "textstyle";
            theCol2.DataField = "LabTypeName";
            theCol2.SortExpression = "LabTypeName";
            theCol2.ReadOnly = true;

            BoundField theCol3 = new BoundField();
            theCol3.HeaderText = "Department";
            theCol3.ItemStyle.CssClass = "textstyle";
            theCol3.DataField = "LabDepartmentName";
            theCol3.SortExpression = "LabDepartmentName";
            theCol3.ReadOnly = true;

            BoundField theCol4 = new BoundField();
            theCol4.HeaderText = "Group Name";
            theCol4.ItemStyle.CssClass = "textstyle";
            theCol4.DataField = "LabName";
            theCol4.SortExpression = "LabName";
            theCol4.ReadOnly = true;

            BoundField theCol5 = new BoundField();
            theCol5.HeaderText = "Status";
            theCol5.DataField = "Status";
            theCol5.ItemStyle.CssClass = "textstyle";
            theCol5.SortExpression = "Status";
            theCol5.ReadOnly = true;

            ButtonField theBtn = new ButtonField();
            theBtn.ButtonType = ButtonType.Link;
            theBtn.CommandName = "Select";
            theBtn.HeaderStyle.CssClass = "textstylehidden";
            theBtn.ItemStyle.CssClass = "textstylehidden";

            grdLab.Columns.Add(theCol0);
            grdLab.Columns.Add(theCol1);
            /////grdLab.Columns.Add(theCol2);   ----- Sanjay 13 Sept 2006
            grdLab.Columns.Add(theCol3);
            grdLab.Columns.Add(theCol4);
            grdLab.Columns.Add(theCol5);

            grdLab.Columns.Add(theBtn);

            grdLab.DataBind();
            grdLab.Columns[0].Visible = false;
        }

        #endregion User functions

        protected void grdLab_Sorting(object sender, GridViewSortEventArgs e)
        {
            //sorting
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
            grdLab.Columns.Clear();
            grdLab.DataSource = theDV;
            BindGrid();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmAdmin_PMTCT_CustomItems.aspx");
        }
    }
}
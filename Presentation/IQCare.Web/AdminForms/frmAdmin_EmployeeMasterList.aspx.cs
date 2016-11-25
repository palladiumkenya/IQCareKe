using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;

namespace IQCare.Web.Admin
{
    /// <summary>
    /// 
    /// </summary>
    public partial class EmployeeMasterList : System.Web.UI.Page
    {
        /// <summary>
        /// Handles the Click event of the btnAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string url;
            url = "frmAdmin_EmployeeMaster.aspx?name=Add&CategoryId=" + Request.QueryString["CategoryId"].ToString() + "&LstName=" + Request.QueryString["LstName"].ToString() + "&Fid=" + Request.QueryString["Fid"].ToString() + "&Upd=" + Request.QueryString["Upd"].ToString() + "&CCID=" + Request.QueryString["CCID"].ToString();
            Response.Redirect(url);
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmAdmin_PMTCT_CustomItems.aspx");
        }

        /// <summary>
        /// Handles the RowDataBound event of the grdMasterEmployee control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void grdMasterEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(grdMasterEmployee, "Select$" + e.Row.RowIndex.ToString()));
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanging event of the grdMasterEmployee control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewSelectEventArgs"/> instance containing the event data.</param>
        protected void grdMasterEmployee_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int thePage = grdMasterEmployee.PageIndex;
            int thePageSize = grdMasterEmployee.PageSize;

            GridViewRow theRow = grdMasterEmployee.Rows[e.NewSelectedIndex];
            int theIndex = thePageSize * thePage + theRow.RowIndex;

            int EmployeeId = Convert.ToInt32(theRow.Cells[0].Text.ToString());
            string EmpName = Request.QueryString["LstName"].ToString().Replace(" ", "");
            string theUrl = string.Format("{0}EmployeeId={1}&LstName={2}&Fid={3}&Upd={4}&CCID={5}", "frmAdmin_EmployeeMaster.aspx?name=" + "Edit" + "&", EmployeeId, EmpName, Request.QueryString["Fid"].ToString(), Request.QueryString["Upd"].ToString(), Request.QueryString["CCID"].ToString());
            Response.Redirect(theUrl);
        }

        /// <summary>
        /// Handles the Sorting event of the grdMasterEmployee control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewSortEventArgs"/> instance containing the event data.</param>
        protected void grdMasterEmployee_Sorting(object sender, GridViewSortEventArgs e)
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
            grdMasterEmployee.Columns.Clear();
            grdMasterEmployee.DataSource = theDV;
            BindGrid();
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            IEmployeeMst EmployeeManager;
            try
            {
                if (!IsPostBack)
                {
                    (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Customize Lists >> ";
                    (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = Request.QueryString["LstName"].ToString();
                    lblHeader.Text = Request.QueryString["LstName"].ToString();
                    EmployeeManager = (IEmployeeMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BEmployeeMst, BusinessProcess.Administration");
                    DataSet theDS = EmployeeManager.GetEmployee();
                    this.grdMasterEmployee.DataSource = theDS.Tables[0];
                    this.grdMasterEmployee.DataBind();
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
                EmployeeManager = null;
            }
        }
        /// <summary>
        /// Binds the grid.
        /// </summary>
        private void BindGrid()
        {
            BoundField theCol0 = new BoundField();
            theCol0.HeaderText = "Employee ID";
            theCol0.DataField = "EmployeeID";
            theCol0.ItemStyle.CssClass = "textstyle";
            theCol0.ReadOnly = true;

            BoundField theCol2 = new BoundField();
            theCol2.HeaderText = "First Name";
            theCol2.ItemStyle.CssClass = "textstyle";
            theCol2.DataField = "FirstName";
            theCol2.SortExpression = "FirstName";
            theCol2.ReadOnly = true;

            BoundField theCol3 = new BoundField();
            theCol3.HeaderText = "Last Name";
            theCol3.ItemStyle.CssClass = "textstyle";
            theCol3.DataField = "LastName";
            theCol3.SortExpression = "LastName";
            theCol3.ReadOnly = true;

            BoundField theCol4 = new BoundField();
            theCol4.HeaderText = "Designation";
            theCol4.ItemStyle.CssClass = "textstyle";
            theCol4.DataField = "Name";
            theCol4.SortExpression = "Name";
            theCol4.ReadOnly = true;

            BoundField theCol6 = new BoundField();
            theCol6.HeaderText = "Status";
            theCol6.DataField = "Status";
            theCol6.ItemStyle.CssClass = "textstyle";
            theCol6.SortExpression = "Status";
            theCol6.ReadOnly = true;

            ButtonField theBtn = new ButtonField();
            theBtn.ButtonType = ButtonType.Link;
            theBtn.CommandName = "Select";
            theBtn.HeaderStyle.CssClass = "textstylehidden";
            theBtn.ItemStyle.CssClass = "textstylehidden";

            grdMasterEmployee.Columns.Add(theCol0);
            grdMasterEmployee.Columns.Add(theCol2);
            grdMasterEmployee.Columns.Add(theCol3);
            grdMasterEmployee.Columns.Add(theCol4);
            grdMasterEmployee.Columns.Add(theCol6);

            grdMasterEmployee.Columns.Add(theBtn);

            grdMasterEmployee.DataBind();
            grdMasterEmployee.Columns[0].Visible = false;
        }
    }
}
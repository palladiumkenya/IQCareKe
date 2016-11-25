using System;
using System.Data;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;
namespace IQCare.Web.Admin
{
     /////////////////////////////////////////////////////////////////////
            // Code Written By   : Pankaj Kumar
            // Written Date      : 25th July 2006
            // Modify Date       : Rakhi Tyagi 
            // Modification Date : 22 Feb 2007
            // Description       : Lab test List
            //
            /// /////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    public partial class LabTestlist : System.Web.UI.Page
    {
        /// <summary>
        /// The main lab identifier
        /// </summary>
        int mainLabId = 0;
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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

                    if (Request.QueryString["LabID"] != null)
                    {
                        mainLabId = int.Parse(Request.QueryString["LabID"]);
                        DataView dv = theDS.Tables[0].DefaultView;
                        dv.RowFilter = "LabTestID=" + mainLabId;
                        DataTable theDt = dv.ToTable();
                        grdLab.DataSource = theDt;
                        H1.InnerText = theDt.Rows[0]["LabName"] + " Parameters";
                    }
                    else
                    {

                       //
                        string theUrl = string.Format("{0}?Name={1}&Fid={2}", "frmAdmin_LaboratoryTestMaster.aspx", "Add", ViewState["FID"].ToString());
                        Response.Redirect(theUrl, true);
                    }
                    this.grdLab.DataBind();
                    ViewState["gridSortDirection"] = "Desc";
                    ViewState["FID"] = Request.QueryString["Fid"].ToString();
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
                LabManager = null;
            }

        }
        /// <summary>
        /// Handles the RowDataBound event of the grdLab control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void grdLab_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //So that when the user clicks on the row - the corresponding row is edited
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                //  e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(grdLab, "Select$" + e.Row.RowIndex.ToString()));
            }
        }

        /// <summary>
        /// Handles the Click event of the btnAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string theUrl;
            if (Request.QueryString["LabID"] != null)

                theUrl = string.Format("{0}Name={1}&Fid={2}&MainLabID={3}", "frmAdmin_LaboratoryTestMaster.aspx?", "Add", ViewState["FID"].ToString(), Request.QueryString["LabID"]);
            else
                theUrl = string.Format("{0}?Name={1}&Fid={2}", "frmAdmin_LaboratoryTestMaster.aspx", "Add", ViewState["FID"].ToString());
            Response.Redirect(theUrl,true);

        }

        /// <summary>
        /// Handles the SelectedIndexChanging event of the grdLab control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewSelectEventArgs"/> instance containing the event data.</param>
        protected void grdLab_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
        

        }


        #region User functions
        /// <summary>
        /// Binds the grid.
        /// </summary>
        private void BindGrid()
        {

            //Bind the fields of the gridview
            BoundField colTestId = new BoundField();
            colTestId.HeaderText = "Lab Test ID";
            colTestId.DataField = "LabTestID";
            colTestId.ItemStyle.CssClass = "textstyle";
            colTestId.ReadOnly = true;
            colTestId.Visible = false;

            BoundField colSequence = new BoundField();
            colSequence.HeaderText = "Priority";
            colSequence.DataField = "Sequence";
            colSequence.ItemStyle.CssClass = "textstyle";
            colSequence.SortExpression = "Sequence";
            colSequence.ItemStyle.Font.Underline = true;
            colSequence.ReadOnly = true;
            colSequence.Visible = false;


            BoundField colLabType = new BoundField();
            colLabType.HeaderText = "Lab Type";
            colLabType.ItemStyle.CssClass = "textstyle";
            colLabType.DataField = "LabTypeName";
            colLabType.SortExpression = "LabTypeName";
            colLabType.ReadOnly = true;

            BoundField colDept = new BoundField();
            colDept.HeaderText = "Department";
            colDept.ItemStyle.CssClass = "textstyle";
            colDept.DataField = "LabDepartmentName";
            colDept.SortExpression = "LabDepartmentName";
            colDept.ReadOnly = true;

            BoundField colLabName = new BoundField();
            colLabName.HeaderText = "Test Name";
            colLabName.ItemStyle.CssClass = "textstyle";
            colLabName.DataField = "LabName";
            colLabName.SortExpression = "LabName";
            colLabName.ReadOnly = true;

            BoundField colStatus = new BoundField();
            colStatus.HeaderText = "Status";
            colStatus.DataField = "Status";
            colStatus.ItemStyle.CssClass = "textstyle";
            colStatus.SortExpression = "Status";
            colStatus.ReadOnly = true;

            //BoundField theCol6 = new BoundField();
            //theCol6.HeaderText = "MainLabID";
            //theCol6.DataField = "MainTestID";
            //theCol6.ItemStyle.CssClass = "textstyle";

            //theCol6.ReadOnly = true;


            ButtonField theBtn = new ButtonField();
            theBtn.ButtonType = ButtonType.Button;
            theBtn.CommandName = "Select";
            theBtn.Text = "Edit Lab";
            //  theBtn.HeaderStyle.CssClass = "textstylehidden";
            //  theBtn.ItemStyle.CssClass = "textstylehidden";

            ButtonField theParamBtn = new ButtonField();
            theParamBtn.ButtonType = ButtonType.Button;
            theParamBtn.CommandName = "Edit";
            theParamBtn.Text = "Parameters";
            // theParamBtn.HeaderStyle.CssClass = "textstylehidden";
            if (Request.QueryString["LabID"] != null)
            {
                theParamBtn.ItemStyle.CssClass = "textstylehidden";
                theBtn.Text = "Edit Parameter";
            }

            grdLab.Columns.Add(colTestId);
            grdLab.Columns.Add(colSequence);
            grdLab.Columns.Add(colDept);
            grdLab.Columns.Add(colLabName);
            grdLab.Columns.Add(colStatus);


            grdLab.Columns.Add(theBtn);
            grdLab.Columns.Add(theParamBtn);

            grdLab.DataBind();
            grdLab.Columns[0].Visible = false;
            grdLab.Columns[1].Visible = false;

        }
        #endregion
        /// <summary>
        /// Handles the Sorting event of the grdLab control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewSortEventArgs"/> instance containing the event data.</param>
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
        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string theUrl;
            if (Request.QueryString["LabID"] != null)
                theUrl = string.Format("frmAdmin_LabTestList.aspx?FID={0}", ViewState["FID"]);
            else
                theUrl = "frmAdmin_PMTCT_CustomItems.aspx";
            Response.Redirect(theUrl,true);

        }

        /// <summary>
        /// Handles the RowCommand event of the grdLab control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs"/> instance containing the event data.</param>
        protected void grdLab_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow theRow;
            theRow = grdLab.Rows[int.Parse(e.CommandArgument.ToString())];
            grdLab.SelectedIndex = theRow.RowIndex;
            string labId = (grdLab.SelectedDataKey.Values["LabTestID"].ToString());
            if (e.CommandName == "Edit")
            {
               
                
              //  int mainLabId = Convert.ToInt32(theRow.Cells[1].Text.ToString());
                string theUrl = string.Format("frmAdmin_LabTestList.aspx?LabID={0}&FID={1}", labId, ViewState["FID"]);
                Response.Redirect(theUrl,true);

            }
            else if (e.CommandName == "Select")
            {
               // theRow = grdLab.Rows[int.Parse(e.CommandArgument.ToString())];
                string theUrl;
                if (Request.QueryString["LabID"] != null)

                    theUrl = string.Format("{0}LabId={1}&Fid={2}&MainLabID={3}", "frmAdmin_LaboratoryTestMaster.aspx?name=" + "Edit" + "&", labId, ViewState["FID"].ToString(), Request.QueryString["LabID"]);
                else
                    theUrl = string.Format("{0}LabId={1}&Fid={2}", "frmAdmin_LaboratoryTestMaster.aspx?name=" + "Edit" + "&", labId, ViewState["FID"].ToString());
                Response.Redirect(theUrl,true);
            }
        }
    }
}
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;
using Interface.Security;

namespace IQCare.Web.Admin
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FacilityList : System.Web.UI.Page
    {
        /////////////////////////////////////////////////////////////////////
        // Code Written By   : Sanjay Rana
        // Written Date      : 13th May 2008
        // Modification Date :
        // Description       : Facility List
        //
        /// <summary>
        /// The facility ds
        /// </summary>
        //

        #region "User Functions"

        private DataSet theFacilityDS;

        /// <summary>
        /// Binds the grid.
        /// </summary>
        /// <param name="theDT">The dt.</param>
        private void BindGrid(DataTable theDT)
        {
            grdMasterFacility.DataSource = theDT;
            BoundField theCol0 = new BoundField();
            theCol0.HeaderText = "Facility Name";
            theCol0.DataField = "FacilityName";
            theCol0.ItemStyle.CssClass = "textstyle";
            theCol0.SortExpression = "FacilityName";
            theCol0.ItemStyle.Font.Underline = true;
            theCol0.ReadOnly = true;

            BoundField theCol1 = new BoundField();
            theCol1.HeaderText = ((DataSet)ViewState["FacilityDS"]).Tables[1].Rows[0][0].ToString().Trim();
            theCol1.DataField = "CountryId";
            theCol1.ItemStyle.CssClass = "textstyle";
            theCol1.SortExpression = "CountryId";
            theCol1.ReadOnly = true;

            BoundField theCol2 = new BoundField();
            theCol2.HeaderText = ((DataSet)ViewState["FacilityDS"]).Tables[1].Rows[1][0].ToString().Trim();
            theCol2.DataField = "PosId";
            theCol2.ItemStyle.CssClass = "textstyle";
            theCol2.SortExpression = "PosId";
            theCol2.ReadOnly = true;

            BoundField theCol3 = new BoundField();
            theCol3.HeaderText = ((DataSet)ViewState["FacilityDS"]).Tables[1].Rows[2][0].ToString().Trim();
            theCol3.DataField = "SatelliteId";
            theCol3.ItemStyle.CssClass = "textstyle";
            theCol3.SortExpression = "SatelliteID";
            theCol3.ReadOnly = true;

            BoundField theCol4 = new BoundField();
            theCol4.HeaderText = "Status";
            theCol4.DataField = "Status";
            theCol4.ItemStyle.CssClass = "textstyle";
            theCol4.SortExpression = "Status";
            theCol4.ReadOnly = true;

            BoundField theCol5 = new BoundField();
            theCol5.HeaderText = "Preferred Location";
            theCol5.DataField = "Preferred";
            theCol5.ItemStyle.CssClass = "textstyle";
            theCol5.SortExpression = "Preferred";
            theCol5.ReadOnly = true;

            BoundField theCol6 = new BoundField();
            theCol6.HeaderText = "FacilityId";
            theCol6.DataField = "FacilityId";
            theCol6.ItemStyle.CssClass = "textstyle";
            theCol6.ReadOnly = true;

            ButtonField theBtn = new ButtonField();
            theBtn.ButtonType = ButtonType.Link;
            theBtn.CommandName = "Select";
            theBtn.HeaderStyle.CssClass = "textstylehidden";
            theBtn.ItemStyle.CssClass = "textstylehidden";

            grdMasterFacility.Columns.Add(theCol0);
            grdMasterFacility.Columns.Add(theCol1);
            grdMasterFacility.Columns.Add(theCol2);
            grdMasterFacility.Columns.Add(theCol3);
            grdMasterFacility.Columns.Add(theCol4);
            grdMasterFacility.Columns.Add(theCol5);
            grdMasterFacility.Columns.Add(theCol6);
            grdMasterFacility.Columns.Add(theBtn);

            grdMasterFacility.DataBind();
            grdMasterFacility.Columns[6].Visible = false;
            //grdMasterDrugs.DataBind();
            //grdMasterDrugs.Columns[0].Visible = false;
        }

        /// <summary>
        /// Fills the system dropdown.
        /// </summary>
        private void FillSystemDropdown()
        {
            BindFunctions theBindManager = new BindFunctions();
            IIQCareSystem SystemManager = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
            DataTable theDT = SystemManager.GetIQCareSystems(0);
            theBindManager.BindCombo(cmbSystem, theDT, "SystemName", "SystemId");
        }

        /// <summary>
        /// Init_s the form.
        /// </summary>
        private void Init_Form()
        {
            AuthenticationManager Authentication = new AuthenticationManager();
            IFacilitySetup FacilityMaster = (IFacilitySetup)ObjectFactory.CreateInstance("BusinessProcess.Administration.BFacility, BusinessProcess.Administration");
            theFacilityDS = FacilityMaster.GetFacilityList(Convert.ToInt32(Session["SystemId"]), ApplicationAccess.FacilitySetup, 0);
            ViewState["FacilityDS"] = theFacilityDS;
            ViewState["grdDataSource"] = theFacilityDS.Tables[0];
            ViewState["SortDirection"] = "Asc";
            BindGrid(theFacilityDS.Tables[0]);
        }

        #endregion "User Functions"

        /// <summary>
        /// Handles the Click event of the btnAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Session["FacilityDS"] = ViewState["FacilityDS"];
            string url = "frmAdmin_FacilitySetup.aspx?FacilityId=0";
            Response.Redirect(url);
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../frmFacilityHome.aspx");
        }

        /// <summary>
        /// Handles the Click event of the btnSystemSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnSystemSave_Click(object sender, EventArgs e)
        {
            try
            {
                Session["SystemId"] = cmbSystem.SelectedValue;
                IIQCareSystem SystemManager = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
                if (cmbSystem.SelectedValue == "2")
                {
                    DataTable theDT = SystemManager.GetIQCareSystems(1);
                }
                tblSystem.Visible = false;
                btnAdd.Enabled = true;
                Init_Form();
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
                return;
            }
        }

        /// <summary>
        /// Handles the RowDataBound event of the grdMasterFacility control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void grdMasterFacility_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(grdMasterFacility, "Select$" + e.Row.RowIndex.ToString()));
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanging event of the grdMasterFacility control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewSelectEventArgs"/> instance containing the event data.</param>
        protected void grdMasterFacility_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            Session["FacilityDS"] = ViewState["FacilityDS"];
            GridViewRow theRow = grdMasterFacility.Rows[e.NewSelectedIndex];
            int FacilityId = Convert.ToInt32(theRow.Cells[6].Text);
            string theUrl = string.Format("{0}?FacilityId={1}", "frmAdmin_FacilitySetup.aspx", FacilityId);
            Response.Redirect(theUrl);
        }

        /// <summary>
        /// Handles the Sorting event of the grdMasterFacility control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewSortEventArgs"/> instance containing the event data.</param>
        protected void grdMasterFacility_Sorting(object sender, GridViewSortEventArgs e)
        {
            IQCareUtils clsUtil = new IQCareUtils();
            DataView theDV;
            if (ViewState["SortDirection"].ToString() == "Asc")
            {
                ViewState["SortDirection"] = "Desc";
                theDV = clsUtil.GridSort((DataTable)ViewState["grdDataSource"], e.SortExpression, ViewState["SortDirection"].ToString());
            }
            else
            {
                ViewState["SortDirection"] = "Asc";
                theDV = clsUtil.GridSort((DataTable)ViewState["grdDataSource"], e.SortExpression, ViewState["SortDirection"].ToString());
            }
            grdMasterFacility.Columns.Clear();
            BindGrid(clsUtil.CreateTableFromDataView(theDV));
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Session["PatientId"] = 0;
                    if (Session["AppUserId"].ToString() != "")
                    {
                        AuthenticationManager Authentiaction = new AuthenticationManager();
                        if (Authentiaction.HasFunctionRight(ApplicationAccess.FacilitySetup, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false &&
                            Authentiaction.HasFunctionRight(ApplicationAccess.FacilitySetup, FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
                        {
                            btnAdd.Enabled = false;
                        }
                        (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
                        (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Facility/Satellite";
                    }
                    else
                    {
                        Session["AppUserId"] = "1";
                        //Session["SystemId"] = "1";
                        btnCancel.Enabled = false;
                    }

                    if (Session["SystemId"].ToString() == "")
                    {
                        btnAdd.Enabled = false;
                        tblSystem.Visible = true;
                        FillSystemDropdown();
                    }
                    else
                    {
                        tblSystem.Visible = false;
                        Init_Form();
                    }
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
                //DrugManager = null;
            }
        }
    }
}
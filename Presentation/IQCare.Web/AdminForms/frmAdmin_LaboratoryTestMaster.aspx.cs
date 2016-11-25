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
    // Modify By         : Vincent Yahuma
    // Modification Date : 05 March 2015
    // Description       : Lab test List
    //
    /// <summary>
    ///
    /// </summary>
    //
    public partial class LaboratoryTestMaster : System.Web.UI.Page
    {
        #region "Variable Declaration"

        //  int LabId;
        /// <summary>
        /// The lab idforselect list
        /// </summary>
        public int LabIdforselectList;

        /// <summary>
        /// The lab manager
        /// </summary>
        private ILabMst LabManager;

        #endregion "Variable Declaration"

        #region "User Functions"

        /// <summary>
        /// Binds the grid.
        /// </summary>
        /// <param name="theMainDT">The main dt.</param>
        private void BindGrid(ref DataTable theMainDT)
        {
            DataView dv = theMainDT.DefaultView;
            dv.RowFilter = "Visible = 1";
            DataTable theDT = dv.ToTable();
            if (theDT.Rows.Count == 0)
            {
                DataRow theDR = theDT.NewRow();
                theDR.SetField(0, 0);
                theDR["Visible"] = false;
                theDT.Rows.Add(theDR);
                this.grdLabUnits.DataSource = theDT;
                this.grdLabUnits.DataBind();
                this.grdLabUnits.Rows[0].Visible = false;
            }
            else
            {
                this.grdLabUnits.DataSource = theDT;
                this.grdLabUnits.DataBind();
            }
        }

        /// <summary>
        /// Fills the values drop downs.
        /// </summary>
        protected void FillValuesDropDowns()
        {
            LabManager = (ILabMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BLabMst, BusinessProcess.Administration");
            DataSet theDDDS = LabManager.GetDropDowns();
            BindFunctions BindManager = new BindFunctions();
            BindManager.BindCombo(ddDepartment, theDDDS.Tables[0], "LabDepartmentName", "LabDepartmentID");

            if (Request.QueryString["name"] == "Edit")
            {
                int LabId = Convert.ToInt32(Request.QueryString["LabId"]);
                DataSet theDS = LabManager.GetLabByID(LabId);
                txtLabName.Text = theDS.Tables[0].Rows[0]["LabName"].ToString();
                this.ddDepartment.SelectedValue = theDS.Tables[0].Rows[0]["LabDepartmentID"].ToString();
                this.ddStatus.SelectedValue = theDS.Tables[0].Rows[0]["DeleteFlag"].ToString();
                //ddlDataType.Items.FindByText(theDS.Tables[0].Rows[0]["dataType"].ToString()).Selected = true;
                if (theDS.Tables[0].Rows[0]["dataType"] != DBNull.Value)
                {
                    //ddlDataType.Items.FindByText(theDS.Tables[0].Rows[0]["dataType"].ToString()).Selected = true;
                    ddlDataType.ClearSelection();
                    ListItem item = ddlDataType.Items.FindByText(theDS.Tables[0].Rows[0]["dataType"].ToString());
                    if (item != null) item.Selected = true;
                }

                if (ddlDataType.SelectedItem.Text == "Select List")
                {
                    getselectlist(theDS.Tables[2]);
                }
                else if (ddlDataType.SelectedItem.Text == "Numeric")
                {
                    DataSet ds = LabManager.GetSubTestDetails(Convert.ToInt32(Request.QueryString["LabId"]));
                    ViewState["MstDS"] = ds;
                    DataTable theDT = ds.Tables[1];
                    DataColumn newCol = new DataColumn("Visible", typeof(bool));
                    newCol.DefaultValue = true;
                    theDT.Columns.Add(newCol);
                    this.BindGrid(ref theDT);
                }
            }
            else
            {
                ViewState["MstDS"] = LabManager.GetSubTestDetails(Convert.ToInt32(Request.QueryString["LabId"]));

                theLabNumerics = ((DataSet)ViewState["MstDS"]).Tables[1];
                DataRow theDR = theLabNumerics.NewRow();
                theDR.SetField(0, 0);
                theLabNumerics.Rows.Add(theDR);

                grdLabUnits.DataSource = theLabNumerics;
                grdLabUnits.DataBind();
                grdLabUnits.Rows[0].Visible = false;
            }
        }

        /// <summary>
        /// Gets or sets the lab numerics.
        /// </summary>
        /// <value>
        /// The lab numerics.
        /// </value>
        private DataTable theLabNumerics
        {
            get
            {
                if (base.Session["LabValues"] == null)
                    return new DataTable();
                else
                {
                    return (DataTable)base.Session["LabValues"];
                }
            }
            set
            {
                base.Session["LabValues"] = value;
            }
        }

        /// <summary>
        /// Fields the validation.
        /// </summary>
        /// <returns></returns>
        private Boolean FieldValidation()
        {
            //Validate fields input values
            if (txtLabName.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Laboratory Test";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtLabName.Focus();
                return false;
            }
            if (ddDepartment.SelectedValue == "0")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Laboratory Test";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                ddDepartment.Focus();
                return false;
            }
            if (txtSeq.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();

                #region "20-jun-07 - 1"

                //theBuilder.DataElements["Control"] = "Sequence No";
                theBuilder.DataElements["Control"] = "Priority";

                #endregion "20-jun-07 - 1"

                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtSeq.Focus();
                return false;
            }

            if ((ddlDataType.SelectedItem.Text == "Select List") && (Session["LaboratorySelectList"] == null))
            {
                IQCareMsgBox.Show("LabSelectList", this);
                return false;
            }
            if (Session["LaboratorySelectList"] != null)
            {
                DataTable dt = (DataTable)Session["LaboratorySelectList"];
                if (dt.Rows.Count == 0)
                {
                    IQCareMsgBox.Show("LabSelectList", this);
                    return false;
                }
            }

            return true;
        }

        #endregion "User Functions"

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            LabIdforselectList = 0;

            lblH2.Text = Request.QueryString["name"];
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Customize Lists >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Laboratory";

            ViewState["FID"] = Request.QueryString["Fid"].ToString();
            if (Request.QueryString["mainLabID"] != null)
                ViewState["MainLabId"] = Convert.ToInt32(Request.QueryString["mainLabID"]);
            else if (Request.QueryString["LabId"] != null)
                ViewState["MainLabId"] = Convert.ToInt32(Request.QueryString["LabId"]);
            else
                ViewState["MainLabId"] = 0;
            if (lblH2.Text == "Add")
            {
                ddStatus.Visible = false;
                lblStatus1.Visible = false;
                lblH2.Text = "Add Laboratory Test";
                tdDataType.ColSpan = 2;
            }
            else if (lblH2.Text == "Edit")
            {
                lblH2.Text = "Edit Laboratory Test";
                btnSave.Text = "Update";
                //txtLabName.Enabled = false;
            }
            try
            {
                if (!IsPostBack)
                {
                    Session.Remove("LaboratorySelectList");
                    ViewState["LabValueID"] = "";
                    ddlDataType.Attributes.Add("OnChange", "JavaScript:ShowHideBoundary();");

                    FillValuesDropDowns();

                    ViewState["UserID"] = Session["AppUserId"].ToString();
                    AuthenticationManager Authentication = new AuthenticationManager();
                    if (Convert.ToInt32(ViewState["FID"]) != 0)
                    {
                        if (Authentication.HasFunctionRight(Convert.ToInt32(ViewState["FID"]), FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                        {
                            btnSave.Enabled = false;
                        }
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
                LabManager = null;
            }
        }

        /// <summary>
        /// Getselectlists the specified dtlab.
        /// </summary>
        /// <param name="dtlab">The dtlab.</param>
        private void getselectlist(DataTable dtlab)
        {
            DataTable theDTselect = CreateSelectedTable();
            DataRow theDR;
            for (int i = 0; i < dtlab.Rows.Count; i++)
            {
                theDR = theDTselect.NewRow();
                theDR["selectlist"] = dtlab.Rows[i]["Result"].ToString().Trim();
                theDTselect.Rows.Add(theDR);
            }
            Session["LaboratorySelectList"] = theDTselect;
        }

        /// <summary>
        /// Creates the selected table.
        /// </summary>
        /// <returns></returns>
        private DataTable CreateSelectedTable()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("selectlist", System.Type.GetType("System.String"));
            return theDT;
        }

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (FieldValidation() == false)
            {
                return;
            }

            try
            {
                saveLabConfig();

                redirect();
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
        /// Saves the lab configuration.
        /// </summary>
        private void saveLabConfig()
        {
            String saveOption = Request.QueryString["name"];
            int labID;
            if (saveOption == "Add")
                labID = Convert.ToInt32(ViewState["MainLabId"]);
            else
                labID = Convert.ToInt32(Request.QueryString["LabId"]);

            DataTable theDt = null;

            if (ddlDataType.SelectedItem.Text == "Numeric")
            {
                updateNumericGridData(false);
                theDt = theLabNumerics;
            }
            else if (ddlDataType.SelectedItem.Text == "Select List")
            {
                if (Session["LaboratorySelectList"] != null)
                {
                    theDt = (DataTable)Session["LaboratorySelectList"];
                }
                else
                {
                    IQCareMsgBox.Show("LabSelectList", this);
                    return;
                }
            }

            LabManager = (ILabMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BLabMst, BusinessProcess.Administration");
            DataTable theDTr = LabManager.SaveLabTestConfig(labID, txtLabName.Text.Trim(), Convert.ToInt32(ddDepartment.SelectedValue), Convert.ToInt32(ViewState["UserID"]),
                ddlDataType.SelectedItem.Text, theDt, Convert.ToInt32(ddStatus.SelectedValue), Convert.ToInt32(ViewState["MainLabId"]), saveOption);

            if (theDTr.Rows[0].ToString() == "0")
            {
                IQCareMsgBox.Show("LabExists", this);
                return;
            }
            Session.Remove("LaboratorySelectList");
        }

        /// <summary>
        /// Handles the Click event of the btnExit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnExit_Click(object sender, EventArgs e)
        {
            redirect();
        }

        /// <summary>
        /// Redirects this instance.
        /// </summary>
        private void redirect()
        {
            //theURL = "frmAdmin_LabTestlist.aspx";
            string theUrl;
            if (Request.QueryString["mainLabID"] == null)
                theUrl = string.Format("{0}?Fid={1}", "frmAdmin_LabTestlist.aspx", ViewState["FID"]);
            else
                theUrl = string.Format("{0}?Fid={1}&LabID={2}", "frmAdmin_LabTestlist.aspx", ViewState["FID"], ViewState["MainLabId"]);

            Response.Redirect(theUrl);
        }

        /// <summary>
        /// Handles the RowCommand event of the grdLabUnits control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs"/> instance containing the event data.</param>
        protected void grdLabUnits_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("AddItem"))
            {
                updateNumericGridData();
            }
        }

        /// <summary>
        /// Updates the numeric grid data.
        /// </summary>
        /// <param name="addItem">if set to <c>true</c> [add item].</param>
        private void updateNumericGridData(Boolean addItem = true)
        {
            DataTable dt = theLabNumerics;
            // DataColumn[] dcs = new DataColumn[] { };
            dt.Clear();
            try
            {
                foreach (GridViewRow r in grdLabUnits.Rows)
                {
                    DataRow drow = dt.NewRow();

                    if (r.Visible == false) continue;
                    drow["ID"] = (((Label)r.FindControl("lblId")).Text == "" ? (Object)DBNull.Value : ((Label)r.FindControl("lblId")).Text);
                    drow["UnitID"] = ((DropDownList)r.FindControl("ddlUnitName")).SelectedValue;
                    drow["SubTestID"] = ((Label)r.FindControl("lblSubTestID")).Text;
                    drow["MinBoundaryValue"] = ((TextBox)r.FindControl("txtMinBoundaryValue")).Text;
                    drow["MaxBoundaryValue"] = ((TextBox)r.FindControl("txtMaxBoundaryValue")).Text;
                    drow["MinNormalRange"] = (((TextBox)r.FindControl("txtMinNormalRange")).Text == "" ? (Object)DBNull.Value : ((TextBox)r.FindControl("txtMinNormalRange")).Text);
                    drow["MaxNormalRange"] = (((TextBox)r.FindControl("txtMaxNormalRange")).Text == "" ? (Object)DBNull.Value : ((TextBox)r.FindControl("txtMaxNormalRange")).Text);
                    if (!addItem)//then get the current default
                        drow["DefaultUnit"] = ((CheckBox)r.FindControl("ckbDefault")).Checked ? "Yes" : "No";

                    dt.Rows.Add(drow);
                }

                if (addItem)
                {
                    DropDownList ddlNewUnitName = (DropDownList)grdLabUnits.FooterRow.FindControl("ddlNewUnitName");
                    TextBox txtNewMinBoundaryValue = (TextBox)grdLabUnits.FooterRow.FindControl("txtNewMinBoundaryValue");
                    TextBox txtNewMaxBoundaryValue = (TextBox)grdLabUnits.FooterRow.FindControl("txtNewMaxBoundaryValue");
                    TextBox txtNewMinNormalRange = (TextBox)grdLabUnits.FooterRow.FindControl("txtNewMinNormalRange");
                    TextBox txtNewMaxNormalRange = (TextBox)grdLabUnits.FooterRow.FindControl("txtNewMaxNormalRange");

                    DataRow theDR = dt.NewRow();
                    theDR.SetField("ID", DBNull.Value);
                    theDR.SetField("UnitID", ddlNewUnitName.SelectedValue);
                    String s = ((Label)grdLabUnits.Rows[0].FindControl("lblSubTestID")).Text;
                    s = (s == "" ? "0" : s);
                    theDR.SetField("SubTestID", s);
                    theDR.SetField("MinBoundaryValue", txtNewMinBoundaryValue.Text);
                    theDR.SetField("MaxBoundaryValue", txtNewMaxBoundaryValue.Text);
                    theDR.SetField("MinNormalRange", (txtNewMinNormalRange.Text == "" ? (Object)DBNull.Value : txtNewMinNormalRange.Text));
                    theDR.SetField("MaxNormalRange", (txtNewMaxNormalRange.Text == "" ? (Object)DBNull.Value : txtNewMaxNormalRange.Text));
                    theDR.SetField("DefaultUnit", "Yes");

                    dt.Rows.Add(theDR);
                }
                theLabNumerics = dt;
                grdLabUnits.DataSource = theLabNumerics;
                grdLabUnits.DataBind();
            }
            catch (Exception ex)
            {
                // this.showErrorMessage(ref ex);
                throw ex;
            }
            // fillBillGrids(false);
        }

        /// <summary>
        /// Handles the RowDataBound event of the grdLabUnits control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void grdLabUnits_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                DropDownList ddlNewUnitName = (DropDownList)e.Row.FindControl("ddlNewUnitName");

                ddlNewUnitName.DataSource = ((DataSet)ViewState["MstDS"]).Tables[0];
                ddlNewUnitName.DataTextField = "Name";
                ddlNewUnitName.DataValueField = "UnitID";
                ddlNewUnitName.DataBind();
                ddlNewUnitName.Items.Insert(0, "Select");

            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlUnitName = (DropDownList)e.Row.FindControl("ddlUnitName");
                ddlUnitName.DataSource = ((DataSet)ViewState["MstDS"]).Tables[0];
                ddlUnitName.DataTextField = "Name";
                ddlUnitName.DataValueField = "UnitID";
                ddlUnitName.DataBind();
                ddlUnitName.Items.Insert(0, "Select");
                ddlUnitName.SelectedValue = ((Label)e.Row.FindControl("lblUnitId")).Text;
            }
            else if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                DropDownList ddEditUnitName = (DropDownList)e.Row.FindControl("ddEditUnitName");
                ddEditUnitName.DataSource = theLabNumerics;
                ddEditUnitName.DataTextField = "Name";
                ddEditUnitName.DataValueField = "UnitID";
                ddEditUnitName.DataBind();
                ddEditUnitName.Items.Insert(0, "Select");
                ddEditUnitName.SelectedValue = ((TextBox)e.Row.FindControl("txtEditUnitId")).Text;
            }
        }
    }
}
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using Application.Common;
using Application.Presentation;
using Interface.Administration;

/////////////////////////////////////////////////////////////////////
// Code Written By   : Rakhi Tyagi
// Written Date      : 1 Sept 2006
// Modification Date : 30 Oct 2006
// Description       : Add/Edit UserGroup  
//  Modification Date : 16 Feb 2007
/// /////////////////////////////////////////////////////////////////
namespace IQCare.Web.Admin
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UserGroup : System.Web.UI.Page
    {
       
        /// <summary>
        /// The group identifier
        /// </summary>
        int GroupId;
        /// <summary>
        /// The i all form count
        /// </summary>
        int iAllFormCount;
        /// <summary>
        /// The i reports count
        /// </summary>
        int iReportsCount;
        /// <summary>
        /// The i admin count
        /// </summary>
        int iAdminCount;
        #region User Function
        /// <summary>
        /// Fields the validations.
        /// </summary>
        /// <returns></returns>
        private Boolean FieldValidations()
        {
            if (txtusergroupname.Text == "")
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["Control"] = "GroupName";
                IQCareMsgBox.Show("BlankTextBox", theMsg, this);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Initialises this instance.
        /// </summary>
        private void Initialise()
        {
            IUserRole UserGroupManager;
            UserGroupManager = (IUserRole)ObjectFactory.CreateInstance("BusinessProcess.Administration.BUserRole, BusinessProcess.Administration");
            DataSet theDS = UserGroupManager.GetUserGroupFeatureList(Convert.ToInt32(Session["SystemId"]), Convert.ToInt32(Session["AppLocationId"]));
            this.grdUserGroupsForm.DataSource = theDS.Tables[1];
            this.grdUserGroupsOther.DataSource = theDS.Tables[0];
            this.grdUserGroupAdminForm.DataSource = theDS.Tables[2];
            this.grdUserGroupAdminForm.DataBind();
            this.grdUserGroupsForm.DataBind();
            this.grdUserGroupsOther.DataBind();

            grdUserGroupAdminForm.Columns[1].Visible = false;
            grdUserGroupsForm.Columns[1].Visible = false;
            grdUserGroupsOther.Columns[1].Visible = false;
            if (Session["SystemId"].ToString() == "2")
            {
                TDChkEnrolID.Visible = true;
            }
            else
            {
                TDCheckCareEnd.Visible = true;
            }

        }

        /// <summary>
        /// Handles the Load event of the frmAdmin_UserGroup control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void frmAdmin_UserGroup_Load(object sender, EventArgs e)
        {
            grdUserGroupsForm.HeaderRow.Cells[0].Attributes.Add("onclick", "SelectHeaderCheckBox('" + grdUserGroupsForm.ClientID + "')");
            //((CheckBox)sender).Attributes.Add("onclick", "SelectHeaderCheckBox('" + grdUserGroupsForm.ClientID + "')");
        }

        /// <summary>
        /// Handles the Load event of the frmAdmin_UserGroupOther control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void frmAdmin_UserGroupOther_Load(object sender, EventArgs e)
        {
            grdUserGroupsOther.HeaderRow.Cells[0].Attributes.Add("onclick", "SelectHeaderReportCheckBox('" + grdUserGroupsOther.ClientID + "')");
        }

        /// <summary>
        /// Handles the Load event of the frmAdmin_AdminForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void frmAdmin_AdminForm_Load(object sender, EventArgs e)
        {
            grdUserGroupAdminForm.HeaderRow.Cells[0].Attributes.Add("onclick", "SelectHeaderAdminFormCheckBox('" + grdUserGroupAdminForm.ClientID + "')");
            //((CheckBox)sender).Attributes.Add("onclick", "SelectHeaderCheckBox('" + grdUserGroupsForm.ClientID + "')");
        }

        /// <summary>
        /// Aunthentications the function.
        /// </summary>
        private void AunthenticationFunction()
        {
            //RTyagi..19Feb 07..
            /***************** Check For User Rights ****************/
            AuthenticationManager Authentiaction = new AuthenticationManager();
            if (Request.QueryString["name"] == "Add")
            {
                if (Authentiaction.HasFunctionRight(ApplicationAccess.UserGroupAdministration, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
                {
                    btnsave.Enabled = false;
                }
            }
            else if (Request.QueryString["name"] == "Edit")
            {
                if (Authentiaction.HasFunctionRight(ApplicationAccess.UserGroupAdministration, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                {
                    string theUrl = "frmAdmin_UserGroupList.aspx";
                    Response.Redirect(theUrl);
                }
                else if (Authentiaction.HasFunctionRight(ApplicationAccess.UserGroupAdministration, FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
                {
                    btnsave.Enabled = false;
                }
            }

        }

        /// <summary>
        /// Users the group data.
        /// </summary>
        /// <returns></returns>
        public DataSet UserGroupData()
        {
            /********** Declare Local Required Variables. **********/

            DataSet theDS = new DataSet();

            DataTable theUserGroupTbl = new DataTable();
            DataTable theOtherUserGroupTbl = new DataTable();
            DataTable theAdminFormTbl = new DataTable();

            DataRow theRow;

            /********** Create DataColumn objects of data types. ********/

            theUserGroupTbl.Columns.Add("Feature", System.Type.GetType("System.Int32"));
            theUserGroupTbl.Columns.Add("Save", System.Type.GetType("System.Int32"));
            theUserGroupTbl.Columns.Add("Update", System.Type.GetType("System.Int32"));
            theUserGroupTbl.Columns.Add("Delete", System.Type.GetType("System.Int32"));
            theUserGroupTbl.Columns.Add("View", System.Type.GetType("System.Int32"));
            theUserGroupTbl.Columns.Add("Print", System.Type.GetType("System.Int32"));

            theOtherUserGroupTbl.Columns.Add("Feature", System.Type.GetType("System.Int32"));
            theOtherUserGroupTbl.Columns.Add("Save", System.Type.GetType("System.Int32"));
            theOtherUserGroupTbl.Columns.Add("Update", System.Type.GetType("System.Int32"));
            theOtherUserGroupTbl.Columns.Add("Delete", System.Type.GetType("System.Int32"));
            theOtherUserGroupTbl.Columns.Add("View", System.Type.GetType("System.Int32"));
            theOtherUserGroupTbl.Columns.Add("Print", System.Type.GetType("System.Int32"));

            theAdminFormTbl.Columns.Add("Feature", System.Type.GetType("System.Int32"));
            theAdminFormTbl.Columns.Add("Save", System.Type.GetType("System.Int32"));
            theAdminFormTbl.Columns.Add("Update", System.Type.GetType("System.Int32"));
            theAdminFormTbl.Columns.Add("Delete", System.Type.GetType("System.Int32"));
            theAdminFormTbl.Columns.Add("View", System.Type.GetType("System.Int32"));
            theAdminFormTbl.Columns.Add("Print", System.Type.GetType("System.Int32"));

            int FeatureID = 0;
            int SaveFlag = 0;
            int UpdateFlag = 0;
            int DeleteFlag = 0;
            int ViewFlag = 0;
            int PrintFlag = 0;
        

            /********** Find Controls and those values in  First GridView. *********/
            for (int i = 0; i < grdUserGroupsForm.Rows.Count; i++)
            {
                CheckBox chkFeature = (CheckBox)grdUserGroupsForm.Rows[i].Cells[0].FindControl("chkFeature");

                if (chkFeature.Checked == true)
                {
                    FeatureID = Convert.ToInt32(grdUserGroupsForm.Rows[i].Cells[1].Text);
                   
                }


                CheckBox chkSave = (CheckBox)grdUserGroupsForm.Rows[i].Cells[2].FindControl("chkSave");
                if (chkSave.Checked == true)
                {
                    SaveFlag = 4;
                   
                }


                CheckBox chkUpdate = (CheckBox)grdUserGroupsForm.Rows[i].Cells[3].FindControl("chkUpdate");
                if (chkUpdate.Checked == true)
                {
                    UpdateFlag = 2;
                   
                }


                CheckBox chkDelete = (CheckBox)grdUserGroupsForm.Rows[i].Cells[4].FindControl("chkDelete");
                if (chkDelete.Checked == true)
                {
                    DeleteFlag = 3;
                }


                CheckBox chkView = (CheckBox)grdUserGroupsForm.Rows[i].Cells[5].FindControl("chkView");
                if (chkView.Checked == true)
                {
                    ViewFlag = 1;
                   
                }

                CheckBox chkPrint = (CheckBox)grdUserGroupsForm.Rows[i].Cells[6].FindControl("chkPrint");
                if (chkPrint.Checked == true)
                {
                    PrintFlag = 5;
                
                }

                if (FeatureID != 0)
                {
                    /******* Create Row Object and Add values. *******/

                    theRow = theUserGroupTbl.NewRow();
                    theRow["Feature"] = FeatureID;
                    theRow["Save"] = SaveFlag;
                    theRow["Update"] = UpdateFlag;
                    theRow["Delete"] = DeleteFlag;
                    theRow["View"] = ViewFlag;
                    theRow["Print"] = PrintFlag;

                    /******* Add Rows into DataTable. *******/

                    theUserGroupTbl.Rows.Add(theRow);
                }
                FeatureID = 0;
                SaveFlag = 0;
                UpdateFlag = 0;
                DeleteFlag = 0;
                ViewFlag = 0;
                PrintFlag = 0;
            }

            /********* Add Table into DataSet. *********/
            theDS.Tables.Add(theUserGroupTbl);


            /********* Find Controls and those values in  Second GridView. ********/

            for (int i = 0; i < grdUserGroupsOther.Rows.Count; i++)
            {
                CheckBox chkFeature = (CheckBox)grdUserGroupsOther.Rows[i].Cells[0].FindControl("chkFeatureOther");

                if (chkFeature.Checked == true)
                {
                    FeatureID = Convert.ToInt32(grdUserGroupsOther.Rows[i].Cells[1].Text);
                }

                CheckBox chkYes = (CheckBox)grdUserGroupsOther.Rows[i].Cells[2].FindControl("chkYes");
                if (chkYes.Checked == true)
                {
                    ViewFlag = 1;
                }


                if (FeatureID != 0)
                {
                    /******* Create Row Object and Add values. *******/

                    theRow = theOtherUserGroupTbl.NewRow();
                    theRow["Feature"] = FeatureID;
                    theRow["View"] = ViewFlag;

                    /******* Add Rows into DataTable. *******/

                    theOtherUserGroupTbl.Rows.Add(theRow);
                }

                FeatureID = 0;
                ViewFlag = 0;
            }
            /********* Add Table into DataSet. *********/
            theDS.Tables.Add(theOtherUserGroupTbl);

            /********* Find Controls and those values in Third GridView. ********/

            for (int i = 0; i < grdUserGroupAdminForm.Rows.Count; i++)
            {
                CheckBox chkFeature = (CheckBox)grdUserGroupAdminForm.Rows[i].Cells[0].FindControl("chkForm");

                if (chkFeature.Checked == true)
                {
                    FeatureID = Convert.ToInt32(grdUserGroupAdminForm.Rows[i].Cells[1].Text);
                }

                CheckBox chkYes = (CheckBox)grdUserGroupAdminForm.Rows[i].Cells[2].FindControl("chk1Yes");
                if (chkYes.Checked == true)
                {
                    ViewFlag = 1;
                }

                if (FeatureID != 0)
                {
                    /******* Create Row Object and Add values. *******/

                    theRow = theAdminFormTbl.NewRow();
                    theRow["Feature"] = FeatureID;
                    theRow["View"] = ViewFlag;

                    /******* Add Rows into DataTable. *******/
                    theAdminFormTbl.Rows.Add(theRow);
                }

                FeatureID = 0;
                ViewFlag = 0;
            }
            /********* Add Table into DataSet. *********/
            theDS.Tables.Add(theAdminFormTbl);

            return theDS;
        }

        #endregion

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {

            //(Master.FindControl("lblheader") as Label).Text = "User Group Administration";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "User Group Administration";

            if (Request.QueryString["name"] != null)
            {
                lblh3.Text = Request.QueryString["name"];
            }
            //grdUserGroupAdminForm.Attributes.Add(  .ClientID + " ',);

            IUserRole UserGroupManager;

            try
            {
                if (Page.IsPostBack != true)
                {

                    if (Request.QueryString["name"] != null)
                    {
                        AunthenticationFunction();
                    }
                    Initialise();

                    UserGroupManager = (IUserRole)ObjectFactory.CreateInstance("BusinessProcess.Administration.BUserRole, BusinessProcess.Administration");
                    iAllFormCount = grdUserGroupsForm.Rows.Count;
                    iReportsCount = grdUserGroupsOther.Rows.Count;
                    iAdminCount = grdUserGroupAdminForm.Rows.Count;
                    if (iAllFormCount != 0)
                    {
                        grdUserGroupsForm.RowDataBound += new GridViewRowEventHandler(grdUserGroupsForm_RowDataBound);
                        grdUserGroupsForm.HeaderRow.Controls[0].Load += new EventHandler(frmAdmin_UserGroup_Load);
                    }
                    if (iReportsCount != 0)
                    {
                        grdUserGroupsOther.RowDataBound += new GridViewRowEventHandler(grdUserGroupsOther_RowDataBound);
                        grdUserGroupsOther.HeaderRow.Controls[0].Load += new EventHandler(frmAdmin_UserGroupOther_Load);
                    }
                    if (iAdminCount != 0)
                    {
                        grdUserGroupAdminForm.HeaderRow.Controls[0].Load += new EventHandler(frmAdmin_AdminForm_Load);
                    }
                    if (lblh3.Text == "Add")
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["Name"] = "User Group Roles";
                        IQCareMsgBox.ShowConfirm("UserGroupDetailSaveRecord", theBuilder, btnsave);
                        lblh3.Text = "Add User Group";
                    }
                    else
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["Name"] = "User Group Roles";
                        IQCareMsgBox.ShowConfirm("UserGroupDetailUpdateRecord", theBuilder, btnsave);
                        lblh3.Text = "Edit User Group";
                    }

                    if (Request.QueryString["name"] != null && Request.QueryString["name"] == "Edit")
                    {
                        GroupId = Convert.ToInt32(Request.QueryString["GroupID"]);
                        DataSet theOtherDS = UserGroupManager.GetUserGroupFeatureListByID(GroupId);
                        txtusergroupname.Text = Request.QueryString["Grpnm"].ToString();

                        #region "Fill Functions"

                        int i = 0;
                        if (theOtherDS.Tables[0].Rows.Count > 0)
                        {
                            for (i = 0; i < theOtherDS.Tables[0].Rows.Count; i++)
                            {
                                int j = 0;
                                for (j = 0; j < grdUserGroupsForm.Rows.Count; j++)
                                {
                                    if (grdUserGroupsForm.Rows[j].Cells[1].Text == theOtherDS.Tables[0].Rows[i]["featureId"].ToString())
                                    {
                                        CheckBox theChkbox = ((CheckBox)grdUserGroupsForm.Rows[j].FindControl("chkFeature"));
                                        theChkbox.Checked = true;
                                        switch (theOtherDS.Tables[0].Rows[i]["functionid"].ToString())
                                        {
                                            case "1":
                                                theChkbox = ((CheckBox)grdUserGroupsForm.Rows[j].FindControl("chkView"));
                                                theChkbox.Checked = true;
                                                break;
                                            case "2":
                                                theChkbox = ((CheckBox)grdUserGroupsForm.Rows[j].FindControl("chkUpdate"));
                                                theChkbox.Checked = true;
                                                break;
                                            case "3":
                                                theChkbox = ((CheckBox)grdUserGroupsForm.Rows[j].FindControl("chkDelete"));
                                                theChkbox.Checked = true;
                                                break;
                                            case "4":
                                                theChkbox = ((CheckBox)grdUserGroupsForm.Rows[j].FindControl("chkSave"));
                                                theChkbox.Checked = true;
                                                break;
                                            case "5":
                                                theChkbox = ((CheckBox)grdUserGroupsForm.Rows[j].FindControl("chkPrint"));
                                                theChkbox.Checked = true;
                                                break;
                                        }
                                    }
                                }
                            }
                        }

                        if (theOtherDS.Tables[1].Rows.Count > 0)
                        {
                            for (i = 0; i < theOtherDS.Tables[1].Rows.Count; i++)
                            {
                                for (int j = 0; j < grdUserGroupsOther.Rows.Count; j++)
                                {

                                    if (grdUserGroupsOther.Rows[j].Cells[1].Text == theOtherDS.Tables[1].Rows[i]["featureId"].ToString())
                                    {
                                        CheckBox theChkbox = (CheckBox)grdUserGroupsOther.Rows[j].FindControl("chkFeatureOther");
                                        theChkbox.Checked = true;

                                        switch (theOtherDS.Tables[1].Rows[i]["functionid"].ToString())
                                        {
                                            case "1":
                                                theChkbox = ((CheckBox)grdUserGroupsOther.Rows[j].FindControl("chkYes"));
                                                theChkbox.Checked = true;
                                                break;
                                        }
                                    }

                                }

                            }
                        }


                        if (theOtherDS.Tables[2].Rows.Count > 0)
                        {
                            for (i = 0; i < theOtherDS.Tables[2].Rows.Count; i++)
                            {
                                for (int j = 0; j < grdUserGroupAdminForm.Rows.Count; j++)
                                {

                                    if (grdUserGroupAdminForm.Rows[j].Cells[1].Text == theOtherDS.Tables[2].Rows[i]["featureId"].ToString())
                                    {
                                        CheckBox theChkbox = (CheckBox)grdUserGroupAdminForm.Rows[j].FindControl("chkForm");
                                        theChkbox.Checked = true;

                                        switch (theOtherDS.Tables[2].Rows[i]["functionid"].ToString())
                                        {
                                            case "1":
                                                theChkbox = ((CheckBox)grdUserGroupAdminForm.Rows[j].FindControl("chk1Yes"));
                                                theChkbox.Checked = true;
                                                break;
                                        }
                                    }

                                }

                            }
                        }
                        if (theOtherDS.Tables[3].Rows[0]["EnrollmentFlag"].ToString() == "1")
                        {
                            chkspenroll.Checked = true;
                        }
                        if (theOtherDS.Tables[3].Rows[0]["CareEndFlag"].ToString() == "1")
                        {
                            chkCareEndPrivilege.Checked = true;
                        }
                        if (theOtherDS.Tables[3].Rows[0]["IdentifierFlag"].ToString() == "1")
                        {
                            chkpatientIdentifiers.Checked = true;
                        }

                        /////////Not Required - Taken care by the User Authentication///////////
                        ////////////if (Session["AppUserName"].ToString() == "System Admin")
                        ////////////{
                        ////////////    btnsave.Enabled = true;
                        ////////////}
                        ////////////else
                        ////////////{
                        ////////////    btnsave.Enabled = false ;
                        ////////////}
                        ////////////////////////////////////////////////////////////////////////
                        #endregion

                    }
                }
            }
            catch (Exception err)
            {
                MsgBuilder theMsgBuilder = new MsgBuilder();
                theMsgBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theMsgBuilder, this);
                return;
            }
            finally
            {
                UserGroupManager = null;
            }
        }

        /// <summary>
        /// Handles the RowDataBound event of the grdUserGroupsForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void grdUserGroupsForm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                e.Row.Cells[0].Attributes.Add("onclick", "SelectRowCheckBoxes('" + e.Row.Controls[0].ClientID + "')");
            }
        }

        /// <summary>
        /// Handles the RowDataBound event of the grdUserGroupsOther control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void grdUserGroupsOther_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Attributes.Add("onclick", "SelectRowCheckBoxesOther('" + e.Row.Controls[0].ClientID + "')");
                //rupesh
                e.Row.Cells[2].Attributes.Add("onclick", "SelectRowFeatureOther('" + e.Row.Controls[2].ClientID + "')");
            }

            //if (e.Row.RowType == DataControlRowType.Header)
            //{
            //    e.Row.Cells[0].Attributes.Add("onclick", "SelectRowCheckBoxesOther('" + e.Row.Controls[0].ClientID + "')");
            //}

        }

        /// <summary>
        /// Handles the RowDataBound event of the grdUserGroupAdminForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void grdUserGroupAdminForm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Attributes.Add("onclick", "SelectRowCheckBoxesAdmin('" + e.Row.Controls[0].ClientID + "')");
                //rupesh
                e.Row.Cells[2].Attributes.Add("onclick", "SelectRowFeatureAdmin('" + e.Row.Controls[2].ClientID + "')");
            }
        }

        /// <summary>
        /// Handles the Click event of the btncancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btncancel_Click(object sender, EventArgs e)
        {
            string theUrl;
            theUrl = "frmAdmin_UserGroupList.aspx";
            Response.Redirect(theUrl);
        }

        /// <summary>
        /// Handles the Click event of the btnsave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (FieldValidations() == false)
            {
                return;
            }
            int UserGroupID = 0;
            int Flag = 0;

            DataSet theDS = new DataSet();

            IUserRole UserRoleManager = (IUserRole)ObjectFactory.CreateInstance("BusinessProcess.Administration.BUserRole, BusinessProcess.Administration");
            try
            {
                if (Request.QueryString["name"] == "Add")
                {
                    GroupId = 0;
                    CheckBox chkSelectAll = (CheckBox)grdUserGroupsForm.HeaderRow.Cells[0].FindControl("chkFormAll");
                    CheckBox chkSelectAllOther = (CheckBox)grdUserGroupsOther.HeaderRow.Cells[0].FindControl("chkOtherAll");
                    CheckBox chkSelectAllForms = (CheckBox)grdUserGroupAdminForm.HeaderRow.Cells[0].FindControl("chkAllForm");
                    theDS = UserGroupData();


                    if (theDS.Tables[0].Rows.Count <= 0 && theDS.Tables[1].Rows.Count <= 0 && theDS.Tables[2].Rows.Count <= 0)
                    {
                        IQCareMsgBox.Show("BlankRow", this);
                        return;
                    }

                    int EnrollmentPrivilage = chkspenroll.Checked == true ? 1 : 0;
                    int CareEndPrivilage = chkCareEndPrivilege.Checked == true ? 1 : 0;
                    int EditIdentifierPrivilage = chkpatientIdentifiers.Checked == true ? 1 : 0;
                    UserGroupID = (int)UserRoleManager.SaveUserGroupDetail(GroupId, txtusergroupname.Text, theDS, Convert.ToInt32(Session["AppUserId"].ToString()), Flag, EnrollmentPrivilage, CareEndPrivilage, EditIdentifierPrivilage);
                    if (UserGroupID == 0)
                    {
                        IQCareMsgBox.Show("UserGroupDetailExists", this);
                        return;
                    }
                    else
                    {
                        IQCareMsgBox.Show("UserGroupDetailSave", this);
                    }

                }
                else if (Request.QueryString["name"] == "Edit")
                {
                    int EnrollmentPrivilage = chkspenroll.Checked == true ? 1 : 0;
                    int CareEndPrivilage = chkCareEndPrivilege.Checked == true ? 1 : 0;
                    int EditIdentifierPrivilage = chkpatientIdentifiers.Checked == true ? 1 : 0;
                    GroupId = Convert.ToInt32(Request.QueryString["GroupID"]);
                    Flag = 1;

                    if (iAllFormCount != 0)
                    {
                        CheckBox chkSelectAll = (CheckBox)grdUserGroupsForm.HeaderRow.Cells[0].FindControl("chkFormAll");
                    }
                    if (iReportsCount != 0)
                    {
                        CheckBox chkSelectAllOther = (CheckBox)grdUserGroupsOther.HeaderRow.Cells[0].FindControl("chkOtherAll");
                    }
                    if (iAdminCount != 0)
                    {
                        CheckBox chkSelectAllForms = (CheckBox)grdUserGroupAdminForm.HeaderRow.Cells[0].FindControl("chkAllForm");
                    }
                    theDS = UserGroupData();

                    if (theDS.Tables[0].Rows.Count <= 0 && theDS.Tables[1].Rows.Count <= 0 && theDS.Tables[2].Rows.Count <= 0)
                    {

                        IQCareMsgBox.Show("BlankRow", this);
                        return;
                    }

                    UserRoleManager.UpdateUserGroup(GroupId, txtusergroupname.Text, theDS, Convert.ToInt32(Session["AppUserId"].ToString()), Flag, EnrollmentPrivilage, CareEndPrivilage, EditIdentifierPrivilage);
                    IQCareMsgBox.Show("UserGroupDetailUpdate", this);
                }
                string theUrl = "frmAdmin_UserGroupList.aspx";
                Response.Redirect(theUrl);

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
                UserRoleManager = null;
            }
        }

    }

}
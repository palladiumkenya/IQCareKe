using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;

namespace IQCare.Web.Admin
{
    /// <summary>
    ///
    /// </summary>
    public partial class AddUser : System.Web.UI.Page
    {
        /////////////////////////////////////////////////////////////////////
        // Code Written By   : Sanjay Rana
        // Written Date      : 25th July 2006
        // Modification Date :
        // Description       : User Master
        //
        /// <summary>
        /// The ht groups
        /// </summary>
        //

        public Hashtable htGroups;

        /// <summary>
        /// The user identifier
        /// </summary>
        private int theUserId;  /// Variable for Passing UserId///

        #region "User Functions"

        /// <summary>
        /// Clear_fieldses this instance.
        /// </summary>
        private void clear_fields()
        {
            txtfirstname.Text = "";
            txtlastname.Text = "";
            // txtusername.Text = "";
            txtPassword.Text = "";
            txtConfirmpassword.Text = "";
            txtPassword.Attributes.Remove("Value");
            txtConfirmpassword.Attributes.Remove("Value");
            lstUsergroup.ClearSelection();
            if (theUserId != 0)
            {
                btnSave.Text = "Update";
                //GetUserRecords();
            }
            txtlastname.Focus();
        }

        /// <summary>
        /// Creates the group table.
        /// </summary>
        private void CreateGroupTable()
        {
            htGroups = new Hashtable();
            htGroups.Clear();
            int i = 0;
            int j = 1;
            for (i = 0; i < lstUsergroup.Items.Count; i++)
            {
                if (Convert.ToInt32(lstUsergroup.Items[i].Selected) == 1)
                {
                    htGroups.Add(j, lstUsergroup.Items[i].Value);
                    j = j + 1;
                }
            }
        }

        /// <summary>
        /// Fields the validation.
        /// </summary>
        /// <returns></returns>
        private Boolean FieldValidation()
        {
            if (txtlastname.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Last Name";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtfirstname.Focus();
                return false;
            }
            if (txtfirstname.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "First Name";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtfirstname.Focus();
                return false;
            }
            if (txtusername.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "User Name";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtusername.Focus();
                return false;
            }
            if (txtPassword.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Password";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtPassword.Focus();
                return false;
            }
            if (txtPassword.Text.Trim() != txtConfirmpassword.Text.Trim())
            {
                IQCareMsgBox.Show("PasswordNotMatch", this);
                txtPassword.Text = "";
                txtConfirmpassword.Text = "";
                txtPassword.Focus();
                return false;
            }

            if (htGroups.Count < 1)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "User Group";
                IQCareMsgBox.Show("BlankList", theBuilder, this);
                lstUsergroup.Focus();
                return false;
            }
            DataSet theXMLDS = new DataSet();
            theXMLDS.ReadXml(Server.MapPath("..\\XMLFiles\\AllMasters.con"));
            DataView theDV = new DataView(theXMLDS.Tables["Mst_Facility"]);
            theDV.RowFilter = "FacilityId=" + Convert.ToInt32(Session["AppLocationId"]);
            if (theDV[0]["StrongPassFlag"] != DBNull.Value)
            {
                if (Convert.ToInt32(theDV[0]["StrongPassFlag"]) == 1)
                {
                    if (!IsStrongPassword(txtPassword.Text.Trim(), txtfirstname.Text, txtlastname.Text, txtusername.Text))
                    {
                        IQCareMsgBox.Show("StrongPassword", this);
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Fill_dropdownses this instance.
        /// </summary>
        private void fill_dropdowns()
        {
            try
            {
                Iuser CmbManager;
                CmbManager = (Iuser)ObjectFactory.CreateInstance("BusinessProcess.Administration.BUser,BusinessProcess.Administration");
                DataSet theDS = CmbManager.FillDropDowns();
                CmbManager = null;

                //// User Groups List
                BindFunctions GblCls = new BindFunctions();
                GblCls.BindCheckedList(lstUsergroup, theDS.Tables[0], "groupname", "groupid");

                //// EmployeeList
                IQCareUtils theUtils = new IQCareUtils();
                DataTable dt = new DataTable();
                DataSet theXMLDS = new DataSet();
                theXMLDS.ReadXml(Server.MapPath("..\\XMLFiles\\AllMasters.con"));
                DataView theDV = new DataView(theXMLDS.Tables["Mst_Employee"]);
                theDV.RowFilter = "DeleteFlag=0";
                if (theDV.Table != null)
                {
                    dt = theUtils.CreateTableFromDataView(theDV);
                }

                GblCls.BindCombo(ddEmployee, dt, "EmployeeName", "EmployeeId");
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }

        /// <summary>
        /// Gets the user records.
        /// </summary>
        private void GetUserRecords()
        {
            try
            {
                Iuser UserManager;
                UserManager = (Iuser)ObjectFactory.CreateInstance("BusinessProcess.Administration.BUser, BusinessProcess.Administration");
                DataSet theDS = UserManager.GetUserRecord(theUserId);
                txtfirstname.Text = theDS.Tables[0].Rows[0]["UserFirstName"].ToString();
                txtlastname.Text = theDS.Tables[0].Rows[0]["UserLastName"].ToString();
                if (theUserId != 0 && txtusername.Text == "")
                {
                    txtusername.Text = theDS.Tables[0].Rows[0]["UserName"].ToString();
                }
                Utility theUtil = new Utility();
                string thePass = theUtil.Decrypt(theDS.Tables[0].Rows[0]["Password"].ToString());
                txtPassword.Attributes.Add("Value", thePass);
                txtConfirmpassword.Attributes.Add("Value", thePass);
                ddEmployee.SelectedValue = theDS.Tables[0].Rows[0]["EmployeeId"].ToString();
                //txtusername.ReadOnly = true;

                foreach (DataRow theDR in theDS.Tables[1].Rows)
                {
                    int i = 0;
                    for (i = 0; i < lstUsergroup.Items.Count; i++)
                    {
                        if (lstUsergroup.Items[i].Value == theDR["groupid"].ToString())
                        {
                            lstUsergroup.Items[i].Selected = true;

                            if (lstUsergroup.Items[i].Text == "System Admin")
                            {
                                lstUsergroup.Items[i].Enabled = false;
                                txtfirstname.ReadOnly = true;
                                txtlastname.ReadOnly = true;
                                txtusername.ReadOnly = true;
                                btnDelete.Enabled = false;
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }
        #endregion "User Functions"

        /// <summary>
        /// Determines whether [is strong password] [the specified password].
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="fname">The fname.</param>
        /// <param name="lname">The lname.</param>
        /// <param name="uname">The uname.</param>
        /// <returns></returns>
        public static bool IsStrongPassword(string password, string fname, string lname, string uname)
        {
            // Minimum and Maximum Length of field - 6 to 12 Characters
            if (password.Length < 6)
                return false;

            // Special Characters - Not Allowed
            // Spaces - Not Allowed
            //if (!(password.All(c => char.IsLetter(c) || char.IsDigit(c))))
            //    return false;

            // Numeric Character - At least one character
            if (!password.Any(c => char.IsDigit(c)))
                return false;

            // At least one Capital Letter
            if (!password.Any(c => char.IsUpper(c)))
                return false;

            string[] Badwords = { "password", Convert.ToString(fname), Convert.ToString(lname), Convert.ToString(uname) };

            if (Badwords.Any(b => password.ToLower().Contains(b.ToLower())))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Page_Init(sender, e);
        }

        /// <summary>
        /// Handles the Click event of the btnDelete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Iuser UserManager;
            int theAffectedRow = 0;
            string theUrl;
            try
            {
                if (theUserId != 0)
                {
                    UserManager = (Iuser)ObjectFactory.CreateInstance("BusinessProcess.Administration.BUser, BusinessProcess.Administration");
                    theAffectedRow = (int)UserManager.DeleteUserRecord(theUserId);
                }
                if (theAffectedRow != 0)
                {
                    theUrl = "frmAdmin_UserList.aspx";
                    Response.Redirect(theUrl);
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
                UserManager = null;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnExit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmAdmin_UserList.aspx");
        }

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            CreateGroupTable();

            if (FieldValidation() == false)
            {
                return;
            }

            Iuser UserManager;
            try
            {
                UserManager = (Iuser)ObjectFactory.CreateInstance("BusinessProcess.Administration.BUser,BusinessProcess.Administration");
                if (btnSave.Text == "Save")
                {
                    int UserId = UserManager.SaveNewUser(txtfirstname.Text, txtlastname.Text, txtusername.Text, txtPassword.Text,
                        Convert.ToInt32(Session["AppUserId"]), Convert.ToInt32(ddEmployee.SelectedValue), htGroups);
                    if (UserId == 0)
                    {
                        IQCareMsgBox.Show("UserExists", this);
                        txtusername.Focus();
                        return;
                    }
                    else
                    {
                        IQCareMsgBox.Show("UserSave", this);
                        //Page_Init(sender, e);
                    }
                }
                else
                {
                    UserManager.UpdateUserRecord(txtfirstname.Text, txtlastname.Text, txtusername.Text, txtPassword.Text,
                        theUserId, Convert.ToInt32(Session["AppUserId"]), Convert.ToInt32(ddEmployee.SelectedValue), htGroups);
                    IQCareMsgBox.Show("UserUpdate", this);
                    //Page_Init(sender, e);
                }
                btnExit_Click(sender, e);
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
                UserManager = null;
            }
        }

        /// <summary>
        /// Handles the Init event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Init(object sender, EventArgs e)
        {
            //(Master.FindControl("lblheader") as Label).Text = "User Administration";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "User Administration";
            theUserId = Convert.ToInt32(Request.QueryString["SelectedUserId"]);
            fill_dropdowns();
            clear_fields();
            AuthenticationManager Authentication = new AuthenticationManager();

            if (theUserId == 0)
            {
                if (Authentication.HasFunctionRight(ApplicationAccess.UserAdministration, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                {
                    btnSave.Enabled = false;
                }
                IQCareMsgBox.ShowConfirm("UserSaveRecord", btnSave);
                lblh2.Text = "Add User";
                btnDelete.Visible = false;
            }
            else
            {
                if (Authentication.HasFunctionRight(ApplicationAccess.UserAdministration, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                {
                    btnSave.Enabled = false;
                }

                if (Authentication.HasFunctionRight(ApplicationAccess.UserAdministration, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                {
                    btnDelete.Enabled = false;
                }
                IQCareMsgBox.ShowConfirm("UpdateUserRecord", btnSave);
                IQCareMsgBox.ShowConfirm("RemoveUserRecord", btnDelete);
                lblh2.Text = "Edit/Remove User";
                btnDelete.Visible = true;
            }
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet theXMLDS = new DataSet();
            theXMLDS.ReadXml(Server.MapPath("..\\XMLFiles\\AllMasters.con"));
            DataView theDV = new DataView(theXMLDS.Tables["Mst_Facility"]);
            theDV.RowFilter = "FacilityId=" + Convert.ToInt32(Session["AppLocationId"]);

            if (theDV[0]["StrongPassFlag"] != DBNull.Value)
            {
                if (Convert.ToInt32(theDV[0]["StrongPassFlag"]) == 1)
                {
                    txtPassword.Attributes.Remove("onblur");
                    txtPassword.Attributes.Add("onblur", "if (validnewuserstrngpwd()){\n alert(document.getElementById('unique_id').innerHTML)\n}");
                }
            }
            if (!IsPostBack)
            {
                if (theUserId != 0)
                {
                    GetUserRecords();
                    if (theDV[0]["StrongPassFlag"] != DBNull.Value)
                    {
                        if (Convert.ToInt32(theDV[0]["StrongPassFlag"]) == 1)
                        {
                            txtPassword.Attributes.Remove("onblur");
                            txtPassword.Attributes.Add("onblur", "if (validstrngpwd('" + txtfirstname.Text + "','" + txtlastname.Text + "','" + txtusername.Text + "')){\n alert(document.getElementById('unique_id').innerHTML)\n}");
                        }
                    }
                }
                else
                {
                    if (theDV[0]["StrongPassFlag"] != DBNull.Value)
                    {
                        if (Convert.ToInt32(theDV[0]["StrongPassFlag"]) == 1)
                        {
                            txtPassword.Attributes.Remove("onblur");
                            txtPassword.Attributes.Add("onblur", "if (validnewuserstrngpwd()){\n alert(document.getElementById('unique_id').innerHTML)\n}");
                        }
                    }
                }
            }
        }
    }
}
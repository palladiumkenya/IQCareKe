using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;

namespace IQCare.Web.Admin
{
    public partial class IQCStageCustomPage : System.Web.UI.Page
    {
        /////////////////////////////////////////////////////////////////////
        // Code Written By   : Meetu Rahul
        // Written Date      : 25th Sep 2008
        // Modification Date :
        // Description       : Stage Custom Master Page
        // Code Enhanced By   :
        /// /////////////////////////////////////////////////////////////////

        #region "UserFunctions"

        private void Clear_Fields()
        {
            if (ViewState["TableName"].ToString() != "HivDisease")
            {
                // lblName.Text = ViewState["ListName"].ToString() + " :";

                ////////// Done by Sanjay on 19th Sept 2006  /////////////////////////////////////////
                ////////// For all the Custom List the ListName field of XML file will be Used //////
                ////if (Convert.ToInt32(ViewState["CategoryId"]) > 0)
                ////  lblName.Text = ViewState["ListName"].ToString() + " :";
                //////////////////////////////////////////////////////////////////////////////////////
            }
            else
            {
                lblName.Text = "OIs or AIDS Defining Illnesses :";
            }
            txtName.Text = "";
            txtSeqNo.Text = "";
            txtcode.Text = "";
            txtStage.Text = "";

            ddStatus.SelectedValue = "0";
            if (Convert.ToInt32(ViewState["Id"]) != 0)
                GetData();
            txtcode.Focus();
        }

        private void GetData()
        {
            ICustomList CustomManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList, BusinessProcess.Administration");
            DataTable theDT = CustomManager.GetCustomMasterDetails(ViewState["TableName"].ToString(), Convert.ToInt32(ViewState["Id"].ToString()), Convert.ToInt32(Session["SystemId"]));
            ViewState["CustomData"] = theDT;

            txtStage.Text = theDT.Rows[0]["Stage"].ToString();
            txtcode.Text = theDT.Rows[0]["Code"].ToString();
            txtName.Text = theDT.Rows[0]["Name"].ToString();
            txtSeqNo.Text = theDT.Rows[0]["SRNO"].ToString();

            if (Convert.ToBoolean(Convert.ToInt32(theDT.Rows[0]["DeleteFlag"])) == true)
            {
                ddStatus.SelectedValue = "1";
            }
        }

        private Boolean FieldValidation()
        {
            DataTable dtretout = new DataTable();
            ICustomList CustomManager;
            CustomManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList,BusinessProcess.Administration");

            if (txtName.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = lblName.Text;
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtName.Focus();
                return false;
            }
            if (txtSeqNo.Text.Trim() == "" && ddStatus.SelectedValue != "1")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Priority";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtSeqNo.Focus();
                return false;
            }
            if (txtcode.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Code";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtcode.Focus();
                return false;
            }

            if (txtStage.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Stage";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtStage.Focus();
                return false;
            }
            if (txtSeqNo.Text == "0")
            {
                IQCareMsgBox.Show("ChkPriority", this);
                return false;
            }

            //if (dtretout.Rows.Count > 0)
            //{
            //    if (dtretout.Rows[0]["Result"].ToString() == "-1")
            //    {
            //        string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            //        script += "var ans=true;\n";
            //        script += "alert('Code and Name already exists');\n";
            //        script += "</script>\n";
            //        ClientScript.RegisterStartupScript(this.GetType(),"confirm", script);
            //        //    //ClientScript.RegisterStartupScript(this.GetType(),"startupScript", "<script> language=JavaScript>alert('Code already exists');</script>");
            //    }
            //    else if (dtretout.Rows[0]["Result"].ToString() == "-2")
            //    {
            //        string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            //        script += "var ans=true;\n";
            //        script += "alert('Name already exists');\n";
            //        script += "</script>\n";
            //        ClientScript.RegisterStartupScript(this.GetType(),"confirm", script);
            //    }
            //    else if (dtretout.Rows[0]["Result"].ToString() == "-3")
            //    {
            //        string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            //        script += "var ans=true;\n";
            //        script += "alert('Code already exists');\n";
            //        script += "</script>\n";
            //        ClientScript.RegisterStartupScript(this.GetType(),"confirm", script);
            //    }
            //    else if (dtretout.Rows[0]["Result"].ToString() == "0")
            //    {
            //        return true;
            //    }

            //    return false;
            //}

            return true;
        }

        #endregion "UserFunctions"

        protected void Page_Load(object sender, EventArgs e)
        {
            txtSeqNo.Attributes.Add("onkeyup", "chkInteger('" + txtSeqNo.ClientID + "');");

            if (Page.IsPostBack != true)
            {
                try
                {
                    ViewState["Id"] = Convert.ToInt32(Request.QueryString["SelectedId"]);
                    Session["UpdateID"] = Convert.ToInt32(Request.QueryString["SelectedId"]);
                    ViewState["TableName"] = Request.QueryString["TableName"].ToString();
                    ViewState["CategoryId"] = Request.QueryString["CategoryId"].ToString();
                    ViewState["ListName"] = Request.QueryString["LstName"].ToString();
                    //ViewState ["DeleteFlag"] =
                    //if (ViewState["ListName"].ToString() == "Patient Referred")
                    //{
                    //    tdstage.Visible = false;
                    //    tdpriority.ColSpan = 2;

                    //}
                    if (Request.QueryString["CCID"] != null)
                    {
                        ViewState["CCID"] = Request.QueryString["CCID"].ToString();
                    }
                    if (!String.IsNullOrEmpty(Request.QueryString["ModId"]))
                    {
                        ViewState["ModuleId"] = Request.QueryString["ModId"].ToString();
                    }
                    ViewState["FID"] = Request.QueryString["Fid"].ToString();
                    ViewState["Update"] = Request.QueryString["Upd"].ToString();
                    //(Master.FindControl("lblRoot") as Label).Text = " » Customize Lists";
                    //(Master.FindControl("lblMark") as Label).Visible = false;
                    //(Master.FindControl("lblheader") as Label).Text = ViewState["ListName"].ToString();
                    (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Customize Lists >> ";
                    (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = ViewState["ListName"].ToString();
                    if (ViewState["ListName"].ToString() == "Emergency Contact Relationship")
                    {
                        //(Master.FindControl("lblheader") as Label).Text = "Emerg. Cont. Relationship";
                        (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Emerg. Cont. Relationship";
                    }
                    if (ViewState["ListName"].ToString() == "Scheduler - Appointment purpose")
                    {
                        //(Master.FindControl("lblheader") as Label).Text = "Sched. Appoi. Purpose";
                        (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Sched. Appoi. Purpose";
                    }
                    //txtSeqNo.Attributes.Add("onkeyup", "");

                    txtSeqNo.Attributes.Add("onkeyup", "chkPostiveInteger('" + txtSeqNo.ClientID + "')");
                    Clear_Fields();

                    AuthenticationManager Authentication = new AuthenticationManager();
                    if (Convert.ToInt32(ViewState["Id"]) == 0)
                    {
                        if (Convert.ToInt32(ViewState["FID"]) != 0)
                        {
                            if (Authentication.HasFunctionRight(Convert.ToInt32(ViewState["FID"]), FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                            {
                                btnSave.Enabled = false;
                            }
                        }
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["Name"] = ViewState["ListName"].ToString();
                        if (Convert.ToInt32(ViewState["CategoryId"]) > 0)
                            theBuilder.DataElements["Name"] = ViewState["ListName"].ToString();
                        IQCareMsgBox.ShowConfirm("CustomSaveRecord", theBuilder, btnSave);
                        if (ViewState["TableName"].ToString() != "HivDisease")
                        {
                            lblHeader.Text = "Add " + ViewState["ListName"].ToString();
                            if (Convert.ToInt32(ViewState["CategoryId"]) > 0)
                                lblHeader.Text = "Add " + ViewState["ListName"].ToString();
                        }
                        else
                        {
                            lblHeader.Text = "Add OIs or AIDS Defining Illnesses";
                        }
                        tdStatus.Visible = false;
                    }
                    else
                    {
                        if (Convert.ToInt32(ViewState["FID"]) != 0)
                        {
                            if (Authentication.HasFunctionRight(Convert.ToInt32(ViewState["FID"]), FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                            {
                                btnSave.Enabled = false;
                            }
                        }

                        /////////////Imposing Business Rule of Limiting Custom List to Add Only /////////
                        if (Convert.ToInt32(ViewState["Update"]) != 1)
                        {
                            DataTable DTupdateflag = new DataTable();
                            ICustomList CustomManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList, BusinessProcess.Administration");
                            DTupdateflag = CustomManager.GetCustomListUpdateFlag(Convert.ToString(ViewState["TableName"]), Convert.ToInt32(ViewState["Id"]), Convert.ToInt32(Session["SystemId"].ToString()));
                            if (DTupdateflag.Rows[0]["UpdateFlag"] != System.DBNull.Value)
                            {
                                if (DTupdateflag.Rows[0].IsNull("UpdateFlag") == true)
                                    ViewState["UpdateFlag"] = 0;
                                else
                                    ViewState["UpdateFlag"] = DTupdateflag.Rows[0]["UpdateFlag"];
                                // int UpdateFlag = Convert.ToInt32(DTupdateflag.Rows[0]["UpdateFlag"]);
                                if (Convert.ToInt32(ViewState["UpdateFlag"]) == 0)
                                {
                                    btnSave.Enabled = false;
                                }
                            }
                        }
                        /////////////////////////////////////////////////////////////////////////////////

                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["Name"] = ViewState["ListName"].ToString();
                        if (Convert.ToInt32(ViewState["CategoryId"]) > 0)
                            theBuilder.DataElements["Name"] = ViewState["ListName"].ToString();
                        IQCareMsgBox.ShowConfirm("CustomUpdateRecord", theBuilder, btnSave);
                        if (ViewState["TableName"].ToString() != "HivDisease")
                        {
                            lblHeader.Text = "Edit" + ViewState["ListName"].ToString();
                            if (Convert.ToInt32(ViewState["CategoryId"]) > 0)
                                lblHeader.Text = "Edit" + ViewState["ListName"].ToString();
                        }
                        else
                        {
                            lblHeader.Text = "Edit OIs or AIDS Defining Illnesses";
                        }
                        tdStatus.Visible = false;
                        btnSave.Text = "Update";
                        tdStatus.Visible = true;
                    }
                }
                catch (Exception err)
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["MessageText"] = err.Message.ToString();
                    IQCareMsgBox.Show("#C1", theBuilder, this);
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (FieldValidation() == false)
            {
                return;
            }

            ICustomList CustomManager;
            try
            {
                CustomManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList,BusinessProcess.Administration");
                DataTable DTupdateprior = new DataTable();
                ICustomList PriorManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList, BusinessProcess.Administration");
                if (btnSave.Text == "Save")
                {
                    DTupdateprior = PriorManager.GetCustomListUpdatePriortorize(Convert.ToString(ViewState["TableName"]), Convert.ToInt32(ViewState["CategoryId"]), txtSeqNo.Text, Convert.ToInt32(Session["SystemId"]));
                    if (DTupdateprior.Rows[0]["UpdateFlag"] != System.DBNull.Value)
                    {
                        if (DTupdateprior.Rows[0].IsNull("UpdateFlag") == true)
                            ViewState["UpdateFlag"] = 0;
                        else
                            ViewState["UpdateFlag"] = DTupdateprior.Rows[0]["UpdateFlag"];
                        if (Convert.ToInt32(ViewState["Updateprior"]) == 2)
                        {
                            IQCareMsgBox.Show("CustomListPriotorize", this);
                            return;
                        }
                    }

                    string strName;
                    strName = txtName.Text.Trim();
                    txtName.Text = strName.Replace("'", "''");
                    string strCode;
                    strCode = txtcode.Text.Trim();
                    txtcode.Text = strCode.Replace("'", "''");

                    string strStage;
                    strStage = txtStage.Text.Trim();
                    txtStage.Text = strStage.Replace("'", "''");

                    DataTable RowsAffected = CustomManager.SaveCustomMasterRecord(ViewState["TableName"].ToString(), ViewState["ListName"].ToString(), txtName.Text.Trim(), txtcode.Text.Trim(), txtStage.Text.Trim(), Convert.ToInt32(txtSeqNo.Text), Convert.ToInt32(ViewState["CategoryId"]), Convert.ToInt32(Session["AppUserId"]), Convert.ToInt32(Session["SystemId"]), Convert.ToInt32(ViewState["CCID"]), Convert.ToInt32(ViewState["ModuleId"]), "");

                    if (RowsAffected.Rows.Count <= 0)
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["Name"] = ViewState["ListName"].ToString();

                        if (Convert.ToInt32(ViewState["CategoryId"]) > 0)
                            theBuilder.DataElements["Name"] = ViewState["ListName"].ToString();
                        IQCareMsgBox.Show("CustomMasterExists", theBuilder, this);
                        return;
                    }
                    else
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["Name"] = ViewState["ListName"].ToString();
                        if (Convert.ToInt32(ViewState["CategoryId"]) == 0)
                        {
                            ViewState["CategoryId"] = RowsAffected.Rows[0][0].ToString();
                        }
                        if (Convert.ToInt32(ViewState["CategoryId"]) > 0)
                            theBuilder.DataElements["Name"] = ViewState["ListName"].ToString();
                        IQCareMsgBox.Show("CustomMasterSave", theBuilder, this);
                        Clear_Fields();
                    }
                    //string Url = string.Format("{0}?TableName={1}&CategoryId={2}&LstName={3}&Fid={4}&Upd={5}", "frmAdmin_IQCStageCustomList.aspx", ViewState["TableName"].ToString(), ViewState["CategoryId"].ToString(), ViewState["ListName"].ToString(), ViewState["FID"].ToString(), ViewState["Update"].ToString());
                    string Url = string.Format("{0}?TableName={1}&CategoryId={2}&LstName={3}&Fid={4}&Upd={5}&ModId={6)", "frmAdmin_IQCStageCustomList.aspx", ViewState["TableName"].ToString(), ViewState["CategoryId"].ToString(), ViewState["ListName"].ToString(), ViewState["FID"].ToString(), ViewState["Update"].ToString(), ViewState["ModuleId"].ToString());
                    Response.Redirect(Url);

                }
                else
                {
                    string strName;
                    strName = txtName.Text.Trim();
                    txtName.Text = strName.Replace("'", "''");
                    string strCode;
                    strCode = txtcode.Text.Trim();
                    txtcode.Text = strCode.Replace("'", "''");

                    string strStage;
                    strStage = txtStage.Text.Trim();
                    txtStage.Text = strStage.Replace("'", "''");

                    DTupdateprior = PriorManager.GetCustomListUpdatePriortorize(Convert.ToString(ViewState["TableName"]), Convert.ToInt32(ViewState["CategoryId"]), txtSeqNo.Text, Convert.ToInt32(Session["SystemId"]));

                    if (DTupdateprior.Rows.Count > 0)
                    {
                        if (DTupdateprior.Rows[0]["UpdateFlag"] != System.DBNull.Value)
                        {
                            if (DTupdateprior.Rows[0].IsNull("UpdateFlag") == true)
                                ViewState["UpdateFlag"] = 0;
                            else
                                ViewState["UpdateFlag"] = DTupdateprior.Rows[0]["UpdateFlag"];
                            if (Convert.ToInt32(ViewState["Updateprior"]) == 2)
                            {
                                IQCareMsgBox.Show("CustomListPriotorize", this);
                                return;
                            }
                        }
                    }

                    if (txtSeqNo.Text != "")
                    {
                        CustomManager.UpdateCustomMasterRecord(Convert.ToString(ViewState["TableName"]), Convert.ToInt32(ViewState["Id"]), txtName.Text.Trim(), txtcode.Text.Trim(), txtStage.Text.Trim(), Convert.ToInt32(txtSeqNo.Text), Convert.ToInt32(ViewState["CategoryId"]), Convert.ToInt32(ddStatus.SelectedValue), Convert.ToInt32(Session["AppUserId"]), Convert.ToInt32(Session["SystemId"]), Convert.ToInt32(ViewState["CCID"]), Convert.ToInt32(ViewState["ModuleId"]), "");
                    }
                    else
                    {
                        CustomManager.UpdateCustomMasterRecord(Convert.ToString(ViewState["TableName"]), Convert.ToInt32(ViewState["Id"]), txtName.Text.Trim(), txtcode.Text.Trim(), txtStage.Text.Trim(), 0, Convert.ToInt32(ViewState["CategoryId"]), Convert.ToInt32(ddStatus.SelectedValue), Convert.ToInt32(Session["AppUserId"]), Convert.ToInt32(Session["SystemId"]), Convert.ToInt32(ViewState["CCID"]), Convert.ToInt32(ViewState["ModuleId"]), "");
                    }

                    if (ddStatus.SelectedValue == "1" && txtSeqNo.Text == "")
                    {
                        IQCareMsgBox.Show("CustomListInactiveUpdate", this);
                    }
                    else
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["Name"] = ViewState["ListName"].ToString();
                        if (Convert.ToInt32(ViewState["CategoryId"]) > 0)
                            theBuilder.DataElements["Name"] = ViewState["ListName"].ToString();
                        IQCareMsgBox.Show("CustomMasterUpdate", theBuilder, this);
                        ViewState["Id"] = 0;
                        Clear_Fields();
                        //string Url = string.Format("{0}?TableName={1}&CategoryId={2}&LstName={3}&Fid={4}&Upd={5}", "frmAdmin_IQCStageCustomList.aspx", ViewState["TableName"].ToString(), ViewState["CategoryId"].ToString(), ViewState["ListName"].ToString(), ViewState["FID"].ToString(), ViewState["Update"].ToString());
                        string Url = string.Format("{0}?TableName={1}&CategoryId={2}&LstName={3}&Fid={4}&Upd={5}&ModId={6)", "frmAdmin_IQCStageCustomList.aspx", ViewState["TableName"].ToString(), ViewState["CategoryId"].ToString(), ViewState["ListName"].ToString(), ViewState["FID"].ToString(), ViewState["Update"].ToString(), ViewState["ModuleId"].ToString());
                        Response.Redirect(Url);
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
                CustomManager = null;
            }
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            //string Url = string.Format("{0}?TableName={1}&CategoryId={2}&LstName={3}&Fid={4}&Upd={5}", "frmAdmin_IQCStageCustomList.aspx", ViewState["TableName"].ToString(), ViewState["CategoryId"].ToString(), ViewState["ListName"].ToString(), ViewState["FID"].ToString(), ViewState["Update"].ToString());
            string Url = string.Format("{0}?TableName={1}&CategoryId={2}&LstName={3}&Fid={4}&Upd={5}&ModId={6)", "frmAdmin_IQCStageCustomList.aspx", ViewState["TableName"].ToString(), ViewState["CategoryId"].ToString(), ViewState["ListName"].ToString(), ViewState["FID"].ToString(), ViewState["Update"].ToString(), ViewState["ModuleId"].ToString());
            Response.Redirect(Url);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clear_Fields();
        }

    }
}
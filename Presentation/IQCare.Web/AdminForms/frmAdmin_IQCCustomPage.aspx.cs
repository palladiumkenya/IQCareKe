using System;
using System.Data;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;

namespace IQCare.Web.Admin
{
    public partial class IQCCustomPage : System.Web.UI.Page
    {
        private Boolean FieldValidation()
        {

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
            if (txtSeqNo.Text == "0")
            {
                IQCareMsgBox.Show("ChkPriority", this);
                return false;

            }


            return true;
        }

        private void Clear_Fields()
        {
            txtcode.Text = "";
            txtName.Text = "";
            txtSeqNo.Text = "";

            ddStatus.SelectedValue = "0";
            if (Convert.ToInt32(ViewState["Id"]) != 0)
                GetData();
            txtcode.Focus();

        }
        private void GetData()
        {
            ICustomList CustomManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList, BusinessProcess.Administration");
            DataTable theDT = CustomManager.GetCustomMasterDetails(ViewState["TableName"].ToString(), Convert.ToInt32(ViewState["Id"]), Convert.ToInt32(Session["SystemId"]));
            txtName.Text = theDT.Rows[0]["Name"].ToString();
            txtSeqNo.Text = theDT.Rows[0]["SRNO"].ToString();
            txtcode.Text = theDT.Rows[0]["Code"].ToString();

            if (Convert.ToBoolean(Convert.ToInt32(theDT.Rows[0]["DeleteFlag"])) == true)
            {
                ddStatus.SelectedValue = "1";
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ViewState["Id"] = Convert.ToInt32(Request.QueryString["SelectedId"]);
                    if (Request.QueryString["TableName"].ToString() == "PreDefinedFields")
                    {
                        ViewState["TableName"] = "ModDeCode";
                    }
                    else
                    {
                        ViewState["TableName"] = Request.QueryString["TableName"].ToString();
                    }
                    ViewState["CategoryId"] = Request.QueryString["CategoryId"].ToString();
                    ViewState["ListName"] = Request.QueryString["LstName"].ToString();
                    ViewState["FID"] = Request.QueryString["Fid"].ToString();
                    ViewState["Update"] = Request.QueryString["Upd"].ToString();
                    if (Request.QueryString["CCID"] != null)
                    {
                        ViewState["CCID"] = Request.QueryString["CCID"].ToString();
                    }
                    if (Request.QueryString["ModId"].ToString() != "")
                    {
                        ViewState["ModuleId"] = Convert.ToInt32(Request.QueryString["ModId"]);
                    }
                    (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Customize Lists >> ";
                    (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = ViewState["ListName"].ToString();

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
                        tdStatus.Visible = false;
                        tdPriority.ColSpan = 2;
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

                                if (Convert.ToInt32(ViewState["UpdateFlag"]) == 0)
                                {
                                    btnSave.Enabled = false;
                                }
                            }
                        }


                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["Name"] = ViewState["ListName"].ToString();
                        if (Convert.ToInt32(ViewState["CategoryId"]) > 0)
                            theBuilder.DataElements["Name"] = ViewState["ListName"].ToString();
                        IQCareMsgBox.ShowConfirm("CustomUpdateRecord", theBuilder, btnSave);
                        if (ViewState["TableName"].ToString() != "HivDisease")
                        {
                            lblHeader.Text = "Edit " + ViewState["ListName"].ToString();
                            if (Convert.ToInt32(ViewState["CategoryId"]) > 0)
                                lblHeader.Text = "Edit " + ViewState["ListName"].ToString();
                        }
                        tdStatus.Visible = false;
                        btnSave.Text = "Update";
                        tdStatus.Visible = true;
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
                            ViewState["UpdateFlag"] = 1;
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

                    CustomManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList,BusinessProcess.Administration");
                    DataTable RowsAffected = CustomManager.SaveCustomMasterRecord(ViewState["TableName"].ToString(), ViewState["ListName"].ToString(), txtName.Text.Trim(), txtcode.Text, "", Convert.ToInt32(txtSeqNo.Text), Convert.ToInt32(ViewState["CategoryId"]), Convert.ToInt32(Session["AppUserId"]), Convert.ToInt32(Session["SystemId"]), Convert.ToInt32(ViewState["CCID"]), Convert.ToInt32(ViewState["ModuleId"]), "");
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
                    //string Url = string.Format("{0}?TableName={1}&CategoryId={2}&LstName={3}&Fid={4}&Upd={5}", "frmAdmin_IQCCustomList.aspx", ViewState["TableName"].ToString(), ViewState["CategoryId"].ToString(), ViewState["ListName"].ToString(), ViewState["FID"].ToString(), ViewState["Update"].ToString());
                    string Url = string.Format("{0}?TableName={1}&CategoryId={2}&LstName={3}&Fid={4}&Upd={5}&ModId={6}", "frmAdmin_IQCCustomList.aspx", ViewState["TableName"].ToString(), ViewState["CategoryId"].ToString(), ViewState["ListName"].ToString(), ViewState["FID"].ToString(), ViewState["Update"].ToString(), ViewState["ModuleId"].ToString());
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
                        CustomManager.UpdateCustomMasterRecord(ViewState["TableName"].ToString(), Convert.ToInt32(ViewState["Id"]), txtName.Text.Trim(), txtcode.Text.Trim(), "", Convert.ToInt32(txtSeqNo.Text), Convert.ToInt32(ViewState["CategoryId"]), Convert.ToInt32(ddStatus.SelectedValue), Convert.ToInt32(Session["AppUserId"]), Convert.ToInt32(Session["SystemId"]), Convert.ToInt32(ViewState["CCID"]), Convert.ToInt32(ViewState["ModuleId"]), "");
                    }
                    else
                    {

                        CustomManager.UpdateCustomMasterRecord(ViewState["TableName"].ToString(), Convert.ToInt32(ViewState["Id"]), txtName.Text.Trim(), txtcode.Text.Trim(), "", 0, Convert.ToInt32(ViewState["CategoryId"]), Convert.ToInt32(ddStatus.SelectedValue), Convert.ToInt32(Session["AppUserId"]), Convert.ToInt32(Session["SystemId"]), Convert.ToInt32(ViewState["CCID"]), Convert.ToInt32(ViewState["ModuleId"]), "");
                    }
                    if (ddStatus.SelectedValue == "1" & txtSeqNo.Text == "")
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
                        //Clear_Fields();
                        //string Url = string.Format("{0}?TableName={1}&CategoryId={2}&LstName={3}&Fid={4}&Upd={5}", "frmAdmin_IQCCustomList.aspx", ViewState["TableName"].ToString(), ViewState["CategoryId"].ToString(), ViewState["ListName"].ToString(), ViewState["FID"].ToString(), ViewState["Update"].ToString());
                        string Url = string.Format("{0}?TableName={1}&CategoryId={2}&LstName={3}&Fid={4}&Upd={5}&ModId={6}", "frmAdmin_IQCCustomList.aspx", ViewState["TableName"].ToString(), ViewState["CategoryId"].ToString(), ViewState["ListName"].ToString(), ViewState["FID"].ToString(), ViewState["Update"].ToString(), ViewState["ModuleId"].ToString());
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clear_Fields();
        }
        protected void btnExit_Click(object sender, EventArgs e)
        {
            //string Url = string.Format("{0}?TableName={1}&CategoryId={2}&LstName={3}&Fid={4}&Upd={5}", "frmAdmin_IQCCustomList.aspx", ViewState["TableName"].ToString(), ViewState["CategoryId"].ToString(), ViewState["ListName"].ToString(), ViewState["FID"].ToString(), ViewState["Update"].ToString());
            string Url = string.Format("{0}?TableName={1}&CategoryId={2}&LstName={3}&Fid={4}&Upd={5}&ModId={6}", "frmAdmin_IQCCustomList.aspx", ViewState["TableName"].ToString(), ViewState["CategoryId"].ToString(), ViewState["ListName"].ToString(), ViewState["FID"].ToString(), ViewState["Update"].ToString(), ViewState["ModuleId"].ToString());
            Response.Redirect(Url);

        }
    }
}
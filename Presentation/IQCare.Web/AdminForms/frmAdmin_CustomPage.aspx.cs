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
    public partial class CustomPage : System.Web.UI.Page
    {
        /////////////////////////////////////////////////////////////////////
        // Code Written By   : Sanjay Rana
        // Written Date      : 23th Aug 2006
        // Modification Date : 05th Sept 2006
        // Description       : Custom Master Page
        //
        // Code Enhanced By   : Jayanta Kr. Das

        /// <summary>
        /// Clear_s the fields.
        /// </summary>
        //

        #region "UserFunctions"

        private void Clear_Fields()
        {
            if (ViewState["TableName"].ToString() != "HivDisease")
            {
                lblName.Text = ViewState["ListName"].ToString() + " :";
              
            }
            else
            {
                lblName.Text = "OIs or AIDS Defining Illnesses :";
            }
            txtName.Text = "";
            txtSeqNo.Text = "";
            ddStatus.SelectedValue = "0";
            txtmultiplier.Text = "";
            if (Convert.ToInt32(ViewState["Id"]) != 0)
                GetData();
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        private void GetData()
        {
            ICustomList CustomManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList, BusinessProcess.Administration");
            DataTable theDT = CustomManager.GetCustomMasterDetails(ViewState["TableName"].ToString(), Convert.ToInt32(ViewState["Id"]), Convert.ToInt32(Session["SystemId"]));
            txtName.Text = theDT.Rows[0]["Name"].ToString();
            txtSeqNo.Text = theDT.Rows[0]["SRNO"].ToString();
            if (ViewState["TableName"].ToString() == "Frequency")
            {
                if (theDT.Rows[0]["multiplier"].ToString() != "0")
                {
                    txtmultiplier.Text = theDT.Rows[0]["multiplier"].ToString();
                }
                if (theDT.Rows[0]["multiplier"].ToString() != "0")
                {
                    rdinteger.Checked = true;
                }
                else
                    rdnomultiplier.Checked = true;
            }
            if (theDT.Rows[0]["DeleteFlag"].ToString() != "")
            {
                if (Convert.ToBoolean(Convert.ToInt32(theDT.Rows[0]["DeleteFlag"])) == true)
                {
                    ddStatus.SelectedValue = "1";
                }
            }
        }

        /// <summary>
        /// Fields the validation.
        /// </summary>
        /// <returns></returns>
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

            if (txtSeqNo.Text == "0")
            {
                IQCareMsgBox.Show("ChkPriority", this);
                return false;
            }
            if (ViewState["TableName"].ToString() == "Frequency")
            {
                if (rdinteger.Checked)
                {
                    if (txtmultiplier.Text == "")
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["Control"] = "Integer";
                        IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                        txtmultiplier.Focus();
                        return false;
                    }
                }
            }

           

            //---- Rupesh----
            if ((lblName.Text.ToString().Trim() == "Laboratory Unit :") && (ddStatus.SelectedValue == "1"))
            {
                //----check if default value of this Lab Unit is 1 ----

                ILabMst LabManager;
                LabManager = (ILabMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BLabMst, BusinessProcess.Administration");
                DataTable theDT = LabManager.CheckDefaultUnit(Convert.ToInt32(ViewState["Id"]));
                if (theDT.Rows.Count > 0)
                {
                    if (theDT.Rows[0]["unitID"].ToString() == ViewState["Id"].ToString())
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["Control"] = theDT.Rows[0]["SubTestName"].ToString();
                        IQCareMsgBox.Show("CannotInactivateUnit", theBuilder, this);
                        ddStatus.Focus();
                        return false;
                    }
                }
            }

            return true;
        }

        #endregion "UserFunctions"

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //txtSeqNo.Attributes.Add("onkeypress", "chkNumeric('"+ txtSeqNo.ClientID + "')");
            if (Page.IsPostBack != true)
            {
                try
                {
                    txtSeqNo.Attributes.Add("onkeyup", "chkPostiveInteger('" + txtSeqNo.ClientID + "')");
                    txtmultiplier.Attributes.Add("onkeyup", "chkPostiveInteger('" + txtmultiplier.ClientID + "')");
                    ViewState["Id"] = Convert.ToInt32(Request.QueryString["SelectedId"]);
                    Session["UpdateID"] = Convert.ToInt32(Request.QueryString["SelectedId"]);
                    ViewState["TableName"] = Request.QueryString["TableName"].ToString();
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
                    if (ViewState["TableName"].ToString() == "Frequency")
                    {
                        divmultiplier.Visible = true;
                    }
                    else
                    {
                        divmultiplier.Visible = false;
                    }

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
                            if (DTupdateflag.Rows.Count > 0)
                            {
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
                        }
                        /////////////////////////////////////////////////////////////////////////////////

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

            ICustomList CustomManager;
            try
            {
                CustomManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList,BusinessProcess.Administration");
                DataTable DTupdateprior = new DataTable();
                DataSet DStheXML = new DataSet();
                DStheXML.ReadXml(MapPath("..\\XMLFiles\\customizelist.xml"));
                DataView theDV = new DataView(DStheXML.Tables["ItemName"]);

                theDV.RowFilter = "TableName='" + ViewState["TableName"] + "' and CategoryId='" + ViewState["CategoryId"] + "' and CountryID='" + ViewState["CCID"] + "'";
                DataTable theDT = theDV.ToTable();
                int SystemID;
                if (theDT.Rows.Count > 0)
                {
                    SystemID = Convert.ToInt32(theDT.Rows[0]["SystemId"]);
                }
                else { SystemID = Convert.ToInt32(Session["SystemId"]); }

                ICustomList PriorManager = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList, BusinessProcess.Administration");
                if (btnSave.Text == "Save")
                {
                    DTupdateprior = PriorManager.GetCustomListUpdatePriortorize(Convert.ToString(ViewState["TableName"]), Convert.ToInt32(ViewState["CategoryId"]), txtSeqNo.Text, SystemID);
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
                    if (ViewState["CCID"].ToString() == "")
                    {
                        ViewState["CCID"] = 0;
                    }
                    //Extracting SystemID
                    string strMultiplier = "";
                    if (rdinteger.Checked)
                    {
                        strMultiplier = txtmultiplier.Text;
                    }

                    DataTable RowsAffected = CustomManager.SaveCustomMasterRecord(ViewState["TableName"].ToString(), ViewState["ListName"].ToString(), txtName.Text.Trim(), "", "", Convert.ToInt32(txtSeqNo.Text),
                    Convert.ToInt32(ViewState["CategoryId"]), Convert.ToInt32(Session["AppUserId"]), SystemID, Convert.ToInt32(ViewState["CCID"]), Convert.ToInt32(ViewState["ModuleId"]), strMultiplier);
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
                        //if (Convert.ToInt32(ViewState["CategoryId"]) == 0)
                        //{
                        //    ViewState["CategoryId"] = RowsAffected.Rows[0][0].ToString();
                        //}
                        if (Convert.ToInt32(RowsAffected.Rows[0][0].ToString()) > 0)
                            theBuilder.DataElements["Name"] = ViewState["ListName"].ToString();
                        IQCareMsgBox.Show("CustomMasterSave", theBuilder, this);
                        Clear_Fields();
                    }
                    //string Url = string.Format("{0}?TableName={1}&CategoryId={2}&LstName={3}&Fid={4}&Upd={5}&CCID={6}", "frmAdmin_CustomList.aspx", ViewState["TableName"].ToString(), ViewState["CategoryId"], ViewState["ListName"].ToString(), ViewState["FID"].ToString(), ViewState["Update"].ToString(), ViewState["CCID"].ToString());
                    string Url = string.Format("{0}?TableName={1}&CategoryId={2}&LstName={3}&Fid={4}&Upd={5}&CCID={6}&ModId={7}", "frmAdmin_CustomList.aspx", ViewState["TableName"].ToString(), ViewState["CategoryId"], ViewState["ListName"].ToString(), ViewState["FID"].ToString(), ViewState["Update"].ToString(), ViewState["CCID"].ToString(), ViewState["ModuleId"].ToString());
                    Response.Redirect(Url);
                }
                else
                {
                    string strName;
                    strName = txtName.Text.Trim();
                    txtName.Text = strName.Replace("'", "''");
                    string strMultiplier = "";
                    if (rdinteger.Checked)
                    {
                        strMultiplier = txtmultiplier.Text;
                    }
                    DTupdateprior = PriorManager.GetCustomListUpdatePriortorize(Convert.ToString(ViewState["TableName"]), Convert.ToInt32(ViewState["CategoryId"]), txtSeqNo.Text, Convert.ToInt32(Session["SystemId"].ToString()));
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
                    if (ViewState["CCID"].ToString() == "")
                    {
                        ViewState["CCID"] = 0;
                    }

                    if (txtSeqNo.Text != "")
                    {
                        CustomManager.UpdateCustomMasterRecord(Convert.ToString(ViewState["TableName"]), Convert.ToInt32(ViewState["Id"]), txtName.Text.Trim(), "", "", Convert.ToInt32(txtSeqNo.Text), Convert.ToInt32(ViewState["CategoryId"]), Convert.ToInt32(ddStatus.SelectedValue), Convert.ToInt32(Session["AppUserId"]), SystemID, Convert.ToInt32(ViewState["CCID"]), Convert.ToInt32(ViewState["ModuleId"]), strMultiplier);
                    }
                    else
                    {
                        CustomManager.UpdateCustomMasterRecord(Convert.ToString(ViewState["TableName"]), Convert.ToInt32(ViewState["Id"]), txtName.Text.Trim(), "", "", 0, Convert.ToInt32(ViewState["CategoryId"]), Convert.ToInt32(ddStatus.SelectedValue), Convert.ToInt32(Session["AppUserId"]), SystemID, Convert.ToInt32(ViewState["CCID"]), Convert.ToInt32(ViewState["ModuleId"]), strMultiplier);
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
                        Clear_Fields();

                        string Url = string.Format("{0}?TableName={1}&CategoryId={2}&LstName={3}&Fid={4}&Upd={5}&CCID={6}&ModId={7}", "frmAdmin_CustomList.aspx", ViewState["TableName"].ToString(), ViewState["CategoryId"].ToString(), ViewState["ListName"].ToString(), ViewState["FID"].ToString(), ViewState["Update"].ToString(), ViewState["CCID"].ToString(), ViewState["ModuleId"].ToString());
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

        /// <summary>
        /// Handles the Click event of the btnExit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnExit_Click(object sender, EventArgs e)
        {
            //string Url = string.Format("{0}?TableName={1}&CategoryId={2}&LstName={3}&Fid={4}&Upd={5}&CCID={6}", "frmAdmin_CustomList.aspx", ViewState["TableName"].ToString(), ViewState["CategoryId"], ViewState["ListName"].ToString(), ViewState["FID"].ToString(), ViewState["Update"].ToString(), ViewState["CCID"].ToString());
            string Url = string.Format("{0}?TableName={1}&CategoryId={2}&LstName={3}&Fid={4}&Upd={5}&CCID={6}&ModId={7}", "frmAdmin_CustomList.aspx", ViewState["TableName"].ToString(), ViewState["CategoryId"], ViewState["ListName"].ToString(), ViewState["FID"].ToString(), ViewState["Update"].ToString(), ViewState["CCID"].ToString(), ViewState["ModuleId"].ToString());
            Response.Redirect(Url);
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clear_Fields();
        }
    }
}
using System;
using System.Data;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;

namespace IQCare.Web.Admin
{
    public partial class LPFTPatientTransfer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Customize Lists >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "LPTF Patient Transfer";

            lblH2.Text = Request.QueryString["name"];

            if (lblH2.Text == "Add")
            {
                lblactive.Visible = false;
                ddStatus.Visible = false;
                lblH2.Text = "Add LPTF Patient Transfer";
            }
            else if (lblH2.Text == "Edit")
            {
                lblH2.Text = "Edit LPTF Patient Transfer";
                btnsave.Text = "Update";
            }
            ICustomList LPTFPatientTransfer;
            try
            {
                LPTFPatientTransfer = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList, BusinessProcess.Administration");
                if (IsPostBack != true)
                {
                    if (Request.QueryString["name"] == "Edit")
                    {
                        int LPTFId = Convert.ToInt32(Request.QueryString["LPTFId"]);
                        DataSet theDS = LPTFPatientTransfer.GetLPTFPatientTransferID(LPTFId);

                        this.txtLPTFName.Text = theDS.Tables[0].Rows[0]["Name"].ToString();
                        ViewState["Name"] = theDS.Tables[0].Rows[0]["Name"].ToString();
                        this.ddAnswer.SelectedValue = theDS.Tables[0].Rows[0]["ARFunded"].ToString();
                        if (theDS.Tables[0].Rows[0]["DeleteFlag"].ToString() == "1")
                        {
                            this.ddStatus.SelectedValue = "1";
                        }
                        else
                        {
                            this.ddStatus.SelectedValue = "0";
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
                LPTFPatientTransfer = null;
            }
        }

        private Boolean FieldValidation()
        {
            MsgBuilder theBuilder = new MsgBuilder();
            if (txtLPTFName.Text.Trim() == "")
            {
                theBuilder.DataElements["Control"] = "LPTF Name";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtLPTFName.Focus();
                return false;
            }
            if (ddAnswer.SelectedIndex == 9)
            {
                theBuilder.DataElements["Control"] = "Answer";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                ddAnswer.Focus();
                return false;
            }
            if (ddStatus.SelectedIndex == 9)
            {
                theBuilder.DataElements["Control"] = "Status";
                IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
                ddStatus.Focus();
                return false;
            }

            return true;
        }

        private void clear_fields()
        {
            //Clear all form fields
            this.txtLPTFName.Text = "";
            ddAnswer.ClearSelection();
            ddStatus.ClearSelection();
            this.txtLPTFName.Focus();
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (FieldValidation() == false)
            {
                return;
            }
            ICustomList LPTFMgr;
            String LPTFId = "";
            DataTable theResultDT = new DataTable();
            try
            {
                LPTFMgr = (ICustomList)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomList, BusinessProcess.Administration");
                String theUserID = Session["AppUserId"].ToString();
                if (Request.QueryString["name"] == "Add")
                {
                    String FlagSave = "0";
                    theResultDT = (DataTable)LPTFMgr.SaveLPTF(LPTFId, txtLPTFName.Text, ddAnswer.SelectedValue, ddStatus.SelectedValue, theUserID, Session["SystemId"].ToString(), FlagSave);
                    if (theResultDT.Rows[0][0].ToString() == "0")
                    {
                        IQCareMsgBox.Show("LPTFPTransferExists", this);
                        return;
                    }
                }
                else if (Request.QueryString["name"] == "Edit")
                {
                    LPTFId = Request.QueryString["LPTFId"];
                    if (ViewState["Name"].ToString() == txtLPTFName.Text)
                    {
                        String FlagUpdate = "1";
                        theResultDT = (DataTable)LPTFMgr.SaveLPTF(LPTFId, txtLPTFName.Text, ddAnswer.SelectedValue, ddStatus.SelectedValue, theUserID, Session["SystemId"].ToString(), FlagUpdate);
                    }
                    else
                    {
                        String FlagUpdate = "2";
                        theResultDT = (DataTable)LPTFMgr.SaveLPTF(LPTFId, txtLPTFName.Text, ddAnswer.SelectedValue, ddStatus.SelectedValue, theUserID, Session["SystemId"].ToString(), FlagUpdate);
                        if (theResultDT.Rows[0][0].ToString() == "0")
                        {
                            IQCareMsgBox.Show("UpdateLPTFPTransfer", this);
                            return;
                        }
                    }
                }
                string url = "frmAdmin_LPTFPatientTransferList.aspx";
                Response.Redirect(url);
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
                LPTFMgr = null;
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            string url = "frmAdmin_LPTFPatientTransferList.aspx";
            Response.Redirect(url);
        }

        protected void btnreset_Click(object sender, EventArgs e)
        {
            clear_fields();
        }
    }
}
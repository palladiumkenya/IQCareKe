using System;
using System.Data;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;
/////////////////////////////////////////////////////////////////////
// Code Written By   : Pankaj Kumar
// Written Date      : 25th July 2006
// Modification Date : 
// Description       : Reason Add/Edit/Delete
//
/// /////////////////////////////////////////////////////////////////
namespace IQCare.Web.Admin
{
    public partial class Reason : System.Web.UI.Page
    {
        int ReasonId;
        #region "User functions"
        protected void FillDropDowns()
        {
            IReason ReasonManager;
            try
            {

                ReasonManager = (IReason)ObjectFactory.CreateInstance("BusinessProcess.Administration.BReason, BusinessProcess.Administration");
                DataSet theDS = ReasonManager.GetReasonCategory();
                BindFunctions BindManager = new BindFunctions();
                BindManager.BindCombo(ddCategory, theDS.Tables[0], "CategoryName", "CategoryID");

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
                ReasonManager = null;
            }
        }
        private void clear_fields()
        {
            txtReasonName.Text = "";
            txtSRNo.Text = "";
            ddCategory.ClearSelection();
            txtReasonName.Focus();
        }
        private Boolean FieldValidation()
        {
            if (txtReasonName.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Reason Name";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtReasonName.Focus();
                return false;
            }
            if (ddCategory.SelectedValue == "0")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Resaon Category";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                ddCategory.Focus();
                return false;
            }

            if (txtSRNo.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "SR No";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtSRNo.Focus();
                return false;
            }
            return true;
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            //(Master.FindControl("lblheader") as Label).Text = "Customise List";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Customize List";
            lblH2.Text = Request.QueryString["name"];

            if (lblH2.Text == "Add")
            {
                ddStatus.Visible = false;
                lblStatus.Visible = false;
                lblH2.Text = "Add Reason";

            }


            else if (lblH2.Text == "Edit")
            {
                lblH2.Text = "Edit Reason";
            }

            IReason ReasonManager;
            try
            {

                if (!IsPostBack)
                {
                    FillDropDowns();

                    if (Request.QueryString["name"] == "Edit")
                    {
                        ReasonId = Convert.ToInt32(Request.QueryString["ReasonId"]);

                        ReasonManager = (IReason)ObjectFactory.CreateInstance("BusinessProcess.Administration.BReason, BusinessProcess.Administration");
                        DataSet theDS = ReasonManager.GetReasonByID(ReasonId);
                        this.txtReasonName.Text = theDS.Tables[0].Rows[0]["ReasonName"].ToString();
                        this.txtSRNo.Text = theDS.Tables[0].Rows[0]["SRNo"].ToString();
                        this.ddCategory.SelectedValue = theDS.Tables[0].Rows[0]["CategoryID"].ToString();
                        if (theDS.Tables[0].Rows[0]["DeleteFlag"].ToString() == "True")
                        {
                            this.ddStatus.SelectedValue = "1";
                        }
                        else
                        {
                            this.ddStatus.SelectedValue = "0";
                        }
                        this.txtSRNo.Text = theDS.Tables[0].Rows[0]["SRNo"].ToString();


                    }
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", this);
                return;
            }
            finally
            {
                ReasonManager = null;
            }

        }



        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (FieldValidation() == false)
            {
                return;
            }
            IReason ReasonManager;
            try
            {
                if (Request.QueryString["name"] == "Add")
                {

                    ReasonManager = (IReason)ObjectFactory.CreateInstance("BusinessProcess.Administration.BReason, BusinessProcess.Administration");
                    int ReasonId = ReasonManager.SaveNewReason(txtReasonName.Text, Convert.ToInt32(ddCategory.SelectedValue), Convert.ToInt32(txtSRNo.Text), 1);
                    if (ReasonId == 0)
                    {
                        IQCareMsgBox.Show("ReasonExists", this);
                        return;
                    }
                    else
                    {
                        IQCareMsgBox.Show("ReasonSave", this);
                        clear_fields();
                    }
                }
                else
                {
                    int ReasonId;
                    ReasonId = Convert.ToInt32(Request.QueryString["ReasonId"]);

                    ReasonManager = (IReason)ObjectFactory.CreateInstance("BusinessProcess.Administration.BReason, BusinessProcess.Administration");
                    ReasonId = ReasonManager.UpdateReason(ReasonId, (txtReasonName.Text.ToUpper()), Convert.ToInt32(ddCategory.SelectedValue), Convert.ToInt32(txtSRNo.Text), 1, Convert.ToInt32(this.ddStatus.SelectedValue));

                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", this);
                return;
            }
            finally
            {
                ReasonManager = null;
            }

        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmAdmin_ReasonList.aspx");
        }
    }

}
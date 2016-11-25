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
// Description       : Designation Add/Edit/Delete
//

namespace IQCare.Web.Admin
{
    public partial class Designation : System.Web.UI.Page
    {
        private int DesignationId;

        protected void Page_Load(object sender, EventArgs e)
        {
            //        (Master.FindControl("lblheader") as Label).Text = "Customise List";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Customise List";
            lblH2.Text = Request.QueryString["name"];

            if (lblH2.Text == "Add")
            {
                ddStatus.Visible = false;
                lblStatus.Visible = false;
                lblH2.Text = "Add Designation";
            }
            else if (lblH2.Text == "Edit")
            {
                lblH2.Text = "Edit Designation";
            }

            IDesignation DesignationManager;
            if (!IsPostBack)
            {
                try
                {
                    if (Request.QueryString["name"] == "Edit")
                    {
                        DesignationId = Convert.ToInt32(Request.QueryString["DesignationId"]);

                        DesignationManager = (IDesignation)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDesignation, BusinessProcess.Administration");
                        DataSet theDS = DesignationManager.GetDesignationByID(DesignationId);
                        this.txtDesignationName.Text = theDS.Tables[0].Rows[0]["Designation_Name"].ToString();
                        if (theDS.Tables[0].Rows[0]["Status"].ToString() == "Active")
                        {
                            this.ddStatus.SelectedValue = "0";
                        }
                        else
                        {
                            this.ddStatus.SelectedValue = "1";
                        }
                        this.txtSeq.Text = theDS.Tables[0].Rows[0]["Sequence"].ToString();
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
                    DesignationManager = null;
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string url = "frmAdmin_Designationlist.aspx";
            Response.Redirect(url);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (FieldValidation() == false)
            {
                return;
            }
            IDesignation DesignationManager;

            try
            {
                if (Request.QueryString["name"] == "Add")
                {
                    DesignationManager = (IDesignation)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDesignation, BusinessProcess.Administration");
                    int DesignationId = DesignationManager.SaveNewDesignation(txtDesignationName.Text, 1, Convert.ToInt32(txtSeq.Text));
                    if (DesignationId == 0)
                    {
                        IQCareMsgBox.Show("DesignationExists", this);
                        return;
                    }
                    else
                    {
                        IQCareMsgBox.Show("DesignationSave", this);
                        clear_fields();
                    }
                }
                else if (Request.QueryString["name"] == "Edit")
                {
                    DesignationManager = (IDesignation)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDesignation, BusinessProcess.Administration");
                    int DesignationId = DesignationManager.UpdateDesignation(Convert.ToInt32(Request.QueryString["designationid"]), txtDesignationName.Text, 1, Convert.ToInt32(this.ddStatus.SelectedValue), Convert.ToInt32(txtSeq.Text));
                    IQCareMsgBox.Show("DesignationUpdate", this);
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
                DesignationManager = null;
            }
        }

        private void clear_fields()
        {
            txtDesignationName.Text = "";
            txtSeq.Text = "";
            txtDesignationName.Focus();
            ddStatus.ClearSelection();
        }

        #region "User functions"

        private Boolean FieldValidation()
        {
            if (txtDesignationName.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Designation Name";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtDesignationName.Focus();
                return false;
            }
            if (txtSeq.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Sequence No";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtSeq.Focus();
                return false;
            }
            return true;
        }

        #endregion "User functions"
    }
}
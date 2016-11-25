using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;

namespace IQCare.Web.Admin
{
    public partial class Occupation : System.Web.UI.Page
    {
        /////////////////////////////////////////////////////////////////////
        // Code Written By   : Pankaj Kumar
        // Written Date      : 25th July 2006
        // Modification Date :
        // Description       : Occupation
        //
        /// /////////////////////////////////////////////////////////////////

        private int OccupationId;

        protected void Page_Load(object sender, EventArgs e)
        {
            //(Master.FindControl("lblheader") as Label).Text = "Customise List";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Customise List";
            if (Page.IsPostBack != true)
            {
                lblH2.Text = Request.QueryString["name"];

                if (lblH2.Text == "Add")
                {
                    ddStatus.Visible = false;
                    lblStatus.Visible = false;
                    lblH2.Text = "Add Occupation";
                }
                else if (lblH2.Text == "Edit")
                {
                    lblH2.Text = "Edit Occupation";
                }

                IOccupation OccupationManager;
                try
                {
                    if (Request.QueryString["name"] == "Edit")
                    {
                        OccupationId = Convert.ToInt32(Request.QueryString["occupationId"]);

                        OccupationManager = (IOccupation)ObjectFactory.CreateInstance("BusinessProcess.Administration.BOccupation, BusinessProcess.Administration");
                        DataSet theDS = OccupationManager.GetOccupationByID(OccupationId);
                        this.txtOccupationName.Text = theDS.Tables[0].Rows[0]["OccupationName"].ToString();
                        if (theDS.Tables[0].Rows[0]["DeleteFlag"].ToString() == "True")
                        {
                            this.ddStatus.SelectedValue = "1";
                        }
                        else
                        {
                            this.ddStatus.SelectedValue = "0";
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
                    OccupationManager = null;
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string url = "frmAdmin_Occupationlist.aspx";
            Response.Redirect(url);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (FieldValidation() == false)
            {
                return;
            }
            IOccupation OccupationManager;

            try
            {
                if (Request.QueryString["name"] == "Add")
                {
                    OccupationManager = (IOccupation)ObjectFactory.CreateInstance("BusinessProcess.Administration.BOccupation, BusinessProcess.Administration");
                    int OccupationId = OccupationManager.SaveNewOccupation(txtOccupationName.Text, 1, Convert.ToInt32(this.txtSeq.Text));
                    if (OccupationId == 0)
                    {
                        IQCareMsgBox.Show("OccupationExists", this);
                        return;
                    }
                    else
                    {
                        IQCareMsgBox.Show("OccupationSave", this);
                        clear_fields();
                    }
                }
                else if (Request.QueryString["name"] == "Edit")
                {
                    OccupationManager = (IOccupation)ObjectFactory.CreateInstance("BusinessProcess.Administration.BOccupation, BusinessProcess.Administration");
                    int OccupationId = OccupationManager.UpdateOccupation(Convert.ToInt32(Request.QueryString["occupationid"]), txtOccupationName.Text.ToUpper(), 1, Convert.ToInt32(this.ddStatus.SelectedValue), Convert.ToInt32(this.txtSeq.Text));
                    IQCareMsgBox.Show("OccupationUpdate", this);
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
                OccupationManager = null;
            }
        }

        #region "User Functions"

        private void clear_fields()
        {
            txtSeq.Text = "";
            txtOccupationName.Text = "";
            txtOccupationName.Focus();
            ddStatus.ClearSelection();
        }

        private Boolean FieldValidation()
        {
            if (txtOccupationName.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Occupation Name";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtOccupationName.Focus();
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

        #endregion "User Functions"
    }
}
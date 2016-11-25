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
// Description       : Disease Add/Edit/Delete
//
/// /////////////////////////////////////////////////////////////////
namespace IQCare.Web.Admin
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ManageDisease : System.Web.UI.Page
    {
        /// <summary>
        /// The disease identifier
        /// </summary>
        int DiseaseId=0;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {

            //(Master.FindControl("lblheader") as Label).Text = "Customise List";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Customise List";
            lblH2.Text = Request.QueryString["name"];

            if (lblH2.Text == "Add")
            {
                ddStatus.Visible = false;
                lblStatus.Visible = false;
                lblH2.Text = "Add Disease";
            }


            else if (lblH2.Text == "Edit")
            {
                lblH2.Text = "Edit Disease";
            }

            IDiseases DiseaseManager;
            if (!IsPostBack)
            {
                if (Request.QueryString["name"] == "Edit")
                {

                    try
                    {

                        int DiseaseId;
                        DiseaseId = Convert.ToInt32(Request.QueryString["DiseaseId"]);

                        DiseaseManager = (IDiseases)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDiseases, BusinessProcess.Administration");
                        DataSet theDS = DiseaseManager.GetDiseasesByID(DiseaseId);
                        this.txtDiseaseName.Text = theDS.Tables[0].Rows[0]["DiseaseName"].ToString();
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
                    catch (Exception err)
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["MessageText"] = err.Message.ToString();
                        IQCareMsgBox.Show("#C1", theBuilder, this);
                        return;
                    }
                    finally
                    {
                        DiseaseManager = null;
                    }

                }
            }
        }
        /// <summary>
        /// Handles the Click event of the btnDelete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string url;
            IDiseases DiseaseManager;
            try
            {
                DiseaseManager = (IDiseases)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDiseases, BusinessProcess.Administration");
                DataSet theDS = DiseaseManager.DeleteDisease(Convert.ToInt32(DiseaseId.ToString()));
                url = "frmAdmin_Diseaselist.aspx";
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
                DiseaseManager = null;
            }
        }
        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string url = "frmAdmin_Diseaselist.aspx";
            Response.Redirect(url);
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
            IDiseases DiseaseManager;

            try
            {

                if (Request.QueryString["name"] == "Add")
                {

                    DiseaseManager = (IDiseases)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDiseases, BusinessProcess.Administration");
                    int DiseaseId = DiseaseManager.SaveNewDisease(txtDiseaseName.Text, 1, Convert.ToInt32(txtSeq.Text));
                    if (DiseaseId == 0)
                    {
                        IQCareMsgBox.Show("DiseaseExists", this);
                        return;
                    }
                    else
                    {
                        IQCareMsgBox.Show("DiseaseSave", this);
                        clear_fields();
                    }
                }
                else if (Request.QueryString["name"] == "Edit")
                {
                    DiseaseManager = (IDiseases)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDiseases, BusinessProcess.Administration");
                    int DiseaseId = DiseaseManager.UpdateDisease(Convert.ToInt32(Request.QueryString["diseaseid"]), txtDiseaseName.Text.ToUpper(), 1, Convert.ToInt32(this.ddStatus.SelectedValue), Convert.ToInt32(txtSeq.Text));
                    IQCareMsgBox.Show("DiseaseUpdate", this);

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
                DiseaseManager = null;
            }

        }

        /// <summary>
        /// Clear_fieldses this instance.
        /// </summary>
        private void clear_fields()
        {
            txtDiseaseName.Text = "";
            txtSeq.Text = "";
            txtDiseaseName.Focus();
            ddStatus.ClearSelection();

        }

        /// <summary>
        /// Fields the validation.
        /// </summary>
        /// <returns></returns>
        private Boolean FieldValidation()
        {
            if (txtDiseaseName.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Disease Name";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtDiseaseName.Focus();
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
       
    }
}
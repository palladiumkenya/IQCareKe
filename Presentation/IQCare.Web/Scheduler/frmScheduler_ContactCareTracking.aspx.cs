using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Scheduler;

namespace IQCare.Web.Scheduler
{
    public partial class ContactCareTracking : BasePage
    {
        #region "Variable declaration section"
        private string EmpId = "0";
        private string patexitval = "0";
        private int printval = 0;
        private Panel theCheckBoxList = new Panel();
        private bool theConditional;
        private DataSet theDSXML = new DataSet();
        private DataView theDVReq = new DataView();
        //Panel PnlMulti = new Panel();
        private int VisitID = 0;
        # endregion

        public void HtmlCheckBoxSelect(object theObj)
        {
            CheckBox theButton = ((CheckBox)theObj);
            string[] theControlId = theButton.ID.ToString().Split('-');
            DataSet theDS = (DataSet)Session["CareEndFields"];

            //DataSet theDS = (DataSet)Session["AllData"];
            int theValue = 0;
            if (theButton.Checked == true)
                theValue = 1;
            else
                theValue = 0;

            foreach (DataRow theDR in theDS.Tables[6].Rows)
            {
                foreach (Control x in DIVCustomItem.Controls)
                {
                    if (x.ID != null)
                    {
                        string[] theIdent = x.ID.Split('-');
                        if (x.GetType().ToString() == "System.Web.UI.WebControls.TextBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[5].ToString() && theValue.ToString() == "1")
                                ((TextBox)x).Enabled = true;
                            else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[5].ToString() && theValue.ToString() == "0")
                            {
                                ((TextBox)x).Enabled = false;
                                ((TextBox)x).Text = "";
                            }
                        }

                        if (x.GetType().ToString() == "System.Web.UI.WebControls.DropDownList" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[5].ToString() && theValue.ToString() == "1")
                                ((DropDownList)x).Enabled = true;
                            else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[5].ToString() && theValue.ToString() == "0")
                            {
                                ((DropDownList)x).Enabled = false;
                                ((DropDownList)x).SelectedValue = "0";
                            }
                        }

                        if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel" && theIdent[0] == "Pnl")
                        {
                            if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[5].ToString() && theValue.ToString() == "1" && theDR["FieldName"].ToString() == theIdent[1].ToString())
                                ((Panel)x).Enabled = true;
                            else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[5].ToString() && theValue.ToString() == "0" && theDR["FieldName"].ToString() == theIdent[1].ToString())
                            {
                                ((Panel)x).Enabled = false;
                            }
                        }

                        if (x.GetType().ToString() == "System.Web.UI.WebControls.Image" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[5].ToString() && theValue.ToString() == "1")
                                ((Image)x).Visible = true;
                            else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[5].ToString() && theValue.ToString() == "0")
                                ((Image)x).Visible = false;
                        }

                        if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputRadioButton" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[5].ToString() && theValue.ToString() == "1")
                                ((HtmlInputRadioButton)x).Visible = true;
                            else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[5].ToString() && theValue.ToString() == "0")
                                ((HtmlInputRadioButton)x).Visible = false;
                        }

                        if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputCheckBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[5].ToString() && theValue.ToString() == "1")
                                ((HtmlInputCheckBox)x).Visible = true;
                            else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[5].ToString() && theValue.ToString() == "0")
                                ((HtmlInputCheckBox)x).Visible = false;
                        }
                    }
                }

                /////////////////////Child Panel/////////////////

                foreach (Control x in PnlConFields.Controls)
                {
                    if (x.ID != null)
                    {
                        string[] theIdent = x.ID.Split('-');
                        if (x.GetType().ToString() == "System.Web.UI.WebControls.TextBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[5].ToString() && theValue.ToString() == "1")
                                ((TextBox)x).Enabled = true;
                            else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[5].ToString() && theValue.ToString() == "0")
                            {
                                ((TextBox)x).Enabled = false;
                                ((TextBox)x).Text = "";
                            }
                        }

                        if (x.GetType().ToString() == "System.Web.UI.WebControls.DropDownList" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[5].ToString() && theValue.ToString() == "1")
                                ((DropDownList)x).Enabled = true;
                            else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[5].ToString() && theValue.ToString() == "0")
                            {
                                ((DropDownList)x).Enabled = false;
                                ((DropDownList)x).SelectedValue = "0";
                            }
                        }

                        if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel" && theIdent[0] == "Pnl")
                        {
                            if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[5].ToString() && theValue.ToString() == "1")
                                ((Panel)x).Enabled = true;
                            else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[5].ToString() && theValue.ToString() == "0")
                            {
                                ((Panel)x).Enabled = false;
                            }
                        }

                        if (x.GetType().ToString() == "System.Web.UI.WebControls.Image" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[5].ToString() && theValue.ToString() == "1")
                                ((Image)x).Visible = true;
                            else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[5].ToString() && theValue.ToString() == "0")
                                ((Image)x).Visible = false;
                        }

                        if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputRadioButton" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[5].ToString() && theValue.ToString() == "1")
                                ((HtmlInputRadioButton)x).Visible = true;
                            else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[5].ToString() && theValue.ToString() == "0")
                                ((HtmlInputRadioButton)x).Visible = false;
                        }

                        if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputCheckBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[5].ToString() && theValue.ToString() == "1")
                                ((HtmlInputCheckBox)x).Visible = true;
                            else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[5].ToString() && theValue.ToString() == "0")
                                ((HtmlInputCheckBox)x).Visible = false;
                        }
                    }
                }

                ////////////////////////////////////////////////
            }
        }

        public void HtmlRadioButtonSelect(object sender)
        {
            HtmlInputRadioButton theButton = ((HtmlInputRadioButton)sender);
            string[] theControlId = theButton.ID.Split('-');
            DataSet theDS = (DataSet)Session["CareEndFields"];
            int theValue = 0;
            if (theButton.Value == "Yes" && theButton.Checked == true)
                theValue = 1;
            else if (theButton.Value == "Yes" && theButton.Checked == false)
                theValue = 0;

            if (theButton.Value == "No" && theButton.Checked == true)
                theValue = 2;
            else if (theButton.Value == "No" && theButton.Checked == false)
                theValue = 0;

            foreach (DataRow theDR in theDS.Tables[6].Rows)
            {
                foreach (Control x in DIVCustomItem.Controls)
                {
                    if (x.ID != null)
                    {
                        string[] theIdent = x.ID.Split('-');
                        if (x.GetType().ToString() == "System.Web.UI.WebControls.TextBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString() && theDR["FieldName"].ToString() == theIdent[1].ToString())
                                ((TextBox)x).Enabled = true;
                            else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString() && theDR["FieldName"].ToString() == theIdent[1].ToString())
                            {
                                ((TextBox)x).Enabled = false;
                                ((TextBox)x).Text = "";
                            }
                        }

                        if (x.GetType().ToString() == "System.Web.UI.WebControls.DropDownList" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString() && theDR["FieldName"].ToString() == theIdent[1].ToString())
                                ((DropDownList)x).Enabled = true;
                            else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString() && theDR["FieldName"].ToString() == theIdent[1].ToString())
                            {
                                ((DropDownList)x).Enabled = false;
                                ((DropDownList)x).SelectedValue = "0";
                            }
                        }

                        if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel" && theIdent[0] == "Pnl")
                        {
                            if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString() && theDR["FieldName"].ToString() == theIdent[1].ToString())
                                ((Panel)x).Enabled = true;
                            else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString() && theDR["FieldName"].ToString() == theIdent[1].ToString())
                            {
                                ((Panel)x).Enabled = false;
                            }
                        }

                        if (x.GetType().ToString() == "System.Web.UI.WebControls.Image" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString() && theDR["FieldName"].ToString() == theIdent[1].ToString())
                                ((Image)x).Visible = true;
                            else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString() && theDR["FieldName"].ToString() == theIdent[1].ToString())
                                ((Image)x).Visible = false;
                        }

                        if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputRadioButton" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString() && theDR["FieldName"].ToString() == theIdent[1].ToString())
                                ((HtmlInputRadioButton)x).Visible = true;
                            else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString() && theDR["FieldName"].ToString() == theIdent[1].ToString())
                                ((HtmlInputRadioButton)x).Visible = false;
                        }

                        if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputCheckBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString() && theDR["FieldName"].ToString() == theIdent[1].ToString())
                            {
                                ((HtmlInputCheckBox)x).Visible = true;
                                ((HtmlInputCheckBox)x).Disabled = false;
                            }
                            else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString() && theDR["FieldName"].ToString() == theIdent[1].ToString())
                            {
                                //((HtmlInputCheckBox)x).Visible = false;
                                ((HtmlInputCheckBox)x).Visible = true;
                                ((HtmlInputCheckBox)x).Disabled = true;
                            }
                        }
                    }
                }

                /////////////child panal////////////////////////////////

                foreach (Control x in PnlConFields.Controls)
                {
                    if (x.ID != null)
                    {
                        string[] theIdent = x.ID.Split('-');
                        if (x.GetType().ToString() == "System.Web.UI.WebControls.TextBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString() && theDR["FieldName"].ToString() == theIdent[1].ToString())
                                ((TextBox)x).Enabled = true;
                            else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString() && theDR["FieldName"].ToString() == theIdent[1].ToString())
                            {
                                ((TextBox)x).Enabled = false;
                                ((TextBox)x).Text = "";
                            }
                        }

                        if (x.GetType().ToString() == "System.Web.UI.WebControls.DropDownList" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString() && theDR["FieldName"].ToString() == theIdent[1].ToString())
                                ((DropDownList)x).Enabled = true;
                            else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString() && theDR["FieldName"].ToString() == theIdent[1].ToString())
                            {
                                ((DropDownList)x).Enabled = false;
                                ((DropDownList)x).SelectedValue = "0";
                            }
                        }

                        if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel" && theIdent[0] == "Pnl")
                        {
                            if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString() && theDR["FieldName"].ToString() == theIdent[1].ToString())
                                ((Panel)x).Enabled = true;
                            else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString() && theDR["FieldName"].ToString() == theIdent[1].ToString())
                            {
                                ((Panel)x).Enabled = false;
                            }
                        }

                        if (x.GetType().ToString() == "System.Web.UI.WebControls.Image" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString() && theDR["FieldName"].ToString() == theIdent[1].ToString())
                                ((Image)x).Visible = true;
                            else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString() && theDR["FieldName"].ToString() == theIdent[1].ToString())
                                ((Image)x).Visible = false;
                        }

                        if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputRadioButton" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString() && theDR["FieldName"].ToString() == theIdent[1].ToString())
                                ((HtmlInputRadioButton)x).Visible = true;
                            else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString() && theDR["FieldName"].ToString() == theIdent[1].ToString())
                                ((HtmlInputRadioButton)x).Visible = false;
                        }

                        if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputCheckBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString() && theDR["FieldName"].ToString() == theIdent[1].ToString())
                                ((HtmlInputCheckBox)x).Visible = true;
                            else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString() && theDR["FieldName"].ToString() == theIdent[1].ToString())
                                ((HtmlInputCheckBox)x).Visible = false;
                        }
                    }
                }

                /////////////////////////////////////////////////////////
            }
        }

        protected void btnCancel_Click1(object sender, EventArgs e)
        {
            string Url = string.Format("{0}?PatientId={1}", "../ClinicalForms/frmPatient_Home.aspx", Session["PatientId"].ToString());
            Response.Redirect(Url);
        }

        protected void btnComplete_ServerClick1(object sender, EventArgs e)
        {
            if (ValidationFormData() == true)
            {
                if (SaveCustomFormData(1) > 0)
                {
                    SaveCancel();
                }
                else
                {
                    IQCareMsgBox.Show("DateCareEndedExists", this);
                    txtCareEndDate.Focus();
                }
            }
        }

        protected void btnsave_ServerClick1(object sender, EventArgs e)
        {
            if (ValidationFormData() == true)
            {
                if (SaveCustomFormData(0) > 0)
                {
                    SaveCancel();
                }
                else
                {
                    IQCareMsgBox.Show("DateCareEndedExists", this);
                    txtCareEndDate.Focus();
                }
            }
        }

        protected void cmbDeathReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["OldDeathSelectedValue"] = cmbDeathReason.SelectedValue;
            if (cmbDeathReason.SelectedValue.ToString() != "" && Convert.ToInt32(cmbDeathReason.SelectedValue) != 0)
            {
                PnlConFields.Controls.Clear();
                if (cmbPatientExitReason.SelectedValue.ToString() == "93")
                {
                    Tr_Deathreason.Visible = true;
                }
                else
                {
                    Tr_Deathreason.Visible = false;
                }

                DataSet DSDeath = (DataSet)Session["CareEndFields"];
                DataView DVDeath = new DataView(DSDeath.Tables[5]);
                DVDeath.RowFilter = "SectionId=" + cmbDeathReason.SelectedValue.ToString();
                DVDeath.Sort = "FieldId asc";
                DataTable DTDeath = new DataTable();
                DTDeath = DVDeath.ToTable();
                ////sanjay///
                DataView DVDDeathReason = new DataView(DSDeath.Tables[1]);
                DVDDeathReason.RowFilter = "SectionId=" + "93";
                DTDeath.Merge(DVDDeathReason.ToTable());
                //DTDeath.Merge(DSDeath.Tables[1]);
                /////////////
                theDVReq = new DataView(DSDeath.Tables[3].Copy());
                ViewState["cmbbox"] = DTDeath;
                if (DTDeath.Rows.Count > 0)
                {
                    PnlConFields.Visible = true;
                    cmbboxLoad(DTDeath, theDVReq, patexitval);
                }

                if (ViewState["TopControl"] != null)
                {
                    if (((DataTable)ViewState["TopControl"]).Rows.Count > 0)
                        DIVCustomItem.Visible = true;
                    LoadPredefinedLabel_Field();
                }
            }
            
        }

        protected void cmbPatientExitReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["OldSelectedValue"] = cmbPatientExitReason.SelectedValue;
            DataSet theDS = (DataSet)Session["CareEndFields"];
            PnlConFields.Controls.Clear();

            if (Convert.ToInt32(cmbPatientExitReason.SelectedValue) > 0)
            {
                if (cmbPatientExitReason.SelectedValue.ToString() == "93")
                {
                    Tr_Deathreason.Visible = true;
                    if (cmbDeathReason.SelectedValue.ToString() != "" && Convert.ToInt32(cmbDeathReason.SelectedValue) != 0)
                    {
                        cmbDeathReason_SelectedIndexChanged(this, e);
                    }
                    //return;
                }
                else
                {
                    Tr_Deathreason.Visible = false;
                    cmbDeathReason.SelectedIndex = 0;
                }
                //DataSet thedS;
                //this.txtCareEndDate.Disabled = false;
                if (cmbPatientExitReason.SelectedValue.ToString() == "91")
                {
                    DataSet thedS;
                    IContactCare CareEnddate = (IContactCare)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BContactCare,BusinessProcess.Scheduler");
                    thedS = (DataSet)CareEnddate.GetCareEndDate(Convert.ToInt32(Session["PatientId"]), Session["Program"].ToString());
                    if (thedS.Tables[0].Rows[0][0].ToString() != "")
                    {
                        //if (this.txtCareEndDate.Value.ToString() == "" || GblIQCare.strfoll == "0")
                        //{
                        this.txtCareEndDate.Value = String.Format("{0:dd-MMM-yyyy}", thedS.Tables[0].Rows[0][0]);

                        //this.txtCareEndDate.Disabled = true;
                        //intfoll = 1;
                        GblIQCare.strfoll = "1";
                        //}
                    }
                    else
                    {
                        //if (GblIQCare.strfoll.ToString() == "1")
                        //{
                        //    txtCareEndDate.Value = "";
                        //    GblIQCare.strfoll = "0";
                        //}
                        if (this.txtDateLastContact.Value != "")
                        {
                            DateTime lastcontDate = Convert.ToDateTime(this.txtDateLastContact.Value).AddDays(90);
                            //lastcontDate.AddDays(90);
                            int result = DateTime.Compare(Convert.ToDateTime(Application["AppCurrentDate"]), lastcontDate);
                            if (result > 0)
                            {
                                this.txtCareEndDate.Value = String.Format("{0:dd-MMM-yyyy}", lastcontDate);
                            }
                            else
                                this.txtCareEndDate.Value = String.Format("{0:dd-MMM-yyyy}", Application["AppCurrentDate"]);
                        }
                    }
                }
                else if ((cmbPatientExitReason.SelectedValue.ToString() == "115") || (cmbPatientExitReason.SelectedValue.ToString() == "118"))
                {
                    if (this.txtDateLastContact.Value != ""  )
                    {
                        if (this.txtCareEndDate.Value == "")
                        {
                            this.txtCareEndDate.Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(this.txtDateLastContact.Value));
                        }
                    }
                }
                else if ((cmbPatientExitReason.SelectedValue.ToString() == "92") || (cmbPatientExitReason.SelectedValue.ToString() == "114"))
                {
                    if (this.txtDateLastContact.Value != "")
                    {
                        if (this.txtCareEndDate.Value == "")
                        {
                            this.txtCareEndDate.Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(this.txtDateLastContact.Value));
                        }
                   }
                }
                else if (cmbPatientExitReason.SelectedValue.ToString() == "93")
                {
                    if (this.txtDeathDate.Value != "")
                    {
                        this.txtCareEndDate.Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(this.txtDeathDate.Value));
                    }
                }
                else
                {
                    if (GblIQCare.strfoll.ToString() == "1")
                    {
                        GblIQCare.strfoll = "0";
                    }
                    //txtCareEndDate.Value = "";
                }

                //PnlConFields.Controls.Clear();
                DataView theDV = new DataView(theDS.Tables[1].Copy());
                theDVReq = new DataView(theDS.Tables[3].Copy());
                theDV.RowFilter = "SectionId=" + cmbPatientExitReason.SelectedValue.ToString();
                theDV.Sort = "FieldId asc";
                DataTable theDT = theDV.ToTable();

                ViewState["cmbbox"] = theDT;
                if (theDT.Rows.Count > 0)
                {
                    PnlConFields.Visible = true;
                    cmbboxLoad(theDT, theDVReq, patexitval);
                }

                if (ViewState["TopControl"] != null)
                {
                    if (((DataTable)ViewState["TopControl"]).Rows.Count > 0)
                        DIVCustomItem.Visible = true;
                    LoadPredefinedLabel_Field();
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int FeatureID = 0, PatientID = 0;//, LocationID = 0;
            FeatureID = Convert.ToInt32(Session["FeatureID"]);
            PatientID = Convert.ToInt32(Session["PatientId"]);
            Tr_Deathreason.Visible = false;

            VisitID = Convert.ToInt32(Session["PatientVisitId"]);
            if (VisitID >= 1)
            {
                btnsave.Disabled = true;
                btnComplete.Disabled = true;
            }
            else
            {
                if (printval == 0)
                {
                    btnsave.Disabled = false;
                    btnComplete.Disabled = false;
                }
            }

            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Care Termination";
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Care Termination";
            Master.ExecutePatientLevel = true;
            Authentication();

            ClearHiddenfield();
            if (!IsPostBack)
            {
                Session["OldData"] = null;
                txtMissedAppDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
                txtMissedAppDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
                txtDateLastContact.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
                txtDateLastContact.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
                txtCareEndDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
                txtCareEndDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
                txtDeathDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
                PnlConFields.EnableViewState = false;
                DIVCustomItem.EnableViewState = false;

                Bind_Combo();
                LoadPredefinedLabel_Field();
                DisplayDeathreasonpnl();
                DisplaySavedFormData();
                EnableDisableControls();

                DataSet theDS;
                IContactCare CareManager = (IContactCare)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BContactCare,BusinessProcess.Scheduler");
                theDS = (DataSet)CareManager.GetFieldsforID(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["AppLocationId"]), Convert.ToInt32(Session["SystemId"]), 1, 8);
                if (this.txtDateLastContact.Value == "")
                {
                    this.txtDateLastContact.Value = ((DateTime)theDS.Tables[2].Rows[0]["Last_Ac_Con_date"]).ToString(Session["AppDateFormat"].ToString());
                }
            }
            else
            {
                LoadPredefinedLabel_Field();
                if (ViewState["OldSelectedValue"] != null && cmbPatientExitReason.SelectedValue.ToString() == ViewState["OldSelectedValue"].ToString())
                {
                    cmbPatientExitReason_SelectedIndexChanged(this, e);
                }
                if (ViewState["OldDeathSelectedValue"] != null && cmbDeathReason.SelectedValue.ToString() == ViewState["OldDeathSelectedValue"].ToString())
                {
                    cmbDeathReason_SelectedIndexChanged(this, e);
                }
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (this.IsPostBack == true)
            {
                EnableDisableControls();
                if (theHitCntrl.Value != "")
                {
                    string[] theCntrl = theHitCntrl.Value.Split('%');
                    CheckControl(DIVCustomItem, theCntrl);
                    CheckControl(PnlConFields, theCntrl);
                    theHitCntrl.Value = "";
                }
                foreach (Control x in DIVCustomItem.Controls)
                {
                    if (x.GetType().ToString() == "System.Web.UI.WebControls.DropDownList")
                    {
                        DropDownList theDList = (DropDownList)x;
                        if (theDList.AutoPostBack == true)
                        {
                            EventArgs s = new EventArgs();
                            ddlSelectList_SelectedIndexChanged(x, s);
                        }
                    }
                }
            }
        }

        private void Authentication()
        {
            AuthenticationManager Authentiaction = new AuthenticationManager();
            if (Authentiaction.HasFunctionRight(ApplicationAccess.CareTracking, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
            {
                string theUrl = string.Format("{0}", "~/ClinicalForms/frmPatient_History.aspx");
                Response.Redirect(theUrl);
            }
            else if (Authentiaction.HasFunctionRight(ApplicationAccess.CareTracking, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
            {
                btnsave.Disabled = true;
                btnComplete.Disabled = true;
            }
            else if (Authentiaction.HasFunctionRight(ApplicationAccess.CareTracking, FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
            {
                btnsave.Disabled = true;
                btnComplete.Disabled = true;
            }
            else if (Authentiaction.HasFunctionRight(ApplicationAccess.CareTracking, FunctionAccess.Print, (DataTable)Session["UserRight"]) == false)
            {
                btnPrint.Enabled = false;
            }
        }
        private void Bind_Combo()
        {
            BindFunctions theBindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            theDSXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));
            ICareEnded CEControl = (ICareEnded)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BCareEnded,BusinessProcess.Scheduler");
            DataSet theDS = CEControl.GetDynamicControl(Convert.ToInt32(Session["TechnicalAreaId"]));
            Session["CareEndFields"] = theDS;
            Session["XMLTables"] = theDSXML;
            theDVReq = new DataView(theDS.Tables[3].Copy());

            theBindManager.BindCombo(cmbPatientExitReason, theDS.Tables[2], "Name", "ID");

            DataTable theDT = new DataTable();
            DataView theDV = new DataView(theDSXML.Tables["Mst_Employee"]);
            theDV.RowFilter = "DeleteFlag=0";
            if (theDV.Table.Rows.Count != 0)
            {
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                if (Convert.ToInt32(Session["AppUserEmployeeId"]) > 0)
                {
                    theDV = new DataView(theDT);
                    theDV.RowFilter = "EmployeeId =" + Session["AppUserEmployeeId"].ToString();
                    if (theDV.Count > 0)
                        theDT = theUtils.CreateTableFromDataView(theDV);
                }
                if (theDV.Count > 0)
                {
                    //ddinterviewer.DataSource = null;
                    theBindManager.BindCombo(ddinterviewer, theDT, "EmployeeName", "EmployeeId");
                    theDV.Dispose();
                }
                //theDT.Clear();
            }
        }

        private void CheckControl(Control theCntrl, string[] theId)
        {
            string theCntrlType = theId[0];
            foreach (Control x in theCntrl.Controls)
            {
                if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel")
                    CheckControl(x, theId);
                else if (x.ID == theId[1] && x.GetType().ToString() == theCntrlType && theCntrlType == "System.Web.UI.WebControls.CheckBox")
                {
                    HtmlCheckBoxSelect(x);
                    return;
                }
                else if (x.ID == theId[1] && x.GetType().ToString() == theCntrlType && theCntrlType == "System.Web.UI.HtmlControls.HtmlInputRadioButton")
                {
                    HtmlRadioButtonSelect(x);
                    return;
                }
            }
        }

        private void ClearHiddenfield()
        {
            hidID.Value = "";
            hidcheckbox.Value = "";
            hidradio.Value = ""; ;
            hidchkbox.Value = "";
            hidIDQty.Value = "";
            hidcheckboxQty.Value = "";
            hidradioQty.Value = "";
            hidchkboxQty.Value = "";
            theHitCntrl.Value = "";
            HiddenMsgBuilderfield.Value = "";
            if ((cmbDeathReason.SelectedValue != "0") && (cmbPatientExitReason.SelectedValue != "93"))
            {
                txtCareEndDate.Value = "";
            }
        }

        private void cmbboxLoad(DataTable theDT, DataView DVReq, string patval)
        {
            DataView DVReqCurr = new DataView();
            //ClearHiddenfield();

            if (theDT.Rows.Count > 0)
            {
                DataSet theDS = (DataSet)Session["CareEndFields"];
                int i = 0;
                PnlConFields.EnableViewState = false;
                foreach (DataRow theDR in theDT.Rows)
                {

                    DataView theDVConditionalField = new DataView(theDS.Tables[6]);
                    theDVConditionalField.RowFilter = "ConFieldId=" + theDR["FieldID"].ToString() + " and ConFieldPredefined=" + theDR["Predefined"].ToString();
                    theDVConditionalField.Sort = "Seq asc";
                    if (theDVConditionalField.Count > 0)
                        theConditional = true;
                    else
                        theConditional = false;

                    DVReqCurr = DVReq;

                    //DVReqCurr.RowFilter = "FieldID=" + theDR["FieldId"].ToString() + " and Predefined =" + theDR["PreDefined"].ToString() + " ";

                    PnlConFields.Controls.Add(new LiteralControl("<td  class='form' align='center' width='50%'>"));
                    LoadFieldTypeControl(DVReqCurr, PnlConFields, theDR["FeatureName"].ToString(), theDR["SectionId"].ToString(), "", theDR["FieldId"].ToString(), theDR["FieldLabel"].ToString(),
                      theDR["FieldName"].ToString(), theDR["ControlId"].ToString(), theDR["SavingTable"].ToString(), theDR["BindingTable"].ToString(), theDR["PreDefined"].ToString(), true);
                    PnlConFields.Controls.Add(new LiteralControl("</td>"));
                    if (i == 1)
                    {
                        PnlConFields.Controls.Add(new LiteralControl("</tr>"));
                        i = 0;
                    }
                    else
                    {
                        i = i + 1;
                    }

                    if (theConditional == true)
                    {
                        for (int row = 0; row < theDVConditionalField.Count; row++)
                        {
                            DVReqCurr = DVReq;

                            DVReqCurr.RowFilter = "FieldID=" + theDR["FieldId"].ToString() + " and Predefined =" + theDR["PreDefined"].ToString() + " ";

                            PnlConFields.Controls.Add(new LiteralControl("<td  class='form' align='center' width='50%'>"));
                            LoadFieldTypeControl(DVReqCurr, PnlConFields, theDVConditionalField[row]["FeatureName"].ToString(), "", "", theDVConditionalField[row]["FieldId"].ToString(), theDVConditionalField[row]["FieldLabel"].ToString(),
                              theDVConditionalField[row]["FieldName"].ToString(), theDVConditionalField[row]["ControlId"].ToString(), theDVConditionalField[row]["PdfTableName"].ToString(), theDVConditionalField[row]["BindSource"].ToString(), theDVConditionalField[row]["PreDefined"].ToString(), false);
                            PnlConFields.Controls.Add(new LiteralControl("</td>"));
                            if (i == 1)
                            {
                                PnlConFields.Controls.Add(new LiteralControl("</tr>"));
                                i = 0;
                            }
                            else
                            {
                                i = i + 1;
                            }
                        }
                    }

                }

                if (ViewState["TopControl"] != null)
                {
                    EmpId = ddinterviewer.SelectedValue.ToString();
                    if (((DataTable)ViewState["TopControl"]).Rows.Count > 0)
                        DIVCustomItem.Visible = true;
                    LoadPredefinedLabel_Field();
                }

               
            }
            
        }

        private void ddlSelectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList theDList = ((DropDownList)sender);
            DataSet theDS = (DataSet)Session["CareEndFields"];
            //DataSet theDS = (DataSet)Session["AllData"];
            string[] theCntrl = theDList.ID.Split('-');
            foreach (DataRow theDR in theDS.Tables[6].Rows)
            {
                foreach (Control x in DIVCustomItem.Controls)
                {
                    if (x.ID != null)
                    {
                        string[] theIdent = x.ID.Split('-');
                        if (x.GetType().ToString() == "System.Web.UI.WebControls.TextBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                                ((TextBox)x).Enabled = true;
                            else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                            {
                                ((TextBox)x).Enabled = false;
                                ((TextBox)x).Text = "";
                            }
                        }

                        if (x.GetType().ToString() == "System.Web.UI.WebControls.DropDownList" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                                ((DropDownList)x).Enabled = true;
                            else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                            {
                                ((DropDownList)x).Enabled = false;
                                ((DropDownList)x).SelectedValue = "0";
                            }
                        }

                        if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel" && theIdent[0] == "Pnl")
                        {
                            if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString() && theDR["FieldName"].ToString() == theIdent[1].ToString())
                                ((Panel)x).Enabled = true;
                            else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString() && theDR["FieldName"].ToString() == theIdent[1].ToString())
                            {
                                ((Panel)x).Enabled = false;
                            }
                        }

                        if (x.GetType().ToString() == "System.Web.UI.WebControls.Image" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                                ((Image)x).Visible = true;
                            else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                                ((Image)x).Visible = false;
                        }

                        if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputRadioButton" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                                ((HtmlInputRadioButton)x).Visible = true;
                            else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                                ((HtmlInputRadioButton)x).Visible = false;
                        }

                        if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputCheckBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                                ((HtmlInputCheckBox)x).Visible = true;
                            else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                                ((HtmlInputCheckBox)x).Visible = false;
                        }
                    }
                }

                foreach (Control x in PnlConFields.Controls)
                {
                    if (x.ID != null)
                    {
                        string[] theIdent = x.ID.Split('-');
                        if (x.GetType().ToString() == "System.Web.UI.WebControls.TextBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                                ((TextBox)x).Enabled = true;
                            else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                            {
                                ((TextBox)x).Enabled = false;
                                ((TextBox)x).Text = "";
                            }
                        }

                        if (x.GetType().ToString() == "System.Web.UI.WebControls.DropDownList" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                                ((DropDownList)x).Enabled = true;
                            else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                            {
                                ((DropDownList)x).Enabled = false;
                                ((DropDownList)x).SelectedValue = "0";
                            }
                        }

                        if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel" && theIdent[0] == "Pnl")
                        {
                            if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString() && theDR["FieldName"].ToString() == theIdent[1].ToString())
                                ((Panel)x).Enabled = true;
                            else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString() && theDR["FieldName"].ToString() == theIdent[1].ToString())
                            {
                                ((Panel)x).Enabled = false;
                            }
                        }

                        if (x.GetType().ToString() == "System.Web.UI.WebControls.Image" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                                ((Image)x).Visible = true;
                            else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                                ((Image)x).Visible = false;
                        }

                        if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputRadioButton" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                                ((HtmlInputRadioButton)x).Visible = true;
                            else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                                ((HtmlInputRadioButton)x).Visible = false;
                        }

                        if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputCheckBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                                ((HtmlInputCheckBox)x).Visible = true;
                            else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                                ((HtmlInputCheckBox)x).Visible = false;
                        }
                    }
                }
            }
        }

        private void DisplayDeathreasonpnl()
        {
            ICareEnded CEControl = (ICareEnded)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BCareEnded,BusinessProcess.Scheduler");
            DataSet dsCareEndedDeath = CEControl.GetCareEndedDeathReason(Convert.ToInt32(Session["TechnicalAreaId"]));
            BindFunctions theBindDeath = new BindFunctions();

            theBindDeath.BindCombo(cmbDeathReason, dsCareEndedDeath.Tables[1], "Name", "DeathReasonID");
        }

        private void DisplaySavedFormData()
        {
            ICareEnded CEControl;

            if (VisitID == 0)
            {
                return;
            }

            CEControl = (ICareEnded)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BCareEnded,BusinessProcess.Scheduler");
            DataSet DSView = CEControl.GetSavedFormData(VisitID, Convert.ToInt32(Session["TechnicalAreaId"]));
            if (DSView.Tables[0].Rows[0]["DateLastContact"].ToString() != "")
            {
                txtDateLastContact.Value = Convert.ToDateTime(DSView.Tables[0].Rows[0]["DateLastContact"]).ToString(Session["AppDateFormat"].ToString());
            }
            ddinterviewer.SelectedValue = DSView.Tables[0].Rows[0]["EmployeeId"].ToString();

            Session["OldData"] = DSView;
            foreach (DataTable theDT in DSView.Tables)
            {
                if (theDT.Columns.Contains("TableName") == true && theDT.Rows.Count > 0)
                //if (theDT.Rows.Count > 0)
                {
                    if (theDT.Rows[0]["TableName"].ToString().ToUpper() == "DTL_PATIENTCAREENDED")
                    {
                        cmbPatientExitReason.SelectedValue = theDT.Rows[0]["PatientExitReason"].ToString();
                        if ((theDT.Rows[0]["MissedAppDate"]).ToString() != "")
                        {
                            txtMissedAppDate.Value = Convert.ToDateTime(theDT.Rows[0]["MissedAppDate"]).ToString(Session["AppDateFormat"].ToString());
                        }
                        if ((theDT.Rows[0]["CareEndedDate"]).ToString() != "")
                        {
                            txtCareEndDate.Value = Convert.ToDateTime(theDT.Rows[0]["CareEndedDate"]).ToString(Session["AppDateFormat"].ToString());
                        }

                        cmbDeathReason.SelectedValue = theDT.Rows[0]["DeathReason"].ToString();

                        if ((theDT.Rows[0]["DeathDate"]).ToString() != "")
                        {
                            txtDeathDate.Value = Convert.ToDateTime(theDT.Rows[0]["DeathDate"]).ToString(Session["AppDateFormat"].ToString());
                        }

                        EventArgs s = new EventArgs();
                        cmbPatientExitReason_SelectedIndexChanged(this, s);
                        cmbDeathReason_SelectedIndexChanged(this, s);
                    }
                }
            }
            EnableDisableControls();
        }

        private void EnableDisableControls()
        {
            DataSet theControlsDS = (DataSet)Session["CareEndFields"];
            /////////Table -0
            foreach (DataRow theCntrlDR in theControlsDS.Tables[0].Rows)
            {
                DataView theCntrlDV = new DataView(theControlsDS.Tables[6]);
                theCntrlDV.RowFilter = "ConFieldId=" + theCntrlDR["FieldId"].ToString();

                if (theCntrlDV.Count > 0)
                {
                    foreach (Control x in DIVCustomItem.Controls)
                    {
                        if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel")
                        {
                            foreach (Control thePnlCntrl in x.Controls)
                            {
                                if (thePnlCntrl.GetType().ToString() == "System.Web.UI.WebControls.CheckBox")
                                {
                                    CheckBox theCntrl = (CheckBox)thePnlCntrl;
                                    string[] theID = theCntrl.ID.Split('-');
                                    if (theID[1] == theCntrlDR["FieldName"].ToString() && theID[3] == theCntrlDR["FieldId"].ToString())
                                    {
                                        if (theCntrl.Checked == true)
                                            HtmlCheckBoxSelect(theCntrl);
                                    }
                                }
                            }
                        }
                        if (x.GetType().ToString() == "System.Web.UI.WebControls.DropDownList")
                        {
                            DropDownList theCntrl = (DropDownList)x;
                            string[] theID = theCntrl.ID.Split('-');
                            if (theID[1] == theCntrlDR["FieldName"].ToString() && theID[3] == theCntrlDR["FieldId"].ToString())
                            {
                                EventArgs s = new EventArgs();
                                ddlSelectList_SelectedIndexChanged(theCntrl, s);
                            }
                        }
                        if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputRadioButton")
                        {
                            HtmlInputRadioButton theCntrl = (HtmlInputRadioButton)x;
                            string[] theID = theCntrl.ID.Split('-');
                            if (theID[1] == theCntrlDR["FieldName"].ToString() && theID[3] == theCntrlDR["FieldId"].ToString())
                            {
                                if (theCntrl.Checked == true)
                                    HtmlRadioButtonSelect(theCntrl);
                            }
                        }
                    }
                    foreach (Control x in PnlConFields.Controls)
                    {
                        if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel")
                        {
                            foreach (Control thePnlCntrl in x.Controls)
                            {
                                if (thePnlCntrl.GetType().ToString() == "System.Web.UI.WebControls.CheckBox")
                                {
                                    CheckBox theCntrl = (CheckBox)thePnlCntrl;
                                    string[] theID = theCntrl.ID.Split('-');
                                    if (theID[1] == theCntrlDR["FieldName"].ToString() && theID[3] == theCntrlDR["FieldId"].ToString())
                                    {
                                        if (theCntrl.Checked == true)
                                            HtmlCheckBoxSelect(theCntrl);
                                    }
                                }
                            }
                        }

                        if (x.GetType().ToString() == "System.Web.UI.WebControls.DropDownList")
                        {
                            DropDownList theCntrl = (DropDownList)x;
                            string[] theID = theCntrl.ID.Split('-');
                            if (theID[1] == theCntrlDR["FieldName"].ToString() && theID[3] == theCntrlDR["FieldId"].ToString())
                            {
                                EventArgs s = new EventArgs();
                                ddlSelectList_SelectedIndexChanged(theCntrl, s);
                            }
                        }
                        if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputRadioButton")
                        {
                            HtmlInputRadioButton theCntrl = (HtmlInputRadioButton)x;
                            string[] theID = theCntrl.ID.Split('-');
                            if (theID[1] == theCntrlDR["FieldName"].ToString() && theID[3] == theCntrlDR["FieldId"].ToString())
                            {
                                if (theCntrl.Checked == true)
                                    HtmlRadioButtonSelect(theCntrl);
                            }
                        }
                    }
                }
            }
            //////////////////Table - 1
            foreach (DataRow theCntrlDR in theControlsDS.Tables[1].Rows)
            {
                DataView theCntrlDV = new DataView(theControlsDS.Tables[6]);
                theCntrlDV.RowFilter = "ConFieldId=" + theCntrlDR["FieldId"].ToString();
                if (theCntrlDV.Count > 0)
                {
                    foreach (Control x in DIVCustomItem.Controls)
                    {
                        if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel")
                        {
                            foreach (Control thePnlCntrl in x.Controls)
                            {
                                if (thePnlCntrl.GetType().ToString() == "System.Web.UI.WebControls.CheckBox")
                                {
                                    CheckBox theCntrl = (CheckBox)thePnlCntrl;
                                    string[] theID = theCntrl.ID.Split('-');
                                    if (theID[1] == theCntrlDR["FieldName"].ToString() && theID[3] == theCntrlDR["FieldId"].ToString())
                                    {
                                        if (theCntrl.Checked == true)
                                            HtmlCheckBoxSelect(theCntrl);
                                    }
                                }
                            }
                        }

                        if (x.GetType().ToString() == "System.Web.UI.WebControls.DropDownList")
                        {
                            DropDownList theCntrl = (DropDownList)x;
                            string[] theID = theCntrl.ID.Split('-');
                            if (theID[1] == theCntrlDR["FieldName"].ToString() && theID[3] == theCntrlDR["FieldId"].ToString())
                            {
                                EventArgs s = new EventArgs();
                                ddlSelectList_SelectedIndexChanged(theCntrl, s);
                            }
                        }
                        if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputRadioButton")
                        {
                            HtmlInputRadioButton theCntrl = (HtmlInputRadioButton)x;
                            string[] theID = theCntrl.ID.Split('-');
                            if (theID[1] == theCntrlDR["FieldName"].ToString() && theID[3] == theCntrlDR["FieldId"].ToString())
                            {
                                if (theCntrl.Checked == true)
                                    HtmlRadioButtonSelect(theCntrl);
                            }
                        }
                    }
                    foreach (Control x in PnlConFields.Controls)
                    {
                        if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel")
                        {
                            foreach (Control thePnlCntrl in x.Controls)
                            {
                                if (thePnlCntrl.GetType().ToString() == "System.Web.UI.WebControls.CheckBox")
                                {
                                    CheckBox theCntrl = (CheckBox)thePnlCntrl;
                                    string[] theID = theCntrl.ID.Split('-');
                                    if (theID[1] == theCntrlDR["FieldName"].ToString() && theID[3] == theCntrlDR["FieldId"].ToString())
                                    {
                                        if (theCntrl.Checked == true)
                                            HtmlCheckBoxSelect(theCntrl);
                                    }
                                }
                            }
                        }

                        if (x.GetType().ToString() == "System.Web.UI.WebControls.DropDownList")
                        {
                            DropDownList theCntrl = (DropDownList)x;
                            string[] theID = theCntrl.ID.Split('-');
                            if (theID[1] == theCntrlDR["FieldName"].ToString() && theID[3] == theCntrlDR["FieldId"].ToString())
                            {
                                EventArgs s = new EventArgs();
                                ddlSelectList_SelectedIndexChanged(theCntrl, s);
                            }
                        }
                        if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputRadioButton")
                        {
                            HtmlInputRadioButton theCntrl = (HtmlInputRadioButton)x;
                            string[] theID = theCntrl.ID.Split('-');
                            if (theID[1] == theCntrlDR["FieldName"].ToString() && theID[3] == theCntrlDR["FieldId"].ToString())
                            {
                                if (theCntrl.Checked == true)
                                    HtmlRadioButtonSelect(theCntrl);
                            }
                        }
                    }

                    foreach (Control x in DIVCustomItem.Controls)
                    {
                        if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel")
                        {
                            foreach (Control thePnlCntrl in x.Controls)
                            {
                                if (thePnlCntrl.GetType().ToString() == "System.Web.UI.WebControls.CheckBox")
                                {
                                    CheckBox theCntrl = (CheckBox)thePnlCntrl;
                                    string[] theID = theCntrl.ID.Split('-');
                                    if (theID[1] == theCntrlDR["FieldName"].ToString() && theID[3] == theCntrlDR["FieldId"].ToString())
                                    {
                                        if (theCntrl.Checked == true)
                                            HtmlCheckBoxSelect(theCntrl);
                                    }
                                }
                            }
                        }

                        if (x.GetType().ToString() == "System.Web.UI.WebControls.DropDownList")
                        {
                            DropDownList theCntrl = (DropDownList)x;
                            string[] theID = theCntrl.ID.Split('-');
                            if (theID[1] == theCntrlDR["FieldName"].ToString() && theID[3] == theCntrlDR["FieldId"].ToString())
                            {
                                EventArgs s = new EventArgs();
                                ddlSelectList_SelectedIndexChanged(theCntrl, s);
                            }
                        }
                        if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputRadioButton")
                        {
                            HtmlInputRadioButton theCntrl = (HtmlInputRadioButton)x;
                            string[] theID = theCntrl.ID.Split('-');
                            if (theID[1] == theCntrlDR["FieldName"].ToString() && theID[3] == theCntrlDR["FieldId"].ToString())
                            {
                                if (theCntrl.Checked == true)
                                    HtmlRadioButtonSelect(theCntrl);
                            }
                        }
                    }
                }
            }
        }
        private void FillCheckBoxListData(DataTable theDT, Panel thePnl, string FieldName, string theFieldName)
        {
            foreach (DataRow DR in theDT.Rows)
            {
                foreach (Control y in thePnl.Controls)
                {
                    if (y.GetType() == typeof(System.Web.UI.WebControls.Panel))
                        FillCheckBoxListData(theDT, (System.Web.UI.WebControls.Panel)y, FieldName, theFieldName);
                    else
                    {
                        if (y.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                        {
                            if (((CheckBox)y).ID == theCheckBoxList.ID + "-" + DR[FieldName].ToString())
                                ((CheckBox)y).Checked = true;
                        }
                        if (y.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                        {
                            if (((System.Web.UI.WebControls.TextBox)y).ID.Contains("OtherTXT") == true)
                            {
                                if (theFieldName.ToString() != "")
                                {
                                    ((TextBox)y).Text = DR[theFieldName].ToString();
                                }
                            }
                            string script = "";
                            script = "<script language = 'javascript' defer ='defer' id = " + ((TextBox)y).ID + ">\n";
                            script += "show('txtother');\n";
                            script += "</script>\n";
                            ClientScript.RegisterStartupScript(this.GetType(), "" + ((TextBox)y).ID + "", script);
                        }
                    }
                }
            }
        }

        private DataTable GetSaveStatement(Control thePnl, DataTable theDT)
        {
            string fName = "";
            string FtbName = "";
  

            foreach (Control y in thePnl.Controls)
            {
                if (y.GetType() == typeof(System.Web.UI.WebControls.Panel))
                {
                    GetSaveStatement(y, theDT);
                }
                else
                {
                    if (y.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                    {
                        if (((TextBox)y).Text != "")
                        {
                            bool theOtherflag = false;
                            string[] theDBInfo = y.ID.Split('-');
                            DataRow theDR = theDT.NewRow();
                            fName = theDBInfo.GetValue(1).ToString();
                            FtbName = theDBInfo.GetValue(2).ToString();
                            theDR["TableName"] = FtbName;
                            theDR["FieldName"] = fName;
                            if (FtbName.ToUpper() == "DTL_PATIENTTRACKINGCARE")
                                theDR["Priority"] = "1";
                            else if (FtbName.ToUpper() == "DTL_PATIENTCAREENDED")
                                theDR["Priority"] = "2";
                            else
                                theDR["Priority"] = "999";

                            if (theDBInfo.GetValue(0).ToString() == "TXT")
                                theDR["Value"] = ((TextBox)y).Text;
                            else if (theDBInfo.GetValue(0).ToString() == "TXTDCM")
                            {
                                if (((TextBox)y).Text == "")
                                {
                                    theDR["Value"] = 0;
                                }
                                else
                                {
                                    theDR["Value"] = Convert.ToDecimal(((TextBox)y).Text);
                                }
                            }
                            else if (theDBInfo.GetValue(0).ToString() == "TXTNUM")
                            {
                                if (((TextBox)y).Text == "")
                                {
                                    theDR["Value"] = 0;
                                }
                                else
                                {
                                    theDR["Value"] = Convert.ToInt32(((TextBox)y).Text);
                                }
                            }
                            else if (theDBInfo.GetValue(0).ToString() == "TXTDT")
                                theDR["Value"] = ((TextBox)y).Text;
                            else if (theDBInfo.GetValue(0).ToString() == "TXTMulti")
                                theDR["Value"] = ((TextBox)y).Text;
                            else if (theDBInfo.GetValue(0).ToString() == "OtherTXT")
                            {
                                if (((TextBox)y).Text != "")
                                {
                                    string[] theList = ((TextBox)y).ID.ToString().Split('-');
                                    DataView theDTView = new DataView(theDT);
                                    theDTView.RowFilter = "TableName = '" + theList.GetValue(2).ToString() + "' and FieldName='" + theList.GetValue(1).ToString() + "' and Value=" + theList.GetValue(4).ToString();
                                    theDTView[0]["OtherDesc"] = ((TextBox)y).Text;
                                    theOtherflag = true;
                                }
                            }
                            if (theOtherflag == false)
                                theDT.Rows.Add(theDR);
                        }
                    }
                    else if (y.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                    {
                        string[] theDBInfo = y.ID.Split('-');
                        DataRow theDR = theDT.NewRow();
                        fName = theDBInfo.GetValue(1).ToString();
                        FtbName = theDBInfo.GetValue(2).ToString();
                        theDR["TableName"] = FtbName;
                        theDR["FieldName"] = fName;
                        theDR["Value"] = ((DropDownList)y).SelectedValue.ToString();
                        if (FtbName.ToUpper() == "DTL_PATIENTTRACKINGCARE")
                            theDR["Priority"] = "1";
                        else if (FtbName.ToUpper() == "DTL_PATIENTCAREENDED")
                            theDR["Priority"] = "2";
                        else
                            theDR["Priority"] = "999";

                        theDT.Rows.Add(theDR);
                    }
                    else if (y.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
                    {
                        string[] theDBInfo = y.ID.Split('-');
                        DataRow theDR = theDT.NewRow();
                        fName = theDBInfo.GetValue(1).ToString();
                        FtbName = theDBInfo.GetValue(2).ToString();
                        theDR["TableName"] = FtbName;
                        theDR["FieldName"] = fName;
                        if (theDBInfo.GetValue(0).ToString() == "RADIO1" && ((HtmlInputRadioButton)y).Checked == true)
                            theDR["Value"] = "1";
                        else if (theDBInfo.GetValue(0).ToString() == "RADIO2" && ((HtmlInputRadioButton)y).Checked == true)
                            theDR["Value"] = "0";
                        if (FtbName.ToUpper() == "DTL_PATIENTTRACKINGCARE")
                            theDR["Priority"] = "1";
                        else if (FtbName.ToUpper() == "DTL_PATIENTCAREENDED")
                            theDR["Priority"] = "2";
                        else
                            theDR["Priority"] = "999";
                        if (theDR["Value"].ToString() != "")
                            theDT.Rows.Add(theDR);
                    }
                    else if (y.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                    {
                        string[] theDBInfo = y.ID.Split('-');
                        DataRow theDR = theDT.NewRow();
                        fName = theDBInfo.GetValue(1).ToString();
                        FtbName = theDBInfo.GetValue(2).ToString();
                        theDR["TableName"] = FtbName;
                        theDR["FieldName"] = fName;
                        if (((HtmlInputCheckBox)y).Checked == true)
                            theDR["Value"] = "1";
                        else if (((HtmlInputCheckBox)y).Checked == true)
                            theDR["Value"] = "0";
                        if (FtbName.ToUpper() == "DTL_PATIENTTRACKINGCARE")
                            theDR["Priority"] = "1";
                        else if (FtbName.ToUpper() == "DTL_PATIENTCAREENDED")
                            theDR["Priority"] = "2";
                        else
                            theDR["Priority"] = "999";

                        theDT.Rows.Add(theDR);
                    }
                    else if (y.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                    {
                        if (((CheckBox)y).Checked == true)
                        {
                            string[] theDBInfo = y.ID.Split('-');

                            DataRow theDR = theDT.NewRow();
                            fName = theDBInfo.GetValue(1).ToString();
                            FtbName = theDBInfo.GetValue(2).ToString();
                            theDR["TableName"] = FtbName;
                            theDR["FieldName"] = fName;
                            theDR["Value"] = theDBInfo.GetValue(5).ToString();
                            if (FtbName.ToUpper() == "DTL_PATIENTTRACKINGCARE")
                                theDR["Priority"] = "1";
                            else if (FtbName.ToUpper() == "DTL_PATIENTCAREENDED")
                                theDR["Priority"] = "2";
                            else
                                theDR["Priority"] = "999";
                            theDT.Rows.Add(theDR);
                        }
                    }
                }
            }
            return theDT;
        }

        private void LoadFieldTypeControl(DataView ReqDv, Panel thePnl, string FeatureName, string SectionId, string SectionName, string CFieldId, string FieldLabel, string FieldName, string ControlID, string SavingTable, string BindingTable, string PreDefined, bool theEnable)
        {
            IQCareUtils theUtils = new IQCareUtils();
            BindFunctions BindManager = new BindFunctions();
            DataView theDV;
            DataView theDVModeCode = new DataView();
            DataView theDVModCode = new DataView();
            DataSet theDSxml = (DataSet)Session["XMLTables"];
            DataTable ReqDT = new DataTable();
            if (ReqDv.Table != null)
            {
                ReqDT = ReqDv.ToTable();
                ViewState["BusRule"] = ReqDT;
            }
            if (ControlID == "1") ///SingleLine Text Box
            {
                Label theLbl = new Label();
                theLbl.ID = "LBL-" + FieldName + "-" + SavingTable + "-" + CFieldId + "-" + thePnl.ID;
                theLbl.Text = FieldLabel + " : ";
                theLbl.CssClass = "bold";
                thePnl.Controls.Add(theLbl);

                TextBox theSingleText = new TextBox();
                theSingleText.ID = "TXT-" + FieldName + "-" + SavingTable + "-" + CFieldId + "-" + thePnl.ID;
                //theSingleText.Load += new EventHandler(theSingleText_Load);
                theSingleText.Width = 180;
                theSingleText.MaxLength = 50;
                theSingleText.Enabled = theEnable;
                thePnl.Controls.Add(theSingleText);
                theSingleText.Attributes.Add("onkeyup", "chkAlphaNumericString('" + theSingleText.ClientID + "');");
                SetBusinessrule(theLbl, theSingleText, Convert.ToInt32(CFieldId));

                DataSet theDS = (DataSet)Session["OldData"];
                if (theDS != null)
                {
                    foreach (DataTable theDT in theDS.Tables)
                    {
                        if (theDT.Columns.Contains("TableName") == true && theDT.Rows.Count > 0)
                        {
                            if (theDT.Rows[0]["TableName"].ToString().ToUpper() == SavingTable.ToUpper())
                            {
                                theSingleText.Text = theDT.Rows[0][FieldName].ToString();
                            }
                        }
                    }
                }

            }
            else if (ControlID == "2") ///DecimalTextBox
            {
                Label theLbl = new Label();
                theLbl.ID = "LBL-" + FieldName + "-" + SavingTable + "-" + CFieldId + "-" + thePnl.ID;
                theLbl.Text = FieldLabel + " : ";
                theLbl.CssClass = "bold";
                thePnl.Controls.Add(theLbl);

                TextBox theSingleDecimalText = new TextBox();
                theSingleDecimalText.ID = "TXTDCM-" + FieldName + "-" + SavingTable + "-" + CFieldId + "-" + thePnl.ID;
                //theSingleDecimalText.Load += new EventHandler(DecimalText_Load);
                theSingleDecimalText.Width = 180;
                theSingleDecimalText.MaxLength = 50;
                theSingleDecimalText.Enabled = theEnable;
                thePnl.Controls.Add(theSingleDecimalText);
                theSingleDecimalText.Attributes.Add("onkeyup", "chkDecimal('" + theSingleDecimalText.ClientID + "');");
                SetBusinessrule(theLbl, theSingleDecimalText, Convert.ToInt32(CFieldId));

                DataSet theDS = (DataSet)Session["OldData"];
                if (theDS != null)
                {
                    foreach (DataTable theDT in theDS.Tables)
                    {
                        if (theDT.Columns.Contains("TableName") == true && theDT.Rows.Count > 0)
                        {
                            if (theDT.Rows[0]["TableName"].ToString().ToUpper() == SavingTable.ToUpper())
                            {
                                theSingleDecimalText.Text = theDT.Rows[0][FieldName].ToString();
                            }
                        }
                    }
                }

            }
            else if (ControlID == "3")   /// Numeric (Integer)
            {
                Label theLbl = new Label();
                theLbl.ID = "LBL-" + FieldName + "-" + SavingTable + "-" + CFieldId + "-" + thePnl.ID;
                theLbl.Text = FieldLabel + " : ";
                theLbl.CssClass = "bold";
                thePnl.Controls.Add(theLbl);

                TextBox theNumberText = new TextBox();
                theNumberText.ID = "TXTNUM-" + FieldName + "-" + SavingTable + "-" + CFieldId + "-" + thePnl.ID;
                //theNumberText.Load += new EventHandler(IntegerText_Load);
                theNumberText.Width = 100;
                theNumberText.MaxLength = 9;
                theNumberText.Enabled = theEnable;
                thePnl.Controls.Add(theNumberText);
                theNumberText.Attributes.Add("onkeyup", "chkNumber('" + theNumberText.ClientID + "');");
                SetBusinessrule(theLbl, theNumberText, Convert.ToInt32(CFieldId));

                DataSet theDS = (DataSet)Session["OldData"];
                if (theDS != null)
                {
                    foreach (DataTable theDT in theDS.Tables)
                    {
                        if (theDT.Columns.Contains("TableName") == true && theDT.Rows.Count > 0)
                        {
                            if (theDT.Rows[0]["TableName"].ToString().ToUpper() == SavingTable.ToUpper())
                            {
                                theNumberText.Text = theDT.Rows[0][FieldName].ToString();
                            }
                        }
                    }
                }

            }
            else if (ControlID == "4") /// Dropdown
            {
                Label theLbl = new Label();
                theLbl.ID = "LBL-" + FieldName + "-" + SavingTable + "-" + CFieldId + "-" + thePnl.ID;
                theLbl.Text = FieldLabel + " : ";
                theLbl.CssClass = "bold";
                thePnl.Controls.Add(theLbl);
                DropDownList ddlSelectList = new DropDownList();

                ddlSelectList.ID = "SelectList-" + FieldName + "-" + SavingTable + "-" + CFieldId + "-" + thePnl.ID;
                theDV = new DataView(theDSxml.Tables[BindingTable]);
                if (BindingTable == "Mst_ModDecode")
                {
                    theDVModCode = new DataView(theDSxml.Tables["mst_modcode"].Copy());
                    theDVModCode.RowFilter = "Name='" + FieldName.ToString() + "' and (DeleteFlag=0 or DeleteFlag is null)";
                    DataTable theDTModCode = theDVModCode.ToTable();
                    if (theDVModCode.Count > 0)
                    {
                        theDVModeCode = new DataView(theDSxml.Tables["mst_modDecode"].Copy());
                        theDVModeCode.RowFilter = "CodeId=" + theDTModCode.Rows[0]["CodeId"].ToString() + " and (DeleteFlag=0 or DeleteFlag is null)";
                        if (theDVModeCode.Count > 0)
                        {
                            DataTable theDT = theDVModeCode.ToTable();
                            ddlSelectList.DataSource = null;
                            BindManager.BindCombo(ddlSelectList, theDT, "Name", "Id");
                        }
                        else
                        {
                            // ddlSelectList.Items.Add("Select");
                        }
                    }
                }
                else
                {
                    DataTable theDT = new DataTable();
                    theDV = new DataView(theDSxml.Tables[BindingTable].Copy());
                    if (BindingTable == "Mst_Decode")
                    {
                        theDVModCode = new DataView(theDSxml.Tables["mst_code"].Copy());
                        theDVModCode.RowFilter = "Name='" + FieldName.ToString() + "' and (DeleteFlag=0 or DeleteFlag is null)";
                        DataTable theDTModCode = theDVModCode.ToTable();
                        theDV.RowFilter = "CodeId=" + theDTModCode.Rows[0]["CodeId"].ToString() + " and (DeleteFlag=0 or DeleteFlag is null)";
                    }
                    else
                    {
                        //theDVModeCode.RowFilter = "CodeId=" + theDTModCode.Rows[0]["CodeId"].ToString() + " and (DeleteFlag=0 or DeleteFlag is null)";
                        theDV.RowFilter = "DeleteFlag=0 or DeleteFlag is null";
                    }
                    if (theDV.Count > 0)
                    {
                        theDT = theDV.ToTable();
                        ddlSelectList.DataSource = null;
                        BindManager.BindCombo(ddlSelectList, theDT, "Name", "Id");
                    }
                    else
                    {
                        //ddlSelectList.Items.Add("Select");
                    }
                }
                ddlSelectList.Width = 180;
                ddlSelectList.Enabled = theEnable;

                if (theConditional == true && theEnable == true)
                {
                    ddlSelectList.AutoPostBack = true;
                    ddlSelectList.SelectedIndexChanged += new EventHandler(ddlSelectList_SelectedIndexChanged);
                }
                thePnl.Controls.Add(ddlSelectList);

                SetBusinessrule(theLbl, ddlSelectList, Convert.ToInt32(CFieldId));

                DataSet theDS = (DataSet)Session["OldData"];
                if (theDS != null)
                {
                    foreach (DataTable theDT in theDS.Tables)
                    {
                        if (theDT.Columns.Contains("TableName") == true && theDT.Rows.Count > 0)
                        {
                            if (theDT.Rows[0]["TableName"].ToString().ToUpper() == SavingTable.ToUpper())
                            {
                                foreach (DataRow DR in theDT.Rows)
                                {
                                    if (DR[FieldName].ToString() != "")
                                    {
                                        //ddlSelectList.SelectedValue = theDT.Rows[0][FieldName].ToString();
                                        ddlSelectList.SelectedValue = DR[FieldName].ToString();
                                    }
                                }
                            }
                        }
                    }
                }

            }
            else if (ControlID == "5") ///Date
            {
                Label theLbl = new Label();
                theLbl.ID = "LBL-" + FieldName + "-" + SavingTable + "-" + CFieldId + "-" + thePnl.ID;
                theLbl.Text = FieldLabel + " : ";
                theLbl.CssClass = "bold";
                thePnl.Controls.Add(theLbl);
                //theDateText_Load.Load += new EventHandler(theDateText_Load);

                TextBox theDateText = new TextBox();
                theDateText.ID = "TXTDT-" + FieldName + "-" + SavingTable + "-" + CFieldId + "-" + thePnl.ID;
                Control ctl = (TextBox)theDateText;

                //theSingleText.Load += new EventHandler(theDateText_Load);

                theDateText.Width = 83;
                theDateText.MaxLength = 11;
                theDateText.Enabled = theEnable;
                theDateText.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
                theDateText.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
                thePnl.Controls.Add(theDateText);

                thePnl.Controls.Add(new LiteralControl("&nbsp;"));

                Image theDateImage = new Image();
                theDateImage.ID = "img" + theDateText.ID;
                theDateImage.Height = 22;
                theDateImage.Width = 22;
                theDateImage.Visible = theEnable;
                theDateImage.ToolTip = "Date Helper";
                theDateImage.ImageUrl = "~/images/cal_icon.gif";
                theDateImage.Attributes.Add("onClick", "w_displayDatePicker('" + ((TextBox)ctl).ClientID + "');");
                thePnl.Controls.Add(theDateImage);
                thePnl.Controls.Add(new LiteralControl("<span class='smallerlabel'>(DD-MMM-YYYY)</span>"));

                SetBusinessrule(theLbl, theDateText, Convert.ToInt32(CFieldId));

                DataSet theDS = (DataSet)Session["OldData"];
                if (theDS != null)
                {
                    foreach (DataTable theDT in theDS.Tables)
                    {
                        if (theDT.Columns.Contains("TableName") == true && theDT.Rows.Count > 0)
                        {
                            if (theDT.Rows[0]["TableName"].ToString().ToUpper() == SavingTable.ToUpper())
                            {
                                //theDateText.Text = theDT.Rows[0][FieldName].ToString();

                                foreach (DataRow DR in theDT.Rows)
                                {
                                    if (DR[FieldName].ToString() != "")
                                    {
                                        //theDateText.Text = Convert.ToDateTime(DR[FieldName].ToString());

                                        theDateText.Text = Convert.ToDateTime(DR[FieldName]).ToString(Session["AppDateFormat"].ToString());
                                    }
                                }
                            }
                        }
                    }
                }

            }
            else if (ControlID == "6")  /// Radio Button
            {
                Label theLbl = new Label();
                theLbl.ID = "LBL-" + FieldName + "-" + SavingTable + "-" + CFieldId + "-" + thePnl.ID;
                theLbl.Text = FieldLabel + " : ";
                theLbl.CssClass = "bold";
                thePnl.Controls.Add(theLbl);

                HtmlInputRadioButton theYesNoRadio1 = new HtmlInputRadioButton();
                theYesNoRadio1.ID = "RADIO1-" + FieldName + "-" + SavingTable + "-" + CFieldId + "-" + thePnl.ID;
                theYesNoRadio1.Value = "Yes";
                theYesNoRadio1.Name = "" + FieldName + "";
                theYesNoRadio1.Attributes.Add("onfocus", "up(this)");
                theYesNoRadio1.Visible = theEnable;

                if (theConditional == true && theEnable == true)
                    theYesNoRadio1.Attributes.Add("onclick", "down(this);SetValue('theHitCntrl','System.Web.UI.HtmlControls.HtmlInputRadioButton%" + theYesNoRadio1.ClientID + "');");
                else
                    theYesNoRadio1.Attributes.Add("onclick", "down(this);");
                thePnl.Controls.Add(theYesNoRadio1);
                thePnl.Controls.Add(new LiteralControl("<label align='labelright' id='lblYes-" + CFieldId + "'>Yes</label>"));

                SetBusinessrule(theLbl, theYesNoRadio1, Convert.ToInt32(CFieldId));

                HtmlInputRadioButton theYesNoRadio2 = new HtmlInputRadioButton();
                theYesNoRadio2.ID = "RADIO2-" + FieldName + "-" + SavingTable + "-" + CFieldId + "-" + thePnl.ID;

                theYesNoRadio2.Value = "No";
                theYesNoRadio2.Name = "" + FieldName + "";
                theYesNoRadio2.Visible = theEnable;

                if (theConditional == true && theEnable == true)
                    theYesNoRadio2.Attributes.Add("onclick", "down(this);SetValue('theHitCntrl','System.Web.UI.HtmlControls.HtmlInputRadioButton%" + theYesNoRadio2.ClientID + "');");
                else
                    theYesNoRadio2.Attributes.Add("onclick", "down(this);");
                theYesNoRadio2.Attributes.Add("onchange", "up(this)");
                thePnl.Controls.Add(theYesNoRadio2);

                thePnl.Controls.Add(new LiteralControl("<label align='labelright' id='lblNo-" + CFieldId + "'>No</label>"));
                SetBusinessrule(theLbl, theYesNoRadio2, Convert.ToInt32(CFieldId));

                DataSet theDS = (DataSet)Session["OldData"];
                if (theDS != null)
                {
                    foreach (DataTable theDT in theDS.Tables)
                    {
                        if (theDT.Columns.Contains("TableName") == true && theDT.Rows.Count > 0)
                        {
                            if (theDT.Rows[0]["TableName"].ToString().ToUpper() == SavingTable.ToUpper())
                            {
                                //if (theDT.Rows[0][FieldName].ToString() == "1")
                                //{
                                //    theYesNoRadio1.Checked = true;
                                //}
                                //else
                                //{
                                //    theYesNoRadio2.Checked = true;
                                //}

                                foreach (DataRow DR in theDT.Rows)
                                {
                                    if (DR[FieldName].ToString() != "")
                                    {
                                        if (DR[FieldName].ToString() == "True")
                                        {
                                            theYesNoRadio1.Checked = true;
                                        }
                                        else
                                        {
                                            theYesNoRadio2.Checked = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }
            else if (ControlID == "7") //Checkbox
            {
                Label theLbl = new Label();
                theLbl.ID = "LBL-" + FieldName + "-" + SavingTable + "-" + CFieldId + "-" + thePnl.ID;
                theLbl.Text = FieldLabel + " : ";
                theLbl.CssClass = "bold";
                thePnl.Controls.Add(theLbl);

                HtmlInputCheckBox theChk = new HtmlInputCheckBox();
                theChk.ID = "Chk-" + FieldName + "-" + SavingTable + "-" + CFieldId + "-" + thePnl.ID;
                theChk.Visible = theEnable;
                thePnl.Controls.Add(theChk);
                SetBusinessrule(theLbl, theChk, Convert.ToInt32(CFieldId));

                DataSet theDS = (DataSet)Session["OldData"];
                if (theDS != null)
                {
                    foreach (DataTable theDT in theDS.Tables)
                    {
                        if (theDT.Columns.Contains("TableName") == true && theDT.Rows.Count > 0)
                        {
                            if (theDT.Rows[0]["TableName"].ToString().ToUpper() == SavingTable.ToUpper())
                            {
                                //if (theDT.Rows[0][FieldName].ToString() == "1")
                                //{
                                //    theChk.Checked = true;
                                //}
                                //else
                                //{
                                //    theChk.Checked = true;
                                //}

                                foreach (DataRow DR in theDT.Rows)
                                {
                                    if (DR[FieldName].ToString() != "")
                                    {
                                        if (DR[FieldName].ToString() == "1")
                                        {
                                            theChk.Checked = true;
                                        }
                                        else
                                        {
                                            theChk.Checked = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }
            else if (ControlID == "8")  /// MultiLine TextBox
            {
                Label theLbl = new Label();
                theLbl.ID = "LBL-" + FieldName + "-" + SavingTable + "-" + CFieldId + "-" + thePnl.ID;
                theLbl.Text = FieldLabel + " : ";
                theLbl.CssClass = "bold";
                thePnl.Controls.Add(theLbl);

                TextBox theMultiText = new TextBox();
                theMultiText.ID = "TXTMulti-" + FieldName + "-" + SavingTable + "-" + CFieldId + "-" + thePnl.ID;
                theMultiText.Width = 200;
                theMultiText.TextMode = TextBoxMode.MultiLine;
                theMultiText.MaxLength = 200;
                thePnl.Controls.Add(theMultiText);
                theMultiText.Enabled = theEnable;
                SetBusinessrule(theLbl, theMultiText, Convert.ToInt32(CFieldId));

                DataSet theDS = (DataSet)Session["OldData"];
                if (theDS != null)
                {
                    foreach (DataTable theDT in theDS.Tables)
                    {
                        if (theDT.Columns.Contains("TableName") == true && theDT.Rows.Count > 0)
                        {
                            if (theDT.Rows[0]["TableName"].ToString().ToUpper() == SavingTable.ToUpper())
                            {
                                //theMultiText.Text = theDT.Rows[0][FieldName].ToString();

                                foreach (DataRow DR in theDT.Rows)
                                {
                                    if (DR[FieldName].ToString() != "")
                                    {
                                        theMultiText.Text = DR[FieldName].ToString();
                                    }
                                }
                            }
                        }
                    }
                }

            }
            else if (ControlID == "9") ///  MultiSelect List
            {
                Panel PnlMulti = new Panel();

                Label theLbl = new Label();
                theLbl.ID = "LBL-" + FieldName + "-" + SavingTable + "-" + CFieldId + "-" + thePnl.ID;
                theLbl.Text = FieldLabel + " : ";
                theLbl.CssClass = "bold";
                thePnl.Controls.Add(theLbl);

                PnlMulti.CssClass = "checkbox";
                PnlMulti.ID = "Pnl-" + FieldName + "-" + SavingTable + "-" + CFieldId + "-" + thePnl.ID;
                theDV = new DataView(theDSxml.Tables[BindingTable]);

                DataTable theBindDT = new DataTable();
                DataSet theCareEndFields = (DataSet)Session["CareEndFields"];
                DataView theXMLDV = new DataView(theDSxml.Tables[BindingTable]);

                DataView convw = new DataView(theCareEndFields.Tables[0]);
                convw.RowFilter = "ControlId=" + ControlID.ToString() + " and FieldId=" + CFieldId.ToString();
                if (theXMLDV.Table.Rows.Count > 0)
                {
                    DataView theFView = new DataView();
                    if (thePnl.ID == "DIVCustomItem")
                    {
                        if (convw.Count > 0 && theEnable == false)
                        {
                            theFView = new DataView(theCareEndFields.Tables[6]);
                        }
                        else
                        {
                            theFView = new DataView(theCareEndFields.Tables[0]);
                        }
                    }
                    else
                    {
                        if (convw.Count > 0)
                        {
                            theFView = new DataView(theCareEndFields.Tables[6]);
                        }
                        else
                        {
                            theFView = new DataView(theCareEndFields.Tables[5]);
                        }
                    }

                    //DataView theFView = new DataView(theCareEndFields.Tables[0]);
                    theFView.RowFilter = "FieldId=" + CFieldId;
                    if (theFView.Count > 0)
                    {
                        if (convw.Count > 0)
                        {
                            for (int i = 0; i < theFView.Table.Columns.Count; i++)
                            {
                                if (theFView.Table.Columns[i].ToString() == "Codeid")
                                {
                                    if (theFView[0]["Codeid"] != DBNull.Value)
                                    {
                                        //theXMLDV.RowFilter = theFView[0]["FilterColumn"].ToString() + " = " + theFView[0]["CategoryId"].ToString();

                                        theDV.RowFilter = "Codeid = " + theFView[0]["Codeid"].ToString();
                                        theBindDT = theDV.ToTable();
                                    }
                                }
                                else if (theFView[0]["FilterColumn"] != DBNull.Value)
                                {
                                    //theXMLDV.RowFilter = theFView[0]["FilterColumn"].ToString() + " = " + theFView[0]["CategoryId"].ToString();
                                    theDV.RowFilter = theFView[0]["FilterColumn"].ToString() + " = " + theFView[0]["CategoryId"].ToString();
                                    theBindDT = theDV.ToTable();
                                }
                            }

                            //if (theFView[0]["Codeid"] != DBNull.Value)
                            //{
                            //    //theXMLDV.RowFilter = theFView[0]["FilterColumn"].ToString() + " = " + theFView[0]["CategoryId"].ToString();

                            //    theDV.RowFilter = "Codeid = " + theFView[0]["Codeid"].ToString();
                            //    theBindDT = theDV.ToTable();

                            //}
                        }
                        else
                        {
                            if (theFView[0]["FilterColumn"] != DBNull.Value)
                            {
                                //theXMLDV.RowFilter = theFView[0]["FilterColumn"].ToString() + " = " + theFView[0]["CategoryId"].ToString();
                                theDV.RowFilter = theFView[0]["FilterColumn"].ToString() + " = " + theFView[0]["CategoryId"].ToString();
                                theBindDT = theDV.ToTable();
                            }
                            else
                            {
                                theBindDT = theXMLDV.ToTable();
                            }
                        }
                    }
                    else
                    {
                        DataView theCView = new DataView(theCareEndFields.Tables[1]);
                        theCView.RowFilter = "FieldId=" + CFieldId;
                        if (theCView.Count > 0)
                        {
                            if (theCView[0]["FilterColumn"] != DBNull.Value)
                            {
                                //theXMLDV.RowFilter = theCView[0]["FilterColumn"].ToString() + " = " + theCView[0]["CategoryId"].ToString();
                                theDV.RowFilter = theCView[0]["FilterColumn"].ToString() + " = " + theCView[0]["CategoryId"].ToString();
                                theBindDT = theDV.ToTable();
                            }
                            else
                            {
                                theBindDT = theXMLDV.ToTable();
                            }
                        }
                    }
                }
                //PnlMulti.Controls.Clear();
                if (theBindDT != null)
                {
                    if (theEnable == true)
                        BindManager.CreateCheckedList(PnlMulti, theBindDT, "SetValue('theHitCntrl','System.Web.UI.WebControls.CheckBox%');", "onclick");
                    else
                        BindManager.CreateCheckedList(PnlMulti, theBindDT, "", "");
                }
                PnlMulti.Enabled = theEnable;
                thePnl.Controls.Add(PnlMulti);

                SetBusinessrule(theLbl, PnlMulti, Convert.ToInt32(CFieldId));

                string theFieldName = "";
                DataSet theCareEndDS = (DataSet)Session["CareEndFields"];
                DataView theFieldDV = new DataView(theCareEndDS.Tables[4]);
                theFieldDV.RowFilter = "SavingTable='" + SavingTable + "'";
                if (theFieldDV.Count > 0)
                    theFieldName = theFieldDV[0]["OtherDesCol"].ToString();
                DataSet theDS = (DataSet)Session["OldData"];
                if (theDS != null)
                {
                    foreach (DataTable theDT in theDS.Tables)
                    {
                        if (theDT.Columns.Contains("TableName") == true && theDT.Rows.Count > 0)
                        {
                            if (theDT.Rows[0]["TableName"].ToString().ToUpper() == SavingTable.ToUpper())
                            {
                                theCheckBoxList = PnlMulti;
                                FillCheckBoxListData(theDT, PnlMulti, FieldName, theFieldName);
                            }
                        }
                    }
                }

            }
        }

        private void LoadPredefinedLabel_Field()
        {
            try
            {
                int td = 0;
                //PnlConFields.Controls.Clear();
                DIVCustomItem.Controls.Clear();
                //PnlConFields.Controls.Clear();
                DataSet theDS = (DataSet)Session["CareEndFields"];

                //DIVCustomItem.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' cellpadding='0' width='100%' border='0>"));
                if (theDS.Tables[0].Rows.Count > 0)
                {
                    ViewState["TopControl"] = theDS.Tables[0];
                    DIVCustomItem.Visible = true;
                    DIVCustomItem.Controls.Clear();
                    foreach (DataRow DRLnkTable in theDS.Tables[0].Rows)
                    {

                        DataView theDVConditionalField = new DataView(theDS.Tables[6]);
                        theDVConditionalField.RowFilter = "ConFieldId=" + DRLnkTable["FieldID"].ToString() + " and ConFieldPredefined=" + DRLnkTable["Predefined"].ToString();
                        theDVConditionalField.Sort = "Seq asc";
                        if (theDVConditionalField.Count > 0)
                            theConditional = true;
                        else
                            theConditional = false;

                        theDVReq.RowFilter = "FieldID=" + DRLnkTable["FieldId"].ToString() + " and Predefined =" + DRLnkTable["PreDefined"].ToString() + " ";

                        if (td == 0)
                        {
                            DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                            //DIVCustomItem.Controls.Add(new LiteralControl("<td class='form' style='width:50%'"));
                            DIVCustomItem.Controls.Add(new LiteralControl("<td  class='form' align='center' width='50%'>"));

                            LoadFieldTypeControl(theDVReq, DIVCustomItem, DRLnkTable["FeatureName"].ToString(), DRLnkTable["SectionId"].ToString(), DRLnkTable["SectionName"].ToString(), DRLnkTable["FieldId"].ToString(), DRLnkTable["FieldLabel"].ToString(), DRLnkTable["FieldName"].ToString(), DRLnkTable["ControlId"].ToString(), DRLnkTable["SavingTable"].ToString(), DRLnkTable["BindingTable"].ToString(), DRLnkTable["PreDefined"].ToString(), true);
                            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                            td = 1;
                        }
                        else
                        {
                            //DIVCustomItem.Controls.Add(new LiteralControl("<td class='form' style='width:50%'"));
                            DIVCustomItem.Controls.Add(new LiteralControl("<td  class='form' align='center' width='50%'>"));
                            LoadFieldTypeControl(theDVReq, DIVCustomItem, DRLnkTable["FeatureName"].ToString(), DRLnkTable["SectionId"].ToString(), DRLnkTable["SectionName"].ToString(), DRLnkTable["FieldId"].ToString(), DRLnkTable["FieldLabel"].ToString(), DRLnkTable["FieldName"].ToString(), DRLnkTable["ControlId"].ToString(), DRLnkTable["SavingTable"].ToString(), DRLnkTable["BindingTable"].ToString(), DRLnkTable["PreDefined"].ToString(), true);
                            DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                            DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                            td = 0;
                        }

                        if (theConditional == true)
                        {
                            for (int i = 0; i < theDVConditionalField.Count; i++)
                            {
                                if (td == 0)
                                {
                                    DIVCustomItem.Controls.Add(new LiteralControl("<tr>"));
                                    //DIVCustomItem.Controls.Add(new LiteralControl("<td class='form' style='width:50%'"));
                                    DIVCustomItem.Controls.Add(new LiteralControl("<td  class='form' align='center' width='50%'>"));

                                    LoadFieldTypeControl(theDVReq, DIVCustomItem, theDVConditionalField[i]["FeatureName"].ToString(), "", "",
                                        theDVConditionalField[i]["FieldId"].ToString(), theDVConditionalField[i]["FieldLabel"].ToString(), theDVConditionalField[i]["FieldName"].ToString(), theDVConditionalField[i]["ControlId"].ToString(),
                                        theDVConditionalField[i]["PdfTableName"].ToString(), theDVConditionalField[i]["BindSource"].ToString(), theDVConditionalField[i]["PreDefined"].ToString(), false);
                                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                    td = 1;
                                }
                                else
                                {
                                    //DIVCustomItem.Controls.Add(new LiteralControl("<td class='form' style='width:50%'"));
                                    DIVCustomItem.Controls.Add(new LiteralControl("<td  class='form' align='center' width='50%'>"));
                                    LoadFieldTypeControl(theDVReq, DIVCustomItem, DRLnkTable["FeatureName"].ToString(), "", "",
                                        theDVConditionalField[i]["FieldId"].ToString(), theDVConditionalField[i]["FieldLabel"].ToString(), theDVConditionalField[i]["FieldName"].ToString(), theDVConditionalField[i]["ControlId"].ToString(),
                                        theDVConditionalField[i]["PdfTableName"].ToString(), theDVConditionalField[i]["BindSource"].ToString(), theDVConditionalField[i]["PreDefined"].ToString(), false);
                                    DIVCustomItem.Controls.Add(new LiteralControl("</td>"));
                                    DIVCustomItem.Controls.Add(new LiteralControl("</tr>"));
                                    td = 0;
                                }
                            }
                        }

                    }
                    //DIVCustomItem.Controls.Add(new LiteralControl("</table>"));
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }

        private void SaveCancel()
        {
            //string Url = string.Format("{0}?PatientId={1}", "../ClinicalForms/frmPatient_Home.aspx", Session["PatientId"].ToString());
            //Response.Redirect(Url);

            //--- For Cancel event, on saving the form ---
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "var ans;\n";
            script += "ans=window.confirm('Care Tracking Form saved successfully. Do you want to close?');\n";
            script += "if (ans==true)\n";
            script += "{\n";
            script += "window.location.href='../ClinicalForms/frmPatient_Home.aspx?PatientId=" + Session["PatientId"] + "';\n";
            script += "}\n";
            script += "</script>\n";
            // ClientScript.RegisterStartupScript(this.GetType(),"confirm", script);
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);

            btnsave.Disabled = true;
            btnComplete.Disabled = true;
            printval = 1;
        }

        #region "GetDynamicControl"
        private int SaveCustomFormData(int dtqty)
        {

            DataTable CEFields = new DataTable();
            CEFields.Columns.Add("TableName", typeof(System.String));
            CEFields.Columns.Add("FieldName", typeof(System.String));
            CEFields.Columns.Add("Value", typeof(System.String));
            CEFields.Columns.Add("OtherDesc", typeof(System.String));
            CEFields.Columns.Add("Priority", typeof(System.Int32));

            DataRow theDR = CEFields.NewRow();
            theDR["TableName"] = "Dtl_PatientTrackingCare";
            theDR["FieldName"] = "DateLastContact";
            theDR["Value"] = txtDateLastContact.Value.ToString();
            theDR["Priority"] = "1";
            CEFields.Rows.Add(theDR);
            theDR = CEFields.NewRow();
            theDR["TableName"] = "Dtl_PatientTrackingCare";
            theDR["FieldName"] = "EmployeeId";
            theDR["Value"] = ddinterviewer.SelectedValue.ToString();
            theDR["Priority"] = "1";
            CEFields.Rows.Add(theDR);
            theDR = CEFields.NewRow();
            theDR["TableName"] = "Dtl_PatientTrackingCare";
            theDR["FieldName"] = "ModuleId";
            theDR["Value"] = Session["TechnicalAreaId"].ToString();
            theDR["Priority"] = "1";
            CEFields.Rows.Add(theDR);
            theDR = CEFields.NewRow();
            theDR["TableName"] = "Dtl_PatientTrackingCare";
            theDR["FieldName"] = "DataQuality";
            theDR["Value"] = dtqty.ToString();
            theDR["Priority"] = "1";
            CEFields.Rows.Add(theDR);

            theDR = CEFields.NewRow();
            theDR["TableName"] = "Dtl_PatientCareEnded";
            theDR["FieldName"] = "MissedAppDate";
            theDR["Value"] = txtMissedAppDate.Value.ToString();
            theDR["Priority"] = "2";
            CEFields.Rows.Add(theDR);
            theDR = CEFields.NewRow();
            theDR["TableName"] = "Dtl_PatientCareEnded";
            theDR["FieldName"] = "CareEnded";
            theDR["Value"] = "1";
            theDR["Priority"] = "2";
            CEFields.Rows.Add(theDR);
            theDR = CEFields.NewRow();
            theDR["TableName"] = "Dtl_PatientCareEnded";
            theDR["FieldName"] = "PatientExitReason";
            theDR["Value"] = cmbPatientExitReason.SelectedValue.ToString();
            theDR["Priority"] = "2";
            CEFields.Rows.Add(theDR);
            theDR = CEFields.NewRow();
            theDR["TableName"] = "Dtl_PatientCareEnded";
            theDR["FieldName"] = "CareEndedDate";
            theDR["Value"] = txtCareEndDate.Value.ToString();
            theDR["Priority"] = "2";
            CEFields.Rows.Add(theDR);

            if (cmbPatientExitReason.SelectedValue.ToString() == "93")
            {
                //theDR = CEFields.NewRow();
                //theDR["TableName"] = "Dtl_PatientCareEnded";
                //theDR["FieldName"] = "DeathReason";
                //theDR["Value"] = cmbDeathReason.SelectedValue.ToString();
                //theDR["Priority"] = "2";
                //CEFields.Rows.Add(theDR);

                theDR = CEFields.NewRow();
                theDR["TableName"] = "Dtl_PatientCareEnded";
                theDR["FieldName"] = "DeathDate";
                theDR["Value"] = txtDeathDate.Value.ToString();
                theDR["Priority"] = "2";
                CEFields.Rows.Add(theDR);
                //if (cmbDeathReason.SelectedValue.ToString() != "0")
                //{
                //    theDR = CEFields.NewRow();
                //    theDR["TableName"] = "mst_patient";
                //    theDR["FieldName"] = "Status";
                //    theDR["Value"] = "1";
                //    theDR["Priority"] = "2";
                //    CEFields.Rows.Add(theDR);

                //}
            }

            CEFields = GetSaveStatement(DIVCustomItem, CEFields);

            DataTable theConField = new DataTable();
            theConField.Columns.Add("TableName", typeof(System.String));
            theConField.Columns.Add("FieldName", typeof(System.String));
            theConField.Columns.Add("Value", typeof(System.String));
            theConField.Columns.Add("OtherDesc", typeof(System.String));
            theConField.Columns.Add("Priority", typeof(System.Int32));

            theConField = GetSaveStatement(PnlConFields, theConField);
            CEFields.Merge(theConField);

            DataView theDV = new DataView(CEFields);
            theDV.Sort = "Priority,TableName asc";
            DataTable theNewDT = theDV.ToTable();
            string TblName = "";
            string theValue = "";
            string theCESQL = "";
            foreach (DataRow theRow in theNewDT.Rows)
            {
                if (TblName != theRow["TableName"].ToString().ToUpper())
                {
                    if (theCESQL != "" && theValue != "")
                    {
                        theCESQL = theCESQL + ")" + theValue + ");";

                        if (TblName.ToUpper() == "DTL_PATIENTTRACKINGCARE")
                        {
                            string theBL = " declare @TrackId int; select @TrackId = ident_current('DTL_PATIENTTRACKINGCARE');";
                            theCESQL = theCESQL + theBL;
                        }
                        else if (TblName.ToUpper() == "DTL_PATIENTCAREENDED")
                        {
                            string theBL = "declare @CareEndId int; select @CareEndId = ident_current('DTL_PATIENTCAREENDED');";
                            theCESQL = theCESQL + theBL;
                        }
                    }
                    if (theRow["TableName"].ToString().ToUpper() == "DTL_PATIENTTRACKINGCARE")
                    {
                        theCESQL = theCESQL + " Insert into [" + theRow["TableName"].ToString() + "](Ptn_Pk,LocationId,UserId,CreateDate";
                        theValue = " values(" + Session["PatientId"].ToString() + "," + Session["AppLocationId"].ToString() + "," + Session["AppUserId"].ToString() + ",getdate()";
                    }
                    else if (theRow["TableName"].ToString().ToUpper() == "DTL_PATIENTCAREENDED")
                    {
                        theCESQL = theCESQL + " Insert into [" + theRow["TableName"].ToString() + "](TrackingId,Ptn_Pk,LocationId,UserId,CreateDate";
                        theValue = " values(@TrackId," + Session["PatientId"].ToString() + "," + Session["AppLocationId"].ToString() + "," + Session["AppUserId"].ToString() + ",getdate()";
                    }
                    else
                    {
                        theCESQL = theCESQL + " Insert into [" + theRow["TableName"].ToString() + "](CareEndedId,Ptn_Pk,LocationId,UserId,CreateDate";
                        theValue = " values(@CareEndId," + Session["PatientId"].ToString() + "," + Session["AppLocationId"].ToString() + "," + Session["AppUserId"].ToString() + ",getdate()";
                    }
                    TblName = theRow["TableName"].ToString().ToUpper();
                }
                if (theCESQL.Contains("," + theRow["FieldName"].ToString()) == true)
                {
                    if (theRow["OtherDesc"].ToString() != "")
                    {
                        DataSet ds = new DataSet();
                        ds = (DataSet)Session["CareEndFields"];
                        DataView theDVOther = new DataView(ds.Tables[4]);
                        theDVOther.RowFilter = "FieldName='" + theRow["FieldName"].ToString() + "'";
                        if (theDVOther.Count >= 1)
                        {
                            theCESQL = theCESQL + ")" + theValue + ");" + " Insert into [" + theRow["TableName"].ToString() + "](CareEndedId,Ptn_Pk,LocationId,UserId,CreateDate, " + theRow["FieldName"].ToString() + "," + theDVOther[0]["OtherDesCol"].ToString() + "";
                            theValue = "";
                            theValue = " values(@CareEndId," + Session["PatientId"].ToString() + "," + Session["AppLocationId"].ToString() + "," + Session["AppUserId"].ToString() + ",getdate()" + ",'" + theRow["Value"].ToString() + "','" + theRow["OtherDesc"].ToString() + "'";
                        }
                    }
                    else
                    {
                        if (theRow["TableName"].ToString().ToUpper() != "DTL_PATIENTCAREENDED")
                        {
                            theCESQL = theCESQL + ")" + theValue + ");" + " Insert into [" + theRow["TableName"].ToString() + "](CareEndedId,Ptn_Pk,LocationId,UserId,CreateDate, " + theRow["FieldName"].ToString() + "";
                            theValue = "";
                            theValue = " values(@CareEndId," + Session["PatientId"].ToString() + "," + Session["AppLocationId"].ToString() + "," + Session["AppUserId"].ToString() + ",getdate()" + ",'" + theRow["Value"].ToString() + "'";
                        }
                    }

                    //theCESQL = theCESQL + ")" + theValue + ");" + " Insert into [" + theRow["TableName"].ToString() + "](CareEndedId,Ptn_Pk,LocationId,UserId,CreateDate, " + theRow["FieldName"].ToString() + "";
                    //theValue = "";
                    //theValue = " values(@CareEndId," + Session["PatientId"].ToString() + "," + Session["AppLocationId"].ToString() + "," + Session["AppUserId"].ToString() + ",getdate()" + ",'" + theRow["Value"].ToString() + ",'" + theRow["OtherDesc"].ToString() + "'";
                }
                else
                {
                    if (theRow["Value"].ToString() != "")
                    {
                        theCESQL = theCESQL + "," + theRow["FieldName"].ToString();
                        theValue = theValue + ",'" + theRow["Value"].ToString() + "'";
                    }
                }
            }

            IContactCare CCControl = (IContactCare)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BContactCare,BusinessProcess.Scheduler");
            DataSet theCheckDS = CCControl.CheckModuleTrackingStatus(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["TechnicalAreaId"]));

            //if (theCESQL.Contains(theValue) == true)
            //    theCESQL = theCESQL + ")" + theValue + ");";
            //else
            //{
            theCESQL = theCESQL + ")" + theValue + ");";
            //}

            if (Convert.ToInt32(cmbPatientExitReason.SelectedValue) == 93 || (Convert.ToInt32(theCheckDS.Tables[0].Rows[0][0]) == Convert.ToInt32(theCheckDS.Tables[1].Rows[0][0])))
            {
                theCESQL = theCESQL + " Update mst_patient set status='" + 1 + "' where ptn_pk= '" + Session["PatientId"].ToString() + "'";
            }

            theCESQL = theCESQL + "exec Pr_Scheduler_PatientCareEnded @TrackId," + Session["PatientId"].ToString() + "";

            //  int i = 0;
            int row = 0;

            ICareEnded CEControl;
            try
            {
                CEControl = (ICareEnded)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BCareEnded,BusinessProcess.Scheduler");
                row = CEControl.SaveGetDynamicControlDatat(theCESQL, Session["PatientId"].ToString(), txtCareEndDate.Value.ToString());
                return row;
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
                throw;
            }
        }

        private void SetBusinessrule(Control theLabel, Control theControl, Int32 FieldID)
        {
            DataTable theDT = ((DataSet)Session["CareEndFields"]).Tables[3];

            DataView theDVBus = new DataView(theDT.Copy());

            theDVBus.RowFilter = "FieldId=" + FieldID.ToString();
            theDVBus.Sort = "FieldId asc";
            theDT = theDVBus.ToTable();

            string theOnKeyUp = "";
            foreach (DataRow DR in theDT.Rows)
            {
                if (DR["Name"].ToString() == "Required Field")
                {
                    if (Convert.ToInt32(DR["FieldId"]) == FieldID && Convert.ToInt32(DR["ControlId"]) == 1 && DR["Name"].ToString() == "Required Field")
                    {
                        if (hidID.Value == "")
                        {
                            hidID.Value = theControl.ID;
                        }
                        else
                        {
                            hidID.Value = hidID.Value + "%" + theControl.ID;
                        }
                        ((Label)theLabel).CssClass = ((Label)theLabel).CssClass + " required";
                        ((Label)theLabel).Text = "*" + ((Label)theLabel).Text;
                    }
                    if (Convert.ToInt32(DR["FieldId"]) == FieldID && Convert.ToInt32(DR["ControlId"]) == 2 && DR["Name"].ToString() == "Required Field")
                    {
                        if (hidID.Value == "")
                        {
                            hidID.Value = theControl.ID;
                        }
                        else
                        {
                            hidID.Value = hidID.Value + "%" + theControl.ID;
                        }
                        ((Label)theLabel).CssClass = ((Label)theLabel).CssClass + " required";
                        ((Label)theLabel).Text = "*" + ((Label)theLabel).Text;
                    }

                    if (Convert.ToInt32(DR["FieldId"]) == FieldID && Convert.ToInt32(DR["ControlId"]) == 3 && DR["Name"].ToString() == "Required Field")
                    {
                        if (hidID.Value == "")
                        {
                            hidID.Value = theControl.ID;
                        }
                        else
                        {
                            hidID.Value = hidID.Value + "%" + theControl.ID;
                        }
                        ((Label)theLabel).CssClass = ((Label)theLabel).CssClass + " required";
                        ((Label)theLabel).Text = "*" + ((Label)theLabel).Text;
                    }

                    if (Convert.ToInt32(DR["FieldId"]) == FieldID && Convert.ToInt32(DR["ControlId"]) == 4 && DR["Name"].ToString() == "Required Field")
                    {
                        if (hidID.Value == "")
                        {
                            hidID.Value = theControl.ID;
                        }
                        else
                        {
                            hidID.Value = hidID.Value + "%" + theControl.ID;
                        }
                        ((Label)theLabel).CssClass = ((Label)theLabel).CssClass + " required";
                        ((Label)theLabel).Text = "*" + ((Label)theLabel).Text;
                    }

                    if (Convert.ToInt32(DR["FieldId"]) == FieldID && Convert.ToInt32(DR["ControlId"]) == 5 && DR["Name"].ToString() == "Required Field")
                    {
                        if (hidID.Value == "")
                        {
                            hidID.Value = theControl.ID;
                        }
                        else
                        {
                            hidID.Value = hidID.Value + "%" + theControl.ID;
                        }
                        ((Label)theLabel).CssClass = ((Label)theLabel).CssClass + " required";
                        ((Label)theLabel).Text = "*" + ((Label)theLabel).Text;
                    }
                    if (Convert.ToInt32(DR["FieldId"]) == FieldID && Convert.ToInt32(DR["ControlId"]) == 6 && DR["Name"].ToString() == "Required Field")
                    {
                        if (hidradio.Value == "")
                        {
                            hidradio.Value = theControl.ID;
                        }
                        else
                        {
                            hidradio.Value = hidradio.Value + "%" + theControl.ID;
                        }

                        ((Label)theLabel).CssClass = ((Label)theLabel).CssClass + " required";
                        ((Label)theLabel).Text = "*" + ((Label)theLabel).Text;
                    }

                    if (Convert.ToInt32(DR["FieldId"]) == FieldID && Convert.ToInt32(DR["ControlId"]) == 7 && DR["Name"].ToString() == "Required Field")
                    {
                        if (hidchkbox.Value == "")
                        {
                            hidchkbox.Value = theControl.ID;
                        }
                        else
                        {
                            hidchkbox.Value = hidchkbox.Value + "%" + theControl.ID;
                        }
                        ((Label)theLabel).CssClass = ((Label)theLabel).CssClass + " required";
                        ((Label)theLabel).Text = "*" + ((Label)theLabel).Text;
                    }

                    if (Convert.ToInt32(DR["FieldId"]) == FieldID && Convert.ToInt32(DR["ControlId"]) == 8 && DR["Name"].ToString() == "Required Field")
                    {
                        if (hidID.Value == "")
                        {
                            hidID.Value = theControl.ID;
                        }
                        else
                        {
                            hidID.Value = hidID.Value + "%" + theControl.ID;
                        }

                        ((Label)theLabel).CssClass = ((Label)theLabel).CssClass + " required";
                        ((Label)theLabel).Text = "*" + ((Label)theLabel).Text;
                    }

                    if (Convert.ToInt32(DR["FieldId"]) == FieldID && Convert.ToInt32(DR["ControlId"]) == 9 && DR["Name"].ToString() == "Required Field")
                    {
                        if (hidcheckbox.Value == "")
                        {
                            hidcheckbox.Value = theControl.ID;

                            //x.GetType().ToString() == "System.Web.UI.WebControls.TextBox"

                            foreach (Control x in PnlConFields.Controls)
                            {
                                if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel")
                                {
                                    if (((Panel)x).Enabled == false)
                                    {
                                        hidcheckbox.Value = "";
                                    }
                                }
                            }
                            foreach (Control x in DIVCustomItem.Controls)
                            {
                                if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel")
                                {
                                    if (((Panel)x).Enabled == false)
                                    {
                                        hidcheckbox.Value = "";
                                    }
                                }
                            }
                        }
                        else
                        {
                            hidcheckbox.Value = hidcheckbox.Value + "%" + theControl.ID;
                        }
                        ((Label)theLabel).CssClass = ((Label)theLabel).CssClass + " required";
                        ((Label)theLabel).Text = "*" + ((Label)theLabel).Text;
                    }
                }
                else if (DR["Name"].ToString() == "Data Quality Check")
                {
                    if (Convert.ToInt32(DR["FieldId"]) == FieldID && Convert.ToInt32(DR["ControlId"]) == 1 && DR["Name"].ToString() == "Data Quality Check")
                    {
                        if (hidIDQty.Value == "")
                        {
                            hidIDQty.Value = theControl.ID;
                        }
                        else
                        {
                            hidIDQty.Value = hidIDQty.Value + "%" + theControl.ID;
                        }

                        //((Label)theLabel).CssClass = ((Label)theLabel).CssClass + " required";

                        //foreach (Control x in PnlConFields.Controls)
                        //{
                        //    if (x.GetType().ToString() == "System.Web.UI.WebControls.TextBox")
                        //    {
                        //        if (((TextBox)x).Enabled == false)
                        //        {
                        //            hidIDQty.Value = "";

                        //        }
                        //    }
                        //}
                        //foreach (Control x in DIVCustomItem.Controls)
                        //{
                        //    if (x.GetType().ToString() == "System.Web.UI.WebControls.TextBox")
                        //    {
                        //        if (((TextBox)x).Enabled == false)
                        //        {
                        //            hidIDQty.Value = "";

                        //        }
                        //    }
                        //}
                    }
                    if (Convert.ToInt32(DR["FieldId"]) == FieldID && Convert.ToInt32(DR["ControlId"]) == 2 && DR["Name"].ToString() == "Data Quality Check")
                    {
                        if (hidIDQty.Value == "")
                        {
                            hidIDQty.Value = theControl.ID;
                        }
                        else
                        {
                            hidIDQty.Value = hidIDQty.Value + "%" + theControl.ID;
                        }
                    }

                    if (Convert.ToInt32(DR["FieldId"]) == FieldID && Convert.ToInt32(DR["ControlId"]) == 3 && DR["Name"].ToString() == "Data Quality Check")
                    {
                        if (hidIDQty.Value == "")
                        {
                            hidIDQty.Value = theControl.ID;
                        }
                        else
                        {
                            hidIDQty.Value = hidIDQty.Value + "%" + theControl.ID;
                        }
                    }

                    if (Convert.ToInt32(DR["FieldId"]) == FieldID && Convert.ToInt32(DR["ControlId"]) == 4 && DR["Name"].ToString() == "Data Quality Check")
                    {
                        if (hidIDQty.Value == "")
                        {
                            hidIDQty.Value = theControl.ID;
                        }
                        else
                        {
                            hidIDQty.Value = hidIDQty.Value + "%" + theControl.ID;
                        }
                    }

                    if (Convert.ToInt32(DR["FieldId"]) == FieldID && Convert.ToInt32(DR["ControlId"]) == 5 && DR["Name"].ToString() == "Data Quality Check")
                    {
                        if (hidIDQty.Value == "")
                        {
                            hidIDQty.Value = theControl.ID;
                        }
                        else
                        {
                            hidIDQty.Value = hidIDQty.Value + "%" + theControl.ID;
                        }
                    }
                    if (Convert.ToInt32(DR["FieldId"]) == FieldID && Convert.ToInt32(DR["ControlId"]) == 6 && DR["Name"].ToString() == "Data Quality Check")
                    {
                        if (hidradioQty.Value == "")
                        {
                            hidradioQty.Value = theControl.ID;
                        }
                        else
                        {
                            hidradioQty.Value = hidradioQty.Value + "%" + theControl.ID;
                        }
                    }

                    if (Convert.ToInt32(DR["FieldId"]) == FieldID && Convert.ToInt32(DR["ControlId"]) == 7 && DR["Name"].ToString() == "Data Quality Check")
                    {
                        if (hidchkboxQty.Value == "")
                        {
                            hidchkboxQty.Value = theControl.ID;
                        }
                        else
                        {
                            hidchkboxQty.Value = hidchkboxQty.Value + "%" + theControl.ID;
                        }
                    }

                    if (Convert.ToInt32(DR["FieldId"]) == FieldID && Convert.ToInt32(DR["ControlId"]) == 8 && DR["Name"].ToString() == "Data Quality Check")
                    {
                        if (hidIDQty.Value == "")
                        {
                            hidIDQty.Value = theControl.ID;
                        }
                        else
                        {
                            hidIDQty.Value = hidIDQty.Value + "%" + theControl.ID;
                        }
                    }

                    if (Convert.ToInt32(DR["FieldId"]) == FieldID && Convert.ToInt32(DR["ControlId"]) == 9 && DR["Name"].ToString() == "Data Quality Check")
                    {
                        if (hidcheckboxQty.Value == "")
                        {
                            hidcheckboxQty.Value = theControl.ID;
                        }
                        else
                        {
                            hidcheckboxQty.Value = hidcheckboxQty.Value + "%" + theControl.ID;
                        }
                    }
                }
                else
                {
                    if (Convert.ToInt32(DR["FieldId"]) == FieldID && Convert.ToInt32(DR["Id"]) == 2)
                    {
                        theOnKeyUp = theOnKeyUp + ";checkMax('" + ((TextBox)theControl).ClientID + "', '" + ((TextBox)theControl).Text + "', '" + DR["value"].ToString() + "')";
                        theOnKeyUp = theOnKeyUp + ";chkDecimal('" + ((TextBox)theControl).ClientID + "')";
                        ((TextBox)theControl).Attributes.Add("onkeyup", theOnKeyUp);
                    }

                    if (Convert.ToInt32(DR["FieldId"]) == FieldID && Convert.ToInt32(DR["Id"]) == 3)
                    {
                        theOnKeyUp = theOnKeyUp + ";checkMin('" + ((TextBox)theControl).ClientID + "', '" + ((TextBox)theControl).Text + "', '" + DR["value"].ToString() + "');";
                        theOnKeyUp = theOnKeyUp + ";chkNumber('" + ((TextBox)theControl).ClientID + "');";
                        ((TextBox)theControl).Attributes.Add("onkeyup", theOnKeyUp);
                    }
                }

                //return false;
            }
        }
        private Boolean ValidationFormData()
        {
            if (txtDateLastContact.Value.ToString() == "")
            {
                IQCareMsgBox.Show("DateLastActualContact", this);
                txtDateLastContact.Focus();
                return false;
            }
            if (cmbPatientExitReason.SelectedValue.ToString() == "0")
            {
                IQCareMsgBox.Show("PatientExitReason", this);
                cmbPatientExitReason.Focus();
                return false;
            }

            if (cmbPatientExitReason.SelectedValue.ToString() == "93")
            {
                if (txtDeathDate.Value.ToString() == "")
                {
                    IQCareMsgBox.Show("Deathdate", this);
                    txtDeathDate.Focus();
                    return false;
                }
            }

            if (ddinterviewer.SelectedValue.ToString() == "0")
            {
                IQCareMsgBox.Show("Signature", this);
                ddinterviewer.Focus();
                return false;
            }

            if (txtCareEndDate.Value.ToString() == "")
            {
                IQCareMsgBox.Show("DateCareEnded", this);
                txtCareEndDate.Focus();
                return false;
            }

            if ((Convert.ToDateTime(txtCareEndDate.Value)) < (Convert.ToDateTime(txtDateLastContact.Value)))
            {
                IQCareMsgBox.Show("DateCareEndedactual", this);
                txtCareEndDate.Focus();
                return false;
            }

            return true;
        }
        # endregion
    }
}
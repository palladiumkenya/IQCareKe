using Application.Common;
using Application.Presentation;
using Entities.Administration;
using Interface.Administration;
using Interface.Clinical;
using Interface.Security;
using IQCare.Web.UILogic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities.PatientCore;

namespace IQCare.Web.Patient
{
    public partial class AddTechnicalArea : BasePage
    {
        /// <summary>
        /// The object factory parameter
        /// </summary>
        string ObjFactoryParameter = "BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical";
        /// <summary>
        /// The get valuefrom ht
        /// </summary>
        Hashtable GetValuefromHT;
        /// <summary>
        /// The flag
        /// </summary>
        int flag = 0;
        bool showRevisit = false;
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("PanelPatiInfo") as Panel).Visible = false;
            appDateimg2.Attributes.Add("onclick", "w_displayDatePicker('" + txtenrollmentDate.ClientID + "');");
            imgDtReEnroll.Attributes.Add("onclick", "w_displayDatePicker('" + txtReEnrollmentDate.ClientID + "');");
            txtenrollmentDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtenrollmentDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3'); isCheckValidDate('" + Application["AppCurrentDate"] + "', '" + txtenrollmentDate.ClientID + "', '" + txtenrollmentDate.ClientID + "');");
            txtReEnrollmentDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtReEnrollmentDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3'); isCheckValidDate('" + Application["AppCurrentDate"] + "', '" + txtReEnrollmentDate.ClientID + "', '" + txtReEnrollmentDate.ClientID + "');");
            Master.ExecutePatientLevel = false;
            //   txtenrollmentDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3');");
            //Ajax.Utility.RegisterTypeForAjax(typeof(frmAddTechnicalArea));
            appDateimg2.Visible = true;
            txtenrollmentDate.Disabled = false;
            trReEnroll.Visible = false;
            btnReEnollPatient.Visible = false;
            if (!IsPostBack)
            {

                LoadPatientDetail();
                BindDropdown();
                ClientScript.RegisterStartupScript(this.GetType(), "Changing", "<script>fnChange();</script>");
                //Automatically sets the technical area that you are in and cannot be changed

                tHeading.InnerText = "Service Enrollment";
                ListItem item = null;
                if (CurrentSession.Current != null && CurrentSession.Current.CurrentServiceArea != null)
                {
                    item = ddlTecharea.Items.FindByValue(CurrentSession.Current.CurrentServiceArea.Id.ToString());
                }
                else if (Request.QueryString["mod"] != null && Request.QueryString["mod"] != "0")
                {

                    //btnContinue.Visible = false;
                    item = ddlTecharea.Items.FindByValue(Request.QueryString["mod"].ToString());
                }
                if (item != null)
                {
                    ddlTecharea.SelectedValue = item.Value;
                    ddlTecharea.Enabled = false;
                    flag = 1;
                    LoadModuleIdentifiers(Convert.ToInt32(ddlTecharea.SelectedValue));
                }
                if (CurrentSession.Current.Facility.PaperLess==false )
                {
                    ddlTecharea.Enabled = true;
                }
                
                base.Session["TechIdentifier"] = null;
            }
            else
            {
                if (Convert.ToInt32(ddlTecharea.SelectedValue) > 0)
                    LoadModuleIdentifiers(Convert.ToInt32(ddlTecharea.SelectedValue.ToString()));
            }
        }

        /// <summary>
        /// Handles the PreRender event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            btnRevisit.Visible = this.showRevisit && this.IsRecord; ;
            btnSaveContinue.Visible = btnContinue.Visible = (Convert.ToInt32(ddlTecharea.SelectedValue) > 0);
        }
        /// <summary>
        /// Loads the patient detail.
        /// </summary>
        private void LoadPatientDetail()
        {
            ViewState["AutoPopulated"] = "False";
           


            if (PatientId < 1 || Session["htPtnRegParameter"] != null)
            {
                GetValuefromHT = (Hashtable)Session["htPtnRegParameter"];
                lblname.Text = GetValuefromHT["FirstName"].ToString() + ' ' + GetValuefromHT["MiddleName"].ToString() + ' ' + GetValuefromHT["LastName"].ToString();
                string Gender = GetValuefromHT["Gender"].ToString() == "16" ? "Male" : "Female";
                Session["PatientSex"] = Gender;
                lblsex.Text = Gender;
                lbldob.Text = GetValuefromHT["DOB"].ToString();
                //lblIQno.Text = theDS.Tables[0].Rows[0]["IQNumber"].ToString();
                DateTime today = DateTime.Today;
                int age = today.Year - Convert.ToDateTime(GetValuefromHT["DOB"].ToString()).Year;
                Session["PatientAge"] = age;
                int ageMonth = today.Month - Convert.ToDateTime(GetValuefromHT["DOB"].ToString()).Month;
                Session["PatientAgeMonths"] = ageMonth;
                ViewState["RegistrationDate"] = GetValuefromHT["RegistrationDate"].ToString();
                //lblIQno.Text = theDS.Tables[0].Rows[0]["IQNumber"].ToString();
                // btnContinue.Visible = false;
                //lblContinue.Visible = false;
            }
            else
            {
                PatientService service = new PatientService(PatientId);

                Entities.PatientCore.Patient patient = service.CurrentPatient;

               // IPatientRegistration ptnMgr = (IPatientRegistration)ObjectFactory.CreateInstance(ObjFactoryParameter);

              //  DataSet theDS = ptnMgr.GetPatientRegistration(patientID, 12);
                if (patient != null)
                {
                    lblname.Text = patient.FullName;
                    //theDS.Tables[0].Rows[0]["Firstname"].ToString() + ' ' + theDS.Tables[0].Rows[0]["Middlename"].ToString() + ' ' + theDS.Tables[0].Rows[0]["Lastname"].ToString();
                    Session["PatientSex"]= lblsex.Text = patient.Sex;
                    //theDS.Tables[0].Rows[0]["sex"].ToString();

                    lbldob.Text = patient.DateOfRegistration.ToString("dd-MMM-yyyy");

                    //theDS.Tables[0].Rows[0]["dob"].ToString();
                    lblIQno.Text = patient.IQNumber;
                    //theDS.Tables[0].Rows[0]["IQNumber"].ToString();
                    //   Session["PatientSex"] = theDS.Tables[0].Rows[0]["sex"].ToString();
                    Session["PatientAge"] = patient.Age;
                        //theDS.Tables[0].Rows[0]["AGE"].ToString(); // +"." + theDS.Tables[0].Rows[0]["AgeInMonths"].ToString();
                    ViewState["RegistrationDate"] = patient.DateOfRegistration;
                }
                //if (theDS.Tables[2].Rows.Count > 0)
                //{
                //        ViewState["RegistrationDate"] = theDS.Tables[2].Rows[0]["VisitDate"].ToString();                   
                   
                //}
            }


        }
        double PatientAge
        {
            get
            {
                return Convert.ToDouble(Session["PatientAge"]);
            }
        }
        string PatientSex
        {
            get
            {
                return Session["PatientSex"].ToString();
            }
        }


        /// <summary>
        /// Binds the dropdown.
        /// </summary>
        protected void BindDropdown()
        {

            List<ServiceArea> dt = PatientService.GetModuleForPatientEnrollment(CurrentSession.Current.Facility.Modules, PatientAge, PatientSex);

            if (dt.Count > 0)
            {

                ddlTecharea.DataSource = dt;
                ddlTecharea.DataTextField = "Name";
                ddlTecharea.DataValueField = "Id";
                ddlTecharea.DataBind();
                ddlTecharea.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        /// <summary>
        /// Fields the validation.
        /// </summary>
        /// <returns></returns>
        private bool FieldValidation()
        {
            //IIQCareSystem IQCareSecurity = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
            DateTime theCurrentDate = SystemSetting.SystemDate;
                //(DateTime)IQCareSecurity.SystemDate();
            IQCareUtils theUtils = new IQCareUtils();
            DateTime RegistrationDate = Convert.ToDateTime(ViewState["RegistrationDate"]);
            int textblankstatus = GetBlankTextboxesstatus(1);

            if (txtenrollmentDate.Value == "")
            {
                // MsgBuilder theBuilder = new MsgBuilder();
                //theBuilder.DataElements["Control"] = "Enrollment Date";

                IQCareMsgBox.NotifyAction(IQCareMsgBox.GetMessage("MissingEnrollmentDate", null), "Patient Enrollment", false, this, "");
                txtenrollmentDate.Focus();
                return false;

                // txtenrollmentDate.Focus();
                //return false;
            }
            DateTime theEnrolDate = Convert.ToDateTime(theUtils.MakeDate(txtenrollmentDate.Value));
            DateTime theReEnrolDate = Convert.ToDateTime(theUtils.MakeDate(txtReEnrollmentDate.Value));
            if (theEnrolDate > theCurrentDate)
            {

                IQCareMsgBox.NotifyAction(IQCareMsgBox.GetMessage("EnrolDate", null), "Patient Enrollment", false, this, "");
                //IQCareMsgBox.Show("EnrolDate", this);
                txtenrollmentDate.Focus();
                return false;
            }
            if (theEnrolDate < RegistrationDate)
            {
                //  IQCareMsgBox.Show("RegistrationDate", this);

                IQCareMsgBox.NotifyAction(IQCareMsgBox.GetMessage("RegistrationDate", null), "Patient Enrollment", false, this, "");
                txtenrollmentDate.Focus();
                return false;
            }
            if (ViewState["CareEndedDate"] != null)
            {
                if (theReEnrolDate > theCurrentDate)
                {
                    //IQCareMsgBox.Show("ReEnrolDate", this);

                    IQCareMsgBox.NotifyAction(IQCareMsgBox.GetMessage("ReEnrolDate", null), "Patient Enrollment", false, this, "");
                    txtReEnrollmentDate.Focus();
                    return false;
                }

                if (theReEnrolDate < (DateTime)ViewState["CareEndedDate"])
                {
                    IQCareMsgBox.NotifyAction(IQCareMsgBox.GetMessage("RegistrationCareEndDate", null), "Patient Enrollment", false, this, "");
                    // IQCareMsgBox.Show("RegistrationCareEndDate", this);
                    txtReEnrollmentDate.Focus();
                    return false;
                }
            }

            if (textblankstatus.ToString() == "0")
            {

                IQCareMsgBox.NotifyAction(IQCareMsgBox.GetMessage("IdentifierRequired", null), "Patient Enrollment", false, this, "");

                return false;
            }

            return true;
        }

        /// <summary>
        /// Handles the Click event of the btnSaveContinue control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void btnSaveContinue_Click(object sender, EventArgs e)
        {

            if (FieldValidation() == false)
            {
                return;
            }
            SavePatientRegistration();
            if (InsertUpdateIdentifiers() == true)
            {
                Session["status"] = "Add";
                IQCareMsgBox.NotifyAction("Service Registration Form saved successfully.", "Patient Registration", false, this, string.Format("javascript:window.location.href='{0}'", this.RedirectUrl));
            }
        }

        string RedirectUrl
        {
            get
            {
                int patientID = Convert.ToInt32(Session["PatientId"]);
                if (Session["TechnicalAreaName"].ToString().Trim() == "Records")
                {

                    string theUrl = string.Format("/Patient/FindAdd.aspx?srvNm={0}&mod={1}", "Records", 0);
                    string strSrvName = ddlTecharea.SelectedItem.Text;
                    string moduleid = ddlTecharea.SelectedItem.Value;

                    Utility objUtil = new Utility();
                    string returnUrl = objUtil.Encrypt((theUrl));

                    string theOrdScript;

                    theOrdScript = string.Format("./../Queue/PatientWaitingList.aspx?srvNm={0}&mod={1}&PID={2}&hashtag={3}", strSrvName, moduleid, patientID, returnUrl);
                    return theOrdScript;


                }
                else
                {
                    //HttpContext.Current.ApplicationInstance.CompleteRequest();
                    return ("./../ClinicalForms/frmPatient_Home.aspx?PatientId=" + patientID);
                }
            }
        }
        /// <summary>
        /// Redirects this instance.
        /// </summary>
        void Redirect()
        {
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            Response.Redirect(this.RedirectUrl, true);


        }
        /// <summary>
        /// Handles the Click event of the btnContinue control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void btnContinue_Click(object sender, EventArgs e)
        {
            if (ddlTecharea.SelectedIndex > 0)
            {
                Session["TechnicalAreaId"] = ddlTecharea.SelectedValue.ToString();
            }
            else
            {
                IQCareMsgBox.Show("SelectTechnicalArea", this);
                return;
            }

            if (txtenrollmentDate.Value == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Enrollment Date";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtenrollmentDate.Focus();
                return;
            }
            int textcontinueblankstatus = GetBlankTextboxesstatus(0);
            if (textcontinueblankstatus.ToString() == "0")
            {
                IQCareMsgBox.Show("IdentifierRequired", this);
                return;
            }
            if (ViewState["Enrolldate"].ToString() == "")
            {
                IQCareMsgBox.Show("TechAreaNotRegistered", this);
                return;
            }
            //VY added in case of records this would go back to find add patients

            this.Redirect();


        }
        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlTecharea control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void ddlTecharea_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (Convert.ToInt32(ddlTecharea.SelectedValue) > 0)
            {
                try
                {
                    txtenrollmentDate.Value = "";
                    flag = 1;
                    LoadModuleIdentifiers(Convert.ToInt32(ddlTecharea.SelectedValue.ToString()));
                }
                catch (Exception err)
                {

                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["MessageText"] = err.Message.ToString();
                    IQCareMsgBox.Show("#C1", theBuilder, this);
                }
                //Revist Date.
                try
                {
                    if (this.IsRecord)
                    {
                        DateTime now = DateTime.Now;
                        DateTime? dateEnrolled = null;
                        DataTable dtEnrollment = this.GetPatientEnrollement(this.PatientId, this.LocationId);
                        dtEnrollment.DefaultView.Sort = "EnrollmentDate Asc";
                        bool isEnrolled = false;
                        if (dtEnrollment.Rows.Count > 0)
                        {
                            DataRow[] dr = dtEnrollment.Select("CareStatus='Active' And ModuleID = " + ddlTecharea.SelectedValue.ToString());
                            if (null != dr && dr.Length > 0)
                            {
                                isEnrolled = true;
                                dateEnrolled = Convert.ToDateTime(dr[0]["EnrollmentDate"]);
                            }
                            else
                            {
                                dr = dtEnrollment.Select("CareStatus='Active'");
                                if (null != dr && dr.Length > 0)
                                {
                                    isEnrolled = true;
                                    dateEnrolled = Convert.ToDateTime(dr[0]["EnrollmentDate"]);
                                }
                            }
                        }


                        IPatientHome pMGR;
                        pMGR = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
                        DataTable dt = pMGR.GetPatientRevisits(this.PatientId, this.LocationId, true);

                        if (dt.Rows.Count > 0)
                        {
                            DateTime LastRevisit = Convert.ToDateTime((dt.Rows[0]["VisitDate"]));
                            if (LastRevisit.AddHours(this.RevistHrsAllowance) < now && isEnrolled) this.showRevisit = true;
                        }
                        else
                        {
                            showRevisit = isEnrolled;
                        }
                    }
                }
                catch { }
            }
        }


        /// <summary>
        /// Loads the field type control.
        /// </summary>
        /// <param name="ControlID">The control identifier.</param>
        /// <param name="FieldName">Name of the field.</param>
        private void LoadFieldTypeControl(string ControlID, string FieldName)
        {
            string fieldlabel = "";
            DataView theLabelDV = new DataView((DataTable)ViewState["ModuleIdentifiers"]);
            theLabelDV.RowFilter = "FieldName='" + FieldName + "'";
            fieldlabel = theLabelDV[0]["FieldLabel"].ToString();

            if (ControlID == "1") ///SingleLine Text Box
            {
                pnlIdentFields.Controls.Add(new LiteralControl("<table width='100%'>"));
                pnlIdentFields.Controls.Add(new LiteralControl("<tr>"));
                pnlIdentFields.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));

                pnlIdentFields.Controls.Add(new LiteralControl("<label align='center'>" + fieldlabel + " :</label>"));

                pnlIdentFields.Controls.Add(new LiteralControl("</td>"));
                pnlIdentFields.Controls.Add(new LiteralControl("<td style='width:60%'>"));
                TextBox theSingleText = new TextBox();
                theSingleText.ID = "txt" + FieldName;
                theSingleText.Width = 180;
                theSingleText.MaxLength = 50;
                theSingleText.CssClass = "form-control";
                pnlIdentFields.Controls.Add(theSingleText);
                BindTextboxes(FieldName);
                theSingleText.Attributes.Add("onKeyup", "chkAlphaNumericString('" + theSingleText.ClientID + "')");
                pnlIdentFields.Controls.Add(new LiteralControl("</td>"));
                pnlIdentFields.Controls.Add(new LiteralControl("</tr>"));
                pnlIdentFields.Controls.Add(new LiteralControl("</table>"));

            }
            else if (ControlID == "2") ///DecimalTextBox
            {

                pnlIdentFields.Controls.Add(new LiteralControl("<table width='100%'>"));
                pnlIdentFields.Controls.Add(new LiteralControl("<tr>"));
                pnlIdentFields.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));

                pnlIdentFields.Controls.Add(new LiteralControl("<label align='center'>" + fieldlabel + " :</label>"));

                pnlIdentFields.Controls.Add(new LiteralControl("</td>"));
                pnlIdentFields.Controls.Add(new LiteralControl("<td style='width:60%'>"));
                TextBox theSingleDecimalText = new TextBox();
                theSingleDecimalText.ID = "txt" + FieldName;
                theSingleDecimalText.CssClass = "form-control";
                theSingleDecimalText.Width = 180;
                theSingleDecimalText.MaxLength = 50;
                pnlIdentFields.Controls.Add(theSingleDecimalText);
                BindTextboxes(FieldName);
                theSingleDecimalText.Attributes.Add("onKeyup", "chkDecimal('" + theSingleDecimalText.ClientID + "')");
                pnlIdentFields.Controls.Add(new LiteralControl("</td>"));
                pnlIdentFields.Controls.Add(new LiteralControl("</tr>"));
                pnlIdentFields.Controls.Add(new LiteralControl("</table>"));

            }
            else if (ControlID == "3")   /// Numeric (Integer)
            {
                pnlIdentFields.Controls.Add(new LiteralControl("<table width='100%'>"));
                pnlIdentFields.Controls.Add(new LiteralControl("<tr>"));
                pnlIdentFields.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));

                pnlIdentFields.Controls.Add(new LiteralControl("<label align='center'>" + fieldlabel + " :</label>"));

                pnlIdentFields.Controls.Add(new LiteralControl("</td>"));
                pnlIdentFields.Controls.Add(new LiteralControl("<td style='width:60%'>"));
                TextBox theNumberText = new TextBox();
                theNumberText.ID = "txt" + FieldName;
                theNumberText.CssClass = "form-control";
                theNumberText.Width = 180;
                theNumberText.MaxLength = 50;
                pnlIdentFields.Controls.Add(theNumberText);
                BindTextboxes(FieldName);
                theNumberText.Attributes.Add("onKeyup", "chkNumeric('" + theNumberText.ClientID + "')");
                pnlIdentFields.Controls.Add(new LiteralControl("</td>"));
                pnlIdentFields.Controls.Add(new LiteralControl("</tr>"));
                pnlIdentFields.Controls.Add(new LiteralControl("</table>"));
            }
            else if (ControlID == "17") ///SingleLine Text Box
            {
                ViewState["AutoPopulated"] = "True";
                pnlIdentFields.Controls.Add(new LiteralControl("<table  width='100%'>"));
                pnlIdentFields.Controls.Add(new LiteralControl("<tr>"));
                pnlIdentFields.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));

                pnlIdentFields.Controls.Add(new LiteralControl("<label align='center'>" + fieldlabel + " :</label>"));

                pnlIdentFields.Controls.Add(new LiteralControl("</td>"));
                pnlIdentFields.Controls.Add(new LiteralControl("<td style='width:60%'>"));
                TextBox theSingleText = new TextBox();
                theSingleText.ID = "txt" + FieldName;
                theSingleText.CssClass = "form-control";
                theSingleText.ReadOnly = true;
                theSingleText.BackColor = System.Drawing.Color.LightGray;
                theSingleText.Width = 180;
                theSingleText.MaxLength = 50;
                pnlIdentFields.Controls.Add(theSingleText);
                BindTextboxes(FieldName);
                theSingleText.Attributes.Add("onKeyup", "chkAlphaNumericString('" + theSingleText.ClientID + "')");
                pnlIdentFields.Controls.Add(new LiteralControl("</td>"));
                pnlIdentFields.Controls.Add(new LiteralControl("</tr>"));
                pnlIdentFields.Controls.Add(new LiteralControl("</table>"));

            }
        }

        /// <summary>
        /// Binds the textboxes.
        /// </summary>
        /// <param name="fieldname">The fieldname.</param>
        private void BindTextboxes(string fieldname)
        {
            DataTable DTPatientIdents = (DataTable)ViewState["PatientIdentdata"];
            foreach (Control x in pnlIdentFields.Controls)
            {
                if (x.GetType() == typeof(TextBox))
                {

                    if ("txt" + fieldname.ToString() == ((TextBox)x).ID)
                    {
                        if (DTPatientIdents.Rows.Count > 0)
                        {
                            //((TextBox)x).Text = "abc";
                            ((TextBox)x).Text = Convert.ToString(DTPatientIdents.Rows[0][fieldname]);
                            if (Convert.ToInt32(Session["IdentifierFlag"]) == 0 && ((TextBox)x).Text != "")
                            {
                                ((TextBox)x).Enabled = false;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the blank textboxesstatus.
        /// </summary>
        /// <param name="flag">The flag.</param>
        /// <returns></returns>
        private int GetBlankTextboxesstatus(int flag)
        {
            //flag=0 for continue & flag=1 for save and continue 
            int Blankstatus = 0;

            DataTable DTPatientIndefiers = (DataTable)ViewState["PatientIdentdata"];
            // DataRow DTPatientIndefiersRow = DTPatientIndefiers.Rows[0];
            DataTable DTModuleIdents = (DataTable)ViewState["ModuleIdentifiers"];
            if (ViewState["AutoPopulated"].ToString() == "True")
            {
                Blankstatus++;
            }
            for (int j = 0; j <= DTModuleIdents.Rows.Count - 1; j++)
            {

                foreach (Control x in pnlIdentFields.Controls)
                {
                    if (x.GetType() == typeof(TextBox))
                    {

                        if (flag == 0 && DTPatientIndefiers.Rows.Count > 0)
                        {

                            if (((TextBox)x).Text != "" && DTPatientIndefiers.Rows[0][DTModuleIdents.Rows[j]["FieldName"].ToString()] != System.DBNull.Value)
                            {
                                Blankstatus++;
                            }
                        }
                        else
                        {
                            if (((TextBox)x).Text != "")
                            {
                                Blankstatus++;
                            }
                        }
                    }
                }
            }
            return Blankstatus;
        }
        /// <summary>
        /// Gets the patient enrollement.
        /// </summary>
        /// <param name="patientID">The patient identifier.</param>
        /// <param name="locationID">The location identifier.</param>
        /// <returns></returns>
        DataTable GetPatientEnrollement(int patientID, int locationID)
        {
            DataTable dt = new DataTable();
            IPatientRegistration PatientManager;
            PatientManager = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
            dt = PatientManager.GetPatientServiceLines(patientID, locationID);
            string[] columnNames = { "PatientID", "LocationID", "ModuleID", "ModuleName", "EnrollmentDate", "CareStatus" };

            DataTable dv = dt.DefaultView.ToTable(true, columnNames);
            return dv;

        }
        /// <summary>
        /// Loads the module identifiers.
        /// </summary>
        /// <param name="ModuleID">The module identifier.</param>
        private void LoadModuleIdentifiers(int ModuleID)
        {
            ViewState["Enrolldate"] = "";
            pnlIdentFields.Controls.Clear();
            IPatientRegistration PatRegMgr = (IPatientRegistration)ObjectFactory.CreateInstance(ObjFactoryParameter);
            DataSet theDS = PatRegMgr.GetFieldNames(ModuleID, Convert.ToInt32(Session["PatientId"]));
            ViewState["PatientIdentdata"] = theDS.Tables[1];
            ViewState["ModuleIdentifiers"] = theDS.Tables[0];

            int AutoFieldValue = 1;

            if (Convert.ToInt32(Session["PatientId"]) != 0)
            {
                if (theDS.Tables[4].Rows[0]["AutoField"].ToString() == "")
                {
                    AutoFieldValue = 0;
                }
            }
            if (Convert.ToInt32(Session["PatientId"]) == 0)
            {
                AutoFieldValue = 0;
            }

            lblEnrollment.InnerText = "*Enrollment Date:";
            if (theDS.Tables[2].Rows.Count > 0 && theDS.Tables[2].Rows[0]["StartDate"].ToString() != "")
            {
                if (Convert.ToInt32(theDS.Tables[2].Rows[0]["ReEnrollCount"]) > 0)
                    lblEnrollment.InnerText = "*Re-Enrollment Date:";
                else
                    lblEnrollment.InnerText = "*Enrollment Date:";

                if (flag == 1)
                {
                    txtenrollmentDate.Value = ((DateTime)theDS.Tables[2].Rows[0]["StartDate"]).ToString(Session["AppDateFormat"].ToString());
                }
                if (theDS.Tables[2].Rows[0]["Enrolchk"].ToString() == "1")
                {
                    ViewState["Enrolldate"] = ((DateTime)theDS.Tables[2].Rows[0]["StartDate"]).ToString(Session["AppDateFormat"].ToString());
                }
                else
                    ViewState["Enrolldate"] = "";
                if (Convert.ToInt32(Session["IdentifierFlag"]) == 0 && txtenrollmentDate.Value != "")
                {
                    appDateimg2.Visible = false;
                    txtenrollmentDate.Disabled = true;
                }

            }

            ////ReEnrollment////
            if (theDS.Tables[3].Rows.Count > 0)
            {
                btnReEnollPatient.Visible = true;
                appDateimg2.Visible = false;
                txtenrollmentDate.Disabled = true;
                ViewState["CareEndedDate"] = theDS.Tables[3].Rows[0]["CareEndedDate"];
            }

            /////
            try
            {
                //For Loading Controls in the form

                DataTable DT = theDS.Tables[0];
                int Numtds = 2, td = 1;
                int countrow = theDS.Tables[0].Rows.Count;
                pnlIdentFields.Controls.Add(new LiteralControl("<table cellspacing='6' cellpadding='0' width='100%' border='0'>"));
                foreach (DataRow DRLnkTable in theDS.Tables[0].Rows)
                {
                    if (td <= Numtds)
                    {
                        if (td == 1)
                        {

                            pnlIdentFields.Controls.Add(new LiteralControl("<tr style='" + HideUnhideAutoPopulated(countrow, AutoFieldValue, DRLnkTable["AutoNumber"].ToString()) + "'>"));
                        }

                        pnlIdentFields.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%;" + HideUnhideAutoPopulated(countrow, AutoFieldValue, DRLnkTable["AutoNumber"].ToString()) + "'>"));
                        LoadFieldTypeControl(DRLnkTable["FieldType"].ToString(), DRLnkTable["FieldName"].ToString());
                        if (td == 2)
                        {
                            pnlIdentFields.Controls.Add(new LiteralControl("</td>"));
                            pnlIdentFields.Controls.Add(new LiteralControl("</tr>"));
                            td = 1;
                        }
                        else
                        {
                            pnlIdentFields.Controls.Add(new LiteralControl("</td>"));
                            td++;
                        }

                    }
                }
                if (td == 2)
                {
                    pnlIdentFields.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                    pnlIdentFields.Controls.Add(new LiteralControl("</td>"));
                    pnlIdentFields.Controls.Add(new LiteralControl("</tr>"));
                }
                td = 1;
                pnlIdentFields.Controls.Add(new LiteralControl("</table>"));
                pnlIdentFields.Controls.Add(new LiteralControl("</br>"));

            }
            catch (Exception err)
            {

                IQCareMsgBox.NotifyAction(err.Message, "Error Loading", false, this, "");
            }

        }

        /// <summary>
        /// Hides the unhide automatic populated.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <param name="finddata">The finddata.</param>
        /// <param name="autofield">The autofield.</param>
        /// <returns></returns>
        private string HideUnhideAutoPopulated(int count, int finddata, string autofield)
        {
            string strhide = string.Empty;
            if (autofield != "0")
            {
                if (finddata == 0)
                {
                    //strhide = "visibility:hidden";
                }
            }
            return strhide;
        }
        // Generating full DML Statement 
        /// <summary>
        /// Inserts the update identifiers.
        /// </summary>
        /// <returns></returns>
        private bool InsertUpdateIdentifiers()
        {
            ICustomFields CustomFields;
            try
            {
                bool theReEnroll = false;
                string sqlstr = string.Empty;
                IQCareUtils theUtils = new IQCareUtils();
                DateTime visitdate = Convert.ToDateTime(theUtils.MakeDate(txtenrollmentDate.Value));
                DateTime ReEnrollDate = Convert.ToDateTime(theUtils.MakeDate(txtReEnrollmentDate.Value));
                string sqlselect;
                string sqlstrset = "";
                string[] strarr = new string[3];

                StringBuilder Insertcbl;
                StringBuilder Insertcb2;
                IPatientRegistration ptnMgr = (IPatientRegistration)ObjectFactory.CreateInstance(ObjFactoryParameter);
                DataSet DSPatDetails = ptnMgr.GetPatientTechnicalAreaDetails(Convert.ToInt32(Session["PatientId"]), ddlTecharea.SelectedItem.ToString(), Convert.ToInt32(ddlTecharea.SelectedValue));
                int blankstatus = GetBlankTextboxesstatus(1);

                if (blankstatus > 0)
                {
                    sqlselect = "UPDATE mst_Patient WITH(ROWLOCK) SET ";
                    DataTable DTModuleIdents = (DataTable)ViewState["ModuleIdentifiers"];

                    for (int j = 0; j <= DTModuleIdents.Rows.Count - 1; j++)
                    {
                        foreach (Control x in pnlIdentFields.Controls)
                        {
                            if (x.GetType() == typeof(TextBox))
                            {
                                if ("txt" + DTModuleIdents.Rows[j]["FieldName"].ToString() == ((TextBox)x).ID)
                                {
                                    DataTable dtduplicates = ptnMgr.CheckDuplicateIdentifiervaule(DTModuleIdents.Rows[j]["FieldName"].ToString(), ((TextBox)x).Text);
                                    if (dtduplicates.Rows.Count == 0 || (dtduplicates.Rows.Count == 1 && dtduplicates.Rows[0]["Ptn_pk"].ToString() == Session["PatientId"].ToString()))
                                    {
                                        if (DTModuleIdents.Rows[j]["AutoNumber"].ToString() == "")
                                        {
                                            if (((TextBox)x).Text == "")
                                            {
                                                sqlstrset += "mst_patient." + DTModuleIdents.Rows[j]["FieldName"] + "=NULL,";
                                            }
                                            else
                                            {
                                                sqlstrset += "mst_patient." + DTModuleIdents.Rows[j]["FieldName"] + "='" + ((TextBox)x).Text + "',";
                                            }
                                        }
                                        else
                                        {
                                            if (((TextBox)x).Text == "")
                                            {
                                                sqlstrset += "mst_patient." + DTModuleIdents.Rows[j]["FieldName"] + "='" + GetMaxAutoPopulate(Convert.ToInt32(DTModuleIdents.Rows[j]["AutoNumber"].ToString()), DTModuleIdents.Rows[j]["FieldName"].ToString(), Convert.ToInt32(DTModuleIdents.Rows[j]["Fieldtype"].ToString())) + "',";
                                            }
                                            else
                                            {
                                                sqlstrset += "mst_patient." + DTModuleIdents.Rows[j]["FieldName"] + "='" + ((TextBox)x).Text + "',";
                                            }
                                        }

                                    }
                                    else if (dtduplicates.Rows.Count > 1 || (dtduplicates.Rows[0]["Ptn_pk"].ToString() != Session["PatientId"].ToString() && dtduplicates.Rows[0][DTModuleIdents.Rows[j]["FieldName"].ToString()].ToString() == ((TextBox)x).Text))
                                    {
                                        MsgBuilder theBuilder = new MsgBuilder();
                                        theBuilder.DataElements["Control"] = DTModuleIdents.Rows[j]["FieldName"].ToString();
                                        IQCareMsgBox.Show("DuplicateIndentifier", theBuilder, this);
                                        return false;

                                    }

                                    else
                                    {
                                        MsgBuilder theBuilder = new MsgBuilder();
                                        theBuilder.DataElements["Control"] = DTModuleIdents.Rows[j]["FieldName"].ToString();
                                        IQCareMsgBox.Show("DuplicateIndentifier", theBuilder, this);
                                        return false;

                                    }
                                }
                            }
                        }
                    }

                    sqlstrset = sqlstrset.Substring(0, sqlstrset.Length - 1);
                    sqlstrset += " where Ptn_pk= " + Session["PatientId"].ToString();
                    sqlstr = sqlselect + sqlstrset;
                    strarr[0] = sqlstr.ToString();

                    Insertcbl = new StringBuilder();
                    if (DSPatDetails.Tables[0].Rows.Count > 0)
                    {
                        Insertcbl.Append("UPDATE ord_visit WITH(ROWLOCK) SET  VisitDate='" + visitdate + "',UpdateDate=getdate()");
                        Insertcbl.Append(" where Ptn_pk= " + Session["PatientId"].ToString() + " and Visit_Id=" + DSPatDetails.Tables[0].Rows[0]["Visit_ID"].ToString());
                    }
                    else
                    {
                        Insertcbl.Append("Insert into ord_visit(Ptn_Pk,LocationID,VisitDate,VisitType,DataQuality,DeleteFlag,UserID,CreateDate)");
                        Insertcbl.Append("values (" + Convert.ToInt32(Session["PatientId"]) + ", " + Session["AppLocationId"].ToString() + ",'" + visitdate + "'," + DSPatDetails.Tables[1].Rows[0]["VisitTypeID"].ToString() + ",0,0,");
                        Insertcbl.Append("" + Session["AppUserId"].ToString() + ", Getdate())");
                    }
                    strarr[1] = Insertcbl.ToString();

                    Insertcb2 = new StringBuilder();
                    if (DSPatDetails.Tables[2].Rows.Count > 0)
                    {
                        if (btnReEnollPatient.Visible == false)
                        {
                            Insertcb2.Append("Update lnk_patientprogramstart WITH(ROWLOCK) SET");
                            Insertcb2.Append(" UpdateDate=Getdate(),StartDate='" + visitdate + "' where Ptn_Pk=" + Convert.ToInt32(Session["PatientId"]) + " and ModuleId=" + Convert.ToInt32(ddlTecharea.SelectedValue));
                        }
                        else
                        {
                            Insertcb2.Append("Update lnk_patientprogramstart WITH(ROWLOCK) SET");
                            Insertcb2.Append(" UpdateDate=Getdate(),StartDate='" + ReEnrollDate + "' where Ptn_Pk=" + Convert.ToInt32(Session["PatientId"]) + " and ModuleId=" + Convert.ToInt32(ddlTecharea.SelectedValue));
                            Insertcb2.Append("; Insert into Lnk_PatientReEnrollment(Ptn_Pk,LocationId,ModuleId,ReEnrollDate,OldEnrollDate,UserId,CreateDate)");
                            Insertcb2.Append("Values(" + Convert.ToInt32(Session["PatientId"]) + ", " + Session["AppLocationId"].ToString() + "," + Convert.ToInt32(ddlTecharea.SelectedValue) + ",'" + ReEnrollDate + "','" + visitdate + "'," + Session["AppUserId"].ToString() + ", Getdate())");
                            theReEnroll = true;
                        }
                    }
                    else
                    {
                        Insertcb2.Append("Insert into lnk_patientprogramstart(Ptn_Pk,ModuleID,StartDate,UserID,CreateDate)");
                        Insertcb2.Append("values (" + Convert.ToInt32(Session["PatientId"]) + ", " + Convert.ToInt32(ddlTecharea.SelectedValue) + ",");
                        Insertcb2.Append("'" + visitdate + "'," + Session["AppUserId"].ToString() + ", Getdate())");

                    }
                    strarr[2] = Insertcb2.ToString();
                    CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
                    int icountprog = CustomFields.SaveUpdateCustomFieldValues(strarr);
                    if (theReEnroll == true)
                    {
                        IPatientHome theReactivationManager = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
                        theReactivationManager.ReActivatePatient(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(ddlTecharea.SelectedValue));
                    }
                    theReEnroll = false;
                    if (icountprog == 0)
                    {
                        return false;
                    }

                    Session["TechnicalAreaId"] = ddlTecharea.SelectedValue.ToString();

                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                //return false;

            }
        }
        /// <summary>
        /// Gets the maximum automatic populate.
        /// </summary>
        /// <param name="startingnumber">The startingnumber.</param>
        /// <param name="columnname">The columnname.</param>
        /// <param name="fieldtype">The fieldtype.</param>
        /// <returns></returns>
        private string GetMaxAutoPopulate(int startingnumber, string columnname, int fieldtype)
        {
            string strmaxvalue = "";
            if (fieldtype == 17)
            {
                IPatientRegistration ptnMgr = (IPatientRegistration)ObjectFactory.CreateInstance(ObjFactoryParameter);
                DataSet DSPatDetails = ptnMgr.GetMaxAutoPopulateIdentifier(columnname);
                if (DSPatDetails.Tables[0].Rows[0][0].ToString() == "")
                {
                    strmaxvalue = startingnumber.ToString();
                }
                else
                {
                    int autoincrement = Convert.ToInt32(DSPatDetails.Tables[0].Rows[0][0].ToString()) + 1;
                    strmaxvalue = Convert.ToString(autoincrement);

                }
            }
            return strmaxvalue;
        }
        /// <summary>
        /// Saves the patient registration.
        /// </summary>
        private void SavePatientRegistration()
        {
            IPatientRegistration PatientManager = (IPatientRegistration)ObjectFactory.CreateInstance(ObjFactoryParameter);
            Hashtable GetValuefromHT = new Hashtable();
            try
            {
                if (Convert.ToInt32(Session["PatientId"]) == 0)
                {
                    if (Convert.ToString(ViewState["ptnid"]) == "")
                    {
                        GetValuefromHT = (Hashtable)Session["htPtnRegParameter"];
                        StringBuilder A = (StringBuilder)Session["CustomRegistration"];
                        DataSet Patientds = PatientManager.Common_GetSaveUpdateforCustomRegistrion(A.ToString());
                        Session["PatientId"] = Patientds.Tables[0].Rows[0]["ptn_pk"].ToString();
                        ViewState["ptnid"] = Patientds.Tables[0].Rows[0]["ptn_pk"].ToString();
                        ViewState["visitPk"] = Patientds.Tables[0].Rows[0]["Visit_Id"].ToString();
                        ViewState["IQNumber"] = Patientds.Tables[0].Rows[0]["IQNumber"].ToString();

                        DataSet theDS = PatientManager.GetFieldNames(Convert.ToInt32(ddlTecharea.SelectedValue), Convert.ToInt32(Session["PatientId"]));
                        if (theDS.Tables[1].Rows.Count > 0)
                        {
                            ViewState["PatientIdentdata"] = theDS.Tables[1];
                        }
                        ViewState["ModuleIdentifiers"] = theDS.Tables[0];
                        if (theDS.Tables[2].Rows.Count > 0 && txtenrollmentDate.Value == "")
                        {
                            txtenrollmentDate.Value = ((DateTime)theDS.Tables[2].Rows[0]["StartDate"]).ToString(Session["AppDateFormat"].ToString());
                            ViewState["Enrolldate"] = ((DateTime)theDS.Tables[2].Rows[0]["StartDate"]).ToString(Session["AppDateFormat"].ToString());
                            if (Convert.ToInt32(Session["IdentifierFlag"]) == 0 && txtenrollmentDate.Value != "")
                            {
                                appDateimg2.Visible = false;
                                txtenrollmentDate.Disabled = true;
                            }
                        }
                    }

                }
            }
            catch (Exception err)
            {
                IQCareMsgBox.NotifyAction(err.Message, "Error Loading", true, this, "");
            }
            finally
            {
                PatientManager = null;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnReEnollPatient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void btnReEnollPatient_Click(object sender, EventArgs e)
        {
            trReEnroll.Visible = true;
            txtReEnrollmentDate.Focus();
        }

        /// <summary>
        /// Handles the Click event of the btnSaveClose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void btnSaveClose_Click(object sender, EventArgs e)
        {

            if (FieldValidation() == false)
            {
                return;
            }
            SavePatientRegistration();
            if (InsertUpdateIdentifiers() == true)
            {
                SuccessRecordsSave();
            }

        }
        /// <summary>
        /// Successes the records save.
        /// </summary>
        private void SuccessRecordsSave()
        {

            // Session["status"] = "Add";
            //  int patientID = Convert.ToInt32(Session["PatientId"]);
            ClientScript.RegisterStartupScript(this.GetType(), "Changing", "<script>fnChange();</script>");
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            //script += "var ans;\n";
            script += "window.alert('Service Registration Form saved successfully.');\n";
            // script += "if (ans==true)\n";
            // script += "{\n";
            script += "window.location.href='./frmFindAddPatient.aspx';\n";
            //script += "}\n";
            script += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "success", script);
        }
        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        
        /// <summary>
        /// Gets the revist HRS allowance.
        /// </summary>
        /// <value>
        /// The revist HRS allowance.
        /// </value>
        int RevistHrsAllowance
        {
            get
            {
                int hrs = 24;
                int.TryParse(ConfigurationManager.AppSettings.Get("RevisitHoursAllowance").ToLower(), out hrs);
                return hrs;
            }
        }
        
        /// <summary>
        /// Gets the location identifier.
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
        
        /// <summary>
        /// Gets a value indicating whether this instance is record.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is record; otherwise, <c>false</c>.
        /// </value>
        bool IsRecord
        {
            get
            {
                return (Session["TechnicalAreaName"].ToString().Trim() == "Records");
            }
        }
        /// <summary>
        /// Handles the Click event of the btnRevisit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void btnRevisit_Click(object sender, EventArgs e)
        {
            //
            try
            {
                //Revist Date.
                DateTime now = DateTime.Now;
                //Entry point
                int moduleID = Convert.ToInt32(ddlTecharea.SelectedValue.ToString());
                //UserID
                IPatientHome pMGR;
                pMGR = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
                pMGR.SavePatientRevisit(now, this.RevistHrsAllowance, moduleID, this.UserId, this.PatientId, this.LocationId);

                this.Redirect();
            }
            catch (Exception err)
            {

                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }

        }
    }
}
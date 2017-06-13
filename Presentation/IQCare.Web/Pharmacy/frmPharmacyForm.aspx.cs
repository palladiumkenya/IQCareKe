using AjaxControlToolkit;
using Application.Common;
using Application.Presentation;
using Interface.Administration;
using Interface.Clinical;
using Interface.Pharmacy;
using IQCare.Web.UILogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace IQCare.Web.Pharmacy
{
    /// <summary>
    ///
    /// </summary>
    public partial class PharmacyForm : BasePage
    {
        ////////////////////////////////////////////////////////////////////
        // Code Written By   : Sanjay Rana
        // Modification Date : 19th Jan 2007
        // Description       : Pharmacy Form
        // Modified By       : Naveen Sharma
        // Modification Date : 14th August 2012

        /// <summary>
        /// The drug table
        /// </summary>
        //

        /// <summary>
        /// The add arv
        /// </summary>
        public DataTable AddARV;

        /// <summary>
        /// The arv checked drugs
        /// </summary>
        public DataTable ARVCheckedDrugs;

        /// <summary>
        /// The non arv drugs
        /// </summary>
        public DataTable NonARVDrugs;

        /// <summary>
        /// The oi drugs
        /// </summary>
        public DataTable OIDrugs;

        /// <summary>
        /// The other drugs
        /// </summary>
        public DataTable OtherDrugs;

        public DataTable VaccineDrugs;

        /// <summary>
        /// The tb drugs
        /// </summary>
        public DataTable TBDrugs;

        public DataTable theDrugTable;

        /// <summary>
        /// The incompflag
        /// </summary>
        private static int incompflag = 0;

        /// <summary>
        /// The count fixed comb
        /// </summary>
        private int CountFixedComb = 0;

        private string sbParameter = "";

        //Amitava Sinha
        /// <summary>
        /// The icount
        /// </summary>
        private int icount;

        //Amitava Sinha
        /// <summary>
        /// The print pres flag
        /// </summary>
        private int PrintPresFlag = 0;

        protected string svDispense
        {
            get
            {
                if (OrderId > 0)
                {
                    return "";
                }
                else if (this.IsPaperless)
                {
                    return "none";
                }
                return "";
            }
        }

        protected string sEdit
        {
            get
            {
                return OrderId > 0 ? "none" : "";
            }
        }

        protected string sView
        {
            get
            {
                return OrderId > 0 ? "" : "none";
            }
        }

        private bool IsDispensed
        {
            get
            {
                return Session["DispensedFlag"] != null && Convert.ToInt32(Session["DispensedFlag"]) == 1;
            }
        }

        protected string sDispensed
        {
            get
            {
                return this.IsDispensed ? "" : "none";
            }
        }

        protected string sNotDispensed
        {
            get
            {
                return this.IsDispensed ? "none" : "";
            }
        }

        /// <summary>
        /// The sb values
        /// </summary>
        private StringBuilder sbValues;

        /// <summary>
        /// The strmultiselect
        /// </summary>
        private string strmultiselect;

        /// <summary>
        /// The table name
        /// </summary>
        private string TableName = "";

        /// <summary>
        /// The ds
        /// </summary>
        private DataSet mainDataSet;

        /// <summary>
        /// The dv
        /// </summary>
        private DataView theDV = new DataView();

        /// <summary>
        /// The dataset for exisiting drugs in an order
        /// </summary>
        private DataSet theExistDS = new DataSet();

        /// <summary>
        /// The thepharm orderedby date
        /// </summary>
        private DateTime thepharmOrderedbyDate, thepharmReportedbyDate, theCurrentDate, theAppntDate;

        /// <summary>
        /// The provider identifier
        /// </summary>
        private int theProviderID;

        /// <summary>
        /// The treatment identifier
        /// </summary>
        private int theTreatmentID, theAppntReason;

        /// <summary>
        /// The uti
        /// </summary>
        private Application.Common.Utility theUti = new Application.Common.Utility();

        /// <summary>
        /// The visit type
        /// </summary>
        private int VisitType = 4;

        /// <summary>
        /// Gets the module identifier.
        /// </summary>
        /// <value>
        /// The module identifier.
        /// </value>
        private int ModuleId
        {
            get
            {
                return
                  Convert.ToInt32(Session["TechnicalAreaId"].ToString());
            }
        }

        private bool IsPaperless
        {
            get { return Session["Paperless"].ToString() == "1"; }
        }

        private bool PMSCMOn
        {
            get
            {
                return Session["SCMModule"] != null;
            }
        }

        //[Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        /// <summary>
        /// Functions the get frequency multiplier.
        /// </summary>
        /// <param name="strfrequency">The strfrequency.</param>
        /// <returns></returns>
        [WebMethod(EnableSession = true), ScriptMethod]
        public static string fnGetFrequencyMultiplier(string strfrequency)
        {
            string multiplier = "";
            DataView theDV = new DataView(((DataSet)HttpContext.Current.Session["MasterData"]).Tables[8]);
            theDV.RowFilter = "FrequencyName='" + strfrequency.ToString() + "'";
            DataTable dtfilter = theDV.ToTable();
            if (dtfilter.Rows.Count > 0)
            {
                multiplier = dtfilter.Rows[0]["multiplier"].ToString();
            }
            return multiplier;
        }

        /// <summary>
        /// Gets the drugs.
        /// </summary>
        /// <param name="prefixText">The prefix text.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public static List<Drugs> GetDrugs(string prefixText, int count)
        {
            List<Drugs> items = new List<Drugs>();
            IDrug objRptFields;
            objRptFields = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BDrug,BusinessProcess.Pharmacy");
            // string sqlQuery;
            //creating Sql Query
            bool CheckQuantity = false;
            int? ExcludeDrugType = null;
            if (HttpContext.Current.Session["Paperless"].ToString() == "1" && HttpContext.Current.Session["SCMModule"] != null)
            {
                CheckQuantity = true;
            }
            if (HttpContext.Current.Session["ARTEndedStatus"].ToString() == "ART Stopped")
            {
                ExcludeDrugType = 37;
            }
            else if (HttpContext.Current.Session["TreatmentProg"] != null && HttpContext.Current.Session["TreatmentProg"].ToString() == "225")
            {
                ExcludeDrugType = 37;
            }

            DataTable dataTable = objRptFields.FindDrugByName(prefixText, CheckQuantity, ExcludeDrugType);
            if (HttpContext.Current.Session["Paperless"].ToString() == "1" && HttpContext.Current.Session["SCMModule"] != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        try
                        {
                            Drugs item = new Drugs();
                            item.DrugId = (int)row["Drug_pk"];
                            item.DrugName = (string)row["DrugName"];
                            item.AvlQty = Convert.ToInt32(row["QTY"].ToString());
                            items.Add(item);
                        }
                        catch { }
                    }
                }
            }
            else
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        try
                        {
                            Drugs item = new Drugs();
                            item.DrugId = (int)row["Drug_pk"];
                            item.DrugName = (string)row["DrugName"];
                            //item.AvlQty = Convert.ToInt32(row["QTY"].ToString());
                            items.Add(item);
                        }
                        catch { }
                    }
                }
            }

            return items;
        }

        /// <summary>
        /// Searches the drugs.
        /// </summary>
        /// <param name="prefixText">The prefix text.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        [ScriptMethod()]
        [WebMethod(EnableSession = true)]
        public static List<string> SearchDrugs(string prefixText, int count)
        {
            List<string> Drugsdetail = new List<string>();
            List<Drugs> lstDrugsDetail = GetDrugs(prefixText, count);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            foreach (Drugs c in lstDrugsDetail)
            {
                Drugsdetail.Add(AutoCompleteExtender.CreateAutoCompleteItem(c.DrugName, serializer.Serialize(c)));
            }

            return Drugsdetail;
        }

        /// <summary>
        /// Finds the control recursive.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public Control FindControlRecursive(Control container, string name)
        {
            if (container.ID == name)
                return container;

            foreach (Control ctrl in container.Controls)
            {
                Control foundCtrl = FindControlRecursive(ctrl, name);
                if (foundCtrl != null)
                    return foundCtrl;
            }
            return null;
        }

        /// <summary>
        /// Nons the art pro phalasxis check.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="drug_id">The drug_id.</param>
        /// <returns></returns>
        public bool NonARTProPhylasxisCheck(DataTable dt, string drug_id)
        {
            Boolean blnCheck = false;
            DataRow[] theFilDT = dt.Select("Drug_pk=" + drug_id + "");
            string drugtypeid = theFilDT[0]["drugtypeid"].ToString();
            if (drugtypeid != "37")
            {
                blnCheck = true;
            }
            return blnCheck;
        }

        /// <summary>
        /// Bses the f_ attributes.
        /// </summary>
        protected void BSF_Attributes()
        {
            txtHeight.Attributes.Add("OnBlur", "CalcualteBSF('" + txtBSA.ClientID + "','" + txtWeight.ClientID + "','" + txtHeight.ClientID + "')");
            txtWeight.Attributes.Add("OnBlur", "CalcualteBSF('" + txtBSA.ClientID + "','" + txtWeight.ClientID + "','" + txtHeight.ClientID + "')");
        }

        /// <summary>
        /// Handles the Click event of the btncancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btncancel_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["opento"] == "ArtForm")
            {
                string script;
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'closeself_00'>\n";
                script += "self.close();\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "closeself_00", script);
                return;
            }

            string theUrl;
            if (Request.QueryString["name"] == "Delete")
            {
                theUrl = string.Format("{0}?PatientId={1}&sts={2}", "../ClinicalForms/frmClinical_DeleteForm.aspx", Session["PatientId"].ToString(), ViewState["Status"].ToString());
            }
            else
            {
                theUrl = string.Format("{0}?PatientId={1}", "../ClinicalForms/frmPatient_Home.aspx", Session["PatientId"].ToString());
            }
            Response.Redirect(theUrl);
        }

        /// <summary>
        /// Handles the Click event of the btnOk control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnOk_Click(object sender, EventArgs e)
        {
            DeleteForm();
        }

        /// <summary>
        /// Handles the Click event of the btnPresPrint control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnPresPrint_Click(object sender, EventArgs e)
        {
            PrintPresFlag = 1;
            if (Convert.ToString(Session["DispensedFlag"]) != "1")
            {
                SavePharmacyForm(0);
                if (incompflag == 1)
                    return;
            }
            if (Session["PrintStatus"] == null)
            {
                IQCareMsgBox.Show("PrintPresCheck", this);
                return;
            }
            if ((Convert.ToInt32(Session["PatientVisitId"]) > 0) && (Session["PrintStatus"].ToString() == "1"))
            {
                string theUrl = string.Format("{0}&PatientId={1}&ReportName={2}&sts={3}", "../Reports/frmReportViewer.aspx?name=Add", Session["PatientId"].ToString(), "PharmacyPrescription", "0");
                Response.Redirect(theUrl);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnsave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["name"] == "Delete")
            {
                //DeleteForm();
                //******* show the message to the user*******//
                string msgString;

                msgString = "Are you sure you want to delete Adult Pharmacy form? \\n";

                string script = "<script language = 'javascript' defer ='defer' id = 'aftersavefunction'>\n";
                script += "var ans;\n";
                script += "ans=window.confirm('" + msgString + "');\n";
                script += "if (ans==true)\n";
                script += "{\n";
                script += "document.getElementById('" + btnOk.ClientID + "').click();\n";
                script += "}\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "aftersavefunction", script);

                return;
            }
            else
            {
                SavePharmacyForm(1);
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlFrequency control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void ddlFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectFrequency(pnlPedia);
            SelectFrequency(PnlOtMed);
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlPharmSignature control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void ddlPharmSignature_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlPharmSignature.SelectedIndex == 3)
            //{
            //    ddlCounselerName.Visible = true;
            //    if (ddlCounselerName.Items.Count > 0)
            //        ddlCounselerName.SelectedIndex = 0;
            //}
            //else
            //{
            //    ddlCounselerName.Visible = false;
            //    lblCounselorName.Visible = false;
            //    txtCounselorName.Visible = false;
            //}
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlTreatment control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void ddlTreatment_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region "Check ARTStop"
            Session["TreatmentProg"] = ddlTreatment.SelectedValue.ToString();
            DateTime? dispensedDate = null;
            if (txtpharmReportedbyDate.Value.Trim() != "")
            {
                dispensedDate = Convert.ToDateTime(txtpharmReportedbyDate.Value);
            }
            pnlPedia.Enabled = !CheckARTStop(dispensedDate);

            //if (Session["TreatmentProg"].ToString() == "225")
            //{
            //    pnlPedia.Enabled = false;
            //}
            //else
            //{
            //    pnlPedia.Enabled = true;
            //}
            //IDrug theValidationManger = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BDrug,BusinessProcess.Pharmacy");
            //DataTable theValidateDT = theValidationManger.CheckARTStopStatus(Convert.ToInt32(Session["PatientId"]));
            //if ((theValidateDT.Rows.Count > 0 && theValidateDT.Rows[0]["ARTStatus"].ToString() == "ART Stopped" && Convert.ToInt32(Session["PatientVisitId"]) == 0 && Convert.ToInt32(ddlTreatment.SelectedValue) == 222) ||
            //    (theValidateDT.Rows.Count > 0 && theValidateDT.Rows[0]["ARTStatus"].ToString() == "ART Stopped" && Convert.ToInt32(Session["PatientVisitId"]) > 0
            //    && Convert.ToDateTime(txtpharmReportedbyDate.Value) >= Convert.ToDateTime(theValidateDT.Rows[0]["ARTEndDate"]) && Convert.ToInt32(ddlTreatment.SelectedValue) == 222))
            //{
            //    pnlPedia.Enabled = false;
            //}

            //theValidationManger = null;
            //theValidateDT.Dispose();
            #endregion "Check ARTStop"
            if (Convert.ToInt32(ddlTreatment.SelectedValue) == 222)
            {
                EnableDisableAllCheckBoxControls(pnlPedia, false);
            }
        }

        private int OrderStatus
        {
            get
            {
                return Convert.ToInt32(hdOrderStatus.Value);
            }
            set
            {
                hdOrderStatus.Value = value.ToString();
            }
        }

        private int OrderId
        {
            get
            {
                return Convert.ToInt32(hdOrderId.Value);
            }
            set
            {
                hdOrderId.Value = value.ToString();
            }
        }

        private int SignatureBy
        {
            get
            {
                return Convert.ToInt32(hdSignature.Value);
            }
            set
            {
                hdSignature.Value = value.ToString();
            }
        }

        private int DispensedBy
        {
            get
            {
                return Convert.ToInt32(hdDispensedBy.Value);
            }
            set
            {
                hdDispensedBy.Value = value.ToString();
            }
        }

        private int PrescribedBy
        {
            get
            {
                return Convert.ToInt32(hdPrescribedBy.Value);
            }
            set
            {
                hdPrescribedBy.Value = value.ToString();
            }
        }

        /// <summary>
        /// Gets the exist pediatric fields.
        /// </summary>
        protected void GetExistPediatricFields()
        {
            IPediatric PediatricManager;
            try
            {
                BindControls();
                PediatricManager = (IPediatric)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BPediatric, BusinessProcess.Pharmacy");
                int pharmacyId = Convert.ToInt32(Session["PatientVisitId"]);
                this.OrderId = pharmacyId;
                int PatientID = Convert.ToInt32(Session["PatientId"]);
                ViewState["PatientId"] = PatientID;
                //pr_Pharmacy_GetExistPaediatricDetails_Constella
                theExistDS = PediatricManager.GetExistPaediatricDetails(pharmacyId);
                if (theExistDS.Tables.Count == 0)
                {
                    IQCareMsgBox.NotifyAction(IQCareMsgBox.GetMessage("NoPharmacyRecordExists", null), "Missing Pharmacy Order", true, this, "");
                    return;
                }
                Session["ExistPharmacyData"] = theExistDS.Tables[0];
                foreach (DataRow theDR in theExistDS.Tables[0].Rows)
                {
                    if (theDR["PrintPrescriptionStatus"].ToString() == "1")
                    {
                        Session["PrintStatus"] = 1;
                    }
                }
                //pr_Pharmacy_GetPediatricDetails_Constella
                DataSet theDrugDS = this.PopulateTheDS(Convert.ToInt32(theExistDS.Tables[0].Rows[0]["Ptn_pk"].ToString()));
                //DataSet theDrugDS = PediatricManager.GetPediatricFields(Convert.ToInt32(theExistDS.Tables[0].Rows[0]["Ptn_pk"].ToString()));
                /*  #region "FixDoseCombination"
              theDS = new DataSet();
              theDS.Tables.Add(theDrugDS.Tables[17].Copy());//--0--performance - gen abbr & only active drugs
              theDS.Tables.Add(theDrugDS.Tables[1].Copy());//--1--
              theDS.Tables.Add(theDrugDS.Tables[2].Copy());//--2--
              theDS.Tables.Add(theDrugDS.Tables[3].Copy());//--3--
              theDS.Tables.Add(theDrugDS.Tables[4].Copy());//--4--
              theDS.Tables.Add(theDrugDS.Tables[5].Copy());//--5--
              theDS.Tables.Add(theDrugDS.Tables[6].Copy());//--6--
              theDS.Tables.Add(theDrugDS.Tables[15].Copy());//--7-- for inactive units in case of edit;
              theDS.Tables.Add(theDrugDS.Tables[8].Copy());//--8--
              theDS.Tables.Add(theDrugDS.Tables[9].Copy());//--9--
              theDS.Tables.Add(theDrugDS.Tables[10].Copy());//--10--
              theDS.Tables.Add(theDrugDS.Tables[11].Copy());//--11--
              theDS.Tables.Add(theDrugDS.Tables[12].Copy());//--12-- stores all (both active/inactive) drugs
              theDS.Tables.Add(theDrugDS.Tables[13].Copy());//--13--  rupesh 04-sep for OI and other medication - for custom frq list
              //theDS.Tables.Add(theDrugDS.Tables[14]);//  rupesh 19-sep-07 for ARV Provider
              theDS.Tables.Add(theDrugDS.Tables[16].Copy());//--14--  rupesh 19-sep-07 for active/inactive ARV Provider
              theDS.Tables.Add(theDrugDS.Tables[21].Copy());//--15--  29Feb08 -- Non-ARTDate
              theDS.Tables.Add(theDrugDS.Tables[22].Copy());//-period taken
              theDS.Tables.Add(theDrugDS.Tables[23].Copy());//-TB Regimen
              theDS.Tables.Add(theDrugDS.Tables[24].Copy());
              theDS.Tables.Add(theDrugDS.Tables[25].Copy());
              theDS.Tables.Add(theDrugDS.Tables[26].Copy());
              theDS.Tables.Add(theDrugDS.Tables[27].Copy());
              theDS.Tables.Add(theDrugDS.Tables[28].Copy());
              theDS.Tables.Add(theDrugDS.Tables[29].Copy());
              #endregion "Class for Drug"
              */

                //---rupesh -- for fixed drug strength / frequency -----
                Session["FixDrugStrength"] = theDrugDS.Tables[18];
                Session["FixDrugFreq"] = theDrugDS.Tables[19];
                //---------------------------------------------------------------
                if ((theExistDS.Tables[0].Rows[0]["Weight"] != System.DBNull.Value) || (theExistDS.Tables[0].Rows[0]["Height"] != System.DBNull.Value))
                {
                    // decimal theWeight = Convert.ToDecimal(theExistDS.Tables[0].Rows[0]["Weight"].ToString());
                    decimal theWeight = 0;
                    decimal.TryParse(theExistDS.Tables[0].Rows[0]["Weight"].ToString(), out theWeight);

                    if (theWeight > 0)
                        txtWeight.Text = Convert.ToString(theWeight);

                    //  decimal theHeight = Convert.ToDecimal(theExistDS.Tables[0].Rows[0]["Height"].ToString());

                    decimal theHeight = 0;
                    decimal.TryParse(theExistDS.Tables[0].Rows[0]["Height"].ToString(), out theHeight);
                    if (theHeight > 0)
                        txtHeight.Text = Convert.ToString(theHeight);
                    if (theWeight > 0 && theHeight > 0)
                    {
                        decimal theBSA = theWeight * theHeight / 3600;
                        theBSA = (decimal)Math.Sqrt(Convert.ToDouble(theBSA));
                        theBSA = Math.Round(theBSA, 2);
                        txtBSA.Text = Convert.ToString(theBSA);
                    }
                }
                pnlPedia.Controls.Clear();

                DataTable dtPatientInfo = (DataTable)Session["PatientInformation"];
                DateTime theDOB = (DateTime)dtPatientInfo.Rows[0]["DOB"];// theDS.Tables[6].Rows[0]["DOB"];
                txtDOB.Text = theDOB.ToString(Session["AppDateFormat"].ToString());
                txtYr.Text = dtPatientInfo.Rows[0]["Age"].ToString();// theDS.Tables[6].Rows[0]["Age"].ToString();
                txtMon.Text = dtPatientInfo.Rows[0]["Age1"].ToString(); //th

                if (theExistDS.Tables[1].Rows.Count > 0)
                {
                    DateTime theAppntmentDate = Convert.ToDateTime(theExistDS.Tables[1].Rows[0]["AppDate"].ToString());
                    txtpharmAppntDate.Value = theAppntmentDate.ToString(Session["AppDateFormat"].ToString());
                    if (Convert.ToInt32(theExistDS.Tables[1].Rows[0]["AppReason"]) > 0)
                    {
                        ddlAppntReason.SelectedValue = theExistDS.Tables[1].Rows[0]["AppReason"].ToString();
                    }
                }
                if (theExistDS.Tables[0].Rows[0]["RegimenLine"] != System.DBNull.Value)
                {
                    ddlregimenLine.SelectedValue = theExistDS.Tables[0].Rows[0]["RegimenLine"].ToString();
                }

                BindddlControls(ref mainDataSet);
                MakeRegimenGenericTable(mainDataSet);
                BindInfantHealthSection(mainDataSet);
                CreateControls(mainDataSet);

                if (theExistDS.Tables[0].Rows[0]["PharmacyPeriodTaken"].ToString() != "")
                {
                    ddlPeriodTaken.SelectedValue = theExistDS.Tables[0].Rows[0]["PharmacyPeriodTaken"].ToString();
                }

                this.PrescribedBy = Convert.ToInt32(theExistDS.Tables[0].Rows[0]["OrderedBy"]);
                this.DispensedBy = Convert.ToInt32(theExistDS.Tables[0].Rows[0]["DispensedBy"]);
                int s = 0;
                int.TryParse(theExistDS.Tables[0].Rows[0]["EmployeeID"].ToString(), out s);
                this.SignatureBy = s;
                this.OrderId = Convert.ToInt32(theExistDS.Tables[0].Rows[0]["ptn_pharmacy_pk"]);
                this.OrderStatus = Convert.ToInt32(theExistDS.Tables[2].Rows[0]["OrderStatus"]);
                // BindDropdownOrderBy(theExistDS.Tables[0].Rows[0]["OrderedBy"].ToString());
                // BindDropdownDispensedBy(theExistDS.Tables[0].Rows[0]["DispensedBy"].ToString());
                // BindDropdownSignature(theExistDS.Tables[0].Rows[0]["EmployeeID"].ToString());

                ddlPharmOrderedbyName.ClearSelection();
                //  ddlPharmOrderedbyName.SelectedValue = theExistDS.Tables[0].Rows[0]["OrderedBy"].ToString();
                ddlDispensedBy.ClearSelection();
                //ddlDispensedBy.SelectedValue = theExistDS.Tables[0].Rows[0]["DispensedBy"].ToString();
                ddlPharmSignature.ClearSelection();
                //---Rupesh : 31Jan08 : Signature was not saving
                // ddlPharmSignature.SelectedValue = theExistDS.Tables[0].Rows[0]["EmployeeID"].ToString();

                ddlTreatment.SelectedValue = theExistDS.Tables[0].Rows[0]["ProgID"].ToString();
                if (theExistDS.Tables[0].Rows[0]["ProgID"].ToString() == "225")
                {
                    Session["TreatmentProg"] = theExistDS.Tables[0].Rows[0]["ProgID"].ToString();
                }
                else
                {
                    Session["TreatmentProg"] = "";
                }
                //rupesh 19-sep-07 for ARV Provider
                ddlProvider.SelectedValue = Convert.ToString(theExistDS.Tables[0].Rows[0]["ProviderID"].ToString());
                if (theExistDS.Tables[0].Rows[0]["OrderedByDate"] != DBNull.Value)
                {
                    DateTime theOrderedByDate = Convert.ToDateTime(theExistDS.Tables[0].Rows[0]["OrderedByDate"].ToString());
                    labelOrderByDate.Text = txtpharmOrderedbyDate.Value = theOrderedByDate.ToString(Session["AppDateFormat"].ToString());
                }
                //txtpharmOrderedbyDate.Attributes.Add("readonly", "true");

                //DateTime theReportedbyDate = Convert.ToDateTime(theExistDS.Tables[0].Rows[0]["DispensedByDate"]);
                if (theExistDS.Tables[0].Rows[0]["DispensedByDate"].ToString() != "")
                {
                    DateTime theReportedbyDate = Convert.ToDateTime(theExistDS.Tables[0].Rows[0]["DispensedByDate"]);
                    labelDispensedDate.Text = txtpharmReportedbyDate.Value = theReportedbyDate.ToString(Session["AppDateFormat"].ToString());
                }

                if (theExistDS.Tables[0].Rows[0]["PharmacyNotes"].ToString() != "")
                {
                    txtClinicalNotes.Text = theExistDS.Tables[0].Rows[0]["PharmacyNotes"].ToString();
                }

                //txtpharmReportedbyDate.Value = theReportedbyDate.ToString(Session["AppDateFormat"].ToString());
                //txtpharmReportedbyDate.Attributes.Add("readonly", "true");
                string theSign = theExistDS.Tables[0].Rows[0]["Signature"].ToString();

                if (theExistDS.Tables[0].Rows[0]["EmployeeID"].ToString() != "0")
                {
                }
                else if (theExistDS.Tables[0].Rows[0]["EmployeeID"].ToString() == "0" && theSign == "1")
                {
                }
                else
                {
                }
              
                DataTable theDT1 = new DataTable();
                theDT1.Columns.Add("DrugId", Type.GetType("System.Int32"));
                theDT1.Columns.Add("DrugName", Type.GetType("System.String"));
                theDT1.Columns.Add("Generic", Type.GetType("System.Int32"));
                theDT1.Columns.Add("DrugTypeId", Type.GetType("System.Int32"));
                AddARV = theDT1.Copy();
                OtherDrugs = theDT1.Copy();
                TBDrugs = theDT1.Copy();
                OIDrugs = theDT1.Copy();
                VaccineDrugs = theDT1.Copy();
                //NonARVDrugs = theDT1.Copy();
               

                foreach (DataRow theDR in theExistDS.Tables[0].Rows)
                {
                    if (theDR["Drug_Pk"] != DBNull.Value)
                    {
                        DataView theDV = new DataView(mainDataSet.Tables[12]);//rupesh for showing inactive drug
                        theDV.RowFilter = "Drug_Pk = " + theDR["Drug_Pk"].ToString();
                        if (theDV.Count > 0)
                        {
                            if (Convert.ToInt32(theDV[0]["DrugTypeId"]) == 37)
                            {
                                DataRow DR = AddARV.NewRow();
                                DR[0] = theDR["Drug_Pk"];
                                DR[1] = theDV[0]["DrugName"];
                                DR[2] = 0;
                                AddARV.Rows.Add(DR);
                                if (trHIVsetFields.Visible == false)
                                {
                                    trHIVsetFields.Visible = true;
                                }
                            }
                            else if (Convert.ToInt32(theDV[0]["DrugTypeId"]) == 31)
                            {
                                DataRow DR = TBDrugs.NewRow();
                                DR[0] = theDR["Drug_Pk"];
                                DR[1] = theDV[0]["DrugName"];
                                DR[2] = 0;
                                TBDrugs.Rows.Add(DR);
                            }
                            else if (Convert.ToInt32(theDV[0]["DrugTypeId"]) == 36)
                            {
                                DataRow DR = OIDrugs.NewRow();
                                DR[0] = theDR["Drug_Pk"];
                                DR[1] = theDV[0]["DrugName"];
                                DR[2] = 0;
                                OIDrugs.Rows.Add(DR);
                            }
                            else if (Convert.ToInt32(theDV[0]["DrugTypeId"]) == 60) // vaccines
                            {
                                DataRow DR = VaccineDrugs.NewRow();
                                DR[0] = theDR["Drug_Pk"];
                                DR[1] = theDV[0]["DrugName"];
                                DR[2] = 0;
                                VaccineDrugs.Rows.Add(DR);
                            }
                            else
                            {
                                DataRow DR = OtherDrugs.NewRow();
                                DR[0] = theDR["Drug_Pk"];
                                DR[1] = theDV[0]["DrugName"];
                                DR[2] = 0;
                                OtherDrugs.Rows.Add(DR);
                            }
                        }
                    }
                }

                LoadAdditionalDrugs(AddARV, pnlPedia);
                LoadAdditionalDrugs(OtherDrugs, PnlOtMed);
                LoadAdditionalDrugs(TBDrugs, pnlOtherTBMedicaton);
                LoadAdditionalDrugs(OIDrugs, PnlOIARV);
                LoadAdditionalDrugs(VaccineDrugs, panelVaccine);

         

                foreach (DataRow dr in theExistDS.Tables[0].Rows)
                {
                    FillOldData(pnlPedia, dr, true);
                    FillOldData(PnlOIARV, dr, true);
                    //FillOldData(PnlRegiment, dr,true);
                    FillOldData(PnlOtMed, dr, false);
                    FillOldData(pnlOtherTBMedicaton, dr, false);
                    FillOldData(panelVaccine, dr, false);
                }
                this.OrderStatus = Convert.ToInt32(theExistDS.Tables[2].Rows[0]["OrderStatus"]);

                if (this.OrderStatus > 1)
                {
                    Session["DispensedFlag"] = 1;
                    btnsave.Enabled = false;
                }
                else if (this.OrderId > 0 && this.OrderStatus == 1)
                {
                    btnsave.Enabled = (this.UserId == this.PrescribedBy || this.UserId == this.SignatureBy) || (PMSCMOn == false && this.IsPaperless);
                }
                //BindChkImmunizations(theExistDS.Tables[0]);
            }
            catch (Exception er)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = er.Message.ToString();
                IQCareMsgBox.Show("C1#", theBuilder, this);
            }
        }

        protected bool CanEdit
        {
            get
            {
                AuthenticationManager Authentiaction = new AuthenticationManager();
                if ((Authentiaction.HasFunctionRight(ApplicationAccess.AdultPharmacy, FunctionAccess.Update, (DataTable)Session["UserRight"]) == false))
                {
                    return (this.OrderId > 1 && (this.UserId == this.PrescribedBy || this.UserId == this.SignatureBy) && OrderStatus == 1) ||
                        (this.OrderId > 1 && this.OrderStatus == 1 && PMSCMOn == false);
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Gets the pediatric fields.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        protected void GetPediatricFields(int patientId)
        {
            IPediatric PediatricManager;
            try
            {
                PediatricManager = (IPediatric)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BPediatric, BusinessProcess.Pharmacy");
                //pr_Pharmacy_GetPediatricDetails_Constella
                DataSet theDrugDS = PopulateTheDS(patientId);
                if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
                {
                    this.mainDataSet.Tables[4].DefaultView.RowFilter = "DeleteFlag = 0";
                }

                DataTable dtPatientInfo = (DataTable)Session["PatientInformation"];

                DateTime theDOB = (DateTime)dtPatientInfo.Rows[0]["DOB"];// theDS.Tables[6].Rows[0]["DOB"];
                txtDOB.Text = theDOB.ToString(Session["AppDateFormat"].ToString());
                txtYr.Text = dtPatientInfo.Rows[0]["Age"].ToString();// theDS.Tables[6].Rows[0]["Age"].ToString();
                txtMon.Text = dtPatientInfo.Rows[0]["Age1"].ToString(); //theDS.Tables[6].Rows[0]["Age1"].ToString();
                ViewState["MasterData"] = mainDataSet;
                BindddlControls(ref mainDataSet);
                CreateControls(mainDataSet);

                if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
                {
                    BindControls();
                    decimal? theWeight = null;
                    decimal? theHeight = null;
                    if (theDrugDS.Tables[31].Rows.Count > 0 && theDrugDS.Tables[31].Rows[0]["Weight"] != System.DBNull.Value)
                    {
                        theWeight = Convert.ToDecimal(theDrugDS.Tables[31].Rows[0]["Weight"].ToString());
                        if (theWeight.HasValue && theWeight.Value > 0)
                        {
                            txtWeight.Text = Convert.ToString(theWeight);
                            dtwt.InnerText = "Entered " + Convert.ToDateTime(theDrugDS.Tables[31].Rows[0]["VisitDate"]).ToString(Session["AppDateFormat"].ToString());
                        }
                    }
                    if (theDrugDS.Tables[30].Rows.Count > 0 && theDrugDS.Tables[30].Rows[0]["Height"] != System.DBNull.Value)
                    {
                        theHeight = Convert.ToDecimal(theDrugDS.Tables[30].Rows[0]["Height"].ToString());
                        if (theHeight.HasValue && theHeight.Value > 0)
                        {
                            txtHeight.Text = Convert.ToString(theHeight);
                            dtht.InnerText = "Entered " + Convert.ToDateTime(theDrugDS.Tables[30].Rows[0]["VisitDate"]).ToString(Session["AppDateFormat"].ToString());
                        }
                    }
                    if (theHeight.HasValue && theWeight.HasValue)
                    {
                        decimal theBSA = theWeight.Value * theHeight.Value / 3600;
                        theBSA = (decimal)Math.Sqrt(Convert.ToDouble(theBSA));
                        theBSA = Math.Round(theBSA, 2);
                        txtBSA.Text = Convert.ToString(theBSA);
                    }
                }
                MakeRegimenGenericTable(mainDataSet);
                BindInfantHealthSection((DataSet)ViewState["MasterData"]);
            }
            catch (Exception er)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = er.Message.ToString();
                IQCareMsgBox.Show("C1#", theBuilder, this);
            }
        }

        /// <summary>
        /// Handles the Init event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Init(object sender, EventArgs e)
        {
            CurrentSession currentSession = CurrentSession.Current;
            Master.PageScriptManager.AsyncPostBackError += new EventHandler<AsyncPostBackErrorEventArgs>(PageScriptManager_AsyncPostBackError);
            DataView theHIVPharmDV = new DataView((DataTable)Session["AppModule"]);
            theHIVPharmDV.RowFilter = "ModuleId=" + Convert.ToInt32(Session["TechnicalAreaId"]);
            ViewState["PharmacyFlag"] = 0;
            if (theHIVPharmDV != null && theHIVPharmDV[0]["PharmacyFlag"] != System.DBNull.Value)
            {
                if (Convert.ToInt32(theHIVPharmDV[0]["PharmacyFlag"]) == 1)
                {
                    trHIVsetFields.Visible = true;
                    ViewState["PharmacyFlag"] = 1;
                }
                else
                {
                    trHIVsetFields.Visible = false;
                    ViewState["PharmacyFlag"] = 0;
                }
            }

            if (currentSession.HasPMSCM) //pmscm is on disable dispensedby and dispensedate
            {
                EnableDisableControl();
                ddlDispensedBy.Enabled = false;
                txtpharmReportedbyDate.Disabled = true;
            }
            if (currentSession.Facility.PaperLess == false && currentSession.HasPMSCM) //pmscm is on, but paperless is off - freeze the page, disable search of drugs
            {
                txtautoDrugName.Enabled = false;
                labelForDispensedBy.Attributes.Remove("Class");
                lbldispensedbydate.Attributes.Remove("Class");
                btnsave.Enabled = false;
            }
            else if (currentSession.Facility.PaperLess && currentSession.HasPMSCM) //pmsscm is on, paperless //enable search of drugs
            {
                txtautoDrugName.Enabled = true;
            }
            else if (currentSession.HasPMSCM) //pmscm is on - enable search of drugs
            {
                txtautoDrugName.Enabled = true;
            }

            if (IsPostBack != true)
            {
                Session["DispensedFlag"] = 0;
                if (Request.QueryString["opento"] == "ArtForm")
                {
                    Session["PatientVisitId"] = 0;
                }

                if (Request.QueryString["name"] == "Delete")
                {
                    btnsave.Text = "Delete";
                }
                Init_Form();
                string strDispensedBy = this.DispensedBy > 0 ? this.DispensedBy.ToString() : "";
                // string strPrescribedBy = this.PrescribedBy > 0 ? this.PrescribedBy.ToString() : "";
                string strSignature = this.SignatureBy > 0 ? this.SignatureBy.ToString() : this.UserId.ToString();

                this.BindDropdownOrderBy();
                this.BindDropdownSignature(strSignature);
                this.BindDropdownDispensedBy(strDispensedBy);
                if (Request.QueryString["RefillFromLast"] == "true")
                {
                    Session["RefillFromLast"] = "1";
                    DateTime cutOffDate = new DateTime(1, 1, 1).AddDays(Convert.ToInt32(Request.QueryString["offset"]));
                    this.GetPatientLastPrescriptionByType(37, cutOffDate);
                }
                else
                {
                    Session["RefillFromLast"] = "0";
                }
            }
        }

        private bool CanDispense
        {
            get
            {
                if (Session["Paperless"].ToString() == "1" && Session["SCMModule"] != null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            int thePtnID = 0;
            Session["PtnRegCTC"] = "";
            Session["CustomfrmDrug"] = "";
            Session["CustomfrmLab"] = "";
            bool isRefill = base.Session["RefillFromLast"].ToString().Equals("1");
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Pharmacy Forms >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Pharmacy Form";
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Pharmacy Form";
            if (Request.QueryString["Prog"] == "ART")
            {
                (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = Convert.ToString(Session["HIVPatientStatus"]);
            }
            else
            {
                (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = Convert.ToString(Session["PMTCTPatientStatus"]);
            };
            if ((Session["HIVPatientStatus"].ToString() == "1") && (Session["PMTCTPatientStatus"].ToString() == "1"))
            {
                btnsave.Enabled = false;
            }
            if (this.OrderId > 0)
            {
                txtautoDrugName.Enabled = (this.UserId == this.PrescribedBy || this.UserId == this.SignatureBy) || (PMSCMOn == false && this.IsPaperless);
                if (this.OrderStatus > 1)
                {
                    txtautoDrugName.Enabled = false;
                }
            }
            if (Session["Paperless"].ToString() == "0")
            {
                labelForDispensedBy.Attributes.Add("Class", "required");
                lbldispensedbydate.Attributes.Add("Class", "required");
            }
            else if (Session["Paperless"].ToString() == "1" && Session["SCMModule"] == null) //pmscm is off, paperless on - enable drug drug search, disable dispensedby. dispensedate if it is a new order
            {
                //txtautoDrugName.Enabled = true;
                if (Convert.ToInt32(Session["PatientVisitId"]) != 0)
                {
                    ddlDispensedBy.Enabled = true;
                    txtpharmReportedbyDate.Disabled = false;
                    labelForPrescribedBy.Attributes.Remove("Class");
                    labelForPrescribedByDate.Attributes.Remove("Class");
                    if (this.OrderStatus > 1)
                    {
                        labelForDispensedBy.Attributes.Remove("Class");
                        lbldispensedbydate.Attributes.Remove("Class");
                    }
                    else
                    {
                        labelForDispensedBy.Attributes.Add("Class", "required");
                        lbldispensedbydate.Attributes.Add("Class", "required");
                    }
                }
                else
                {
                    ddlDispensedBy.Enabled = false;
                    txtpharmReportedbyDate.Disabled = true;
                    labelForDispensedBy.Attributes.Add("Class", "required");
                    lbldispensedbydate.Attributes.Add("Class", "required");
                }
            }
            DataTable theDTModule = (DataTable)Session["AppModule"];
            string ModuleId = "";
            foreach (DataRow theDR in theDTModule.Rows)
            {
                if (ModuleId == "")
                    ModuleId = theDR["ModuleId"].ToString();
                else
                    ModuleId = ModuleId + "," + theDR["ModuleId"].ToString();
            }

            if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            {
                //theUti.SetSession();
            }

            PutCustomControl();

            this.Authenticate();

            if (!IsPostBack)
            {
                //Original ord date when the form was first loaded
                if (Convert.ToInt32(Session["PatientVisitId"]) != 0)
                {
                    ViewState["OrigOrdDate"] = theExistDS.Tables[0].Rows[0]["OrderedByDate"];
                    if (theExistDS.Tables[0].Rows[0]["DispensedByDate"].ToString() != "")
                    {
                        Session["OrigDispensbyDate"] = theExistDS.Tables[0].Rows[0]["DispensedByDate"].ToString();
                    }
                    else
                    {
                        Session["OrigDispensbyDate"] = null;
                    }
                }
                else
                //ViewState["OrigOrdDate"] = null;
                {
                    Session["OrigDispensbyDate"] = null;
                    ViewState.Remove("OrigOrdDate");
                }

                txtWeight.Attributes.Add("onkeyup", "chkDecimal('" + txtWeight.ClientID + "')");
                txtHeight.Attributes.Add("onkeyup", "chkDecimal('" + txtHeight.ClientID + "')");
                txtBSA.Attributes.Add("onkeyup", "chkNumeric('" + txtBSA.ClientID + "')");

                txtpharmAppntDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
                // txtpharmOrderedbyDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
                txtpharmAppntDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");

                txtpharmOrderedbyDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
                //  txtpharmOrderedbyDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
                txtpharmOrderedbyDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");

                txtpharmReportedbyDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
                //   txtpharmReportedbyDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
                txtpharmReportedbyDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");

                //ddlTreatment.Attributes.Add("onchange", "fnCheckUnCheck()");
                Session["AddARV"] = null;
                Session["OtherDrugs"] = null;
                Session["MasterDrugTable"] = null;
                Session["AddTB"] = null;
                Session["VaccineDrugs"] = null;
                Session["OIDrugs"] = null;
                Session["PharmacyId"] = null;
                thePtnID = Convert.ToInt32(Session["PatientId"]);
                ViewState["PtnID"] = thePtnID;
                Session["PtnID"] = thePtnID;
                ViewState["UserID"] = Session["AppUserId"].ToString();
                ViewState["LocationId"] = Convert.ToInt32(Session["AppLocationId"]);
                ViewState["SelectedDrug"] = theDrugTable;
                ViewState["MasterData"] = mainDataSet;
                Session["MasterData"] = mainDataSet;
                if (Request.QueryString["Prog"] == "ART")
                {
                    ViewState["Status"] = Session["HIVPatientStatus"].ToString();
                    Session["Status"] = Session["HIVPatientStatus"].ToString();
                }
                else
                {
                    ViewState["Status"] = Session["PMTCTPatientStatus"].ToString();
                    Session["Status"] = Session["PMTCTPatientStatus"].ToString();
                }

                if (mainDataSet != null) // rupesh 18-sep-07
                {
                    DataView theEnollDV = new DataView(mainDataSet.Tables[10]);
                    theEnollDV.RowFilter = "ModuleId=" + Session["TechnicalAreaId"].ToString();
                    if (theEnollDV.Count > 0)
                        ViewState["EnrolmentDate"] = theEnollDV[0]["StartDate"];
                    else
                        Response.Redirect("../frmFindAddPatient.aspx");
                }
                BSF_Attributes();
       
                if (mainDataSet != null)
                {
                    DataTable theDT = new DataTable();
                    // --- check inactive /active cases
                    theDT.Columns.Add("DrugId", Type.GetType("System.Int32"));
                    theDT.Columns.Add("DrugName", Type.GetType("System.String"));
                    theDT.Columns.Add("Generic", Type.GetType("System.Int32"));
                    theDT.Columns.Add("DrugTypeId", Type.GetType("System.Int32"));
                    theDT.Columns.Add("Abbr", Type.GetType("System.String"));

                    foreach (DataRow dr in mainDataSet.Tables[0].Rows)
                    {
                        DataRow theDR = theDT.NewRow();
                        theDR[0] = dr["Drug_Pk"];
                        theDR[1] = dr["DrugName"];
                        theDR[2] = 0;
                        theDR[3] = dr["DrugTypeId"];
                        theDR[4] = dr["GenericAbbrevation"];
                        theDT.Rows.Add(theDR);
                    }

                    //Changes for Duplicate Drug Name--Amitava Sinha
                    ViewState["MasterDrugTable"] = theDT;
                }
              
                if (ViewState["OldDS"] == null)
                {
                    if (this.theExistDS.Tables.Count > 0 && this.theExistDS.Tables[0].Rows.Count == 0)
                    {
                        IQCareMsgBox.Show("NoPaediatricRecordExists", this);
                        return;
                    }
                    else
                    {
                        ViewState["OldDS"] = this.theExistDS;
                    }
                }
                if ((Convert.ToInt32(Session["PatientVisitId"]) > 0
                    && Convert.ToInt32(Session["PatientVisitId"]) != 0)
                    || isRefill)
                {
                    Session["AddARV"] = AddARV;
                    Session["OtherDrugs"] = OtherDrugs;
                    Session["AddTB"] = TBDrugs;
                    Session["VaccineDrugs"] = VaccineDrugs;
                    //Session["AddNonARV"] = NonARVDrugs;
                    Session["OIDrugs"] = OIDrugs;
                    ViewState["PharmacyId"] = Session["PatientVisitId"].ToString();
                }
                else if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
                {
                }
                else if (Request.QueryString["name"] == "Delete")
                {
                }
            }
            else
            {
                if (PrintPresFlag == 0)
                {
                    #region "Additional ARV"
                    if ((DataTable)Application["AddARV"] != null)
                    {
                        Session["AddARV"] = (DataTable)Application["AddARV"];
                        ViewState["MasterDrugTable"] = (DataTable)Application["MasterData"];
                        Application.Remove("MasterData");
                        Application.Remove("AddARV");
                    }

                    if ((DataTable)Session["AddARV"] != null)
                    {
                        DataTable theDT = (DataTable)Session["AddARV"];
                        LoadAdditionalDrugs(theDT, pnlPedia);
                    }
                    #endregion "Additional ARV"

                    #region "Additional Other Medications"
                    if ((DataTable)Application["OtherDrugs"] != null)
                    {
                        Session["OtherDrugs"] = (DataTable)Application["OtherDrugs"];
                        Application.Remove("OtherDrugs");
                        ViewState["MasterDrugTable"] = (DataTable)Application["MasterData"];
                        Application.Remove("MasterData");
                    }
                    if ((DataTable)Session["OtherDrugs"] != null)
                    {
                        DataTable theDT = (DataTable)Session["OtherDrugs"];
                        LoadAdditionalDrugs(theDT, PnlOtMed);
                    }
                    #endregion "Additional Other Medications"
                    #region "Additional OI Medications"
                    if ((DataTable)Application["OIDrugs"] != null)
                    {
                        Session["OIDrugs"] = (DataTable)Application["OIDrugs"];
                        Application.Remove("OIDrugs");
                        ViewState["MasterDrugTable"] = (DataTable)Application["MasterData"];
                        Application.Remove("MasterData");
                    }
                    if ((DataTable)Session["OIDrugs"] != null)
                    {
                        DataTable theDT = (DataTable)Session["OIDrugs"];
                        LoadAdditionalDrugs(theDT, PnlOIARV);
                    }
                    #endregion "Additional OI Medications"
                    #region "TB Drugs"
                    if ((DataTable)Application["AddTB"] != null)
                    {
                        Session["AddTB"] = (DataTable)Application["AddTB"];
                        ViewState["MasterDrugTable"] = (DataTable)Application["MasterData"];
                        Application.Remove("MasterData");
                        Application.Remove("AddTB");
                    }
                    if ((DataTable)Session["AddTB"] != null)
                    {
                        DataTable theDT = (DataTable)Session["AddTB"];
                        LoadAdditionalDrugs(theDT, pnlOtherTBMedicaton);
                    }
                    #endregion "TB Drugs"

                    #region "Vaccines"
                    if ((DataTable)Application["VaccineDrugs"] != null)
                    {
                        Session["VaccineDrugs"] = (DataTable)Application["VaccineDrugs"];
                        ViewState["MasterDrugTable"] = (DataTable)Application["MasterData"];
                        Application.Remove("MasterData");
                        Application.Remove("VaccineDrugs");
                    }
                    if ((DataTable)Session["VaccineDrugs"] != null)
                    {
                        DataTable theDT = (DataTable)Session["VaccineDrugs"];
                        LoadAdditionalDrugs(theDT, panelVaccine);
                    }
                    #endregion "Vaccines"

                    CreateControls((DataSet)ViewState["MasterData"]);
                }
            }

            if (Session["HIVPatientStatus"].ToString() == "1" && Session["PMTCTPatientStatus"].ToString() == "1")
            {
                btnsave.Enabled = false;
            }
            if (Session["CareEndFlag"].ToString() == "1")
            {
                btnsave.Enabled = true;
            }
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script language='javascript' type='text/javascript'>fnCheckUnCheck();</script>");

            #region "Check ARTStop"

            //IDrug theValidationManger = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BDrug,BusinessProcess.Pharmacy");
            //DataTable theValidateDT = theValidationManger.CheckARTStopStatus(Convert.ToInt32(Session["PatientId"]));
            //if (theValidateDT != null && theValidateDT.Rows.Count > 0 && theValidateDT.Rows[0]["ARTStatus"].ToString() == "ART Stopped" && theValidateDT.Rows[0]["ARTEndDate"] != DBNull.Value)
            //{
            //    DateTime artEndDate = Convert.ToDateTime(theValidateDT.Rows[0]["ARTEndDate"]);
            //    int treatment = 0;
            //    int.TryParse(ddlTreatment.SelectedValue, out treatment);
            //    if (Convert.ToInt32(Session["PatientVisitId"]) > 0 && theExistDS.Tables[0].Rows[0]["DispensedByDate"] != DBNull.Value)
            //    {
            //        DateTime theReportedbyDate = Convert.ToDateTime(theExistDS.Tables[0].Rows[0]["DispensedByDate"]);

            //        if (theReportedbyDate >= artEndDate && treatment == 222)
            //        {
            //            pnlPedia.Enabled = false;
            //        }
            //    }
            //    else if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            //    {
            //        if (treatment == 222)
            //        {
            //            pnlPedia.Enabled = false;
            //        }
            //    }
            //    else
            //    {
            //        pnlPedia.Enabled = true;
            //    }
            //}

            //if (
            //    (theValidateDT.Rows.Count > 0 &&
            //    theValidateDT.Rows[0]["ARTStatus"].ToString() == "ART Stopped" &&
            //    Convert.ToInt32(Session["PatientVisitId"]) == 0 &&
            //    Convert.ToInt32(ddlTreatment.SelectedValue) == 222
            //    ) ||
            //    (
            //        theValidateDT.Rows.Count > 0 &&
            //        theValidateDT.Rows[0]["ARTStatus"].ToString() == "ART Stopped" &&
            //        Convert.ToInt32(Session["PatientVisitId"]) > 0 &&
            //        Convert.ToDateTime(txtpharmReportedbyDate.Value) >= Convert.ToDateTime(theValidateDT.Rows[0]["ARTEndDate"]) &&
            //        Convert.ToInt32(ddlTreatment.SelectedValue) == 222
            //    )
            //    )
            //{
            //    pnlPedia.Enabled = false;
            //}
            //else
            //{
            //    pnlPedia.Enabled = true;
            //}

            //theValidationManger = null;
            //theValidateDT.Dispose();
            DateTime? dispensedDate = null;
            if (txtpharmReportedbyDate.Value.Trim() != "")
            {
                dispensedDate = Convert.ToDateTime(txtpharmReportedbyDate.Value);
            }

            pnlPedia.Enabled = !CheckARTStop(dispensedDate);
            #endregion "Check ARTStop"
        }

        private bool CheckARTStop(DateTime? dispensedDate)
        {
            IDrug theValidationManger = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BDrug,BusinessProcess.Pharmacy");
            DataTable theValidateDT = theValidationManger.CheckARTStopStatus(Convert.ToInt32(Session["PatientId"]));

            bool artStopped = false;
            if (theValidateDT != null && theValidateDT.Rows.Count > 0 && theValidateDT.Rows[0]["ARTStatus"].ToString() == "ART Stopped" && theValidateDT.Rows[0]["ARTEndDate"] != DBNull.Value)
            {
                DateTime artEndDate = Convert.ToDateTime(theValidateDT.Rows[0]["ARTEndDate"]);
                int treatment = 0;
                int.TryParse(ddlTreatment.SelectedValue, out treatment);
                if (Convert.ToInt32(Session["PatientVisitId"]) > 0 && dispensedDate != null)
                {
                    DateTime theReportedbyDate = Convert.ToDateTime(theExistDS.Tables[0].Rows[0]["DispensedByDate"]);

                    if (theReportedbyDate >= artEndDate && treatment == 222)
                    {
                        artStopped = true;
                    }
                }
                else if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
                {
                    if (treatment == 222)
                    {
                        artStopped = true;
                    }
                }
                else
                {
                    artStopped = false;
                }
            }
            return artStopped;
        }

        /// <summary>
        /// Saves the pharmacy form.
        /// </summary>
        /// <param name="printflag">The printflag.</param>
        protected void SavePharmacyForm(int printflag)
        {
            bool blnQtyDispensed = false;
            int patientId, dispensedBy, orderBy, orderType, employeeId = 0, signature;//, LocationID;
            decimal height = 0, weight = 0;
            int periodTaken = 0;
            //  Boolean flag1 = false;
            incompflag = 0;
            if (FieldValidation() == false)
            {
                incompflag = 1;
                return;
            }
            DataSet theDrgMst = ((DataSet)ViewState["MasterData"]);
            ViewState.Remove("Data");

            ViewState["Data"] = MakeDrugTable(pnlPedia);
            if (((DataTable)ViewState["Data"]).Rows.Count > 0)
            {
                if (ViewState["PharmacyFlag"].ToString() == "1" && ddlregimenLine.SelectedIndex == 0)
                {
                    IQCareMsgBox.Show("RegimenLineExists", this);
                    incompflag = 1;
                    return;
                }

                if (trHIVsetFields.Visible == true && ddlregimenLine.SelectedIndex == 0)
                {
                    IQCareMsgBox.Show("RegimenLineExists", this);
                    incompflag = 1;
                    return;
                }
                DataRow[] theFilDT = ((DataTable)ViewState["Data"]).Select("DrugId=99999");
                if (theFilDT.Length > 0)
                {
                    ViewState.Remove("Data");
                    IQCareMsgBox.Show("PharmacyIncompleteData", this);
                    incompflag = 1;
                    return;
                }
            }
            if (ddlTreatment.SelectedValue.ToString() == "222")
            {
                if (DuplicateRegimenValidate((DataTable)ViewState["Data"], (DataSet)ViewState["MasterData"]) == false)
                {
                    incompflag = 1;
                    return;
                }
            }

            DataTable _dt = MakeDrugTable(PnlOtMed);
            ViewState["Data"] = _dt;
            if (((DataTable)ViewState["Data"]).Rows.Count > 0)
            {
                DataRow[] theFilDT = ((DataTable)ViewState["Data"]).Select("DrugId=99999");

                if (theFilDT.Length > 0)
                {
                    ViewState.Remove("Data");
                    IQCareMsgBox.Show("PharmacyIncompleteData", this);
                    incompflag = 1;
                    return;
                }
            }
            DataTable _dtVac = MakeDrugTable(panelVaccine);
            ViewState["Data"] = _dtVac;
            if (((DataTable)ViewState["Data"]).Rows.Count > 0)
            {
                DataRow[] theFilDT = ((DataTable)ViewState["Data"]).Select("DrugId=99999");

                if (theFilDT.Length > 0)
                {
                    ViewState.Remove("Data");
                    IQCareMsgBox.Show("PharmacyIncompleteData", this);
                    incompflag = 1;
                    return;
                }
            }
            //ViewState["Data"] = MakeInfantHealthSection();
            DataTable theDT = MakeDrugTable(pnlOtherTBMedicaton);
            if (theDT.Rows.Count > 0)
            {
                DataRow[] theFilDT = theDT.Select("DrugId=99999");
                if (theFilDT.Length > 0)
                {
                    ViewState.Remove("Data");
                    IQCareMsgBox.Show("PharmacyIncompleteData", this);
                    incompflag = 1;
                    return;
                }
            }

            if (theDT.Rows.Count == 0)
            {
                IQCareMsgBox.Show("PharmacyIncompleteData", this);
                incompflag = 1;
                return;
            }

            if (ddlTreatment.SelectedValue.ToString() == "223")
            {
                //if (ProPhalaxisCheck(theDT) == false)
                //{
                //    IQCareMsgBox.Show("PharmacyIncompleteData", this);
                //    incompflag = 1;
                //    return;
                //}
            }

            if (Session["Paperless"].ToString() == "1")
            {
                if (Session["SCMModule"] != null)
                {
                    //blnQtyDispensed = true;
                }
                else
                {
                    for (int i = 0; i < theDT.Rows.Count; i++)
                    {
                        if (theDT.Rows[i]["QtyDispensed"].ToString() != "0" && theDT.Rows[i]["QtyDispensed"].ToString() != "")
                        {
                            blnQtyDispensed = true;
                        }
                    }
                }

                if (blnQtyDispensed == true)
                {
                    if (fieldValidationPaperLess() == false)
                    {
                        incompflag = 1;
                        return;
                    }
                }
                if (blnQtyDispensed == true)
                {
                    if (QuantityDispensed() == false)
                    {
                        incompflag = 1;
                        return;
                    }
                }
            }

            IQCareUtils theUtils = new IQCareUtils();
            IPediatric bPedMgr = (IPediatric)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BPediatric, BusinessProcess.Pharmacy");
            try
            {
                patientId = Convert.ToInt32(Session["PatientId"]);
                int locationId = 0;
                if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
                    locationId = Convert.ToInt32(Session["AppLocationId"]);
                else
                    locationId = Convert.ToInt32(Session["ServiceLocationId"].ToString());

                periodTaken = Convert.ToInt32(ddlPeriodTaken.SelectedItem.Value);
                orderBy = Convert.ToInt32(ddlPharmOrderedbyName.SelectedValue.ToString());
                dispensedBy = Convert.ToInt32(ddlDispensedBy.SelectedValue.ToString());
                theTreatmentID = Convert.ToInt32(ddlTreatment.SelectedValue.ToString());
                signature = employeeId = Convert.ToInt32(ddlPharmSignature.SelectedValue.ToString());
                //  signature = 1;
                theProviderID = Convert.ToInt32(ddlProvider.SelectedValue.ToString());
                if (txtHeight.Text != "")
                    height = Convert.ToDecimal(txtHeight.Text);
                if (txtWeight.Text != "")
                    weight = Convert.ToDecimal(txtWeight.Text);
                if (txtpharmOrderedbyDate.Value == "")
                {
                    thepharmOrderedbyDate = Convert.ToDateTime(theUtils.MakeDate("01-01-1900"));
                }
                else
                {
                    thepharmOrderedbyDate = Convert.ToDateTime(theUtils.MakeDate(txtpharmOrderedbyDate.Value));
                }

                if (txtpharmReportedbyDate.Value == "")
                {
                    thepharmReportedbyDate = Convert.ToDateTime(theUtils.MakeDate("01-01-1900"));
                }
                else
                {
                    thepharmReportedbyDate = Convert.ToDateTime(theUtils.MakeDate(txtpharmReportedbyDate.Value));
                }

                if (txtpharmAppntDate.Value == "")
                {
                    theAppntDate = Convert.ToDateTime(theUtils.MakeDate("01-01-1900"));
                }
                else
                {
                    theAppntDate = Convert.ToDateTime(theUtils.MakeDate(txtpharmAppntDate.Value));
                }
                theAppntReason = Convert.ToInt32(ddlAppntReason.SelectedValue);

                decimal AgeD = Convert.ToDecimal(txtYr.Text.Trim().ToString()) + Convert.ToDecimal(txtMon.Text.Trim().ToString()) / 12;
                if (AgeD >= 14)
                {
                    orderType = 116;
                }
                else
                    orderType = 117;

                ViewState["RegimenLine"] = ddlregimenLine.SelectedValue;

                int SCMFlag;//if SCM Module is On then SCMFlag=1 else SCMFlag=2
                if (Session["SCMModule"] != null)
                    SCMFlag = 1;
                else
                    SCMFlag = 2;

                foreach (DataRow theDR in theDT.Rows)
                {
                    if (theDR["PrintPrescriptionStatus"].ToString() == "1")
                    {
                        Session["PrintStatus"] = 1;
                    }
                }
                if ((Convert.ToInt32(Session["PatientVisitId"]) == 0) && (ViewState["PharmacyDetail"] == null || Convert.ToInt32(ViewState["PharmacyDetail"]) == 0))
                {
                    if (ViewState["PharmacyDetail"] == null || Convert.ToInt32(ViewState["PharmacyDetail"]) == 0)
                    {
                        CustomFieldClinical theCustomManager = new CustomFieldClinical();
                        DataTable theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Insert", ApplicationAccess.PaediatricPharmacy, (DataSet)ViewState["CustomFieldsDS"]);

                        ViewState["PharmacyDetail"] = bPedMgr.SaveUpdatePaediatricDetail(patientId, 0, Convert.ToInt32(ViewState["LocationId"]),
                            Convert.ToInt32(ViewState["RegimenLine"]), txtClinicalNotes.Text, theDT, theDrgMst, orderBy, Convert.ToDateTime(thepharmOrderedbyDate), dispensedBy,
                            Convert.ToDateTime(thepharmReportedbyDate), signature, employeeId, orderType, VisitType, Convert.ToInt32(ViewState["UserID"]), height, weight,
                            Convert.ToInt32(ViewState["flagFDC"]), theTreatmentID, theProviderID, theCustomDataDT, periodTaken, 1, SCMFlag, theAppntDate, theAppntReason,
                            this.ModuleId);
                        ViewState["PharmacyId"] = Convert.ToInt32(ViewState["PharmacyDetail"].ToString());
                        Session["PharmacyId"] = Convert.ToInt32(ViewState["PharmacyDetail"].ToString());
                        Session["PatientVisitId"] = Session["PharmacyId"];
                        if (Convert.ToInt32(ViewState["PharmacyDetail"]) == 0)
                        {
                            IQCareMsgBox.Show("PharmacyDetailExists", this);
                            return;
                        }
                        else
                        {
                            if ((Convert.ToString(Session["PrintStatus"]) == "1") || (Session["PrintStatus"] == null))
                            {
                                SaveCancel(printflag);
                            }
                            else
                            {
                                if (printflag == 0)
                                {
                                    SaveCancel(2);
                                }
                                else
                                    SaveCancel(printflag);
                            }
                        }

                        theDT.Rows.Clear();
                    }
                }
                else
                {
                    int pharmacyId = Convert.ToInt32(Session["PharmacyId"]);
                    if (pharmacyId != 0)
                    {
                        ViewState["PharmacyDetail"] = pharmacyId;
                    }

                    CustomFieldClinical theCustomManager = new CustomFieldClinical();
                    DataTable theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Update", ApplicationAccess.PaediatricPharmacy, (DataSet)ViewState["CustomFieldsDS"]);
                    ViewState["PharmacyDetail"] = bPedMgr.SaveUpdatePaediatricDetail(patientId, Convert.ToInt32(ViewState["PharmacyDetail"]), Convert.ToInt32(Session["AppLocationId"]), Convert.ToInt32(ViewState["RegimenLine"]), txtClinicalNotes.Text, theDT, theDrgMst, orderBy, Convert.ToDateTime(thepharmOrderedbyDate), dispensedBy, Convert.ToDateTime(thepharmReportedbyDate), signature, employeeId, orderType, VisitType, Convert.ToInt32(ViewState["UserID"]), height, weight, Convert.ToInt32(ViewState["flagFDC"]), theTreatmentID, theProviderID, theCustomDataDT, periodTaken, 2, SCMFlag, theAppntDate, theAppntReason, this.ModuleId); // rupesh 19-sep-07 for ARV Provider
                    ViewState["PharmacyId"] = Convert.ToInt32(pharmacyId);
                    Session["PharmacyId"] = pharmacyId.ToString();
                    Session["PatientVisitId"] = Session["PharmacyId"];

                    if (Convert.ToInt32(ViewState["PharmacyDetail"]) == 0)
                    {
                        IQCareMsgBox.Show("ErrorinSavingPaediatricDetail", this);
                    }
                    else
                    {
                        //IQCareMsgBox.Show("PaediatricDetailUpdate", this);
                    }
                    if (Convert.ToInt32(ViewState["PharmacyDetail"]) != 0)
                    {
                        if ((Convert.ToString(Session["PrintStatus"]) == "1") || (Session["PrintStatus"] == null))
                        {
                            SaveCancel(printflag);
                        }
                        else
                        {
                            if (printflag == 0)
                            {
                                SaveCancel(2);
                            }
                            else
                                SaveCancel(printflag);
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("C1#", theMsg, this);
            }
            finally
            {
                bPedMgr = null;
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the txtautoDrugName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void txtautoDrugName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                IQCareUtils theUtils = new IQCareUtils();
                DataView theAutoDV;
                DataView theExistsDV;
                DataSet theAutoDS = (DataSet)ViewState["MasterData"];
                int DrugId;
                if (hdCustID.Value != "")
                {
                    if ((Convert.ToInt32(hdCustID.Value) != 0))
                    {
                        DrugId = Convert.ToInt32(hdCustID.Value);
                        theAutoDV = new DataView(theAutoDS.Tables[0]);

                        theAutoDV.RowFilter = "Drug_Pk = " + DrugId;
                        DataTable theAutoDT = theUtils.CreateTableFromDataView(theAutoDV);
                        if (theAutoDT.Rows[0]["DrugTypeId"].ToString() == "37")
                        {
                            if (Session["AddARV"] == null)
                            {
                                DataTable theDT = new DataTable();
                                theDT.Columns.Add("DrugId", Type.GetType("System.Int32"));
                                theDT.Columns.Add("DrugName", Type.GetType("System.String"));
                                theDT.Columns.Add("Generic", Type.GetType("System.Int32"));
                                theDT.Columns.Add("DrugTypeId", Type.GetType("System.Int32"));
                                //theDT.Columns.Add("StrengthId", System.Type.GetType("System.Int32"));
                                Session["AddARV"] = theDT;
                            }
                            DataTable ExistDT = (DataTable)Session["AddARV"];
                            theExistsDV = new DataView(ExistDT);
                            theExistsDV.RowFilter = "DrugId =" + theAutoDT.Rows[0]["Drug_pk"];
                            DataTable theSelExistsDT = theUtils.CreateTableFromDataView(theExistsDV);
                            if (theSelExistsDT.Rows.Count == 0)
                            {
                                DataRow DR = ExistDT.NewRow();
                                DR[0] = theAutoDT.Rows[0]["Drug_pk"];
                                DR[1] = theAutoDT.Rows[0]["DrugName"];
                                DR[2] = 0;
                                DR[3] = theAutoDT.Rows[0]["DrugTypeId"];
                                //DR[4] = theAutoDT.Rows[0]["StrengthId"];
                                ExistDT.Rows.Add(DR);
                                LoadAdditionalDrugs(ExistDT, pnlPedia);
                                Session["AddARV"] = ExistDT;
                                if (trHIVsetFields.Visible == false)
                                {
                                    trHIVsetFields.Visible = true;
                                }
                            }
                            else
                            {
                                IQCareMsgBox.Show("DrugExists", this);
                                txtautoDrugName.Text = "";
                                LoadAdditionalDrugs(ExistDT, pnlPedia);
                                return;
                            }
                        }
                        else if (theAutoDT.Rows[0]["DrugTypeId"].ToString() == "31")
                        {
                            if (Session["AddTB"] == null)
                            {
                                DataTable theTBDT = new DataTable();
                                theTBDT.Columns.Add("DrugId", Type.GetType("System.Int32"));
                                theTBDT.Columns.Add("DrugName", Type.GetType("System.String"));
                                theTBDT.Columns.Add("Generic", Type.GetType("System.Int32"));
                                theTBDT.Columns.Add("DrugTypeId", Type.GetType("System.Int32"));
                                //theTBDT.Columns.Add("StrengthId", System.Type.GetType("System.Int32"));
                                Session["AddTB"] = theTBDT;
                            }
                            DataTable ExistTBDT = (DataTable)Session["AddTB"];
                            theExistsDV = new DataView(ExistTBDT);
                            theExistsDV.RowFilter = "DrugId =" + theAutoDT.Rows[0]["Drug_pk"];
                            DataTable theSelExistsDT = theUtils.CreateTableFromDataView(theExistsDV);
                            if (theSelExistsDT.Rows.Count == 0)
                            {
                                DataRow DR = ExistTBDT.NewRow();
                                DR[0] = theAutoDT.Rows[0]["Drug_pk"];
                                DR[1] = theAutoDT.Rows[0]["DrugName"];
                                DR[2] = 0;
                                DR[3] = theAutoDT.Rows[0]["DrugTypeId"];
                                //DR[4] = theAutoDT.Rows[0]["StrengthId"];
                                ExistTBDT.Rows.Add(DR);
                                LoadAdditionalDrugs(ExistTBDT, pnlOtherTBMedicaton);
                                Session["AddTB"] = ExistTBDT;
                            }
                            else
                            {
                                IQCareMsgBox.Show("DrugExists", this);
                                txtautoDrugName.Text = "";
                                LoadAdditionalDrugs(ExistTBDT, pnlOtherTBMedicaton);
                                return;
                            }
                        }
                        else if (theAutoDT.Rows[0]["DrugTypeId"].ToString() == "36")
                        {
                            if (Session["OIDrugs"] == null)
                            {
                                DataTable theOthDT = new DataTable();
                                theOthDT.Columns.Add("DrugId", Type.GetType("System.Int32"));
                                theOthDT.Columns.Add("DrugName", Type.GetType("System.String"));
                                theOthDT.Columns.Add("Generic", Type.GetType("System.Int32"));
                                theOthDT.Columns.Add("DrugTypeId", Type.GetType("System.Int32"));
                                //theOthDT.Columns.Add("StrengthId", System.Type.GetType("System.Int32"));
                                Session["OIDrugs"] = theOthDT;
                            }
                            DataTable ExistOIDT = (DataTable)Session["OIDrugs"];
                            theExistsDV = new DataView(ExistOIDT);
                            theExistsDV.RowFilter = "DrugId =" + theAutoDT.Rows[0]["Drug_pk"];
                            DataTable theSelExistsDT = theUtils.CreateTableFromDataView(theExistsDV);
                            if (theSelExistsDT.Rows.Count == 0)
                            {
                                DataRow DR = ExistOIDT.NewRow();
                                DR[0] = theAutoDT.Rows[0]["Drug_pk"];
                                DR[1] = theAutoDT.Rows[0]["DrugName"];
                                DR[2] = 0;
                                DR[3] = theAutoDT.Rows[0]["DrugTypeId"];
                                //DR[4] = theAutoDT.Rows[0]["StrengthId"];
                                ExistOIDT.Rows.Add(DR);
                                LoadAdditionalDrugs(ExistOIDT, PnlOIARV);
                                Session["OIDrugs"] = ExistOIDT;
                            }
                            else
                            {
                                IQCareMsgBox.Show("DrugExists", this);
                                txtautoDrugName.Text = "";
                                LoadAdditionalDrugs(ExistOIDT, PnlOIARV);
                                return;
                            }
                        }
                        else if (theAutoDT.Rows[0]["DrugTypeId"].ToString() == "60")
                        {
                            if (Session["VaccineDrugs"] == null)
                            {
                                DataTable theVacDT = new DataTable();
                                theVacDT.Columns.Add("DrugId", Type.GetType("System.Int32"));
                                theVacDT.Columns.Add("DrugName", Type.GetType("System.String"));
                                theVacDT.Columns.Add("Generic", Type.GetType("System.Int32"));
                                theVacDT.Columns.Add("DrugTypeId", Type.GetType("System.Int32"));
                                //theOthDT.Columns.Add("StrengthId", System.Type.GetType("System.Int32"));
                                Session["VaccineDrugs"] = theVacDT;
                            }
                            DataTable existVacDT = (DataTable)Session["VaccineDrugs"];
                            theExistsDV = new DataView(existVacDT);
                            theExistsDV.RowFilter = "DrugId =" + theAutoDT.Rows[0]["Drug_pk"];
                            DataTable theSelExistsDT = theUtils.CreateTableFromDataView(theExistsDV);
                            if (theSelExistsDT.Rows.Count == 0)
                            {
                                DataRow DR = existVacDT.NewRow();
                                DR[0] = theAutoDT.Rows[0]["Drug_pk"];
                                DR[1] = theAutoDT.Rows[0]["DrugName"];
                                DR[2] = 0;
                                DR[3] = theAutoDT.Rows[0]["DrugTypeId"];
                                //DR[4] = theAutoDT.Rows[0]["StrengthId"];
                                existVacDT.Rows.Add(DR);
                                LoadAdditionalDrugs(existVacDT, panelVaccine);
                                Session["VaccineDrugs"] = existVacDT;
                            }
                            else
                            {
                                IQCareMsgBox.Show("DrugExists", this);
                                txtautoDrugName.Text = "";
                                LoadAdditionalDrugs(existVacDT, panelVaccine);
                                return;
                            }
                        }
                        else
                        {
                            if (Session["OtherDrugs"] == null)
                            {
                                DataTable theOthDT = new DataTable();
                                theOthDT.Columns.Add("DrugId", Type.GetType("System.Int32"));
                                theOthDT.Columns.Add("DrugName", Type.GetType("System.String"));
                                theOthDT.Columns.Add("Generic", Type.GetType("System.Int32"));
                                theOthDT.Columns.Add("DrugTypeId", Type.GetType("System.Int32"));
                                //theOthDT.Columns.Add("StrengthId", System.Type.GetType("System.Int32"));
                                Session["OtherDrugs"] = theOthDT;
                            }
                            DataTable ExistOthDT = (DataTable)Session["OtherDrugs"];
                            theExistsDV = new DataView(ExistOthDT);
                            theExistsDV.RowFilter = "DrugId =" + theAutoDT.Rows[0]["Drug_pk"];
                            DataTable theSelExistsDT = theUtils.CreateTableFromDataView(theExistsDV);
                            if (theSelExistsDT.Rows.Count == 0)
                            {
                                DataRow DR = ExistOthDT.NewRow();
                                DR[0] = theAutoDT.Rows[0]["Drug_pk"];
                                DR[1] = theAutoDT.Rows[0]["DrugName"];
                                DR[2] = 0;
                                DR[3] = theAutoDT.Rows[0]["DrugTypeId"];
                                //DR[4] = theAutoDT.Rows[0]["StrengthId"];
                                ExistOthDT.Rows.Add(DR);
                                LoadAdditionalDrugs(ExistOthDT, PnlOtMed);
                                Session["OtherDrugs"] = ExistOthDT;
                            }
                            else
                            {
                                IQCareMsgBox.Show("DrugExists", this);
                                txtautoDrugName.Text = "";
                                LoadAdditionalDrugs(ExistOthDT, PnlOtMed);
                                return;
                            }
                        }
                        if (Convert.ToInt32(Session["PatientVisitId"]) > 0 && Session["ExistPharmacyData"] != null)
                        {
                            foreach (DataRow dr in ((DataTable)(Session["ExistPharmacyData"])).Rows)
                            {
                                FillOldData(PnlOIARV, dr, false);
                                FillOldData(pnlPedia, dr, false);
                                FillOldData(PnlOtMed, dr, false);
                                FillOldData(pnlOtherTBMedicaton, dr, false);
                                FillOldData(panelVaccine, dr, false);
                                //FillMaternalHealth(dr);
                            }
                        }

                        txtautoDrugName.Text = "";
                    }
                }
                //gvCustomer.DataSource = GetCustomerDetail(Convert.ToInt32(hdCustID.Value));
                //gvCustomer.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //}
        }

        /// <summary>
        /// Handles the TextChanged event of the txtDose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void txtDose_TextChanged(object sender, EventArgs e)
        {
            SelectFrequency(pnlPedia);
            SelectFrequency(PnlOtMed);
        }

        /// <summary>
        /// Sorts the data table.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="sort">The sort.</param>
        private static void SortDataTable(DataTable dt, string sort)
        {
            DataTable newDT = dt.Clone();
            int rowCount = dt.Rows.Count;

            DataRow[] foundRows = dt.Select(null, sort);
            for (int i = 0; i < rowCount; i++)
            {
                object[] arr = new object[dt.Columns.Count];
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    arr[j] = foundRows[i][j];
                }
                DataRow data_row = newDT.NewRow();
                data_row.ItemArray = arr;
                newDT.Rows.Add(data_row);
            }

            //clear the incoming dt
            dt.Rows.Clear();

            for (int i = 0; i < newDT.Rows.Count; i++)
            {
                object[] arr = new object[dt.Columns.Count];
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    arr[j] = newDT.Rows[i][j];
                }

                DataRow data_row = dt.NewRow();
                data_row.ItemArray = arr;
                dt.Rows.Add(data_row);
            }
        }

        private void BindScheduleList(int drugId, ref DropDownList ddlVacSchedule)
        {
            IDrugMst drgMst = (IDrugMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDrugMst, BusinessProcess.Administration");

            DataTable dtSchedule = drgMst.GetScheduleByDrugID(drugId);
            BindFunctions theBindMgr = new BindFunctions();
            theBindMgr.BindCombo(ddlVacSchedule, dtSchedule, "Name", "Id");
        }

        /// <summary>
        /// Adds the controls attributes.
        /// </summary>
        /// <param name="theContainer">The container.</param>
        private void AddControlsAttributes(Control theContainer)
        {
            foreach (Control x in theContainer.Controls)
            {
                if (x.GetType() == typeof(Panel))
                {
                    foreach (Control y in x.Controls)
                    {
                        if (y.GetType() == typeof(Panel))
                        {
                            AddControlsAttributes(y);
                        }
                        else
                        {
                            if (y.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                            {
                                ((TextBox)y).Attributes.Add("onkeyup", "chkNumeric('" + ((TextBox)y).ClientID + "')");
                            }
                        }
                    }
                }
            }
        }

        // bool isRefill = false;
        /// <summary>
        /// Authenticates this instance.
        /// </summary>
        private void Authenticate()
        {
            /***************** Check For User Rights ****************/
            AuthenticationManager Authentiaction = new AuthenticationManager();
            if (Authentiaction.HasFunctionRight(ApplicationAccess.AdultPharmacy, FunctionAccess.Print, (DataTable)Session["UserRight"]) == false)
            {
                btnPrint.Enabled = false;
            }

            if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            {
                if (Authentiaction.HasFunctionRight(ApplicationAccess.AdultPharmacy, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
                {
                    btnsave.Enabled = false;
                }
                if (Request.QueryString["Prog"] == "PMTCT")
                {
                    ddlTreatment.SelectedValue = "223";
                    //ddlTreatment.Enabled = false;
                }
                btnRandom.Enabled = false;
            }
            else if (Convert.ToInt32(Session["PatientVisitId"]) != 0)
            {
                if (Authentiaction.HasFunctionRight(ApplicationAccess.AdultPharmacy, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                {
                    int PatientID = Convert.ToInt32(Session["PatientId"]);
                    string theUrl = "";
                    theUrl = string.Format("{0}?PatientId={1}", "../ClinicalForms/frmPatient_History.aspx", PatientID);
                    Response.Redirect(theUrl);
                }
                else if (Authentiaction.HasFunctionRight(ApplicationAccess.AdultPharmacy, FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
                {
                    btnsave.Enabled = false;
                }

                int PID = Convert.ToInt32(Session["PatientId"]);
                Session["PharmacyId"] = Convert.ToInt32(Session["PatientVisitId"]);
                //FillOldCustomData(pnlCustomList, PID);
                FillOldCustomData(PID);
                btnRandom.Enabled = true;
            }
            else if (Request.QueryString["name"] == "Delete")
            {
                int PID = Convert.ToInt32(Session["PatientId"]);
                if (Authentiaction.HasFunctionRight(ApplicationAccess.AdultPharmacy, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                {
                    int PatientID = Convert.ToInt32(Session["PatientId"]);
                    string theUrl = "";
                    theUrl = string.Format("{0}?PatientId={1}", "../ClinicalForms/frmClinical_DeleteForm.aspx", PatientID);
                    Response.Redirect(theUrl);
                }
                else if (Authentiaction.HasFunctionRight(ApplicationAccess.AdultPharmacy, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == false)
                {
                    btnsave.Text = "Delete";

                    btnsave.Enabled = false;
                    // btnQualityCheck.Visible = false;
                }
                btnRandom.Enabled = false;
                FillOldCustomData(PID);
            }
        }

        /// <summary>
        /// Binds the tb drug controls.
        /// </summary>
        /// <param name="drugId">The drug identifier.</param>
        /// <param name="Generic">The generic.</param>
        /// <param name="MstPanel">The MST panel.</param>
        private void BindTBDrugControls(int drugId, int Generic, Panel MstPanel)
        {
            //oi and other medications
            //if (MstPanel.Controls.Count < 1)
            //{
            Control thehdrCntrl = FindControlRecursive(MstPanel, "pnlTBDrug" + MstPanel.ID);
            if (thehdrCntrl == null)
            {
                #region "TB Medication"
                Panel thelblPnl = new Panel();
                thelblPnl.ID = "pnlTBDrug" + MstPanel.ID;
                thelblPnl.Height = 20;
                thelblPnl.Width = 880;
                thelblPnl.Controls.Clear();

                Label theLabel = new Label();
                theLabel.ID = "lblTBDrug";
                theLabel.Text = "TB Medications";//earlier it was "OI Treatment and Other Medications";
                theLabel.Font.Bold = true;
                thelblPnl.Controls.Add(theLabel);
                MstPanel.Controls.Add(thelblPnl);

                /////////////////////////////////////////////////
                Panel theheaderPnl = new Panel();
                theheaderPnl.ID = "pnlHeaderTBDrug";
                theheaderPnl.Height = 20;
                theheaderPnl.Width = 880;
                theheaderPnl.Font.Bold = true;
                theheaderPnl.Controls.Clear();

                Label theSP = new Label();
                theSP.ID = "lblTBDrgSp";
                theSP.Width = 5;
                theSP.Text = "";
                theheaderPnl.Controls.Add(theSP);

                Label theLabel1 = new Label();
                theLabel1.ID = "lblTBDrgNm";
                theLabel1.Text = "Drug Name";
                theLabel1.Width = 200;
                theheaderPnl.Controls.Add(theLabel1);

                Label theLabel2 = new Label();
                theLabel2.ID = "lblTBDrgDose";
                theLabel2.Text = "Dose";
                theLabel2.Width = 45;
                theheaderPnl.Controls.Add(theLabel2);

                //Label theLabel3 = new Label();
                //theLabel3.ID = "lblTBDrgUnits";
                //theLabel3.Text = "Unit";
                //theLabel3.Width = 90;
                //theheaderPnl.Controls.Add(theLabel3);

                Label theLabel4 = new Label();
                theLabel4.ID = "lblTBDrgFrequency";
                theLabel4.Text = "Frequency";
                theLabel4.Width = 80;
                theheaderPnl.Controls.Add(theLabel4);

                Label theLabel5 = new Label();
                theLabel5.ID = "lblTBDrgDuration";
                theLabel5.Text = "Duration (Days)";
                theLabel5.Width = 55;
                theLabel5.Attributes.Add("Style", "text-align:center");
                theheaderPnl.Controls.Add(theLabel5);

                Label theLabel6 = new Label();
                theLabel6.ID = "lblTBDrgPrescribed";
                theLabel6.Text = "Quantity Prescribed";
                theLabel6.Width = 75;
                theLabel6.Attributes.Add("Style", "text-align:center");
                theheaderPnl.Controls.Add(theLabel6);

                Label theLabel7 = new Label();
                theLabel7.ID = "lblTBDrgDispensed";
                theLabel7.Text = "Quantity Dispensed";
                theLabel7.Width = 75;
                theLabel7.Attributes.Add("Style", "text-align:center");
                theheaderPnl.Controls.Add(theLabel7);

                //Label theLabel8 = new Label();
                //theLabel8.ID = "lblTBDrgFinanced";
                //theLabel8.Text = "AR";
                //theLabel8.Width = 20;
                //theheaderPnl.Controls.Add(theLabel8);

                Label theLabel9 = new Label();
                theLabel9.ID = "lblTBTreatmentPhase";
                theLabel9.Text = "Treatment Phase";
                theLabel9.Attributes.Add("Style", "text-align:center");
                theLabel9.Width = 75;
                theheaderPnl.Controls.Add(theLabel9);

                Label lblStSp2 = new Label();
                lblStSp2.Width = 10;
                lblStSp2.ID = "stSpace2" + drugId + "^" + Generic;
                lblStSp2.Text = "";
                theheaderPnl.Controls.Add(lblStSp2);

                Label theLabel10 = new Label();
                theLabel10.ID = "lblTBTreatmentMonth";
                theLabel10.Text = "Month";
                theLabel10.Width = 50;
                theheaderPnl.Controls.Add(theLabel10);

                Label lblProphylaxis = new Label();
                lblProphylaxis.Text = "Prophylaxis";
                lblProphylaxis.ID = "lblProphylaxis" + MstPanel.ID;
                lblProphylaxis.Font.Bold = true;
                lblProphylaxis.Visible = true;
                theheaderPnl.Controls.Add(lblProphylaxis);

                Label lblSpace8 = new Label();
                lblSpace8.Width = 15;
                lblSpace8.ID = "lblPrescriptionSpace" + MstPanel.ID;
                lblSpace8.Text = "";
                theheaderPnl.Controls.Add(lblSpace8);

                Label lblPrintPrescription = new Label();
                lblPrintPrescription.Text = "Print Prescription";
                lblPrintPrescription.Width = 70;
                lblPrintPrescription.Attributes.Add("Style", "text-align:center");
                lblPrintPrescription.ID = "lblPrintPrescription" + MstPanel.ID;
                lblPrintPrescription.Font.Bold = true;
                lblPrintPrescription.Visible = true;
                theheaderPnl.Controls.Add(lblPrintPrescription);

                MstPanel.Controls.Add(theheaderPnl);
                #endregion "TB Medication"
            }
            //}
            Control thedrgCntrl = FindControlRecursive(MstPanel, "pnl" + drugId + "^" + Generic);
            if (thedrgCntrl == null)
            {
                Panel thePnl = new Panel();
                thePnl.ID = "pnl" + drugId + "^" + Generic;
                //thePnl.Height = 30;
                thePnl.Width = 880;
                thePnl.Controls.Clear();

                Label lblStSp = new Label();
                lblStSp.Width = 5;
                lblStSp.ID = "stSpace" + drugId + "^" + Generic;
                lblStSp.Text = "";
                lblStSp.Height = 45;
                thePnl.Controls.Add(lblStSp);

                DataView theDV;
                DataSet theDS = (DataSet)ViewState["MasterData"];
                if (Generic == 0)
                {
                    //theDV = new DataView(theDS.Tables[0]);
                    theDV = new DataView(theDS.Tables[12]); // rupesh for both active & inactive drug 31/07/07
                    theDV.RowFilter = "drug_pk = " + drugId;
                }
                else
                {
                    theDV = new DataView(theDS.Tables[4]);
                    if (drugId.ToString().LastIndexOf("9999") > 0)
                    {
                        drugId = Convert.ToInt32(drugId.ToString().Substring(0, drugId.ToString().Length - 4));
                    }
                    theDV.RowFilter = "GenericId = " + drugId;
                    //theDV = new DataView(theDS.Tables[4]);
                    //theDV.RowFilter = "GenericId = " + DrugId;
                }

                Label theDrugNm = new Label();
                theDrugNm.ID = "drgNm" + drugId + "^" + Generic;
                theDrugNm.Text = theDV[0][1].ToString();
                theDrugNm.Width = 190;
                thePnl.Controls.Add(theDrugNm);

                /////// Space//////
                Label theSpace = new Label();
                theSpace.ID = "theSpace" + drugId + "^" + Generic;
                theSpace.Width = 10;
                theSpace.Text = "";
                thePnl.Controls.Add(theSpace);
                ////////////////////

                TextBox theDose = new TextBox();
                theDose.ID = "drgDose" + drugId + "^" + Generic;
                theDose.Width = 30;
                theDose.Text = "";
                theDose.Load += new EventHandler(Control_Load);
                thePnl.Controls.Add(theDose);

                BindFunctions theBindMgr = new BindFunctions();

                /////// Space//////
                Label theSpace2 = new Label();
                theSpace2.ID = "theSpace2*" + drugId + "^" + Generic;
                theSpace2.Width = 10;
                theSpace2.Text = "";
                thePnl.Controls.Add(theSpace2);
                ////////////////////

                DropDownList theFrequency = new DropDownList();

                theFrequency.ID = "drgFrequency" + drugId + "^" + Generic;
                theFrequency.Width = 70;
                DataTable DTFreq = new DataTable();
                //DTFreq = theDS.Tables[6]; // Rupesh 03-Sept
                DTFreq = theDS.Tables[8];
                theBindMgr.BindCombo(theFrequency, DTFreq, "FrequencyName", "FrequencyId");
                thePnl.Controls.Add(theFrequency);

                /////// Space//////
                Label theSpace3 = new Label();
                theSpace3.ID = "theSpace3*" + drugId + "^" + Generic;
                theSpace3.Width = 10;
                theSpace3.Text = "";
                thePnl.Controls.Add(theSpace3);
                ////////////////////

                TextBox theDuration = new TextBox();
                theDuration.ID = "drgDuration" + drugId + "^" + Generic;
                theDuration.Attributes.Add("OnBlur", "CalculateTotalDailyDose('ctl00_IQCareContentPlaceHolder_drgDose" + drugId + "^" + Generic + "', 'ctl00_IQCareContentPlaceHolder_drgFrequency" + drugId + "^" + Generic + "','ctl00_IQCareContentPlaceHolder_drgDuration" + drugId + "^" + Generic + "','ctl00_IQCareContentPlaceHolder_drgQtyPrescribed" + drugId + "^" + Generic + "');");
                theDuration.Width = 50;
                theDuration.Text = "";
                theDuration.Load += new EventHandler(Control_Load);
                thePnl.Controls.Add(theDuration);

                ////////////Space////////////////////////
                Label theSpace4 = new Label();
                theSpace4.ID = "theSpace4*" + drugId + "^" + Generic;
                theSpace4.Width = 10;
                theSpace4.Text = "";
                thePnl.Controls.Add(theSpace4);
                ////////////////////////////////////////

                TextBox theQtyPrescribed = new TextBox();
                theQtyPrescribed.ID = "drgQtyPrescribed" + drugId + "^" + Generic;

                theQtyPrescribed.Width = 60;
                theQtyPrescribed.Text = "";
                //theQtyPrescribed.Load += new EventHandler(DecimalText_Load);
                theQtyPrescribed.Load += new EventHandler(Control_Load);
                thePnl.Controls.Add(theQtyPrescribed);

                ////////////Space////////////////////////
                Label theSpace5 = new Label();
                theSpace5.ID = "theSpace5*" + drugId + "^" + Generic;
                theSpace5.Width = 10;
                theSpace5.Text = "";
                thePnl.Controls.Add(theSpace5);
                ////////////////////////////////////////

                TextBox theQtyDispensed = new TextBox();
                theQtyDispensed.ID = "drgQtyDispensed" + drugId + "^" + Generic;
                theQtyDispensed.Width = 60;
                theQtyDispensed.Text = "";
                #region "13-Jun-07 -3"
                //theQtyDispensed.Load += new EventHandler(DecimalText_Load);
                theQtyDispensed.Load += new EventHandler(Control_Load); // rupesh
                #endregion "13-Jun-07 -3"
                //if (Session["SCMModule"] != null)
                //    theQtyDispensed.Attributes.Add("onkeyup", "chknotwrite('" + theQtyDispensed.ClientID + "')");
                //    //theQtyDispensed.Enabled = false;
                thePnl.Controls.Add(theQtyDispensed);

                ////////////Space////////////////////////
                Label theSpace6 = new Label();
                theSpace6.ID = "theSpace6*" + drugId + "^" + Generic;
                theSpace6.Width = 10;
                theSpace6.Text = "";
                thePnl.Controls.Add(theSpace6);
                ////////////////////////////////////////

                //Treatment Phase
                DropDownList theTreatmenPhase = new DropDownList();
                theTreatmenPhase.ID = "drgTreatmenPhase" + drugId + "^" + Generic;
                theTreatmenPhase.Width = 70;
                DataTable DTTrPhase = new DataTable();
                DTTrPhase = MakeTreatmentPhase();
                theBindMgr.BindCombo(theTreatmenPhase, DTTrPhase, "Name", "Id");
                thePnl.Controls.Add(theTreatmenPhase);

                ////////////Space////////////////////////
                Label theSpace8 = new Label();
                theSpace8.ID = "theSpace8*" + drugId + "^" + Generic;
                theSpace8.Width = 10;
                theSpace8.Text = "";
                thePnl.Controls.Add(theSpace8);
                //////////////////////////////////////

                //Treatment Months

                DropDownList theTreatmenMonth = new DropDownList();
                theTreatmenMonth.ID = "drgTreatmenMonth" + drugId + "^" + Generic;
                theTreatmenMonth.Width = 60;
                DataTable DTTrMonth = new DataTable();
                DTTrMonth = MakeTreatmentMonth();
                theBindMgr.BindCombo(theTreatmenMonth, DTTrMonth, "Name", "Id");
                thePnl.Controls.Add(theTreatmenMonth);

                ////////////Space///////////////////////
                Label theSpace7 = new Label();
                theSpace7.ID = "theSpace7" + drugId;
                theSpace7.Width = 10;
                theSpace7.Text = "";
                thePnl.Controls.Add(theSpace7);

                CheckBox theOtherARTProPhChk = new CheckBox();
                theOtherARTProPhChk.ID = "chkProphylaxis" + drugId;
                theOtherARTProPhChk.Width = 10;
                theOtherARTProPhChk.Text = "";
                theOtherARTProPhChk.Enabled = true;
                //if (ddlTreatment.SelectedItem.Value.ToString() == "223" || ddlTreatment.SelectedItem.Value.ToString() == "224")
                //{
                //    theOtherARTProPhChk.Enabled = true;
                //}
                //else
                //    theOtherARTProPhChk.Enabled = false;
                thePnl.Controls.Add(theOtherARTProPhChk);
                //}

                //////print prescription TB////////////////
                ////////////Space///////////////////////
                Label theSpace10 = new Label();
                theSpace10.ID = "theSpace8" + drugId;
                theSpace10.Width = 80;
                theSpace10.Text = "";
                thePnl.Controls.Add(theSpace10);

                CheckBox printPrescriptionChk = new CheckBox();
                printPrescriptionChk.ID = "chkPrintPrescription" + drugId;
                printPrescriptionChk.Width = 10;
                printPrescriptionChk.Text = "";
                printPrescriptionChk.Attributes.Add("onclick", "javascript:return fnchecked('" + printPrescriptionChk.ClientID + "')");
                thePnl.Controls.Add(printPrescriptionChk);

                ////////////Space///////////////////////
                Label theSpace9 = new Label();
                theSpace9.ID = "theSpace9" + drugId;
                theSpace9.Width = 50;
                theSpace9.Text = "";
                thePnl.Controls.Add(theSpace9);

                thePnl.Controls.Add(CreateRemoveLinkButton(drugId, MstPanel.ID));

                /////Patient Instructions/////////
                thePnl.Controls.Add(new LiteralControl("<br />"));
                if (Request.Browser.Browser == "IE")
                {
                    thePnl.Controls.Add(new LiteralControl("<br />"));
                }
                Label ptnInstructions = new Label();
                ptnInstructions.ID = "lblPtnInstructions" + drugId;
                //ptnInstructions.Width = 215;
                ptnInstructions.Text = "Patient Instructions:  ";
                ptnInstructions.Font.Bold = true;
                thePnl.Controls.Add(ptnInstructions);

                TextBox ptnIns = new TextBox();
                ptnIns.Width = 700;
                ptnIns.ID = "txtPtnInstructions" + drugId;
                thePnl.Controls.Add(ptnIns);

                thePnl.Controls.Add(new LiteralControl("<script language='javascript' type='text/javascript' id = 'chkOrNot'>"));
                thePnl.Controls.Add(new LiteralControl("fnchecked('chkPrintPrescription" + drugId + "')"));
                thePnl.Controls.Add(new LiteralControl("</script>"));

                MstPanel.Controls.Add(thePnl);
                /////////Space panel/////////////////////////
                Panel thePnlspace = new Panel();
                thePnlspace.ID = "pnlspace_" + drugId;
                thePnlspace.Height = 3; //was 3 previously
                thePnlspace.Width = 880;
                thePnlspace.Controls.Clear();
                MstPanel.Controls.Add(thePnlspace);
            }
        }

        private void BindVaccineControls(int drugId, int genericId, Panel thePanel)
        {
            string panelId = "pnlVacDrug" + thePanel.ID;
            Control thehdrCntrl = FindControlRecursive(thePanel, panelId);
            if (thehdrCntrl == null)
            {
                Panel thelblPnl = new Panel();
                thelblPnl.ID = panelId;
                thelblPnl.Height = 20;
                thelblPnl.Width = 880;
                thelblPnl.Controls.Clear();

                //Label theLabel = new Label();
                //theLabel.ID = "lblVacDrug";
                //theLabel.Text = "Vaccinations";
                //theLabel.Font.Bold = true;
                //thelblPnl.Controls.Add(theLabel);
                //thePanel.Controls.Add(thelblPnl);

                thePanel.Controls.Add(new LiteralControl("<div class='panel-heading'><strong>Vaccinations</strong></div>"));

                /////////////////////////////////////////////////
                Panel theheaderPnl = new Panel();
                theheaderPnl.ID = "pnlHeaderVacDrug";
                theheaderPnl.Height = 20;
                theheaderPnl.Width = 880;
                theheaderPnl.Font.Bold = true;
                theheaderPnl.Controls.Clear();

                Label theSP = new Label();
                theSP.ID = "lblVacDrgSp";
                theSP.Width = 5;
                theSP.Text = "";
                theheaderPnl.Controls.Add(theSP);

                Label theLabel1 = new Label();
                theLabel1.ID = "lblVacNm";
                theLabel1.Text = "Drug Name";
                theLabel1.Width = 300;
                theheaderPnl.Controls.Add(theLabel1);


                Label theLabel9 = new Label();
                theLabel9.ID = "lblVaccinationSchedule";
                theLabel9.Text = "Schedule";
                theLabel9.Attributes.Add("Style", "text-align:center");
                theLabel9.Width = 75;
                theheaderPnl.Controls.Add(theLabel9);

                Label theLabel2 = new Label();
                theLabel2.ID = "lblVBDose";
                theLabel2.Text = "Dose";
                theLabel2.Width = 45;
                theheaderPnl.Controls.Add(theLabel2);

                Label theLabel5 = new Label();
                theLabel5.ID = "lblVacDuration";
                theLabel5.Text = "Duration (Days)";
                theLabel5.Width = 55;
                theLabel5.Attributes.Add("Style", "text-align:center");
                theheaderPnl.Controls.Add(theLabel5);
                
              

                //Label theLabel3 = new Label();
                //theLabel3.ID = "lblTBDrgUnits";
                //theLabel3.Text = "Unit";
                //theLabel3.Width = 90;
                //theheaderPnl.Controls.Add(theLabel3);

                Label theLabel4 = new Label();
                theLabel4.ID = "lblVacFrequency";
                theLabel4.Text = "Frequency";
                theLabel4.Width = 80;
                theheaderPnl.Controls.Add(theLabel4);

             

                Label theLabel6 = new Label();
                theLabel6.ID = "lblVacDrgPrescribed";
                theLabel6.Text = "Quantity Prescribed";
                theLabel6.Width = 75;
                theLabel6.Attributes.Add("Style", "text-align:center");
                theheaderPnl.Controls.Add(theLabel6);
         
                Label theLabel7 = new Label();
                theLabel7.ID = "lblVacDrgDispensed";
                theLabel7.Text = "Quantity Dispensed";
                theLabel7.Width = 75;
                theLabel7.Attributes.Add("Style", "text-align:center");
                theheaderPnl.Controls.Add(theLabel7);

               
                Label lblStSp2 = new Label();
                lblStSp2.Width = 10;
                lblStSp2.ID = "stSpace2" + drugId + "^" + genericId;
                lblStSp2.Text = "";
                theheaderPnl.Controls.Add(lblStSp2);

                Label lblProphylaxis = new Label();
                lblProphylaxis.Text = "Prophylaxis";
                lblProphylaxis.ID = "lblVacProphylaxis" + thePanel.ID;
                lblProphylaxis.Font.Bold = true;
                lblProphylaxis.Visible = false;
                theheaderPnl.Controls.Add(lblProphylaxis);

                Label lblSpace8 = new Label();
                lblSpace8.Width = 15;
                lblSpace8.ID = "lblVacPrescriptionSpace" + thePanel.ID;
                lblSpace8.Text = "";
                theheaderPnl.Controls.Add(lblSpace8);

                Label lblPrintPrescription = new Label();
                lblPrintPrescription.Text = "Print Prescription";
                lblPrintPrescription.Width = 70;
                lblPrintPrescription.Attributes.Add("Style", "text-align:center");
                lblPrintPrescription.ID = "lblVacPrintPrescription" + thePanel.ID;
                lblPrintPrescription.Font.Bold = true;
                lblPrintPrescription.Visible = true;
                theheaderPnl.Controls.Add(lblPrintPrescription);
                thePanel.Controls.Add(theheaderPnl);
            }
            Control thedrgCntrl = FindControlRecursive(thePanel, "pnl" + drugId + "^" + genericId);
            if (thedrgCntrl == null)
            {
                Panel thePnl = new Panel();
                thePnl.ID = "pnl" + drugId + "^" + genericId;
                //thePnl.Height = 30;
                thePnl.Width = 880;
                thePnl.Controls.Clear();
                Label lblStSp = new Label();
                lblStSp.Width = 5;
                lblStSp.ID = "stSpace" + drugId + "^" + genericId;
                lblStSp.Text = "";
                lblStSp.Height = 45;
              
                thePnl.Controls.Add(lblStSp);
               

                DataView theDV;
                DataSet theDS = (DataSet)ViewState["MasterData"];
                if (genericId == 0)
                {
                    //theDV = new DataView(theDS.Tables[0]);
                    theDV = new DataView(theDS.Tables[12]); // rupesh for both active & inactive drug 31/07/07
                    theDV.RowFilter = "drug_pk = " + drugId;
                }
                else
                {
                    theDV = new DataView(theDS.Tables[4]);
                    if (drugId.ToString().LastIndexOf("9999") > 0)
                    {
                        drugId = Convert.ToInt32(drugId.ToString().Substring(0, drugId.ToString().Length - 4));
                    }
                    theDV.RowFilter = "GenericId = " + drugId;
                    //theDV = new DataView(theDS.Tables[4]);
                    //theDV.RowFilter = "GenericId = " + DrugId;
                }

                Label theDrugNm = new Label();
                theDrugNm.ID = "drgNm" + drugId + "^" + genericId;
                theDrugNm.Text = theDV[0][1].ToString();
                theDrugNm.Width = 280;
                thePnl.Controls.Add(theDrugNm);
              
                /////// Space//////
                Label theSpace = new Label();
                theSpace.ID = "theSpace" + drugId + "^" + genericId;
                theSpace.Width = 10;
                theSpace.Text = "";
                thePnl.Controls.Add(theSpace);
                
                ////////////////////
                //Vaccination Schedule
                DropDownList ddlVacSchedule = new DropDownList();
                ddlVacSchedule.ID = "drgVacSchedule" + drugId + "^" + genericId;
                ddlVacSchedule.Width = 70;
             
                //IDrugMst drgMst = (IDrugMst)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDrugMst, BusinessProcess.Administration");

                //DataTable dtSchedule = drgMst.GetScheduleByDrugID(drugId);

                //theBindMgr.BindCombo(ddlVacSchedule, dtSchedule, "Name", "Id");
                BindScheduleList(drugId, ref ddlVacSchedule);
                thePnl.Controls.Add(ddlVacSchedule);
            
                Label theSpace8 = new Label();
                theSpace8.ID = "theSpace8*" + drugId + "^" + genericId;
                theSpace8.Width = 10;
                theSpace8.Text = "";
                thePnl.Controls.Add(theSpace8);
                //////////////////////////////////////
                TextBox theDose = new TextBox();
                theDose.ID = "drgDose" + drugId + "^" + genericId;
                theDose.Width = 30;
                theDose.Text = "";
                theDose.Load += new EventHandler(Control_Load);
                thePnl.Controls.Add(theDose);
             
                BindFunctions theBindMgr = new BindFunctions();
                /////// Space//////
                Label theSpace2 = new Label();
                theSpace2.ID = "theSpace2*" + drugId + "^" + genericId;
                theSpace2.Width = 10;
                theSpace2.Text = "";
                thePnl.Controls.Add(theSpace2);
                ////////////////////

                DropDownList theFrequency = new DropDownList();

                theFrequency.ID = "drgFrequency" + drugId + "^" + genericId;
                theFrequency.Width = 70;
                DataTable DTFreq = new DataTable();
                //DTFreq = theDS.Tables[6]; // Rupesh 03-Sept
                DTFreq = theDS.Tables[8];
                theBindMgr.BindCombo(theFrequency, DTFreq, "FrequencyName", "FrequencyId", "", "Now");
                thePnl.Controls.Add(theFrequency);

                /////// Space//////
                Label theSpace3 = new Label();
                theSpace3.ID = "theSpace3*" + drugId + "^" + genericId;
                theSpace3.Width = 10;
                theSpace3.Text = "";
                thePnl.Controls.Add(theSpace3);
               
                TextBox theDuration = new TextBox();
                theDuration.ID = "drgDuration" + drugId + "^" + genericId;
                theDuration.Attributes.Add("OnBlur", "CalculateTotalDailyDose('ctl00_IQCareContentPlaceHolder_drgDose" + drugId + "^" + genericId + "', 'ctl00_IQCareContentPlaceHolder_drgFrequency" + drugId + "^" + genericId + "','ctl00_IQCareContentPlaceHolder_drgDuration" + drugId + "^" + genericId + "','ctl00_IQCareContentPlaceHolder_drgQtyPrescribed" + drugId + "^" + genericId + "');");
                theDuration.Width = 50;
                theDuration.Text = "1";
                theDuration.Load += new EventHandler(Control_Load);
                thePnl.Controls.Add(theDuration);
           
                ////////////Space////////////////////////
                Label theSpace4 = new Label();
                theSpace4.ID = "theSpace4*" + drugId + "^" + genericId;
                theSpace4.Width = 10;
                theSpace4.Text = "";
                thePnl.Controls.Add(theSpace4);
                TextBox theQtyPrescribed = new TextBox();
                theQtyPrescribed.ID = "drgQtyPrescribed" + drugId + "^" + genericId;

                theQtyPrescribed.Width = 60;
                theQtyPrescribed.Text = "";
                //theQtyPrescribed.Load += new EventHandler(DecimalText_Load);
                theQtyPrescribed.Load += new EventHandler(Control_Load);
                thePnl.Controls.Add(theQtyPrescribed);

                ////////////Space////////////////////////
                Label theSpace5 = new Label();
                theSpace5.ID = "theSpace5*" + drugId + "^" + genericId;
                theSpace5.Width = 10;
                theSpace5.Text = "";
                thePnl.Controls.Add(theSpace5);
                TextBox theQtyDispensed = new TextBox();
                theQtyDispensed.ID = "drgQtyDispensed" + drugId + "^" + genericId;
                theQtyDispensed.Width = 60;
                theQtyDispensed.Text = "";
             
                thePnl.Controls.Add(theQtyDispensed);

                ////////////Space////////////////////////
                Label theSpace6 = new Label();
                theSpace6.ID = "theSpace6*" + drugId + "^" + genericId;
                theSpace6.Width = 10;
                theSpace6.Text = "";
                thePnl.Controls.Add(theSpace6);
                CheckBox theOtherARTProPhChk = new CheckBox();
                theOtherARTProPhChk.ID = "chkProphylaxis" + drugId;
                theOtherARTProPhChk.Width = 10;
                theOtherARTProPhChk.Text = "";
                theOtherARTProPhChk.Enabled = true;
                theOtherARTProPhChk.Visible = false;
               
                thePnl.Controls.Add(theOtherARTProPhChk);
              
                Label theSpace10 = new Label();
                theSpace10.ID = "theSpace8" + drugId;
                theSpace10.Width = 80;
                theSpace10.Text = "";
                thePnl.Controls.Add(theSpace10);

                CheckBox printPrescriptionChk = new CheckBox();
                printPrescriptionChk.ID = "chkPrintPrescription" + drugId;
                printPrescriptionChk.Width = 10;
                printPrescriptionChk.Text = "";
                printPrescriptionChk.Attributes.Add("onclick", "javascript:return fnchecked('" + printPrescriptionChk.ClientID + "')");
                thePnl.Controls.Add(printPrescriptionChk);

                ////////////Space///////////////////////
                Label theSpace9 = new Label();
                theSpace9.ID = "theSpace9" + drugId;
                theSpace9.Width = 50;
                theSpace9.Text = "";
                thePnl.Controls.Add(theSpace9);

                thePnl.Controls.Add(CreateRemoveLinkButton(drugId, thePanel.ID));
                
                /////Patient Instructions/////////
                thePnl.Controls.Add(new LiteralControl("<br />"));
                if (Request.Browser.Browser == "IE")
                {
                    thePnl.Controls.Add(new LiteralControl("<br />"));
                }
               
                Label ptnInstructions = new Label();
                ptnInstructions.ID = "lblPtnInstructions" + drugId;
                //ptnInstructions.Width = 215;
                ptnInstructions.Text = "Patient Instructions:  ";
                ptnInstructions.Font.Bold = true;
                thePnl.Controls.Add(ptnInstructions);
                TextBox ptnIns = new TextBox();
                ptnIns.Width = 700;
                ptnIns.ID = "txtPtnInstructions" + drugId;
                thePnl.Controls.Add(ptnIns);

                thePnl.Controls.Add(new LiteralControl("<script language='javascript' type='text/javascript' id = 'chkOrNot'>"));
                thePnl.Controls.Add(new LiteralControl("fnchecked('chkPrintPrescription" + drugId + "')"));
                thePnl.Controls.Add(new LiteralControl("</script>"));
                thePanel.Controls.Add(thePnl);
                /////////Space panel/////////////////////////
                Panel thePnlspace = new Panel();
                thePnlspace.ID = "pnlspace_" + drugId;
                thePnlspace.Height = 3; //was 3 previously
                thePnlspace.Width = 880;
                thePnlspace.Controls.Clear();
                thePanel.Controls.Add(thePnlspace);
            }
        }

        /// <summary>
        /// Binds the additional drug controls.
        /// </summary>
        /// <param name="drugId">The drug identifier.</param>
        /// <param name="Generic">The generic.</param>
        /// <param name="MstPanel">The MST panel.</param>
        private void BindAdditionalDrugControls(int drugId, int Generic, Panel MstPanel)
        {
            //oi and other medications
            //if (MstPanel.Controls.Count < 1)
            //{
            Control thehdrCntrl = FindControlRecursive(MstPanel, "pnlDrug" + MstPanel.ID);
            if (thehdrCntrl == null)
            {
                #region "OI & Other Medication"
                Panel thelblPnl = new Panel();
                thelblPnl.ID = "pnlDrug" + MstPanel.ID;
                thelblPnl.Height = 20;
                thelblPnl.Width = 880;
                thelblPnl.Controls.Clear();

                Label theLabel = new Label();
                theLabel.ID = "lblDrug" + MstPanel.ID;
                if (MstPanel.ID == "PnlOIARV")
                {
                    theLabel.Text = "OI Treatment and Non-HIV/AIDS Medications";//earlier it was "OI Treatment and Other Medications";
                }
                else
                    theLabel.Text = "Other Medications";//earlier it was "OI Treatment and Other Medications";

                theLabel.Font.Bold = true;
                thelblPnl.Controls.Add(theLabel);
                MstPanel.Controls.Add(thelblPnl);

                /////////////////////////////////////////////////
                Panel theheaderPnl = new Panel();
                theheaderPnl.ID = "pnlHeaderOtherDrug" + MstPanel.ID;
                theheaderPnl.Height = 20;
                theheaderPnl.Width = 880;
                theheaderPnl.Font.Bold = true;
                theheaderPnl.Controls.Clear();

                Label theSP = new Label();
                theSP.ID = "lblDrgSp" + MstPanel.ID;
                theSP.Width = 5;
                theSP.Text = "";
                theheaderPnl.Controls.Add(theSP);

                Label theLabel1 = new Label();
                theLabel1.ID = "lblDrgNm" + MstPanel.ID;
                theLabel1.Text = "Drug Name";
                theLabel1.Width = 200;
                theheaderPnl.Controls.Add(theLabel1);

                Label lblSpace = new Label();
                lblSpace.Width = 10;
                lblSpace.ID = "lblSpaceAdd_0" + MstPanel.ID;
                lblSpace.Text = "";
                theheaderPnl.Controls.Add(lblSpace);

                Label theLabel2 = new Label();
                theLabel2.ID = "lblDrgDose";
                theLabel2.Text = "Dose";
                theLabel2.Width = 50;
                theheaderPnl.Controls.Add(theLabel2);

                Label lblSpace1 = new Label();
                lblSpace1.Width = 20;
                lblSpace1.ID = "lblSpaceAdd_1" + MstPanel.ID;
                lblSpace1.Text = "";
                theheaderPnl.Controls.Add(lblSpace1);

                Label theLabel4 = new Label();
                theLabel4.ID = "lblDrgFrequency" + MstPanel.ID;
                theLabel4.Text = "Frequency";
                theLabel4.Width = 55;
                theheaderPnl.Controls.Add(theLabel4);

                Label lblSpace2 = new Label();
                lblSpace2.Width = 40;
                lblSpace2.ID = "lblSpaceAdd_2" + MstPanel.ID;
                lblSpace2.Text = "";
                theheaderPnl.Controls.Add(lblSpace2);

                Label theLabel5 = new Label();
                theLabel5.ID = "lblDrgDuration" + MstPanel.ID;
                theLabel5.Text = "Duration (Days)";
                theLabel5.Width = 40;
                theLabel5.Attributes.Add("Style", "text-align:center");
                theheaderPnl.Controls.Add(theLabel5);

                Label lblSpace3 = new Label();
                lblSpace3.Width = 30;
                lblSpace3.ID = "lblSpaceAdd_4" + MstPanel.ID;
                lblSpace3.Text = "";
                theheaderPnl.Controls.Add(lblSpace3);

                Label theLabel6 = new Label();
                theLabel6.ID = "lblDrgPrescribed" + MstPanel.ID;
                theLabel6.Text = "Quantity Prescribed";
                theLabel6.Width = 70;
                theLabel6.Attributes.Add("Style", "text-align:center");
                theheaderPnl.Controls.Add(theLabel6);

                Label lblSpace4 = new Label();
                lblSpace4.Width = 10;
                lblSpace4.ID = "lblSpaceAdd_5" + MstPanel.ID;
                lblSpace4.Text = "";
                theheaderPnl.Controls.Add(lblSpace4);

                Label theLabel7 = new Label();
                theLabel7.ID = "lblDrgDispensed" + MstPanel.ID;
                theLabel7.Text = "Quantity Dispensed";
                theLabel7.Width = 70;
                theLabel7.Attributes.Add("Style", "text-align:center");
                theheaderPnl.Controls.Add(theLabel7);

                Label lblSpace5 = new Label();
                lblSpace5.Width = 15;
                lblSpace5.ID = "lblSpace_6" + MstPanel.ID;
                lblSpace5.Text = "";
                theheaderPnl.Controls.Add(lblSpace5);

                Label lblProphylaxis = new Label();
                lblProphylaxis.Text = "Prophylaxis";
                lblProphylaxis.ID = "lblProphylaxis" + MstPanel.ID; ;
                lblProphylaxis.Font.Bold = true;
                lblProphylaxis.Visible = true;
                theheaderPnl.Controls.Add(lblProphylaxis);

                Label lblSpace8 = new Label();
                lblSpace8.Width = 15;
                lblSpace8.ID = "lblPrescriptionSpace" + MstPanel.ID;
                lblSpace8.Text = "";
                theheaderPnl.Controls.Add(lblSpace8);

                Label lblPrintPrescription = new Label();
                lblPrintPrescription.Text = "Print Prescription";
                lblPrintPrescription.Width = 70;
                lblPrintPrescription.Attributes.Add("Style", "text-align:center");
                lblPrintPrescription.ID = "lblPrintPrescription" + MstPanel.ID;
                lblPrintPrescription.Font.Bold = true;
                lblPrintPrescription.Visible = true;
                theheaderPnl.Controls.Add(lblPrintPrescription);

                MstPanel.Controls.Add(theheaderPnl);
                #endregion "OI & Other Medication"
            }
            //}
            Control thedrgCntrl = FindControlRecursive(MstPanel, "pnl" + drugId + "^" + Generic);
            if (thedrgCntrl == null)
            {
                Panel thePnl = new Panel();
                thePnl.ID = "pnl" + drugId + "^" + Generic;
                //thePnl.Height = 35;
                thePnl.Width = 880;
                thePnl.Controls.Clear();

                Label lblStSp = new Label();
                lblStSp.Width = 5;
                lblStSp.ID = "stSpace" + drugId + "^" + Generic;
                lblStSp.Text = "";
                lblStSp.Height = 45;
                thePnl.Controls.Add(lblStSp);

                DataView theDV;
                DataSet theDS = (DataSet)ViewState["MasterData"];
                if (Generic == 0)
                {
                    //theDV = new DataView(theDS.Tables[0]);
                    theDV = new DataView(theDS.Tables[12]); // rupesh for both active & inactive drug 31/07/07
                    theDV.RowFilter = "drug_pk = " + drugId;
                }
                else
                {
                    theDV = new DataView(theDS.Tables[4]);
                    theDV.RowFilter = "GenericId = " + drugId;
                }

                Label theDrugNm = new Label();
                theDrugNm.ID = "drgNm" + drugId + "^" + Generic;
                theDrugNm.Text = theDV[0][1].ToString();
                theDrugNm.Width = 200;
                thePnl.Controls.Add(theDrugNm);

                /////// Space//////
                Label theSpace = new Label();
                theSpace.ID = "theSpace" + drugId + "^" + Generic;
                theSpace.Width = 10;
                theSpace.Text = "";
                thePnl.Controls.Add(theSpace);

                //////////////////////
                TextBox theDose = new TextBox();
                theDose.ID = "drgDose" + drugId + "^" + Generic;
                theDose.Width = 50;
                theDose.Text = "";
                theDose.Load += new EventHandler(Control_Load);
                thePnl.Controls.Add(theDose);

                BindFunctions theBindMgr = new BindFunctions();

                /////// Space//////
                Label theSpace2 = new Label();
                theSpace2.ID = "theSpace2*" + drugId + "^" + Generic;
                theSpace2.Width = 10;
                theSpace2.Text = "";
                thePnl.Controls.Add(theSpace2);
                ////////////////////

                DropDownList theFrequency = new DropDownList();
                theFrequency.ID = "drgFrequency" + drugId + "^" + Generic;
                theFrequency.Width = 80;
                DataTable DTFreq = new DataTable();
                //DTFreq = theDS.Tables[6]; // Rupesh 03-Sept
                DTFreq = theDS.Tables[8];
                theBindMgr.BindCombo(theFrequency, DTFreq, "FrequencyName", "FrequencyId");
                thePnl.Controls.Add(theFrequency);

                /////// Space//////
                Label theSpace3 = new Label();
                theSpace3.ID = "theSpace3*" + drugId + "^" + Generic;
                theSpace3.Width = 15;
                theSpace3.Text = "";
                thePnl.Controls.Add(theSpace3);
                ////////////////////

                TextBox theDuration = new TextBox();
                theDuration.ID = "drgDuration" + drugId + "^" + Generic;
                theDuration.Attributes.Add("OnBlur", "CalculateTotalDailyDose('ctl00_IQCareContentPlaceHolder_drgDose" + drugId + "^" + Generic + "', 'ctl00_IQCareContentPlaceHolder_drgFrequency" + drugId + "^" + Generic + "','ctl00_IQCareContentPlaceHolder_drgDuration" + drugId + "^" + Generic + "','ctl00_IQCareContentPlaceHolder_drgQtyPrescribed" + drugId + "^" + Generic + "');");
                theDuration.Width = 60;
                theDuration.Text = "";
                theDuration.Load += new EventHandler(Control_Load);
                thePnl.Controls.Add(theDuration);

                ////////////Space////////////////////////
                Label theSpace4 = new Label();
                theSpace4.ID = "theSpace4*" + drugId + "^" + Generic;
                theSpace4.Width = 15;
                theSpace4.Text = "";
                thePnl.Controls.Add(theSpace4);
                ////////////////////////////////////////

                TextBox theQtyPrescribed = new TextBox();
                theQtyPrescribed.ID = "drgQtyPrescribed" + drugId + "^" + Generic;

                theQtyPrescribed.Width = 60;
                theQtyPrescribed.Text = "";
                //theQtyPrescribed.Load += new EventHandler(DecimalText_Load);
                theQtyPrescribed.Load += new EventHandler(Control_Load);
                thePnl.Controls.Add(theQtyPrescribed);

                ////////////Space////////////////////////
                Label theSpace5 = new Label();
                theSpace5.ID = "theSpace5*" + drugId + "^" + Generic;
                theSpace5.Width = 15;
                theSpace5.Text = "";
                thePnl.Controls.Add(theSpace5);
                ////////////////////////////////////////

                TextBox theQtyDispensed = new TextBox();
                theQtyDispensed.ID = "drgQtyDispensed" + drugId + "^" + Generic;
                theQtyDispensed.Width = 60;
                theQtyDispensed.Text = "";
                #region "13-Jun-07 -3"
                //theQtyDispensed.Load += new EventHandler(DecimalText_Load);
                theQtyDispensed.Load += new EventHandler(Control_Load); // rupesh
                #endregion "13-Jun-07 -3"
                thePnl.Controls.Add(theQtyDispensed);

                ////////////Space////////////////////////
                Label theSpace6 = new Label();
                theSpace6.ID = "theSpace6*" + drugId + "^" + Generic;
                theSpace6.Width = 30;
                theSpace6.Text = "";
                thePnl.Controls.Add(theSpace6);
                //////////////////////////////////////////

                ////////////Space///////////////////////

                CheckBox theOtherARTProPhChk = new CheckBox();
                theOtherARTProPhChk.ID = "chkProphylaxis" + drugId;
                theOtherARTProPhChk.Width = 10;
                theOtherARTProPhChk.Text = "";
                theOtherARTProPhChk.Enabled = true;
                //if (ddlTreatment.SelectedItem.Value.ToString() == "223" || ddlTreatment.SelectedItem.Value.ToString() == "224")
                //{
                //    theOtherARTProPhChk.Enabled = true;
                //}
                //else
                //    theOtherARTProPhChk.Enabled = false;
                thePnl.Controls.Add(theOtherARTProPhChk);
                //}

                //////print prescription ARV////////////////
                ////////////Space///////////////////////
                Label theSpace8 = new Label();
                theSpace8.ID = "theSpace8" + drugId;
                theSpace8.Width = 90;
                theSpace8.Text = "";
                thePnl.Controls.Add(theSpace8);

                CheckBox printPrescriptionChk = new CheckBox();
                printPrescriptionChk.ID = "chkPrintPrescription" + drugId;
                printPrescriptionChk.Width = 10;
                printPrescriptionChk.Text = "";
                printPrescriptionChk.Attributes.Add("onclick", "javascript:return fnchecked('" + printPrescriptionChk.ClientID + "')");
                thePnl.Controls.Add(printPrescriptionChk);

                ////////////Space///////////////////////
                Label theSpace9 = new Label();
                theSpace9.ID = "theSpace9" + drugId;
                theSpace9.Width = 100;
                theSpace9.Text = "";
                thePnl.Controls.Add(theSpace9);

                thePnl.Controls.Add(CreateRemoveLinkButton(drugId, MstPanel.ID));
                /////Patient Instructions/////////
                thePnl.Controls.Add(new LiteralControl("<br />"));
                if (Request.Browser.Browser == "IE")
                {
                    thePnl.Controls.Add(new LiteralControl("<br />"));
                }
                Label ptnInstructions = new Label();
                ptnInstructions.ID = "lblPtnInstructions" + drugId;
                //ptnInstructions.Width = 215;
                ptnInstructions.Text = "Patient Instructions:  ";
                ptnInstructions.Font.Bold = true;
                thePnl.Controls.Add(ptnInstructions);

                TextBox ptnIns = new TextBox();
                ptnIns.Width = 700;
                ptnIns.ID = "txtPtnInstructions" + drugId;
                thePnl.Controls.Add(ptnIns);

                thePnl.Controls.Add(new LiteralControl("<script language='javascript' type='text/javascript' id = 'chkOrNot'>"));
                thePnl.Controls.Add(new LiteralControl("fnchecked('chkPrintPrescription" + drugId + "')"));
                thePnl.Controls.Add(new LiteralControl("</script>"));

                MstPanel.Controls.Add(thePnl);

                /////////Space panel/////////////////////////
                Panel thePnlspace = new Panel();
                thePnlspace.ID = "pnlspace_" + drugId;
                thePnlspace.Height = 3;//was 3 previously
                thePnlspace.Width = 880;
                thePnlspace.Controls.Clear();
                MstPanel.Controls.Add(thePnlspace);
            }
        }

        private LinkButton CreateRemoveLinkButton(int drugId, string panelId)
        {
            LinkButton thelnkRemove = new LinkButton();
            thelnkRemove.ID = "lnkrmv%" + panelId + "^" + drugId;
            thelnkRemove.Width = 20;
            thelnkRemove.Text = "Remove";
            thelnkRemove.Click += new EventHandler(Remove_panel);
            if (Session["ExistPharmacyData"] != null)
            {
                if (((DataTable)Session["ExistPharmacyData"]).Rows.Count > 0)
                {
                    IQCareUtils theUtilsCF = new IQCareUtils();
                    DataView theExistDrgDV = new DataView((DataTable)Session["ExistPharmacyData"]);
                    theExistDrgDV.RowFilter = "Drug_Pk=" + drugId;
                    DataTable theDTfiltdrg = (DataTable)theUtilsCF.CreateTableFromDataView(theExistDrgDV);
                    if (Convert.ToInt32(Session["PatientVisitId"]) > 0 && theDTfiltdrg.Rows.Count > 0)
                    {
                        //if (theDTfiltdrg.Rows[0]["DispensedByDate"].ToString().Equals(""))
                        //{
                        //    thelnkRemove.Visible = true;
                        //}
                        //else
                        //{
                        //    thelnkRemove.Visible = false;
                        //}
                        //  thelnkRemove.Visible = true;
                        //if (this.OrderId > 0 && this.OrderStatus == 1)
                        //{
                        thelnkRemove.Visible = (this.UserId == this.PrescribedBy || this.UserId == this.SignatureBy) && (theDTfiltdrg.Rows[0]["DispensedByDate"].ToString().Equals(""));
                        //}
                        //thelnkRemove.Visible = false;
                    }
                }
            }
            thelnkRemove.OnClientClick = "return confirm('Are you sure you want to Remove this Drug?')";
            return thelnkRemove;
        }

        private DataTable UserList
        {
            get
            {
                DataSet theDS = new DataSet();
                theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));

                IQCareUtils theUtils = new IQCareUtils();
                DataTable dt = new DataTable("Users");
                if (theDS.Tables["Users"] != null)
                {
                    DataView theDV = new DataView(theDS.Tables["Users"]);
                    if (theDV.Table != null)
                    {
                        dt = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    }
                }
                return dt;
            }
        }

        private DataTable EmployeeList
        {
            get
            {
                DataSet theDS = new DataSet();
                theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));

                IQCareUtils theUtils = new IQCareUtils();
                DataTable dt = new DataTable("Mst_Employee");
                if (theDS.Tables["Mst_Employee"] != null)
                {
                    DataView theDV = new DataView(theDS.Tables["Mst_Employee"]);
                    if (theDV.Table != null)
                    {
                        dt = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    }
                }
                return dt;
            }
        }

        private int EmployeeId
        {
            get
            {
                return Convert.ToInt32(Session["AppUserEmployeeId"].ToString());
            }
        }

        /// <summary>
        /// Binds the controls.
        /// </summary>
        private void BindControls()
        {
            if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            {
            }

            IDrug DrugManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BDrug, BusinessProcess.Pharmacy");
            BindFunctions theBindMgr = new BindFunctions();
            DataTable theDT = new DataTable();
            DataSet theDSXML = new DataSet();
            IQCareUtils theUtils = new IQCareUtils();
            theDSXML.ReadXml(Server.MapPath("..\\XMLFiles\\AllMasters.con"));

            //if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            //{
            /*if (theDSXML.Tables["Users"] != null)
            {
                theDV = new DataView(theDSXML.Tables["Users"]);
                if (theDV.Table != null)
                {
                    if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
                    {
                        theDV.RowFilter = "DeleteFlag = 0 And EmployeeID > 0";
                    }
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);

                    theBindMgr.BindCombo(ddlPharmOrderedbyName, theDT, "Names", "UserId", "",Session["AppUserId"].ToString());
                    string strDispensedBy = "";
                    if (Session["SCMModule"] == null)
                    {
                        strDispensedBy = Session["AppUserId"].ToString();
                    }
                    else
                    {
                        theDT.DefaultView.RowFilter = "Names = 'Select'";
                    }
                    theBindMgr.BindCombo(ddlDispensedBy, theDT.DefaultView.ToTable(), "Names", "UserId", "", strDispensedBy);
                }
                theDV = new DataView(theDSXML.Tables["Users"]);

                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    theDV = new DataView(theDT);
                    string rowFilter = "UserId = " + Session["AppUserId"].ToString();
                    theDV.RowFilter = rowFilter;
                    if (theDV.Count > 0)
                        theDT = theUtils.CreateTableFromDataView(theDV);
                    theBindMgr.BindCombo(ddlPharmSignature, theDT, "Names", "UserId", "", Session["AppUserId"].ToString());
                    theDV.Dispose();
                    theDT.Clear();
                }
            }*/
            if (theDSXML.Tables["Mst_Decode"] != null)
            {
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "CodeId=26 and (DeleteFlag=0 or DeleteFlag IS NULL)";
                //
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);

                    theBindMgr.BindCombo(ddlAppntReason, theDT, "Name", "Id");
                    theDV.Dispose();
                    theDT.Clear();
                }
            }
            if (theDSXML.Tables["mst_RegimenLine"] != null)
            {
                theDV = new DataView(theDSXML.Tables["mst_RegimenLine"]);
                theDV.RowFilter = "DeleteFlag=0";
                //
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    theBindMgr.BindCombo(ddlregimenLine, theDT, "Name", "Id");
                    //Interface.SCM.IDrug regimen = (Interface.SCM.IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
                    //theBindMgr.BindCombo(ddlregimenLine, regimen.GetPharmacyRegimenClassification(), "DisplayName", "LookUpItemId");
                    theDV.Dispose();
                    theDT.Clear();
                }
            }
        }

        /// <summary>
        /// Binds the custom controls.
        /// </summary>
        /// <param name="drugId">The drug identifier.</param>
        /// <param name="Generic">The generic.</param>
        /// <param name="MstPanel">The MST panel.</param>
        private void BindCustomControls(int drugId, int Generic, Panel MstPanel)
        {
            Control thehdrCntrl = FindControlRecursive(MstPanel, "pnlARVDrug" + MstPanel.ID);
            if (thehdrCntrl == null)
            {
                #region "ARV Medication"
                Panel thelblPnl = new Panel();
                thelblPnl.ID = "pnlARVDrug" + MstPanel.ID;
                thelblPnl.Height = 20;
                thelblPnl.Width = 880;
                thelblPnl.Controls.Clear();

                Label theLabel = new Label();
                theLabel.ID = "lblDrug" + MstPanel.ID;
                theLabel.Text = "ARV Medications";//earlier it was "OI Treatment and Other Medications";
                theLabel.Font.Bold = true;
                thelblPnl.Controls.Add(theLabel);
                MstPanel.Controls.Add(thelblPnl);

                /////////////////////////////////////////////////
                Panel theheaderPnl = new Panel();
                theheaderPnl.ID = "pnlARVhdrDrug" + MstPanel.ID;
                theheaderPnl.Height = 20;
                theheaderPnl.Width = 880;
                theheaderPnl.Font.Bold = true;
                theheaderPnl.Controls.Clear();

                Label theSP = new Label();
                theSP.ID = "lblDrgSp" + MstPanel.ID;
                theSP.Width = 5;
                theSP.Text = "";
                theheaderPnl.Controls.Add(theSP);

                Label theLabel1 = new Label();
                theLabel1.ID = "lblDrgNm" + MstPanel.ID;
                theLabel1.Text = "Drug Name";
                theLabel1.Width = 200;
                theheaderPnl.Controls.Add(theLabel1);

                Label lblSpace = new Label();
                lblSpace.Width = 10;
                lblSpace.ID = "lblSpaceAdd_0" + MstPanel.ID;
                lblSpace.Text = "";
                theheaderPnl.Controls.Add(lblSpace);

                Label theLabelDose = new Label();
                theLabelDose.ID = "lblDrgDose" + MstPanel.ID;
                theLabelDose.Text = "Dose";
                theLabelDose.Width = 50;
                theheaderPnl.Controls.Add(theLabelDose);

                Label lblSpace1 = new Label();
                lblSpace1.Width = 15;
                lblSpace1.ID = "lblSpaceAdd_1" + MstPanel.ID;
                lblSpace1.Text = "";
                theheaderPnl.Controls.Add(lblSpace1);

                Label theLabel4 = new Label();
                theLabel4.ID = "lblDrgFrequency" + MstPanel.ID;
                theLabel4.Text = "Frequency";
                theLabel4.Width = 55;
                theheaderPnl.Controls.Add(theLabel4);

                Label lblSpace2 = new Label();
                lblSpace2.Width = 40;
                lblSpace2.ID = "lblSpaceAdd_2" + MstPanel.ID;
                lblSpace2.Text = "";
                theheaderPnl.Controls.Add(lblSpace2);

                Label theLabel5 = new Label();
                theLabel5.ID = "lblDrgDuration" + MstPanel.ID;
                theLabel5.Text = "Duration (Days)";
                theLabel5.Width = 40;
                theLabel5.Attributes.Add("Style", "text-align:center");
                theheaderPnl.Controls.Add(theLabel5);

                Label lblSpace3 = new Label();
                lblSpace3.Width = 35;
                lblSpace3.ID = "lblSpaceAdd_4" + MstPanel.ID;
                lblSpace3.Text = "";
                theheaderPnl.Controls.Add(lblSpace3);

                Label theLabel6 = new Label();
                theLabel6.ID = "lblDrgPrescribed" + MstPanel.ID;
                theLabel6.Text = "Quantity Prescribed";
                theLabel6.Width = 70;
                theLabel6.Attributes.Add("Style", "text-align:center");
                theheaderPnl.Controls.Add(theLabel6);

                Label lblSpace4 = new Label();
                lblSpace4.Width = 10;
                lblSpace4.ID = "lblSpaceAdd_5" + MstPanel.ID;
                lblSpace4.Text = "";
                theheaderPnl.Controls.Add(lblSpace4);

                Label theLabel7 = new Label();
                theLabel7.ID = "lblDrgDispensed" + MstPanel.ID;
                theLabel7.Text = "Quantity Dispensed";
                theLabel7.Width = 70;
                theLabel7.Attributes.Add("Style", "text-align:center");
                theheaderPnl.Controls.Add(theLabel7);

                Label lblSpace5 = new Label();
                lblSpace5.Width = 15;
                lblSpace5.ID = "lblpropSpace" + MstPanel.ID;
                lblSpace5.Text = "";
                theheaderPnl.Controls.Add(lblSpace5);

                Label lblProphylaxis = new Label();
                lblProphylaxis.Text = "Prophylaxis";
                lblProphylaxis.ID = "lblProphylaxis" + MstPanel.ID;
                lblProphylaxis.Font.Bold = true;
                lblProphylaxis.Visible = true;
                theheaderPnl.Controls.Add(lblProphylaxis);

                Label lblSpace8 = new Label();
                lblSpace8.Width = 15;
                lblSpace8.ID = "lblPrescriptionSpace" + MstPanel.ID;
                lblSpace8.Text = "";
                lblSpace8.EnableViewState = false;
                theheaderPnl.Controls.Add(lblSpace8);

                Label lblPrintPrescription = new Label();
                lblPrintPrescription.Text = "Print Prescription";
                lblPrintPrescription.Width = 70;
                lblPrintPrescription.Attributes.Add("Style", "text-align:center");
                lblPrintPrescription.ID = "lblPrintPrescription" + MstPanel.ID;
                lblPrintPrescription.Font.Bold = true;
                lblPrintPrescription.Visible = true;
                lblPrintPrescription.EnableViewState = false;
                theheaderPnl.Controls.Add(lblPrintPrescription);

                MstPanel.Controls.Add(theheaderPnl);
                #endregion "ARV Medication"
            }
            Control thedrgCntrl = FindControlRecursive(MstPanel, "pnl_" + drugId);
            if (thedrgCntrl == null)
            {
                Panel thePnl = new Panel();
                thePnl.ID = "pnl_" + drugId;
                //thePnl.Height = 30;
                thePnl.Width = 880;
                thePnl.Controls.Clear();

                Label lblStSp = new Label();
                lblStSp.Width = 5;
                lblStSp.ID = "stSpace" + drugId;
                lblStSp.Text = "";
                lblStSp.Height = 45;
                thePnl.Controls.Add(lblStSp);

                DataView theDV;
                DataSet theDS_Custom = (DataSet)ViewState["MasterData"];
                theDV = new DataView(theDS_Custom.Tables[12]);
                if (drugId.ToString().LastIndexOf("8888") > 0)
                {
                    drugId = Convert.ToInt32(drugId.ToString().Substring(0, drugId.ToString().Length - 4));
                }
                theDV.RowFilter = "Drug_Pk = " + drugId;

                Label theDrugNm = new Label();
                theDrugNm.ID = "drgNm" + drugId;
                theDrugNm.Text = theDV[0][1].ToString();
                theDrugNm.Width = 200;
                thePnl.Controls.Add(theDrugNm);

                /////// Space//////
                Label theSpace = new Label();
                theSpace.ID = "theSpace_" + drugId;
                theSpace.Width = 10;
                theSpace.Text = "";
                ////////////////////

                thePnl.Controls.Add(theSpace);

                TextBox theDose = new TextBox();
                theDose.ID = "drgDose" + drugId + "^" + Generic;
                theDose.Width = 50;
                theDose.Text = "";
                theDose.Load += new EventHandler(Control_Load);
                thePnl.Controls.Add(theDose);

                BindFunctions theBindMgr = new BindFunctions();

                ////////////Space////////////////////////
                Label theSpace1 = new Label();
                theSpace1.ID = "theSpace1" + drugId;
                theSpace1.Width = 10;
                theSpace1.Text = "";
                thePnl.Controls.Add(theSpace1);
                ////////////////////////////////////////

                DropDownList theDrugFrequency = new DropDownList();
                theDrugFrequency.ID = "drgFrequency" + drugId;
                theDrugFrequency.Width = 80;
                #region "BindCombo"
                DataTable theDTF = new DataTable();
                DataView theDVFrequency = new DataView(theDS_Custom.Tables[8]);

                DataTable theDTFrequency = new DataTable();
                if (theDVFrequency.Count > 0)
                {
                    IQCareUtils theUtils = new IQCareUtils();
                    theDTFrequency = theUtils.CreateTableFromDataView(theDVFrequency);
                    theBindMgr.BindCombo(theDrugFrequency, theDTFrequency, "FrequencyName", "FrequencyId");
                }
                #endregion "BindCombo"
                thePnl.Controls.Add(theDrugFrequency);

                ////////////Space////////////////////////
                Label theSpace2 = new Label();
                theSpace2.ID = "theSpace2" + drugId;
                theSpace2.Width = 15;
                theSpace2.Text = "";
                thePnl.Controls.Add(theSpace2);
                ////////////////////////////////////////

                TextBox theDuration = new TextBox();
                theDuration.ID = "drgDuration" + drugId;
                theDuration.Attributes.Add("OnBlur", "CalculateTotalDailyDose('ctl00_IQCareContentPlaceHolder_drgDose" + drugId + "^" + Generic + "', 'ctl00_IQCareContentPlaceHolder_drgFrequency" + drugId + "','ctl00_IQCareContentPlaceHolder_drgDuration" + drugId + "','ctl00_IQCareContentPlaceHolder_drgQtyPrescribed" + drugId + "');");
                theDuration.Width = 60;
                theDuration.Load += new EventHandler(Control_Load);
                thePnl.Controls.Add(theDuration);

                ////////////Space////////////////////////
                Label theSpace3 = new Label();
                theSpace3.ID = "theSpace3" + drugId;
                theSpace3.Width = 15;
                theSpace3.Text = "";
                thePnl.Controls.Add(theSpace3);
                ////////////////////////////////////////

                TextBox theQtyPrescribed = new TextBox();
                theQtyPrescribed.ID = "drgQtyPrescribed" + drugId;

                theQtyPrescribed.Width = 60;
                theQtyPrescribed.Load += new EventHandler(Control_Load);
                thePnl.Controls.Add(theQtyPrescribed);
                ////////////Space////////////////////////
                Label theSpace4 = new Label();
                theSpace4.ID = "theSpace4" + drugId;
                theSpace4.Width = 15;
                theSpace4.Text = "";
                thePnl.Controls.Add(theSpace4);
                ////////////////////////////////////////

                TextBox theQtyDispensed = new TextBox();
                theQtyDispensed.ID = "drgQtyDispensed" + drugId;
                theQtyDispensed.Width = 60;
                theQtyDispensed.Load += new EventHandler(Control_Load);
                //if (Session["SCMModule"] != null)
                //    theQtyDispensed.Attributes.Add("onkeyup", "chknotwrite('" + theQtyDispensed.ClientID + "')");
                //    //theQtyDispensed.Enabled = false;
                thePnl.Controls.Add(theQtyDispensed);
                ////////////Space////////////////////////
                Label theSpace5 = new Label();
                theSpace5.ID = "theSpace5" + drugId;
                theSpace5.Width = 15;
                theSpace5.Text = "";
                thePnl.Controls.Add(theSpace5);

                ////////////Space///////////////////////
                Label theSpace6 = new Label();
                theSpace6.ID = "theSpace6" + drugId;
                theSpace6.Width = 15;
                theSpace6.Text = "";
                thePnl.Controls.Add(theSpace6);

                //if (ddlTreatment.SelectedItem.Value.ToString() == "223")
                //{
                CheckBox theOtherARTProPhChk = new CheckBox();
                theOtherARTProPhChk.ID = "chkProphylaxis" + drugId;
                theOtherARTProPhChk.Width = 10;
                theOtherARTProPhChk.Text = "";
                if (ddlTreatment.SelectedItem.Value.ToString() == "222")
                {
                    theOtherARTProPhChk.Enabled = false;
                }
                else
                    theOtherARTProPhChk.Enabled = true;
                thePnl.Controls.Add(theOtherARTProPhChk);

                //////print prescription ARV////////////////
                ////////////Space///////////////////////
                Label theSpace8 = new Label();
                theSpace8.ID = "theSpace8" + drugId;
                theSpace8.Width = 90;
                theSpace8.Text = "";
                thePnl.Controls.Add(theSpace8);

                CheckBox printPrescriptionChk = new CheckBox();
                printPrescriptionChk.ID = "chkPrintPrescription" + drugId;
                printPrescriptionChk.Width = 10;
                printPrescriptionChk.Text = "";
                printPrescriptionChk.Attributes.Add("onclick", "javascript:return fnchecked('" + printPrescriptionChk.ClientID + "')");
                if (MstPanel.ID == "pnlPedia")
                {
                    printPrescriptionChk.AutoPostBack = true;
                    AsyncPostBackTrigger trig = new AsyncPostBackTrigger();
                    trig.ControlID = printPrescriptionChk.ID;
                }
                thePnl.Controls.Add(printPrescriptionChk);

                ////////////Space///////////////////////
                Label theSpace7 = new Label();
                theSpace7.ID = "theSpace7" + drugId;
                theSpace7.Width = 90;
                theSpace7.Text = "";
                thePnl.Controls.Add(theSpace7);

                thePnl.Controls.Add(this.CreateRemoveLinkButton(drugId, MstPanel.ID));

                /////Patient Instructions/////////
                thePnl.Controls.Add(new LiteralControl("<br />"));
                if (Request.Browser.Browser == "IE")
                {
                    thePnl.Controls.Add(new LiteralControl("<br />"));
                }
                Label ptnInstructions = new Label();
                ptnInstructions.ID = "lblPtnInstructions" + drugId;
                //ptnInstructions.Width = 815;
                ptnInstructions.Text = "Patient Instructions:  ";
                ptnInstructions.Font.Bold = true;
                thePnl.Controls.Add(ptnInstructions);

                TextBox ptnIns = new TextBox();
                ptnIns.Width = 700;
                ptnIns.ID = "txtPtnInstructions" + drugId;
                thePnl.Controls.Add(ptnIns);

                thePnl.Controls.Add(new LiteralControl("<script language='javascript' type='text/javascript' id = 'chkOrNot'>"));
                thePnl.Controls.Add(new LiteralControl("fnchecked('chkPrintPrescription" + drugId + "')"));
                thePnl.Controls.Add(new LiteralControl("</script>"));

                MstPanel.Controls.Add(thePnl);
                Panel thePnlspace = new Panel();
                thePnlspace.ID = "pnlspace_" + drugId;
                thePnlspace.Height = 3;//was 3 prevoiusly
                thePnlspace.Width = 800;
                thePnlspace.Controls.Clear();
                MstPanel.Controls.Add(thePnlspace);

                //hidchkbox1.Value = hidchkbox1.Value + "," + theOtherARTProPhChk.ID;
                //}
                //else
                //{
                //}
            }
        }

        /// <summary>
        /// Bindddls the controls.
        /// </summary>
        /// <param name="theDS">The ds.</param>theDS.Tables[14]
        private void BindddlControls(ref DataSet theDS)
        {
            /*******/
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            DataView theDVTreat = new DataView(theDS.Tables[11]);
            theDVTreat.RowFilter = "DeleteFlag=0";
            DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDVTreat);
            BindManager.BindCombo(ddlTreatment, theDT, theDT.Columns[1].ColumnName, theDT.Columns[0].ColumnName);

            theDVTreat.Dispose();
            theDT.Clear();

            //--------Rupesh 19-sep-07 - for ARV Provider
            DataView theDVProvider = new DataView(theDS.Tables[14]);
            //theDVProvider.RowFilter = "DeleteFlag=0";
            theDT = (DataTable)theUtils.CreateTableFromDataView(theDVProvider);
            BindManager.BindCombo(ddlProvider, theDT, theDT.Columns[1].ColumnName, theDT.Columns[0].ColumnName, "Name", "1");
            theDVProvider.Dispose();
            theDT.Clear();
            //ddlProvider.SelectedIndex = 1;

            //Period Taken
            DataView DVPeriodTaken = new DataView(theDS.Tables[16]);
            DataTable dtPeriodTaken = theUtils.CreateTableFromDataView(DVPeriodTaken);
            BindManager.BindCombo(ddlPeriodTaken, dtPeriodTaken, dtPeriodTaken.Columns[1].ColumnName.ToString(), dtPeriodTaken.Columns[0].ColumnName.ToString());
        }

        private void BindDropdownDispensedBy(String userId = "")
        {
            //DataSet theDS = new DataSet();
            userId = userId == "0" ? "" : userId;
            //theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            //if (theDS.Tables["Mst_Employee"] != null)
            //{
            DataView theDV = new DataView(this.UserList);

            string rowFilter = "EmployeeId Is Not Null Or EmployeeId > 0 And UserDeleteFlag = 0";
            if (userId != "")
            {
                rowFilter = "UserId = " + userId;
            }
            //}
            theDV.RowFilter = rowFilter;
            if (theDV.Table != null)
            {
                DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);

                BindManager.BindCombo(ddlDispensedBy, theDT, "Name", "UserId", "", userId);
                ListItem item = ddlDispensedBy.Items.FindByValue(userId);
                if (item == null)
                {
                    item = ddlDispensedBy.Items.FindByValue(this.UserId.ToString());
                }
                if (item != null)
                {
                    item.Selected = true;
                    labelDispensedBy.Text = item.Text;
                }
            }
        }

        /// <summary>
        /// Binds the dropdown order by.
        /// </summary>
        /// <param name="EmployeeId">The employee identifier.</param>
        private void BindDropdownOrderBy()
        {
            //DataSet theDS = new DataSet();
            String userId = "";
            //theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            //if (theDS.Tables["Mst_Employee"] != null)
            //{
            DataView theDV = new DataView(this.UserList);

            string rowFilter = "EmployeeId Is Not Null Or EmployeeId > 0 And UserDeleteFlag = 0";
            if (OrderId > 0)
            {
                userId = this.PrescribedBy.ToString();
            }
            else if (IsPaperless && this.EmployeeId > 0)
            {
                userId = this.UserId.ToString();
            }
            if (userId != "")
            {
                rowFilter = "UserId = " + userId;
            }
            theDV.RowFilter = rowFilter;
            if (theDV.Table != null)
            {
                DataTable theDT = theUtils.CreateTableFromDataView(theDV);

                BindManager.BindCombo(ddlPharmOrderedbyName, theDT, "Name", "UserId", "", userId);
                ListItem item = ddlPharmOrderedbyName.Items.FindByValue(userId);
                if (item == null)
                {
                    item = ddlPharmOrderedbyName.Items.FindByValue(this.UserId.ToString());
                }
                if (item != null)
                {
                    item.Selected = true;
                    labelOrderByName.Text = item.Text;
                }
            }
        }

        /// <summary>
        /// Binds the dropdown signature.
        /// </summary>
        /// <param name="EmployeeId">The employee identifier.</param>
        private void BindDropdownSignature(String userId = "")
        {
            //DataSet theDS = new DataSet();
            userId = userId == "0" ? "" : userId;
            //theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            //if (theDS.Tables["Mst_Employee"] != null)
            //{
            DataView theDV = new DataView(this.UserList);

            string rowFilter = "EmployeeId Is Not Null Or EmployeeId > 0 And UserDeleteFlag = 0";
            if (userId != "")
            {
                rowFilter = "UserId = " + userId;
            }
            //}
            theDV.RowFilter = rowFilter;
            if (theDV.Table != null)
            {
                DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);

                BindManager.BindCombo(ddlPharmSignature, theDT, "Name", "UserId", "", userId);
                ListItem item = ddlPharmSignature.Items.FindByValue(userId);
                if (item == null)
                {
                    item = ddlPharmSignature.Items.FindByValue(this.UserId.ToString());
                }
                if (item != null)
                {
                    item.Selected = true;
                    labelSignature.Text = item.Text;
                }
            }
            /*DataSet theDS = new DataSet();
            theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            if (theDS.Tables["Users"] != null)
            {
                DataView theDV = new DataView(theDS.Tables["Users"]);
                // theDV.RowFilter = "DeleteFlag = 0";
                if (theDV.Table != null)
                {
                    DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    if (Convert.ToInt32(Session["AppUserId"]) > 0)
                    {
                        theDV = new DataView(theDT);
                        theDV.RowFilter = "UserId IN(" + Session["AppUserId"].ToString() + "," + userId + ")";
                        if (theDV.Count > 0)
                            theDT = theUtils.CreateTableFromDataView(theDV);
                    }
                    BindManager.BindCombo(ddlPharmSignature, theDT, "Names", "UserId");
                }
            }*/
        }

        /// <summary>
        /// Binds the infant health section.
        /// </summary>
        /// <param name="theDS">The ds.</param>
        private void BindInfantHealthSection(DataSet theDS)
        {
            BindFunctions theBindManager = new BindFunctions();
            DataTable theDT;
            DataView theDV = new DataView(theDS.Tables[22]);
            theDV.RowFilter = "drugtypeid=60 and DrugName <> '' and GenericID=0";
            theDT = theDV.ToTable();
            //theBindManager.BindCheckedList(chkImmunizations, theDT, "DrugName", "Drug_pk");
            int j = 0, k = 0;
            TableRow trInfantHealth = new TableRow();
            for (int i = 0; i < theDT.Rows.Count; i++)
            {
                if (j == 0)
                {
                    TableCell tcchkInfantHealth1 = new TableCell();
                    TableCell tcDDInfantHealth1 = new TableCell();
                    HtmlInputCheckBox chkInfantHealth = new HtmlInputCheckBox();
                    chkInfantHealth.ID = theDT.Rows[i]["Drug_pk"] + "-" + theDT.Rows[i]["GenericID"].ToString();
                    chkInfantHealth.Value = theDT.Rows[i]["DrugName"].ToString();
                    tcchkInfantHealth1.Controls.Add(chkInfantHealth);
                    tcchkInfantHealth1.Controls.Add(new LiteralControl(chkInfantHealth.Value));
                    trInfantHealth.Cells.Add(tcchkInfantHealth1);
                    DataView theDV_IH = new DataView(theDS.Tables[19]);
                    theDV_IH.RowFilter = "DrugID = " + Convert.ToInt32(theDT.Rows[i]["Drug_pk"]) + "";
                    DropDownList ddlInfantHealth = new DropDownList();
                    ddlInfantHealth.ID = "ddl" + theDT.Rows[i]["Drug_pk"] + theDT.Rows[i]["GenericID"];
                    theBindManager.BindCombo(ddlInfantHealth, theDV_IH.ToTable(), "Name", "ID");
                    tcDDInfantHealth1.Controls.Add(ddlInfantHealth);
                    trInfantHealth.Cells.Add(tcDDInfantHealth1);
                    j = 1;
                    k++;
                }
                else if (j == 1)
                {
                    TableCell tcchkInfantHealth2 = new TableCell();
                    TableCell tcDDInfantHealth2 = new TableCell();
                    HtmlInputCheckBox chkInfantHealth = new HtmlInputCheckBox();
                    chkInfantHealth.ID = theDT.Rows[i]["Drug_pk"] + "-" + theDT.Rows[i]["GenericID"].ToString();
                    chkInfantHealth.Value = theDT.Rows[i]["DrugName"].ToString();
                    tcchkInfantHealth2.Controls.Add(chkInfantHealth);
                    tcchkInfantHealth2.Controls.Add(new LiteralControl(chkInfantHealth.Value));
                    trInfantHealth.Cells.Add(tcchkInfantHealth2);
                    DataView theDV_IH = new DataView(theDS.Tables[19]);
                    theDV_IH.RowFilter = "DrugID = " + Convert.ToInt32(theDT.Rows[i]["Drug_pk"]) + "";
                    DropDownList ddlInfantHealth = new DropDownList();
                    ddlInfantHealth.ID = "ddl" + theDT.Rows[i]["Drug_pk"] + theDT.Rows[i]["GenericID"];
                    theBindManager.BindCombo(ddlInfantHealth, theDV_IH.ToTable(), "Name", "ID");
                    tcDDInfantHealth2.Controls.Add(ddlInfantHealth);
                    trInfantHealth.Cells.Add(tcDDInfantHealth2);
                    j = 2;
                    k++;
                }
                else if (j == 2)
                {
                    TableCell tcchkInfantHealth3 = new TableCell();
                    TableCell tcDDInfantHealth3 = new TableCell();
                    HtmlInputCheckBox chkInfantHealth = new HtmlInputCheckBox();
                    chkInfantHealth.ID = theDT.Rows[i]["Drug_pk"] + "-" + theDT.Rows[i]["GenericID"].ToString();
                    chkInfantHealth.Value = theDT.Rows[i]["DrugName"].ToString();
                    tcchkInfantHealth3.Controls.Add(chkInfantHealth);
                    tcchkInfantHealth3.Controls.Add(new LiteralControl(chkInfantHealth.Value));
                    trInfantHealth.Cells.Add(tcchkInfantHealth3);
                    DataView theDV_IH = new DataView(theDS.Tables[19]);
                    theDV_IH.RowFilter = "DrugID = " + Convert.ToInt32(theDT.Rows[i]["Drug_pk"]) + "";
                    DropDownList ddlInfantHealth = new DropDownList();
                    ddlInfantHealth.ID = "ddl" + theDT.Rows[i]["Drug_pk"] + theDT.Rows[i]["GenericID"];
                    theBindManager.BindCombo(ddlInfantHealth, theDV_IH.ToTable(), "Name", "ID");
                    tcDDInfantHealth3.Controls.Add(ddlInfantHealth);
                    trInfantHealth.Cells.Add(tcDDInfantHealth3);
                    j = 0;
                    k++;
                }
                if (k == 3)
                {
                    //tblInfantHealth.Rows.Add(trInfantHealth);
                    k = 0;
                    trInfantHealth = new TableRow();
                }
            }
        }

        /// <summary>
        /// Clears the objects.
        /// </summary>
        private void ClearObjects()
        {
            ViewState.Remove("OrigOrdDate");
            ViewState.Remove("PtnID");
            ViewState.Remove("UserID");
            ViewState.Remove("LocationId");
            ViewState.Remove("SelectedDrug");
            ViewState.Remove("MasterData");
            ViewState.Remove("Status");
            ViewState.Remove("EnrolmentDate");
            ViewState.Remove("MasterDrugTable");
            ViewState.Remove("OldDS");
            ViewState.Remove("AddARV");
            ViewState.Remove("OtherDrugs");
            ViewState.Remove("PharmacyId");
            ViewState.Remove("PatientId");
            ViewState.Remove("Data");
            ViewState.Remove("ControlCreated");
            ViewState.Remove("CustomFieldsData");
            ViewState.Remove("CustomFieldsMulti");
            ViewState.Remove("PharmacyDetail");
        }

        /// <summary>
        /// Handles the Load event of the Control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Control_Load(object sender, EventArgs e)
        {
            TextBox tbox = (TextBox)sender;
            tbox.MaxLength = 5;
            //tbox.Attributes.Add("onkeyup", "chkNumber('" + tbox.ClientID + "')");
            if (Session["SCMModule"] != null && Session["Paperless"].ToString() == "1")
            {
                if (tbox.ClientID.Contains("QtyDispensed"))
                {
                    tbox.Attributes.Add("readOnly", "true");
                }
                else
                {
                    tbox.Attributes.Add("onkeyup", "chkDecimal('" + tbox.ClientID + "')");
                }
            }
            else if (Session["SCMModule"] == null && Session["Paperless"].ToString() == "1" && (tbox.ClientID.Contains("QtyDispensed")))
            {
                if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
                {
                    tbox.Attributes.Add("readOnly", "true");
                }
                else
                {
                    tbox.Attributes.Clear();
                    //("readonly","false");
                    tbox.Attributes.Add("onkeyup", "chkDecimal('" + tbox.ClientID + "')");
                }
            }
            else
            {
                tbox.Attributes.Add("onkeyup", "chkDecimal('" + tbox.ClientID + "')");
            }
        }

        /// <summary>
        /// Creates the ar VCHK table.
        /// </summary>
        /// <returns></returns>
        private DataTable CreateARVchkTable()
        {
            DataTable theDT = new DataTable();
            DataColumn theDrugId;

            DataColumn theDrugchecked;

            theDrugId = new DataColumn("DrugID");
            theDrugId.DataType = Type.GetType("System.String");
            theDT.Columns.Add(theDrugId);
            theDrugchecked = new DataColumn("theDrugchecked");
            theDrugchecked.DataType = Type.GetType("System.Int32");
            theDT.Columns.Add(theDrugchecked);

            return theDT;
        }

        /// <summary>
        /// Creates the controls.
        /// </summary>
        /// <param name="theCntlDS">The CNTL ds.</param>
        private void CreateControls(DataSet theCntlDS)
        {
            DataSet theDS = new DataSet();
            //theDS.ReadXml(Server.MapPath("..\\XMLFiles\\pediatricpharmacylist.xml"));

            #region CreateTable"
            //DataTable theDrugTable = new DataTable();

            if (ViewState["SelectedDrug"] != null)
            {
                theDrugTable = (DataTable)ViewState["SelectedDrug"];
            }
            else
            {
                theDrugTable = MakeTable();
            }

            #endregion CreateTable"

            int theDrugId;
            try
            {
                int i = 0;
                int genericId = 0;
                foreach (DataRow dr in theDrugTable.Rows)
                {
                    theDrugId = 0;
                    theDrugId = Convert.ToInt32(dr[0].ToString());

                    genericId = 0;
                    if (Convert.ToInt32(dr["GenericId"]) > 0)
                    {
                        genericId = Convert.ToInt32(dr["GenericId"].ToString());
                    }
                    else
                    {
                        genericId = Convert.ToInt32(dr["DrugId"].ToString());
                    }

                    Panel thePnl = new Panel();
                    thePnl.ID = "pnl" + genericId;
                    string strBrowser = Request.Browser.Browser;
                    if (strBrowser == "IE")
                    {
                        thePnl.Height = 20;
                    }
                    else
                    {
                        thePnl.Height = 30;
                    }
                    //thePnl.Height = 20;
                    thePnl.Width = 870;
                    thePnl.Controls.Clear();
                    if (genericId > 10000)
                    {
                        Label theHeading = new Label();

                        if (Convert.ToInt32(dr["GenericId"]) > 0)
                        {
                            theHeading.Text = dr["GenericName"].ToString();
                            theHeading.ID = "lbl" + dr["GenericName"].ToString();
                        }
                        else
                        {
                            theHeading.Text = dr["DrugName"].ToString();
                            theHeading.ID = "lbl" + dr["DrugName"].ToString();
                        }
                        theHeading.Font.Bold = true;
                        thePnl.Controls.Add(theHeading);
                        if (theHeading.Text == "NRTI")
                        {
                        }
                    }
                    else
                    {
                        Label theDrugNm = new Label();
                        theDrugNm.ID = "drgNm" + genericId;
                        if (Convert.ToInt32(dr["GenericId"]) > 0)
                        {
                            theDrugNm.Text = dr["GenericName"].ToString();
                        }
                        else
                        {
                            theDrugNm.Text = dr["DrugName"].ToString();
                        }
                        theDrugNm.Width = 80;
                        thePnl.Controls.Add(theDrugNm);

                        Label theSpace = new Label();
                        theSpace.ID = "lblSp1" + genericId;
                        theSpace.Width = 18;
                        theSpace.Text = "";
                        thePnl.Controls.Add(theSpace);

                        BindFunctions theBindMgr = new BindFunctions();
                        DropDownList theDrugStrength = new DropDownList();
                        theDrugStrength.ID = "drgStrength" + genericId;
                        theDrugStrength.Width = 80;
                        #region "BindCombo"

                        DataTable theDTS = new DataTable();
                        DataView theDVStrength = new DataView(theCntlDS.Tables[1]);

                        if (Convert.ToInt32(dr["GenericId"]) > 0)
                        {
                            theDVStrength.RowFilter = "GenericId = " + genericId;
                        }
                        else
                        {
                            theDTS = (DataTable)Session["FixDrugStrength"];
                            theDVStrength = new DataView(theDTS);
                            theDVStrength.RowFilter = "Drug_pk = " + Convert.ToInt32(dr["DrugId"]) + " and StrengthId>0";
                        }
                        DataTable theDTStrength = new DataTable();
                        if (theDVStrength.Count > 0)
                        {
                            IQCareUtils theUtils = new IQCareUtils();
                            theDTStrength = theUtils.CreateTableFromDataView(theDVStrength);
                            theBindMgr.BindCombo(theDrugStrength, theDTStrength, "StrengthName", "StrengthId");
                        }

                        #endregion "BindCombo"
                        thePnl.Controls.Add(theDrugStrength);

                        //////////////Space////////////////////////
                        Label theSpace6 = new Label();
                        theSpace6.ID = "lblSp6" + genericId;
                        theSpace6.Width = 20;
                        theSpace6.Text = "";
                        thePnl.Controls.Add(theSpace6);
                        //////////////////////////////////////////

                        TextBox theDose = new TextBox();
                        theDose.ID = "drgDose" + genericId;
                        theDose.Width = 86;
                        theDose.Load += new EventHandler(DecimalText_Load);
                        thePnl.Controls.Add(theDose);

                        //////////////Space////////////////////////
                        Label theSpace1 = new Label();
                        theSpace1.ID = "lblSp2" + genericId;
                        theSpace1.Width = 20;
                        theSpace1.Text = "";
                        thePnl.Controls.Add(theSpace1);
                        //////////////////////////////////////////

                        DropDownList ddlFrequency = new DropDownList();
                        ddlFrequency.ID = "drgFrequency" + genericId;
                        ddlFrequency.Width = 80;
                        //ddlFrequency.AutoPostBack = true; //29Jan08 -- changed for doing in javascript
                        #region "BindCombo"
                        DataTable theDTF = new DataTable();
                        DataView theDVFrequency = new DataView(theCntlDS.Tables[2]);
                        if (Convert.ToInt32(dr["GenericId"]) > 0)
                        {
                            theDVFrequency.RowFilter = "GenericId = " + genericId;
                        }
                        else
                        {
                            theDTF = (DataTable)Session["FixDrugFreq"];
                            theDVFrequency = new DataView(theDTF);
                            theDVFrequency.RowFilter = "Drug_pk = " + Convert.ToInt32(dr["DrugId"]) + " and FrequencyId>0";
                        }
                        DataTable theDTFrequency = new DataTable();
                        if (theDVFrequency.Count > 0)
                        {
                            IQCareUtils theUtils = new IQCareUtils();
                            theDTFrequency = theUtils.CreateTableFromDataView(theDVFrequency);
                            theBindMgr.BindCombo(ddlFrequency, theDTFrequency, "FrequencyName", "FrequencyId");
                        }
                        #endregion "BindCombo"
                        //29Jan08 -- changed for doing in javascript
                        //ddlFrequency.SelectedIndexChanged += new EventHandler(ddlFrequency_SelectedIndexChanged);
                        thePnl.Controls.Add(ddlFrequency);

                        //////////////Space////////////////////////
                        Label theSpace2 = new Label();
                        theSpace2.ID = "lblSp3" + genericId;
                        theSpace2.Width = 20;
                        theSpace2.Text = "";
                        thePnl.Controls.Add(theSpace2);
                        //////////////////////////////////////////

                        TextBox theDuration = new TextBox();
                        theDuration.ID = "drgDuration" + genericId;
                        theDuration.Width = 86;
                        theDuration.Load += new EventHandler(DecimalText_Load);
                        thePnl.Controls.Add(theDuration);

                        //////////////Space////////////////////////
                        Label theSpace3 = new Label();
                        theSpace3.ID = "lblSp4" + genericId;
                        theSpace3.Width = 25;
                        theSpace3.Text = "";
                        thePnl.Controls.Add(theSpace3);
                        //////////////////////////////////////////

                        TextBox theQtyPrescribed = new TextBox();
                        theQtyPrescribed.ID = "drgQtyPrescribed" + genericId;
                        theQtyPrescribed.Width = 86;
                        theQtyPrescribed.Load += new EventHandler(DecimalText_Load);
                        thePnl.Controls.Add(theQtyPrescribed);
                        //////////////Space////////////////////////
                        Label theSpace4 = new Label();
                        theSpace4.ID = "lblSp5" + genericId;
                        theSpace4.Width = 25;
                        theSpace4.Text = "";
                        thePnl.Controls.Add(theSpace4);
                        //////////////////////////////////////////

                        TextBox theQtyDispensed = new TextBox();
                        theQtyDispensed.ID = "drgQtyDispensed" + genericId;
                        theQtyDispensed.Width = 86;
                        theQtyDispensed.Load += new EventHandler(DecimalText_Load);
                        thePnl.Controls.Add(theQtyDispensed);
                        //////////////Space////////////////////////
                        Label theSpace5 = new Label();
                        theSpace5.ID = "lblSp8" + genericId;
                        theSpace5.Width = 20;
                        theSpace5.Text = "";
                        thePnl.Controls.Add(theSpace5);

                        //Prophylaxis
                        CheckBox ProphylaxisChk = new CheckBox();
                        ProphylaxisChk.ID = "chkProphylaxis" + genericId;
                        ProphylaxisChk.Width = 10;
                        ProphylaxisChk.Text = "";
                        thePnl.Controls.Add(ProphylaxisChk);

                        ////////////Space///////////////////////
                        Label theSpace8 = new Label();
                        theSpace8.ID = "lblSp11" + genericId;
                        theSpace8.Width = 20;
                        theSpace8.Text = "";
                        thePnl.Controls.Add(theSpace8);
                        //////////////////////////////////////////
                        if (genericId == 281 || genericId == 150)
                        {
                            ProphylaxisChk.Visible = true;
                            Label theSpacehidden = new Label();
                            theSpacehidden.ID = "theSpacehidden" + genericId;
                            theSpacehidden.Width = 20;
                            theSpacehidden.Text = "";
                            thePnl.Controls.Add(theSpacehidden);
                        }

                        if (i == 0)
                        {
                            hidchkbox.Value = ProphylaxisChk.ID;
                        }
                        else
                        {
                            hidchkbox.Value = hidchkbox.Value + "," + ProphylaxisChk.ID;
                        }
                        i = i + 1;
                    }

                    if (genericId == 10006 || genericId == 281 || genericId == 150)
                    {
                        //PnlOIARV.Controls.Add(thePnl);
                    }
                    else
                    {
                        pnlPedia.Controls.Add(thePnl);
                    }

                    AddControlsAttributes(pnlPedia);
                }
            }
            catch (Exception er)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = er.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }

        /// <summary>
        /// Creates the table.
        /// </summary>
        /// <returns></returns>
        private DataTable CreateTable()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add(new DataColumn("DrugID", Type.GetType("System.Int32")));
            theDT.Columns.Add(new DataColumn("StrengthId", Type.GetType("System.Int32")));
            theDT.Columns.Add(new DataColumn("Frequencyid", Type.GetType("System.Int32")));
            theDT.Columns.Add(new DataColumn("SingleDose", Type.GetType("System.Decimal")));
            theDT.Columns.Add(new DataColumn("Duration", Type.GetType("System.Decimal")));
            theDT.Columns.Add(new DataColumn("DrugSchedule", Type.GetType("System.Int32")));
            theDT.Columns.Add(new DataColumn("QtyPrescribed", Type.GetType("System.Decimal")));
            theDT.Columns.Add(new DataColumn("QtyDispensed", Type.GetType("System.Decimal")));
            theDT.Columns.Add(new DataColumn("Financed", Type.GetType("System.Int32")));
            theDT.Columns.Add(new DataColumn("DrugName", Type.GetType("System.String")));
            theDT.Columns.Add(new DataColumn("GenericId", Type.GetType("System.Int32")));
            theDT.Columns.Add(new DataColumn("Dose", Type.GetType("System.Decimal")));
            theDT.Columns.Add(new DataColumn("UnitId", Type.GetType("System.Int32")));
            theDT.Columns.Add(new DataColumn("TotDailyDose", Type.GetType("System.Decimal")));
            theDT.Columns.Add(new DataColumn("TBRegimenId", Type.GetType("System.Int32")));
            theDT.Columns.Add(new DataColumn("TreatmentPhase", Type.GetType("System.String")));
            theDT.Columns.Add(new DataColumn("TrMonth", Type.GetType("System.Int32")));
            theDT.Columns.Add(new DataColumn("Prophylaxis", Type.GetType("System.Int32")));
            theDT.Columns.Add(new DataColumn("PrintPrescriptionStatus", Type.GetType("System.Int32")));
            theDT.Columns.Add(new DataColumn("PatientInstructions", Type.GetType("System.String")));
            theDT.Columns.Add(new DataColumn("ScheduleId", Type.GetType("System.Int32")));
            return theDT;
        }

        /// <summary>
        /// Handles the SelectedChanged event of the ddlFixDrugname control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ddlFixDrugname_SelectedChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the Load event of the DecimalText control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void DecimalText_Load(object sender, EventArgs e)
        {
            TextBox tbox = (TextBox)sender;
            tbox.MaxLength = 5;
            tbox.Attributes.Add("onkeyup", "chkDecimal('" + tbox.ClientID + "')");
        }

        /// <summary>
        /// Deletes the form.
        /// </summary>
        private void DeleteForm()
        {
            int theResultRow, OrderNo;
            string FormName;
            OrderNo = Convert.ToInt32(Session["PatientVisitId"]);
            FormName = "Pharmacy Form";

            IPediatric PediatricManager = (IPediatric)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BPediatric, BusinessProcess.Pharmacy");
            theResultRow = PediatricManager.DeletePediatricForms(FormName, OrderNo, Convert.ToInt32(Session["PatientId"].ToString()), Convert.ToInt32(ViewState["UserID"]));

            if (theResultRow == 0)
            {
                IQCareMsgBox.Show("RemoveFormError", this);
                return;
            }
            else
            {
                string theUrl;
                theUrl = string.Format("{0}?PatientId={1}", "../ClinicalForms/frmPatient_Home.aspx", Session["PatientId"].ToString());
                Response.Redirect(theUrl);
            }
        }

        /// <summary>
        /// Handles the Load event of the Dose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Dose_Load(object sender, EventArgs e)
        {
            TextBox tbox = (TextBox)sender;
            tbox.MaxLength = 8;
            Int32 ipos = Convert.ToInt32(tbox.ID.IndexOf("^"));
            int DrugId = Convert.ToInt32(tbox.ID.Substring(7, ipos - 7));
            //int DrugId = Convert.ToInt32(tbox.ID.Substring(7, tbox.ID.Length - 7));
            DataView theDV = new DataView(((DataSet)ViewState["MasterData"]).Tables[9]);
            if (Convert.ToDecimal(tbox.Text) == 0)
            {
                theDV.RowFilter = "GenericId= " + DrugId.ToString();
            }
            else
            {
                theDV.RowFilter = "DrugId= " + DrugId.ToString();
            }
            if (ViewState["PharmacyId"] == null)
                tbox.Text = "";
            if (theDV.Count > 0)
            {
                tbox.Attributes.Add("onkeyup", "chkNumeric('" + tbox.ClientID + "'); AddBoundary('" + tbox.ClientID + "','" + theDV[0]["MinDose"] + "','" + theDV[0]["MaxDose"] + "')");
            }
            else
            {
                tbox.Attributes.Add("onkeyup", "chkNumeric('" + tbox.ClientID + "'); AddBoundary('" + tbox.ClientID + "','0','99999999')");
            }
        }

        /// <summary>
        /// Duplicates the regimen validate.
        /// </summary>
        /// <param name="DrugTable">The drug table.</param>
        /// <param name="Master">The master.</param>
        /// <returns></returns>
        private bool DuplicateRegimenValidate(DataTable DrugTable, DataSet Master)
        {
            IDrug DrugManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BDrug,BusinessProcess.Pharmacy");
            IQCareUtils theUtils = new IQCareUtils();
            DataSet objPatientStatus = new DataSet();
            #region "Regimen"

            string theRegimen = "";

            for (int i = 0; i < DrugTable.Rows.Count; i++)
            {
                if (Convert.ToInt32(DrugTable.Rows[i]["GenericId"]) == 0)
                {
                    DataView theDV = new DataView(Master.Tables[0]);
                    theDV.RowFilter = "Drug_Pk = " + DrugTable.Rows[i]["DrugId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];
                    if (theDV.Count > 0)
                    {
                        if (theRegimen == "")
                        {
                            theRegimen = theDV[0]["GenericAbbrevation"].ToString();
                        }
                        else
                        {
                            theRegimen = theRegimen + "/" + theDV[0]["GenericAbbrevation"].ToString();
                        }
                    }
                    theRegimen = theRegimen.Trim();
                }
                else
                {
                    DataView theDV = new DataView(Master.Tables[4]);
                    theDV.RowFilter = "GenericId = " + DrugTable.Rows[i]["GenericId"] + " and DrugTypeID = 37"; ///DrugAbbreviation = " + theDrgMst.Rows[i][2];
                    if (theDV.Count > 0)
                    {
                        if (theRegimen == "")
                        {
                            theRegimen = theDV[0]["GenericAbbrevation"].ToString();
                        }
                        else
                        {
                            theRegimen = theRegimen + "/" + theDV[0]["GenericAbbrevation"].ToString();
                        }
                    }
                    theRegimen = theRegimen.Trim();
                }
            }

            #endregion "Regimen"
            string strreporteddate = "";
            if (Session["SCMModule"] == null)
            {
                strreporteddate = txtpharmReportedbyDate.Value;
            }
            else
            {
                strreporteddate = txtpharmOrderedbyDate.Value;
            }
            objPatientStatus = DrugManager.GetPatientRecordformStatus(Convert.ToInt32(Session["PatientId"]));

            string PreRegimeType = "";
            if (objPatientStatus.Tables[6].Rows.Count > 0)
            {
                PreRegimeType = objPatientStatus.Tables[6].Rows[0][0].ToString();
            };
            string ARVTherapy = "";
            DateTime dtVisitDate = DateTime.Now;
            if (objPatientStatus.Tables[5].Rows.Count > 0)
            {
                ARVTherapy = objPatientStatus.Tables[5].Rows[0][0].ToString();
                dtVisitDate = Convert.ToDateTime(objPatientStatus.Tables[5].Rows[0][1].ToString());
            };

            if (objPatientStatus.Tables[4].Rows.Count > 0)
            {
                if (theRegimen != "")
                {
                    if ((Convert.ToInt32(Session["PatientVisitId"]) == 0) && (Session["PharmacyId"] == null))
                    {
                        if (objPatientStatus.Tables[5].Rows.Count > 0)
                        {
                            // string ARVTherapy = objPatientStatus.Tables[5].Rows[0][0].ToString();
                            //DateTime dtVisitDate = Convert.ToDateTime(objPatientStatus.Tables[5].Rows[0][1].ToString());
                            //string PreRegimeType = objPatientStatus.Tables[6].Rows[0][0].ToString();
                            int Regimen = 0;
                            int PreRegimen = 0;

                            foreach (char a in PreRegimeType)
                            {
                                PreRegimen += Convert.ToInt32(a);
                            }

                            foreach (char b in theRegimen)
                            {
                                Regimen += Convert.ToInt32(b);
                            }

                            if (PreRegimen != Regimen)
                            {
                                if (ARVTherapy == "95")
                                {
                                    if (dtVisitDate <= Convert.ToDateTime(theUtils.MakeDate(strreporteddate)))
                                    {
                                        IQCareMsgBox.Show("currentregimenchange", this);
                                        return false;
                                    }
                                }
                            }
                            else if (PreRegimen == Regimen)
                            {
                                if (ARVTherapy == "98")
                                {
                                    if (dtVisitDate <= Convert.ToDateTime(theUtils.MakeDate(strreporteddate)))
                                    {
                                        IQCareMsgBox.Show("Changeregimen", this);
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                    if ((Convert.ToInt32(Session["PatientVisitId"]) != 0))
                    {
                        if (objPatientStatus.Tables[4].Rows.Count != 1)
                        {
                            if (objPatientStatus.Tables[5].Rows.Count > 0)
                            {
                                // string ARVTherapy = objPatientStatus.Tables[5].Rows[0][0].ToString();
                                // DateTime dtVisitDate = Convert.ToDateTime(objPatientStatus.Tables[5].Rows[0][1].ToString());
                                // string PreRegimeType = objPatientStatus.Tables[6].Rows[0][0].ToString();
                                int Regimen = 0;
                                int PreRegimen = 0;

                                foreach (char a in PreRegimeType)
                                {
                                    PreRegimen += Convert.ToInt32(a);
                                }

                                foreach (char b in theRegimen)
                                {
                                    Regimen += Convert.ToInt32(b);
                                }

                                if (PreRegimen != Regimen)
                                {
                                    if (ARVTherapy == "95")
                                    {
                                        if (dtVisitDate <= Convert.ToDateTime(theUtils.MakeDate(strreporteddate)))
                                        {
                                            IQCareMsgBox.Show("currentregimenchange", this);
                                            return false;
                                        }
                                    }
                                }
                                else if ((PreRegimen == Regimen) && (PreRegimeType != theRegimen))
                                {
                                    if (ARVTherapy == "98")
                                    {
                                        if (dtVisitDate <= Convert.ToDateTime(theUtils.MakeDate(strreporteddate)))
                                        {
                                            IQCareMsgBox.Show("Changeregimen", this);
                                            return false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Enables the disable all CheckBox controls.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="checkedVal">if set to <c>true</c> [checked value].</param>
        /// <exception cref="Exception">SelectAllCheckBoxControls   + exp.Message</exception>
        private void EnableDisableAllCheckBoxControls(Control parent, bool checkedVal)
        {
            try
            {
                foreach (Control y in parent.Controls)
                {
                    if (y.GetType() == typeof(Panel))
                    {
                        //
                        foreach (Control x in y.Controls)
                        {
                            if (x.GetType() == typeof(Panel))
                            {
                                EnableDisableAllCheckBoxControls(x, checkedVal);
                            }
                            else if (x.GetType().ToString().Equals("System.Web.UI.WebControls.CheckBox"))
                            {
                                CheckBox chk = (CheckBox)x;
                                chk.Checked = checkedVal;
                            }
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                throw new Exception("SelectAllCheckBoxControls  " + exp.Message);
            }
        }

        /// <summary>
        /// Enables the disable control.
        /// </summary>
        private void EnableDisableControl()
        {
            //--- For Cancel event, on saving the form ---
            string script = "<script language = 'javascript' defer ='defer' id = 'Disable'>\n";
            script += "var dddispBy=document.getElementById('" + ddlDispensedBy.ClientID + "');\n";
            script += "var txtdispDate=document.getElementById('" + txtpharmReportedbyDate.ClientID + "');\n";
            script += "document.getElementById('appDateimg2').disabled = true;\n";
            script += "</script>\n";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Disable", script);
        }

        /// <summary>
        /// Fields the validation.
        /// </summary>
        /// <returns></returns>
        private bool FieldValidation()
        {
            theCurrentDate = SystemSetting.SystemDate;
            IQCareUtils theUtils = new IQCareUtils();

            if (ddlTreatment.SelectedIndex == 0)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["Control"] = "Treatment Program";
                IQCareMsgBox.Show("BlankDropDown", theMsg, this);
                return false;
            }

            if (ddlProvider.SelectedIndex == 0)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["Control"] = "Drug Provider";
                IQCareMsgBox.Show("BlankDropDown", theMsg, this);
                return false;
            }

            if (ddlPharmOrderedbyName.SelectedIndex == 0)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["Control"] = "Prescribed By";
                IQCareMsgBox.Show("BlankDropDown", theMsg, this);
                return false;
            }
            if ((Session["Paperless"].ToString() == "0") && (Session["SCMModule"] == null) && (ddlDispensedBy.SelectedIndex == 0))
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["Control"] = "Dispensed By";
                IQCareMsgBox.Show("BlankDropDown", theMsg, this);
                return false;
            }
            if (ddlPharmSignature.SelectedIndex == 0)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["Control"] = "Signature";
                IQCareMsgBox.Show("BlankDropDown", theMsg, this);
                return false;
            }

            if (txtpharmOrderedbyDate.Value.Trim() == "")
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["Control"] = "PrescribedByDate";
                IQCareMsgBox.Show("BlankTextBox", theMsg, this);
                return false;
            }
            if (txtpharmOrderedbyDate.Value.Trim() != "")
            {
                DateTime theVisitDate = Convert.ToDateTime(theUtils.MakeDate(txtpharmOrderedbyDate.Value.Trim()));

                if (ViewState["EnrolmentDate"] != null)
                {
                    DateTime theEnrolmentDate = Convert.ToDateTime(ViewState["EnrolmentDate"].ToString());
                    if (theEnrolmentDate > theVisitDate)
                    {
                        IQCareMsgBox.Show("PharmacyDetailOrderDate", this);
                        txtpharmOrderedbyDate.Focus();
                        return false;
                    }
                    else if (theVisitDate > theCurrentDate)
                    {
                        IQCareMsgBox.Show("PharmacyDetailOrderTDate", this);
                        txtpharmOrderedbyDate.Focus();
                        return false;
                    }
                }
            }
            if ((Session["Paperless"].ToString() == "0") && (Session["SCMModule"] == null) && (txtpharmReportedbyDate.Value.Trim() == ""))
            {
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["Control"] = "DispensedByDate";
                    IQCareMsgBox.Show("BlankTextBox", theMsg, this);
                    return false;
                }
            }

            if (txtpharmReportedbyDate.Value.Trim() != "")
            {
                DateTime theVisitDate = Convert.ToDateTime(theUtils.MakeDate(txtpharmReportedbyDate.Value.Trim()));

                if (ViewState["EnrolmentDate"] != null)
                {
                    DateTime theEnrolmentDate = Convert.ToDateTime(ViewState["EnrolmentDate"].ToString());
                    if (theEnrolmentDate > theVisitDate)
                    {
                        IQCareMsgBox.Show("PharmacyDetailReportedDate", this);
                        txtpharmReportedbyDate.Focus();
                        return false;
                    }
                    else if (theVisitDate > theCurrentDate)
                    {
                        IQCareMsgBox.Show("PharmacyDetailReportedTDate", this);
                        txtpharmReportedbyDate.Focus();
                        return false;
                    }
                }
            }

            if ((txtpharmOrderedbyDate.Value.Trim() != "") && (txtpharmReportedbyDate.Value.Trim() != ""))
            {
                DateTime theOrdByDate = Convert.ToDateTime(theUtils.MakeDate(txtpharmOrderedbyDate.Value.Trim()));
                DateTime theDispByDate = Convert.ToDateTime(theUtils.MakeDate(txtpharmReportedbyDate.Value.Trim()));
                if (theOrdByDate > theDispByDate)
                {
                    IQCareMsgBox.Show("PharmacyOrderDispenseDate", this);
                    txtpharmOrderedbyDate.Focus();
                    return false;
                }
            }

            if (txtWeight.Text.Trim() != "")
            {
                if (Convert.ToDecimal(txtWeight.Text.ToString()) <= 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["Control"] = "Weight";
                    IQCareMsgBox.Show("GreatThanZero", theMsg, this);
                    return false;
                }
            }

            if (txtHeight.Text.Trim() != "")
            {
                if (Convert.ToDecimal(txtHeight.Text.ToString()) <= 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["Control"] = "Height";
                    IQCareMsgBox.Show("GreatThanZero", theMsg, this);
                    return false;
                }
            }

            //Decimal AgeD = Convert.ToDecimal(txtYr.Text.Trim().ToString()) + Convert.ToDecimal(txtMon.Text.Trim().ToString()) / 12;
            //if (AgeD > 17)
            //{
            //    IQCareMsgBox.Show("PharmacyDetailAge", this);
            //    return false;
            //}

            //---Non-ART already filled : starts-- 29Feb08//

            DataTable theDT = ((DataSet)ViewState["MasterData"]).Tables[15];
            if ((txtpharmOrderedbyDate.Value.Trim() != "") && (theDT.Rows.Count > 0))
            {
                DateTime theOrdByDate = Convert.ToDateTime(theUtils.MakeDate(txtpharmOrderedbyDate.Value.Trim()));
                DateTime theNonARTDate;
                foreach (DataRow theDR in theDT.Rows)
                {
                    theNonARTDate = Convert.ToDateTime(theDR["VisitDate"].ToString());
                    if (theOrdByDate == theNonARTDate)
                    {
                        IQCareMsgBox.Show("PharmacyOrderNonARTDate", this);
                        txtpharmOrderedbyDate.Focus();
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Fields the validation paper less.
        /// </summary>
        /// <returns></returns>
        private bool fieldValidationPaperLess()
        {
            //

            theCurrentDate = SystemSetting.SystemDate;
            IQCareUtils theUtils = new IQCareUtils();

            if (ddlPharmOrderedbyName.SelectedIndex == 0)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["Control"] = "Prescribed By";
                IQCareMsgBox.Show("BlankDropDown", theMsg, this);
                return false;
            }

            if (ddlPharmSignature.SelectedIndex == 0)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["Control"] = "Signature";
                IQCareMsgBox.Show("BlankDropDown", theMsg, this);
                return false;
            }

            if (txtpharmOrderedbyDate.Value.Trim() == "")
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["Control"] = "PrescribedByDate";
                IQCareMsgBox.Show("BlankTextBox", theMsg, this);
                return false;
            }
            if (txtpharmOrderedbyDate.Value.Trim() != "")
            {
                DateTime theVisitDate = Convert.ToDateTime(theUtils.MakeDate(txtpharmOrderedbyDate.Value.Trim()));

                if (ViewState["EnrolmentDate"] != null)
                {
                    DateTime theEnrolmentDate = Convert.ToDateTime(ViewState["EnrolmentDate"].ToString());
                    if (theEnrolmentDate > theVisitDate)
                    {
                        IQCareMsgBox.Show("PharmacyDetailOrderDate", this);
                        txtpharmOrderedbyDate.Focus();
                        return false;
                    }
                    else if (theVisitDate > theCurrentDate)
                    {
                        IQCareMsgBox.Show("PharmacyDetailOrderTDate", this);
                        txtpharmOrderedbyDate.Focus();
                        return false;
                    }
                }
            }

            if (txtpharmReportedbyDate.Value.Trim() != "")
            {
                DateTime theVisitDate = Convert.ToDateTime(theUtils.MakeDate(txtpharmReportedbyDate.Value.Trim()));

                if (ViewState["EnrolmentDate"] != null)
                {
                    DateTime theEnrolmentDate = Convert.ToDateTime(ViewState["EnrolmentDate"].ToString());
                    if (theEnrolmentDate > theVisitDate)
                    {
                        IQCareMsgBox.Show("PharmacyDetailReportedDate", this);
                        txtpharmReportedbyDate.Focus();
                        return false;
                    }
                    else if (theVisitDate > theCurrentDate)
                    {
                        IQCareMsgBox.Show("PharmacyDetailReportedTDate", this);
                        txtpharmReportedbyDate.Focus();
                        return false;
                    }
                }
            }

            if ((txtpharmOrderedbyDate.Value.Trim() != "") && (txtpharmReportedbyDate.Value.Trim() != ""))
            {
                DateTime theOrdByDate = Convert.ToDateTime(theUtils.MakeDate(txtpharmOrderedbyDate.Value.Trim()));
                DateTime theDispByDate = Convert.ToDateTime(theUtils.MakeDate(txtpharmReportedbyDate.Value.Trim()));
                if (theOrdByDate > theDispByDate)
                {
                    IQCareMsgBox.Show("PharmacyOrderDispenseDate", this);
                    txtpharmOrderedbyDate.Focus();
                    return false;
                }
            }

            //if (txtWeight.Text.Trim() == "")
            //{
            //    MsgBuilder theMsg = new MsgBuilder();
            //    theMsg.DataElements["Control"] = "Weight";
            //    IQCareMsgBox.Show("BlankTextBox", theMsg, this);
            //    return false;
            //}
            if (txtWeight.Text.Trim() != "")
            {
                if (Convert.ToDecimal(txtWeight.Text.ToString()) <= 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["Control"] = "Weight";
                    IQCareMsgBox.Show("GreatThanZero", theMsg, this);
                    return false;
                }
            }

            //if (txtHeight.Text.Trim() == "")
            //{
            //    MsgBuilder theMsg = new MsgBuilder();
            //    theMsg.DataElements["Control"] = "Height";
            //    IQCareMsgBox.Show("BlankTextBox", theMsg, this);
            //    return false;
            //}
            if (txtHeight.Text.Trim() != "")
            {
                if (Convert.ToDecimal(txtHeight.Text.ToString()) <= 0)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["Control"] = "Height";
                    IQCareMsgBox.Show("GreatThanZero", theMsg, this);
                    return false;
                }
            }

            //Decimal AgeD = Convert.ToDecimal(txtYr.Text.Trim().ToString()) + Convert.ToDecimal(txtMon.Text.Trim().ToString()) / 12;
            //if (AgeD > 17)
            //{
            //    IQCareMsgBox.Show("PharmacyDetailAge", this);
            //    return false;
            //}

            //---Non-ART already filled : starts-- 29Feb08//

            DataTable theDT = ((DataSet)ViewState["MasterData"]).Tables[15];
            if ((txtpharmOrderedbyDate.Value.Trim() != "") && (theDT.Rows.Count > 0))
            {
                DateTime theOrdByDate = Convert.ToDateTime(theUtils.MakeDate(txtpharmOrderedbyDate.Value.Trim()));
                DateTime theNonARTDate;
                foreach (DataRow theDR in theDT.Rows)
                {
                    theNonARTDate = Convert.ToDateTime(theDR["VisitDate"].ToString());
                    if (theOrdByDate == theNonARTDate)
                    {
                        IQCareMsgBox.Show("PharmacyOrderNonARTDate", this);
                        txtpharmOrderedbyDate.Focus();
                        return false;
                    }
                }
            }

            //---Non-ART already filled : ends-- 29Feb08 //

            int PtnID = Convert.ToInt32(Session["PatientId"]);
            IPediatric PediatricManager = (IPediatric)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BPediatric, BusinessProcess.Pharmacy");
            DataSet dsExist = PediatricManager.GetExistPharmacyForm(PtnID, Convert.ToDateTime(theUtils.MakeDate(txtpharmOrderedbyDate.Value)));

            if (dsExist != null && dsExist.Tables[0].Rows.Count > 0)
            {
                if ((Convert.ToInt32(Session["PatientVisitId"]) != 0) && (Convert.ToInt32(dsExist.Tables[0].Rows[0][0]) == 0))
                {
                    if (Convert.ToDateTime(ViewState["OrigOrdDate"]) != Convert.ToDateTime(theUtils.MakeDate(txtpharmOrderedbyDate.Value)))
                    {
                        IQCareMsgBox.Show("PharmacyDetailExists", this);
                        return false;
                    }
                }
                //for patient transfer date check
                int PatientID = Convert.ToInt32(Session["PatientId"]);
                if (TransferValidation(PatientID) == false)
                {
                    return false;
                }
                if ((Convert.ToInt32(Session["PatientVisitId"]) == 0) && (ViewState["PharmacyDetail"] == null))
                {
                    if (Convert.ToInt32(dsExist.Tables[0].Rows[0][0]) == 0)
                    {
                        IQCareMsgBox.Show("PharmacyDetailExists", this);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            return true;
        }

        /// <summary>
        /// Fills the old custom data.
        /// </summary>
        /// <param name="PatID">The pat identifier.</param>
        private void FillOldCustomData(Int32 PatID)
        {
            DataSet dsvalues = null;
            ICustomFields CustomFields;
            Int32 PharmacyId = 0;
            if (Session["PatientVisitId"] != null)
                PharmacyId = Convert.ToInt32(Session["PatientVisitId"]);
            try
            {
                DataSet theCustomFields = (DataSet)ViewState["CustomFieldsDS"];
                string theTblName = string.Empty;
                if (theCustomFields.Tables[0].Rows.Count > 0)
                {
                    theTblName = theCustomFields.Tables[0].Rows[0]["FeatureName"].ToString().Replace(" ", "_");
                }
                string theColName = "";
                foreach (DataRow theDR in theCustomFields.Tables[0].Rows)
                {
                    if (theDR["ControlId"].ToString() != "9")
                    {
                        if (theColName == "")
                            theColName = theDR["Label"].ToString();
                        else
                            theColName = theColName + "," + theDR["Label"].ToString();
                    }
                }

                CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
                //dsvalues = CustomFields.GetCustomFieldValues("dtl_CustomField_Pharmacy" + theTblName.ToString().Replace("-", "_"), theColName, Convert.ToInt32(PatID.ToString()), 0, 0, 0, Convert.ToInt32(PharmacyId), Convert.ToInt32(ApplicationAccess.PaediatricPharmacy));
                dsvalues = CustomFields.GetCustomFieldValues("dtl_CustomField_Pharmacy", theColName, Convert.ToInt32(PatID.ToString()), 0, 0, 0, Convert.ToInt32(PharmacyId), Convert.ToInt32(ApplicationAccess.PaediatricPharmacy));
                CustomFieldClinical theCustomManager = new CustomFieldClinical();

                theCustomManager.FillCustomFieldData(theCustomFields, dsvalues, pnlCustomList, "PPharm");
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
            finally
            {
                CustomFields = null;
            }
        }

        //}
        /// <summary>
        /// Fills the old data.
        /// </summary>
        /// <param name="Cntrl">The CNTRL.</param>
        /// <param name="theDR">The dr.</param>
        /// <param name="fillTotDose">if set to <c>true</c> [fill tot dose].</param>
        private void FillOldData(Control Cntrl, DataRow theDR, bool fillTotDose)
        {
            if (theDR["Drug_Pk"] != DBNull.Value)
            {
                int y = 0;
                int drugId;
                string FrequencyNm = string.Empty;

                int ipos;

                if (Convert.ToInt32(theDR["Drug_Pk"]) == 0)
                {
                    drugId = Convert.ToInt32(theDR["GenericId"]);
                }
                else
                {
                    drugId = Convert.ToInt32(theDR["Drug_Pk"]);
                }
                foreach (Control x in Cntrl.Controls)
                {
                    if (x.GetType() == typeof(Panel))
                    {
                        //FillOldData(x, theDR); // 27Feb08
                        if (fillTotDose == true)
                            FillOldData(x, theDR, true);
                        else
                            FillOldData(x, theDR, false);
                    }
                    else
                    {
                        if (x.GetType() == typeof(DropDownList))
                        {
                            if (x.ID.StartsWith("DDRegimenLine"))
                            {
                                ((DropDownList)x).SelectedValue = theDR["RegimenLine"].ToString();
                            }

                            if (x.ID.StartsWith("drgStrength"))
                            {
                                ipos = Convert.ToInt32(x.ID.IndexOf("^"));
                                if (ipos > 0)
                                {
                                    y = Convert.ToInt32(x.ID.Substring(11, ipos - 11));
                                }
                                else
                                {
                                    y = Convert.ToInt32(x.ID.Substring(11, x.ID.Length - 11));
                                }
                                //y = Convert.ToInt32(x.ID.Substring(11, x.ID.Length - 11));
                                if (y == drugId)
                                {
                                    if (Convert.ToInt32(theDR["StrengthId"]) > 0)
                                    {
                                        ((DropDownList)x).SelectedValue = theDR["StrengthId"].ToString();
                                    }
                                    else if (Convert.ToInt32(theDR["UnitId"]) > 0)
                                    {
                                        ((DropDownList)x).SelectedValue = theDR["UnitId"].ToString();
                                    }
                                }
                            }
                            if (x.ID.StartsWith("theUnit"))
                            {
                                ipos = Convert.ToInt32(x.ID.IndexOf("^"));
                                if (ipos > 0)
                                {
                                    y = Convert.ToInt32(x.ID.Substring(7, ipos - 7));
                                }
                                else
                                {
                                    y = Convert.ToInt32(x.ID.Substring(7, x.ID.Length - 7));
                                }
                                //y = Convert.ToInt32(x.ID.Substring(7, x.ID.Length - 7));
                                if (y == drugId)
                                {
                                    ((DropDownList)x).SelectedValue = theDR["UnitId"].ToString();
                                }
                            }
                            if (x.ID.StartsWith("drgFrequency"))
                            {
                                ipos = Convert.ToInt32(x.ID.IndexOf("^"));
                                if (ipos > 0)
                                {
                                    y = Convert.ToInt32(x.ID.Substring(12, ipos - 12));
                                }
                                else
                                {
                                    y = Convert.ToInt32(x.ID.Substring(12, x.ID.Length - 12));
                                }
                                //y = Convert.ToInt32(x.ID.Substring(12, x.ID.Length - 12));
                                if (y == drugId)
                                {
                                    ((DropDownList)x).SelectedValue = theDR["FrequencyId"].ToString();
                                    FrequencyNm = ((DropDownList)x).SelectedItem.ToString();
                                }
                            }
                            if (x.ID.StartsWith("drgVacSchedule"))
                            {
                                DropDownList ddlSchedule = (DropDownList)x;
                                BindScheduleList(drugId, ref ddlSchedule);

                                ipos = Convert.ToInt32(x.ID.IndexOf("^"));
                                if (ipos > 0)
                                {
                                    y = Convert.ToInt32(x.ID.Substring(14, ipos - 14));
                                }
                                else
                                {
                                    y = Convert.ToInt32(x.ID.Substring(14, x.ID.Length - 14));
                                }

                                if (y == drugId)
                                {
                                    ddlSchedule.SelectedValue = theDR["ScheduleId"].ToString();
                                }
                            }
                            if (x.ID.StartsWith("drgTreatmenPhase"))
                            {
                                ipos = Convert.ToInt32(x.ID.IndexOf("^"));
                                if (ipos > 0)
                                {
                                    y = Convert.ToInt32(x.ID.Substring(16, ipos - 16));
                                }
                                else
                                {
                                    y = Convert.ToInt32(x.ID.Substring(16, x.ID.Length - 16));
                                }

                                if (y == drugId)
                                {
                                    ((DropDownList)x).SelectedValue = theDR["TreatmentPhase"].ToString();
                                }
                            }
                            if (x.ID.StartsWith("drgTreatmenMonth"))
                            {
                                ipos = Convert.ToInt32(x.ID.IndexOf("^"));
                                if (ipos > 0)
                                {
                                    y = Convert.ToInt32(x.ID.Substring(16, ipos - 16));
                                }
                                else
                                {
                                    y = Convert.ToInt32(x.ID.Substring(16, x.ID.Length - 16));
                                }

                                if (y == drugId)
                                {
                                    ((DropDownList)x).SelectedValue = theDR["Month"].ToString();
                                }
                            }
                        }
                        if (x.GetType() == typeof(TextBox))
                        {
                            if (x.ID.StartsWith("drgDose"))
                            {
                                ipos = Convert.ToInt32(x.ID.IndexOf("^"));
                                if (ipos > 0)
                                {
                                    y = Convert.ToInt32(x.ID.Substring(7, ipos - 7));
                                }
                                else
                                {
                                    y = Convert.ToInt32(x.ID.Substring(7, x.ID.Length - 7));
                                }
                                //y = Convert.ToInt32(x.ID.Substring(7, x.ID.Length - 7));
                                if (y == drugId)
                                {
                                    int DecPos = theDR["SingleDose"].ToString().IndexOf(".");
                                    if (DecPos != -1)
                                    {
                                        //int DecValue = Convert.ToInt32(theDR["DispensedQuantity"].ToString().Substring(DecPos + 1, 2));
                                        decimal DecValue = Convert.ToDecimal(theDR["SingleDose"].ToString().Substring(DecPos + 1, 1));
                                        if (DecValue > 0)
                                        {
                                            ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["SingleDose"].ToString()), 1));
                                        }
                                        else
                                        {
                                            ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["SingleDose"]), 1));
                                        }
                                    }
                                    else
                                    {
                                        if (theDR["SingleDose"] != System.DBNull.Value)
                                            ((TextBox)x).Text = Convert.ToString(Math.Round(Convert.ToDecimal(theDR["SingleDose"]), 1));
                                    }
                                }
                            }
                            if (x.ID.StartsWith("theDose"))
                            {
                                ipos = Convert.ToInt32(x.ID.IndexOf("^"));
                                if (ipos > 0)
                                {
                                    y = Convert.ToInt32(x.ID.Substring(7, ipos - 7));
                                }
                                else
                                {
                                    y = Convert.ToInt32(x.ID.Substring(7, x.ID.Length - 7));
                                }
                                //y = Convert.ToInt32(x.ID.Substring(7, x.ID.Length - 7));
                                if (y == drugId)
                                {
                                    ((TextBox)x).Text = theDR["Dose"].ToString();
                                }
                            }
                            if (x.ID.StartsWith("drgDuration"))
                            {
                                ipos = Convert.ToInt32(x.ID.IndexOf("^"));
                                if (ipos > 0)
                                {
                                    y = Convert.ToInt32(x.ID.Substring(11, ipos - 11));
                                }
                                else
                                {
                                    y = Convert.ToInt32(x.ID.Substring(11, x.ID.Length - 11));
                                }
                                //y = Convert.ToInt32(x.ID.Substring(11, x.ID.Length - 11));
                                if (y == drugId)
                                {
                                    int DecPos = theDR["Duration"].ToString().IndexOf(".");
                                    if (DecPos != -1)
                                    {
                                        //int DecValue = Convert.ToInt32(theDR["DispensedQuantity"].ToString().Substring(DecPos + 1, 2));
                                        decimal DecValue = Convert.ToDecimal(theDR["Duration"].ToString().Substring(DecPos + 1, 2));
                                        if (DecValue > 0)
                                        {
                                            ((TextBox)x).Text = theDR["Duration"].ToString();
                                        }
                                        else
                                        {
                                            ((TextBox)x).Text = Convert.ToString(Convert.ToInt32(theDR["Duration"]));
                                        }
                                    }
                                    else
                                    {
                                        if (theDR["Duration"] != System.DBNull.Value)
                                        {
                                            ((TextBox)x).Text = Convert.ToString(Convert.ToInt32(theDR["Duration"]));
                                        }
                                    }
                                    //((TextBox)x).Text = theDR["Duration"].ToString();
                                }
                            }
                            if (x.ID.StartsWith("drgQtyPrescribed"))
                            {
                                ipos = Convert.ToInt32(x.ID.IndexOf("^"));
                                if (ipos > 0)
                                {
                                    y = Convert.ToInt32(x.ID.Substring(16, ipos - 16));
                                }
                                else
                                {
                                    y = Convert.ToInt32(x.ID.Substring(16, x.ID.Length - 16));
                                }
                                //y = Convert.ToInt32(x.ID.Substring(16, x.ID.Length - 16));
                                if (y == drugId)
                                {
                                    int DecPos = theDR["OrderedQuantity"].ToString().IndexOf(".");
                                    //rupesh
                                    //int DecValue = Convert.ToInt32(theDR["OrderedQuantity"].ToString().Substring(DecPos + 1, 2));

                                    if (DecPos != -1)
                                    {
                                        decimal DecValue = Convert.ToDecimal(theDR["OrderedQuantity"].ToString().Substring(DecPos + 1, 2));
                                        if (DecValue > 0)
                                        {
                                            ((TextBox)x).Text = theDR["OrderedQuantity"].ToString();
                                        }
                                        else
                                        {
                                            if (theDR["Duration"] != System.DBNull.Value)
                                            {
                                                ((TextBox)x).Text = Convert.ToString(Convert.ToInt32(theDR["OrderedQuantity"]));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (theDR["Duration"] != System.DBNull.Value)
                                        {
                                            ((TextBox)x).Text = Convert.ToString(Convert.ToInt32(theDR["OrderedQuantity"]));
                                        }
                                    }
                                    //((TextBox)x).Text = theDR["OrderedQuantity"].ToString();
                                }
                            }
                            if (x.ID.StartsWith("drgQtyDispensed"))
                            {
                                ipos = Convert.ToInt32(x.ID.IndexOf("^"));
                                if (ipos > 0)
                                {
                                    y = Convert.ToInt32(x.ID.Substring(15, ipos - 15));
                                }
                                else
                                {
                                    y = Convert.ToInt32(x.ID.Substring(15, x.ID.Length - 15));
                                }
                                //y = Convert.ToInt32(x.ID.Substring(15, x.ID.Length - 15));
                                if (y == drugId)
                                {
                                    int DecPos = theDR["DispensedQuantity"].ToString().IndexOf(".");
                                    //int DecValue = Convert.ToInt32(theDR["DispensedQuantity"].ToString().Substring(DecPos + 1, 2));
                                    if (DecPos != -1)
                                    {
                                        decimal DecValue = Convert.ToDecimal(theDR["DispensedQuantity"].ToString().Substring(DecPos + 1, 2));
                                        if (theDR["DispensedQuantity"].ToString() != "0.00")
                                        {
                                            if (DecValue > 0)
                                            {
                                                ((TextBox)x).Text = theDR["DispensedQuantity"].ToString();
                                            }
                                            else
                                            {
                                                ((TextBox)x).Text = Convert.ToString(Convert.ToInt32(theDR["DispensedQuantity"]));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        ((TextBox)x).Text = Convert.ToString(Convert.ToInt32(theDR["DispensedQuantity"]));
                                    }
                                    //((TextBox)x).Text = theDR["DispensedQuantity"].ToString();
                                }
                            }

                            //patient instructions
                            if (x.ID.StartsWith("txtPtnInstructions"))
                            {
                                ipos = Convert.ToInt32(x.ID.IndexOf("^"));
                                if (ipos > 0)
                                {
                                    y = Convert.ToInt32(x.ID.Substring(18, ipos - 18));
                                }
                                else
                                {
                                    y = Convert.ToInt32(x.ID.Substring(18, x.ID.Length - 18));
                                }

                                //y = Convert.ToInt32(x.ID.Substring(18, x.ID.Length - 18));
                                if (y == drugId)
                                {
                                    if (theDR["PatientInstructions"] != System.DBNull.Value)
                                    {
                                        ((TextBox)x).Text = theDR["PatientInstructions"].ToString();
                                    }
                                }
                            }
                        }

                        if (x.GetType() == typeof(CheckBox))
                        {
                            if (x.ID.StartsWith("FinChk"))
                            {
                                ipos = Convert.ToInt32(x.ID.IndexOf("^"));
                                if (ipos > 0)
                                {
                                    y = Convert.ToInt32(x.ID.Substring(6, ipos - 6));
                                }
                                else
                                {
                                    y = Convert.ToInt32(x.ID.Substring(6, x.ID.Length - 6));
                                }
                                //y = Convert.ToInt32(x.ID.Substring(6, x.ID.Length - 6));
                                if (y == drugId)
                                {
                                    if (Convert.ToInt32(theDR["Financed"].ToString()) == 1)
                                    {
                                        ((CheckBox)x).Checked = true;
                                    }
                                    else
                                    {
                                        ((CheckBox)x).Checked = false;
                                    }
                                }
                            }
                            if (x.ID.StartsWith("chkProphylaxis"))
                            {
                                ipos = Convert.ToInt32(x.ID.IndexOf("^"));
                                if (ipos > 0)
                                {
                                    y = Convert.ToInt32(x.ID.Substring(14, ipos - 14));
                                }
                                else
                                {
                                    y = Convert.ToInt32(x.ID.Substring(14, x.ID.Length - 14));
                                }

                                if (y == drugId)
                                {
                                    if (Convert.ToInt32(theDR["Prophylaxis"].ToString()) == 1)
                                    {
                                        ((CheckBox)x).Checked = true;
                                    }
                                    else
                                    {
                                        ((CheckBox)x).Checked = false;
                                    }
                                }
                            }
                            if (x.ID.StartsWith("chkPrintPrescription"))
                            {                                
                                y = Convert.ToInt32(x.ID.Substring(20, x.ID.Length - 20));

                                if (y == drugId)
                                {
                                    if (Convert.ToInt32(theDR["PrintPrescriptionStatus"].ToString()) == 1)
                                    {
                                        ((CheckBox)x).Checked = true;
                                    }
                                    else
                                    {
                                        ((CheckBox)x).Checked = false;
                                    }
                                }
                            }
                        }
                        if (x.GetType() == typeof(LinkButton))
                        {
                            if (x.ID.StartsWith("lnkrmv"))
                            {
                                ipos = Convert.ToInt32(x.ID.IndexOf("^"));
                                if (ipos > 0)
                                {
                                    ipos = ipos + 1;
                                    y = Convert.ToInt32(x.ID.Substring(ipos, x.ID.Length - ipos));
                                }
                                if (y == drugId)
                                {
                                    //here ((LinkButton)x).Visible = false;
                                }
                            }
                        }
                    }
                }
            }
        }

       
        /// <summary>
        /// Fills the old fixed data.
        /// </summary>
        /// <param name="Cntrl">The CNTRL.</param>
        /// <param name="theDR">The dr.</param>
        /// <param name="fillTotDose">if set to <c>true</c> [fill tot dose].</param>
        private void FillOldFixedData(Control Cntrl, DataRow theDR, Boolean fillTotDose)
        {
            int DrugId;
            string FrequencyNm = string.Empty;
            Int32 Frequency = 0;

            if (Convert.ToInt32(theDR["Drug_Pk"]) != 0)
            {
                foreach (DataRow theDRFixDose in mainDataSet.Tables[23].Rows)
                {
                    if (Convert.ToInt32(theDRFixDose["drug_pk"]) == Convert.ToInt32(theDR["drug_pk"]))
                    {
                        DrugId = Convert.ToInt32(theDR["Drug_Pk"]);
                        foreach (Control x in Cntrl.Controls)
                        {
                            if (x.GetType() == typeof(Panel))
                            {
                                //FillOldData(x, theDR); // 27Feb08
                                if (fillTotDose == true)
                                    FillOldFixedData(x, theDR, true);
                                else
                                    FillOldFixedData(x, theDR, false);
                            }
                            else
                            {
                                if (x.GetType() == typeof(DropDownList))
                                {
                                    if (x.ID == "theFixedDrugName")
                                    {
                                        ((DropDownList)x).SelectedValue = DrugId.ToString();
                                        EventArgs s = new EventArgs();
                                        ddlFixDrugname_SelectedChanged((DropDownList)x, s);
                                    }
                                    if (x.ID == "theFixedDrugStrength")
                                    {
                                        if (Convert.ToInt32(theDR["StrengthId"]) > 0)
                                        {
                                            ((DropDownList)x).SelectedValue = theDR["StrengthId"].ToString();
                                        }
                                        else if (Convert.ToInt32(theDR["UnitId"]) > 0)
                                        {
                                            ((DropDownList)x).SelectedValue = theDR["UnitId"].ToString();
                                        }
                                    }
                                    if (x.ID.StartsWith("theUnit"))
                                    {
                                        ((DropDownList)x).SelectedValue = theDR["UnitId"].ToString();
                                    }
                                    if (x.ID == "drgFixFrequency")
                                    {
                                        ((DropDownList)x).SelectedValue = theDR["FrequencyId"].ToString();
                                        FrequencyNm = ((DropDownList)x).SelectedItem.ToString();
                                    }
                                    if (x.ID.StartsWith("drgTreatmenPhase"))
                                    {
                                        ((DropDownList)x).SelectedValue = theDR["TreatmentPhase"].ToString();
                                    }
                                    if (x.ID.StartsWith("drgTreatmenMonth"))
                                    {
                                        ((DropDownList)x).SelectedValue = theDR["Month"].ToString();
                                    }
                                }
                                if (x.GetType() == typeof(TextBox))
                                {
                                    if (x.ID == "drgFixTotalDose")
                                    {
                                        if (FrequencyNm == "OD")
                                        {
                                            Frequency = 1;
                                        }
                                        else if (FrequencyNm == "BD")
                                        {
                                            Frequency = 2;
                                        }
                                        else if (FrequencyNm == "1OD")
                                        {
                                            Frequency = 1;
                                        }
                                        else if (FrequencyNm == "2OD")
                                        {
                                            Frequency = 2;
                                        }
                                        else if (FrequencyNm == "1BD")
                                        {
                                            Frequency = 2;
                                        }
                                        else if (FrequencyNm == "3OD" || FrequencyNm == "TD")
                                        {
                                            Frequency = 3;
                                        }
                                        else if (FrequencyNm == "qid")
                                        {
                                            Frequency = 4;
                                        }
                                        if (theDR["SingleDose"].ToString() != "")
                                        {
                                            int DecPos = theDR["SingleDose"].ToString().IndexOf(".");
                                            //int DecValue = Convert.ToInt32(theDR["SingleDose"].ToString().Substring(DecPos + 1, 2));
                                            #region "14-Jun-07 -2 "
                                            //int DecValue = Convert.ToInt32(theDR["SingleDose"].ToString().Substring(DecPos + 1, theDR["SingleDose"].ToString().Trim().Length));
                                            int DecValue = Convert.ToInt32(theDR["SingleDose"].ToString().Substring(DecPos + 1, theDR["SingleDose"].ToString().Trim().Length - (DecPos + 1)));
                                            #endregion "14-Jun-07 -2 "
                                            if (DecValue > 0)
                                            {
                                                //((TextBox)x).Text = theDR["SingleDose"].ToString();
                                                if (fillTotDose == true)
                                                    ((TextBox)x).Text = Convert.ToString(Frequency * Convert.ToDecimal(theDR["SingleDose"].ToString()));
                                                else
                                                    ((TextBox)x).Text = theDR["TotDailyDose"].ToString();
                                            }
                                            else
                                            {
                                                if (fillTotDose == true)
                                                    ((TextBox)x).Text = Convert.ToString(Frequency * Convert.ToInt32(theDR["SingleDose"]));
                                                else
                                                    ((TextBox)x).Text = theDR["TotDailyDose"].ToString();
                                            }
                                            //((TextBox)x).Text = Convert.ToString(Frequency * Convert.ToInt32(theDR["SingleDose"].ToString()));
                                        }
                                        else
                                        {
                                            ((TextBox)x).Text = Convert.ToString(Frequency * Convert.ToInt32(theDR["Dose"].ToString()));
                                        }
                                        FrequencyNm = string.Empty;
                                    }
                                    if (x.ID == "drgFixDose")
                                    {
                                        int DecPos = theDR["SingleDose"].ToString().IndexOf(".");
                                        if (DecPos != -1)
                                        {
                                            //int DecValue = Convert.ToInt32(theDR["DispensedQuantity"].ToString().Substring(DecPos + 1, 2));
                                            decimal DecValue = Convert.ToDecimal(theDR["SingleDose"].ToString().Substring(DecPos + 1, 2));
                                            if (DecValue > 0)
                                            {
                                                ((TextBox)x).Text = theDR["SingleDose"].ToString();
                                            }
                                            else
                                            {
                                                ((TextBox)x).Text = Convert.ToString(Convert.ToInt32(theDR["SingleDose"]));
                                            }
                                        }
                                        else
                                        {
                                            ((TextBox)x).Text = Convert.ToString(Convert.ToInt32(theDR["SingleDose"]));
                                        }
                                    }
                                    if (x.ID.StartsWith("theDose"))
                                    {
                                        ((TextBox)x).Text = theDR["Dose"].ToString();
                                    }
                                    if (x.ID == "drgFixDuration")
                                    {
                                        int DecPos = theDR["Duration"].ToString().IndexOf(".");
                                        if (DecPos != -1)
                                        {
                                            //int DecValue = Convert.ToInt32(theDR["DispensedQuantity"].ToString().Substring(DecPos + 1, 2));
                                            decimal DecValue = Convert.ToDecimal(theDR["Duration"].ToString().Substring(DecPos + 1, 2));
                                            if (DecValue > 0)
                                            {
                                                ((TextBox)x).Text = theDR["Duration"].ToString();
                                            }
                                            else
                                            {
                                                ((TextBox)x).Text = Convert.ToString(Convert.ToInt32(theDR["Duration"]));
                                            }
                                        }
                                        else
                                        {
                                            ((TextBox)x).Text = Convert.ToString(Convert.ToInt32(theDR["Duration"]));
                                        }
                                        //((TextBox)x).Text = theDR["Duration"].ToString();
                                    }
                                    if (x.ID == "drgFixQtyPrescribed")
                                    {
                                        int DecPos = theDR["OrderedQuantity"].ToString().IndexOf(".");
                                        //rupesh
                                        //int DecValue = Convert.ToInt32(theDR["OrderedQuantity"].ToString().Substring(DecPos + 1, 2));

                                        if (DecPos != -1)
                                        {
                                            decimal DecValue = Convert.ToDecimal(theDR["OrderedQuantity"].ToString().Substring(DecPos + 1, 2));
                                            if (DecValue > 0)
                                            {
                                                ((TextBox)x).Text = theDR["OrderedQuantity"].ToString();
                                            }
                                            else
                                            {
                                                ((TextBox)x).Text = Convert.ToString(Convert.ToInt32(theDR["OrderedQuantity"]));
                                            }
                                        }
                                        else
                                        {
                                            ((TextBox)x).Text = Convert.ToString(Convert.ToInt32(theDR["OrderedQuantity"]));
                                        }
                                        //((TextBox)x).Text = theDR["OrderedQuantity"].ToString();
                                    }
                                    if (x.ID == "drgFixQtyDispensed")
                                    {
                                        int DecPos = theDR["DispensedQuantity"].ToString().IndexOf(".");
                                        //int DecValue = Convert.ToInt32(theDR["DispensedQuantity"].ToString().Substring(DecPos + 1, 2));
                                        if (DecPos != -1)
                                        {
                                            decimal DecValue = Convert.ToDecimal(theDR["DispensedQuantity"].ToString().Substring(DecPos + 1, 2));
                                            if (theDR["DispensedQuantity"].ToString() != "0.00")
                                            {
                                                if (DecValue > 0)
                                                {
                                                    ((TextBox)x).Text = theDR["DispensedQuantity"].ToString();
                                                }
                                                else
                                                {
                                                    ((TextBox)x).Text = Convert.ToString(Convert.ToInt32(theDR["DispensedQuantity"]));
                                                }
                                            }
                                        }
                                        else
                                        {
                                            ((TextBox)x).Text = Convert.ToString(Convert.ToInt32(theDR["DispensedQuantity"]));
                                        }
                                        //((TextBox)x).Text = theDR["DispensedQuantity"].ToString();
                                    }
                                }

                                if (x.GetType() == typeof(CheckBox))
                                {
                                    if (x.ID.StartsWith("FinChk"))
                                    {
                                        if (Convert.ToInt32(theDR["Financed"].ToString()) == 1)
                                        {
                                            ((CheckBox)x).Checked = true;
                                        }
                                        else
                                        {
                                            ((CheckBox)x).Checked = false;
                                        }
                                    }
                                    if (x.ID == "chkFixProphylaxis")
                                    {
                                        if (Convert.ToInt32(theDR["Prophylaxis"].ToString()) == 1)
                                        {
                                            ((CheckBox)x).Checked = true;
                                        }
                                        else
                                        {
                                            ((CheckBox)x).Checked = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        //Generate a string builder for Insert Query Values
        //and Update Query Values
        /// <summary>
        /// Generates the custom fields values.
        /// </summary>
        /// <param name="Cntrl">The CNTRL.</param>
        private void GenerateCustomFieldsValues(Control Cntrl)
        {
            string pnlName = Cntrl.ID;
            sbValues = new StringBuilder();
            strmultiselect = string.Empty;
            string strfName = string.Empty;
            Boolean radioflag = false;

            Int32 stpos = 0;
            Int32 enpos = 0;
            if (ViewState["CustomFieldsData"] != null)
            {
                foreach (Control x in Cntrl.Controls)
                {
                    if (x.GetType() == typeof(TextBox))
                    {
                        if (x.ID.Substring(0, 16).ToString().ToUpper() == pnlName.ToUpper() + "TXT")
                        {
                            strfName = pnlName.ToUpper() + "TXT";
                            stpos = strfName.Length;
                            enpos = x.ID.Length - stpos;
                            strfName = x.ID.Substring(stpos, enpos).ToString();
                            if (((TextBox)x).Text != "")
                            {
                                sbValues.Append(",[" + strfName + "] = '" + ((TextBox)x).Text.ToString() + "'");
                            }
                            else
                            {
                                sbValues.Append(",[" + strfName + "] = ' " + "'");
                            }
                        }
                        else if (x.ID.Substring(0, 16).ToString().ToUpper() == pnlName.ToUpper() + "NUM")
                        {
                            strfName = pnlName.ToUpper() + "NUM";
                            stpos = strfName.Length;
                            enpos = x.ID.Length - stpos;
                            strfName = x.ID.Substring(stpos, enpos).ToString();
                            if (((TextBox)x).Text != "")
                            {
                                sbValues.Append(",[" + strfName + "]=" + ((TextBox)x).Text.ToString());
                            }
                            else
                            {
                                sbValues.Append("," + strfName + "=Null");
                            }
                        }
                        else if (x.ID.Substring(0, 15).ToString().ToUpper() == pnlName.ToUpper() + "DT")
                        {
                            strfName = pnlName.ToUpper() + "DT";
                            stpos = strfName.Length;
                            enpos = x.ID.Length - stpos;
                            strfName = x.ID.Substring(stpos, enpos).ToString();
                            if (((TextBox)x).Text != "")
                            {
                                sbValues.Append(",[" + strfName + "]='" + Convert.ToDateTime(((TextBox)x).Text.ToString()) + "'");
                            }
                            else
                            {
                                sbValues.Append(",[" + strfName + "]=" + "Null");
                            }
                        }
                    }
                    if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
                    {
                        if (x.ID.Substring(0, 19).ToString().ToUpper() == pnlName.ToUpper() + "RADIO1")
                        {
                            radioflag = false;
                            strfName = pnlName.ToUpper() + "RADIO1";
                            stpos = strfName.Length;
                            enpos = x.ID.Length - stpos;
                            strfName = x.ID.Substring(stpos, enpos).ToString();
                            if (((HtmlInputRadioButton)x).Checked == true)
                            {
                                sbValues.Append(",[" + strfName + "]=" + "1");
                                radioflag = true;
                            }
                        }
                        if (x.ID.Substring(0, 19).ToString().ToUpper() == pnlName.ToUpper() + "RADIO2")
                        {
                            strfName = pnlName.ToUpper() + "RADIO2";
                            stpos = strfName.Length;
                            enpos = x.ID.Length - stpos;
                            strfName = x.ID.Substring(stpos, enpos).ToString();
                            if (((HtmlInputRadioButton)x).Checked == true)
                            {
                                sbValues.Append(",[" + strfName + "]=" + "0");
                                radioflag = true;
                            }
                            if (radioflag == false)
                            {
                                sbValues.Append(",[" + strfName + "]=" + "Null");
                            }
                        }
                    }
                    if (x.GetType() == typeof(DropDownList))
                    {
                        if (x.ID.Substring(0, 23).ToString().ToUpper() == pnlName.ToUpper() + "SELECTLIST")
                        {
                            strfName = pnlName.ToUpper() + "SELECTLIST";
                            stpos = strfName.Length;
                            enpos = x.ID.Length - stpos;
                            strfName = x.ID.Substring(stpos, enpos).ToString();
                            if (((DropDownList)x).SelectedValue != "0")
                            {
                                sbValues.Append(",[" + strfName + "] = " + ((DropDownList)x).SelectedValue.ToString() + " ");
                            }
                            else
                            {
                                sbValues.Append(",[" + strfName + "] =  " + "0");
                            }
                        }
                    }
                }
            }

            if (ViewState["CustomFieldsData"] == null)
            {
                foreach (Control x in Cntrl.Controls)
                {
                    if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                    {
                        if (x.ID.Substring(0, 16).ToString().ToUpper() == pnlName.ToUpper() + "TXT")
                        {
                            if (((TextBox)x).Text != "")
                            {
                                sbValues.Append(",'" + ((TextBox)x).Text.ToString() + "'");
                            }
                            else
                            {
                                sbValues.Append(",' " + "'");
                            }
                        }
                        else if (x.ID.Substring(0, 16).ToString().ToUpper() == pnlName.ToUpper() + "NUM")
                        {
                            if (((TextBox)x).Text != "")
                            {
                                sbValues.Append("," + ((TextBox)x).Text.ToString());
                            }
                            else
                            {
                                sbValues.Append(",0");
                            }
                        }
                        else if (x.ID.Substring(0, 15).ToString().ToUpper() == pnlName.ToUpper() + "DT")
                        {
                            if (((TextBox)x).Text != "")
                            {
                                sbValues.Append(",'" + Convert.ToDateTime(((TextBox)x).Text.ToString()) + "'");
                            }
                            else
                            {
                                sbValues.Append(",Null");
                            }
                        }
                    }
                    if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
                    {
                        if (x.ID.Substring(0, 19).ToString().ToUpper() == pnlName.ToUpper() + "RADIO1")
                        {
                            radioflag = false;
                            if (((HtmlInputRadioButton)x).Checked == true)
                            {
                                sbValues.Append(",1");
                                radioflag = true;
                            }
                        }
                        if (x.ID.Substring(0, 19).ToString().ToUpper() == pnlName.ToUpper() + "RADIO2")
                        {
                            if (((HtmlInputRadioButton)x).Checked == true)
                            {
                                sbValues.Append(",0");
                                radioflag = true;
                            }
                            if (radioflag == false)
                            {
                                sbValues.Append(",Null");
                            }
                        }
                    }
                    if (x.GetType() == typeof(DropDownList))
                    {
                        if (x.ID.Substring(0, 23).ToString().ToUpper() == pnlName.ToUpper() + "SELECTLIST")
                        {
                            if (((DropDownList)x).SelectedValue != "0")
                            {
                                sbValues.Append("," + ((DropDownList)x).SelectedValue.ToString() + " ");
                            }
                            else
                            {
                                sbValues.Append(", " + "0");
                            }
                        }
                    }
                }
            }
            if (ViewState["CustomFieldsMulti"] != null || ViewState["CustomFieldsMulti"] == null)
            {
                foreach (Control x in Cntrl.Controls)
                {
                    if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBoxList))
                    {
                        if (x.ID.Substring(0, 28).ToString().ToUpper() == pnlName.ToUpper() + "MULTISELECTLIST")
                        {
                            foreach (ListItem li in ((CheckBoxList)x).Items)
                            {
                                if (Convert.ToInt32(li.Selected) == 1)
                                {
                                    strmultiselect += " " + li.Value.ToString() + ",";
                                }
                            }
                            strmultiselect += "^";
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the type of the patient last prescription by.
        /// </summary>
        /// <param name="drugTypeID">The drug type identifier.</param>
        /// <param name="cutOffDate">The cut off date.</param>
        private void GetPatientLastPrescriptionByType(int drugTypeID, DateTime cutOffDate)
        {
            int _patientID = Convert.ToInt32(Session["PatientId"]);
            bool _showMostRecent = true;
            IPediatric PediatricManager = (IPediatric)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BPediatric, BusinessProcess.Pharmacy");
            try
            {
                DataTable dtPharmOrders = PediatricManager.GetPatientPharmacyOrders(_patientID, cutOffDate, _showMostRecent, drugTypeID);
                if (dtPharmOrders.Rows.Count == 0) return;

                int _lastPharmacyID = Convert.ToInt32(dtPharmOrders.Rows[0]["ptn_pharmacy_pk"]);

                DataSet _lastDataSet = PediatricManager.GetExistPaediatricDetails(_lastPharmacyID);
                if (_lastDataSet.Tables.Count == 0) return;
                base.Session["ExistPharmacyData"] = _lastDataSet.Tables[0];

                foreach (DataRow theDR in _lastDataSet.Tables[0].Rows)
                {
                    if (theDR["PrintPrescriptionStatus"].ToString() == "1")
                    {
                        Session["PrintStatus"] = 1;
                    }
                }
                //pr_Pharmacy_GetPediatricDetails_Constella
                DataSet theDrugDS = this.PopulateTheDS(_patientID);
                /*
                 * DataSet theDrugDS = PediatricManager.GetPediatricFields(_patientID);
                #region "FixDoseCombination"
                theDS = new DataSet();
                theDS.Tables.Add(theDrugDS.Tables[17].Copy());//--0--performance - gen abbr & only active drugs
                theDS.Tables.Add(theDrugDS.Tables[1].Copy());//--1--
                theDS.Tables.Add(theDrugDS.Tables[2].Copy());//--2--
                theDS.Tables.Add(theDrugDS.Tables[3].Copy());//--3--
                theDS.Tables.Add(theDrugDS.Tables[4].Copy());//--4--
                theDS.Tables.Add(theDrugDS.Tables[5].Copy());//--5--
                theDS.Tables.Add(theDrugDS.Tables[6].Copy());//--6--
                theDS.Tables.Add(theDrugDS.Tables[15].Copy());//--7-- for inactive units in case of edit;
                theDS.Tables.Add(theDrugDS.Tables[8].Copy());//--8--
                theDS.Tables.Add(theDrugDS.Tables[9].Copy());//--9--
                theDS.Tables.Add(theDrugDS.Tables[10].Copy());//--10--
                theDS.Tables.Add(theDrugDS.Tables[11].Copy());//--11--
                theDS.Tables.Add(theDrugDS.Tables[12].Copy());//--12-- stores all (both active/inactive) drugs
                theDS.Tables.Add(theDrugDS.Tables[13].Copy());//--13--  rupesh 04-sep for OI and other medication - for custom frq list
                //theDS.Tables.Add(theDrugDS.Tables[14]);//  rupesh 19-sep-07 for ARV Provider
                theDS.Tables.Add(theDrugDS.Tables[16].Copy());//--14--  rupesh 19-sep-07 for active/inactive ARV Provider
                theDS.Tables.Add(theDrugDS.Tables[21].Copy());//--15--  29Feb08 -- Non-ARTDate
                theDS.Tables.Add(theDrugDS.Tables[22].Copy());//-period taken
                theDS.Tables.Add(theDrugDS.Tables[23].Copy());//-TB Regimen
                theDS.Tables.Add(theDrugDS.Tables[24].Copy());
                theDS.Tables.Add(theDrugDS.Tables[25].Copy());
                theDS.Tables.Add(theDrugDS.Tables[26].Copy());
                theDS.Tables.Add(theDrugDS.Tables[27].Copy());
                theDS.Tables.Add(theDrugDS.Tables[28].Copy());
                theDS.Tables.Add(theDrugDS.Tables[29].Copy());
                #endregion "FixDoseCombination"
                */
                //---rupesh -- for fixed drug strength / frequency -----
                Session["FixDrugStrength"] = theDrugDS.Tables[18];
                Session["FixDrugFreq"] = theDrugDS.Tables[19];
                //---------------------------------------------------------------
                if ((_lastDataSet.Tables[0].Rows[0]["Weight"] != System.DBNull.Value) || (_lastDataSet.Tables[0].Rows[0]["Height"] != System.DBNull.Value))
                {
                    decimal theWeight = Convert.ToDecimal(_lastDataSet.Tables[0].Rows[0]["Weight"].ToString());
                    if (theWeight > 0)
                        txtWeight.Text = Convert.ToString(theWeight);
                    decimal theHeight = Convert.ToDecimal(_lastDataSet.Tables[0].Rows[0]["Height"].ToString());
                    if (theHeight > 0)
                        txtHeight.Text = Convert.ToString(theHeight);
                    decimal theBSA = theWeight * theHeight / 3600;
                    theBSA = (decimal)Math.Sqrt(Convert.ToDouble(theBSA));
                    theBSA = Math.Round(theBSA, 2);
                    txtBSA.Text = Convert.ToString(theBSA);
                }
                pnlPedia.Controls.Clear();

                //DateTime theDOBirth = (DateTime)theDS.Tables[6].Rows[0]["DOB"];
                //txtDOB.Text = theDOBirth.ToString(Session["AppDateFormat"].ToString());
                //txtYr.Text = theDS.Tables[6].Rows[0]["Age"].ToString();
                //txtMon.Text = theDS.Tables[6].Rows[0]["Age1"].ToString();

                DataTable dtPatientInfo = (DataTable)Session["PatientInformation"];
                DateTime theDOB = (DateTime)dtPatientInfo.Rows[0]["DOB"];// theDS.Tables[6].Rows[0]["DOB"];
                txtDOB.Text = theDOB.ToString(Session["AppDateFormat"].ToString());
                txtYr.Text = dtPatientInfo.Rows[0]["Age"].ToString();// theDS.Tables[6].Rows[0]["Age"].ToString();
                txtMon.Text = dtPatientInfo.Rows[0]["Age1"].ToString(); //th

                if (_lastDataSet.Tables[1].Rows.Count > 0)
                {
                    /*  DateTime theAppntmentDate = Convert.ToDateTime(_lastDataSet.Tables[1].Rows[0]["AppDate"].ToString());
                      txtpharmAppntDate.Value = theAppntmentDate.ToString(Session["AppDateFormat"].ToString());
                      if (Convert.ToInt32(_lastDataSet.Tables[1].Rows[0]["AppReason"]) > 0)
                      {
                          ddlAppntReason.SelectedValue = _lastDataSet.Tables[1].Rows[0]["AppReason"].ToString();
                      }*/
                }
                if (_lastDataSet.Tables[0].Rows[0]["RegimenLine"] != System.DBNull.Value)
                {
                    ddlregimenLine.SelectedValue = _lastDataSet.Tables[0].Rows[0]["RegimenLine"].ToString();
                }

                BindddlControls(ref mainDataSet);
                MakeRegimenGenericTable(mainDataSet);
                BindInfantHealthSection(mainDataSet);
                ViewState.Remove("SelectedDrug");
                CreateControls(mainDataSet);

                if (_lastDataSet.Tables[0].Rows[0]["PharmacyPeriodTaken"].ToString() != "")
                {
                    ddlPeriodTaken.SelectedValue = _lastDataSet.Tables[0].Rows[0]["PharmacyPeriodTaken"].ToString();
                }

                ddlTreatment.SelectedValue = _lastDataSet.Tables[0].Rows[0]["ProgID"].ToString();
                if (_lastDataSet.Tables[0].Rows[0]["ProgID"].ToString() == "225")
                {
                    Session["TreatmentProg"] = _lastDataSet.Tables[0].Rows[0]["ProgID"].ToString();
                }
                else
                {
                    Session["TreatmentProg"] = "";
                }
                //rupesh 19-sep-07 for ARV Provider
                ddlProvider.SelectedValue = Convert.ToString(_lastDataSet.Tables[0].Rows[0]["ProviderID"].ToString());

                #region "CreateAdditional Controls"

                #region "TableCreation"
                DataTable theDT1 = new DataTable();
                theDT1.Columns.Add("DrugId", Type.GetType("System.Int32"));
                theDT1.Columns.Add("DrugName", Type.GetType("System.String"));
                theDT1.Columns.Add("Generic", Type.GetType("System.Int32"));
                theDT1.Columns.Add("DrugTypeId", Type.GetType("System.Int32"));
                AddARV = theDT1.Copy();
                OtherDrugs = theDT1.Copy();
                TBDrugs = theDT1.Copy();
                OIDrugs = theDT1.Copy();
                //NonARVDrugs = theDT1.Copy();
                #endregion "TableCreation"

                foreach (DataRow theDR in _lastDataSet.Tables[0].Rows)
                {
                    if (theDR["Drug_Pk"] != System.DBNull.Value)
                    {
                        DataView theDV = new DataView(mainDataSet.Tables[12]);//rupesh for showing inactive drug
                        theDV.RowFilter = "Drug_Pk = " + theDR["Drug_Pk"].ToString();
                        if (theDV.Count > 0)
                        {
                            if (Convert.ToInt32(theDV[0]["DrugTypeId"]) == 37)
                            {
                                DataRow DR = AddARV.NewRow();
                                DR[0] = theDR["Drug_Pk"];
                                DR[1] = theDV[0]["DrugName"];
                                DR[2] = 0;
                                AddARV.Rows.Add(DR);
                                if (trHIVsetFields.Visible == false)
                                {
                                    trHIVsetFields.Visible = true;
                                }
                            }
                            else if (Convert.ToInt32(theDV[0]["DrugTypeId"]) == 31)
                            {
                                DataRow DR = TBDrugs.NewRow();
                                DR[0] = theDR["Drug_Pk"];
                                DR[1] = theDV[0]["DrugName"];
                                DR[2] = 0;
                                TBDrugs.Rows.Add(DR);
                            }
                            else if (Convert.ToInt32(theDV[0]["DrugTypeId"]) == 36)
                            {
                                DataRow DR = OIDrugs.NewRow();
                                DR[0] = theDR["Drug_Pk"];
                                DR[1] = theDV[0]["DrugName"];
                                DR[2] = 0;
                                OIDrugs.Rows.Add(DR);
                            }
                            else
                            {
                                DataRow DR = OtherDrugs.NewRow();
                                DR[0] = theDR["Drug_Pk"];
                                DR[1] = theDV[0]["DrugName"];
                                DR[2] = 0;
                                OtherDrugs.Rows.Add(DR);
                            }
                        }
                    }
                }

                LoadAdditionalDrugs(AddARV, pnlPedia);
                LoadAdditionalDrugs(OtherDrugs, PnlOtMed);
                LoadAdditionalDrugs(TBDrugs, pnlOtherTBMedicaton);
                LoadAdditionalDrugs(OIDrugs, PnlOIARV);

                #endregion "CreateAdditional Controls"

                foreach (DataRow dr in _lastDataSet.Tables[0].Rows)
                {
                    FillOldData(pnlPedia, dr, true);
                    FillOldData(PnlOIARV, dr, true);
                    //FillOldData(PnlRegiment, dr,true);
                    FillOldData(PnlOtMed, dr, false);
                    FillOldData(pnlOtherTBMedicaton, dr, false);
                }
                try
                {
                    if (HttpContext.Current.Session["Paperless"].ToString() == "1" && HttpContext.Current.Session["SCMModule"] != null)
                    {
                        var dispenseqtyTextBox = Page.GetAllControlsOfType<TextBox>().Where(t => t.ID.StartsWith("drgQtyDispensed"));
                        dispenseqtyTextBox.ToList<TextBox>().ForEach(t =>
                        {
                            t.Text = "";
                        });
                    }
                }
                catch { }

                this.theExistDS = _lastDataSet;
            }
            catch (Exception er)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = er.Message.ToString();
                IQCareMsgBox.Show("C1#", theBuilder, this);
            }
        }

        /// <summary>
        /// Init_s the form.
        /// </summary>
        private void Init_Form()
        {
            try
            {
                txtYr.Attributes.Add("readonly", "true");
                txtMon.Attributes.Add("readonly", "true");
                txtDOB.Attributes.Add("readonly", "true");
                int patientId = Convert.ToInt32(Session["PatientId"]);

                if (theExistDS.Tables.Count > 0)
                    ddlTreatment.SelectedValue = theExistDS.Tables[0].Rows[0]["ProgId"].ToString();

                this.OrderId = Convert.ToInt32(Session["PatientVisitId"]);
                if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
                {
                    ViewState["PtnID"] = Convert.ToInt32(Session["PatientId"]);
                    GetPediatricFields(patientId);
                }
                else
                {
                    GetExistPediatricFields();
                }
            }
            catch (Exception er)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = er.Message.ToString();
                IQCareMsgBox.Show("C1#", theBuilder, this);
            }
        }

        //Amitava Sinha
        //Generating full DML Statement
        /// <summary>
        /// Inserts the custom fields values.
        /// </summary>
        private void InsertCustomFieldsValues()
        {
            GenerateCustomFieldsValues(pnlCustomList);
            string sqlstr = string.Empty;
            //string sqlselect;
            Int32 PharmacyId = 0;
            DateTime OrderedbyDate = System.DateTime.Now;
            Int32 PatientId = Convert.ToInt32(Session["PatientId"]);
            ICustomFields CustomFields;

            if (ViewState["PharmacyId"] != null)
                PharmacyId = Convert.ToInt32(ViewState["PharmacyId"]);
            if (txtpharmOrderedbyDate.Value.ToString() != "")
                OrderedbyDate = Convert.ToDateTime(txtpharmOrderedbyDate.Value.ToString());

            if (sbValues.ToString().Trim() != "")
            {
                //Rupesh
                //sqlstr = "INSERT INTO dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "(ptn_pk,LocationID,ptn_pharmacy_pk,OrderedbyDate " + sbParameter.ToString() + " )";
                sqlstr = "INSERT INTO dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "(ptn_pk,LocationID,ptn_pharmacy_pk,OrderedByDate " + sbParameter.ToString() + " )";
                //sqlstr += " VALUES(" + PatientId.ToString() + "," + Application["AppLocationID"] + "," + PharmacyId + ",'" + OrderedbyDate + "'" + sbValues.ToString() + ")";
                sqlstr += " VALUES(" + PatientId.ToString() + "," + Session["AppLocationId"].ToString() + "," + PharmacyId + ",'" + OrderedbyDate + "'" + sbValues.ToString() + ")";

                try
                {
                    CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
                    icount = CustomFields.SaveCustomFieldValues(sqlstr.ToString());
                    if (icount == -1)
                    {
                        return;
                    }
                }
                catch
                {
                }
                finally
                {
                    CustomFields = null;
                }
            }
            if (strmultiselect.ToString() != "")
            {
                string[] FieldValues = strmultiselect.Split(new char[] { '^' });
            }
        }

        /// <summary>
        /// Loads the additional drugs.
        /// </summary>
        /// <param name="theDT">The dt.</param>
        /// <param name="thePanel">The panel.</param>
        private void LoadAdditionalDrugs(DataTable theDT, Panel thePanel)
        {
            //thePanel.Controls.Clear();
            if (theDT.Rows.Count > 0)
            {
                if (thePanel.ID == "pnlPedia")
                {
                    pnlARV.Visible = true;
                }
                else if (thePanel.ID == "pnlOtherTBMedicaton")
                {
                    pnlTB.Visible = true;
                }
                else if (thePanel.ID == "PnlOIARV")
                {
                    pnlOI.Visible = true;
                }
                else if (thePanel.ID == "PnlOtMed")
                {
                    pnlOther.Visible = true;
                }
                else if (thePanel.ID == "panelVaccine")
                {
                    divVaccine.Visible = true;
                }
            }
            foreach (DataRow theDR in theDT.Rows)
            {
                //if (thePanel.ID == "PnlAddARV")
                if (thePanel.ID == "pnlPedia")
                {
                    BindCustomControls(Convert.ToInt32(theDR[0]), Convert.ToInt32(theDR[2]), thePanel);
                }
                else if (thePanel.ID == "panelVaccine")
                {
                    BindVaccineControls(Convert.ToInt32(theDR[0]), Convert.ToInt32(theDR[2]), thePanel);
                }
                else if (thePanel.ID == "pnlOtherTBMedicaton")
                {
                    BindTBDrugControls(Convert.ToInt32(theDR[0]), Convert.ToInt32(theDR[2]), thePanel);
                }
                else
                {
                    BindAdditionalDrugControls(Convert.ToInt32(theDR[0]), Convert.ToInt32(theDR[2]), thePanel);
                }
            }
        }

        /// <summary>
        /// Makes the drug table.
        /// </summary>
        /// <param name="theContainer">The container.</param>
        /// <returns></returns>
        private DataTable MakeDrugTable(Control theContainer)
        {
            int c = 0;//c=total length of id
            DataTable theDT = new DataTable();

            if (ViewState["Data"] == null)
            {
                theDT = CreateTable();
            }
            else
            {
                theDT = (DataTable)ViewState["Data"];
            }

            #region "Variables"
            decimal Dose = 0;
            int UnitId = 0;
            int theStrengthId = 0;

            int theFrequencyId = 0;
            Decimal theDuration = 0;
            Decimal theQtyPrescribed = 0;
            Decimal theQtyDispensed = 0;
            Decimal theQtyPrescribed1 = 0;
            Decimal theQtyDispensed1 = 0;
            string theTreatmentPhase = "";
            int theMonth = 0;
            string strVaccineSchedule = "";
            int theProphylaxis = 0;
            int printPrescriptionStatus = 999;
            string patientInstructions = "999";
            if (ddlTreatment.SelectedItem.Value.ToString() == "223" || ddlTreatment.SelectedItem.Value.ToString() == "224" || ddlTreatment.SelectedItem.Value.ToString() == "225")
            {
                theProphylaxis = 999;
            }
            int theFinanced = 99;

            #endregion "Variables"

            if (theContainer.ID == "PnlOIARV") //--ARV
            {
                #region "ARV and OI"
                //pnl 1 - id=PnlDrug - no btn  AND pnl 3 - id =PnlOIARV - no btn
                //DataTable theARVDrug = (DataTable)Session["SelectedDrug"];
                if (Session["OIDrugs"] != null)
                {
                    DataTable theARVDrug = (DataTable)Session["OIDrugs"];
                    #region "18-Jun-07 - 1"
                    int TotColFilled = 0; // rupesh
                    #endregion "18-Jun-07 - 1"
                    foreach (DataRow theDR in theARVDrug.Rows)
                    {
                        Dose = 0;
                        UnitId = 0;
                        theStrengthId = 0;

                        theFrequencyId = 0;
                        theDuration = 0;
                        theQtyPrescribed = 0;
                        theQtyDispensed = 0;
                        theQtyPrescribed1 = 0;
                        theQtyDispensed1 = 0;
                        theTreatmentPhase = "";
                        DataRow theRow;
                        DataRow[] DRStrength = ((DataSet)ViewState["MasterData"]).Tables[1].Select("GenericId=" + Convert.ToInt32(theDR["DrugId"]));
                        if (DRStrength[0]["StrengthId"] != System.DBNull.Value)
                            theStrengthId = Convert.ToInt32(DRStrength[0]["StrengthId"]);
                        foreach (Control y in theContainer.Controls)
                        {
                            if (y.GetType() == typeof(Panel))
                            {
                                //
                                foreach (Control x in y.Controls)
                                {
                                    if (x.GetType() == typeof(Panel))
                                    {
                                        MakeDrugTable(x);
                                    }
                                    else
                                    {
                                        if (x.GetType() == typeof(DropDownList))
                                        {
                                            if (x.ID != null)
                                            {
                                                if (x.ID.EndsWith(theDR["DrugId"].ToString() + "^0") && x.ID.StartsWith("drgFrequency"))
                                                {
                                                    theFrequencyId = Convert.ToInt32(((DropDownList)x).SelectedValue);
                                                    #region "18-Jun-07 - 5"
                                                    if (theFrequencyId != 0)
                                                        TotColFilled++;
                                                    #endregion "18-Jun-07 - 5"
                                                }
                                            }
                                        }
                                        if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                                        {
                                            if (x.ID.EndsWith(theDR["DrugId"].ToString() + "^" + theDR["GenericId"].ToString()) && x.ID.StartsWith("drgDose"))
                                            {
                                                if (((TextBox)x).Text != "")
                                                {
                                                    Dose = Convert.ToDecimal(((TextBox)x).Text);
                                                    #region "18-Jun-07 - 9"
                                                    if (Dose != 0)
                                                        TotColFilled++;
                                                    #endregion "18-Jun-07 - 9"
                                                }
                                            }
                                            if (x.ID.EndsWith(theDR["DrugId"].ToString() + "^" + theDR["GenericId"].ToString()) && x.ID.StartsWith("drgDuration"))
                                            {
                                                if (((TextBox)x).Text != "")
                                                {
                                                    theDuration = Convert.ToDecimal(((TextBox)x).Text);
                                                    #region "18-Jun-07 - 9"
                                                    if (theDuration != 0)
                                                        TotColFilled++;
                                                    #endregion "18-Jun-07 - 9"
                                                }
                                            }
                                            if (x.ID.EndsWith(theDR["DrugId"].ToString() + "^" + theDR["GenericId"].ToString()) && x.ID.StartsWith("drgQtyPrescribed"))
                                            {
                                                if (((TextBox)x).Text != "")
                                                {
                                                    theQtyPrescribed = Convert.ToDecimal(((TextBox)x).Text);
                                                    #region "18-Jun-07 - 10"
                                                    if (theQtyPrescribed != 0)
                                                        TotColFilled++;
                                                    #endregion "18-Jun-07 - 10"
                                                }
                                            }

                                            if (x.ID.EndsWith(theDR["DrugId"].ToString() + "^" + theDR["GenericId"].ToString()) && x.ID.StartsWith("drgQtyDispensed"))
                                            {
                                                if (Session["Paperless"].ToString() == "1")
                                                {
                                                    if (TotColFilled > 3)
                                                    {
                                                        if (((TextBox)x).Text == "" || ((TextBox)x).Text == "0")
                                                        {
                                                            theQtyDispensed = 99;
                                                            #region "18-Jun-07 - 8"
                                                            if (theQtyDispensed != 0)
                                                                TotColFilled++;
                                                            #endregion "18-Jun-07 - 8"
                                                        }
                                                        else
                                                        {
                                                            if (((TextBox)x).Text != "")
                                                            {
                                                                theQtyDispensed = Convert.ToDecimal(((TextBox)x).Text);
                                                                #region "18-Jun-07 - 8"
                                                                if (theQtyDispensed != 0)
                                                                    TotColFilled++;
                                                                #endregion "18-Jun-07 - 8"
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (((TextBox)x).Text != "")
                                                    {
                                                        theQtyDispensed = Convert.ToDecimal(((TextBox)x).Text);
                                                        #region "18-Jun-07 - 8"
                                                        if (theQtyDispensed != 0)
                                                            TotColFilled++;
                                                        #endregion "18-Jun-07 - 8"
                                                    }
                                                }
                                            }

                                            //patient instructions
                                            if (x.ID.StartsWith("txtPtnInstructions"))
                                            {
                                                c = x.ID.Length;
                                                if (x.ID.EndsWith(theDR["DrugId"].ToString()) && x.ID.StartsWith("txtPtnInstructions"))
                                                {
                                                    if (((TextBox)x).Text != "")
                                                    {
                                                        patientInstructions = Convert.ToString(((TextBox)x).Text);
                                                        //TotColFilled++;
                                                    }
                                                    else
                                                    {
                                                        patientInstructions = "";
                                                    }
                                                }
                                            }

                                            //}
                                        }
                                        //Prophylaxis
                                        if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                                        {
                                            //Prophylaxis
                                            if (x.ID.StartsWith("chkProphylaxis"))
                                            {
                                                c = x.ID.Length;
                                                if (x.ID.EndsWith(theDR["DrugId"].ToString() + "^" + theDR["GenericId"].ToString()) && x.ID.StartsWith("chkProphylaxis"))
                                                {
                                                    if (((CheckBox)x).Checked == true)
                                                    {
                                                        theProphylaxis = 1;
                                                        TotColFilled++;
                                                    }
                                                }
                                            }

                                            //print prescription
                                            if (x.ID.StartsWith("chkPrintPrescription"))
                                            {
                                                c = x.ID.Length;
                                                if (x.ID.EndsWith(theDR["DrugId"].ToString()) && x.ID.StartsWith("chkPrintPrescription"))
                                                {
                                                    if (((CheckBox)x).Checked == true)
                                                    {
                                                        printPrescriptionStatus = 1;
                                                        //TotColFilled++;
                                                    }
                                                    else
                                                    {
                                                        printPrescriptionStatus = 0;
                                                        //TotColFilled++;
                                                    }
                                                }
                                            }

                                            //}
                                        }
                                    }

                                    //if (theContainer.ID == "PnlOIARV")
                                    //{
                                    //paper less
                                    if (Session["Paperless"].ToString() == "1")
                                    {
                                        if (theStrengthId != 0 && Dose != 0 && theFrequencyId != 0 && theDuration != 0 && theQtyPrescribed != 0 && theQtyDispensed != 0 && theProphylaxis != 999 && printPrescriptionStatus != 999 && patientInstructions != "999")
                                        {
                                            theRow = theDT.NewRow();
                                            theRow["DrugId"] = theDR["DrugId"];
                                            theRow["GenericId"] = 0;

                                            theRow["Dose"] = Dose;
                                            theRow["UnitId"] = 0;
                                            theRow["StrengthId"] = theStrengthId;
                                            theRow["FrequencyId"] = theFrequencyId;
                                            theRow["Duration"] = theDuration;
                                            theRow["QtyPrescribed"] = theQtyPrescribed;
                                            if (theQtyDispensed == 99)
                                            {
                                                theRow["QtyDispensed"] = 0;
                                            }
                                            else
                                            {
                                                theRow["QtyDispensed"] = theQtyDispensed;
                                            }

                                            theRow["Financed"] = 1;
                                            theRow["Prophylaxis"] = theProphylaxis;
                                            theRow["PrintPrescriptionStatus"] = printPrescriptionStatus;
                                            theRow["PatientInstructions"] = patientInstructions;
                                            theDT.Rows.Add(theRow);
                                            #region "Reset Variables
                                            Dose = 0;
                                            UnitId = 0;
                                            theStrengthId = 0;
                                            theFrequencyId = 0;
                                            theDuration = 0;
                                            theQtyPrescribed = 0;
                                            theQtyDispensed = 0;
                                            theFinanced = 99;
                                            if (ddlTreatment.SelectedItem.Value.ToString() == "223" || ddlTreatment.SelectedItem.Value.ToString() == "224" || ddlTreatment.SelectedItem.Value.ToString() == "225")
                                            {
                                                theProphylaxis = 999;
                                            }
                                            else
                                            {
                                                theProphylaxis = 0;
                                            }
                                            printPrescriptionStatus = 999;
                                            patientInstructions = "999";
                                            #endregion "Reset Variables
                                        }
                                    }
                                    else
                                    {
                                        if (theStrengthId != 0 && Dose != 0 && theFrequencyId != 0 && theDuration != 0 && theQtyPrescribed != 0 && theQtyDispensed != 0 && theProphylaxis != 999 && printPrescriptionStatus != 999 && patientInstructions != "999")
                                        {
                                            theRow = theDT.NewRow();

                                            theRow["DrugId"] = theDR["DrugId"];
                                            theRow["GenericId"] = 0;

                                            theRow["Dose"] = Dose;
                                            theRow["UnitId"] = 0;
                                            theRow["StrengthId"] = theStrengthId;
                                            theRow["FrequencyId"] = theFrequencyId;
                                            theRow["Duration"] = theDuration;
                                            theRow["QtyPrescribed"] = theQtyPrescribed;

                                            theRow["QtyDispensed"] = theQtyDispensed;

                                            theRow["Financed"] = 1;
                                            theRow["Prophylaxis"] = theProphylaxis;
                                            theRow["PrintPrescriptionStatus"] = printPrescriptionStatus;
                                            theRow["PatientInstructions"] = patientInstructions;
                                            theDT.Rows.Add(theRow);
                                            #region "Reset Variables
                                            Dose = 0;
                                            UnitId = 0;
                                            theStrengthId = 0;
                                            theFrequencyId = 0;
                                            theDuration = 0;
                                            theQtyPrescribed = 0;
                                            theQtyDispensed = 0;
                                            theFinanced = 99;
                                            if (ddlTreatment.SelectedItem.Value.ToString() == "223" || ddlTreatment.SelectedItem.Value.ToString() == "224" || ddlTreatment.SelectedItem.Value.ToString() == "225")
                                            {
                                                theProphylaxis = 999;
                                            }
                                            else
                                            {
                                                theProphylaxis = 0;
                                            }
                                            printPrescriptionStatus = 999;
                                            patientInstructions = "999";
                                            #endregion "Reset Variables
                                        }
                                    }
                                    //}
                                }
                                #region "18-Jun-07 - 12"
                                if (Session["Paperless"].ToString() != "1")
                                {
                                    int ChkColFilled;
                                    if (ddlTreatment.SelectedItem.Value.ToString() == "223")
                                    {
                                        if (Session["SCMModule"] != null)
                                            ChkColFilled = 3;
                                        else
                                            ChkColFilled = 4;
                                        if ((TotColFilled > 0 && TotColFilled < ChkColFilled) && (theContainer.ID == "PnlOIARV"))
                                        {
                                            theDT.Rows.Clear();
                                            theRow = theDT.NewRow();
                                            theRow[0] = 99999;
                                            theDT.Rows.Add(theRow);
                                            return theDT;
                                        }
                                        else
                                            TotColFilled = 0;
                                    }
                                    else
                                    {
                                        if ((TotColFilled > 0 && TotColFilled < 2) && (theContainer.ID == "PnlOIARV"))
                                        {
                                            theDT.Rows.Clear();
                                            theRow = theDT.NewRow();
                                            theRow[0] = 99999;
                                            theDT.Rows.Add(theRow);
                                            return theDT;
                                        }
                                        else
                                            TotColFilled = 0;
                                    }
                                }

                                #endregion "18-Jun-07 - 12"
                            }
                        }
                    }
                }
                #endregion "ARV and OI"
            }
            else if (theContainer.ID == "pnlPedia")
            {
                #region "Additional ARV"
                //pnl 2 -id= PnlAddARV  btn-txt = "Other ARV Medications"
                if (Session["AddARV"] != null)
                {
                    DataTable theADDARVDrug = (DataTable)Session["AddARV"];
                    int TotelColFilled = 0;
                    int DrugID = 0;
                    if (theADDARVDrug == null)
                        return theDT;
                    foreach (DataRow theDR in theADDARVDrug.Rows)
                    {
                        Dose = 0;
                        theStrengthId = 0;
                        theFrequencyId = 0;
                        theDuration = 0;
                        theQtyPrescribed = 0;
                        theQtyDispensed = 0;
                        //theFinanced = 99;
                        DataRow[] DRStrength = ((DataSet)ViewState["MasterData"]).Tables[1].Select("GenericId=" + Convert.ToInt32(theDR["DrugId"]));
                        if (DRStrength[0]["StrengthId"] != System.DBNull.Value)
                            theStrengthId = Convert.ToInt32(DRStrength[0]["StrengthId"]);
                        DataRow theRow;
                        foreach (Control y in theContainer.Controls)
                        {
                            if (y.GetType() == typeof(Panel))
                            {
                                foreach (Control x in y.Controls)
                                {
                                    if (x.GetType() == typeof(Panel))
                                    {
                                        MakeDrugTable(x);
                                    }
                                    else
                                    {
                                        DrugID = Convert.ToInt32(theDR["DrugId"]);
                                        if (x.GetType() == typeof(DropDownList))
                                        {
                                            c = x.ID.Length;

                                            if (x.ID.StartsWith("drgFrequency"))
                                            {
                                                if (x.ID.Substring(12, c - 12) == DrugID.ToString() && x.ID.StartsWith("drgFrequency"))
                                                {
                                                    theFrequencyId = Convert.ToInt32(((DropDownList)x).SelectedValue);
                                                    TotelColFilled++;
                                                }
                                            }
                                        }
                                        if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                                        {
                                            if (x.ID.EndsWith(theDR["DrugId"].ToString() + "^0") && x.ID.StartsWith("drgDose"))
                                            {
                                                if (((TextBox)x).Text != "")
                                                {
                                                    Dose = Convert.ToDecimal(((TextBox)x).Text);
                                                    #region "18-Jun-07 - 9"
                                                    if (Dose != 0)
                                                        TotelColFilled++;
                                                    #endregion "18-Jun-07 - 9"
                                                }
                                            }
                                            if (x.ID.StartsWith("drgDuration"))
                                            {
                                                c = x.ID.Length;
                                                if (x.ID.Substring(11, c - 11) == DrugID.ToString() && x.ID.StartsWith("drgDuration"))
                                                {
                                                    if (((TextBox)x).Text != "")
                                                    {
                                                        theDuration = Convert.ToDecimal(((TextBox)x).Text);
                                                        TotelColFilled++;
                                                    }
                                                }
                                            }
                                            //if (x.ID.EndsWith(theDR["DrugId"].ToString()) && x.ID.StartsWith("drgQtyPrescribed"))
                                            if (x.ID.StartsWith("drgQtyPrescribed"))
                                            {
                                                c = x.ID.Length;
                                                if (x.ID.Substring(16, c - 16) == DrugID.ToString() && x.ID.StartsWith("drgQtyPrescribed"))
                                                {
                                                    if (((TextBox)x).Text != "")
                                                    {
                                                        theQtyPrescribed = Convert.ToDecimal(((TextBox)x).Text);
                                                        TotelColFilled++;
                                                    }
                                                }
                                            }
                                            //if (x.ID.EndsWith(theDR["DrugId"].ToString()) && x.ID.StartsWith("drgQtyDispensed"))
                                            if (x.ID.StartsWith("drgQtyDispensed"))
                                            {
                                                c = x.ID.Length;
                                                if (x.ID.Substring(15, c - 15) == DrugID.ToString() && x.ID.StartsWith("drgQtyDispensed"))
                                                {
                                                    //for paperless
                                                    if (Session["Paperless"].ToString() == "1")
                                                    {
                                                        if (((TextBox)x).Text == "")
                                                        {
                                                            theQtyDispensed = 99;
                                                            //TotelColFilled++;
                                                        }
                                                        else
                                                        {
                                                            if (((TextBox)x).Text != "")
                                                            {
                                                                theQtyDispensed = Convert.ToDecimal(((TextBox)x).Text);
                                                                TotelColFilled++;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (((TextBox)x).Text != "")
                                                        {
                                                            theQtyDispensed = Convert.ToDecimal(((TextBox)x).Text);
                                                            TotelColFilled++;
                                                        }
                                                    }
                                                }
                                            }

                                            //patient instructions
                                            if (x.ID.StartsWith("txtPtnInstructions"))
                                            {
                                                c = x.ID.Length;
                                                if (x.ID.EndsWith(DrugID.ToString()) && x.ID.StartsWith("txtPtnInstructions"))
                                                {
                                                    if (((TextBox)x).Text != "")
                                                    {
                                                        patientInstructions = Convert.ToString(((TextBox)x).Text);
                                                        //TotelColFilled++;
                                                    }
                                                    else
                                                    {
                                                        patientInstructions = "";
                                                    }
                                                }
                                            }
                                        }

                                        if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                                        {
                                            if (x.ID.StartsWith("chkProphylaxis"))
                                            {
                                                c = x.ID.Length;
                                                if (x.ID.EndsWith(DrugID.ToString()) && x.ID.StartsWith("chkProphylaxis"))
                                                {
                                                    if (((CheckBox)x).Checked == true)
                                                    {
                                                        theProphylaxis = 1;
                                                        TotelColFilled++;
                                                    }
                                                    else
                                                    {
                                                        if (ddlTreatment.SelectedItem.Value.ToString() == "224" || ddlTreatment.SelectedItem.Value.ToString() == "225")
                                                        {
                                                            theProphylaxis = 0;
                                                            TotelColFilled++;
                                                        }
                                                    }
                                                }
                                            }

                                            //print prescription
                                            if (x.ID.StartsWith("chkPrintPrescription"))
                                            {
                                                c = x.ID.Length;
                                                if (x.ID.EndsWith(DrugID.ToString()) && x.ID.StartsWith("chkPrintPrescription"))
                                                {
                                                    if (((CheckBox)x).Checked == true)
                                                    {
                                                        printPrescriptionStatus = 1;
                                                        //TotelColFilled++;
                                                    }
                                                    else
                                                    {
                                                        printPrescriptionStatus = 0;
                                                        //TotelColFilled++;
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    if (Session["Paperless"].ToString() == "1")
                                    {
                                        //if (theStrengthId != 0 && theFrequencyId != 0 && theDuration != 0 && theQtyPrescribed != 0 && theQtyDispensed != 0 && theFinanced != 99 && theProphylaxis != 999)
                                        //if (theStrengthId != 0 && theFrequencyId != 0 && theDuration != 0 && theQtyPrescribed != 0 && theQtyDispensed != 0 && theProphylaxis != 999 && printPrescriptionStatus != 999 && patientInstructions != "999")
                                        if (theStrengthId != 0 && theFrequencyId != 0 && theDuration != 0 && theQtyPrescribed != 0 && theQtyDispensed != 0 && printPrescriptionStatus != 999 && patientInstructions != "999")
                                        {
                                            theRow = theDT.NewRow();
                                            if (Convert.ToInt32(theDR["Generic"]) == 0)
                                            {
                                                theRow["DrugId"] = DrugID;
                                                theRow["GenericId"] = 0;
                                            }
                                            else
                                            {
                                                theRow["DrugId"] = 0;
                                                theRow["GenericId"] = DrugID;
                                            }
                                            theRow["Dose"] = Dose;
                                            theRow["UnitId"] = 0;
                                            theRow["StrengthId"] = theStrengthId;
                                            theRow["FrequencyId"] = theFrequencyId;
                                            theRow["Duration"] = theDuration;
                                            theRow["QtyPrescribed"] = theQtyPrescribed;
                                            if (theQtyDispensed == 99)
                                            {
                                                theRow["QtyDispensed"] = 0;
                                            }
                                            else
                                            {
                                                theRow["QtyDispensed"] = theQtyDispensed;
                                            }
                                            //theRow["QtyDispensed"] = theQtyDispensed;

                                            theRow["Prophylaxis"] = theProphylaxis;
                                            theRow["PrintPrescriptionStatus"] = printPrescriptionStatus;
                                            theRow["PatientInstructions"] = patientInstructions;
                                            theRow["Financed"] = theFinanced;

                                            theDT.Rows.Add(theRow);
                                            #region "Reset Variables
                                            Dose = 0;
                                            UnitId = 0;
                                            theStrengthId = 0;
                                            theFrequencyId = 0;
                                            theDuration = 0;
                                            theQtyPrescribed = 0;
                                            theQtyDispensed = 0;
                                            theFinanced = 99;
                                            if (ddlTreatment.SelectedItem.Value.ToString() == "223" || ddlTreatment.SelectedItem.Value.ToString() == "224" || ddlTreatment.SelectedItem.Value.ToString() == "225")
                                            {
                                                theProphylaxis = 999;
                                            }
                                            else
                                            {
                                                theProphylaxis = 0;
                                            }
                                            printPrescriptionStatus = 999;
                                            patientInstructions = "999";

                                            #endregion "Reset Variables
                                        }
                                    }
                                    else
                                    {
                                        //if (theStrengthId != 0 && theFrequencyId != 0 && theDuration != 0 && theQtyPrescribed != 0 && theQtyDispensed != 0 && theFinanced != 99 && theProphylaxis != 999)
                                        //if (theStrengthId != 0 && theFrequencyId != 0 && theDuration != 0 && theQtyPrescribed != 0 && theQtyDispensed != 0 && theProphylaxis != 999 && printPrescriptionStatus != 999 && patientInstructions != "999")
                                        if (theStrengthId != 0 && theFrequencyId != 0 && theDuration != 0 && theQtyPrescribed != 0 && theQtyDispensed != 0 && printPrescriptionStatus != 999 && patientInstructions != "999")
                                        {
                                            theRow = theDT.NewRow();
                                            if (Convert.ToInt32(theDR["Generic"]) == 0)
                                            {
                                                theRow["DrugId"] = theDR["DrugId"];
                                                theRow["GenericId"] = 0;
                                            }
                                            else
                                            {
                                                theRow["DrugId"] = 0;
                                                theRow["GenericId"] = theDR["DrugId"];
                                            }
                                            theRow["Dose"] = Dose;
                                            theRow["UnitId"] = 0;
                                            theRow["StrengthId"] = theStrengthId;
                                            theRow["FrequencyId"] = theFrequencyId;
                                            theRow["Duration"] = theDuration;
                                            theRow["QtyPrescribed"] = theQtyPrescribed;
                                            theRow["QtyDispensed"] = theQtyDispensed;
                                            theRow["Prophylaxis"] = theProphylaxis;
                                            theRow["PatientInstructions"] = patientInstructions;
                                            theRow["PrintPrescriptionStatus"] = printPrescriptionStatus;

                                            theRow["Financed"] = theFinanced;

                                            theDT.Rows.Add(theRow);
                                            #region "Reset Variables
                                            Dose = 0;
                                            UnitId = 0;
                                            theStrengthId = 0;
                                            theFrequencyId = 0;
                                            theDuration = 0;
                                            theQtyPrescribed = 0;
                                            theQtyDispensed = 0;
                                            theFinanced = 99;
                                            if (ddlTreatment.SelectedItem.Value.ToString() == "223" || ddlTreatment.SelectedItem.Value.ToString() == "224" || ddlTreatment.SelectedItem.Value.ToString() == "225")
                                            {
                                                theProphylaxis = 999;
                                            }
                                            else
                                            {
                                                theProphylaxis = 0;
                                            }
                                            printPrescriptionStatus = 999;
                                            patientInstructions = "999";

                                            #endregion "Reset Variables
                                        }
                                    }
                                    int total = TotelColFilled;
                                }
                            }
                        }
                        #region "18-Jun-07 - 12"
                        int ChkColFilled;
                        if (ddlTreatment.SelectedItem.Value.ToString() == "222" || ddlTreatment.SelectedItem.Value.ToString() == "224" || ddlTreatment.SelectedItem.Value.ToString() == "225")
                        {
                            if (Session["SCMModule"] != null)
                                //ChkColFilled = 4; if profilixis is consider
                                ChkColFilled = 3;
                            else
                                //ChkColFilled = 5;
                                ChkColFilled = 4;
                            if ((TotelColFilled > 0 && TotelColFilled < ChkColFilled) && (theContainer.ID == "pnlPedia"))
                            {
                                theDT.Rows.Clear();
                                theRow = theDT.NewRow();
                                theRow[0] = 99999;
                                theDT.Rows.Add(theRow);
                                return theDT;
                            }
                            else
                                TotelColFilled = 0;
                        }
                        else
                        {
                            if (Session["SCMModule"] != null)
                                //ChkColFilled = 5;
                                ChkColFilled = 4;
                            else
                                //ChkColFilled = 6;
                                ChkColFilled = 5;

                            if ((TotelColFilled > 0 && TotelColFilled < ChkColFilled) && (theContainer.ID == "pnlPedia"))
                            {
                                theDT.Rows.Clear();
                                theRow = theDT.NewRow();
                                theRow[0] = 99999;
                                theDT.Rows.Add(theRow);
                                return theDT;
                            }
                            else
                                TotelColFilled = 0;
                        }

                        #endregion "18-Jun-07 - 12"
                    }
                }
                #endregion "Additional ARV"
            }
            else if (theContainer.ID == "pnlOtherTBMedicaton")
            {
                #region "TB Drug"
                //pnl4 - id = PnlOtherMedication btn-txt = "OI Treatment & Other Medications"
                if (Session["AddTB"] != null)
                {
                    DataTable theADDTBDrug = (DataTable)Session["AddTB"];
                    int drugID = 0;
                    int TotelColFilled = 0;
                    if (theADDTBDrug == null)
                        return theDT;
                    foreach (DataRow theDR in theADDTBDrug.Rows)
                    {
                        drugID = 0;
                        Dose = 0;
                        theStrengthId = 0;
                        theFrequencyId = 0;
                        theDuration = 0;
                        theQtyPrescribed1 = 0;
                        theQtyDispensed1 = 0;
                        theFinanced = 0;
                        theTreatmentPhase = "";
                        theMonth = 0;
                        DataRow[] DRStrength = ((DataSet)ViewState["MasterData"]).Tables[1].Select("GenericId=" + Convert.ToInt32(theDR["DrugId"]));
                        if (DRStrength[0]["StrengthId"] != System.DBNull.Value)
                            theStrengthId = Convert.ToInt32(DRStrength[0]["StrengthId"]);
                        DataRow theRow;
                        foreach (Control y in theContainer.Controls)
                        {
                            if (y.GetType() == typeof(Panel))
                            {
                                foreach (Control x in y.Controls)
                                {
                                    if (x.GetType() == typeof(Panel))
                                    {
                                        MakeDrugTable(x);
                                    }
                                    else
                                    {
                                        if (theDR["DrugId"].ToString().LastIndexOf("8888") > 0) //--- if '8888' is added at the end of id - drug
                                        {
                                            drugID = Convert.ToInt32(theDR["DrugId"].ToString().Substring(0, theDR["DrugId"].ToString().Length - 4));
                                        }
                                        else if (theDR["DrugId"].ToString().LastIndexOf("9999") > 0) //--- if '9999' is added at the end of id  - generic
                                        {
                                            drugID = Convert.ToInt32(theDR["DrugId"].ToString().Substring(0, theDR["DrugId"].ToString().Length - 4));
                                        }
                                        else
                                        {
                                            drugID = Convert.ToInt32(theDR["DrugId"]);
                                        }
                                        if (x.GetType() == typeof(DropDownList))
                                        {
                                            if (x.ID.EndsWith(drugID.ToString() + "^" + theDR["Generic"].ToString()) && x.ID.StartsWith("drgFrequency"))
                                            {
                                                theFrequencyId = Convert.ToInt32(((DropDownList)x).SelectedValue);
                                                TotelColFilled++;
                                            }
                                            if (x.ID.EndsWith(drugID.ToString() + "^" + theDR["Generic"].ToString()) && x.ID.StartsWith("drgTreatmenPhase"))
                                            {
                                                theTreatmentPhase = Convert.ToString(((DropDownList)x).SelectedValue);
                                                if (theTreatmentPhase != "0")
                                                    TotelColFilled++;
                                            }
                                            if (x.ID.EndsWith(drugID.ToString() + "^" + theDR["Generic"].ToString()) && x.ID.StartsWith("drgTreatmenMonth"))
                                            {
                                                theMonth = Convert.ToInt32(((DropDownList)x).SelectedValue);
                                                if (theMonth != 0)
                                                    TotelColFilled++;
                                            }
                                        }
                                        if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                                        {
                                            if (x.ID.EndsWith(theDR["DrugId"].ToString() + "^0") && x.ID.StartsWith("drgDose"))
                                            {
                                                if (((TextBox)x).Text != "")
                                                {
                                                    Dose = Convert.ToDecimal(((TextBox)x).Text);
                                                    #region "18-Jun-07 - 9"
                                                    if (Dose != 0)
                                                        TotelColFilled++;
                                                    #endregion "18-Jun-07 - 9"
                                                }
                                            }
                                            if (x.ID.EndsWith(drugID.ToString() + "^" + theDR["Generic"].ToString()) && x.ID.StartsWith("drgDuration"))
                                            {
                                                if (((TextBox)x).Text != "")
                                                {
                                                    theDuration = Convert.ToDecimal(((TextBox)x).Text);
                                                    TotelColFilled++;
                                                }
                                            }
                                            if (x.ID.EndsWith(drugID.ToString() + "^" + theDR["Generic"].ToString()) && x.ID.StartsWith("drgQtyPrescribed"))
                                            {
                                                if (((TextBox)x).Text != "")
                                                {
                                                    theQtyPrescribed1 = Convert.ToDecimal(((TextBox)x).Text);
                                                    TotelColFilled++;
                                                }
                                            }
                                            if (x.ID.EndsWith(drugID.ToString() + "^" + theDR["Generic"].ToString()) && x.ID.StartsWith("drgQtyDispensed"))
                                            {
                                                if (((TextBox)x).Text != "")
                                                {
                                                    theQtyDispensed1 = Convert.ToDecimal(((TextBox)x).Text);
                                                    TotelColFilled++;
                                                }
                                            }

                                            //patient instructions
                                            if (x.ID.StartsWith("txtPtnInstructions"))
                                            {
                                                c = x.ID.Length;
                                                if (x.ID.EndsWith(drugID.ToString()) && x.ID.StartsWith("txtPtnInstructions"))
                                                {
                                                    if (((TextBox)x).Text != "")
                                                    {
                                                        patientInstructions = Convert.ToString(((TextBox)x).Text);
                                                        //TotelColFilled++;
                                                    }
                                                    else
                                                    {
                                                        patientInstructions = "";
                                                    }
                                                }
                                            }
                                        }
                                        if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                                        {
                                            if (x.ID.StartsWith("chkProphylaxis"))
                                            {
                                                c = x.ID.Length;
                                                if (x.ID.EndsWith(drugID.ToString()) && x.ID.StartsWith("chkProphylaxis"))
                                                {
                                                    if (((CheckBox)x).Checked == true)
                                                    {
                                                        theProphylaxis = 1;
                                                        //TotelColFilled++;
                                                    }
                                                }
                                            }

                                            //print prescription
                                            if (x.ID.StartsWith("chkPrintPrescription"))
                                            {
                                                c = x.ID.Length;
                                                if (x.ID.EndsWith(drugID.ToString()) && x.ID.StartsWith("chkPrintPrescription"))
                                                {
                                                    if (((CheckBox)x).Checked == true)
                                                    {
                                                        printPrescriptionStatus = 1;
                                                        //TotelColFilled++;
                                                    }
                                                    else
                                                    {
                                                        printPrescriptionStatus = 0;
                                                        //TotelColFilled++;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (Session["Paperless"].ToString() == "1")
                        {
                            //if (UnitId != 0 || theFrequencyId != 0 || Dose != 0 || theDuration != 0 || theQtyPrescribed != 0 || theQtyDispensed != 0 || theTreatmentPhase != "0" || theMonth != 0 || theFinanced != 99)
                            //if (theFrequencyId != 0 && Dose != 0 && theDuration != 0 && theQtyPrescribed1 != 0 && theTreatmentPhase != "0" && theMonth != 0 && printPrescriptionStatus != 999 && patientInstructions != "999" || theProphylaxis != 999)
                            if (theFrequencyId != 0 && Dose != 0 && theDuration != 0 && theQtyPrescribed1 != 0 && theTreatmentPhase != "0" && theMonth != 0 && printPrescriptionStatus != 999 && patientInstructions != "999")
                            {
                                theRow = theDT.NewRow();
                                if (Convert.ToInt32(theDR["Generic"]) == 0)
                                {
                                    theRow["DrugId"] = drugID;
                                    theRow["GenericId"] = 0;
                                }
                                else
                                {
                                    theRow["DrugId"] = 0;
                                    theRow["GenericId"] = drugID;
                                }
                                theRow["Dose"] = Dose;
                                theRow["UnitId"] = UnitId;
                                theRow["StrengthId"] = theStrengthId;
                                theRow["FrequencyId"] = theFrequencyId;
                                theRow["Duration"] = theDuration;
                                theRow["QtyPrescribed"] = theQtyPrescribed1;
                                //theRow["QtyDispensed"] = theQtyDispensed;

                                theRow["QtyDispensed"] = theQtyDispensed1;
                                theRow["Financed"] = theFinanced;
                                theRow["TreatmentPhase"] = theTreatmentPhase;
                                theRow["TrMonth"] = theMonth;
                                theRow["Prophylaxis"] = theProphylaxis;
                                theRow["PrintPrescriptionStatus"] = printPrescriptionStatus;
                                theRow["PatientInstructions"] = patientInstructions;
                                theDT.Rows.Add(theRow);
                                #region "Reset Variables
                                Dose = 0;
                                UnitId = 0;
                                theStrengthId = 0;
                                theFrequencyId = 0;
                                theDuration = 0;
                                theQtyPrescribed = 0;
                                theQtyDispensed = 0;
                                theFinanced = 99;
                                if (ddlTreatment.SelectedItem.Value.ToString() == "223")
                                {
                                    theProphylaxis = 999;
                                }
                                else
                                {
                                    theProphylaxis = 0;
                                }
                                printPrescriptionStatus = 999;
                                patientInstructions = "999";
                                #endregion "Reset Variables
                            }
                        }
                        else
                        {
                            //if (theFrequencyId != 0 && Dose != 0 && theDuration != 0 && theQtyPrescribed1 != 0 && theQtyDispensed1 != 0 && theTreatmentPhase != "0" && theMonth != 0 && theProphylaxis != 999 && printPrescriptionStatus != 999 && patientInstructions != "999")
                            if (theFrequencyId != 0 && Dose != 0 && theDuration != 0 && theQtyPrescribed1 != 0 && theQtyDispensed1 != 0 && theTreatmentPhase != "0" && theMonth != 0 && printPrescriptionStatus != 999 && patientInstructions != "999")
                            {
                                theRow = theDT.NewRow();
                                if (Convert.ToInt32(theDR["Generic"]) == 0)
                                {
                                    theRow["DrugId"] = drugID;
                                    theRow["GenericId"] = 0;
                                }
                                else
                                {
                                    theRow["DrugId"] = 0;
                                    theRow["GenericId"] = drugID;
                                }
                                theRow["Dose"] = Dose;
                                theRow["UnitId"] = UnitId;
                                theRow["StrengthId"] = theStrengthId;
                                theRow["FrequencyId"] = theFrequencyId;
                                theRow["Duration"] = theDuration;
                                theRow["QtyPrescribed"] = theQtyPrescribed1;
                                //theRow["QtyDispensed"] = theQtyDispensed;

                                theRow["QtyDispensed"] = theQtyDispensed1;
                                theRow["Financed"] = theFinanced;
                                theRow["TreatmentPhase"] = theTreatmentPhase;
                                theRow["TrMonth"] = theMonth;
                                theRow["Prophylaxis"] = theProphylaxis;
                                theRow["PrintPrescriptionStatus"] = printPrescriptionStatus;
                                theRow["PatientInstructions"] = patientInstructions;
                                theDT.Rows.Add(theRow);
                                #region "Reset Variables
                                Dose = 0;
                                UnitId = 0;
                                theStrengthId = 0;
                                theFrequencyId = 0;
                                theDuration = 0;
                                theQtyPrescribed = 0;
                                theQtyDispensed = 0;
                                theFinanced = 99;
                                if (ddlTreatment.SelectedItem.Value.ToString() == "223")
                                {
                                    theProphylaxis = 999;
                                }
                                else
                                {
                                    theProphylaxis = 0;
                                }
                                printPrescriptionStatus = 999;
                                patientInstructions = "999";
                                #endregion "Reset Variables
                            }
                        }

                        #region "18-Jun-07 - 12"
                        int ChkColFilled;

                        if (Session["SCMModule"] != null)
                            //ChkColFilled = 5;
                            ChkColFilled = 3;
                        else
                            //ChkColFilled = 6;
                            ChkColFilled = 4;

                        if ((TotelColFilled > 0 && TotelColFilled < ChkColFilled) && (theContainer.ID == "pnlOtherTBMedicaton"))
                        {
                            theDT.Rows.Clear();
                            theRow = theDT.NewRow();
                            theRow[0] = 99999;
                            theDT.Rows.Add(theRow);
                            return theDT;
                        }
                        else
                            TotelColFilled = 0;

                        #endregion "18-Jun-07 - 12"
                    }
                }
                #endregion "TB Drug"
            }
            else if (theContainer.ID == "PnlOtMed")
            {
                #region "Additional Drugs"
                //pnl4 - id = PnlOtherMedication btn-txt = "OI Treatment & Other Medications"
                if (Session["OtherDrugs"] != null)
                {
                    DataTable theOtherDrug = (DataTable)Session["OtherDrugs"];
                    int DrugID = 0;
                    int TotelColFilled = 0;
                    if (theOtherDrug == null)
                        return theDT;
                    foreach (DataRow theDR in theOtherDrug.Rows)
                    {
                        DrugID = 0;
                        theStrengthId = 0;
                        theFrequencyId = 0;
                        theDuration = 0;
                        theQtyPrescribed1 = 0;
                        theQtyDispensed1 = 0;
                        DataRow[] DRStrength = ((DataSet)ViewState["MasterData"]).Tables[1].Select("GenericId=" + Convert.ToInt32(theDR["DrugId"]));
                        if (DRStrength[0]["StrengthId"] != System.DBNull.Value)
                            theStrengthId = Convert.ToInt32(DRStrength[0]["StrengthId"]);
                        DataRow theRow;
                        foreach (Control y in theContainer.Controls)
                        {
                            if (y.GetType() == typeof(Panel))
                            {
                                foreach (Control x in y.Controls)
                                {
                                    if (x.GetType() == typeof(Panel))
                                    {
                                        MakeDrugTable(x);
                                    }
                                    else
                                    {
                                        if (theDR["DrugId"].ToString().LastIndexOf("8888") > 0) //--- if '8888' is added at the end of id - drug
                                        {
                                            DrugID = Convert.ToInt32(theDR["DrugId"].ToString().Substring(0, theDR["DrugId"].ToString().Length - 4));
                                        }
                                        else if (theDR["DrugId"].ToString().LastIndexOf("9999") > 0) //--- if '9999' is added at the end of id  - generic
                                        {
                                            DrugID = Convert.ToInt32(theDR["DrugId"].ToString().Substring(0, theDR["DrugId"].ToString().Length - 4));
                                        }
                                        else
                                        {
                                            DrugID = Convert.ToInt32(theDR["DrugId"]);
                                        }
                                        if (x.GetType() == typeof(DropDownList))
                                        {
                                            if (x.ID.EndsWith(DrugID.ToString() + "^" + theDR["Generic"].ToString()) && x.ID.StartsWith("drgFrequency"))
                                            {
                                                theFrequencyId = Convert.ToInt32(((DropDownList)x).SelectedValue);
                                                TotelColFilled++;
                                            }
                                        }
                                        if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                                        {
                                            if (x.ID.EndsWith(theDR["DrugId"].ToString() + "^0") && x.ID.StartsWith("drgDose"))
                                            {
                                                if (((TextBox)x).Text != "")
                                                {
                                                    Dose = Convert.ToDecimal(((TextBox)x).Text);

                                                    if (Dose != 0)
                                                        TotelColFilled++;
                                                }
                                            }

                                            if (x.ID.EndsWith(DrugID.ToString() + "^" + theDR["Generic"].ToString()) && x.ID.StartsWith("drgDuration"))
                                            {
                                                if (((TextBox)x).Text != "")
                                                {
                                                    theDuration = Convert.ToDecimal(((TextBox)x).Text);
                                                    TotelColFilled++;
                                                }
                                            }
                                            if (x.ID.EndsWith(DrugID.ToString() + "^" + theDR["Generic"].ToString()) && x.ID.StartsWith("drgQtyPrescribed"))
                                            {
                                                if (((TextBox)x).Text != "")
                                                {
                                                    theQtyPrescribed1 = Convert.ToDecimal(((TextBox)x).Text);
                                                    TotelColFilled++;
                                                }
                                            }
                                            if (x.ID.EndsWith(DrugID.ToString() + "^" + theDR["Generic"].ToString()) && x.ID.StartsWith("drgQtyDispensed"))
                                            {
                                                if (((TextBox)x).Text != "")
                                                {
                                                    theQtyDispensed1 = Convert.ToDecimal(((TextBox)x).Text);
                                                    TotelColFilled++;
                                                }
                                            }

                                            //patient instructions
                                            if (x.ID.StartsWith("txtPtnInstructions"))
                                            {
                                                c = x.ID.Length;
                                                if (x.ID.EndsWith(DrugID.ToString()) && x.ID.StartsWith("txtPtnInstructions"))
                                                {
                                                    if (((TextBox)x).Text != "")
                                                    {
                                                        patientInstructions = Convert.ToString(((TextBox)x).Text);
                                                        //TotelColFilled++;
                                                    }
                                                    else
                                                    {
                                                        patientInstructions = "";
                                                    }
                                                }
                                            }
                                        }
                                        if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                                        {
                                            if (x.ID.StartsWith("chkProphylaxis"))
                                            {
                                                c = x.ID.Length;
                                                if (x.ID.EndsWith(DrugID.ToString()) && x.ID.StartsWith("chkProphylaxis"))
                                                {
                                                    if (((CheckBox)x).Checked == true)
                                                    {
                                                        theProphylaxis = 1;
                                                    }
                                                    TotelColFilled++;
                                                }
                                            }

                                            //print prescription
                                            if (x.ID.StartsWith("chkPrintPrescription"))
                                            {
                                                c = x.ID.Length;
                                                if (x.ID.EndsWith(DrugID.ToString()) && x.ID.StartsWith("chkPrintPrescription"))
                                                {
                                                    if (((CheckBox)x).Checked == true)
                                                    {
                                                        printPrescriptionStatus = 1;
                                                        //TotelColFilled++;
                                                    }
                                                    else
                                                    {
                                                        printPrescriptionStatus = 0;
                                                        //TotelColFilled++;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (Session["Paperless"].ToString() == "1")
                        {
                            //if (theFrequencyId != 0 && Dose != 0 && theDuration != 0 && theQtyPrescribed1 != 0 && printPrescriptionStatus != 999 && patientInstructions != "999" || theProphylaxis != 999)
                            if (theFrequencyId != 0 && Dose != 0 && theDuration != 0 && theQtyPrescribed1 != 0 && printPrescriptionStatus != 999 && patientInstructions != "999")
                            {
                                theRow = theDT.NewRow();
                                if (Convert.ToInt32(theDR["Generic"]) == 0)
                                {
                                    theRow["DrugId"] = DrugID;
                                    theRow["GenericId"] = 0;
                                }
                                else
                                {
                                    theRow["DrugId"] = 0;
                                    theRow["GenericId"] = DrugID;
                                }
                                theRow["Dose"] = Dose;
                                theRow["UnitId"] = UnitId;
                                theRow["StrengthId"] = theStrengthId;
                                theRow["FrequencyId"] = theFrequencyId;
                                theRow["Duration"] = theDuration;
                                theRow["QtyPrescribed"] = theQtyPrescribed1;
                                //theRow["QtyDispensed"] = theQtyDispensed;

                                theRow["QtyDispensed"] = theQtyDispensed1;
                                theRow["Financed"] = 99;
                                theRow["Prophylaxis"] = theProphylaxis;
                                theRow["PrintPrescriptionStatus"] = printPrescriptionStatus;
                                theRow["PatientInstructions"] = patientInstructions;
                                theDT.Rows.Add(theRow);

                                Dose = 0;
                                UnitId = 0;
                                theStrengthId = 0;
                                theFrequencyId = 0;
                                theDuration = 0;
                                theQtyPrescribed = 0;
                                theQtyDispensed = 0;
                                theFinanced = 99;
                                if (ddlTreatment.SelectedItem.Value.ToString() == "223")
                                {
                                    theProphylaxis = 999;
                                }
                                else
                                {
                                    theProphylaxis = 0;
                                }
                                printPrescriptionStatus = 999;
                                patientInstructions = "999";
                            }
                        }
                        else
                        {
                            //if (theFrequencyId != 0 && Dose != 0 && theDuration != 0 && theQtyPrescribed1 != 0 && theQtyDispensed1 != 0 && printPrescriptionStatus != 999 && patientInstructions != "999" || theProphylaxis != 999)
                            if (theFrequencyId != 0 && Dose != 0 && theDuration != 0 && theQtyPrescribed1 != 0 && theQtyDispensed1 != 0 && printPrescriptionStatus != 999 && patientInstructions != "999")
                            {
                                theRow = theDT.NewRow();
                                if (Convert.ToInt32(theDR["Generic"]) == 0)
                                {
                                    theRow["DrugId"] = DrugID;
                                    theRow["GenericId"] = 0;
                                }
                                else
                                {
                                    theRow["DrugId"] = 0;
                                    theRow["GenericId"] = DrugID;
                                }
                                theRow["Dose"] = Dose;
                                theRow["UnitId"] = UnitId;
                                theRow["StrengthId"] = theStrengthId;
                                theRow["FrequencyId"] = theFrequencyId;
                                theRow["Duration"] = theDuration;
                                theRow["QtyPrescribed"] = theQtyPrescribed1;
                                //theRow["QtyDispensed"] = theQtyDispensed;

                                theRow["QtyDispensed"] = theQtyDispensed1;
                                theRow["Financed"] = 99;
                                theRow["Prophylaxis"] = theProphylaxis;
                                theRow["PrintPrescriptionStatus"] = printPrescriptionStatus;
                                theRow["PatientInstructions"] = patientInstructions;
                                theDT.Rows.Add(theRow);

                                Dose = 0;
                                UnitId = 0;
                                theStrengthId = 0;
                                theFrequencyId = 0;
                                theDuration = 0;
                                theQtyPrescribed = 0;
                                theQtyDispensed = 0;
                                theFinanced = 99;
                                if (ddlTreatment.SelectedItem.Value.ToString() == "223")
                                {
                                    theProphylaxis = 999;
                                }
                                else
                                {
                                    theProphylaxis = 0;
                                }
                                printPrescriptionStatus = 999;
                                patientInstructions = "999";
                            }
                        }

                        int ChkColFilled;
                        if (Session["SCMModule"] != null)
                            //ChkColFilled = 5;
                            ChkColFilled = 4;
                        else
                            ChkColFilled = 5;
                        //ChkColFilled = 6;

                        if ((TotelColFilled > 0 && TotelColFilled < ChkColFilled) && (theContainer.ID == "PnlOtMed"))
                        {
                            theDT.Rows.Clear();
                            theRow = theDT.NewRow();
                            theRow[0] = 99999;
                            theDT.Rows.Add(theRow);
                            return theDT;
                        }
                        else
                            TotelColFilled = 0;
                    }
                }
                #endregion "Additional Drugs"
            }
            else if(theContainer.ID == "panelVaccine")
            {
                if (Session["VaccineDrugs"] != null)
                {
                    DataTable dtVaccine = (DataTable)Session["VaccineDrugs"];
                    int drugID = 0;
                    int TotelColFilled = 0;
                    if (dtVaccine == null)
                        return theDT;
                    foreach (DataRow theDR in dtVaccine.Rows)
                    {
                        drugID = 0;
                        Dose = 0;
                        theStrengthId = 0;
                        theFrequencyId = 0;
                        theDuration = 0;
                        theQtyPrescribed1 = 0;
                        theQtyDispensed1 = 0;
                        theFinanced = 0;
                        theTreatmentPhase = "";
                        theMonth = 0;
                        DataRow[] DRStrength = ((DataSet)ViewState["MasterData"]).Tables[1].Select("GenericId=" + Convert.ToInt32(theDR["DrugId"]));
                        if (DRStrength[0]["StrengthId"] != DBNull.Value)
                            theStrengthId = Convert.ToInt32(DRStrength[0]["StrengthId"]);
                        DataRow theRow;
                        foreach (Control y in theContainer.Controls)
                        {
                            if (y.GetType() == typeof(Panel))
                            {
                                foreach (Control x in y.Controls)
                                {
                                    if (x.GetType() == typeof(Panel))
                                    {
                                        MakeDrugTable(x);
                                    }
                                    else
                                    {
                                        if (theDR["DrugId"].ToString().LastIndexOf("8888") > 0) //--- if '8888' is added at the end of id - drug
                                        {
                                            drugID = Convert.ToInt32(theDR["DrugId"].ToString().Substring(0, theDR["DrugId"].ToString().Length - 4));
                                        }
                                        else if (theDR["DrugId"].ToString().LastIndexOf("9999") > 0) //--- if '9999' is added at the end of id  - generic
                                        {
                                            drugID = Convert.ToInt32(theDR["DrugId"].ToString().Substring(0, theDR["DrugId"].ToString().Length - 4));
                                        }
                                        else
                                        {
                                            drugID = Convert.ToInt32(theDR["DrugId"]);
                                        }
                                        if (x.GetType() == typeof(DropDownList))
                                        {
                                            if (x.ID.EndsWith(drugID.ToString() + "^" + theDR["Generic"].ToString()) && x.ID.StartsWith("drgFrequency"))
                                            {
                                                theFrequencyId = Convert.ToInt32(((DropDownList)x).SelectedValue);
                                                TotelColFilled++;
                                            }
                                            if (x.ID.EndsWith(drugID.ToString() + "^" + theDR["Generic"].ToString()) && x.ID.StartsWith("drgVacSchedule"))
                                            {
                                                strVaccineSchedule = Convert.ToString(((DropDownList)x).SelectedValue);
                                                if (strVaccineSchedule.Trim().Length> 0 && strVaccineSchedule != "0" )
                                                    TotelColFilled++;
                                            }
                                           
                                        }
                                        if (x.GetType() == typeof(TextBox))
                                        {
                                            if (x.ID.EndsWith(theDR["DrugId"].ToString() + "^0") && x.ID.StartsWith("drgDose"))
                                            {
                                                if (((TextBox)x).Text != "")
                                                {
                                                    Dose = Convert.ToDecimal(((TextBox)x).Text);
                                                    #region "18-Jun-07 - 9"
                                                    if (Dose != 0)
                                                        TotelColFilled++;
                                                    #endregion "18-Jun-07 - 9"
                                                }
                                            }
                                            if (x.ID.EndsWith(drugID.ToString() + "^" + theDR["Generic"].ToString()) && x.ID.StartsWith("drgDuration"))
                                            {
                                                if (((TextBox)x).Text != "")
                                                {
                                                    theDuration = Convert.ToDecimal(((TextBox)x).Text);
                                                    TotelColFilled++;
                                                }
                                            }
                                            if (x.ID.EndsWith(drugID.ToString() + "^" + theDR["Generic"].ToString()) && x.ID.StartsWith("drgQtyPrescribed"))
                                            {
                                                if (((TextBox)x).Text != "")
                                                {
                                                    theQtyPrescribed1 = Convert.ToDecimal(((TextBox)x).Text);
                                                    TotelColFilled++;
                                                }
                                            }
                                            if (x.ID.EndsWith(drugID.ToString() + "^" + theDR["Generic"].ToString()) && x.ID.StartsWith("drgQtyDispensed"))
                                            {
                                                if (((TextBox)x).Text != "")
                                                {
                                                    theQtyDispensed1 = Convert.ToDecimal(((TextBox)x).Text);
                                                    TotelColFilled++;
                                                }
                                            }

                                            //patient instructions
                                            if (x.ID.StartsWith("txtPtnInstructions"))
                                            {
                                                c = x.ID.Length;
                                                if (x.ID.EndsWith(drugID.ToString()) && x.ID.StartsWith("txtPtnInstructions"))
                                                {
                                                    if (((TextBox)x).Text != "")
                                                    {
                                                        patientInstructions = Convert.ToString(((TextBox)x).Text);
                                                        //TotelColFilled++;
                                                    }
                                                    else
                                                    {
                                                        patientInstructions = "";
                                                    }
                                                }
                                            }
                                        }
                                        
                                        if (x.GetType() == typeof(CheckBox))
                                        {
                                            if (x.ID.StartsWith("chkProphylaxis"))
                                            {
                                                c = x.ID.Length;
                                                if (x.ID.EndsWith(drugID.ToString()) && x.ID.StartsWith("chkProphylaxis"))
                                                {
                                                    if (((CheckBox)x).Checked == true)
                                                    {
                                                        theProphylaxis = 1;
                                                        //TotelColFilled++;
                                                    }
                                                }
                                            }

                                            //print prescription
                                            if (x.ID.StartsWith("chkPrintPrescription"))
                                            {
                                                c = x.ID.Length;
                                                if (x.ID.EndsWith(drugID.ToString()) && x.ID.StartsWith("chkPrintPrescription"))
                                                {
                                                    if (((CheckBox)x).Checked == true)
                                                    {
                                                        printPrescriptionStatus = 1;
                                                        //TotelColFilled++;
                                                    }
                                                    else
                                                    {
                                                        printPrescriptionStatus = 0;
                                                        //TotelColFilled++;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (Session["Paperless"].ToString() == "1")
                        {
                            //if (UnitId != 0 || theFrequencyId != 0 || Dose != 0 || theDuration != 0 || theQtyPrescribed != 0 || theQtyDispensed != 0 || theTreatmentPhase != "0" || theMonth != 0 || theFinanced != 99)
                            //if (theFrequencyId != 0 && Dose != 0 && theDuration != 0 && theQtyPrescribed1 != 0 && theTreatmentPhase != "0" && theMonth != 0 && printPrescriptionStatus != 999 && patientInstructions != "999" || theProphylaxis != 999)
                            if (theFrequencyId != 0 && Dose != 0 && theDuration != 0 && theQtyPrescribed1 != 0 && strVaccineSchedule != "0"  && printPrescriptionStatus != 999 && patientInstructions != "999")
                            {
                                theRow = theDT.NewRow();
                                if (Convert.ToInt32(theDR["Generic"]) == 0)
                                {
                                    theRow["DrugId"] = drugID;
                                    theRow["GenericId"] = 0;
                                }
                                else
                                {
                                    theRow["DrugId"] = 0;
                                    theRow["GenericId"] = drugID;
                                }
                                theRow["Dose"] = Dose;
                                theRow["UnitId"] = UnitId;
                                theRow["StrengthId"] = theStrengthId;
                                theRow["FrequencyId"] = theFrequencyId;
                                theRow["Duration"] = theDuration;
                                theRow["QtyPrescribed"] = theQtyPrescribed1;
                                //theRow["QtyDispensed"] = theQtyDispensed;

                                theRow["QtyDispensed"] = theQtyDispensed1;
                                theRow["Financed"] = theFinanced;
                                //theRow["TreatmentPhase"] = theTreatmentPhase;
                              //  theRow["TrMonth"] = theMonth;
                                theRow["Prophylaxis"] = theProphylaxis;
                                theRow["PrintPrescriptionStatus"] = printPrescriptionStatus;
                                theRow["PatientInstructions"] = patientInstructions;
                                theRow["ScheduleId"] = strVaccineSchedule;
                                theDT.Rows.Add(theRow);
                                #region "Reset Variables
                                Dose = 0;
                                UnitId = 0;
                                theStrengthId = 0;
                                theFrequencyId = 0;
                                theDuration = 0;
                                theQtyPrescribed = 0;
                                theQtyDispensed = 0;
                                theFinanced = 99;
                                if (ddlTreatment.SelectedItem.Value.ToString() == "223")
                                {
                                    theProphylaxis = 999;
                                }
                                else
                                {
                                    theProphylaxis = 0;
                                }
                                printPrescriptionStatus = 999;
                                patientInstructions = "999";
                                #endregion "Reset Variables
                            }
                        }
                        else
                        {
                            //if (theFrequencyId != 0 && Dose != 0 && theDuration != 0 && theQtyPrescribed1 != 0 && theQtyDispensed1 != 0 && theTreatmentPhase != "0" && theMonth != 0 && theProphylaxis != 999 && printPrescriptionStatus != 999 && patientInstructions != "999")
                            if (theFrequencyId != 0 && Dose != 0 && theDuration != 0 && theQtyPrescribed1 != 0 && theQtyDispensed1 != 0 && strVaccineSchedule != "0"  && printPrescriptionStatus != 999 && patientInstructions != "999")
                            {
                                theRow = theDT.NewRow();
                                if (Convert.ToInt32(theDR["Generic"]) == 0)
                                {
                                    theRow["DrugId"] = drugID;
                                    theRow["GenericId"] = 0;
                                }
                                else
                                {
                                    theRow["DrugId"] = 0;
                                    theRow["GenericId"] = drugID;
                                }
                                theRow["Dose"] = Dose;
                                theRow["UnitId"] = UnitId;
                                theRow["StrengthId"] = theStrengthId;
                                theRow["FrequencyId"] = theFrequencyId;
                                theRow["Duration"] = theDuration;
                                theRow["QtyPrescribed"] = theQtyPrescribed1;
                                theRow["ScheduleId"] = strVaccineSchedule;
                                //theRow["QtyDispensed"] = theQtyDispensed;

                                theRow["QtyDispensed"] = theQtyDispensed1;
                                theRow["Financed"] = theFinanced;
                                //theRow["TreatmentPhase"] = theTreatmentPhase;
                               // theRow["TrMonth"] = theMonth;
                                theRow["Prophylaxis"] = theProphylaxis;
                                theRow["PrintPrescriptionStatus"] = printPrescriptionStatus;
                                theRow["PatientInstructions"] = patientInstructions;
                                theDT.Rows.Add(theRow);
                                #region "Reset Variables
                                Dose = 0;
                                UnitId = 0;
                                theStrengthId = 0;
                                theFrequencyId = 0;
                                theDuration = 0;
                                theQtyPrescribed = 0;
                                theQtyDispensed = 0;
                                theFinanced = 99;
                                if (ddlTreatment.SelectedItem.Value.ToString() == "223")
                                {
                                    theProphylaxis = 999;
                                }
                                else
                                {
                                    theProphylaxis = 0;
                                }
                                printPrescriptionStatus = 999;
                                patientInstructions = "999";
                                #endregion "Reset Variables
                            }
                        }

                        #region "18-Jun-07 - 12"
                        int ChkColFilled;

                        if (Session["SCMModule"] != null)
                            //ChkColFilled = 5;
                            ChkColFilled = 3;
                        else
                            //ChkColFilled = 6;
                            ChkColFilled = 4;

                        if ((TotelColFilled > 0 && TotelColFilled < ChkColFilled) && (theContainer.ID == "pnlOtherTBMedicaton"))
                        {
                            theDT.Rows.Clear();
                            theRow = theDT.NewRow();
                            theRow[0] = 99999;
                            theDT.Rows.Add(theRow);
                            return theDT;
                        }
                        else
                            TotelColFilled = 0;

                        #endregion "18-Jun-07 - 12"
                    }
                }
            }
            return theDT;
        }

        /// <summary>
        /// Makes the drug table regimen.
        /// </summary>
        /// <param name="theContainer">The container.</param>
        /// <returns></returns>
        private DataTable MakeDrugTableRegimen(Control theContainer)
        {
           // IDrug DrugManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BDrug, BusinessProcess.Pharmacy");

            DataTable theDT = new DataTable();

            if (Session["Data"] == null)
            {
                theDT = CreateTable();
            }
            else
            {
                theDT = (DataTable)Session["Data"];
            }

            DataSet theGenericDS = new DataSet();

            return theDT;
        }

        /// <summary>
        /// Makes the regimen generic table.
        /// </summary>
        /// <param name="theDS">The ds.</param>
        private void MakeRegimenGenericTable(DataSet theDS)
        {
            DataTable theDT;
            if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            {
                theDT = theDS.Tables[17];
            }
            else
            {
                theDT = theDS.Tables[17];
            }

            DataView theDV;//= new DataView();
            BindFunctions theBindMgr = new BindFunctions();
            int RegimenId = -1;
            string GenericID = string.Empty;
            string GenericName = string.Empty;

            DataTable theDT1 = new DataTable();
            theDT1.Columns.Add("ID", Type.GetType("System.Int32"));
            theDT1.Columns.Add("TBRegimenID", Type.GetType("System.Int32"));
            theDT1.Columns.Add("Name", Type.GetType("System.String"));
            theDT1.Columns.Add("UserID", Type.GetType("System.Int32"));
            theDT1.Columns.Add("GenericID", Type.GetType("System.String"));
            theDT1.Columns.Add("GenericName", Type.GetType("System.String"));
            theDT1.Columns.Add("Status", Type.GetType("System.String"));

            DataView DV = new DataView(theDT);
            //DV.Sort = "GenericID asc";
            IQCareUtils theUtil = new IQCareUtils();
            theDT = theUtil.CreateTableFromDataView(DV);

            for (int i = 0; i < theDT.Rows.Count; i++)
            {
                if (Convert.ToInt32(theDT.Rows[i]["ID"]) > 0)
                {
                    if (RegimenId != Convert.ToInt32(theDT.Rows[i]["ID"]))
                    {
                        RegimenId = Convert.ToInt32(theDT.Rows[i]["ID"]);

                        theDV = new DataView(theDT);
                        theDV.RowFilter = "ID = " + RegimenId;

                        if (theDV.Count > 0)
                        {
                            for (int j = 0; j < theDV.Count; j++)
                            {
                                if (GenericID.Trim() == "")
                                {
                                    GenericID = Convert.ToString(theDV[j].Row["GenericID"]);
                                }
                                else
                                {
                                    if (GenericID.Contains(Convert.ToString(theDV[j].Row["GenericID"])) == false)
                                        GenericID = GenericID + "/" + " " + Convert.ToString(theDV[j].Row["GenericID"]);
                                }

                                if (GenericName.Trim() == "")
                                {
                                    GenericName = Convert.ToString(theDV[j].Row["GenericName"]);
                                }
                                else
                                {
                                    if (GenericName.Contains(Convert.ToString(theDV[j].Row["GenericName"])) == false)
                                        GenericName = GenericName + "/" + " " + Convert.ToString(theDV[j].Row["GenericName"]);
                                }
                            }

                            DataRow theDR = theDT1.NewRow();
                            theDR["ID"] = Convert.ToInt32(theDT.Rows[i]["ID"]);
                            theDR["TBRegimenID"] = Convert.ToInt32(theDT.Rows[i]["TBRegimenID"]);
                            theDR["Name"] = Convert.ToString(theDT.Rows[i]["Name"]);
                            theDR["UserID"] = Convert.ToInt32(theDT.Rows[i]["UserID"]);
                            theDR["GenericID"] = GenericID;
                            theDR["GenericName"] = GenericName;
                            theDR["Status"] = Convert.ToString(theDT.Rows[i]["Status"]);
                            theDT1.Rows.Add(theDR);
                            GenericID = "";
                            GenericName = "";
                        }
                    }
                }
                else
                {
                    DataRow theDR = theDT1.NewRow();
                    theDR["ID"] = Convert.ToInt32(theDT.Rows[i]["ID"]);
                    theDR["TBRegimenID"] = Convert.ToInt32(theDT.Rows[i]["TBRegimenID"]);
                    theDR["Name"] = Convert.ToString(theDT.Rows[i]["Name"]);
                    theDR["UserID"] = Convert.ToInt32(theDT.Rows[i]["UserID"]);
                    theDR["GenericID"] = Convert.ToString(theDT.Rows[0]["GenericID"]); ;
                    theDR["GenericName"] = Convert.ToString(theDT.Rows[i]["GenericName"]);
                    theDR["Status"] = Convert.ToString(theDT.Rows[i]["Status"]);
                    theDT1.Rows.Add(theDR);
                }
            }

            DV = new DataView(theDT1);
            DV.Sort = "TBRegimenID asc";
            theDT1 = theUtil.CreateTableFromDataView(DV);
            //theBindMgr.BindCombo(ddlARVCombReg, theDT1, "Name", "ID");
        }

        /// <summary>
        /// Makes the table.
        /// </summary>
        /// <returns></returns>
        private DataTable MakeTable()
        {
            DataTable theDT = new DataTable();
            DataColumn[] theKey = new DataColumn[2];
            theDT.Columns.Add("DrugId", Type.GetType("System.Int32"));
            theDT.Columns.Add("DrugName", Type.GetType("System.String"));
            theDT.Columns.Add("GenericId", Type.GetType("System.Int32"));
            theDT.Columns.Add("GenericName", Type.GetType("System.String"));
            theKey[0] = theDT.Columns[0];
            theKey[1] = theDT.Columns[2];
            theDT.PrimaryKey = theKey;
            return theDT;
        }

        /// <summary>
        /// Makes the treatment month.
        /// </summary>
        /// <returns></returns>
        private DataTable MakeTreatmentMonth()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("Id", Type.GetType("System.Int32"));
            theDT.Columns.Add("Name", Type.GetType("System.String"));
            DataRow DR1 = theDT.NewRow();
            DR1[0] = 0;
            DR1[1] = "Select";
            theDT.Rows.Add(DR1);

            for (int i = 1; i <= 12; i++)
            {
                DataRow DR = theDT.NewRow();
                DR[0] = i;
                DR[1] = Convert.ToString(i);
                theDT.Rows.Add(DR);
            }

            return theDT;
        }

        /// <summary>
        /// Makes the treatment phase.
        /// </summary>
        /// <returns></returns>
        private DataTable MakeTreatmentPhase()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("Id", Type.GetType("System.Int32"));
            theDT.Columns.Add("Name", Type.GetType("System.String"));
            DataRow DR1 = theDT.NewRow();
            DR1[0] = 0;
            DR1[1] = "Select";
            theDT.Rows.Add(DR1);

            DataRow DR2 = theDT.NewRow();
            DR2[0] = 1;
            DR2[1] = "Intensive";
            theDT.Rows.Add(DR2);

            DataRow DR3 = theDT.NewRow();
            DR3[0] = 2;
            DR3[1] = "Continue";
            theDT.Rows.Add(DR3);

            return theDT;
        }

        /// <summary>
        /// Handles the AsyncPostBackError event of the PageScriptManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="AsyncPostBackErrorEventArgs" /> instance containing the event data.</param>
        private void PageScriptManager_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
        {
            string message = e.Exception.Message;
            Master.PageScriptManager.AsyncPostBackErrorMessage = message;
        }

        private DataSet PopulateTheDS(int patientId)
        {
            IPediatric PediatricManager = (IPediatric)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BPediatric, BusinessProcess.Pharmacy");
            DataSet theDrugDS = PediatricManager.GetPediatricFields(patientId);
            #region "FixDoseCombination"
            this.mainDataSet = new DataSet();
            mainDataSet.Tables.Add(theDrugDS.Tables[17].Copy());//--0--performance - gen abbr & only active drugs
            mainDataSet.Tables.Add(theDrugDS.Tables[1].Copy());//--1--
            mainDataSet.Tables.Add(theDrugDS.Tables[2].Copy());//--2--
            mainDataSet.Tables.Add(theDrugDS.Tables[3].Copy());//--3--
            mainDataSet.Tables.Add(theDrugDS.Tables[4].Copy());//--4--
            mainDataSet.Tables.Add(theDrugDS.Tables[5].Copy());//--5--
            mainDataSet.Tables.Add(theDrugDS.Tables[6].Copy());//--6--
            mainDataSet.Tables.Add(theDrugDS.Tables[15].Copy());//--7-- for inactive units in case of edit;
            mainDataSet.Tables.Add(theDrugDS.Tables[8].Copy());//--8--
            mainDataSet.Tables.Add(theDrugDS.Tables[9].Copy());//--9--
            mainDataSet.Tables.Add(theDrugDS.Tables[10].Copy());//--10--
            mainDataSet.Tables.Add(theDrugDS.Tables[11].Copy());//--11--
            mainDataSet.Tables.Add(theDrugDS.Tables[12].Copy());//--12-- stores all (both active/inactive) drugs
            mainDataSet.Tables.Add(theDrugDS.Tables[13].Copy());//--13--  rupesh 04-sep for OI and other medication - for custom frq list
            mainDataSet.Tables.Add(theDrugDS.Tables[16].Copy());//--14--  rupesh 19-sep-07 for active/inactive ARV Provider
            mainDataSet.Tables.Add(theDrugDS.Tables[21].Copy());//--15--  29Feb08 -- Non-ARTDate
            mainDataSet.Tables.Add(theDrugDS.Tables[22].Copy());//-period taken
            mainDataSet.Tables.Add(theDrugDS.Tables[23].Copy());//-TB Regimen
            mainDataSet.Tables.Add(theDrugDS.Tables[24].Copy());
            mainDataSet.Tables.Add(theDrugDS.Tables[25].Copy());
            mainDataSet.Tables.Add(theDrugDS.Tables[26].Copy());
            mainDataSet.Tables.Add(theDrugDS.Tables[27].Copy());
            mainDataSet.Tables.Add(theDrugDS.Tables[28].Copy());
            mainDataSet.Tables.Add(theDrugDS.Tables[29].Copy());
            #endregion "FixDoseCombination"
            ViewState["MasterData"] = mainDataSet;
            return theDrugDS;
        }

        /// <summary>
        /// Proes the phalaxis check.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        private bool ProPhalaxisCheck(DataTable dt)
        {
            bool blnProCheck = true;
            DataTable theDTDrug = ((DataSet)ViewState["MasterData"]).Tables[0];
            foreach (DataRow r in dt.Rows)
            {
                if (r["Prophylaxis"].ToString() == "999")
                {
                    blnProCheck = NonARTProPhylasxisCheck(theDTDrug, r["DrugId"].ToString());
                }
            }
            return blnProCheck;
        }

        // Create Custom Controls
        /// <summary>
        /// Puts the custom control.
        /// </summary>
        private void PutCustomControl()
        {
            ICustomFields CustomFields;
            CustomFieldClinical theCustomField = new CustomFieldClinical();
            try
            {
                CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields,BusinessProcess.Administration");
                DataSet theDS = CustomFields.GetCustomFieldListforAForm(Convert.ToInt32(ApplicationAccess.PaediatricPharmacy));
                if (theDS.Tables[0].Rows.Count != 0)
                {
                    theCustomField.CreateCustomControlsForms(pnlCustomList, theDS, "PPharm");
                    //ViewState["CustomFieldsDS"] = theDS;
                    //pnlCustomList.Visible = true;
                }
                //theCustomField.CreateCustomControlsForms(pnlCustomList, theDS, "PPharm");
                ViewState["CustomFieldsDS"] = theDS;
                pnlCustomList.Visible = true;
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
            finally
            {
                CustomFields = null;
            }
        }

        /// <summary>
        /// Quantities the dispensed.
        /// </summary>
        /// <returns></returns>
        private bool QuantityDispensed()
        {
            if (ddlDispensedBy.SelectedIndex == 0 || txtpharmReportedbyDate.Value.Trim() == "")//dispance by
            {
                IQCareMsgBox.Show("PharmacyDispensedDateby", this);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Handles the panel event of the Remove control.
        /// </summary>
        /// <param name="s">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Remove_panel(object s, EventArgs e)
        {
            LinkButton lnkremove = (LinkButton)s;
            //lnkremove.Attributes.Add("onclick", "return confirm('Are you sure you want to Remove this Drug?')");
            string lnkId = lnkremove.ID.ToString();
            int ipos = 0;
            int Drugid = 0;
            string pnlName = "";
            Control pnlremoving;
            Control pnlheading;
            if (lnkId.StartsWith("lnkrmv"))
            {
                ipos = Convert.ToInt32(lnkId.IndexOf("^"));
                if (ipos > 0)
                {
                    ipos = ipos + 1;
                    Drugid = Convert.ToInt32(lnkId.Substring(ipos, lnkId.Length - ipos));
                }
                ipos = Convert.ToInt32(lnkId.IndexOf("%"));
                if (ipos > 0)
                {
                    ipos = ipos + 1;
                    pnlName = lnkId.Substring(ipos, lnkId.IndexOf("^") - ipos);
                }
                #region "Additional ARV"
                if (pnlName.ToString() == "pnlPedia")
                {
                    if ((DataTable)Session["AddARV"] != null)
                    {
                        DataTable theDT = (DataTable)Session["AddARV"];
                        DataRow[] theDR = theDT.Select("DrugId = " + Drugid.ToString());

                        theDT.Rows.Remove(theDR[0]);
                        Session["AddARV"] = theDT;
                        pnlremoving = pnlPedia.FindControl("pnl_" + Drugid);
                        if (pnlremoving != null)
                        {
                            if (pnlremoving.GetType() == typeof(Panel))
                            {
                                pnlPedia.Controls.Remove(pnlremoving);
                            }
                        }
                        if (theDR[0] != null && theDT.Rows.Count == 0)
                        {
                            pnlheading = pnlPedia.FindControl("pnlARVDrug");
                            pnlPedia.Controls.Remove(pnlheading);
                            pnlheading = pnlPedia.FindControl("Header");
                            pnlPedia.Controls.Remove(pnlheading);
                            pnlARV.Visible = false;
                            if (ViewState["PharmacyFlag"].ToString() == "0" && trHIVsetFields.Visible == true)
                            {
                                txtpharmAppntDate.Value = "";
                                ddlAppntReason.SelectedIndex = 0;
                                trHIVsetFields.Visible = false;
                            }
                        }
                    }
                }
                #endregion  "Additional ARV"
                #region "Vaccines"
                if (pnlName.ToString() == "panelVaccine")
                {
                    if ((DataTable)Session["VaccineDrugs"] != null)
                    {
                        DataTable theDT = (DataTable)Session["VaccineDrugs"];
                        DataRow[] theDR = theDT.Select("DrugId = " + Drugid.ToString());

                        theDT.Rows.Remove(theDR[0]);
                        Session["VaccineDrugs"] = theDT;
                        pnlremoving = panelVaccine.FindControl("pnl" + Drugid + "^0");
                        if (pnlremoving != null)
                        {
                            if (pnlremoving.GetType() == typeof(Panel))
                            {
                                PnlOIARV.Controls.Remove(pnlremoving);
                            }
                        }
                        if (theDR[0] != null && theDT.Rows.Count == 0)
                        {
                            pnlheading = panelVaccine.FindControl("pnlVacDrug");
                            panelVaccine.Controls.Remove(pnlheading);
                            pnlheading = panelVaccine.FindControl("pnlHeaderVacDrug");
                            panelVaccine.Controls.Remove(pnlheading);
                            panelVaccine.Visible = false;
                        }
                    }
                }
                #endregion "Vaccines"
                #region "TB Drugs"
                if (pnlName.ToString() == "pnlOtherTBMedicaton")
                {
                    if ((DataTable)Session["AddTB"] != null)
                    {
                        DataTable theDT = (DataTable)Session["AddTB"];
                        DataRow[] theDR = theDT.Select("DrugId = " + Drugid.ToString());

                        theDT.Rows.Remove(theDR[0]);
                        Session["AddTB"] = theDT;
                        pnlremoving = pnlOtherTBMedicaton.FindControl("pnl" + Drugid + "^0");
                        if (pnlremoving != null)
                        {
                            if (pnlremoving.GetType() == typeof(Panel))
                            {
                                pnlOtherTBMedicaton.Controls.Remove(pnlremoving);
                            }
                        }
                        if (theDR[0] != null && theDT.Rows.Count == 0)
                        {
                            pnlheading = pnlOtherTBMedicaton.FindControl("pnlTBDrug");
                            pnlOtherTBMedicaton.Controls.Remove(pnlheading);
                            pnlheading = pnlOtherTBMedicaton.FindControl("pnlHeaderTBDrug");
                            pnlOtherTBMedicaton.Controls.Remove(pnlheading);
                            pnlTB.Visible = false;
                        }
                    }
                }
                #endregion "TB Drugs"
                #region "Additional OI and Other Medications"
                if (pnlName.ToString() == "PnlOtMed")
                {
                    if ((DataTable)Session["OtherDrugs"] != null)
                    {
                        DataTable theDT = (DataTable)Session["OtherDrugs"];
                        DataRow[] theDR = theDT.Select("DrugId = " + Drugid.ToString());

                        theDT.Rows.Remove(theDR[0]);
                        Session["OtherDrugs"] = theDT;
                        pnlremoving = PnlOtMed.FindControl("pnl" + Drugid + "^0");
                        if (pnlremoving != null)
                        {
                            if (pnlremoving.GetType() == typeof(Panel))
                            {
                                PnlOtMed.Controls.Remove(pnlremoving);
                            }
                        }
                        if (theDR[0] != null && theDT.Rows.Count == 0)
                        {
                            pnlheading = PnlOtMed.FindControl("pnlDrugPnlOtherMedication");
                            PnlOtMed.Controls.Remove(pnlheading);
                            pnlheading = PnlOtMed.FindControl("pnlHeaderOtherDrugPnlOtherMedication");
                            PnlOtMed.Controls.Remove(pnlheading);
                            pnlOther.Visible = false;
                        }
                    }
                }
                #endregion "Additional OI and Other Medications"
                #region "Additional OI"
                if (pnlName.ToString() == "PnlOIARV")
                {
                    if ((DataTable)Session["OIDrugs"] != null)
                    {
                        DataTable theDT = (DataTable)Session["OIDrugs"];
                        DataRow[] theDR = theDT.Select("DrugId = " + Drugid.ToString());

                        theDT.Rows.Remove(theDR[0]);
                        Session["OIDrugs"] = theDT;
                        pnlremoving = PnlOIARV.FindControl("pnl" + Drugid + "^0");
                        if (pnlremoving != null)
                        {
                            if (pnlremoving.GetType() == typeof(Panel))
                            {
                                PnlOIARV.Controls.Remove(pnlremoving);
                            }
                        }
                        if (theDR[0] != null && theDT.Rows.Count == 0)
                        {
                            pnlheading = PnlOIARV.FindControl("pnlDrugPnlOIARV");
                            PnlOIARV.Controls.Remove(pnlheading);
                            pnlheading = PnlOIARV.FindControl("pnlHeaderOtherDrugPnlOIARV");
                            PnlOIARV.Controls.Remove(pnlheading);
                            pnlOI.Visible = false;
                        }
                    }
                }
                #endregion
            }
        }

        /// <summary>
        /// Saves the cancel.
        /// </summary>
        /// <param name="phflag">The phflag.</param>
        private void SaveCancel(int phflag)
        {
            if ((phflag == 1) || (phflag == 2))
            {
                string strSession = Session["PtnID"].ToString();
                string strRequest = string.Empty;
                if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
                {
                    strRequest = "Add";
                }
                else
                {
                    strRequest = "Edit";
                }
                string strStatus = ViewState["Status"].ToString();
                string strPharmacyID = Session["PharmacyId"].ToString();
                string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n ";
                script += "var ans;\n ";
                if (phflag == 2)
                {
                    script += "ans=window.confirm('Drug Order saved successfully. You havent checked any Drug for Printing. Do you want to close?');\n";
                }
                else
                    script += "ans=window.confirm('Drug Order saved successfully. Do you want to close?');\n";
                script += "if (ans==true)\n ";

                script += "{\n ";

                if (Request.QueryString["opento"] == "ArtForm")
                {
                    if (Convert.ToInt32(Session["ArtEncounterPatientVisitId"]) > 0)
                    {
                        Session["PatientVisitId"] = Session["ArtEncounterPatientVisitId"];
                    }
                    else
                    {
                        Session["LabId"] = 0;
                        Session["PatientVisitId"] = 0;
                    }
                    script += "self.close();";
                }
                else
                {
                    script += "Redirect('" + strSession + "','" + strRequest + "','" + strStatus + "');\n ";
                }

                script += "}\n ";

                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);
            }
        }

        /// <summary>
        /// Selects the frequency.
        /// </summary>
        /// <param name="theContainer">The container.</param>
        private void SelectFrequency(Control theContainer)
        {
            string pnlName = theContainer.ID;
            string temp = "";
            decimal SingleDose = 0;
            int Frequency = 0;
            int DrgId = 0;
            int doseDrgId = 0;
            int totalDrgId = 0;
            int olddrgid = 0;
            int cpos;
            foreach (Control y in theContainer.Controls)
            {
                if (y.GetType() == typeof(Panel))
                    foreach (Control x in y.Controls)
                    {
                        if (x.GetType() == typeof(Panel))
                        {
                            SelectFrequency(x);
                        }
                        else
                        {
                            if (x.GetType() == typeof(System.Web.UI.WebControls.Label))
                            {
                                if (x.ID.StartsWith("drgNm"))
                                {
                                    cpos = Convert.ToInt32(x.ID.IndexOf("^"));
                                    if (cpos > 0)
                                    {
                                        DrgId = Convert.ToInt32(x.ID.Substring(5, cpos - 5));
                                    }
                                    else
                                    {
                                        DrgId = Convert.ToInt32(x.ID.Substring(5, x.ID.Length - 5));
                                    }
                                    //DrgId = Convert.ToInt32(x.ID.Substring(5));
                                    if (DrgId != olddrgid)
                                    {
                                        olddrgid = DrgId;
                                        SingleDose = 0;
                                        Frequency = 0;
                                    }
                                }
                            }
                            if (x.GetType() == typeof(DropDownList))
                            {
                                if (x.ID.StartsWith("drgFrequency"))
                                //if (x.FindControl( "drgFrequency" + Convert.ToString(DrgId)))
                                {
                                    cpos = Convert.ToInt32(x.ID.IndexOf("^"));
                                    if (cpos > 0)
                                    {
                                        DrgId = Convert.ToInt32(x.ID.Substring(12, cpos - 12));
                                    }
                                    else
                                    {
                                        DrgId = Convert.ToInt32(x.ID.Substring(12, x.ID.Length - 12));
                                    }
                                    //DrgId = Convert.ToInt32(x.ID.Substring(12));

                                    if (((DropDownList)x).SelectedItem != null)
                                        temp = ((DropDownList)x).SelectedItem.Text.ToString();

                                    if (temp == "OD")
                                    {
                                        Frequency = 1;
                                    }
                                    else if (temp == "BD" || temp == "bid")
                                    {
                                        Frequency = 2;
                                    }
                                    else if (temp == "1OD")
                                    {
                                        Frequency = 1;
                                    }
                                    else if (temp == "2OD")
                                    {
                                        Frequency = 2;
                                    }
                                    else if (temp == "1BD")
                                    {
                                        Frequency = 2;
                                    }
                                    else if (temp == "3OD" || temp == "TD")
                                    {
                                        Frequency = 3;
                                    }
                                    else if (temp == "qid" || temp == "QID" || temp == "QD")
                                    {
                                        Frequency = 4;
                                    }
                                    else if (temp == "Weekly")
                                    {
                                        Frequency = 7;
                                    }
                                }
                            }
                            if (x.GetType() == typeof(TextBox))
                            {
                                if (x.ID.StartsWith("drgDose"))
                                {
                                    cpos = Convert.ToInt32(x.ID.IndexOf("^"));
                                    if (cpos > 0)
                                    {
                                        doseDrgId = Convert.ToInt32(x.ID.Substring(7, cpos - 7));
                                    }
                                    else
                                    {
                                        doseDrgId = Convert.ToInt32(x.ID.Substring(7, x.ID.Length - 7));
                                    }
                                    //doseDrgId = Convert.ToInt32(x.ID.Substring(7));
                                    olddrgid = doseDrgId;
                                    if (DrgId == doseDrgId)
                                    {
                                        if (((TextBox)x).Text != "")
                                        {
                                            string sValue = ((TextBox)x).Text.ToString();
                                            int DecPos = sValue.IndexOf(".");
                                            int DecValue = 0;
                                            if (DecPos > 0)
                                            {
                                                DecValue = Convert.ToInt32(sValue.Substring(DecPos + 1, sValue.Length - (DecPos + 1)));
                                            }
                                            if (DecValue > 0)
                                            {
                                                SingleDose = Convert.ToDecimal(((TextBox)x).Text);
                                            }
                                            else
                                            {
                                                SingleDose = Convert.ToDecimal(((TextBox)x).Text);
                                            }
                                            //SingleDose = Convert.ToInt32(((TextBox)x).Text);
                                        }
                                    }
                                }
                                if (x.ID.StartsWith("drgTotalDose"))
                                {
                                    cpos = Convert.ToInt32(x.ID.IndexOf("^"));
                                    if (cpos > 0)
                                    {
                                        totalDrgId = Convert.ToInt32(x.ID.Substring(12, cpos - 12));
                                    }
                                    else
                                    {
                                        totalDrgId = Convert.ToInt32(x.ID.Substring(12, x.ID.Length - 12));
                                    }
                                    //totalDrgId = Convert.ToInt32(x.ID.Substring(12));
                                    olddrgid = doseDrgId;
                                    if (DrgId == totalDrgId)
                                    {
                                        ((TextBox)x).Text = Convert.ToString(Frequency * SingleDose);
                                        if (((TextBox)x).Text == "0")
                                        {
                                            ((TextBox)x).Text = "";
                                        }
                                    }

                                    //theDose = Convert.ToInt32(((TextBox)x).Text);
                                }
                            }
                            //}
                        }
                    }
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the theFixedDrugName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void theFixedDrugName_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void btnRandom_Click(object sender, EventArgs e)
        {
            if ((Convert.ToInt32(Session["PatientVisitId"]) > 0))
            {
                string theUrl = string.Format("{0}&PatientId={1}&ReportName={2}&sts={3}", "../Reports/frmReportViewer.aspx?name=Add", Session["PatientId"].ToString(), "PharmacyPrescription", "0");
                Response.Redirect(theUrl);
            }
        }

        /// <summary>
        /// Transfers the validation.
        /// </summary>
        /// <param name="PId">The p identifier.</param>
        /// <returns></returns>
        private bool TransferValidation(int PId)
        {
            IPatientTransfer IPTransferMgr;
            IPTransferMgr = (IPatientTransfer)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientTransfer, BusinessProcess.Clinical");
            if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            {
                DataSet DS = IPTransferMgr.GetLatestTransferDate(PId, 0);
                if (DS.Tables[0].Rows[0]["NotExistTransferDate"].ToString() != "0")
                {
                    if (Convert.ToDateTime(txtpharmOrderedbyDate.Value) < Convert.ToDateTime(DS.Tables[0].Rows[0]["TransferDate"]))
                    {
                        IQCareMsgBox.Show("TransferDate_4", this);
                        txtpharmOrderedbyDate.Focus();
                        return false;
                    }
                }
            }
            else if (Convert.ToInt32(Session["PatientVisitId"]) != 0)
            {
                int visitPK = Convert.ToInt32(Request.QueryString["visitid"]);
                DataSet DS = IPTransferMgr.GetLatestTransferDate(PId, visitPK);
                if (DS.Tables[0].Rows[0]["NotExistTransferDate"].ToString() != "0")
                {
                    if (DS.Tables[1].Rows[0]["PrevDate"].ToString() == "0" && DS.Tables[2].Rows[0]["LaterDate"].ToString() != "0")
                    {
                        if (Convert.ToDateTime(txtpharmOrderedbyDate.Value) > Convert.ToDateTime(DS.Tables[2].Rows[0]["LaterDate"]))
                        {
                            IQCareMsgBox.Show("TransferDate_5", this);
                            txtpharmOrderedbyDate.Focus();
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        //Generating full DML Statement
        /// <summary>
        /// Updates the custom fields values.
        /// </summary>
        private void UpdateCustomFieldsValues()
        {
            GenerateCustomFieldsValues(pnlCustomList);
            string sqlstr = string.Empty;
            int PatientId = Convert.ToInt32(Session["PatientId"]);

            Int32 PharmacyId = 0;
            DateTime OrderedbyDate = System.DateTime.Now;

            if (ViewState["PharmacyId"] != null)
                PharmacyId = Convert.ToInt32(ViewState["PharmacyId"]);
            if (txtpharmOrderedbyDate.Value.ToString() != "")
                OrderedbyDate = Convert.ToDateTime(txtpharmOrderedbyDate.Value.ToString());
            ICustomFields CustomFields;

            if (sbValues.ToString().Trim() != "")
            {
                if (ViewState["CustomFieldsData"] != null)
                {
                    sbValues = sbValues.Remove(0, 1);
                    sqlstr = "UPDATE dtl_CustomField_" + TableName.ToString().Replace("-", "_") + " SET ";
                    //sqlstr += sbValues.ToString() + " where Ptn_pk= " + PatientId.ToString() + " and LocationID=" + Application["AppLocationID"] + " and ptn_pharmacy_pk=" + PharmacyId.ToString();
                    sqlstr += sbValues.ToString() + " where Ptn_pk= " + PatientId.ToString() + " and ptn_pharmacy_pk=" + PharmacyId.ToString();
                }
                else
                {
                    sqlstr = "INSERT INTO dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "(ptn_pk,LocationID,ptn_pharmacy_pk,OrderedByDate " + sbParameter.ToString() + " )";
                    //sqlstr += " VALUES(" + PatientId.ToString() + "," + Application["AppLocationID"] + "," + PharmacyId + ",'" + OrderedbyDate + "'" + sbValues.ToString() + ")";
                    sqlstr += " VALUES(" + PatientId.ToString() + "," + Session["AppLocationId"] + "," + PharmacyId + ",'" + OrderedbyDate + "'" + sbValues.ToString() + ")";
                    ViewState["CustomFieldsData"] = 1;
                    //Session["AppLocationId"]
                }

                try
                {
                    CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
                    icount = CustomFields.SaveCustomFieldValues(sqlstr.ToString());
                    if (icount == -1)
                    {
                        return;
                    }
                }
                catch
                {
                }
                finally
                {
                    CustomFields = null;
                }
            }
            if (strmultiselect.ToString() != "")
            {
                string[] FieldValues = strmultiselect.Split(new char[] { '^' });
            }
        }

        //                    }
        /// <summary>
        /// Validates the fixed comb data.
        /// </summary>
        /// <param name="Cntrl">The CNTRL.</param>
        /// <returns></returns>
        private bool ValidateFixedCombData(Control Cntrl)
        {
            foreach (Control x in Cntrl.Controls)
            {
                if (x.GetType() == typeof(Panel))
                {
                    return ValidateFixedCombData(x);
                }
                else
                {
                    if (x.GetType() == typeof(DropDownList) && x.ID == "theFixedDrugName")
                    {
                        if (Convert.ToInt32(((DropDownList)x).SelectedValue) > 0)
                        {
                            if (CountFixedComb == 0)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        ///
        /// </summary>
        public class Drugs
        {
            /// <summary>
            /// The _avlqty
            /// </summary>
            protected int _avlqty;

            /// <summary>
            /// The _ drug identifier
            /// </summary>
            protected int _DrugId;

            /// <summary>
            /// The _drug name
            /// </summary>
            protected string _drugName;

            /// <summary>
            /// Gets or sets the avl qty.
            /// </summary>
            /// <value>
            /// The avl qty.
            /// </value>
            public int AvlQty
            {
                get { return _avlqty; }
                set { _avlqty = value; }
            }

            /// <summary>
            /// Gets or sets the drug identifier.
            /// </summary>
            /// <value>
            /// The drug identifier.
            /// </value>
            public int DrugId
            {
                get { return _DrugId; }
                set { _DrugId = value; }
            }

            /// <summary>
            /// Gets or sets the name of the drug.
            /// </summary>
            /// <value>
            /// The name of the drug.
            /// </value>
            public string DrugName
            {
                get { return _drugName; }
                set { _drugName = value; }
            }
        }
    }
}
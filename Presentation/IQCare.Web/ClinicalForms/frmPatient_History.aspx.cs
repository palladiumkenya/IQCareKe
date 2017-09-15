using System;
using System.Data;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;
using Interface.Clinical;
using IQCare.Web.UILogic;

namespace IQCare.Web.Clinical
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PatientHistory : BasePage
    {
        /// <summary>
        /// The p identifier
        /// </summary>
        public string PId, PtnSts, DQ;
  

        /// <summary>
        /// Forms the iq care.
        /// </summary>
        /// <param name="theDS">The ds.</param>
        private void FormIQCare(DataSet theDS)
        {
            //IPatientHome PatientManager;
            //PatientManager = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
            int tmpYear = 0;
            int tmpMonth = 0;
            TreeNode root = new TreeNode();
            TreeNode theMRoot = new TreeNode();
            bool flagyear = true;
           // int PtnPMTCTStatus = 0;
            int PtnARTStatus = 0;
            if (PtnSts == "0" || PtnSts == "")
            {
                if (Session["PtnPrgStatus"] != null)
                {
                    DataTable theStatusDT = (DataTable)Session["PtnPrgStatus"];
                    DataTable theCEntedStatusDT = (DataTable)Session["CEndedStatus"];
                    string PatientExitReason = string.Empty;
                    string PMTCTCareEnded = string.Empty;
                    string CareEnded = string.Empty;
                    if (theCEntedStatusDT.Rows.Count > 0)
                    {
                        PatientExitReason = Convert.ToString(theCEntedStatusDT.Rows[0]["PatientExitReason"]);
                        PMTCTCareEnded = Convert.ToString(theCEntedStatusDT.Rows[0]["PMTCTCareEnded"]);
                        CareEnded = Convert.ToString(theCEntedStatusDT.Rows[0]["CareEnded"]);
                    }
                    //if ((theStatusDT.Rows[0]["PMTCTStatus"].ToString() == "PMTCT Care Ended") || (PatientExitReason == "93" && PMTCTCareEnded == "1"))
                    //    PtnPMTCTStatus = 1;
                    //else
                    //    PtnPMTCTStatus = 0;
                    //if ((theStatusDT.Rows[0]["ART/PalliativeCare"].ToString() == "Care Ended") || (PatientExitReason == "93" && CareEnded == "1"))
                    //    PtnARTStatus = 1;
                    //else
                    //    PtnARTStatus = 0;
                }
            }
            else
            {
                //PtnPMTCTStatus = 1;
                PtnARTStatus = 1;
            }
            DataTable linkedForms = PatientService.GetModuleForms(LocationId, Convert.ToInt32(Session["TechnicalAreaId"]));
            foreach (DataRow theDR in theDS.Tables[1].Rows)
            {

                if (theDR["TranDate"] != DBNull.Value && theDR["TranDate"].ToString().Trim()!="" &&  ((DateTime)theDR["TranDate"]).Year != 1900)
                {
                    DQ = "";
                    if (tmpYear != ((DateTime)theDR["TranDate"]).Year)
                    {
                        root = new TreeNode();
                        root.Text = ((DateTime)theDR["TranDate"]).Year.ToString();
                        root.Value = "";
                        if (flagyear)
                        {
                            root.Expand();
                            flagyear = false;
                        }
                        else
                        {
                            root.Collapse();
                        }
                        TreeViewExisForm.Nodes.Add(root);
                        tmpYear = ((DateTime)theDR["TranDate"]).Year;
                        tmpMonth = 0;
                    }

                    if (tmpYear == ((DateTime)theDR["TranDate"]).Year && tmpMonth != ((DateTime)theDR["TranDate"]).Month)
                    {
                        theMRoot = new TreeNode();
                        theMRoot.Text = ((DateTime)theDR["TranDate"]).ToString("MMMM");
                        theMRoot.Value = "";
                        root.ChildNodes.Add(theMRoot);
                        tmpMonth = ((DateTime)theDR["TranDate"]).Month;
                    }

                    if (theDR["DataQuality"].ToString() == "1")
                    {
                        DQ = "Data Quality Done";

                    }

                    if (tmpYear == ((DateTime)theDR["TranDate"]).Year && tmpMonth == ((DateTime)theDR["TranDate"]).Month)
                    {
                       int _locationId = Convert.ToInt32(theDS.Tables[0].Rows[0]["LocationID"].ToString());
                        TreeNode theFrmRoot = new TreeNode();
                        theFrmRoot.Text = theDR["FormName"].ToString() + " ( " + ((DateTime)theDR["TranDate"]).ToString(Session["AppDateFormat"].ToString()) + " )";
                        string _formName = theDR["FormName"].ToString();
                        if (Convert.ToString(Session["TechnicalAreaId"]) == Convert.ToString(theDR["Module"]) || theDR["FormName"].ToString() == "Patient Registration")
                        {
                            if (DQ != "")
                            {
                                theFrmRoot.ImageUrl = "~/images/15px-Yes_check.svg.png";
                            }
                            else if (theDR["CAUTION"].ToString() == "1")
                                        {
                                            theFrmRoot.ImageUrl = "~/images/caution.png";
                                        }
                            else
                            {
                                theFrmRoot.ImageUrl = "~/Images/No_16x.ico";
                            }
                        }
                        else
                        {
                            if((_formName == "Pharmacy") || (_formName == "Laboratory") || (_formName == "Paediatric Pharmacy") || _formName.Contains("Service Request"))
                            {
                                if (Session["Paperless"].ToString() == "1")
                                {

                                    if ((_formName == "Pharmacy") || (_formName == "Laboratory") || (_formName == "Paediatric Pharmacy") || _formName.Contains("Service Request"))
                                    {
                                        if (theDR["CAUTION"].ToString() == "1")
                                        {
                                            theFrmRoot.ImageUrl = "~/images/caution.png";
                                        }
                                        else
                                            theFrmRoot.ImageUrl = "~/images/15px-Yes_check.svg.png";
                                    }

                                }
                                else
                                {
                                    if (DQ != "")
                                    {
                                        theFrmRoot.ImageUrl = "~/images/15px-Yes_check.svg.png";
                                    }
                                    else
                                    {
                                        theFrmRoot.ImageUrl = "~/Images/No_16x.ico";
                                    }
                                }
                            }
                            else
                            {
                                //find if form is linked to this module
                               
                                if (linkedForms != null && linkedForms.Select("FormName = '"+_formName+"'").Length > 0)
                                {
                                    if (DQ != "")
                                    {
                                        theFrmRoot.ImageUrl = "~/images/15px-Yes_check.svg.png";
                                    }
                                    else
                                    {
                                        theFrmRoot.ImageUrl = "~/Images/No_16x.ico";
                                    }
                                }
                                else
                                {
                                    theFrmRoot.ImageUrl = "~/Images/lock.jpg";
                                    theFrmRoot.ImageToolTip = "You are Not Authorized to Access this Functionality";
                                    theFrmRoot.SelectAction = TreeNodeSelectAction.None;
                                }
                            }
                        }
                        //else
                        //{
                        //    if (Convert.ToInt32(theDR["Module"]) > 2)
                        //    {
                        //        theFrmRoot.ImageUrl = "~/Images/lock.jpg";
                        //        theFrmRoot.ImageToolTip = "You are Not Authorized to Access this Functionality";
                        //        theFrmRoot.SelectAction = TreeNodeSelectAction.None;
                        //    }
                                
                        //    else if (Convert.ToString(Session["TechnicalAreaId"]) == Convert.ToString(theDR["Module"]) || (_formName == "Pharmacy") || 
                        //        (_formName == "Laboratory") || (_formName == "Paediatric Pharmacy") ||_formName.Contains("Service Request") )
                        //    {
                               
                        //    }
                        //    else
                        //    {
                        //        theFrmRoot.ImageUrl = "~/Images/lock.jpg";
                        //        theFrmRoot.ImageToolTip = "You are Not Authorized to Access this Functionality";
                        //        theFrmRoot.SelectAction = TreeNodeSelectAction.None;
                        //    }
                        //}
                        theFrmRoot.NavigateUrl = "";
                        theFrmRoot.Value = Convert.ToInt32(PId) + "%" + theDR["OrderNo"].ToString() + "%" + theDR["LocationID"].ToString() + "%" + PtnARTStatus + "%" + theDR["Module"].ToString() + "%" + theDR["FormName"].ToString();
                        theMRoot.ChildNodes.Add(theFrmRoot);
                    }
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
            //(Master.FindControl("lblRoot") as Label).Text = "Clinical Forms >>";
            //(Master.FindControl("lblMark") as Label).Visible = false;
            //(Master.FindControl("lblheader") as Label).Text = "Patient History";
            //(Master.FindControl("lblformname") as Label).Text = "Existing Forms";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Patient History";
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Existing Forms";

           

            #region "Refresh Patient Records"
            IPatientHome PManager;
            if (null == Session["PatientInformation"])
            {
                PManager = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
                System.Data.DataSet thePDS = PManager.GetPatientDetails(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["SystemId"]), Convert.ToInt32(Session["TechnicalAreaId"]));
                //System.Data.DataSet thePDS = PManager.GetPatientDetails(Convert.ToInt32(Request.QueryString["PatientId"]), Convert.ToInt32(Session["SystemId"]));

                Session["PatientInformation"] = thePDS.Tables[0];
            }
            #endregion

            IPatientHome PatientManager;
            PatientManager = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");

            try
            {
                (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = Convert.ToString(Session["PatientStatus"]);
                if (!IsPostBack)
                {
                    IFacilitySetup FacilityMaster = (IFacilitySetup)ObjectFactory.CreateInstance("BusinessProcess.Administration.BFacility, BusinessProcess.Administration");
                    //theFacilityDS = FacilityMaster.GetFacilityList(Convert.ToInt32(Session["SystemId"]), 1001);
                    //theFacilityDS = FacilityMaster.GetSystemBasedLabels(Convert.ToInt32(Session["SystemId"]), 1001, 0);
                    //ViewState["FacilityDS"] = theFacilityDS;
                    //SetPageLabels();
                    //******************************************************//
                    if (Session["PatientId"] == null || Convert.ToString(Session["PatientId"]) == "0")
                    {
                        Session["PatientId"] = Request.QueryString["PatientId"];  //remove it after session of patient set on find add when patient selected from grid.
                    }

                    PId = Convert.ToString(Session["PatientId"]); 
                    PtnSts = Convert.ToString(Session["PatientStatus"]); ;
                    if (Session["PatientId"] != null && Convert.ToInt32(Session["PatientId"]) != 0)
                    {
                        PId = Session["PatientId"].ToString();
                    }
                    if (Session["PatientStatus"] != null)
                    {
                        PtnSts = Session["PatientStatus"].ToString();
                    }
                    DataSet theDS = PatientManager.GetPatientHistory(Convert.ToInt32(PId));
                    ViewState["theCFDT"] = theDS.Tables[3].Copy();
                    FormIQCare(theDS);
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
                PatientManager = null;
            }


        }

        /// <summary>
        /// Handles the Click event of the btnExit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void btnExit_Click(object sender, EventArgs e)
        {
            string theUrl;
            string origin = String.Empty;
            if (Session["urlOrigin"] != null)
            {
                origin = Session["urlOrigin"].ToString();
            }
            
            ////theUrl = string.Format("{0}?PatientId={1}", "frmPatient_Home.aspx", Request.QueryString["PatientId"].ToString());
            if(origin == "greencard")
            {
                theUrl = string.Format("{0}", "~/CCC/Patient/PatientHome.aspx");
            }
            else
                theUrl = string.Format("{0}", "frmPatient_Home.aspx");
            Response.Redirect(theUrl);
        }

        /// <summary>
        /// Sets the page labels.
        /// </summary>
        private void SetPageLabels()
        {
            DataTable theDT = ((DataSet)ViewState["FacilityDS"]).Tables[1];
            if (theDT.Rows.Count > 0)
            {
                // lblFileRef.InnerHtml = theDT.Rows[0]["Label"].ToString();
            }
        }
        /// <summary>
        /// Handles the SelectedNodeChanged event of the TreeViewExisForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void TreeViewExisForm_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (TreeViewExisForm.SelectedNode.Value == "")
                return;

            string[] theName = TreeViewExisForm.SelectedNode.Text.Split('(');
            string[] theValue = TreeViewExisForm.SelectedNode.Value.Split('%');
            string url = "";
            string PgName;
            Session["PatientId"] = Convert.ToInt32(theValue[0]);
            Session["PatientVisitId"] = Convert.ToInt32(theValue[1]);
            Session["ServiceLocationId"] = Convert.ToInt32(theValue[2]);
            Session["PatientStatus"] = Convert.ToInt32(theValue[3]);
            Session["CEModule"] = theValue[4].ToString();

            Session["Redirect"] = "1";
            string strFormName = theName[0];
            if (strFormName.Contains("Service Request"))
            {
                Guid g = Guid.NewGuid();
                url = string.Format("{0}", "~/./ClinicalService/ServiceSubmitResult.aspx?key=" + g.ToString());
                Session["PatientClientId"] = Convert.ToInt32(theValue[0]);
                Response.Redirect(url);
            }
            else
            {
                switch (theName[0].Trim())
                {
                    case "Patient Registration":
                        //url = string.Format("{0}", "~/frmPatientRegistration.aspx");
                        url = string.Format("{0}", "~/Patient/Registration.aspx");
                        Response.Redirect(url);
                        break;

                    case "HIV-Enrollment":
                        if (Convert.ToInt32(Session["TechnicalAreaId"]) == 2)
                        {
                            if (Session["SystemId"].ToString() == "1")
                            { PgName = "frmClinical_Enrolment.aspx"; }
                            else { PgName = "frmClinical_PatientRegistrationCTC.aspx"; }
                            url = string.Format("{0}", "" + PgName + "");
                            Response.Redirect(url);
                        }
                        break;

                    case "Initial Evaluation":
                        if (Convert.ToInt32(Session["TechnicalAreaId"]) == 2)
                        {
                            url = string.Format("{0}", "./frmClinical_InitialEvaluation.aspx");
                            Response.Redirect(url);
                        }
                        break;


                    case "Prior ART/HIV Care":
                        if (Convert.ToInt32(Session["TechnicalAreaId"]) == 202)
                        {
                            url = string.Format("{0}", "./frm_PriorArt_HivCare.aspx");
                            Response.Redirect(url);
                        }
                        break;

                    case "ART Care":
                        if (Convert.ToInt32(Session["TechnicalAreaId"]) == 202)
                        {
                            url = string.Format("{0}", "./frmClinical_ARTCare.aspx");
                            Response.Redirect(url);
                        }
                        break;

                    //********************//
                    //john - start
                    case "ART Therapy":
                        if (Convert.ToInt32(Session["TechnicalAreaId"]) == 203)
                        {
                            url = string.Format("{0}", "./frmClinical_ARVTherapy.aspx");
                            Response.Redirect(url);
                        }
                        break;
                    //john - end

                    case "ART History":
                        if (Convert.ToInt32(Session["TechnicalAreaId"]) == 203)
                        {
                            url = string.Format("{0}", "./frmClinical_ARTHistory.aspx");
                            Response.Redirect(url);
                        }
                        break;

                    case "Pharmacy":
                        //if (Convert.ToInt32(Session["TechnicalAreaId"]) == 2)
                        //{
                        if (Session["SystemId"].ToString() == "1")
                        {
                            url = string.Format("{0}", "~/./Pharmacy/frmPharmacyForm.aspx");
                            Response.Redirect(url);
                        }
                        else
                        {
                            url = string.Format("{0}", "~/./Pharmacy/frmPharmacy_CTC.aspx");
                            Response.Redirect(url);
                        }
                        //}
                        break;
                    case "Paediatric Pharmacy":
                        //if (Convert.ToInt32(Session["TechnicalAreaId"]) == 2)
                        //{
                        url = string.Format("{0}", "~/./Pharmacy/frmPharmacyForm.aspx");
                        Response.Redirect(url);
                        //}
                        break;
                    case "ART Follow-Up":
                        //if (Convert.ToInt32(Session["TechnicalAreaId"]) == 2)
                        //{
                        //url = string.Format("{0}&patientid={1}&visitid={2}&locationid={3}&sts={4}", "./frmClinical_ARTFollowup.aspx?name=Edit", Convert.ToInt32(PId), theDR["OrderNo"].ToString(), theDR["LocationID"].ToString(), PtnARTStatus);
                        url = string.Format("{0}", "./frmClinical_ARTFollowup.aspx");
                        //theFrmRoot.NavigateUrl = url;
                        Response.Redirect(url);
                        //}
                        break;
                    case "HIV Care/ART Encounter":
                        url = string.Format("{0}", "./frmClinical_HIVCareARTCardEncounter.aspx");
                        Response.Redirect(url);
                        break;
                    case "Initial and Follow up Visits":
                        url = string.Format("{0}", "./frmClinical_InitialFollowupVisit.aspx");
                        Response.Redirect(url);
                        break;

                    case "Laboratory":
                        // if (Session["Paperless"].ToString() == "0")
                        url = string.Format("{0}", "~/./Laboratory/LabResultPage.aspx");
                        //else
                        //{
                        //    // url = string.Format("{0}", "~/./Laboratory/frmDynamicLab.aspx");  
                        //    url = string.Format("{0}", "~/./Laboratory/LabRequestForm.aspx?mode=results");
                        //}
                        Response.Redirect(url);

                        break;
                    case "X-Ray":
                        if (Session["Paperless"].ToString() == "1")
                        {
                            url = string.Format("{0}", "~/./Laboratory/frmDynamicLab.aspx?mode=xray");
                        }
                        else
                        {
                            url = string.Format("{0}", "~/./Laboratory/frmLabOrder.aspx");
                        }
                        Response.Redirect(url);


                        break;
                    case "Home Visit":
                        url = string.Format("{0}", "~/./Scheduler/frmScheduler_HomeVisit.aspx");
                        Response.Redirect(url);
                        break;
                    case "Non-ART Follow-Up":
                        if (Convert.ToInt32(Session["TechnicalAreaId"]) == 2)
                        {
                            url = string.Format("{0}", "./frmClinical_NonARTFollowUp.aspx");
                            Response.Redirect(url);
                        }
                        break;
                    case "Care Tracking":
                        url = string.Format("{0}", "~/./Scheduler/frmScheduler_ContactCareTracking.aspx");
                        Response.Redirect(url);
                        // theFrmRoot.NavigateUrl = url;
                        break;

                    //default: break;

                }
            }
            foreach (DataRow DRCustomFrm in ((DataTable)ViewState["theCFDT"]).Rows)
            {
                if (DRCustomFrm["FeatureName"].ToString() == theName[0].Trim())
                {
                    DataView theDV = new DataView((DataTable)ViewState["theCFDT"]);
                    theDV.RowFilter = "FeatureName='" + theName[0].Trim() + "'";
                    DataTable dtview = theDV.ToTable();
                    Session["FeatureID"] = Convert.ToString(dtview.Rows[0]["FeatureID"]);
                    AuthenticationManager Authentication = new AuthenticationManager();
                    if (Authentication.HasFunctionRight(Convert.ToInt32(dtview.Rows[0]["FeatureID"]), FunctionAccess.View, (DataTable)Session["UserRight"]) == true)
                    {
                        //url = string.Format("{0}", "./frmClinical_CustomForm.aspx");
                        url = string.Format("{0}", "~/ClinicalForms/CustomForm.aspx");
                        Response.Redirect(url);
                    }
                    else
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["MessageText"] = "You are Not Authorized to Access this Form.";
                        IQCareMsgBox.Show("#C1", theBuilder, this);
                    }

                }
            }

            TreeViewExisForm.SelectedNode.NavigateUrl = url;

        }
    }
}
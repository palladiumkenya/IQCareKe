using System;
using System.Data;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Clinical;
namespace IQCare.Web.Clinical
{
    /// <summary>
    /// 
    /// </summary>
    public partial class DeleteForm : BasePage
    {

        /// <summary>
        /// The p identifier
        /// </summary>
        public string PId, PtnSts, DQ;
        /// <summary>
        /// The location identifier
        /// </summary>
        int LocationID;
        /// <summary>
        /// Handles the Init event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Init(object sender, EventArgs e)
        {
            //RTyagi..04April.07
            /***************** Check For User Rights ****************/
            AuthenticationManager Authentication = new AuthenticationManager();
            if (Request.QueryString["name"] != null)
            {
                if (Request.QueryString["name"] == "Delete")
                {
                    if (Authentication.HasFunctionRight(ApplicationAccess.DeleteForm, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                    {
                        //int PatientID = Convert.ToInt32(Request.QueryString["PatientId"]);
                        int PatientID = Convert.ToInt32(Session["PatientId"]);

                        string theUrl = "";
                        theUrl = string.Format("{0}", "frmPatient_History.aspx");
                        Response.Redirect(theUrl);
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
                (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Delete Forms";
                (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Delete Forms";

                ////if (Request.QueryString["sts"] != null)
                if (Session["PatientStatus"] != null)
                {
                    (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = Session["PatientStatus"].ToString();
                    PtnSts = Session["PatientStatus"].ToString();
                }

                //////if (Request.QueryString["PatientId"] != null)
                if ((Convert.ToInt32(Session["PatientId"])) >= 1)
                {
                    //*****Draw treestructure with all the patient existing forms(Excluding Initial Evaluation and Enrollment form)
                    GetAllPatientForms();
                }

            }
            Form.EnableViewState = true;
            //BindHeader();
        }

        /// <summary>
        /// Handles the SelectedNodeChanged event of the TreeViewExistingForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void TreeViewExistingForm_SelectedNodeChanged(object sender, EventArgs e)
        {
             if (TreeViewExisForm.SelectedNode.Value == "")
                return;

            string[] theName = TreeViewExisForm.SelectedNode.Text.Split('(');
            string[] theValue = TreeViewExisForm.SelectedNode.Value.Split('%');
            string url = "";
          
            Session["PatientId"] = Convert.ToInt32(theValue[0]);
            Session["PatientVisitId"] = Convert.ToInt32(theValue[1]);
            Session["ServiceLocationId"] = Convert.ToInt32(theValue[2]);
            Session["PatientStatus"] = Convert.ToInt32(theValue[3]);
            Session["CEModule"] = theValue[4].ToString();

            Session["Redirect"] = "1";

            switch (theName[0].Trim())
            {
                case "Patient Registration":
                    url = string.Format("{0}", "~/frmPatientRegistration.aspx?name=Delete");
                    Response.Redirect(url);
                    break;
                case "HIV Care/ART Encounter":

                    if (Convert.ToInt32(Session["TechnicalAreaId"]) == 202)
                    {
                        url = string.Format("{0}", "./frmClinical_HIVCareARTCardEncounter.aspx?name=Delete");
                        Response.Redirect(url);
                    }
                    break;

                case "Prior ART/HIV Care":
                    if (Convert.ToInt32(Session["TechnicalAreaId"]) == 202)
                    {
                        url = string.Format("{0}", "./frm_PriorArt_HivCare.aspx?name=Delete");
                        Response.Redirect(url);
                    }
                    break;
                case "Initial Evaluation":
                    if (Convert.ToInt32(Session["TechnicalAreaId"]) == 2)
                    {
                        url = string.Format("{0}", "./frmClinical_InitialEvaluation.aspx?name=Delete");
                        Response.Redirect(url);
                    }
                    break;
                case "ART Care":
                    if (Convert.ToInt32(Session["TechnicalAreaId"]) == 202)
                    {
                        url = string.Format("{0}", "./frmClinical_ARTCare.aspx?name=Delete");
                        Response.Redirect(url);
                    }
                    break;


                case "ART Therapy":
                    if (Convert.ToInt32(Session["TechnicalAreaId"]) == 203)
                    {
                        url = string.Format("{0}", "./frmClinical_ARVTherapy.aspx?name=Delete");
                        Response.Redirect(url);
                    }
                    break;

                case "ART History":
                    if (Convert.ToInt32(Session["TechnicalAreaId"]) == 203)
                    {
                        url = string.Format("{0}", "./frmClinical_ARTHistory.aspx?name=Delete");
                        Response.Redirect(url);
                    }
                    break;

                case "Pharmacy":
                    if (Session["SystemId"].ToString() == "1")
                    {
                        url = string.Format("{0}", "~/./Pharmacy/frmPharmacyForm.aspx?name=Delete");
                        Response.Redirect(url);
                    }
                    else
                    {
                        url = string.Format("{0}", "~/./Pharmacy/frmPharmacy_CTC.aspx?name=Delete");
                        Response.Redirect(url);
                    }

                    break;
                case "Paediatric Pharmacy":
                    //if (Convert.ToInt32(Session["TechnicalAreaId"]) == 2)
                    //{
                    url = string.Format("{0}", "~/./Pharmacy/frmPharmacyForm.aspx?name=Delete");
                    Response.Redirect(url);
                    //}
                    break;

                case "ART Follow-Up":
                    url = string.Format("{0}", "./frmClinical_ARTFollowup.aspx?name=Delete");
                    Response.Redirect(url);
                    break;

                case "Initial and Follow up Visits":
                    url = string.Format("{0}", "./frmClinical_InitialFollowupVisit.aspx?name=Delete");
                    Response.Redirect(url);
                    break;

                case "Laboratory":
                    url = string.Format("{0}", "~/./Laboratory/frmLabOrder.aspx?name=Delete");
                    Response.Redirect(url);
                    break;

                case "Home Visit":
                    url = string.Format("{0}", "~/./Scheduler/frmScheduler_HomeVisit.aspx?name=Delete");
                    Response.Redirect(url);
                    break;
                case "Non-ART Follow-Up":
                    if (Convert.ToInt32(Session["TechnicalAreaId"]) == 2)
                    {
                        url = string.Format("{0}", "./frmClinical_NonARTFollowUp.aspx?name=Delete");
                        Response.Redirect(url);
                    }
                    break;
                case "Care Tracking":
                    url = string.Format("{0}", "~/./Scheduler/frmScheduler_ContactCareTracking.aspx?name=Delete");
                    Response.Redirect(url);
                    break;

                //default: break;

            }

            foreach (DataRow DRCustomFrm in ((DataTable)ViewState["theCFDT"]).Rows)
            {
                if (DRCustomFrm["FeatureName"].ToString() == theName[0].Trim())
                {
                    DataView theDV = new DataView((DataTable)ViewState["theCFDT"]);
                    theDV.RowFilter = "FeatureName='" + theName[0].Trim() + "'";
                    DataTable dtview = theDV.ToTable();
                    Session["FeatureID"] = Convert.ToString(dtview.Rows[0]["FeatureID"]);
                    //url = string.Format("{0}", "./frmClinical_CustomForm.aspx?name=Delete");
                    //Response.Redirect(url);
                    AuthenticationManager Authentication = new AuthenticationManager();
                    if (Authentication.HasFunctionRight(Convert.ToInt32(dtview.Rows[0]["FeatureID"]), FunctionAccess.Delete, (DataTable)Session["UserRight"]) == true)
                    {
                        url = string.Format("{0}", "./frmClinical_CustomForm.aspx?name=Delete");
                        Response.Redirect(url);
                    }
                    else
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["MessageText"] = "You are Not Authorized to Access this Form.";
                        IQCareMsgBox.Show("#C1", theBuilder, this);
                    }
                    //url = string.Format("{0}&patientid={1}&visitid={2}&locationid={3}&FormID={4}&sts={5}", "./frmClinical_CustomForm.aspx?name=Edit", Convert.ToInt32(PId), theDR["OrderNo"].ToString(), theDR["LocationID"].ToString(), DRCustomFrm["FeatureID"].ToString(), PtnPMTCTStatus);
                    //theFrmRoot.NavigateUrl = url;
                }
            }

            TreeViewExisForm.SelectedNode.NavigateUrl = url;
        }
        /// <summary>
        /// Handles the Click event of the btnBack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnBack_Click(object sender, EventArgs e)
        {
            string theUrl;
            //theUrl = string.Format("{0}?PatientId={1}", "frmPatient_Home.aspx", Request.QueryString["PatientId"].ToString());
            theUrl = string.Format("{0}", "frmPatient_Home.aspx");
            Response.Redirect(theUrl);
        }

        #region "User Defined Function"
        /// <summary>
        /// Gets all patient forms.
        /// </summary>
        private void GetAllPatientForms()
        {
           
            int PtnARTStatus = 0;

            int phar = Convert.ToInt32(Session["PatientVisitId"]);

            int tmpYear = 0;
            int tmpMonth = 0;
            TreeNode root = new TreeNode();
            TreeNode theMRoot = new TreeNode();
            bool flagyear = true;
            if (PtnSts == "0")
            {
                if (Session["PtnPrgStatus"] != null)
                {
                    DataTable theStatusDT = (DataTable)Session["PtnPrgStatus"];
                   
                    if (theStatusDT.Rows[0]["ART/PalliativeCare"].ToString() == "Care Ended")
                        PtnARTStatus = 1;
                    else
                        PtnARTStatus = 0;
                }
            }

            IDeleteForm FormManager;
            //IPatientHome PatientManager;
            try
            {
                //PId = Request.QueryString["PatientId"].ToString();
                PId = Session["PatientId"].ToString();

                FormManager = (IDeleteForm)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BDeleteForm, BusinessProcess.Clinical");
                DataSet theDS = FormManager.GetPatientForms(Convert.ToInt32(PId));
                //ViewState["theCFDT"] = theDS.Tables[2];
                ViewState["theCFDT"] = theDS.Tables[3].Copy();

                DataView dv = theDS.Tables[1].DefaultView;

                dv.Sort = "FormName";
                DataTable dt = dv.ToTable();

                LocationID = Convert.ToInt32(theDS.Tables[0].Rows[0]["LocationID"].ToString());                      

                foreach (DataRow theDR in theDS.Tables[1].Rows)
                {
                    if ((theDR["FormName"].ToString() != "Patient Registration") && (theDR["FormName"].ToString() != "HIV-Enrollment") && (theDR["FormName"].ToString() != "Initial Evaluation"))
                    {
                        if (((DateTime)theDR["TranDate"]).Year != 1900)
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
                                this.LocationID = Convert.ToInt32(theDS.Tables[0].Rows[0]["LocationID"].ToString());
                                TreeNode theFrmRoot = new TreeNode();
                                //theFrmRoot.NavigateUrl = "";
                                theFrmRoot.Text = theDR["FormName"].ToString() + " ( " + ((DateTime)theDR["TranDate"]).ToString(Session["AppDateFormat"].ToString()) + " )";
                                if (Convert.ToString(Session["TechnicalAreaId"]) == Convert.ToString(theDR["Module"]) || theDR["FormName"].ToString() == "Patient Registration")
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
                                    if (Convert.ToInt32(theDR["Module"]) > 2)
                                    {
                                        theFrmRoot.ImageUrl = "~/Images/lock.jpg";
                                        theFrmRoot.ImageToolTip = "You are Not Authorized to Access this Functionality";
                                        theFrmRoot.SelectAction = TreeNodeSelectAction.None;


                                    }
                                    else if (Convert.ToString(Session["TechnicalAreaId"]) == Convert.ToString(theDR["Module"]) || (theDR["FormName"].ToString() == "Paediatric Pharmacy"))
                                    {
                                        if (Session["Paperless"].ToString() == "1")
                                        {
                                            if (theDR["CAUTION"].ToString() == "1")
                                            {
                                                theFrmRoot.ImageUrl = "~/images/caution.png";
                                            }

                                        }
                                        else
                                        {
                                            if (DQ != "")
                                            {
                                                //theFrmRoot.NavigateUrl = "~/./Pharmacy/frmPharmacy_Paediatric.aspx?name=Delete";
                                                theFrmRoot.ImageUrl = "~/images/15px-Yes_check.svg.png";
                                            }
                                            else
                                            {
                                                theFrmRoot.ImageUrl = "~/Images/No_16x.ico";
                                            }
                                        }

                                    }

                                    else if (Convert.ToString(Session["TechnicalAreaId"]) == Convert.ToString(theDR["Module"]) || (theDR["FormName"].ToString() == "Laboratory"))
                                    {

                                        if ((theDR["FormName"].ToString() == "Laboratory") && (Convert.ToString(Session["TechnicalAreaId"]) != "0") && (Convert.ToInt32(Session["TechnicalAreaId"]) > 2))
                                        {
                                            theFrmRoot.ImageUrl = "~/Images/lock.jpg";
                                            theFrmRoot.ImageToolTip = "You are Not Authorized to Access this Functionality";
                                            theFrmRoot.SelectAction = TreeNodeSelectAction.None;
                                        }
                                        else
                                        {
                                            if (Session["Paperless"].ToString() == "1")
                                            {
                                                if (theDR["CAUTION"].ToString() == "1")
                                                {
                                                    theFrmRoot.ImageUrl = "~/images/caution.png";
                                                }
                                                else
                                                    theFrmRoot.ImageUrl = "~/images/15px-Yes_check.svg.png";

                                            }
                                            else
                                            {
                                                if (DQ != "")
                                                {

                                                    //theFrmRoot.NavigateUrl = "~/./Laboratory/frmLabOrder.aspx?name=Delete";
                                                    theFrmRoot.ImageUrl = "~/images/15px-Yes_check.svg.png";
                                                }
                                                else
                                                {
                                                    theFrmRoot.ImageUrl = "~/Images/No_16x.ico";
                                                }
                                            }
                                        }
                                    }

                                    else if (Convert.ToString(Session["TechnicalAreaId"]) == Convert.ToString(theDR["Module"]) || (theDR["FormName"].ToString() == "Pharmacy"))
                                    {

                                        if ((theDR["FormName"].ToString() == "Pharmacy") && (Convert.ToInt32(Session["TechnicalAreaId"]) > 2))
                                        {
                                            theFrmRoot.ImageUrl = "~/Images/lock.jpg";
                                            theFrmRoot.ImageToolTip = "You are Not Authorized to Access this Functionality";
                                            theFrmRoot.SelectAction = TreeNodeSelectAction.None;
                                        }
                                        else
                                        {
                                            if ((Convert.ToString(theDR["ID"]) == "222") && (Convert.ToString(Session["TechnicalAreaId"]) == "1"))
                                            {
                                                theFrmRoot.ImageUrl = "~/Images/lock.jpg";
                                                theFrmRoot.ImageToolTip = "You are Not Authorized to Access this Functionality";
                                                theFrmRoot.SelectAction = TreeNodeSelectAction.None;

                                            }
                                            else if ((Convert.ToString(theDR["ID"]) == "223") && (Convert.ToString(Session["TechnicalAreaId"]) == "2"))
                                            {
                                                theFrmRoot.ImageUrl = "~/Images/lock.jpg";
                                                theFrmRoot.ImageToolTip = "You are Not Authorized to Access this Functionality";
                                                theFrmRoot.SelectAction = TreeNodeSelectAction.None;

                                            }
                                            else
                                            {
                                                if (Session["Paperless"].ToString() == "1")
                                                {
                                                    if (theDR["CAUTION"].ToString() == "1")
                                                    {
                                                        theFrmRoot.ImageUrl = "~/images/caution.png";
                                                    }
                                                    else
                                                        theFrmRoot.ImageUrl = "~/images/15px-Yes_check.svg.png";

                                                }
                                                else
                                                {
                                                    if (DQ != "")
                                                    {
                                                        //theFrmRoot.NavigateUrl = "~/./Pharmacy/frmPharmacy_Adult.aspx?name=Delete";
                                                        theFrmRoot.ImageUrl = "~/images/15px-Yes_check.svg.png";
                                                    }
                                                    else
                                                    {
                                                        theFrmRoot.ImageUrl = "~/Images/No_16x.ico";
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    else if (Convert.ToString(Session["TechnicalAreaId"]) == Convert.ToString(theDR["Module"]) || (theDR["FormName"].ToString() == "ART Follow-Up"))
                                    {
                                        if (Convert.ToString(Session["TechnicalAreaId"]) == "2")
                                        {
                                            if (DQ != "")
                                            {

                                                //theFrmRoot.NavigateUrl = "~/./frmClinical_ARTFollowup.aspx?name=Delete";
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
                                    else if (Convert.ToString(Session["TechnicalAreaId"]) == Convert.ToString(theDR["Module"]) || (theDR["FormName"].ToString() == "Home Visit"))
                                    {

                                        if (Convert.ToString(Session["TechnicalAreaId"]) == "2")
                                        {

                                            if (DQ != "")
                                            {

                                                //theFrmRoot.NavigateUrl = "~/./Scheduler/frmScheduler_HomeVisit.aspx?name=Delete";
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

                                    else if (Convert.ToString(Session["TechnicalAreaId"]) == Convert.ToString(theDR["Module"]) || (theDR["FormName"].ToString() == "Non-ART Follow-Up"))
                                    {
                                        if (Convert.ToString(Session["TechnicalAreaId"]) == "2")
                                        {

                                            if (DQ != "")
                                            {

                                                //theFrmRoot.NavigateUrl = "~/./Pharmacy/frmPharmacy_Paediatric.aspx?name=Delete";
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

                                    else if (Convert.ToString(Session["TechnicalAreaId"]) == Convert.ToString(theDR["Module"]) || (theDR["FormName"].ToString() == "Care Tracking"))
                                    {

                                        if (Convert.ToString(Session["TechnicalAreaId"]) == "2")
                                        {

                                            if (DQ != "")
                                            {

                                                //theFrmRoot.NavigateUrl = "~/./Scheduler/frmScheduler_ContactCareTrackingnew.aspx?name=Delete";
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

                                    else if (Convert.ToString(Session["TechnicalAreaId"]) == Convert.ToString(theDR["Module"]) || (theDR["FormName"].ToString() == "Paediatric Pharmacy"))
                                    {
                                        if (Session["Paperless"].ToString() == "1")
                                        {
                                            if (theDR["CAUTION"].ToString() == "1")
                                            {
                                                theFrmRoot.ImageUrl = "~/images/caution.png";
                                            }
                                            else
                                                theFrmRoot.ImageUrl = "~/images/15px-Yes_check.svg.png";

                                        }
                                        else
                                        {
                                            if (DQ != "")
                                            {

                                                //theFrmRoot.NavigateUrl = "~/./Pharmacy/frmPharmacy_Paediatric.aspx?name=Delete";
                                                theFrmRoot.ImageUrl = "~/images/15px-Yes_check.svg.png";
                                            }
                                            else
                                            {
                                                theFrmRoot.ImageUrl = "~/Images/No_16x.ico";
                                            }
                                        }

                                    }

                                    else if (Convert.ToString(Session["TechnicalAreaId"]) == Convert.ToString(theDR["Module"]) || (theDR["FormName"].ToString() == "Initial Evaluation"))
                                    {
                                        if (Convert.ToString(Session["TechnicalAreaId"]) == "2")
                                        {

                                            if (DQ != "")
                                            {
                                                //theFrmRoot.NavigateUrl = "~/./frmClinical_InitialEvaluation.aspx?name=Edit";
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

                                    else
                                    {
                                        theFrmRoot.ImageUrl = "~/Images/lock.jpg";
                                        theFrmRoot.ImageToolTip = "You are Not Authorized to Access this Functionality";
                                        theFrmRoot.SelectAction = TreeNodeSelectAction.None;
                                    }
                                }
                                theFrmRoot.NavigateUrl = "";
                                theFrmRoot.Value = Convert.ToInt32(PId) + "%" + theDR["OrderNo"].ToString() + "%" + theDR["LocationID"].ToString() + "%" + PtnARTStatus + "%" + theDR["Module"].ToString() + "%" + theDR["FormName"].ToString();
                                theMRoot.ChildNodes.Add(theFrmRoot);
                            }
                        }




                    }


                }







                // Link for Custom/Dynamic Forms
                //foreach (DataRow DRCustomFrm in theDS.Tables[2].Rows)
                //{
                //    if (DRCustomFrm["FeatureName"].ToString() == theDR["FormName"].ToString())
                //    {
                //        url = string.Format("{0}&patientid={1}&visitid={2}&locationid={3}&FormID={4}&sts={5}", "./frmClinical_CustomForm.aspx?name=Delete", Convert.ToInt32(PId), theDR["OrderNo"].ToString(), theDR["LocationID"].ToString(), DRCustomFrm["FeatureID"].ToString(), PtnPMTCTStatus);
                //        theFrmRoot.NavigateUrl = url;
                //    }
                //}



                /********* Redirct to selected form ************/
                //switch (theDR["FormName"].ToString())
                //{
                //    //case "Enrollment": url = string.Format("{0}&patientid={1}&visitid={2}&locationid={3}&sts={4}", "./frmClinical_Enrolment.aspx?name=Edit", Convert.ToInt32(PId), theDR["OrderNo"].ToString(), LocationID, PtnSts.ToString());
                //    //    theFrmRoot.NavigateUrl = url;
                //    //    break;
                //    //case "Initial Evaluation": url = string.Format("{0}&patientid={1}&visitid={2}&locationid={3}&sts={4}", "./frmClinical_InitialEvaluation.aspx?name=Edit", Convert.ToInt32(PId), theDR["OrderNo"].ToString(), LocationID, PtnSts.ToString());
                //    //    theFrmRoot.NavigateUrl = url;
                //    //    break; 
                //    case "ART Follow-Up": url = string.Format("{0}", "./frmClinical_ARTFollowup.aspx?name=Delete");
                //    //case "ART Follow-Up": url = string.Format("{0}&patientid={1}&visitid={2}&sts={3}", "./frmClinical_ARTFollowup.aspx?name=Delete", Convert.ToInt32(PId), theDR["OrderNo"].ToString(), PtnSts.ToString());
                //        theFrmRoot.NavigateUrl = url;
                //        break;
                //    case "Pharmacy":
                //        if (Session["SystemId"].ToString() == "2")
                //        {
                //            url = string.Format("{0}&PatientId={1}&PharmacyId={2}&visitid={3}&sts={4}&locationid={5}", "~/./Pharmacy/frmPharmacy_CTC.aspx?name=Delete", Convert.ToInt32(PId), theDR["PharmacyNo"].ToString(), theDR["OrderNo"].ToString(), PtnSts.ToString(), theDR["LocationID"].ToString());
                //        }
                //        else
                //        {
                //            url = string.Format("{0}&PatientId={1}&PharmacyId={2}&visitid={3}&sts={4}&locationid={5}", "~/./Pharmacy/frmPharmacy_Adult.aspx?name=Delete", Convert.ToInt32(PId), theDR["PharmacyNo"].ToString(), theDR["OrderNo"].ToString(), PtnSts.ToString(), theDR["LocationID"].ToString());
                //            //url=string.Format("{0}&patientid={1}&PharmacyID={2}&sts={3}", "~/./Pharmacy/frmPharmacy_Adult.aspx?name=Delete", Convert.ToInt32(PId), theDR["OrderNo"].ToString(), PtnSts.ToString());
                //        }

                //        theFrmRoot.NavigateUrl = url;
                //        break;
                //    case "Laboratory": url = string.Format("{0}&patientid={1}&LabID={2}&sts={3}", "~/./Laboratory/frmLabOrder.aspx?name=Delete", Convert.ToInt32(PId), theDR["OrderNo"].ToString(), PtnSts.ToString());
                //        theFrmRoot.NavigateUrl = url;
                //        break;
                //    case "Home Visit": url = string.Format("{0}&patientid={1}&OrderId={2}&sts={3}&locationid{4}", "~/./Scheduler/frmScheduler_HomeVisit.aspx?name=Delete", Convert.ToInt32(PId), theDR["OrderNo"].ToString(), PtnSts.ToString(), theDR["LocationID"].ToString());
                //        theFrmRoot.NavigateUrl = url;
                //        break;
                //    case "Paediatric Pharmacy": url = string.Format("{0}&PatientId={1}&PharmacyId={2}&visitid={3}&sts={4}&locationid={5}", "~/./Pharmacy/frmPharmacy_Paediatric.aspx?name=Delete", Convert.ToInt32(PId), theDR["PharmacyNo"].ToString(), theDR["OrderNo"].ToString(), PtnSts.ToString(), theDR["LocationID"].ToString());
                //        //string.Format("{0}&patientid={1}&PharmacyID={2}&sts={3}", "~/./Pharmacy/frmPharmacy_Paediatric.aspx?name=Delete", Convert.ToInt32(PId), theDR["OrderNo"].ToString(), PtnSts.ToString());
                //        theFrmRoot.NavigateUrl = url;
                //        break;
                //    case "Non-ART Follow-Up": url = string.Format("{0}&PatientId={1}&PharmacyID={2}&visitid={3}&sts={4}", "./frmClinical_NonARTFollowUp.aspx?name=Delete", Convert.ToInt32(PId), theDR["PharmacyNo"].ToString(), theDR["OrderNo"].ToString(), PtnSts.ToString());
                //        theFrmRoot.NavigateUrl = url;
                //        break;

                //    case "Patient Record - Follow Up": url = string.Format("{0}&patientid={1}&visitid={2}&sts={3}", "./frmClinical_PatientRecordCTC.aspx?name=Delete", Convert.ToInt32(PId), theDR["OrderNo"].ToString(), PtnSts.ToString());
                //        theFrmRoot.NavigateUrl = url;
                //        break;
                //    //case "Care Tracking": url = string.Format("{0}&PatientId={1}&TrackingId={2}&CareendedId={3}&sts={4}", "~/./Scheduler/frmScheduler_ContactCareTrackingnew.aspx?name=Edit", Convert.ToInt32(PId), theDR["OrderNo"].ToString(), theDR["PharmacyNo"].ToString(), PtnSts.ToString());
                //    //    theFrmRoot.NavigateUrl = url;
                //    //    break;

                //    default: break;
                //}

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
                FormManager = null;
            }

        }

        
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Application.Presentation;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;
using IQCare.CCC.UILogic;
using IQCare.CCC.UILogic.Baseline;
using IQCare.CCC.UILogic.Enrollment;
using Interface.CCC.Visit;
using Entities.CCC.Visit;

namespace IQCare.Web.CCC.Patient
{
    public partial class PatientHome : System.Web.UI.Page
    {
        public decimal march_height;
        protected int ptnPk = 0;
        protected int labTestId = 0;
        protected Decimal vlValue = 0;
        protected IPatientLabOrderManager _lookupData = (IPatientLabOrderManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.visit.BPatientLabOrdermanager, BusinessProcess.CCC");

        protected int PatientId
        {
            get { return Convert.ToInt32(Session["PatientPK"]); }
        }

        protected int PatientMasterVisitId
        {
            get { return Convert.ToInt32(Session["PatientMasterVisitId"]); }
        }

        protected int UserId
        {
            get { return Convert.ToInt32(Session["AppUserId"]); }
        }

        protected string PatientGender
        {
            get { return Convert.ToString(Session["Gender"]); }
        }

        protected string PatientStatus
        {
            get { return Convert.ToString(Session["PatientStatus"]); }
        }
        protected string PatientType
        {

            get
            {
                var patientLookupManager = new PatientLookupManager();
                return patientLookupManager.GetPatientTypeId(PatientId);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var patientTransfer = new PatientTransferInmanager();
            var patientDiagnosis = new PatientHivDiagnosisManager();
            var patientEntryPoint = new PatientEntryPointManager();
            Session["TechnicalAreaId"] = 203;
            var objTransfer = patientTransfer.GetPatientTransferIns(PatientId);
            var objDiagnosis = patientDiagnosis.GetPatientHivDiagnosisList(PatientId);
            var objEntryPoint = patientEntryPoint.GetPatientEntryPoints(Convert.ToInt32(Session["PatientPK"]));

            if (objTransfer.Count > 0)
            {
                foreach (var item in objTransfer)
                {
                    lblTransferinDate.Text = "<h6>" + item.TransferInDate.ToString("dd-MMM-yyyy") + "</h6>";
                    lblTreatmentStartDate.Text = "<h6>" + item.TreatmentStartDate.ToString("dd-MMM-yyyy") + "</h6>"; ;
                    //lblTIRegimen.Text = "<h6>" + LookupLogic.GetLookupNameById(Convert.ToInt32(item.CurrentTreatment)).ToString() + "</h6>"; ;
                    lblFacilityFrom.Text = "<h6>" + item.FacilityFrom.ToString() + "</h6>"; ;
                }
            }
            else
            {
                lblTransferinDate.Text = "N/A";
                lblTreatmentStartDate.Text = "N/A";
                lblTIRegimen.Text = "N/A";
                lblFacilityFrom.Text = "N/A";
            }

            if (objDiagnosis.Count > 0)
            {
                foreach (var item in objDiagnosis)
                {
                    if (item.HivDiagnosisDate.HasValue)
                    {
                        DateTime HivDiagnosisDate = item.HivDiagnosisDate.Value;
                        lblDateOfHivDiagnosis.Text = HivDiagnosisDate.ToString("dd-MMM-yyyy");
                    }
                    else
                    {
                        lblDateOfHivDiagnosis.Text = "Not Taken";
                    }

                    if (item.EnrollmentDate.HasValue)
                    {
                        lblDateOfEnrollment.Text = item.EnrollmentDate.Value.ToString("dd-MMM-yyyy");
                    }
                    else
                    {
                        lblDateOfEnrollment.Text = "Not Taken";
                    }
                    
                    // lblWhoStage.Text = LookupLogic.GetLookupNameById(item.EnrollmentWhoStage).ToString();
                    //lblDateOfHivDiagnosis.Text = item.HivDiagnosisDate.ToString("dd-MMM-yyyy");
                    lblARTInitiationDate.Text = Convert.ToString(item.ArtInitiationDate);
                }

            }
            else
            {
                lblDateOfHivDiagnosis.Text = "Not Taken";
                lblDateOfEnrollment.Text = "Not Taken";
                lblWhoStage.Text = "Not Taken";
                lblARTInitiationDate.Text = "Not Taken";
            }

            if (objEntryPoint.Count > 0)
            {
                foreach (var item in objEntryPoint)
                {
                    lblEntryPoint.Text = LookupLogic.GetLookupNameById(item.EntryPointId);
                }
            }
            else
            {
                lblEntryPoint.Text = "missing";
            }

            if (!IsPostBack)
            {
                ILookupManager mgr =
                    (ILookupManager)
                    ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");

                List<LookupItemView> keyPopulationList = mgr.GetLookItemByGroup("PopulationType");
                if (keyPopulationList != null && keyPopulationList.Count > 0)
                {
                    bioPatientPopulation.Items.Add(new ListItem("select", "0"));
                    foreach (var item in keyPopulationList)
                    {
                        bioPatientPopulation.Items.Add(new ListItem(item.ItemDisplayName, item.ItemId.ToString()));
                    }
                }

                List<LookupCounty> ct = mgr.GetLookupCounties();

                if (ct != null && ct.Count > 0)
                {
                    smrCounty.Items.Add(new ListItem("select", "0"));
                    foreach (var item in ct)
                    {
                        smrCounty.Items.Add(new ListItem(item.CountyName, item.CountyId.ToString()));
                    }
                }

                List<LookupItemView> vw = mgr.GetGenderOptions();
                if (vw != null && vw.Count > 0)
                {
                    trtGender.Items.Add(new ListItem("select", "0"));

                    foreach (var item in vw)
                    {
                        trtGender.Items.Add(new ListItem(item.ItemName, item.ItemId.ToString()));
                    }
                }

                if (vw != null && vw.Count > 0)
                {
                    Gender.Items.Add(new ListItem("select", "0"));

                    foreach (var item in vw)
                    {
                        Gender.Items.Add(new ListItem(item.ItemName, item.ItemId.ToString()));
                    }
                }

                List<LookupItemView> keyPopList = mgr.GetLookItemByGroup("KeyPopulation");
                if (keyPopList != null && keyPopList.Count > 0)
                {
                    var patientLookUp = new PatientLookupManager();
                    int sex = patientLookUp.GetPatientSexId(Convert.ToInt32(Session["PatientPK"]));
                    string gender = LookupLogic.GetLookupNameById(sex);
                    //bioPatientKeyPopulation.Items.Add(new ListItem("select", "0"));
                    foreach (var item in keyPopList)
                    {
                        if (gender == "Female" && item.DisplayName == "Men having Sex with Men")
                        {

                        }
                        else if (gender == "Male" && item.DisplayName == "Female Sex Worker")
                        {

                        }
                        else
                        {
                            bioPatientKeyPopulation.Items.Add(new ListItem(item.ItemDisplayName, item.ItemId.ToString()));
                        }       
                    }
                }

                // Get Patient Regimen Map:
                IPatientTreatmentTrackerManager patientTreatmentTrackerManager = (IPatientTreatmentTrackerManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Lookup.BPatientTreatmentTrackerManager, BusinessProcess.CCC");
                var curentRegimen = patientTreatmentTrackerManager.GetCurrentPatientRegimen(PatientId);

                if (curentRegimen != null)
                {
                    if (curentRegimen.RegimenId > 0)
                    {
                        //lblCurrentRegimen.Text = "<span class='label label-success'>" + curentRegimen.Regimen.ToString() + " started on : " + Convert.ToDateTime(curentRegimen.DispensedByDate).ToString("dd-MMM-yyyy") + "</span>";
                        lblCurrentRegimen.Text = "<span class='label label-success'>" + curentRegimen.Regimen.ToString() + "</span>";
                    }
                    else
                    {
                        lblCurrentRegimen.Text = "<span class='label label-danger'>Patient NOT on ARVs</span>";
                    }
                }
                else
                {
                    lblCurrentRegimen.Text = "<span class='label label-danger'>Patient NOT on ARVs</span>";
                }

                //Get Adherance Status
                ILookupManager patientAdheLookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");

                var adheranceStatus = patientAdheLookupManager.GetPatientAdherence(PatientId);

                if (adheranceStatus != null)
                {
                    string adheranceString = LookupLogic.GetLookupNameById(adheranceStatus.Score);
                    switch (adheranceString)
                    {
                        case "Poor":
                            lblAdheranceStatus.Text = "<span class='label label-danger'> Poor [Offer Adherence Interventions]</span>";
                            break;
                        case "Good":
                            lblAdheranceStatus.Text = "<span class='label label-success'> Good </span>";
                            break;
                        case "Fair":
                            lblAdheranceStatus.Text = "<span class='label label-warning'> Fair [Consider Adherence Intervetion]</span>";
                            break;
                    }

                }
                else
                {
                    lblAdheranceStatus.Text = "<span class='label label-danger'>Adherance Assessment Not Done</span>";
                }

                /*update Treatment Initiation for New Patients */
                if (PatientType == "New")
                {
                    var patientTreatmentManager = new PatientTreatmentTrackerManager();
                    var ptnTreatmentInitiation = patientTreatmentManager.GetCurrentPatientRegimen(PatientId);
                    var ptnTreatmentBaseline = patientTreatmentManager.GetPatientbaselineRegimenLookup(PatientId);
                    if (ptnTreatmentInitiation != null)
                    {
                        if (ptnTreatmentBaseline != null)
                        {
                            if (ptnTreatmentBaseline.DispensedByDate.HasValue)
                            {
                                DateTime DispensedByDate = (DateTime)ptnTreatmentBaseline.DispensedByDate;

                                lblFirstline.Text = DispensedByDate.ToString("dd-MMM-yyyy");
                                lblcohort.Text = DispensedByDate.ToString("MMM") + "-" + DispensedByDate.Year;
                            }

                            lblRegimenName.Text = ptnTreatmentInitiation.Regimen.ToString();
                            //lblCurrentRegimen.Text = "<span class='label label-success'>" + ptnTreatmentBaseline.Regimen.ToString() + "</span>";
                            lblARTInitiationDate.Text = ptnTreatmentBaseline.CreateDate.ToString("dd-MMM-yyyy");
                        }
                    }
                    else
                    {
                        lblDateOfARTInitiation.Text = "<span class='label'> Not dispensed</span>";
                        lblcohort.Text = "<span class='label label-danger'>N/A</span>";
                       // lblCurrentRegimen.Text = "<span class='label label-danger'>PATIENT NOT ON ARVs</span>";

                    }
                }

                // viral Load Alerts
               
                //PatientLabTracker vltestId = _lookupData.GetPatientLabTestId(PatientId);  //check patient has vl lab
                PatientLabTracker lastVL = _lookupData.GetPatientLastVL(PatientId);

                //if (lastVL != null)
                //{
                //    labTestId = lastVL.LabTestId;
                //}
                if (lastVL != null)  
                {
                    //var labOrder = _lookupData.GetPatientCurrentviralLoadInfo(PatientId);  //get vl lab details for patient

                    //if (lastVL != null)
                    //{
                        //foreach (var item in _lookupData.GetPatientVlById(labOrder.Id))
                        //{
                        //    vlValue = item.ResultValues;
                        //}                              

                        // vlValue = Convert.ToDecimal(_lookupData.GetPatientVL(LabOrder.Id));
                        if (PatientType == "New")
                        {
                            //lblbaselineVL.Text = Convert.ToString(vlValue);
                            DateTime x = Convert.ToDateTime(lastVL.SampleDate);
                            lblBlDate.Text = x.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            lblbaselineVL.Text = "<span class='label label-danger'>Not Taken</span>";
                            lblBlDate.Text = "<span class='label label-danger'>N/A</span>";
                        }

                        switch (lastVL.Results)
                        {

                            case "Pending":
                                var pendingDueDate = Convert.ToDateTime(lastVL.SampleDate);
                                DateTime sampleDate = Convert.ToDateTime(lastVL.SampleDate.ToString());

                                if ((DateTime.Today.Subtract(sampleDate).Days > 30))
                                {
                                    lblVL.Text = "<span class='label label-danger' > Overdue | Ordered On: " + ((DateTime)lastVL.SampleDate).ToString("dd-MMM-yyyy") + "</span>";
                                    lblvlDueDate.Text = "<span class='label label-success'> " + pendingDueDate.AddMonths(6).ToString("dd-MMM-yyy") + " </span>";
                                }
                                else if ((lastVL.Results == "Pending") && (DateTime.Today.Subtract(sampleDate).Days < 30))
                                {
                                    lblVL.Text = "<span class='label label-warning'> Pending | Ordered On: " + ((DateTime)lastVL.SampleDate).ToString("dd-MMM-yyyy") + "</span>";
                                    lblvlDueDate.Text = "<span class='label label-success'> " + pendingDueDate.AddMonths(6).ToString("dd-MMM-yyy") + " </span>";
                                }
                                else
                                {

                                    lblVL.Text = "<span class='label label-warning'>" + lastVL.Results + "| Date: " + ((DateTime)lastVL.SampleDate).ToString("dd-MMM-yyyy") + "</span>";
                                    lblbaselineVL.Text = "<span class='label label-warning'>" + lastVL.Results + "| Date: " + ((DateTime)lastVL.SampleDate).ToString("dd-MMM-yyyy") + "</span>";
                                }
                                break;

                            case "Complete":
                                if (lastVL.ResultValues >= 1000)
                                {
                                    lblVL.Text = "<span class='label label-danger'>" + lastVL.ResultValues + " copies/ml</span>";
                                    lblvlDueDate.Text = "<span class='label label-success' > " + ((DateTime)lastVL.SampleDate).AddMonths(3).ToString("dd-MMM-yyyy") + "</span>";
                                }
                               else if (lastVL.ResultValues <= 50)
                                {

                                    lblVL.Text = "<span class='label label-success'> Undetectable VL</span>";
                                    lblvlDueDate.Text = "<span class='label label-success' > " + ((DateTime)lastVL.SampleDate).AddMonths(12).ToString("dd-MMM-yyyy") + "</span>";

                                }
                                else
                                {
                                    lblVL.Text = "<span class='label label-success' > Complete | Results : " + lastVL.ResultValues + " copies/ml</span>";
                                    lblvlDueDate.Text = "<span class='label label-success' > " + ((DateTime)lastVL.SampleDate).AddMonths(12).ToString("dd-MMM-yyyy") + "</span>";
                                   
                                }
                                break;
                                default:
                                    var patientEnrollment = new PatientEnrollmentManager();
                                    var enrolDate = patientEnrollment.GetPatientEnrollmentDate(PatientId);
                                    DateTime today = DateTime.Today;
                                    TimeSpan difference = today.Date - enrolDate.Date;
                                    int days = (int)difference.TotalDays;

                                    if (days < 180)
                                    {
                                        lblvlDueDate.Text = "<span class='label label-success'>" + enrolDate.AddMonths(6).ToString("dd-MMM-yyyy") + "</span>";
                                        lblVL.Text = "<span class='label label-success fa fa-exclamation'><strong>  VL Not Requested  </strong></span>";

                                    }
                                    else
                                    {
                                        lblVL.Text = "<span class='label label-danger'> Not Available </span>";
                                        lblvlDueDate.Text = "<span class='label label-danger'><strong> Overdue </strong></span>";
                                    }
                                break;
                        }
                  
                    //}
                }
                else
                {
                    var patientEnrollment = new PatientEnrollmentManager();
                    var enrolDate = patientEnrollment.GetPatientEnrollmentDate(PatientId);
                    DateTime today = DateTime.Today;
                    TimeSpan difference = today.Date - enrolDate.Date;
                    int days = (int)difference.TotalDays;

                    if (days < 180)
                    {
                        lblvlDueDate.Text = "<span class='label label-success'>" + enrolDate.AddMonths(6).ToString("dd-MMM-yyyy") + "</span>";
                        lblVL.Text = "<span class='label label-success fa fa-exclamation'><strong>  VL Not Requested  </strong></span>";

                    }
                    else
                    {

                        lblVL.Text = "<span class='label label-danger'> Not Available </span>";
                        lblvlDueDate.Text = "<span class='label label-danger'><strong> Overdue </strong></span>";
                    }
                }
            }
        }                        
      }
   }
       


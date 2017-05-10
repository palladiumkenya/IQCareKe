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
        public int PatientMasterVisitId;
        public decimal march_height;
        protected int ptnPk = 0;
        protected int labTestId = 0;
        protected Decimal vlValue = 0;
        protected IPatientLabOrderManager _lookupData = (IPatientLabOrderManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.visit.BPatientLabOrdermanager, BusinessProcess.CCC");

        protected int PatientId
        {
            get { return Convert.ToInt32(Session["PatientPK"]); }
        }

        protected int UserId
        {
            get { return Convert.ToInt32(Session["AppUserId"]); }
        }

        protected string PatientGender
        {
            get { return Convert.ToString(Session["Gender"]); }
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
                    lblDateOfHivDiagnosis.Text = item.HivDiagnosisDate.ToString("dd-MMM-yyyy");
                    lblDateOfEnrollment.Text = item.EnrollmentDate.ToString("dd-MMM-yyyy");
                    // lblWhoStage.Text = LookupLogic.GetLookupNameById(item.EnrollmentWhoStage).ToString();
                    lblDateOfHivDiagnosis.Text = item.HivDiagnosisDate.ToString("dd-MMM-yyyy");
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
                    bioPatientKeyPopulation.Items.Add(new ListItem("select", "0"));
                    foreach (var item in keyPopList)
                    {
                        bioPatientKeyPopulation.Items.Add(new ListItem(item.ItemDisplayName, item.ItemId.ToString()));
                    }
                }

                // Get Patient Regimen Map:
                //IPatientTreatmentTrackerManager patientTreatmentTrackerManager = (IPatientTreatmentTrackerManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Lookup.BPatientTreatmentTrackerManager, BusinessProcess.CCC");
                //var curentRegimen = patientTreatmentTrackerManager.GetCurrentPatientRegimen(PatientId);

                //if (curentRegimen != null)
                //{
                //    if (curentRegimen.RegimenId > 0)
                //    {
                //        lblCurrentRegimen.Text = "< span class='label label-success'>" + curentRegimen.TreatmentStatus.ToString() + "</span>";
                //    }
                //    else
                //    {
                //        lblCurrentRegimen.Text = "<span class='label label-danger'>Patient NOT on ARVs</span>";
                //    }
                //}
                //else
                //{
                //    lblCurrentRegimen.Text = "<span class='label label-danger'>Patient NOT on ARVs</span>";
                //}

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
                if (PatientType=="New")
                {
                    var patientTreatmentManager =new PatientTreatmentTrackerManager();
                    var ptnTreatmentInitiation = patientTreatmentManager.GetCurrentPatientRegimen(PatientId);
                    if (ptnTreatmentInitiation != null)
                    {
                        lblFirstline.Text = ptnTreatmentInitiation.CreateDate.ToString("dd-MMM-yyyy");
                        lblcohort.Text = ptnTreatmentInitiation.CreateDate.ToString("MMM")  + "-" + ptnTreatmentInitiation.CreateDate.Year;
                        lblRegimenName.Text = ptnTreatmentInitiation.Regimen.ToString();
                        lblCurrentRegimen.Text = "<span class='label label-success'>" + ptnTreatmentInitiation.Regimen.ToString() + "</span>";

                    }
                    else
                    {
                        lblDateOfARTInitiation.Text = "<span class='label label-danger'> NO dispensing</span>";
                        lblcohort.Text = "<span class='label label-danger'>N/A</span>";
                        lblCurrentRegimen.Text = "<span class='label label-danger'>PATIENT NOT ON ARVS</span>";

                    }
                }

                // viral Load Alerts
                //PatientLookup _patientlookup= mgr.GetPatientPtn_pk(PatientId);
                //if (_patientlookup != null)
                //{
                //    ptnPk = Convert.ToInt16(_patientlookup.ptn_pk);
                //}
                PatientLabTracker vltestId = _lookupData.GetPatientLabTestId(PatientId);
                if (vltestId != null)
                {
                    labTestId = vltestId.LabTestId;
                }
                if (labTestId > 0)
                {
                    var labOrder = _lookupData.GetPatientCurrentviralLoadInfo(PatientId);

                    if (labOrder != null)
                    {
                        foreach (var item in _lookupData.GetPatientVL(labOrder.Id))
                        {
                            vlValue = item.ResultValues;
                        }
                        // vlValue = Convert.ToDecimal(_lookupData.GetPatientVL(LabOrder.Id));
                        if (PatientType == "New")
                        {
                            lblbaselineVL.Text = Convert.ToString(vlValue);
                            DateTime x = Convert.ToDateTime(labOrder.SampleDate);
                            lblBlDate.Text = x.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            lblbaselineVL.Text = "<span class='label label-danger'>Not Taken</span>";
                            lblBlDate.Text = "<span class='label label-danger'>N/A</span>";
                        }

                        switch (labOrder.Results)
                        {
                               
                            case "Pending":
                                lblVL.Text = "<span class='label label-warning'>" + labOrder.Results + "/ Date: " + ((DateTime)labOrder.SampleDate).ToString("DD-MMM-YYY") + "</span>";
                                lblvlDueDate.Text = "<span class='label label-success'> N/A </span>";
                                break;
                            case "Complete":
                                if (vlValue > 1000)
                                {
                                    lblVL.Text = "<span class='label label-danger'>" + vlValue + " copies/ml</span>";
                                    lblvlDueDate.Text = ((DateTime)labOrder.SampleDate).AddMonths(3).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    lblvlDueDate.Text = ((DateTime)labOrder.SampleDate).AddMonths(6).ToString("dd-MMM-yyyy");
                                }
                                break;
                            default:
                                break;
                        }
                        DateTime sampleDate = Convert.ToDateTime(labOrder.SampleDate.ToString());
                        if (sampleDate.Subtract(DateTime.Today).Days > 30)
                        {
                            lblVL.Text = "<span class='label label-danger' > Overdue | Ordered On: " + ((DateTime)labOrder.SampleDate).ToString("dd-MMM-yyyy") + "</span>";

                        }
                        else
                        {
                            lblVL.Text = "<span class='label label-warning'> Pending | Ordered On: " + ((DateTime)labOrder.SampleDate).ToString("dd-MMM-yyyy") + "</span>";

                        }
                        
                    }
                    else
                    {
                            var patientEnrollment = new PatientEnrollmentManager();
                            var enrolDate = patientEnrollment.GetPatientEnrollmentDate(PatientId);
                            DateTime today=DateTime.Today;
                            TimeSpan difference = today.Date-enrolDate.Date;
                             int days = (int)difference.TotalDays;

                        if (days < 10)
                        {
                            lblvlDueDate.Text = "<span class='label label-danger'>" + enrolDate.AddMonths(6).ToString("dd-MMM-yyyy") + "</span>";
                            lblVL.Text = "<span class='label label-danger fa fa-exclamation'><strong> Request VL NOW! </strong></span>";

                        }
                        else
                        {
                            lblvlDueDate.Text = "<span class='label label-success'>" + enrolDate.AddMonths(6).ToString("dd-MMM-yyyy") + "</span>";
                            lblVL.Text = "<span class='label label-danger fa fa-exclamation'><strong> Not Done/Pending </strong></span>";
                        }
                    }
                }
                else
                {
                    lblVL.Text = "<span class='label label-danger'> VL Not Requested </span>";
                    lblvlDueDate.Text = "<span class='label label-danger'><strong> Not Available </strong></span>";
                    var patientEnrollment = new PatientEnrollmentManager();
                    var enrolDate = patientEnrollment.GetPatientEnrollmentDate(PatientId);
                    lblvlDueDate.Text = "<span class='label label-success'>"+enrolDate.AddMonths(6).ToString("dd-MMM-yyyy")+"</span>";
                }
            }
        }
    }
}
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
using Interface.CCC;

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
            get { return Convert.ToInt32(Session["patientId"]); }
        }

        protected int UserId
        {
            get { return Convert.ToInt32(Session["AppUserId"]); }
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
            var objEntryPoint = patientEntryPoint.GetPatientEntryPoints(Convert.ToInt32(Session["patientId"]));

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
                    lblARTInitiationDate.Text = item.ArtInitiationDate.ToString("dd-MMM-yyyy");
                }

            }
            else
            {
                lblDateOfHivDiagnosis.Text = "Missing";
                lblDateOfEnrollment.Text = "Missing";
                lblWhoStage.Text = "Missing";
                lblARTInitiationDate.Text = "Missing";
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
                ILookupManager regimenMap = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
                var regimen = regimenMap.GetCurentPatientRegimen(PatientId);

                if (regimen != null)
                {
                    if (regimen.RegimenId > 0)
                    {
                        lblCurrentRegimen.Text = "< span class='label label-success'>" + regimen.RegimenType.ToString() + "</span>";
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
                            lblAdheranceStatus.Text = "<span class='label labe-success'> Good </span>";
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


                // viral Load Alerts
                //PatientLookup _patientlookup= mgr.GetPatientPtn_pk(PatientId);
                //if (_patientlookup != null)
                //{
                //    ptnPk = Convert.ToInt16(_patientlookup.ptn_pk);
                //}
                PatientLabTracker _vltestId = _lookupData.GetPatientLabTestId(PatientId);
                if (_vltestId != null)
                {
                    labTestId = _vltestId.LabTestId;
                }
                if (labTestId > 0)
                {
                    var LabOrder = _lookupData.GetPatientCurrentviralLoadInfo(PatientId);

                    if (LabOrder != null)
                    {
                        foreach (var item in _lookupData.GetPatientVL(LabOrder.Id))
                        {
                            vlValue = item.ResultValues;
                        }
                        // vlValue = Convert.ToDecimal(_lookupData.GetPatientVL(LabOrder.Id));
                        switch (LabOrder.Results)
                        {
                            case "Pending":
                                lblVL.Text = "<span class='label label-warning'>" + LabOrder.Results + "/ Date: " + ((DateTime)LabOrder.SampleDate).ToString("DD-MMM-YYY") + "</span>";
                                lblvlDueDate.Text = "<span class='label label-success'>N/A</span>";
                                break;
                            case "Complete":
                                if (vlValue > 1000)
                                {
                                    lblVL.Text = "<span class='label label-danger'>" + vlValue + " copies/ml</span>";
                                    lblvlDueDate.Text = ((DateTime)LabOrder.SampleDate).AddMonths(3).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    lblvlDueDate.Text = ((DateTime)LabOrder.SampleDate).AddMonths(6).ToString("dd-MMM-yyyy");
                                }
                                break;
                            default:
                                break;
                        }
                        DateTime sampleDate = Convert.ToDateTime(LabOrder.SampleDate.ToString());
                        if (sampleDate.Subtract(DateTime.Today).Days > 30)
                        {
                            lblVL.Text = "<span class='label label-danger' > Overdue | Ordered On: " + ((DateTime)LabOrder.SampleDate).ToString("dd-MMM-yyyy") + "</span>";

                        }
                        else
                        {
                            lblVL.Text = "<span class='label label-warning'> Pending | Ordered On: " + ((DateTime)LabOrder.SampleDate).ToString("dd-MMM-yyyy") + "</span>";

                        }


                    }
                    else
                    {
                        lblVL.Text = "<span class='label label-danger fa fa-exclamation'><strong> Not Done/Pending </strong></span>";
                    }
                }
                else
                {
                    lblVL.Text = "<span class='label label-danger'>Patient Not Referenced</span>";
                    lblvlDueDate.Text = "<span class='label label-danger'><strong>Not Available</strong></span>";
                }
            }
        }
    }
}
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

namespace IQCare.Web.CCC.Patient
{
    public partial class PatientHome : System.Web.UI.Page
    {
        public int PatientMasterVisitId;
        public decimal march_height;
        protected int ptnPk=0;
        protected Decimal vlValue=0;
        protected  IPatientLabOrderManager _lookupData = (IPatientLabOrderManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.visit.BPatientLabOrdermanager, BusinessProcess.CCC");

        protected int PatientId
        {
            get { return Convert.ToInt32(Session["patientId"]); }
        }

        protected int UserId
        {
            get { return Convert.ToInt32(Session["AppUserId"]); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var patientTransfer=new PatientTransferInmanager();
            var patientDiagnosis=new PatientHivDiagnosisManager();
            var patientEntryPoint=new PatientEntryPointManager();

            var objTransfer = patientTransfer.GetPatientTransferIns(PatientId);
            var objDiagnosis = patientDiagnosis.GetPatientHivDiagnosisList(PatientId);
            var objEntryPoint = patientEntryPoint.GetPatientEntryPoints(Convert.ToInt32(Session["patientId"]));

            if (objTransfer.Count > 0)
            {
                foreach (var item in objTransfer)
                {
                    lblTransferinDate.Text = "<h6>" + item.TransferInDate.ToString("dd-MMM-yyyy") + "</h6>";
                    lblTreatmentStartDate.Text = "<h6>" + item.TreatmentStartDate.ToString("dd-MMM-yyyy") + "</h6>"; ;
                    lblTIRegimen.Text = "<h6>" + LookupLogic.GetLookupNameById(Convert.ToInt32(item.CurrentTreatment)).ToString() + "</h6>"; ;
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

                List<LookupItemView> keyPopList = mgr.GetLookItemByGroup("KeyPopulation");
                if (keyPopList != null && keyPopList.Count > 0)
                {
                    bioPatientKeyPopulation.Items.Add(new ListItem("select", "0"));
                    foreach (var item in keyPopList)
                    {
                        bioPatientKeyPopulation.Items.Add(new ListItem(item.ItemDisplayName, item.ItemId.ToString()));
                    }
                }

                // viral Load Alerts
                PatientLookup _patientlookup= mgr.GetPatientPtn_pk(PatientId);
                if (_patientlookup != null)
                {
                    ptnPk = Convert.ToInt16(_patientlookup.ptn_pk);
                }
                if (ptnPk > 0) {
                        var LabOrder = _lookupData.GetPatientCurrentviralLoadInfo(ptnPk);
                        if (LabOrder != null)
                        {
                            vlValue = Convert.ToDecimal(_lookupData.GetPatientVL(LabOrder.Id));
                            switch (LabOrder.OrderStatus)
                            {
                                case "Pending":
                                    lblVL.Text ="<span class='label label-warning'>"+ LabOrder.OrderStatus + "/ Date: " + LabOrder.OrderDate.ToString("DD-MMM-YYY")+"</span>";
                                    lblvlDueDate.Text = "<span class='label label-success'>N/A</span>";
                                    break;
                                case "Complete":
                                    if (vlValue > 1000)
                                    {
                                    lblVL.Text = "<span class='label label-danger'>"+ vlValue +" copies/ml</span>";
                                        lblvlDueDate.Text = LabOrder.OrderDate.AddMonths(3).ToString("DD-MMM-YYYY");
                                    }
                                    else
                                    {
                                        lblvlDueDate.Text = LabOrder.OrderDate.AddMonths(6).ToString("DD-MMM-YYYY");
                                    }
                                    break;
                                default:
                                    break;
                            }
                            lblVL.Text = LabOrder.LabTestId.ToString()+" Date: "+ LabOrder.OrderDate.ToString("DD-MMM-YYY");

                        }else
                        {
                        lblVL.Text = "<span class='label label-danger fa fa-exclamation'><strong> Not Done/Pending </strong></span>";
                        }
                }else
                {
                    lblVL.Text = "<span class='label label-danger'>Patient Not Referenced</span>";
                    lblvlDueDate.Text = "<span class='label label-danger'><strong>Not Available</strong></span>";
                }
            }       
        }      
    }
}
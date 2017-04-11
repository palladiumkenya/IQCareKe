using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Application.Presentation;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;
using IQCare.CCC.UILogic;
using IQCare.CCC.UILogic.Baseline;
using IQCare.CCC.UILogic.Enrollment;

namespace IQCare.Web.CCC.Patient
{
    public partial class PatientHome : System.Web.UI.Page
    {
        public int PatientMasterVisitId;
        public decimal march_height;

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


            }       
        }      
    }
}
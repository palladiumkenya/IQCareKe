using System;
using System.Web;
using System.Web.Services;
using IQCare.CCC.UILogic.Baseline;

namespace IQCare.Web.CCC.WebService
{
    /// <summary>
    /// Summary description for PatientbaselineService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class PatientbaselineService : System.Web.Services.WebService
    {
        internal string JsonMessage;
        internal int Result;
        private int _patientId;
        private int _patientMasterVisitId;
       

        [WebMethod(EnableSession = true)]
        public string AddPatientTransferStatus(int patientId, int patientMastervisitId, int serviceAreaId,
            DateTime transferinDate,
            DateTime treatmentStartDate, string currentTreatment, string facilityFrom, int mflCode, string countyFrom,
            string transferInNotes,int userId)
        {
            try
            {
                _patientId = Convert.ToInt32(HttpContext.Current.Session["patientId"]);
               _patientMasterVisitId = Convert.ToInt32(HttpContext.Current.Session["PatientmasterVisitId"]);

                PatientTransferInmanager patientTranfersInManager = new PatientTransferInmanager();
                Result = patientTranfersInManager.AddpatientTransferIn(patientId, _patientMasterVisitId, serviceAreaId,
                    transferinDate, treatmentStartDate, currentTreatment, facilityFrom, mflCode, countyFrom,
                    transferInNotes,userId);
                if (Result > 0)
                {
                    JsonMessage = "Patient TransferIn status captured successfully!";
                }
            }
            catch (Exception e)
            {
                JsonMessage = e.Message + ' ' + e.InnerException;
            }

            return JsonMessage;

        }

        [WebMethod(EnableSession = true)]
        public string AddPatientArtUseHistory(int patientId, int patientMasterVisitId,string[][] artuseStrings, int userId)
        {

            try
            {
                
                PatientArvHistoryManager patientArtUseHistory=new PatientArvHistoryManager();
                _patientId = Convert.ToInt32(HttpContext.Current.Session["patientId"]);
                _patientMasterVisitId = Convert.ToInt32(HttpContext.Current.Session["PatientmasterVisitId"]);


                for (int i=0;i<=artuseStrings.Length;i++)
                {
                    Result = patientArtUseHistory.AddPatientArtUseHistory(_patientId, _patientMasterVisitId, artuseStrings[i][i], artuseStrings[i][i], artuseStrings[i][i],Convert.ToDateTime(artuseStrings[i][i]),userId);
                }
                
                   
                if (Result > 0)
                {
                    JsonMessage = "Patient ARV Use History Captured Successfully!";
                }
            }
            catch (Exception e)
            {
                JsonMessage=e.Message+' '+e.InnerException;
            }
            return JsonMessage;
        }

        [WebMethod]
        public string AddPatientHIVDiagnosis(int patientId,int patientMasterVisitId, DateTime dateOfHIVDiagnsosi,DateTime dateofEnrollment,int whoStage,DateTime dateofArtInitiation)
        {
            try
            {

            }
            catch (Exception e)
            {
                JsonMessage = e.Message + ' ' + e.InnerException;
            }

            return JsonMessage;
        }

        [WebMethod(EnableSession = true)]
        public string AddPatientHivDiagnosis(int patientId, int patientMasterVisitId, DateTime hivDiagnosisDate, DateTime enrollmentDate, int enrollmentWhoStage, DateTime artInitiationDate, bool artHistoryUse, bool hivRetest, int hivRetestTypeId, string reasonForNotRetest,int userId)
        {
            try
            {
                PatientHivEnrollmentBaselineManager patientHivEnrollmentBaseline = new PatientHivEnrollmentBaselineManager();
                _patientId = Convert.ToInt32(HttpContext.Current.Session["patientId"]);
                _patientMasterVisitId = Convert.ToInt32(HttpContext.Current.Session["PatientmasterVisitId"]);

                patientHivEnrollmentBaseline.AddHivEnrollmentBaseline(_patientId, _patientMasterVisitId,
                    hivDiagnosisDate, enrollmentDate, enrollmentWhoStage, artInitiationDate, artHistoryUse, hivRetest,
                    hivRetestTypeId, reasonForNotRetest,userId);
                if (Result > 0)
                {
                    JsonMessage = "Patient HIV Enrollment Baseline Information Captured successfully";
                }
            }
            catch (Exception e)
            {
                JsonMessage = e.Message + ' ' + e.InnerException;
            }
            return JsonMessage;
        }

        [WebMethod(EnableSession = true)]
        public string AddPatientArtUseInitiationBaseline(int patientId, int patientMasterVisitId, bool hbvInfected, bool pregnant,
            bool tbInfected, int whoStage, bool breastfeeding, int cd4Count, decimal viralLoad, DateTime viralLoadDate,
            decimal muac, decimal weight, decimal height, string artCohort, DateTime firstlineStartDate,
            int startRegimen,int userId)
        {
            try
            {
                PatientBaslineAssessmentManager patientArtInitiationBasline=new PatientBaslineAssessmentManager();
                _patientId = Convert.ToInt32(HttpContext.Current.Session["patientId"]);
                _patientMasterVisitId = Convert.ToInt32(HttpContext.Current.Session["PatientmasterVisitId"]);
                // Excess arguments provided
                /*Result = patientArtInitiationBasline.AddArtInitiationbaseline(_patientId, _patientMasterVisitId,
                    hbvInfected, pregnant, tbInfected, whoStage, breastfeeding, cd4Count, viralLoad, viralLoadDate, muac,
                    weight, height, artCohort, firstlineStartDate, startRegimen,userId);*/
                Result = patientArtInitiationBasline.AddArtInitiationbaseline(_patientId, _patientMasterVisitId,
                hbvInfected, pregnant, tbInfected, whoStage, breastfeeding, cd4Count, muac,
                weight, height, userId);
                if (Result > 0)
                {
                    JsonMessage = "Patient ART Initiation Baseline Captured Successfully!";
                }
            }
            catch (Exception e)
            {
                JsonMessage = e.Message + ' ' + e.InnerException;
            }

            return JsonMessage;

        }
    }
}

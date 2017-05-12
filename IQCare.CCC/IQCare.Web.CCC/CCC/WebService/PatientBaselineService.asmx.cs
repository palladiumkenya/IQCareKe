using System;
using System.Web;
using System.Web.Services;
using Entities.CCC.Baseline;
using IQCare.CCC.UILogic.Baseline;
using Newtonsoft.Json;
using IQCare.CCC.UILogic;
using IQCare.CCC.UILogic.Enrollment;

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
        private string _jsonMessage;
        private int _result;
        private int _patientId;
        private int _patientMasterVisitId;
      //  private int _patientTypeId;
        private string _patinetType;


        [WebMethod(EnableSession = true)]
        public string ManagePatientTransferStatus(int patientId, int patientMastervisitId, int serviceAreaId,
            DateTime transferinDate,
            DateTime treatmentStartDate, string currentTreatment, string facilityFrom, int mflCode, string countyFrom,
            string transferInNotes, int userId)
        {
            try
            {
                _patientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
                _patientMasterVisitId = Convert.ToInt32(HttpContext.Current.Session["PatientmasterVisitId"]);

                var patientTranfersInManager = new PatientTransferInmanager();
                _result = patientTranfersInManager.ManagePatientTransferIn(patientId, _patientMasterVisitId, serviceAreaId,transferinDate, treatmentStartDate, currentTreatment, facilityFrom, mflCode, countyFrom,transferInNotes, userId);
                if (_result > 0)
                {
                    _jsonMessage = "Patient TransferIn status Complete!";
                }
            }
            catch (Exception e)
            {
                _jsonMessage = e.Message + ' ' + e.InnerException;
            }

            return _jsonMessage;

        }

        [WebMethod(EnableSession = true)]
        public string ManagePatientHivDiagnosis(int id,int patientId, int patientMasterVisitId, DateTime hivDiagnosisDate,
            DateTime enrollmentDate, int enrollmentWhoStage, string artInitiationStr, int userId)
        {
            try
            {
                var patientHivDiagnosis = new PatientHivDiagnosisManager(); 
                _result = patientHivDiagnosis.ManagePatientHivDiagnosis(0, patientId, patientMasterVisitId, hivDiagnosisDate,enrollmentDate, enrollmentWhoStage, artInitiationStr, userId);
                if (_result > 0)
                {
                    _jsonMessage = "Patient HIV Diagnosis Complete!";
                }
            }
            catch (Exception e)
            {
                _jsonMessage = e.Message + ' ' + e.InnerException;
            }

            return _jsonMessage;
        }

        [WebMethod(EnableSession = true)]
        public string ManagePatientArvHistory(int id, int patientId, int patientMasterVisitId, dynamic artuseStrings, int userId)
        {
            try
            {
                dynamic artuse = JsonConvert.DeserializeObject(artuseStrings);

                var patientHivHistory=new PatientArvHistoryManager();
                if (id < 1)
                {
                    foreach (var item in artuse)
                    {
                        _result = patientHivHistory.AddPatientArtUseHistory(id, patientId, patientMasterVisitId, item.treatment.ToString(),item.purpose.ToString(), item.regimen.ToString(),Convert.ToDateTime(item.dateLastUsed), userId);
                    }   
                }
                else
                {
                    foreach (var item in artuse)
                    {
                        _result = patientHivHistory.UpdatePatientArtUseHistory(id, patientId, patientMasterVisitId, item.treatment, item.purpose, item.regimen, item.dateLastUsed, userId);
                    }
                }
                if (_result > 0)
                {
                    _jsonMessage = "Patient ARV History Complete!";
                }
            }
            catch (Exception e)
            {
                _jsonMessage = e.Message + ' ' + e.InnerException;
            }
           return _jsonMessage; 
        }

        [WebMethod(EnableSession = true)]
        public string ManagePatientBaselineAssessment(int id, int patientId, int patientMasterVisitId, bool pregnant, bool hbvInfected
           , bool tbInfected, int whoStage, bool breastfeeding, decimal cd4Count, decimal muac,
            decimal weight, decimal height, int userId)
        {
            try
            {
                var patientBaseline = new PatientBaselineAssessmentManager();

                _result = patientBaseline.ManagePatientBaselineAssessment(id, patientId, patientMasterVisitId, hbvInfected,
                    pregnant,
                    tbInfected, whoStage, breastfeeding, cd4Count, muac, weight, height, userId);

                if (_result > 0)
                {
                    _jsonMessage = "Patient Baseline Assessment Complete!";
                }
            }
            catch (Exception e)
            {
                _jsonMessage = e.Message + ' ' + e.InnerException;
            }
            return _jsonMessage;
        }

        [WebMethod(EnableSession = true)]
        public string ManagePatientTreatmentInitiation(int id, int patientId, int patientMasterVisitid,
            DateTime dateStartedOnFirstLine, string cohort, int regimen, decimal baselineViralload,
            DateTime baselineViralLoadDate, int userId)
        {
            try
            {
                var patientTreatment = new PatientTreatmentInitiationManager();
                patientTreatment.ManagePatientTreatmentInititation(id, patientId, patientMasterVisitid,dateStartedOnFirstLine, cohort, regimen, baselineViralload, baselineViralLoadDate, userId);
                if (_result > 0)
                {
                    _jsonMessage = "PatientTreatment Initiation Complete!";
                }
            }
            catch (Exception e)
            {
                _jsonMessage = e.Message + ' ' + e.InnerException;
            }

            return _jsonMessage;
        }

        [WebMethod(EnableSession =true)]
        public string GetPatientType(int patientId)
        {
            try {

                var patientLookupManager = new PatientLookupManager();
                _patinetType = patientLookupManager.GetPatientTypeId(patientId);
                _jsonMessage = _patinetType;
            }
            catch(Exception e) {
                _jsonMessage = e.Message;
            }
            return _jsonMessage;
        }

        [WebMethod(EnableSession = true)]
        public DateTime GetPatientEnrollmentDate(int patientId)
        {
            DateTime enrollmentDate = DateTime.Today;
            try
            {
                var patientEnrollment =new PatientEnrollmentManager();
                enrollmentDate = patientEnrollment.GetPatientEnrollmentDate(patientId);
            }
            catch (Exception e)
            {
                _jsonMessage = e.Message;
            }

            return enrollmentDate;
        }

        [WebMethod(EnableSession = true)]
        public string GetPatientBaseline(int patientId)
        {
            try
            {
                var patientBaseline=new PatientBaselineLookupManager();
                var patientBaselineObject = patientBaseline.GetPatientBaseline(patientId);
                _jsonMessage = JsonConvert.SerializeObject(patientBaselineObject);
            }
            catch (Exception e)
            {
                _jsonMessage = e.Message;
            }

            return _jsonMessage;
        }

        [WebMethod(EnableSession = true)]
        public string GetRegimenCategory(int regimenId)
        {
            try
            {
                var lookupManager=new LookupLogic();
                _jsonMessage = LookupLogic.GetRegimenCategory(regimenId).ToString();
            }
            catch (Exception e)
            {
                _jsonMessage = e.Message;
            }

            return _jsonMessage;
        }

        [WebMethod(EnableSession = true)]
        public string GetPatientPriorArvHistory(int patientId)
        {
            try
            {
                var patientArvHistory=new PatientArvHistoryManager();
                var arvHistory = patientArvHistory.GetPatientArtUseHistory(patientId);
                foreach (var item in arvHistory)
                {
                    _jsonMessage += "<tr> <td align='left'>" + item.Purpose + "</td> <td align='left'>" + item.Regimen + "</td> <td align='left'>" + item.DateLastUsed.ToString("dd-mmm-yyy") + "</td> </tr>";
                }
               // _jsonMessage=JsonConvert.SerializeObject(patientArvHistory.GetPatientArtUseHistory(patientId))
            }
            catch (Exception e)
            {
                _jsonMessage = e.Message;
            }
            return _jsonMessage;
        }

        [WebMethod(EnableSession = true)]
        public string GetPatientBaselineInfo(int patientId)
        {
            try
            {
                var patientBaseline = new PatientBaselineLookupManager();
                _jsonMessage = JsonConvert.SerializeObject(patientBaseline.GetPatientBaseline(patientId));
            }
            catch (Exception e)
            {
                _jsonMessage = e.Message;
            }

            return _jsonMessage;
        }

        [WebMethod(EnableSession = true)]
        public string GetNewPatientBaselineVitals(int patientId)
        {
            try
            {
                var vitalsBaseline=new PatientVitalsManager();
                _jsonMessage = JsonConvert.SerializeObject(vitalsBaseline.GetPatientVitalsBaseline(patientId));
            }
            catch (Exception e)
            {
                _jsonMessage = e.Message;
            }
            return _jsonMessage;
        }
    }
}

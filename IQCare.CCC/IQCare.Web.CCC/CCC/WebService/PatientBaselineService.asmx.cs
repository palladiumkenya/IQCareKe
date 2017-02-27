using System;
using System.Web;
using System.Web.Services;
using IQCare.CCC.UILogic.Baseline;
using Newtonsoft.Json;

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


        [WebMethod(EnableSession = true)]
        public string AddPatientTransferStatus(int patientId, int patientMastervisitId, int serviceAreaId,
            DateTime transferinDate,
            DateTime treatmentStartDate, string currentTreatment, string facilityFrom, int mflCode, string countyFrom,
            string transferInNotes, int userId)
        {
            try
            {
                _patientId = Convert.ToInt32(HttpContext.Current.Session["patientId"]);
                _patientMasterVisitId = Convert.ToInt32(HttpContext.Current.Session["PatientmasterVisitId"]);

                PatientTransferInmanager patientTranfersInManager = new PatientTransferInmanager();
                _result = patientTranfersInManager.AddpatientTransferIn(patientId, _patientMasterVisitId, serviceAreaId,
                    transferinDate, treatmentStartDate, currentTreatment, facilityFrom, mflCode, countyFrom,
                    transferInNotes, userId);
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
            DateTime enrollmentDate, int enrollmentWhoStage, DateTime artInitiationDate, int userId)
        {
            try
            {
                var patientHivDiagnosis=new PatientHivDiagnosisManager();
                if (id < 1)
                {
                  _result=  patientHivDiagnosis.AddPatientHivDiagnosis(0, patientId, patientMasterVisitId, hivDiagnosisDate,
                        enrollmentDate, enrollmentWhoStage, artInitiationDate, userId);
                }
                else
                {
                    _result = patientHivDiagnosis.UpdatePatientHivDiagnosis(id, patientId, patientMasterVisitId,
                        hivDiagnosisDate, enrollmentDate, enrollmentWhoStage, artInitiationDate);
                }
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
        public string ManagePatientArvHistory(int id, int patientId, int patientMasterVisitId, string artuseStrings, int userId)
        {
            try
            {
                dynamic artuse = JsonConvert.DeserializeObject(artuseStrings);

                var patientHivHistory=new PatientArvHistoryManager();
                if (id < 1)
                {
                    foreach (var item in artuse)
                    {
                        _result = patientHivHistory.AddPatientArtUseHistory(id, patientId, patientMasterVisitId, item.treatment,item.purpose, item.regimen, item.dateLastUsed, userId);
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
        public string ManagePatientBaselineAssessment(int id, int patientId, int patientMasterVisitId, bool hbvInfected,
            bool pregnant, bool tbInfected, int whoStage, bool breastfeeding, decimal cd4Count, decimal muac,
            decimal weight, decimal height, int userId)
        {
            try
            {

                var patientBaseline = new PatientBaslineAssessmentManager();
                if (id < 1)
                {
                    _result = patientBaseline.AddArtInitiationbaseline(id, patientId, patientMasterVisitId, hbvInfected, pregnant,
                         tbInfected, whoStage, breastfeeding, cd4Count, muac, weight, height, userId);
                }
                else
                {
                    _result = patientBaseline.UpdateArtInitiationbaseline(id, patientId, patientMasterVisitId,
                        hbvInfected, pregnant, tbInfected, whoStage, breastfeeding, cd4Count, muac, weight, height,
                        userId);
                }
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
                var patientTreatment=new PatientTreatmentInitiationManager();
                if (id < 1)
                {
                    patientTreatment.AddPatientTreatmentInititation(id, patientId, patientMasterVisitid,
                        dateStartedOnFirstLine, cohort, regimen, baselineViralload, baselineViralLoadDate, userId);
                }
                else
                {
                    _result = patientTreatment.UpdatePatientTreatmentInititation(id, patientId, patientMasterVisitid,
                        dateStartedOnFirstLine, cohort, regimen, baselineViralload, baselineViralLoadDate);
                }
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
    }
}

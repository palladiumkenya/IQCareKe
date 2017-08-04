using Entities.CCC.Assessment;
using IQCare.CCC.UILogic.assessment;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace IQCare.Web.CCC.WebService
{
    /// <summary>
    /// Summary description for PatientTreatmentPreparation
    /// </summary>
    /// 

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class PatientTreatmentPreparation : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod(EnableSession = true)]
        public int CheckIfPsychosocialCriteriaExists(int patientId)
        {
            int result = 0;
            string msg = "";
            PatientPsychosocialCriteriaManager _PatientPsychosocialCriteriaManager = new PatientPsychosocialCriteriaManager();

            try {

                result = _PatientPsychosocialCriteriaManager.CheckIfARTPreparationExists(patientId);
            }
            catch (Exception e)
            {
                // var msg = new JavaScriptSerializer().Serialize(e.Message);
                throw e.InnerException;

            }
            return result;

        }

        [WebMethod(EnableSession = true)]
        public string AddPatientPsychosocialCriteria(int patientId, int patientmastervisitId, bool benefitART, bool alcohol, bool depression, bool disclosure, bool administerART, bool adherence, bool locator,bool caregiver)
        {
            string msg = "";
            int result = 0;
            PatientPsychosocialCriteriaManager _PatientPsychosocialCriteriaManager = new PatientPsychosocialCriteriaManager();


            try
            {
                PatientPsychoscialCriteria _patientPsychosocialCriteria = new PatientPsychoscialCriteria()
                {
                    PatientId = patientId,
                    PatientMasterVisitId = patientmastervisitId,
                    BenefitART = benefitART,
                    Alcohol = alcohol,
                    Depression = depression,
                    Disclosure = disclosure,
                    AdministerART = administerART,
                    //effectsART=effectsART,
                    //dependents=dependents,
                    AdherenceBarriers = adherence,
                    AccurateLocator = locator,
                    startART=caregiver
                };

                result = _PatientPsychosocialCriteriaManager.AddPreparation(_patientPsychosocialCriteria);
                if (result > 0)
                {
                    msg = "Patient Psychosocial criteria assessment completed!";
                    msg = new JavaScriptSerializer().Serialize(msg);
                }
            }
            catch (Exception e)
            {
                msg = e.Message;
                msg = new JavaScriptSerializer().Serialize(msg);
            }
            return msg;
        }

        [WebMethod(EnableSession = true)]
        public string EditPatientPsychosocialCriteria(int patientId, int patientmastervisitId, bool benefitART, bool Alcohol, bool depression, bool disclosure, bool administerART, bool adherence, bool locator)
        {
            string msg = "";
            int result = 0;
            PatientPsychosocialCriteriaManager _PatientPsychosocialCriteriaManager = new PatientPsychosocialCriteriaManager();


            try
            {
                PatientPsychoscialCriteria _patientPsychosocialCriteria = new PatientPsychoscialCriteria()
                {
                    PatientId = patientId,
                    PatientMasterVisitId = patientmastervisitId,
                    BenefitART = benefitART,
                    Alcohol = Alcohol,
                    Depression = depression,
                    Disclosure = disclosure,
                    AdministerART = administerART,
                    AdherenceBarriers = adherence,
                    AccurateLocator = locator
                };

                result = _PatientPsychosocialCriteriaManager.EditPreparation(_patientPsychosocialCriteria);
                if (result > 0)
                {
                    msg = "Patient Psychosocial criteria assessment update completed!";
                    msg = new JavaScriptSerializer().Serialize(msg);
                }
            }
            catch (Exception e)
            {
                msg = e.Message;
                msg = new JavaScriptSerializer().Serialize(msg);
            }
            return msg;
        }


        [WebMethod(EnableSession = true)]
        public List<PatientPsychoscialCriteria> GetPatientPsychosocialCriteria(int patientId)
        {
            PatientPsychosocialCriteriaManager _PatientPsychosocialCriteriaManager = new PatientPsychosocialCriteriaManager();
            try
            {
                return _PatientPsychosocialCriteriaManager.GetPatientPsychosocialCriteriaDetails(patientId);
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }

        [WebMethod(EnableSession = true)]
        public string DeletePsychosocial(int id)
        {
            int result = 0;
            string msg = "";

            PatientPsychosocialCriteriaManager _PatientPsychosocialCriteriaManager = new PatientPsychosocialCriteriaManager();

            try
            {
                result = _PatientPsychosocialCriteriaManager.DeletePreparation(id);
                if (result > 0)
                {
                    msg = "Psychosocial criteria deleted successfuly";
                    msg = new JavaScriptSerializer().Serialize(msg);

                }
            }
            catch (Exception e)
            {
                msg = e.Message;
                msg = new JavaScriptSerializer().Serialize(msg);

            }
            return msg;
        }


        /*///////////////////////////////////////////////////////////////////////////////*/

        [WebMethod(EnableSession = true)]
        public int CheckIfSupportSystemCriteriaExists(int patientId)
        {
            int result = 0;
            string msg = "";

            PatientSupportSystemCriteriaManager _PatientSupportSystemCriteriaManager = new PatientSupportSystemCriteriaManager();

            try
            {

                result = _PatientSupportSystemCriteriaManager.checkIfARTPreparationExists(patientId);
            }
            catch (Exception e)
            {
                // var msg = new JavaScriptSerializer().Serialize(e.Message);
                throw e.InnerException;

            }
            return result;

        }

        [WebMethod(EnableSession = true)]
        public string AddPatientSupportSystemCriteria(int patientId, int patientmastervisitId, bool takingART, bool TSIdentified, bool smsreminder, bool othersupport)
        {
            string msg = "";
            int result = 0;
            PatientSupportSystemCriteriaManager _PatientSupportSystemCriteriaManager = new PatientSupportSystemCriteriaManager();


            try
            {
                PatientSupportSystemCriteria _patientSupportSystemCriteria = new PatientSupportSystemCriteria()
                {
                    PatientId = patientId,
                    PatientMasterVisitId = patientmastervisitId,
                    TakingART = takingART,
                    TSIdentified = TSIdentified,
                    EnrollSMSReminder = smsreminder,
                    OtherSupportSystems = othersupport
                };

                result = _PatientSupportSystemCriteriaManager.AddPreparation(_patientSupportSystemCriteria);
                if (result > 0)
                {
                    msg = "Patient Psychosocial criteria assessment completed!";
                    msg = new JavaScriptSerializer().Serialize(msg);
                }
            }
            catch (Exception e)
            {
                msg = e.Message;
                msg = new JavaScriptSerializer().Serialize(msg);
            }
            return msg;
        }

        [WebMethod(EnableSession = true)]
        public string EditPatientSupportSystemCriteria(int patientId, int patientmastervisitId, bool takingART, bool TSIdentified, bool smsreminder, bool othersupport)
        {
            string msg = "";
            int result = 0;

            PatientSupportSystemCriteriaManager _PatientSupportSystemCriteriaManager = new PatientSupportSystemCriteriaManager();


            try
            {
                PatientSupportSystemCriteria _PatientSupportSystemCriteria = new PatientSupportSystemCriteria()
                {
                    PatientId = patientId,
                    PatientMasterVisitId = patientmastervisitId,
                    TakingART = takingART,
                    TSIdentified = TSIdentified,
                    EnrollSMSReminder = smsreminder,
                    OtherSupportSystems = othersupport
                };

                result = _PatientSupportSystemCriteriaManager.EditPreparation(_PatientSupportSystemCriteria);
                if (result > 0)
                {
                    msg = "Patient Support System criteria assessment update completed!";
                    msg = new JavaScriptSerializer().Serialize(msg);
                }
            }
            catch (Exception e)
            {
                msg = e.Message;
                msg = new JavaScriptSerializer().Serialize(msg);
            }
            return msg;
        }


        [WebMethod(EnableSession = true)]
        public List<PatientSupportSystemCriteria> GetPatientSupportSystemCriteria(int patientId)
        {
            PatientSupportSystemCriteriaManager _PatientSupportSystemCriteriaManager = new PatientSupportSystemCriteriaManager();
            try
            {
                return _PatientSupportSystemCriteriaManager.GetPatientSupportSystemCriteriaDetails(patientId);
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }

        [WebMethod(EnableSession = true)]
        public string DeleteSupportSystemCriteria(int id)
        {
            int result = 0;
            string msg = "";

            PatientSupportSystemCriteriaManager _PatientSupportSystemCriteriaManager = new PatientSupportSystemCriteriaManager();

            try
            {
                result = _PatientSupportSystemCriteriaManager.DeletePreparation(id);
                if (result > 0)
                {
                    msg = "Psychosocial criteria deleted successfuly";
                    msg = new JavaScriptSerializer().Serialize(msg);

                }
            }
            catch (Exception e)
            {
                msg = e.Message;
                msg = new JavaScriptSerializer().Serialize(msg);

            }
            return msg;
        }



    }

}

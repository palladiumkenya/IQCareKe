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
            //string msg = "";
            PatientPsychosocialCriteriaManager patientPsychosocialCriteriaManager = new PatientPsychosocialCriteriaManager();

            try {

                result = patientPsychosocialCriteriaManager.CheckIfARTPreparationExists(patientId);
            }
            catch (Exception e)
            {
                // var msg = new JavaScriptSerializer().Serialize(e.Message);
                throw e.InnerException;

            }
            return result;

        }

        [WebMethod(EnableSession = true)]
        public string AddPatientPsychosocialCriteria(int patientId, int patientmastervisitId, bool benefitART, bool alcohol, bool depression, bool disclosure, bool administerART, bool effectsART, bool dependents, bool adherence, bool locator,bool caregiver)
        {
            string msg = "";
            int result = 0;
            
            PatientPsychosocialCriteriaManager patientPsychosocialCriteriaManager = new PatientPsychosocialCriteriaManager();


            try
            {
                PatientPsychoscialCriteria patientPsychosocialCriteria = new PatientPsychoscialCriteria()
                {
                    PatientId = patientId,
                    PatientMasterVisitId = patientmastervisitId,
                    BenefitART = benefitART,
                    Alcohol = alcohol,
                    Depression = depression,
                    Disclosure = disclosure,
                    AdministerART = administerART,
                    effectsART = effectsART,
                    dependents = dependents,
                    AdherenceBarriers = adherence,
                    AccurateLocator = locator,
                    startART=caregiver
                };

                int isInserted = 0;
                isInserted = patientPsychosocialCriteriaManager.CheckIfARTPreparationExists(patientId);
                if (isInserted <1)
                {
                    result = patientPsychosocialCriteriaManager.AddPreparation(patientPsychosocialCriteria);
                    if (result > 0)
                    {
                        msg = "Patient Psychosocial criteria assessment completed!";
                        msg = new JavaScriptSerializer().Serialize(msg);
                    }
                }
                else
                {
                    msg = "Patient Psychosocial criteria assessment has already been completed!";
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
            PatientPsychosocialCriteriaManager patientPsychosocialCriteriaManager = new PatientPsychosocialCriteriaManager();


            try
            {
                PatientPsychoscialCriteria patientPsychosocialCriteria = new PatientPsychoscialCriteria()
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

                result = patientPsychosocialCriteriaManager.EditPreparation(patientPsychosocialCriteria);
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
            PatientPsychosocialCriteriaManager patientPsychosocialCriteriaManager = new PatientPsychosocialCriteriaManager();
            try
            {
                return patientPsychosocialCriteriaManager.GetPatientPsychosocialCriteriaDetails(patientId);
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

            PatientPsychosocialCriteriaManager patientPsychosocialCriteriaManager = new PatientPsychosocialCriteriaManager();

            try
            {
                result = patientPsychosocialCriteriaManager.DeletePreparation(id);
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
            //string msg = "";

            PatientSupportSystemCriteriaManager patientSupportSystemCriteriaManager = new PatientSupportSystemCriteriaManager();

            try
            {

                result = patientSupportSystemCriteriaManager.checkIfARTPreparationExists(patientId);
            }
            catch (Exception e)
            {
                // var msg = new JavaScriptSerializer().Serialize(e.Message);
                throw e.InnerException;

            }
            return result;

        }

        [WebMethod(EnableSession = true)]
        public string AddPatientSupportSystemCriteria(int patientId, int patientmastervisitId, bool takingART,bool supportGroup, bool TSIdentified, bool smsreminder, bool othersupport)
        {
            string msg = "";
            int result = 0;
            PatientSupportSystemCriteriaManager patientSupportSystemCriteriaManager = new PatientSupportSystemCriteriaManager();


            try
            {
                PatientSupportSystemCriteria patientSupportSystemCriteria = new PatientSupportSystemCriteria()
                {
                    PatientId = patientId,
                    PatientMasterVisitId = patientmastervisitId,
                    TakingART = takingART,
                    supportGroup=supportGroup,
                    TSIdentified = TSIdentified,
                    EnrollSMSReminder = smsreminder,
                    OtherSupportSystems = othersupport
                };

                int isInserted = 0;
                isInserted = patientSupportSystemCriteriaManager.checkIfARTPreparationExists(patientId);
                if (isInserted < 1)
                {
                    result = patientSupportSystemCriteriaManager.AddPreparation(patientSupportSystemCriteria);
                    if (result > 0)
                    {
                        msg = "Patient Psychosocial criteria assessment completed!";
                        msg = new JavaScriptSerializer().Serialize(msg);
                    }
                }
                else
                {
                    msg = "Patient Psychosocial criteria assessment already completed!";
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
        public string EditPatientSupportSystemCriteria(int patientId, int patientmastervisitId, bool takingART,bool supportGroup, bool TSIdentified, bool smsreminder, bool othersupport)
        {
            string msg = "";
            int result = 0;

            PatientSupportSystemCriteriaManager patientSupportSystemCriteriaManager = new PatientSupportSystemCriteriaManager();


            try
            {
                PatientSupportSystemCriteria patientSupportSystemCriteria = new PatientSupportSystemCriteria()
                {
                    PatientId = patientId,
                    PatientMasterVisitId = patientmastervisitId,
                    TakingART = takingART,
                    supportGroup=supportGroup,
                    TSIdentified = TSIdentified,
                    EnrollSMSReminder = smsreminder,
                    OtherSupportSystems = othersupport
                };

                result = patientSupportSystemCriteriaManager.EditPreparation(patientSupportSystemCriteria);
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
            PatientSupportSystemCriteriaManager patientSupportSystemCriteriaManager = new PatientSupportSystemCriteriaManager();
            try
            {
                return patientSupportSystemCriteriaManager.GetPatientSupportSystemCriteriaDetails(patientId);
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

            PatientSupportSystemCriteriaManager patientSupportSystemCriteriaManager = new PatientSupportSystemCriteriaManager();

            try
            {
                result = patientSupportSystemCriteriaManager.DeletePreparation(id);
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

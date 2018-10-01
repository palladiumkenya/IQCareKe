using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using IQCare.CCC.UILogic.Adherence;
using Interface.CCC;
using Application.Presentation;
using IQCare.CCC.UILogic.Visit;
using Entities.CCC.Adherence;

namespace IQCare.Web.CCC.WebService
{
    /// <summary>
    /// Summary description for AdherenceService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AdherenceService : System.Web.Services.WebService
    {
        private string Msg { get; set; }
        private int Result { get; set; }
        private int EncounterResult = 0;
        private int encounterTypeId = 0;
        [WebMethod(EnableSession = true)]
        public string addAdherenceHIVStatus(int patientId, int patientMasterVisitId, int createdBy, int AcceptedStatus, int DisclosureComplete)
        {
            IPatientEncounter patientEncounter = (IPatientEncounter)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientEncounter, BusinessProcess.CCC");
            PatientEncounterManager patientEncounterManager = new PatientEncounterManager();
            encounterTypeId = patientEncounterManager.GetPatientEncounterId("EncounterType", "Adherence-Barriers");
            var foundEncounter = patientEncounterManager.GetEncounterIfExists(Convert.ToInt32(patientId), Convert.ToInt32(patientMasterVisitId), Convert.ToInt32(encounterTypeId));
            if (foundEncounter != null)
            {
                EncounterResult = foundEncounter.Id;
            }
            else
            {
                    EncounterResult = patientEncounterManager.AddpatientEncounter(Convert.ToInt32(patientId),Convert.ToInt32(patientMasterVisitId),
                        patientEncounterManager.GetPatientEncounterId("EncounterType", "Adherence-Barriers"), 203,createdBy);
            }
            if(EncounterResult > 0)
            {
                try
                {
                    var AL = new AdherenceLogic();
                    Result = AL.addAdherenceHIVStatus(patientId, patientMasterVisitId, createdBy, AcceptedStatus, DisclosureComplete);
                    if (Result > 0)
                    {
                        Msg = " HIV Status Added Successfully";
                    }
                }
                catch (Exception e)
                {
                    Msg = e.Message;
                }
            }
            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public string addUnderstandHIV(int patientId, int patientMasterVisitId, int createdBy, int understandHIVEffects, int understandART, int understandSideEffects,
            int understandAdherenceBenefits, int understandConsequences)
        {
            try
            {
                var AL = new AdherenceLogic();
                Result = AL.addUnderstandingHIV(patientId, patientMasterVisitId, createdBy, understandHIVEffects, understandART, understandSideEffects, understandAdherenceBenefits, understandConsequences);
                if(Result>0)
                {
                    Msg = "Understanding HIV infection and ART saved successfully";
                }
            }
            catch(Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public string addAdherenceDailyRoutine(int patientId, int patientMasterVisitId, int createdBy,string typicalDay, string medicineAdministration, string travelCase, string primaryCaregiver)
        {
            try
            {
                var AL = new AdherenceLogic();
                Result = AL.addDailyRoutine(patientId, patientMasterVisitId, createdBy, typicalDay, medicineAdministration, travelCase, primaryCaregiver);
                if (Result > 0)
                {
                    Msg = "Daily routine added successfully";
                }
            }
            catch(Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public string addAdherenceReferrals(int patientId, int patientMasterVisitId, int createdBy,int patientReferred, int appointmentsAttended, string experience)
        {
            try
            {
                var AL = new AdherenceLogic();
                Result = AL.addReferrals(patientId,patientMasterVisitId, createdBy,patientReferred,appointmentsAttended,experience);
                if (Result > 0)
                {
                    Msg = "Referrals and Networks added successfully";
                }
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public string addAdherenceScreen(int patientId, int patientMasterVisitId, int createdBy,float total, string depressionSeverity, string recommendedManagement)
        {
            try
            {
                var AL = new AdherenceLogic();
                Result = AL.addAdherenceScreening(patientId, patientMasterVisitId, createdBy,total,depressionSeverity,recommendedManagement);
                if (Result > 0)
                {
                    Msg = "Screening added successfully";
                }
            }
            catch(Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }

        [WebMethod(EnableSession = true)]
        public string addPsychosocialCircumstances(int patientId, int patientMasterVisitId, int CreatedBy,string livingWith, string aware,int supportSystem, string supportSystemNotes,
            int relationshipChanges, string relationshipChangesNotes, int bothered, string botheredNotes, int treatedDifferently, string treatedDifferentlyNotes,
            int interferenceStigma, string interferenceStigmaNotes, int stoppedMedication, string stoppedMedicationNotes)
        {
            try
            {
                var AL = new AdherenceLogic();
                Result = AL.addPsychosocialCircumstances(patientId, patientMasterVisitId, CreatedBy, livingWith, aware, supportSystem, supportSystemNotes, relationshipChanges,
                    relationshipChangesNotes, bothered, botheredNotes, treatedDifferently, treatedDifferentlyNotes, interferenceStigma, interferenceStigmaNotes, stoppedMedication,
                    stoppedMedicationNotes);
                if (Result > 0)
                {
                    Msg = "Psychosocial Circumstances Added";
                }
            }
            catch(Exception e)
            {
                Msg = e.Message;
            }
            
            return Msg;
        }

        [WebMethod]
        public List<HIVStatus> getHIVStatus(int patientId, int patientMasterVisitId)
        {
            var AL = new AdherenceLogic();
            List<HIVStatus> HIVStatusList = new List<HIVStatus>();
            HIVStatusList = AL.getHIVStatus(patientId, patientMasterVisitId);
            return HIVStatusList;
        }

        [WebMethod]
        public string updateHIVStatus(int patientId, int patientMasterVisitId, int createdBy, int AcceptedStatus, int DisclosureComplete, int statusId)
        {
            try
            {
                var HS = new AdherenceLogic();
                Result = HS.updateHIVStatus(patientId,patientMasterVisitId,createdBy,AcceptedStatus,DisclosureComplete,statusId);
                if (Result > 0)
                {
                    Msg = "Awareness of HIV status updated";
                }
            }
            catch(Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }
        [WebMethod]
        public string updateDailyRoutine(int patientId, int patientMasterVisitId, int createdBy, string typicalDay, string medicineAdministration, string travelCase, string primaryCaregiver, int DRId)
        {
            try
            {
                var DR = new AdherenceLogic();
                Result = DR.updateDailyRoutine(patientId, patientMasterVisitId,createdBy, typicalDay, medicineAdministration, travelCase, primaryCaregiver, DRId);
                if (Result > 0)
                {
                    Msg = "Daily routine updated";
                }
            }
            catch(Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }
        [WebMethod]
        public string updateUnderstandingHIV(int patientId, int patientMasterVisitId, int createdBy, int understandHIVEffects, int understandART, int understandSideEffects,
            int understandAdherenceBenefits, int understandConsequences, int uId)
        {
            try
            {
                var UH = new AdherenceLogic();
                Result = UH.updateUnderstandingHIV(patientId, patientMasterVisitId, createdBy, understandHIVEffects, understandART, understandSideEffects, understandAdherenceBenefits, understandConsequences, uId);
                if(Result > 0)
                {
                    Msg = "Understanding of HIV infection and ART updated";
                }
            }
            catch(Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }
        [WebMethod]
        public string updatePsychosocialCircumstances(int patientId, int patientMasterVisitId, int CreatedBy, string livingWith, string aware, int supportSystem, string supportSystemNotes, int relationshipChanges,
            string relationshipChangesNotes, int bothered, string botheredNotes, int treatedDifferently,string treatedDifferentlyNotes, int interferenceStigma, string interferenceStigmaNotes,
            int stoppedMedication, string stoppedMedicationNotes, int PCId)
        {
            try
            {
                var PC = new AdherenceLogic();
                Result = PC.updatePsychosocialCircumetances(patientId,patientMasterVisitId, CreatedBy, livingWith, aware, supportSystem, supportSystemNotes, relationshipChanges,
            relationshipChangesNotes, bothered, botheredNotes, treatedDifferently, treatedDifferentlyNotes, interferenceStigma, interferenceStigmaNotes,
            stoppedMedication, stoppedMedicationNotes, PCId);
                if(Result > 0)
                {
                    Msg = "Psychosocial circumstances updated";
                }
            }
            catch(Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }
        [WebMethod]
        public string updateReferrals(int patientId, int patientMasterVisitId, int createdBy, int patientReferred, int appointmentsAttended, string experience, int RefId)
        {
            try
            {
                var REFS = new AdherenceLogic();
                Result = REFS.updateReferrals(patientId, patientMasterVisitId, createdBy, patientReferred, appointmentsAttended, experience, RefId);
                if(Result > 0)
                {
                    Msg = "Referrals and networks updated";
                }
            }
            catch(Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }
        public string updateAdherenceScreening(int patientId, int patientMasterVisitId, int createdBy, float total, string depressionSeverity, string recommendedManagement,int ScreeningId)
        {
            try
            {
                var AScreening = new AdherenceLogic();
                Result = AScreening.updateAdherenceScreening(patientId, patientMasterVisitId, createdBy, total, depressionSeverity, recommendedManagement, ScreeningId);
                if(Result > 0)
                {
                    Msg = "Depression health screening updated";
                }
            }
            catch(Exception e)
            {
                Msg = e.Message;
            }
            return Msg;
        }
    }
}

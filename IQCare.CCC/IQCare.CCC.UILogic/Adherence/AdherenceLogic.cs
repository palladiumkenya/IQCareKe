using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.CCC.Adherence;
using Application.Presentation;
using Interface.CCC.Adherence;

namespace IQCare.CCC.UILogic.Adherence
{
    public class AdherenceLogic
    {
        public int Result;
        private IAdherence _adherence = (IAdherence)ObjectFactory.CreateInstance("BusinessProcess.CCC.Adherence.BAdherence, BusinessProcess.CCC");
        public int addAdherenceHIVStatus(int patientId, int patientMasterVisitId, int createdBy, int AcceptedStatus, int DisclosureComplete)
        {
            HIVStatus status = new HIVStatus()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                AcceptedStatus = AcceptedStatus,
                DisclosureComplete = DisclosureComplete
            };
            Result = _adherence.AddHIVStatus(status);
            return Result;
        }

        public int addAdherenceScreening(int patientId, int patientMasterVisitId, int createdBy, float total, string depressionSeverity, string recommendedManagement)
        {
            AdherenceScreening AS = new AdherenceScreening()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                Total = total,
                DepressionSeverity = depressionSeverity,
                RecommendedManagement = recommendedManagement
            };
            Result = _adherence.AddAdherenceScreening(AS);
            return Result;
        }
        public int addPsychosocialCircumstances(int patientId, int patientMasterVisitId, int createdBy, string livingWith, string aware, int supportSystem, string supportSystemNotes,
            int relationshipChanges, string relationshipChangesNotes, int bothered, string botheredNotes, int treatedDifferently, string treatedDifferentlyNotes, int interferenceStigma,
            string interferenceStigmaNotes, int stoppedMedication, string stoppedMedicationNotes)
        {
            PsychosocialCircumstances PS = new PsychosocialCircumstances()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                LivingWith = livingWith,
                Aware = aware,
                SupportSystem = supportSystem,
                SupportSystemNotes = supportSystemNotes,
                RelationshipChanges = relationshipChanges,
                RelationshipChangesNotes = relationshipChangesNotes,
                Bothered = bothered,
                BotheredNotes = botheredNotes,
                TreatedDifferently = treatedDifferently,
                TreatedDifferentlyNotes = treatedDifferentlyNotes,
                InterferenceStigma = interferenceStigma,
                StoppedMedication = stoppedMedication,
                StoppedMedicationNotes = stoppedMedicationNotes
            };
            Result = _adherence.AddPsychosocialCircumstances(PS);
            return Result;
        }
        public int addDailyRoutine(int patientId, int patientMasterVisitId, int createdBy, string typicalDay, string medicineAdministration, string travelCase, string primaryCaregiver)
        {
            DailyRoutine DR = new DailyRoutine()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                TypicalDay = typicalDay,
                MedicineAdministration = medicineAdministration,
                TravelCase = travelCase,
                PrimaryCaregiver = primaryCaregiver
            };
            Result = _adherence.AddDailyRoutine(DR);
            return Result;
        }
        public int addUnderstandingHIV(int patientId, int patientMasterVisitId, int createdBy, int understandHIVEffects, int understandART, int understandSideEffects, int understandAdherenceBenefits,
            int understandConsequences)
        {
            UnderstandingHIV UH = new UnderstandingHIV()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                UnderstandHIVEffects = understandHIVEffects,
                UnderstandART = understandART,
                UnderstandSideEffect = understandSideEffects,
                UnderstandAdherenceBenefits = understandAdherenceBenefits,
                UnderstandConsequences = understandConsequences
            };
            Result = _adherence.AddUnderstandingHIV(UH);
            return Result;
        }

        public int addReferrals(int patientId, int patientMasterVisitId, int createdBy, int patientReferred, int appointmentsAttended, string experience)
        {
            Referrals REFS = new Referrals()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                CreatedBy = createdBy,
                PatientReferred = patientReferred,
                AppointmentsAttended = appointmentsAttended,
                Experience = experience
            };
            Result = _adherence.AddReferrals(REFS);
            return Result;
        }

        public List<HIVStatus> getHIVStatus(int PatientID, int PatientMasterVisitID)
        {

            List<HIVStatus> HIVStatusList = new List<HIVStatus>();
            try
            {
                HIVStatusList = _adherence.getHIVStatus(PatientID, PatientMasterVisitID);
            }
            catch(Exception)
            {
                throw;
            }
            return HIVStatusList;
        }
        public List<UnderstandingHIV> getUnderstandingHIV(int patientId, int patientMasterVisitId)
        {
            List<UnderstandingHIV> understandingHIVList = new List<UnderstandingHIV>();
            try
            {
                understandingHIVList = _adherence.getUnderstandingHIV(patientId, patientMasterVisitId);
            }
            catch
            {
                throw;
            }
            return understandingHIVList;
        }
        public List<PsychosocialCircumstances> getPsychosocialCircumstances(int patientId, int patientMasterVisitId)
        {
            List<PsychosocialCircumstances> psychosocialList = new List<PsychosocialCircumstances>();
            try
            {
                psychosocialList = _adherence.getPsychosocialCircumstances(patientId, patientMasterVisitId);
            }
            catch
            {
                throw;
            }
            return psychosocialList;
        }
        public List<Referrals> getReferrals(int patientId, int patientMasterVisitId)
        {
            List<Referrals> referralsList = new List<Referrals>();

            referralsList = _adherence.getReferrals(patientId, patientMasterVisitId);

            return referralsList;
        }
        public List<AdherenceScreening> getAdherenceScreening(int patientId, int patientMasterVisitId)
        {
            List<AdherenceScreening> screeningList = new List<AdherenceScreening>();
            try
            {
                screeningList = _adherence.getAdherenceScreening(patientId, patientMasterVisitId);
            }
            catch
            {
                throw;
            }
            return screeningList;
        }
        public List<DailyRoutine> getDailyRoutine(int patientId, int patientMasterVisitId)
        {
            List<DailyRoutine> dailyRoutineList = new List<DailyRoutine>();
            try
            {
                dailyRoutineList = _adherence.getDailyRoutine(patientId, patientMasterVisitId);
            }
            catch
            {
                throw;
            }
            return dailyRoutineList;
        }

        public int updateHIVStatus(int patientId, int patientMasterVisitId, int createdBy, int acceptedStatus, int disclosureComplete, int StatusId)
        {
            HIVStatus HS = new HIVStatus()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                AcceptedStatus = acceptedStatus,
                DisclosureComplete = disclosureComplete,
                Id = StatusId
            };
            Result = _adherence.updateHIVStatus(HS);
            return Result;
        }
        public int updateDailyRoutine(int patientId, int patientMasterVisitId, int createdBy, string typicalDay, string medicineAdministration, string travelCase, string primaryCaregiver, int DRId)
        {
            DailyRoutine DR = new DailyRoutine()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                TypicalDay = typicalDay,
                MedicineAdministration = medicineAdministration,
                TravelCase = travelCase,
                PrimaryCaregiver = primaryCaregiver,
                Id = DRId
            };
            Result = _adherence.updateDailyRoutine(DR);
            return Result;
        }
        public int updateUnderstandingHIV(int patientId, int patientMasterVisitId, int createdBy, int understandHIVEffects, int understandART, int understandSideEffects, int understandAdherenceBenefits,
            int understandConsequences, int uId)
        {
            UnderstandingHIV UH = new UnderstandingHIV()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                UnderstandHIVEffects = understandHIVEffects,
                UnderstandART = understandART,
                UnderstandSideEffect = understandSideEffects,
                UnderstandAdherenceBenefits = understandAdherenceBenefits,
                UnderstandConsequences = understandConsequences,
                Id = uId
            };
            Result = _adherence.updateUnderstandingHIV(UH);
            return Result;
        }
        public int updatePsychosocialCircumetances(int patientId, int patientMasterVisitId, int createdBy, string livingWith, string aware, int supportSystem, string supportSystemNotes,
            int relationshipChanges, string relationshipChangesNotes, int bothered, string botheredNotes, int treatedDifferently, string treatedDifferentlyNotes, int interferenceStigma,
            string interferenceStigmaNotes, int stoppedMedication, string stoppedMedicationNotes, int PCId)
        {
            PsychosocialCircumstances PC = new PsychosocialCircumstances()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                LivingWith = livingWith,
                Aware = aware,
                SupportSystem = supportSystem,
                SupportSystemNotes = supportSystemNotes,
                RelationshipChanges = relationshipChanges,
                RelationshipChangesNotes = relationshipChangesNotes,
                Bothered = bothered,
                BotheredNotes = botheredNotes,
                TreatedDifferently = treatedDifferently,
                TreatedDifferentlyNotes = treatedDifferentlyNotes,
                InterferenceStigma = interferenceStigma,
                InterferenceStigmaNotes = interferenceStigmaNotes,
                StoppedMedication = stoppedMedication,
                StoppedMedicationNotes = stoppedMedicationNotes,
                Id = PCId
            };
            Result = _adherence.updatePsychosocialCircumstances(PC);
            return Result;
        }
        public int updateReferrals(int patientId, int patientMasterVisitId, int createdBy, int patientReferred, int appointmentsAttended, string experience, int RefId)
        {
            Referrals refs = new Referrals()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                PatientReferred = patientReferred,
                AppointmentsAttended = appointmentsAttended,
                Id = RefId
            };
            Result = _adherence.updateReferrals(refs);
            return Result;
        }
        public int updateAdherenceScreening(int patientId, int patientMasterVisitId, int createdBy, float total, string depressionSeverity, string recommendedManagement, int ScreeningId)
        {
            AdherenceScreening _AS = new AdherenceScreening()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                Total = total,
                DepressionSeverity = depressionSeverity,
                RecommendedManagement = recommendedManagement,
                Id = ScreeningId
            };
            Result = _adherence.updateAdherenceScreening(_AS);
            return Result;
        }
    }
}

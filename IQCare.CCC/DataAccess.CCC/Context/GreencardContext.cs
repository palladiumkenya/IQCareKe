using DataAccess.Base;
using Entities.Common;
using System.Data.Common;
using System.Data.Entity;
using Entities.CCC.Baseline;
using Entities.CCC.Encounter;
using Entities.CCC.Enrollment;
using Entities.CCC.Screening;
using Entities.CCC.Triage;
using Entities.CCC.Visit;
using DataAccess.Context;
using Entities.CCC.Appointment;
using Entities.CCC.Tb;
using Entities.PatientCore;
using Interface.CCC.Encounter;
using Entities.CCC.Lookup;
using Entities.CCC.Assessment;
using Entities.CCC.Interoperability;
using Entities.CCC.pharmacy;

namespace DataAccess.CCC.Context
{
    public class GreencardContext : BaseContext
    {
        public GreencardContext() : base((DbConnection)DataMgr.GetConnection(), true)
        {
            Configuration.ProxyCreationEnabled = false;
            // DataMgr.OpenDecryptedSession(base.Database.Connection);
            Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer<GreencardContext>(null);
        }


        public DbSet<PatientEntity> Patients { get; set; }
        public DbSet<PersonContact> PatientContacts { get; set; }
        public DbSet<PatientOVCStatus> PatientOvcStatuses { get; set; }
        public DbSet<PatientPopulation> PatientPopulations { get; set; }

        //Enrollment
        public DbSet<PatientEntryPoint> PatientEntryPoint { get; set; }
        public DbSet<PatientEntityIdentifier> PatientIdentifiers { get; set; }
        public DbSet<PatientEntityEnrollment> PatientEntityEnrollments { get; set; }
        public DbSet<PersonGreenCardLookup> PersonGreenCardLookup { get; set; }
        public DbSet<ServiceAreaIdentifiers> ServiceAreaIdentifiers { get; set; }
        public DbSet<Identifier> Identifiers { get; set; }
        public DbSet<PatientReEnrollment> ReEnrollments { get; set; }
        public DbSet<HivReConfirmatoryTest> HivReConfirmatoryTests { get; set; }
        public DbSet<PatientArtDistribution> PatientArtDistributions { get; set; }
        public DbSet<PersonIdentifier> PersonIdentifiers { get; set; }

        //Screening
        public DbSet<PatientScreening> PatientScreenings { get; set; }

        //Triage
        public DbSet<PatientAdverseEvent> PatientAdverseEvents { get; set; }
        public DbSet<Entities.CCC.Triage.PatientAllergy> PatientAllergies { get; set; }
        public DbSet<PatientChronicIllness> PatientChronicIllnesses { get; set; }
        public DbSet<PatientVital> PatientVitals { get; set; }
        public DbSet<Entities.CCC.Triage.PatientFamilyPlanningMethod> PatientFamilyPlanningMethod { get; set; }
        public DbSet<Entities.CCC.Triage.PatientFamilyPlanning> PatientFamilyPlanning { get; set; }
        public DbSet<PatientPreganancy> PatientPregnancy { get; set; }
        public DbSet<PatientPregnancyIndicator> PatientPregnancyIndicator { get; set; }
        public DbSet<PatientAdverseEventOutcome> PatientAdverseEventOutcome { get; set; }

        //Visit
        public DbSet<PatientMasterVisit> PatientMasterVisit { get; set; }
        public DbSet<Entities.CCC.Visit.PatientVisit> PatientVisit { get; set; }
        public DbSet<Entities.CCC.Visit.PatientEncounter> PatientEncounters { get; set; }
      


        //Baseline Entities
        //public DbSet<PatientArtUseHistory> PatientArtUseHistories { get; set; }
        public DbSet<PatientDisclosure> PatientDisclosures { get; set; }
        public DbSet<PatientHivEnrollmentBaseline> PatientHivEnrollmentBaselines { get; set; }
        public DbSet<PatientHivTesting> PatientHivTestings { get; set; }
        public DbSet<PatientTransferIn> PatientTransferIns { get; set; }
        public DbSet<PatientHivDiagnosis> DiagnosisArvHistory { get; set; }
        public DbSet<PatientBaselineAssessment> PatientArtInitiation { get; set; }
        public DbSet<PatientArvHistory> PatientArtUseHistory { get; set; }
        public DbSet<INHProphylaxis> INHProphylaxis { get; set; }
        public DbSet<PatientTreatmentInitiation> PatientTreatmentInitiations { get; set; }


        //Encounter
        public DbSet<ComplaintsHistory> ComplaintsHistory { get; set; }
        public DbSet<PatientAdherenceOutcome> PatientAdherenceOutcome { get; set; }
        public DbSet<PatientCareEnding> PatientCareEnding { get; set; }
        public DbSet<PatientClinicalDiagnosis> PatientClinicalDiagnosis { get; set; }
        public DbSet<PatientClinicalNotes> PatientClinicalNotes { get; set; }
        //public DbSet<PatientFamilyPlanning> PatientFamilyPlanning { get; set; }
        //public DbSet<PatientFamilyPlanningMethod> PatientFamilyPlanningMethod { get; set; }
        public DbSet<PatientPhdp> PatientPhdp { get; set; }
        public DbSet<PatientProphylaxis> PatientProphylaxis { get; set; }
        public DbSet<PatientReferral> PatientReferral { get; set; }
        public DbSet<PatientTbTreatmentTracker> PatientTbTreatmentTracker { get; set; }
        public DbSet<PatientTreatmentEventTracker> PatientTreatmentEventTracker { get; set; }
        public DbSet<PatientVaccination> PatientVaccination { get; set; }
        public DbSet<PhysicalExamination> PhysicalExamination { get; set; }
        public DbSet<Pregnancy> Pregnancies { get; set; }
        public DbSet<PregnancyIndicator> PregnancyIndicators { get; set; }
        public DbSet<LabOrderEntity> LabOrder { get; set; }
       
        public DbSet<LabResultsEntity> LabResults { get; set; }
        public DbSet<PatientAdherenceAssessment> PatientAdherenceAssessments { get; set; }
        public DbSet<PatientLinkage> PatientLinkages { get; set; }
        public DbSet<PatientWhoStage> PatientWhoStages { get; set; }

        //Appointment and Labs
        public DbSet<PatientAppointment> PatientAppointments { get; set; }
        public DbSet<AppointmentSummary> AppointmentSummary { get; set; }
        public DbSet<BlueCardAppointment> BlueCardAppointments { get; set; }
        public DbSet<PatientLabTracker> PatientLabTracker { get; set; }
        public DbSet<LabDetailsEntity> PatientLabDetails { get; set; }

        //Consent
        public DbSet<Entities.CCC.Consent.PatientConsent> PatientConsents { get; set; }

        //TB ICF/IPT
        public DbSet<PatientIcf> PatientIcfs { get; set; }
        public DbSet<PatientIcfAction> PatientIcfActions { get; set; }
        public DbSet<PatientIpt> PatientIpts { get; set; }
        public DbSet<PatientIptOutcome> PatientIptOutcomes { get; set; }
        public DbSet<PatientIptWorkup> PatientIptWorkups { get; set; }


        //Patient categorization
        public DbSet<PatientCategorization> PatientCategorizations { get; set; }

        // Patient Assessment| ART Treatment Preparation
        public DbSet<PatientPsychoscialCriteria> PatientPsychosocialCriteria { get; set; }
        public DbSet<PatientSupportSystemCriteria> PatientSupportSystemCriteria { get; set; }
        //Interop
        public DbSet<InteropPlacerType> InteropPlacerTypes { get; set; }
        public DbSet<InteropPlacerValues> InteropPlacerValues { get; set; }

        //Pharmacy
        public DbSet<PatientPharmacyDispense> PatientPharmacyDispenses { get; set; }
        public DbSet<PharmacyOrder> PharmacyOrders { get; set; }
    }
}

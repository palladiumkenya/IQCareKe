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
using Entities.PatientCore;

namespace DataAccess.CCC.Context
{
    public class GreencardContext : BaseContext
    {
        public GreencardContext() : base((DbConnection) DataMgr.GetConnection(), true)
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

        //Screening
        public DbSet<PatientScreening> PatientScreenings { get; set; }

        //Triage
        public DbSet<PatientAdverseEvent> PatientAdverseEvents { get; set; }
        public DbSet<Entities.CCC.Triage.PatientAllergy> PatientAllergies { get; set; }
        public DbSet<PatientChronicIllness> PatientChronicIllnesses { get; set; }
        public DbSet<PatientVital> PatientVitals { get; set; }

        //Visit
        public DbSet<PatientMasterVisit> PatientMasterVisit { get; set; }
        public DbSet<Entities.CCC.Visit.PatientEncounter> PatientEncounters { get; set; }
        public DbSet<PatientLabTracker> PatientLabTracker { get; set; }


        //Baseline Entities
        public DbSet<PatientArtUseHistory> PatientArtUseHistories { get; set; }
        public DbSet<PatientDisclosure> PatientDisclosures { get; set; }
        public DbSet<PatientHivEnrollmentBaseline> PatientHivEnrollmentBaselines { get; set; }
        public DbSet<PatientHivTesting> PatientHivTestings { get; set; }
        public DbSet<PatientTransferIn> PatientTransferIns { get; set; }
        public DbSet<DiagnosisArvHistory> DiagnosisArvHistory { get; set; }
        public DbSet<PatientArtInitiationBaseline> PatientArtInitiation { get; set; }
        public DbSet<PatientArtUseHistory> PatientArtUseHistory { get; set; }


        //Encounter
        public DbSet<ComplaintsHistory> ComplaintsHistory { get; set; }
        public DbSet<PatientAdherenceOutcome> PatientAdherenceOutcome { get; set; }
        public DbSet<PatientCareEnding> PatientCareEnding { get; set; }
        public DbSet<PatientClinicalDiagnosis> PatientClinicalDiagnosis { get; set; }
        public DbSet<PatientClinicalNotes> PatientClinicalNotes { get; set; }
        public DbSet<PatientFamilyPlanning> PatientFamilyPlanning { get; set; }
        public DbSet<PatientFamilyPlanningMethod> PatientFamilyPlanningMethod { get; set; }
        public DbSet<PatientPhdp> PatientPhdp { get; set; }
        public DbSet<PatientProphylaxis> PatientProphylaxis { get; set; }
        public DbSet<PatientReferral> PatientReferral { get; set; }
        public DbSet<PatientTbTreatmentTracker> PatientTbTreatmentTracker { get; set; }
        public DbSet<PatientTreatmentEventTracker> PatientTreatmentEventTracker { get; set; }
        public DbSet<PatientVaccination> PatientVaccination { get; set; }
        public DbSet<PhysicalExamination> PhysicalExamination { get; set; }
        public DbSet<Pregnancy> Pregnancies { get; set; }
        public DbSet<PregnancyIndicator> PregnancyIndicators { get; set; }
       

       

        //Appointment
        public DbSet<PatientAppointment> PatientAppointments { get; set; }

    }
}

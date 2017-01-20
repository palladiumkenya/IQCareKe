using DataAccess.Base;
using Entities.Common;
using Entities.PatientCore;
using System.Data.Common;
using System.Data.Entity;
using Entities.CCC.Baseline;
using Entities.CCC.Encounter;
using Entities.CCC.Enrollment;
using Entities.CCC.Screening;
using Entities.CCC.Triage;
using Entities.CCC.Visit;
using PatientDiagnosis = Entities.CCC.Encounter.PatientDiagnosis;
using PatientEnrollment = Entities.PatientCore.PatientEnrollment;


namespace DataAccess.CCC.Context
{
    public class GreencardContext : DbContext
    {
        public GreencardContext() : base((DbConnection) DataMgr.GetConnection(), true)
        {
        }

        //public GreencardContext(string connection) : base(connection)
        //{

        //}

        public DbSet<Patient> Patients { get; set; }

        public DbSet<PersonContact> PatientContacts { get; set; }
        // public DbSet<PersonLocation> PatientLocations { get; set; }
        // public DbSet<PatientMaritalStatus> PatientMaritalStatuses { get; set; }
        public DbSet<PatientOVCStatus> PatientOvcStatuses { get; set; }
        public DbSet<PatientPopulation> PatientPopulations { get; set; }

        //Enrollment
        public DbSet<PatientEnrollment> PatientEnrollments { get; set; }
        public DbSet<PatientEntryPoint> PatientEntryPoint { get; set; }
        public DbSet<Entities.CCC.Enrollment.PatientIdentifier> PatientIdentifiers { get; set; }

        //Screening
        public DbSet<PatientScreening> PatientScreenings { get; set; }

        //Triage
        public DbSet<PatientAdverseEvent> PatientAdverseEvents { get; set; }
        public DbSet<Entities.CCC.Triage.PatientAllergy> PatientAllergies { get; set; }
        public DbSet<PatientChronicIllness> PatientChronicIllnesses { get; set; }
        public DbSet<PatientVital> PatientVitals { get; set; }

        //Visit
        public DbSet<PatientMasterVisit> PatientMasterVisit { get; set; }
        public DbSet<PatientEncounter> PatientEncounters { get; set; }

        // public DbSet<PersonRelationship> PersonRelationship { get; set; }
        // public DbSet<PatientTreatmentSupporter> PatientTreatmentSupporters { get; set; }

        //Baseline Entities
        public DbSet<PatientArtUseHistory> PatientArtUseHistories { get; set; }
        public DbSet<PatientArtInitiationBaseline> PatientArtInitiationBaselines { get; set; }
        public DbSet<PatientDiagnosis> PatientDiagnoses { get; set; }
        public DbSet<PatientDisclosure> PatientDisclosures { get; set; }
        public DbSet<PatientHivEnrollmentBaseline> PatientHivEnrollmentBaselines { get; set; }
        public DbSet<PatientHivTesting> PatientHivTestings { get; set; }
        public DbSet<PatientTransferIn> PatientTransferIns { get; set; }

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
    }
}

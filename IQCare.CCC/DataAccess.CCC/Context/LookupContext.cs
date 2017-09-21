using System.Data.Entity;
using DataAccess.Context;
using Entities.CCC.Lookup;
using System.Data.Common;
using DataAccess.Base;
using Entities.CCC.Appointment;
using Entities.CCC.Interoperability;

namespace DataAccess.CCC.Context
{
    public class LookupContext :BaseContext
    {

        public LookupContext() : base((DbConnection)DataMgr.GetConnection(), true) {

            Configuration.ProxyCreationEnabled = false;
            // DataMgr.OpenDecryptedSession(base.Database.Connection);
            // Configuration.LazyLoadingEnabled = true;
            Database.SetInitializer<LookupContext>(null);
        }
        //public LookupContext(string connection) : base(connection)
        //{

        //}

        public DbSet<LookupItemView> Lookups { get; set; }
        public DbSet<LookupItem> LookupItems { get; set; }
        public DbSet<LookupMaster> LookupMasters { get; set; } 
        public DbSet<LookupMasterItem> LookupMasterItems { get; set; }
        public DbSet<LookupCounty> LookupCounties { get; set; }
        public DbSet<LookupFacility> LookupFacility { get; set; }
        public DbSet<LookupLabs> LookupLaboratories { get; set; }
        public DbSet<LookupTestParameter> LookupTestParameter { get; set; }
        public DbSet<LookupPreviousLabs> LookupPreviousLaboratories { get; set; }
        public DbSet<LookupFacilityViralLoad> LookupFacilityViralLoad { get; set; }
        public DbSet<PatientLookup> PatientLookups { get; set; }
        public DbSet<PersonLookUp> PersonLookUps { get; set; }
        public DbSet<PersonContactLookUp> PersonContactLookUps { get; set; }
        public DbSet<PatientBaselineLookup> PatientBaselineLookups { get; set; }
        public DbSet<PatientServiceEnrollmentLookup> PatientServiceEnrollmentLookups { get; set; }
        public DbSet<PatientRegimenLookup> PatientRegimenLookups { get; set; }
        public DbSet<LookupPatientAdherence> LookupPatientAdherence { get; set; }
        public DbSet<PatientTreatmentSupporterLookup> PatientTreatmentSupporterLookups { get; set; }
        public DbSet<LookupFacilityStatistics> LookupFacilityStatistics { get; set; }
        public DbSet<PatientTreamentTrackerLookup> PatientTreamentTrackerLookups { get; set; }
        public DbSet<FacilityList> FacilityLists { get; set; }
        public DbSet<PatientRegistrationLookup> PatientRegistrationLookups { get; set; }
        public DbSet<TestingSummaryStatistics> TestingSummaryStatistics { get; set; }
        public DbSet<PatientStabilitySummary> PatientStabilitySummaries { get; set; }

        //Interoperability
        public DbSet<PatientMessage> PatientMessages { get; set; }
        public DbSet<DrugPrescriptionEntity> DrugPrescriptionMessages { get; set; }
    }
}

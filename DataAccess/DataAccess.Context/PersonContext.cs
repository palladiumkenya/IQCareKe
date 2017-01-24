using System.Data.Common;
using System.Data.Entity;
using DataAccess.Base;
using Entities.Common;
using Entities.PatientCore;

namespace DataAccess.Context
{

    public class PersonContext :BaseContext
    {
        public PersonContext() : base((DbConnection) DataMgr.GetConnection(), true)
        {
            Configuration.ProxyCreationEnabled = false;
            // DataMgr.OpenDecryptedSession(base.Database.Connection);
            Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer<PersonContext>(null);
        }

        public DbSet<Person> Persons  { get; set; }
        public DbSet<PersonContact> PersonContacts { get; set; }
        public DbSet<PersonRelationship> PersonRelationships { get; set; }
        public DbSet<PersonLocation> PersonLocations { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientOVCStatus> PatientOvcStatus { get; set; }
        public DbSet<PatientMaritalStatus> PatientMaritalStatuses { get; set; }
        public DbSet<PatientPopulation> PatientPopulations { get; set; }
    }
}

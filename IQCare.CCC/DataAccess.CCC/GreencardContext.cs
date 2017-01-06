using DataAccess.Base;
using DataAccess.Context;
using Entities.Common;
using Entities.PatientCore;
using System.Data.Common;
using System.Data.Entity;


namespace DataAccess.CCC
{
   public class GreencardContext:DbContext
    {
        public GreencardContext() :  base((DbConnection)DataMgr.GetConnection(), true) {
        }
        //public GreencardContext(string connection) : base(connection)
        //{

        //}
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientContact> PatientContacts { get; set; }
        public DbSet<PatientEnrollment> PatientEnrollments { get; set; }
        public DbSet<PatientLocation> PatientLocations { get; set; }
       // public DbSet<PatientMaritalStatus> PatientMaritalStatuses { get; set; }
        public DbSet<PatientOVCStatus> PatientOvcStatuses { get; set; }
        public DbSet<PatientPopulation> PatientPopulations { get; set; }
      public DbSet<PersonRelationship> PersonRelationship { get; set; }
       // public DbSet<PatientTreatmentSupporter> PatientTreatmentSupporters { get; set; }
    }
}

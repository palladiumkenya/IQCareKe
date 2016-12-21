using System.Data.Entity;
using Common.Data;
using Common.Data.Repository;
using PatientManagement.Core.Model;


namespace PatientManagement.Data
{
   public class PatientContext:BaseContext
    {
        public PatientContext() : base()
        {
        }

        public PatientContext(string connection) : base(connection)
        {

        }

        public DbSet<Core.Model.Patient> Patients { get; set; }
        public DbSet<PatientContact> PatientContacts { get; set; }
        public DbSet<PatientEnrollment> PatientEnrollments { get; set; }
        public DbSet<PatientLocation> PatientLocations { get; set; }
        public DbSet<PatientMaritalStatus> PatientMaritalStatuses { get; set; }
        public DbSet<PatientOVCStatus> PatientOvcStatuses { get; set; }
        public DbSet<PatientPopulation> PatientPopulations { get; set; }
        public DbSet<PatientRelationship> PatientRelationships { get; set; }
        public DbSet<PatientTreatmentSupporter> PatientTreatmentSupporters { get; set; }
    }
}

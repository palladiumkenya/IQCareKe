using System.Data.Entity;
using Common.Data;
using Common.Data.Repository;



namespace PatientManagement.Data
{
   public class PatientContext:BaseContext
    {
        public PatientContext() : base() {
        }
        public PatientContext(string connection) : base(connection)
        {

        }
        public DbSet<Core.Model.Patient> Patients { get; set; }
    }
}

using System.Data.Entity;
using Common.Data;
using TriageManagement.Core.Model;

namespace TriageManagement.Data
{
    public class TriageContext :BaseContext
    {
        public TriageContext() : base()
        {

        }

        public TriageContext(string connection) : base(connection)
        {

        }

        public DbSet<PatientVitals> PatientVitalses { get; set; }
        public DbSet<PatientAllergies> PatientAllergieses { get; set; }
        public DbSet<AdverseEvents> AdverseEventses { get; set; }
        public DbSet<PatientChronicIllness> PatientChronicIllnesses { get; set; }
    }

}

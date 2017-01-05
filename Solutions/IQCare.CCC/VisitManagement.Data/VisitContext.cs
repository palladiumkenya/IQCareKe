using System.Data.Entity;
using Common.Data;
using VisitManagement.Core.Model;

namespace VisitManagement.Data
{
    public class VisitContext : BaseContext
    {
        
        public VisitContext() : base()
        {
        }

        public VisitContext(string connection) : base(connection)
        {

        }

        public DbSet<PatientMasterVisit> PatientMasterVisits { get; set; }  
        public DbSet<PatientEncounter> PatientEncounters { get; set; }
    }
}

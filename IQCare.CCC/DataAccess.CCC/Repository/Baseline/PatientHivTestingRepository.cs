using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Baseline;
using DataAccess.Context;
using Entities.CCC.Baseline;

namespace DataAccess.CCC.Repository.Baseline
{
    public class PatientHivTestingRepository : BaseRepository<PatientHivTesting>, IPatientHivTestingRepository
    {
        public PatientHivTestingRepository(GreencardContext context) : base(context)
        {
        }

        public PatientHivTestingRepository() : this(new GreencardContext())
        {
        }
    }
}
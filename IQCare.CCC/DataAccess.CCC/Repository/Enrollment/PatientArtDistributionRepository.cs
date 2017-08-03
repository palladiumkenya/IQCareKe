using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.enrollment;
using DataAccess.Context;
using Entities.CCC.Enrollment;

namespace DataAccess.CCC.Repository.Enrollment
{
    public class PatientArtDistributionRepository : BaseRepository<PatientArtDistribution>, IPatientArtDistributionRepository
    {
        private readonly GreencardContext _context;

        public PatientArtDistributionRepository() : this(new GreencardContext())
        {
        }

        public PatientArtDistributionRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}
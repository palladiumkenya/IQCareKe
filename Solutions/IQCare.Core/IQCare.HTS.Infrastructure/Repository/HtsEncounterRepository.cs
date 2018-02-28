using IQCare.HTS.Core.Interfaces.Repositories;
using IQCare.HTS.Core.Model;
using IQCare.SharedKernel.Infrastructure.Repository;

namespace IQCare.HTS.Infrastructure.Repository
{
    public class HtsEncounterRepository : BaseRepository<HtsEncounter, int>, IHtsEncounterRepository
    {
        public HtsEncounterRepository(HtsDbContext context) : base(context)
        {
        }
    }
}
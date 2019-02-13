using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.CCC.Adherence;
using DataAccess.CCC.Context;
using DataAccess.Context;
using DataAccess.CCC.Interface.Adherence;

namespace DataAccess.CCC.Repository.Adherence
{
    public class AdherenceRepository:BaseRepository<HIVStatus>, IAdherenceRepository
    {
        private readonly GreencardContext _context;
        public AdherenceRepository():base(new GreencardContext())
        {

        }
        public AdherenceRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
        public List<HIVStatus> getHIVStatus(int personId, int patientMasterVisitId)
        {
            IAdherenceRepository adherenceRepository = new AdherenceRepository();
            var HIVStatusList = adherenceRepository.GetAll().Where(x => x.PatientId == personId && x.PatientMasterVisitId == patientMasterVisitId).OrderBy(x => x.Id);
            return HIVStatusList.ToList();
        }
        public int updateHIVStatus(HIVStatus Hs)
        {
            IAdherenceRepository adherenceRepository = new AdherenceRepository();
            adherenceRepository.updateHIVStatus(Hs);
            return Hs.Id;
        }
    }
}

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
    public class AdherenceScreeningRepository : BaseRepository<AdherenceScreening>, IAdherenceScreeningRepository
    {
        private readonly GreencardContext _context;
        public AdherenceScreeningRepository():base(new GreencardContext())
        {

        }
        public AdherenceScreeningRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
        public List<AdherenceScreening> getAdherenceScreening(int personId, int patientMasterVisitId)
        {
            IAdherenceScreeningRepository screeningRepository = new AdherenceScreeningRepository();
            var screeningList = screeningRepository.GetAll().Where(x => x.PatientId == personId && x.PatientMasterVisitId == patientMasterVisitId).OrderBy(x => x.Id);
            return screeningList.ToList();
        }
        public int updateAdherenceScreening(AdherenceScreening _AS)
        {
            IAdherenceScreeningRepository screeningRepository = new AdherenceScreeningRepository();
            screeningRepository.updateAdherenceScreening(_AS);
            return _AS.Id;
        }
    }
}

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
    public class AdherenceHIVInfectionRepository : BaseRepository<UnderstandingHIV>, IAdherenceHIVInfectionRepository
    {
        private readonly GreencardContext _context;
        public AdherenceHIVInfectionRepository():base(new GreencardContext())
        {

        }
        public AdherenceHIVInfectionRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
        public List<UnderstandingHIV> getHIVUnderstanding(int personId, int patientMasterVisitId)
        {
            IAdherenceHIVInfectionRepository HIVInfectionRepository = new AdherenceHIVInfectionRepository();
            var HIVInfectionList = HIVInfectionRepository.GetAll().Where(x => x.PatientId == personId && x.PatientMasterVisitId == patientMasterVisitId).OrderBy(x => x.Id);
            return HIVInfectionList.ToList();
        }
        public int updateHIVUnderstanding(UnderstandingHIV UH)
        {
            IAdherenceHIVInfectionRepository HIVInfectionRepository = new AdherenceHIVInfectionRepository();
            HIVInfectionRepository.updateHIVUnderstanding(UH);
            return UH.Id;
        }
    }
}

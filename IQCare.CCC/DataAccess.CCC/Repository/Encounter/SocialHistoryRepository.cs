using System.Collections.Generic;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Encounter;
using DataAccess.Context;
using Entities.CCC.Encounter;
using System.Linq;

namespace DataAccess.CCC.Repository.Encounter
{
    public class SocialHistoryRepository: BaseRepository<PatientSocialHistory>, ISocialHistoryRepository
    {
        private readonly GreencardContext _context;
        public SocialHistoryRepository():base(new GreencardContext())
        {

        }
        public SocialHistoryRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }

        public List<PatientSocialHistory> getSocialHistory(int personId, int patientMasterVisitId)
        {
            ISocialHistoryRepository socialHistoryRepository = new SocialHistoryRepository();
            var socialHistoryList = socialHistoryRepository.GetAll().Where(x => x.PatientId == personId && x.PatientMasterVisitId == patientMasterVisitId).OrderBy(x => x.Id);
            return socialHistoryList.ToList();
        }

        public int updateSocialHistory(PatientSocialHistory SH)
        {
            ISocialHistoryRepository socialHistoryRepository = new SocialHistoryRepository();
            socialHistoryRepository.updateSocialHistory(SH);
            return SH.Id;
        }
    }
}

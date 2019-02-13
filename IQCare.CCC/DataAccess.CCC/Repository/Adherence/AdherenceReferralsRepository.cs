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
    public class AdherenceReferralsRepository : BaseRepository<Referrals>, IAdherenceReferralsRepository
    {
        private readonly GreencardContext _context;
        public AdherenceReferralsRepository():base(new GreencardContext())
        {

        }
        public AdherenceReferralsRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
        public List<Referrals> getReferrals(int personId, int patientMasterVisitId)
        {
            IAdherenceReferralsRepository referralsRepository = new AdherenceReferralsRepository();
            var referralsList = referralsRepository.GetAll().Where(x => x.PatientId == personId && x.PatientMasterVisitId == patientMasterVisitId).OrderBy(x => x.Id);
            return referralsList.ToList();
        }
        public int updateReferrals(Referrals refs)
        {
            IAdherenceReferralsRepository referralsRepository = new AdherenceReferralsRepository();
            referralsRepository.updateReferrals(refs);
            return refs.Id;
        }
    }
}

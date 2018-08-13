using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.CCC.Adherence;
using DataAccess.Context;

namespace DataAccess.CCC.Interface.Adherence
{
    public interface IAdherenceReferralsRepository: IRepository<Referrals>
    {
        List<Referrals> getReferrals(int personId, int patientMasterVisitId);
        int updateReferrals(Referrals refs);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Context;
using Entities.CCC.Encounter;

namespace DataAccess.CCC.Interface.Encounter
{
    public interface ISocialHistoryRepository : IRepository<PatientSocialHistory>
    {
        List<PatientSocialHistory> getSocialHistory(int personId, int patientMasterVisitId);
        int updateSocialHistory(PatientSocialHistory SH);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.CCC.Encounter;
using System.Data;

namespace Interface.CCC.Encounter
{
    public interface ISocialHistory
    {
        int AddSocialHistory(PatientSocialHistory SH);
        int updateSocialHistory(PatientSocialHistory SH);
        List<PatientSocialHistory> getSocialHistory(int patientid, int patientMasterVisitId);
    }
}

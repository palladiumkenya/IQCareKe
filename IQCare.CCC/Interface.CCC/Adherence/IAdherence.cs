using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.CCC.Adherence;

namespace Interface.CCC.Adherence
{
    public interface IAdherence
    {
        int AddHIVStatus(HIVStatus HS);
        int AddAdherenceScreening(AdherenceScreening AS);
        int AddPsychosocialCircumstances(PsychosocialCircumstances PC);
        int AddDailyRoutine(DailyRoutine DR);
        int AddUnderstandingHIV(UnderstandingHIV UH);
        int AddReferrals(Referrals REFS);
        List<HIVStatus> getHIVStatus(int patientid, int patientMasterVisitId);
        List<DailyRoutine> getDailyRoutine(int patientId, int patientMasterVisitId);
        List<UnderstandingHIV> getUnderstandingHIV(int patientId, int patientMasterVisitId);
        List<PsychosocialCircumstances> getPsychosocialCircumstances(int patientId, int patientMasterVisitId);
        List<Referrals> getReferrals(int patientId, int patientMasterVisitId);
        List<AdherenceScreening> getAdherenceScreening(int patientId, int patientMasterVisitId);
        int updateHIVStatus(HIVStatus HS);
        int updateDailyRoutine(DailyRoutine DR);
        int updateUnderstandingHIV(UnderstandingHIV UH);
        int updatePsychosocialCircumstances(PsychosocialCircumstances PC);
        int updateReferrals(Referrals refs);
        int updateAdherenceScreening(AdherenceScreening _AS);
    }
}

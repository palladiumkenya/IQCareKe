using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface.CCC.Adherence;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Adherence;

namespace BusinessProcess.CCC.Adherence
{
    public class BAdherence:ProcessBase, IAdherence
    {
        private int result;
        public int AddHIVStatus(HIVStatus HS)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.AdherenceRepository.Add(HS);
                unitOfWork.Complete();
                unitOfWork.Dispose();
                return HS.Id;
            }
        }

        public int AddAdherenceScreening(AdherenceScreening AS)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.AdherenceScreeningRepository.Add(AS);
                unitOfWork.Complete();
                unitOfWork.Dispose();
                return AS.Id;
            }
        }

        public int AddPsychosocialCircumstances(PsychosocialCircumstances PC)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.AdherencePsychosocialRepository.Add(PC);
                unitOfWork.Complete();
                unitOfWork.Dispose();
                return PC.Id;
            }
        }
        public int AddDailyRoutine(DailyRoutine DR)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.AdherenceDailyRoutineRepository.Add(DR);
                unitOfWork.Complete();
                unitOfWork.Dispose();
                return DR.Id;
            }
        }

        public int AddUnderstandingHIV(UnderstandingHIV UH)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.AdherenceHIVInfectionRepository.Add(UH);
                unitOfWork.Complete();
                unitOfWork.Dispose();
                return UH.Id;
            }
        }
        public int AddReferrals(Referrals REFS)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.AdherenceReferralsRepository.Add(REFS);
                unitOfWork.Complete();
                unitOfWork.Dispose();
                return REFS.Id;
            }
        }
        public List<HIVStatus> getHIVStatus(int patientId, int patientMasterVisitId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var HIVStatus = unitOfWork.AdherenceRepository.getHIVStatus(patientId, patientMasterVisitId);
                unitOfWork.Dispose();
                return HIVStatus.ToList();
            }
        }
        public List<DailyRoutine> getDailyRoutine(int patientId, int patientMasterVisitId)
        {
            using (UnitOfWork unitofwork = new UnitOfWork(new GreencardContext()))
            {
                var dailyRoutineList = unitofwork.AdherenceDailyRoutineRepository.getDailyRoutine(patientId, patientMasterVisitId);
                unitofwork.Dispose();
                return dailyRoutineList.ToList();
            }
        }
        public List<UnderstandingHIV> getUnderstandingHIV(int patientId, int patientMasterVisitId)
        {
            using (UnitOfWork unitofwork = new UnitOfWork(new GreencardContext()))
            {
                var understandingHIVList = unitofwork.AdherenceHIVInfectionRepository.getHIVUnderstanding(patientId, patientMasterVisitId);
                unitofwork.Dispose();
                return understandingHIVList.ToList();
            }
        }
        public List<PsychosocialCircumstances> getPsychosocialCircumstances(int patientId, int patientMasterVisitId)
        {
            using (UnitOfWork unitofwork = new UnitOfWork(new GreencardContext()))
            {
                var psychosocialList = unitofwork.AdherencePsychosocialRepository.getPsychosocialCircumstances(patientId, patientMasterVisitId);
                unitofwork.Dispose();
                return psychosocialList.ToList();
            }
        }
        public List<Referrals> getReferrals(int patientId, int patientMasterVisitId)
        {
            using (UnitOfWork unitofwork = new UnitOfWork(new GreencardContext()))
            {
                var referralsList = unitofwork.AdherenceReferralsRepository.getReferrals(patientId, patientMasterVisitId);
                unitofwork.Dispose();
                return referralsList.ToList();
            }
        }
        public List<AdherenceScreening> getAdherenceScreening(int patientId, int patientMasterVisitId)
        {
            using (UnitOfWork unitofwork = new UnitOfWork(new GreencardContext()))
            {
                var screeningList = unitofwork.AdherenceScreeningRepository.getAdherenceScreening(patientId, patientMasterVisitId);
                unitofwork.Dispose();
                return screeningList.ToList();
            }
        }

        public int updateHIVStatus(HIVStatus HS)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.AdherenceRepository.Update(HS);
                result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return result;
            }
        }
        public int updateDailyRoutine(DailyRoutine DR)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.AdherenceDailyRoutineRepository.Update(DR);
                result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return result;
            }
        }
        public int updateUnderstandingHIV(UnderstandingHIV UH)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.AdherenceHIVInfectionRepository.Update(UH);
                result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return result;
            }
        }
        public int updatePsychosocialCircumstances(PsychosocialCircumstances PC)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.AdherencePsychosocialRepository.Update(PC);
                result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return result;
            }
        }
        public int updateReferrals(Referrals PC)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.AdherenceReferralsRepository.Update(PC);
                result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return result;
            }
        }
        public int updateAdherenceScreening(AdherenceScreening AR)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.AdherenceScreeningRepository.Update(AR);
                result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return result;
            }
        }
    }
}

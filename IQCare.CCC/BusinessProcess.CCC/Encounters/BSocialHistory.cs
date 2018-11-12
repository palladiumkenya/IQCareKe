using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Encounter;
using Interface.CCC.Encounter;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.CCC;
using System.Data;
using static Entities.CCC.Encounter.PatientEncounter;
using DataAccess.CCC.Repository.Encounter;

namespace BusinessProcess.CCC.Encounters
{
    public class BSocialHistory:ProcessBase, ISocialHistory
    {
        private int result;
        public int AddSocialHistory(PatientSocialHistory SH)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.SocialHistoryRepository.Add(SH);
                unitOfWork.Complete();
                unitOfWork.Dispose();
                return SH.Id;
            }
        }

        public int updateSocialHistory(PatientSocialHistory SH)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.SocialHistoryRepository.Update(SH);
                result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return result;
            }
        }

        public List<PatientSocialHistory> getSocialHistory(int patientId, int patientMasterVisitId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var socialHistory = unitOfWork.SocialHistoryRepository.getSocialHistory(patientId, patientMasterVisitId);
                unitOfWork.Dispose();
                return socialHistory.ToList();
            }
        }
    }
}

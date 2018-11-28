using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Triage;
using Interface.CCC.Triage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessProcess.CCC.Triage
{
    public class PatientPregnancyIndicatorManager : ProcessBase, IpatientPregnancyIndicatorManager
    {
        //private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        private int _result = 0;

        public int AddPregnancyIndicator(PatientPregnancyIndicator a)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientPregnanacyIndicatorRepository.Add(a);
                _result= unitOfWork.Complete();
                unitOfWork.Dispose();
                return _result;
            }
        }

        public int UpdatePreganacyIndcator(PatientPregnancyIndicator u)
        {

            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var pg = unitOfWork.PatientPregnanacyIndicatorRepository.FindBy(
                         x => x.PatientId == u.PatientId & !x.DeleteFlag)
                         .FirstOrDefault();
                if (pg != null)
                {
                    pg.VisitDate = u.VisitDate;
                    pg.LMP = u.LMP;
                    pg.EDD = u.EDD;
                    pg.AncProfile = u.AncProfile;
                    pg.AncProfileDate = u.AncProfileDate;
                    pg.PregnancyStatusId = u.PregnancyStatusId;
                }
                unitOfWork.PatientPregnanacyIndicatorRepository.Update(pg);
                _result= unitOfWork.Complete();
                unitOfWork.Dispose();
                return _result;
            }
        }

        public int DeletePregnancyIndicator(int id)
        {

            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var pg = unitOfWork.PatientPregnanacyIndicatorRepository.GetById(id);
                unitOfWork.PatientPregnanacyIndicatorRepository.Remove(pg);
                _result= unitOfWork.Complete();
                unitOfWork.Dispose();
                return _result;
            }
        }

        public List<PatientPregnancyIndicator> GetPregnancyIndicator(int patientId)
        {

            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var pgIndicatorList = unitOfWork.PatientPregnanacyIndicatorRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag).ToList();
                unitOfWork.Dispose();
                return pgIndicatorList;
            }
        }
        public int  GetLastPregnancyStatus(int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var pgPregnancyIndicator = unitOfWork.PatientPregnanacyIndicatorRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag).OrderByDescending(x => x.PatientMasterVisitId).FirstOrDefault();
                unitOfWork.Dispose();
                return pgPregnancyIndicator.PregnancyStatusId;
            }
        }


        public int CheckIfPregnancyIndicatorExisists(int patientId)
        {

            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var patientTrnasferId =
                   unitOfWork.PatientPregnanacyIndicatorRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag)
                       .Select(x => x.Id)
                       .FirstOrDefault();
                unitOfWork.Dispose();
                return Convert.ToInt32(patientTrnasferId);
            }
        }
    }
}
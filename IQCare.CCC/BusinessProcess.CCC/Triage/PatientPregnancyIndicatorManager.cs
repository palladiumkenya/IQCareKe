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
        private int Result = 0;

        public int AddPregnancyIndicator(PatientPregnancyIndicator a)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientPregnanacyIndicatorRepository.Add(a);
                Result= _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }
        }

        public int UpdatePreganacyIndcator(PatientPregnancyIndicator u)
        {

            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var PG = _unitOfWork.PatientPregnanacyIndicatorRepository.FindBy(
                         x => x.PatientId == u.PatientId & !x.DeleteFlag)
                         .FirstOrDefault();
                if (PG != null)
                {
                    PG.LMP = u.LMP;
                    PG.EDD = u.EDD;
                    PG.ANCProfile = u.ANCProfile;
                    PG.ANCProfileDate = u.ANCProfileDate;
                    PG.PregnancyStatusId = u.PregnancyStatusId;
                }
                _unitOfWork.PatientPregnanacyIndicatorRepository.Update(PG);
                Result= _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }
        }

        public int DeletePregnancyIndicator(int Id)
        {

            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var PG = _unitOfWork.PatientPregnanacyIndicatorRepository.GetById(Id);
                _unitOfWork.PatientPregnanacyIndicatorRepository.Remove(PG);
                Result= _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }
        }

        public List<PatientPregnancyIndicator> GetPregnancyIndicator(int patientId)
        {

            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var pgIndicatorList = _unitOfWork.PatientPregnanacyIndicatorRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag).ToList();
                _unitOfWork.Dispose();
                return pgIndicatorList;
            }
        }

        public int CheckIfPregnancyIndicatorExisists(int patientId)
        {

            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var patientTrnasferId =
                   _unitOfWork.PatientPregnanacyIndicatorRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag)
                       .Select(x => x.Id)
                       .FirstOrDefault();
                _unitOfWork.Dispose();
                return Convert.ToInt32(patientTrnasferId);
            }
        }
    }
}
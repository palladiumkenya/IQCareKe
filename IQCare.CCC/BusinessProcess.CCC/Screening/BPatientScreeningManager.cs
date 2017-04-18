using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Screening;
using Interface.CCC.Screening;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessProcess.CCC.Screening
{
    public class BPatientScreeningManager : IPatientScreeningManager
    {
        // private UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        internal int Result = 0;

        public int AddPatientScreening(PatientScreening a)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientScreeningRepository.Add(a);
                Result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }
        }

        public int UpdatePatientScreening(PatientScreening u)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var PS = _unitOfWork.PatientScreeningRepository.FindBy(
                    x => x.PatientId == u.PatientId & !x.DeleteFlag)
                    .FirstOrDefault();
                if (PS != null)
                {
                    PS.ScreeningTypeId = u.ScreeningTypeId;
                    PS.ScreeningDone = u.ScreeningDone;
                    PS.ScreeningDate = u.ScreeningDate;
                    PS.ScreeningCategoryId = u.ScreeningCategoryId;
                    PS.ScreeningValueId = u.ScreeningValueId;
                    PS.Comment = u.Comment;
                }
                _unitOfWork.PatientScreeningRepository.Update(PS);
                Result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }
        }

        public int DeletePatientScreening(int Id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var PS = _unitOfWork.PatientScreeningRepository.GetById(Id);
                _unitOfWork.PatientScreeningRepository.Remove(PS);
                Result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }
        }

        public List<PatientScreening> GetPatientScreening(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var screeningList = _unitOfWork.PatientScreeningRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag).ToList();
                _unitOfWork.Dispose();
                return screeningList;
            }
        }

        public int CheckIfPatientScreeningExists(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var PS = _unitOfWork.PatientScreeningRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag)
                      .Select(x => x.Id)
                      .FirstOrDefault();
                _unitOfWork.Dispose();
                return Convert.ToInt32(PS);
            }
        }
    }
}
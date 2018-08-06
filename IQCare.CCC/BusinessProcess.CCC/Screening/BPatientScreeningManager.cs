using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Screening;
using Interface.CCC.Screening;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessProcess.CCC.Screening
{
    public class BPatientScreeningManager : ProcessBase,IPatientScreeningManager
    {
        // private UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        internal int Result = 0;

        public int AddPatientScreening(PatientScreening a)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientScreeningRepository.Add(a);
                Result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return Result;
            }
        }

        public int UpdatePatientScreening(PatientScreening u)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var ps = unitOfWork.PatientScreeningRepository.FindBy(
                    x => x.Id == u.Id)
                    .FirstOrDefault();
                if (ps != null)
                {
                    ps.VisitDate = u.VisitDate;
                    ps.ScreeningTypeId = u.ScreeningTypeId;
                    ps.ScreeningDone = u.ScreeningDone;
                    ps.ScreeningDate = u.ScreeningDate;
                    ps.ScreeningCategoryId = u.ScreeningCategoryId;
                    ps.ScreeningValueId = u.ScreeningValueId;
                    ps.Comment = u.Comment;
                }
                unitOfWork.PatientScreeningRepository.Update(ps);
                Result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return Result;
            }
        }

        public int updatePatientScreeningById(PatientScreening p)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientScreeningRepository.Update(p);
                Result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }
        }

        public int DeletePatientScreening(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var ps = unitOfWork.PatientScreeningRepository.GetById(id);
                unitOfWork.PatientScreeningRepository.Remove(ps);
                Result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return Result;
            }
        }

        public List<PatientScreening> GetPatientScreening(int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var screeningList = unitOfWork.PatientScreeningRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag).ToList();
                unitOfWork.Dispose();
                return screeningList;
            }
        }

        public List<PatientScreening> GetPatientScreening(int patientId, DateTime visitDate, int screeningCategoryId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var screeningList = unitOfWork.PatientScreeningRepository.FindBy(x => x.PatientId == patientId & x.VisitDate == visitDate && x.ScreeningCategoryId == screeningCategoryId & !x.DeleteFlag).ToList();
                unitOfWork.Dispose();
                return screeningList;
            }
        }

        public int CheckIfPatientScreeningExists(int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var PS = unitOfWork.PatientScreeningRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag)
                      .Select(x => x.Id)
                      .FirstOrDefault();
                unitOfWork.Dispose();
                return Convert.ToInt32(PS);
            }
        }
        public int checkScreeningByScreeningCategoryId(int patientId, int screenTypeId, int screeningCategoryId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var PS = unitOfWork.PatientScreeningRepository.FindBy(x => x.PatientId == patientId & x.ScreeningTypeId == screenTypeId & x.ScreeningCategoryId == screeningCategoryId & !x.DeleteFlag)
                      .Select(x => x.Id)
                      .FirstOrDefault();
                unitOfWork.Dispose();
                return Convert.ToInt32(PS);
            }
        }

        public int CheckIfPatientScreeningExists(int patientId, DateTime visitDate, int screeningCategoryId, int screeningTypeId ) {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var PS = unitOfWork.PatientScreeningRepository.FindBy(x => x.PatientId == patientId & x.VisitDate == visitDate & x.ScreeningCategoryId == screeningCategoryId & x.ScreeningTypeId == screeningTypeId & !x.DeleteFlag)
                      .Select(x => x.Id)
                      .FirstOrDefault();
                unitOfWork.Dispose();
                return Convert.ToInt32(PS);
            }
        }
    }
}
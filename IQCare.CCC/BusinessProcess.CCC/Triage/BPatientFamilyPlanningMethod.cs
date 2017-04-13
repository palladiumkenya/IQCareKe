using Interface.CCC.Triage;
using System;
using System.Collections.Generic;
using System.Linq;
using Entities.CCC.Triage;
using DataAccess.CCC.Repository;
using DataAccess.CCC.Context;

namespace BusinessProcess.CCC.Triage
{
    public class BPatientFamilyPlanningMethod : IPatientFamilyPlanningMethodManager
    {
       // private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
       private int Result=0;

        public int AddFamilyPlanningMethod(PatientFamilyPlanningMethod a)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientFamilyPlanningMethodRepository.Add(a);
                Result= _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }
        }

        public int UpdateFamilyPlanningMethod (PatientFamilyPlanningMethod u)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var FP =
                  _unitOfWork.PatientFamilyPlanningMethodRepository.FindBy(
                          x => x.PatientId == u.PatientId & !x.DeleteFlag)
                      .FirstOrDefault();
                if (FP != null)
                {
                    FP.PatientFPId = u.PatientFPId;
                    FP.FPMethodId = u.FPMethodId;
                }
                _unitOfWork.PatientFamilyPlanningMethodRepository.Update(FP);
                Result= _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }
        }

        public int DeleteFamilyPlanningMethod(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var FP = _unitOfWork.PatientFamilyPlanningMethodRepository.GetById(id);
                _unitOfWork.PatientFamilyPlanningMethodRepository.Remove(FP);
                Result= _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }
        }

        public List<PatientFamilyPlanningMethod> GetPatientFamilyPlanningMethod(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var fpMethodList = _unitOfWork.PatientFamilyPlanningMethodRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag).ToList();
                _unitOfWork.Dispose();
                return fpMethodList;
            }
        }

        public int CheckIfPatientHasFamilyPlanningMethod(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var FP = _unitOfWork.PatientFamilyPlanningMethodRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag)
                          .Select(x => x.Id)
                          .FirstOrDefault();
                _unitOfWork.Dispose();
                            return Convert.ToInt32(FP);
            }
        }
    }
}

using Interface.CCC.Triage;
using System;
using System.Collections.Generic;
using System.Linq;
using Entities.CCC.Triage;
using DataAccess.CCC.Repository;
using DataAccess.CCC.Context;
using DataAccess.Base;

namespace BusinessProcess.CCC.Triage
{
    public class BPatientFamilyPlanningMethod : ProcessBase, IPatientFamilyPlanningMethodManager
    {
       // private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
       private int _result=0;

        public int AddFamilyPlanningMethod(PatientFamilyPlanningMethod a)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientFamilyPlanningMethodRepository.Add(a);
                _result= unitOfWork.Complete();
                unitOfWork.Dispose();
                return _result;
            }
        }

        public int UpdateFamilyPlanningMethod (PatientFamilyPlanningMethod u)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var FP =
                  unitOfWork.PatientFamilyPlanningMethodRepository.FindBy(
                          x => x.PatientId == u.PatientId & !x.DeleteFlag)
                      .FirstOrDefault();
                if (FP != null)
                {
                    FP.PatientFPId = u.PatientFPId;
                    FP.FPMethodId = u.FPMethodId;
                }
                unitOfWork.PatientFamilyPlanningMethodRepository.Update(FP);
                _result= unitOfWork.Complete();
                unitOfWork.Dispose();
                return _result;
            }
        }

        public int DeleteFamilyPlanningMethod(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var FP = unitOfWork.PatientFamilyPlanningMethodRepository.GetById(id);
                unitOfWork.PatientFamilyPlanningMethodRepository.Remove(FP);
                _result= unitOfWork.Complete();
                unitOfWork.Dispose();
                return _result;
            }
        }

        public List<PatientFamilyPlanningMethod> GetPatientFamilyPlanningMethod(int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var fpMethodList = unitOfWork.PatientFamilyPlanningMethodRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag).ToList();
                unitOfWork.Dispose();
                return fpMethodList;
            }
        }

        public int CheckIfPatientHasFamilyPlanningMethod(int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var FP = unitOfWork.PatientFamilyPlanningMethodRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag)
                          .Select(x => x.Id)
                          .FirstOrDefault();
                unitOfWork.Dispose();
                            return Convert.ToInt32(FP);
            }
        }
    }
}

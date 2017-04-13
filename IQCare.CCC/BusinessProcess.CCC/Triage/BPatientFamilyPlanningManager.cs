using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Triage;
using Interface.CCC.Triage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessProcess.CCC.Triage
{
    
    public class BPatientFamilyPlanningManager  :IpatientFamilyPlanningManager
    {
        // private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        private int Result=0;

        public int AddFamilyPlanningStatus(PatientFamilyPlanning a)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientFamilyPlanningRepository.Add(a);
                Result= _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }
        }

        public int UpdateFamilyPlanningStatus(PatientFamilyPlanning u)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var FP = _unitOfWork.PatientFamilyPlanningRepository.FindBy(x => x.PatientId == u.PatientId & !x.DeleteFlag).FirstOrDefault();

                if (FP != null)
                {
                    FP.FamilyPlaningStatusId = u.FamilyPlaningStatusId;
                    FP.ReasonNotOnFP = u.ReasonNotOnFP;
                }
                _unitOfWork.PatientFamilyPlanningRepository.Update(FP);
                Result= _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }
        }
        public int DeleteFamilyPlanningStatus(int Id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var FP = _unitOfWork.PatientFamilyPlanningRepository.GetById(Id);
                _unitOfWork.PatientFamilyPlanningRepository.Remove(FP);
                Result= _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }
        }
    
        public int CheckFamilyPlanningExists(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var FP =_unitOfWork.PatientFamilyPlanningRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag)
                    .Select(x => x.Id)
                    .FirstOrDefault();
                _unitOfWork.Dispose();
                    return Convert.ToInt32(FP);
            }
        }

        List<PatientFamilyPlanning> GetPatientFamilyPlanningStatus(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var familyPlanningList = _unitOfWork.PatientFamilyPlanningRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag).ToList();
                _unitOfWork.Dispose();
                return familyPlanningList;
            }
        }

        List<PatientFamilyPlanning> IpatientFamilyPlanningManager.GetPatientFamilyPlanningStatus(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var familyPlanningList = _unitOfWork.PatientFamilyPlanningRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag).ToList();
                _unitOfWork.Dispose();
                return familyPlanningList;
            }
        }
    }
}

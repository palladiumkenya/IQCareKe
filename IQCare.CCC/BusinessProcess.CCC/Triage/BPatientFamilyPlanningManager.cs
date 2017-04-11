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
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());


        public int AddFamilyPlanningStatus(PatientFamilyPlanning a)
        {
            try
            {
                 _unitOfWork.PatientFamilyPlanningRepository.Add(a);
                return _unitOfWork.Complete();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        public int UpdateFamilyPlanningStatus(PatientFamilyPlanning u)
        {
            try
            {
                var FP = _unitOfWork.PatientFamilyPlanningRepository.FindBy(x => x.PatientId == u.PatientId & !x.DeleteFlag).FirstOrDefault();
                
                if (FP != null)
                {
                    FP.FamilyPlaningStatusId= u.FamilyPlaningStatusId;
                    FP.ReasonNotOnFP = u.ReasonNotOnFP;
                }
                _unitOfWork.PatientFamilyPlanningRepository.Update(FP);
                return _unitOfWork.Complete();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }
        public int DeleteFamilyPlanningStatus(int Id);
    
        public int CheckFamilyPlanningExists(int patientId)
        {
            try
            {
                var FP =
           _unitOfWork.PatientFamilyPlanningRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag)
               .Select(x => x.Id)
               .FirstOrDefault();
                return Convert.ToInt32(FP);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        List<PatientFamilyPlanning> GetPatientFamilyPlanningStatus(int patientId)
        {
            try
            {
                return
                     _unitOfWork.PatientFamilyPlanningRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag).ToList();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }
    }
}

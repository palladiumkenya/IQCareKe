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
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());

        public int AddFamilyPlanningMethod(PatientFamilyPlanningMethod a)
        {
            try
            {
                _unitOfWork.PatientFamilyPlanningMethodRepository.Add(a);
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

        public int UpdateFamilyPlanningMethod (PatientFamilyPlanningMethod u)
        {
            try
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

        public int DeleteFamilyPlanningMethod(int id)
        {
            try
            {
                var FP = _unitOfWork.PatientFamilyPlanningMethodRepository.GetById(id);
                _unitOfWork.PatientFamilyPlanningMethodRepository.Remove(FP);
                return  _unitOfWork.Complete();
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

        public List<PatientFamilyPlanningMethod> GetPatientFamilyPlanningMethod(int patientId)
        {
            try
            {
                return _unitOfWork.PatientFamilyPlanningMethodRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag).ToList();
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

        public int CheckIfPatientHasFamilyPlanningMethod(int patientId)
        {
            try
            {
                var FP =
                    _unitOfWork.PatientFamilyPlanningMethodRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag)
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
    }
}

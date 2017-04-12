using Interface.CCC.Screening;
using System;
using System.Collections.Generic;
using System.Linq;
using Entities.CCC.Screening;
using DataAccess.CCC.Repository;
using DataAccess.CCC.Context;

namespace BusinessProcess.CCC.Screening
{
    public class BPatientScreeningManager : IPatientScreeningManager
    {
        private UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());

        public int AddPatientScreening(PatientScreening a)
        {
            try
            {
                _unitOfWork.PatientScreeningRepository.Add(a);
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

        public int UpdatePatientScreening(PatientScreening u)
        {
            try
            {
                var PS =
         _unitOfWork.PatientScreeningRepository.FindBy(
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


        public int DeletePatientScreening(int Id)
        {
            try
            {
                var PS = _unitOfWork.PatientScreeningRepository.GetById(Id);
                _unitOfWork.PatientScreeningRepository.Remove(PS);
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

        public List<PatientScreening> GetPatientScreening(int patientId)
        {
            try
            {
                return _unitOfWork.PatientScreeningRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag).ToList();
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

        public int CheckIfPatientScreeningExists(int patientId)
        {
            try
            {
                var PS =
                 _unitOfWork.PatientScreeningRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag)
                     .Select(x => x.Id)
                     .FirstOrDefault();
                return Convert.ToInt32(PS);
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

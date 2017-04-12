using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Triage;
using Interface.CCC.Triage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessProcess.CCC.Triage
{
    public class PatientPregnancyIndicatorManager : IpatientPregnancyIndicatorManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());

        public int AddPregnancyIndicator(PatientPregnancyIndicator a)
        {
            try
            {
                _unitOfWork.PatientPregnanacyIndicatorRepository.Add(a);
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

        public int UpdatePreganacyIndcator(PatientPregnancyIndicator u)
        {
            try
            {
                var PG =
         _unitOfWork.PatientPregnanacyIndicatorRepository.FindBy(
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

        public int DeletePregnancyIndicator(int Id)
        {
            try
            {
                var PG = _unitOfWork.PatientPregnanacyIndicatorRepository.GetById(Id);
                _unitOfWork.PatientPregnanacyIndicatorRepository.Remove(PG);
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

        public List<PatientPregnancyIndicator> GetPregnancyIndicator(int patientId)
        {
            try
            {
                return _unitOfWork.PatientPregnanacyIndicatorRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag).ToList();
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

        public int CheckIfPregnancyIndicatorExisists(int patientId)
        {
            try
            {
                var patientTrnasferId =
                    _unitOfWork.PatientPregnanacyIndicatorRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag)
                        .Select(x => x.Id)
                        .FirstOrDefault();
                return Convert.ToInt32(patientTrnasferId);
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
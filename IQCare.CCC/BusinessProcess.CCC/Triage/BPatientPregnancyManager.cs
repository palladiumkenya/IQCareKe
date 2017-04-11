using Interface.CCC.Triage;
using System;
using System.Collections.Generic;
using Entities.CCC.Triage;
using DataAccess.CCC.Repository;
using DataAccess.CCC.Context;
using System.Linq;

namespace BusinessProcess.CCC.Triage
{
    public class BPatientPregnancyManager :IPatientPregnancyManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());

        public int AddPatientPregnancy(PatientPreganancy a)
        {
            try
            {
                _unitOfWork.PatientPregnancyRepository.Add(a);
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

        public int UpdatePatientPreganacy(PatientPreganancy u)
        {
            try
            {
                var PG =
                   _unitOfWork.PatientPregnancyRepository.FindBy(
                           x => x.PatientId == u.PatientId & !x.DeleteFlag)
                       .FirstOrDefault();
                if (PG != null)
                {
                    PG.LMP = u.LMP;
                    PG.EDD = u.EDD;
                    PG.Gravidae = u.Gravidae;
                    PG.parity = u.parity;
                    PG.Outcome = u.Outcome;
                    PG.DateOfOutcome = u.DateOfOutcome;
                }
                _unitOfWork.PatientPregnancyRepository.Update(PG);
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


        public int DeletePatientPregnancy(int Id)
        {
            try
            {
                var PG = _unitOfWork.PatientPregnancyRepository.GetById(Id);
                _unitOfWork.PatientPregnancyRepository.Remove(PG);
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

        public int CheckIfPatientPregnancyExisists(int patientId)
        {
            try
            {
                var PG =
                  _unitOfWork.PatientPregnancyRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag)
                      .Select(x => x.Id)
                      .FirstOrDefault();
                return Convert.ToInt32(PG);
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



        public List<PatientPreganancy> GetPatientPregnancy(int patientId)
        {
            try
            {
                return _unitOfWork.PatientPregnancyRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag).ToList();
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

using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Baseline;
using Interface.CCC.Baseline;

namespace BusinessProcess.CCC.Baseline
{
    public class BPatientTreatmentInitiationManager:ProcessBase,IPatientTreatmentInitiationManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        private int Result = 0;

        public int AddPatientTreatmentInitiation(PatientTreatmentInitiation patientTreatmentInitiation)
        {
            try
            {
                _unitOfWork.PatientTreatmentInitiationRepository.Add(patientTreatmentInitiation);
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

        public int UpdatePatientTreatmentInitiation(PatientTreatmentInitiation patientTreatmentInitiation)
        {
            try
            {
                var patientTreatment =
                    _unitOfWork.PatientTreatmentInitiationRepository.FindBy(
                        x => x.PatientId == patientTreatmentInitiation.PatientId & !x.DeleteFlag).FirstOrDefault();
                if (patientTreatment != null)
                {
                    patientTreatment.BaselineViralload = patientTreatmentInitiation.BaselineViralload;
                    patientTreatment.BaselineViralloadDate = patientTreatmentInitiation.BaselineViralloadDate;
                    patientTreatment.Cohort = patientTreatmentInitiation.Cohort;
                    patientTreatment.DateStartedOnFirstline = patientTreatmentInitiation.DateStartedOnFirstline;
                    patientTreatment.Regimen = patientTreatmentInitiation.Regimen;

                    _unitOfWork.PatientTreatmentInitiationRepository.Update(patientTreatment);
                    Result = _unitOfWork.Complete();
                }

                return Result;
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

        public int DeletePatientTreatmentInitiation(int id)
        {
            try
            {
                var item = _unitOfWork.PatientTreatmentInitiationRepository.GetById(id);
                _unitOfWork.PatientTreatmentInitiationRepository.Remove(item);
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

        public List<PatientTreatmentInitiation> GetPatientTreatmentInitiation(int patientId)
        {
            try
            {
                return _unitOfWork.PatientTreatmentInitiationRepository.FindBy(x => x.PatientId == patientId).ToList();

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

        public int CheckIfPatientTreatmentExists(int patientId)
        {
            try
            {
                var recordExists =
    _unitOfWork.PatientTreatmentInitiationRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag)
        .Select(x => x.Id)
        .FirstOrDefault();
                return Convert.ToInt32(recordExists);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally { _unitOfWork.Dispose();}
        }
    }
}

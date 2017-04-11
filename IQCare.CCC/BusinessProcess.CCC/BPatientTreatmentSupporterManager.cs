using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Base;
using Entities.PatientCore;
using Interface.CCC;
using DataAccess.CCC.Repository;
using DataAccess.Context;

namespace BusinessProcess.CCC
{

    public class BPatientTreatmentSupporterManager:ProcessBase,IPatientTreatmeantSupporterManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext());
        private int _result;

        private PatientTreatmentSupporter _patienPersonTreatmentSupporter;

        public int AddPatientTreatmentSupporter(PatientTreatmentSupporter patientTreatmentSupporter)
        {
            try
            {
                _unitOfWork.PatientTreatmentSupporterRepository.Add(patientTreatmentSupporter);
                return _result = _unitOfWork.Complete();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                //_unitOfWork.Dispose();
            }
    
        }

        public int UpdatePatientTreatmentSupporter(PatientTreatmentSupporter patientTreatmentSupporter)
        {
            try
            {
                _unitOfWork.PatientTreatmentSupporterRepository.Update(patientTreatmentSupporter);
                return _result = _unitOfWork.Complete();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                //_unitOfWork.Dispose();
            }
          
        }

        public int DeletePatientTreatmentSupporter(int id)
        {
            try
            {
                _patienPersonTreatmentSupporter = _unitOfWork.PatientTreatmentSupporterRepository.GetById(id);
                _unitOfWork.PatientTreatmentSupporterRepository.Remove(_patienPersonTreatmentSupporter);
                return _result = _unitOfWork.Complete();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                //_unitOfWork.Dispose();
            }
   

        }

        public List<PatientTreatmentSupporter> GetCurrentTreatmentSupporter(int personId)
        {
            try
            {
                List<PatientTreatmentSupporter> patientTreatmentSupporters =
                    _unitOfWork.PatientTreatmentSupporterRepository.FindBy(
                            x => x.PersonId == personId & x.DeleteFlag == false)
                        .OrderByDescending(x => x.Id)
                        .Take(1)
                        .ToList();
                return patientTreatmentSupporters;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                //_unitOfWork.Dispose();
            }

        }

        public List<PatientTreatmentSupporter> GetAllTreatmentSupporter(int personId)
        {
            try
            {
                List<PatientTreatmentSupporter> patientTreatmentSupporters =
                    _unitOfWork.PatientTreatmentSupporterRepository.FindBy(
                            x => x.PersonId == personId & x.DeleteFlag == false)
                        .OrderByDescending(x => x.Id)
                        .ToList();

                return patientTreatmentSupporters;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                //_unitOfWork.Dispose();
            }

        }
    }
}

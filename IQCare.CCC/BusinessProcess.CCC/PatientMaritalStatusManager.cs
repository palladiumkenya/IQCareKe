using DataAccess.Base;
using DataAccess.CCC.Repository;
using DataAccess.Context;
using Entities.PatientCore;
using Interface.CCC;
using System.Collections.Generic;
using System.Linq;
using System;

namespace BusinessProcess.CCC
{
    public class PatientMaritalStatusManager : ProcessBase, IPatientMaritalStatusManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext());
        private int result;

        public int AddPatientMaritalStatus(PatientMaritalStatus patientMarital)
        {
            try
            {
                _unitOfWork.PatientMaritalStatusRepository.Add(patientMarital);
                return result = _unitOfWork.Complete();
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

        public int DeletePatientMaritalStatus(int id)
        {
            try
            {
                var patientmaritalstatus = _unitOfWork.PatientMaritalStatusRepository.GetById(id);
                _unitOfWork.PatientMaritalStatusRepository.Remove(patientmaritalstatus);
                return result = _unitOfWork.Complete();
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

        public List<PatientMaritalStatus> GetAllMaritalStatuses(int personId)
        {
            try
            {
                List<PatientMaritalStatus> myList;
                myList = _unitOfWork.PatientMaritalStatusRepository.FindBy(x => x.PersonId == personId & !x.DeleteFlag)
                    .OrderBy(x => x.Id)
                    .ToList();
                return myList;
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

        public int UpdatePatientMaritalStatus(PatientMaritalStatus patientMarital)
        {
            try
            {
                _unitOfWork.PatientMaritalStatusRepository.Update(patientMarital);
                return result = _unitOfWork.Complete();
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

        public PatientMaritalStatus GetFirstPatientMaritalStatus(int personId)
        {
            try
            {
                return _unitOfWork.PatientMaritalStatusRepository.FindBy(x => x.PersonId == personId)
                    .OrderByDescending(o => o.CreateDate)
                    .FirstOrDefault();
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

        public PatientMaritalStatus GetCurrentPatientMaritalStatus(int personId)
        {
            try
            {
                return _unitOfWork.PatientMaritalStatusRepository.FindBy(x => x.PersonId == personId && !x.DeleteFlag)
                    .OrderBy(x => x.Id).FirstOrDefault();
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

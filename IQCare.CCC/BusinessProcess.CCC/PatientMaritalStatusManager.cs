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
        //private readonly UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext());
        private int result;

        public int AddPatientMaritalStatus(PatientMaritalStatus patientMarital)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext()))
            {
                _unitOfWork.PatientMaritalStatusRepository.Add(patientMarital);
                result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return result;
            }      
        }

        public int DeletePatientMaritalStatus(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext()))
            {
                var patientmaritalstatus = _unitOfWork.PatientMaritalStatusRepository.GetById(id);
                _unitOfWork.PatientMaritalStatusRepository.Remove(patientmaritalstatus);
                result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return result;
            }      
        }

        public List<PatientMaritalStatus> GetAllMaritalStatuses(int personId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext()))
            {
                List<PatientMaritalStatus> myList;
                myList = _unitOfWork.PatientMaritalStatusRepository.FindBy(x => x.PersonId == personId & !x.DeleteFlag)
                    .OrderBy(x => x.Id)
                    .ToList();
                return myList;
            }
   
        }

        public int UpdatePatientMaritalStatus(PatientMaritalStatus patientMarital)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext()))
            {
                _unitOfWork.PatientMaritalStatusRepository.Update(patientMarital);
                result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return result;

            }
        
        }

        public PatientMaritalStatus GetFirstPatientMaritalStatus(int personId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext()))
            {
                var patientms= _unitOfWork.PatientMaritalStatusRepository.FindBy(x => x.PersonId == personId)
             .OrderByDescending(o => o.CreateDate)
             .FirstOrDefault();
                _unitOfWork.Dispose();
                return patientms;
            }
       
        }

        public PatientMaritalStatus GetCurrentPatientMaritalStatus(int personId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext()))
            {
                var patientms= _unitOfWork.PatientMaritalStatusRepository.FindBy(x => x.PersonId == personId && !x.DeleteFlag)
                    .OrderBy(x => x.Id).FirstOrDefault();
                _unitOfWork.Dispose();
                return patientms;

            }
       
        }
    }
}

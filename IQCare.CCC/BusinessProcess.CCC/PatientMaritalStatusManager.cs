using Interface.CCC;
using System.Collections.Generic;
using System.Linq;
using DataAccess.CCC.Repository;
using DataAccess.Context;
using Entities.PatientCore;

namespace BusinessProcess.CCC 
{
    public class PatientMaritalStatusManager : IPatientMaritalStatusManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext());

        public void AddPatientMaritalStatus(PatientMaritalStatus patientMarital)
        {
            _unitOfWork.PatientMaritalStatusRepository.Add(patientMarital);
            _unitOfWork.Complete();
        }

        public void DeletePatientMaritalStatus(int id)
        {
            var patientmaritalstatus = _unitOfWork.PatientMaritalStatusRepository.GetById(id);
            _unitOfWork.PatientMaritalStatusRepository.Remove(patientmaritalstatus);
            _unitOfWork.Complete();
        }

        public List<PatientMaritalStatus> GetAllMaritalStatuses(int personId)
        {
            List<PatientMaritalStatus> myList;
           myList= _unitOfWork.PatientMaritalStatusRepository.FindBy(x => x.PersonId == personId & x.DeleteFlag)
                .OrderBy(x => x.PatientMasterVisitId)
                .ToList();
            return myList;
        }

        public void UpdatePatientMaritalStatus(PatientMaritalStatus patientMarital)
        {
           _unitOfWork.PatientMaritalStatusRepository.Update(patientMarital);
            _unitOfWork.Complete();
        }
    }
}

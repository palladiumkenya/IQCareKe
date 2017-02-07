using DataAccess.Base;
using DataAccess.CCC.Repository;
using DataAccess.Context;
using Entities.PatientCore;
using Interface.CCC;
using System.Collections.Generic;
using System.Linq;

namespace BusinessProcess.CCC
{
    public class PatientMaritalStatusManager : ProcessBase, IPatientMaritalStatusManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext());
        private int result;

        public int AddPatientMaritalStatus(PatientMaritalStatus patientMarital)
        {
            _unitOfWork.PatientMaritalStatusRepository.Add(patientMarital);
          return result= _unitOfWork.Complete();
        }

        public int DeletePatientMaritalStatus(int id)
        {
            var patientmaritalstatus = _unitOfWork.PatientMaritalStatusRepository.GetById(id);
            _unitOfWork.PatientMaritalStatusRepository.Remove(patientmaritalstatus);
          return result= _unitOfWork.Complete();
        }

        public List<PatientMaritalStatus> GetAllMaritalStatuses(int personId)
        {
            List<PatientMaritalStatus> myList;
           myList= _unitOfWork.PatientMaritalStatusRepository.FindBy(x => x.PersonId == personId & x.DeleteFlag)
                //.OrderBy(x => x.i)
                .ToList();
            return myList;
        }

        public int UpdatePatientMaritalStatus(PatientMaritalStatus patientMarital)
        {
           _unitOfWork.PatientMaritalStatusRepository.Update(patientMarital);
          return result=  _unitOfWork.Complete();
        }
    }
}

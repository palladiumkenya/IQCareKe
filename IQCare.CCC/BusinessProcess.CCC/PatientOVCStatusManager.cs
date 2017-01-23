using System.Collections.Generic;
using System.Linq;
using DataAccess.CCC.Repository;
using DataAccess.Context;
using Interface.CCC;
using Entities.PatientCore;

namespace BusinessProcess.CCC
{
    public class PatientOvcStatusManager : IPatientOvcStatusmanager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext());

        public void AddPatientOvcStatus(PatientOVCStatus ovc)
        {

           _unitOfWork.PatientOvcStatusRepository.Add(ovc);
            _unitOfWork.Complete();
        }

        public void DeletePatientOvcStatus(int id)
        {
          var personovcstatus= _unitOfWork.PatientOvcStatusRepository.GetById(id);
            _unitOfWork.PatientOvcStatusRepository.Remove(personovcstatus);
            _unitOfWork.Complete();
        }

        public List<PatientOVCStatus> GetPatientOvcStatus(int id)
        {
            List<PatientOVCStatus> myList;
           myList= _unitOfWork.PatientOvcStatusRepository.FindBy(x => x.Id==id & x.DeleteFlag==false & x.Active).ToList();
            return myList;
        }

        public void UpdatePatientOvcStatus(PatientOVCStatus ovc)
        {
            _unitOfWork.PatientOvcStatusRepository.Update(ovc);
            _unitOfWork.Complete();
        }
    }
}

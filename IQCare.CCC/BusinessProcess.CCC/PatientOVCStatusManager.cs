using System.Collections.Generic;
using System.Linq;
using DataAccess.Base;
using DataAccess.CCC.Repository;
using DataAccess.Context;
using Interface.CCC;
using Entities.PatientCore;
using System;

namespace BusinessProcess.CCC
{
    public class PatientOvcStatusManager :ProcessBase, IPatientOvcStatusmanager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext());
        private int _result;

        public int AddPatientOvcStatus(PatientOVCStatus ovc)
        {
            
           _unitOfWork.PatientOvcStatusRepository.Add(ovc);
          return _result= _unitOfWork.Complete();
        }

        public int DeletePatientOvcStatus(int id)
        {
          var personovcstatus= _unitOfWork.PatientOvcStatusRepository.GetById(id);
            _unitOfWork.PatientOvcStatusRepository.Remove(personovcstatus);
           return _result=  _unitOfWork.Complete();
        }

        public List<PatientOVCStatus> GetPatientOvcStatus(int id)
        {
            List<PatientOVCStatus> myList;
           myList= _unitOfWork.PatientOvcStatusRepository.FindBy(x => x.Id==id & x.DeleteFlag==false & x.Active).ToList();
            return myList;
        }

        public int UpdatePatientOvcStatus(PatientOVCStatus ovc)
        {
            _unitOfWork.PatientOvcStatusRepository.Update(ovc);
          return _result= _unitOfWork.Complete();
        }

        public PatientOVCStatus GetSpecificPatientOvcStatus(int personId)
        {
            return _unitOfWork.PatientOvcStatusRepository.FindBy(x => x.PersonId == personId).FirstOrDefault();
        }
    }
}

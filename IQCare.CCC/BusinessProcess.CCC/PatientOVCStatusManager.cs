using System.Collections.Generic;
using System.Linq;
using DataAccess.Base;
using DataAccess.CCC.Repository;
using DataAccess.Context;
using Interface.CCC;
using Entities.PatientCore;

namespace BusinessProcess.CCC
{
    public class PatientOvcStatusManager :ProcessBase, IPatientOvcStatusmanager
    {
       // private readonly UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext());
        private int _result;

        public int AddPatientOvcStatus(PatientOVCStatus ovc)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext()))
            {
                _unitOfWork.PatientOvcStatusRepository.Add(ovc);
                 _result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return _result;
            }

        }

        public int DeletePatientOvcStatus(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext()))
            {
                var personovcstatus = _unitOfWork.PatientOvcStatusRepository.GetById(id);
                _unitOfWork.PatientOvcStatusRepository.Remove(personovcstatus);
                _result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return _result;
            }
        }

        public List<PatientOVCStatus> GetPatientOvcStatus(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext()))
            {
                List<PatientOVCStatus> myList;
                myList = _unitOfWork.PatientOvcStatusRepository.FindBy(x => x.Id == id & x.DeleteFlag == false & x.Active).ToList();
                _unitOfWork.Dispose();
                return myList;
            }

        }

        public int UpdatePatientOvcStatus(PatientOVCStatus ovc)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext()))
            {
                _unitOfWork.PatientOvcStatusRepository.Update(ovc);
                 _result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return _result;
            }
          
        }

        public PatientOVCStatus GetSpecificPatientOvcStatus(int personId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext()))
            {
              var patientovc=  _unitOfWork.PatientOvcStatusRepository.FindBy(x => x.PersonId == personId)
                                .OrderByDescending(o => o.CreateDate)
                                .FirstOrDefault();
                _unitOfWork.Dispose();
                return patientovc;
            }
        }

        public PatientOVCStatus GetOvcByPersonAndGuardian(int personId, int guardianId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext()))
            {
                var patientovc = _unitOfWork.PatientOvcStatusRepository.FindBy(x => x.PersonId == personId && x.GuardianId == guardianId).FirstOrDefault();
                _unitOfWork.Dispose();
                return patientovc;

            }   
        }
    }
}

using DataAccess.Base;
using Interface.CCC.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.PatientCore;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Enrollment;

namespace BusinessProcess.CCC.Patient
{
    public class BPatient : ProcessBase, IPatientManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        internal int Result;

        public int AddPatient(PatientEntity patient)
        {
            _unitOfWork.PatientRepository.Add(patient);
            Result = _unitOfWork.Complete();
            return patient.Id;
        }

        public int DeletePatient(int id)
        {
            throw new NotImplementedException();
        }

        public PatientEntity GetPatient(int id)
        {
            var patientInfo = _unitOfWork.PatientRepository.GetById(id);
            return patientInfo;
        }

        public int UpdatePatient(PatientEntity patient)
        {
            throw new NotImplementedException();
        }

        public List<PatientEntity> CheckPersonEnrolled(int persionId)
        {
            List<PatientEntity> person = _unitOfWork.PatientRepository.FindBy(x => x.PersonId == persionId).ToList();
            return person;
        }
    }
}

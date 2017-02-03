using DataAccess.Base;
using Interface.CCC.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.PatientCore;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;

namespace BusinessProcess.CCC.Patient
{
    public class BPatient : ProcessBase, IPatientManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        internal int Result;

        public int AddPatient(Entities.CCC.Enrollment.PatientEntity patient)
        {
            _unitOfWork.PatientRepository.Add(patient);
            Result = _unitOfWork.Complete();
            return patient.Id;
        }

        public int DeletePatient(int id)
        {
            throw new NotImplementedException();
        }

        public Entities.CCC.Enrollment.PatientEntity GetPatient(int id)
        {
            throw new NotImplementedException();
        }

        public int UpdatePatient(Entities.CCC.Enrollment.PatientEntity patient)
        {
            throw new NotImplementedException();
        }
    }
}

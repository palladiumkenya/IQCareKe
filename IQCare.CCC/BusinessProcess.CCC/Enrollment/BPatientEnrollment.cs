using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Enrollment;
using Interface.CCC.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessProcess.CCC.Enrollment
{
    public class BPatientEnrollment : ProcessBase, IPatientEnrollmentManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        internal int Result;

        public int AddPatientEnrollment(PatientEntityEnrollment patientEnrollment)
        {
            _unitOfWork.PatientEnrollmentRepository.Add(patientEnrollment);
            Result = _unitOfWork.Complete();
            return patientEnrollment.Id;
        }

        public int DeletePatientEnrollment(int id)
        {
            throw new NotImplementedException();
        }

        public int UpdatePatientEnrollment(PatientEntityEnrollment patientEnrollment)
        {
            throw new NotImplementedException();
        }

        public List<PatientEntityEnrollment> GetPatientEnrollmentByPatientId(int patientId)
        {
            return _unitOfWork.PatientEnrollmentRepository.FindBy(x => x.PatientId == patientId).ToList();
        }
    }
}

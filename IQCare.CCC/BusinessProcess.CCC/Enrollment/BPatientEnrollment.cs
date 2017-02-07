using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Enrollment;
using Interface.CCC.Enrollment;
using System;

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
    }
}

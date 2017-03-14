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
            var enrollment = _unitOfWork.PatientEnrollmentRepository.GetById(id);
            _unitOfWork.PatientEnrollmentRepository.Remove(enrollment);
            return _unitOfWork.Complete();
        }

        public int UpdatePatientEnrollment(PatientEntityEnrollment patientEnrollment)
        {
            var enrollment=new PatientEntityEnrollment()
            {
                ServiceAreaId = patientEnrollment.ServiceAreaId,
                EnrollmentDate = patientEnrollment.EnrollmentDate,
                EnrollmentStatusId = patientEnrollment.EnrollmentStatusId,
                TransferIn = patientEnrollment.TransferIn
            };

            _unitOfWork.PatientEnrollmentRepository.Update(enrollment);
            return _unitOfWork.Complete();
        }

        public List<PatientEntityEnrollment> GetPatientEnrollmentByPatientId(int patientId)
        {
            return _unitOfWork.PatientEnrollmentRepository.FindBy(x => x.PatientId == patientId).ToList();
        }

        public DateTime GetPatientEnrollmentDate(int patientId)
        {
            DateTime enrollmentDate =
                _unitOfWork.PatientEnrollmentRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag)
                    .Select(x => x.EnrollmentDate)
                    .FirstOrDefault();
           return enrollmentDate; 
        }
    }
}

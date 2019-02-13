using DataAccess.Base;
using DataAccess.Records;
using DataAccess.Records.Context;
using Entities.Records.Enrollment;
using Interface.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessProcess.Records.Enrollment
{
    public class BPatientEnrollment : ProcessBase, IPatientEnrollmentManager
    {
        // private readonly UnitOfWork _unitOfWork = new UnitOfWork(new RecordContext());
        internal int Result;

        public int AddPatientEnrollment(PatientEntityEnrollment patientEnrollment)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new RecordContext()))
            {
                _unitOfWork.PatientEnrollmentRepository.Add(patientEnrollment);
                _unitOfWork.Complete();
                var Id = patientEnrollment.Id;
                _unitOfWork.Dispose();
                return Id;
            }

        }

        public int DeletePatientEnrollment(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new RecordContext()))
            {
                var enrollment = _unitOfWork.PatientEnrollmentRepository.GetById(id);
                _unitOfWork.PatientEnrollmentRepository.Remove(enrollment);
                Result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }
        }

        public int UpdatePatientEnrollment(PatientEntityEnrollment patientEnrollment)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new RecordContext()))
            {
                _unitOfWork.PatientEnrollmentRepository.Update(patientEnrollment);
                _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return patientEnrollment.Id;
            }
        }

        public List<PatientEntityEnrollment> GetPatientEnrollmentByPatientId(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new RecordContext()))
            {
                var patientEnrollmentList = _unitOfWork.PatientEnrollmentRepository.FindBy(x => x.PatientId == patientId && !x.CareEnded).ToList();
                _unitOfWork.Dispose();
                return patientEnrollmentList;
            }

        }

        public PatientEntityEnrollment GetPatientEntityEnrollment(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new RecordContext()))
            {
                var patientList = _unitOfWork.PatientEnrollmentRepository.FindBy(x => x.Id == id).First();
                _unitOfWork.Dispose();
                return patientList;
            }
        }

        public DateTime GetPatientEnrollmentDate(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new RecordContext()))
            {
                DateTime enrollmentDate =
          _unitOfWork.PatientEnrollmentRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag)
              .Select(x => x.EnrollmentDate)
              .FirstOrDefault();
                _unitOfWork.Dispose();
                return enrollmentDate;
            }
        }

        public List<PatientEntityEnrollment> GetPatientByPatientIdCareEnded(int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new RecordContext()))
            {
                var enrollmentCareEnded = unitOfWork.PatientEnrollmentRepository.FindBy(x => x.PatientId == patientId && x.CareEnded).ToList();
                unitOfWork.Dispose();
                return enrollmentCareEnded;
            }
        }
    }
}


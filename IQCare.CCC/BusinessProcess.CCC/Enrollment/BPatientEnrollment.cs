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
       // private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        internal int Result;

        public int AddPatientEnrollment(PatientEntityEnrollment patientEnrollment)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientEnrollmentRepository.Add(patientEnrollment);
               _unitOfWork.Complete();
                var Id= patientEnrollment.Id;
                _unitOfWork.Dispose();
                return Id;
            }
   
        }

        public int DeletePatientEnrollment(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var enrollment = _unitOfWork.PatientEnrollmentRepository.GetById(id);
                _unitOfWork.PatientEnrollmentRepository.Remove(enrollment);
                Result= _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }
        }

        public int UpdatePatientEnrollment(PatientEntityEnrollment patientEnrollment)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientEnrollmentRepository.Update(patientEnrollment);
                _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return patientEnrollment.Id;   
            }
        }

        public List<PatientEntityEnrollment> GetPatientEnrollmentByPatientId(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var patientEnrollmentList = _unitOfWork.PatientEnrollmentRepository.FindBy(x => x.PatientId == patientId && !x.CareEnded).ToList();
                _unitOfWork.Dispose();
                return patientEnrollmentList;
            }

        }

        public PatientEntityEnrollment GetPatientEntityEnrollment(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
               var patientList= _unitOfWork.PatientEnrollmentRepository.FindBy(x => x.Id == id).First();
                _unitOfWork.Dispose();
                return patientList;
            }
        }

        public DateTime GetPatientEnrollmentDate(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                DateTime enrollmentDate =
          _unitOfWork.PatientEnrollmentRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag)
              .Select(x => x.EnrollmentDate)
              .FirstOrDefault();
                _unitOfWork.Dispose();
                return enrollmentDate;
            }
        }
    }
}

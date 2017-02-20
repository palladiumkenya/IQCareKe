using System.Collections.Generic;
using System.Linq;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Baseline;
using Interface.CCC.Baseline;

namespace BusinessProcess.CCC.Baseline
{

    public class BPatientHivEnrollmentBaselineManager:ProcessBase,IPatientHivEnrolmetBaselineManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        internal int Result;

        public int AddPatientHivEnrollment(PatientHivEnrollmentBaseline patientHivEnrollmentBaseline)
        {
            _unitOfWork.PatientHivEnrollmentBaselineRepository.Add(patientHivEnrollmentBaseline);
            return Result = _unitOfWork.Complete();
        }

        public int UpdatePatientHivEnrollment(PatientHivEnrollmentBaseline patientHivEnrollmentBaseline)
        {
            _unitOfWork.PatientHivEnrollmentBaselineRepository.Update(patientHivEnrollmentBaseline);
            return Result = _unitOfWork.Complete();
        }

        public int DeletePatientHivEnrollment(int id)
        {
            var patientHivEnrollmentBaseline = _unitOfWork.PatientHivEnrollmentBaselineRepository.GetById(id);
            _unitOfWork.PatientHivEnrollmentBaselineRepository.Remove(patientHivEnrollmentBaseline);
            return Result = _unitOfWork.Complete();
        }

        public List<PatientHivEnrollmentBaseline> GetPatientHivEnrollmentBaselines(int patientId)
        {
            var patientHivEnrollment = _unitOfWork.PatientHivEnrollmentBaselineRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag)
                    .OrderByDescending(x => x.Id)
                    .Distinct()
                    .Take(1)
                    .ToList();
            return patientHivEnrollment;
        }
    }
}

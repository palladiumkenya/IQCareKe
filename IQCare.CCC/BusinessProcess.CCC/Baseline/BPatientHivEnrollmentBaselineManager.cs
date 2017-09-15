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
        //private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        internal int Result;

        public int AddPatientHivEnrollment(PatientHivEnrollmentBaseline patientHivEnrollmentBaseline)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientHivEnrollmentBaselineRepository.Add(patientHivEnrollmentBaseline);
                Result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }
      
        }

        public int UpdatePatientHivEnrollment(PatientHivEnrollmentBaseline patientHivEnrollmentBaseline)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientHivEnrollmentBaselineRepository.Update(patientHivEnrollmentBaseline);
                Result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }
           
        }

        public int DeletePatientHivEnrollment(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var patientHivEnrollmentBaseline = _unitOfWork.PatientHivEnrollmentBaselineRepository.GetById(id);
                _unitOfWork.PatientHivEnrollmentBaselineRepository.Remove(patientHivEnrollmentBaseline);
                Result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }
         
        }

        public List<PatientHivEnrollmentBaseline> GetPatientHivEnrollmentBaselines(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var patientHivEnrollment = _unitOfWork.PatientHivEnrollmentBaselineRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag)
                    .OrderByDescending(x => x.Id)
                    .Distinct()
                    .Take(1)
                    .ToList();
                _unitOfWork.Dispose();
                return patientHivEnrollment;
            }
            
        }
    }
}

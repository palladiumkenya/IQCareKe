using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Enrollment;
using Interface.CCC.Enrollment;

namespace BusinessProcess.CCC.Enrollment
{
    public class BPatientReEnrollment : ProcessBase, IPatientReEnrollmentManager
    {
        public int AddPatientReEnrollment(PatientReEnrollment patientReEnrollment)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientReEnrollmentRepository.Add(patientReEnrollment);
                unitOfWork.Complete();
                unitOfWork.Dispose();
                int reEnrollmentId = patientReEnrollment.Id;
                return reEnrollmentId;
            }
        }
    }
}

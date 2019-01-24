using DataAccess.Base;
using DataAccess.Records;
using DataAccess.Records.Context;
using Entities.Records.Enrollment;
using Interface.Records.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessProcess.Records.Enrollment
{
    public class BPatientReEnrollment : ProcessBase, IPatientReEnrollmentManager
    {
        public int AddPatientReEnrollment(PatientReEnrollment patientReEnrollment)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new RecordContext()))
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

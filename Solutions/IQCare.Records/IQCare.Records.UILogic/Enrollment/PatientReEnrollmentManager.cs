using Application.Presentation;
using Entities.Records.Enrollment;
using Interface.Records.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.Records.UILogic.Enrollment
{
    public class PatientReEnrollmentManager
    {
        IPatientReEnrollmentManager _mgr = (IPatientReEnrollmentManager)ObjectFactory.CreateInstance("BusinessProcess.Records.Enrollment.BPatientReEnrollment, BusinessProcess.Records");

        public int AddPatientReEnrollment(int patientId, DateTime reEnrollmentDate)
        {
            PatientReEnrollment patientReEnrollment = new PatientReEnrollment()
            {
                PatientId = patientId,
                ReenrollmentDate = reEnrollmentDate
            };

            return _mgr.AddPatientReEnrollment(patientReEnrollment);
        }
    }
}

using System;
using Application.Presentation;
using Entities.CCC.Enrollment;
using Interface.CCC.Enrollment;

namespace IQCare.CCC.UILogic.Enrollment
{
    public class PatientReEnrollmentManager
    {
        IPatientReEnrollmentManager _mgr = (IPatientReEnrollmentManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Enrollment.BPatientReEnrollment, BusinessProcess.CCC");

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

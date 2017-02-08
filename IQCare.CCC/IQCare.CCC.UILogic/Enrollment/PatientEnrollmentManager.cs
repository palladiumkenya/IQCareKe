using Application.Presentation;
using Entities.CCC.Enrollment;
using Interface.CCC.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.CCC.UILogic.Enrollment
{
    public class PatientEnrollmentManager
    {
        IPatientEnrollmentManager _mgr = (IPatientEnrollmentManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Enrollment.BPatientEnrollment, BusinessProcess.CCC");

        public int addPatientEnrollment(PatientEntityEnrollment patientEnrollment)
        {
            int returnValue;
            try
            {
                returnValue = _mgr.AddPatientEnrollment(patientEnrollment);
                return returnValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

using Application.Presentation;
using Entities.CCC.Enrollment;
using Interface.CCC.Enrollment;
using System;
using System.Collections.Generic;

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

        public int updatePatientEnrollment(PatientEntityEnrollment patientEntityEnrollment)
        {
            try
            {
                return _mgr.UpdatePatientEnrollment(patientEntityEnrollment);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public DateTime GetPatientEnrollmentDate(int patientId)
        {
            return _mgr.GetPatientEnrollmentDate(patientId);
        }

        public List<PatientEntityEnrollment> GetPatientEnrollmentByPatientId(int patientId)
        {
            return _mgr.GetPatientEnrollmentByPatientId(patientId);
        }

        public PatientEntityEnrollment GetPatientEntityEnrollment(int id)
        {
            return _mgr.GetPatientEntityEnrollment(id);
        }
    }
}

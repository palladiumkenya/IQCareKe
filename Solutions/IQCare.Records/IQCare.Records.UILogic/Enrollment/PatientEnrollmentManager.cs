using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface.Records;
using Entities.Records.Enrollment;
using Application.Presentation;
using Application.Common;

namespace IQCare.Records.UILogic.Enrollment
{
  public  class PatientEnrollmentManager
    {
        IPatientEnrollmentManager _mgr = (IPatientEnrollmentManager)ObjectFactory.CreateInstance("BusinessProcess.Records.Enrollment,BusinessProcess.Records");
        public int addPatientEnrollment(int patientId, string enrollmentDate, int userId)
        {
            int returnValue;
           
            PatientEncounterManager patientEncounterManager = new PatientEncounterManager();
            try
            {
                PatientEntityEnrollment patientEnrollment = new PatientEntityEnrollment
                {
                    PatientId = patientId,
                    ServiceAreaId = 1,
                    EnrollmentDate = DateTime.Parse(enrollmentDate),
                    CreatedBy = userId,
                    CreateDate = DateTime.Now,
                    DeleteFlag = false
                };

                returnValue = _mgr.AddPatientEnrollment(patientEnrollment);
                return returnValue;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                throw new Exception(e.Message);
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

        public List<PatientEntityEnrollment> GetPatientByPatientIdCareEnded(int patientId)
        {
            try
            {
                return _mgr.GetPatientByPatientIdCareEnded(patientId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

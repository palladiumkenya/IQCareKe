using Application.Presentation;
using Entities.CCC.Enrollment;
using Interface.CCC.Enrollment;
using System;
using System.Collections.Generic;
using IQCare.CCC.UILogic.Visit;
using System.Linq;

namespace IQCare.CCC.UILogic.Enrollment
{
    public class PatientEnrollmentManager
    {
        IPatientEnrollmentManager _mgr = (IPatientEnrollmentManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Enrollment.BPatientEnrollment, BusinessProcess.CCC");

        public int addPatientEnrollment(int patientId, string enrollmentDate, int userId)
        {
            int returnValue;
           // int result;
            PatientEncounterManager patientEncounterManager=new PatientEncounterManager();
            try
            {
                var entityEnrollment = GetPatientEnrollmentByPatientId(patientId).Where(pe=> pe.ServiceAreaId ==1).FirstOrDefault();
                if (entityEnrollment == null)
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
                }
                else
                {
                   
                    List<PatientEntityEnrollment> listEnrollment = new List<PatientEntityEnrollment>();
                    listEnrollment.Add(entityEnrollment);
                    var enrollmentAuditData = AuditDataUtility.AuditDataUtility.Serializer(listEnrollment);

                    entityEnrollment.EnrollmentDate = DateTime.Parse(enrollmentDate);
                    entityEnrollment.AuditData = enrollmentAuditData;

                    returnValue=  updatePatientEnrollment(entityEnrollment);

                   
                }
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

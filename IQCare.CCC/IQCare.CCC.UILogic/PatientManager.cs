using Application.Presentation;
using Entities.CCC.Enrollment;
using Interface.CCC.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.CCC.UILogic
{
    public class PatientManager
    {
        IPatientManager _mgr = (IPatientManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Patient.BPatient, BusinessProcess.CCC");

        public int AddPatient(Entities.CCC.Enrollment.PatientEntity patient)
        {
            int returnValue;

            try
            {
                returnValue = _mgr.AddPatient(patient);
                return returnValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PatientEntity> CheckPersonEnrolled(int personId)
        {
            return _mgr.CheckPersonEnrolled(personId);
        }

        public PatientEntity GetPatientEntity(int patientId)
        {
            return _mgr.GetPatient(patientId);
        }

        public string GetPatientType(int patientId)
        {
            int patientTypeId = _mgr.GetPatientType(patientId);
            string patientType = LookupLogic.GetLookupNameById(patientTypeId);
            return patientType;
        }
    }
}

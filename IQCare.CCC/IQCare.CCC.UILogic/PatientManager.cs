using Application.Presentation;
using Entities.CCC.Enrollment;
using Interface.CCC.Patient;
using System;
using System.Collections.Generic;
using Entities.CCC.Lookup;
using IQCare.CCC.UILogic.Helpers;

namespace IQCare.CCC.UILogic
{
    public class PatientManager
    {
        IPatientManager _mgr = (IPatientManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Patient.BPatient, BusinessProcess.CCC");

        public int AddPatient(PatientEntity patient)
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

        public int UpdatePatient(PatientEntity patient, int id)
        {
            int returnValue = 0;
            try
            {
                returnValue = _mgr.UpdatePatient(patient, id);
                return returnValue;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }
        public PatientEntity GetPatientEntityByPersonId(int personId)
        {
            return PatientEntityHelper.MapFromPatientPersonView(_mgr.GetPatientEntityByPersonId(personId));
        }
        public PatientEntity CheckPersonEnrolled(int personId)
        {
            return PatientEntityHelper.MapFromPatientPersonView(_mgr.CheckPersonEnrolled(personId));
        }

        public PatientEntity GetPatientEntity(int patientId)
        {
            return PatientEntityHelper.MapFromPatientPersonView(_mgr.GetPatient(patientId));
        }

        public string GetPatientType(int patientId)
        {
            int patientTypeId = _mgr.GetPatientType(patientId);
            string patientType = LookupLogic.GetLookupNameById(patientTypeId);
            return patientType;
        }

        public List<PatientRegistrationLookup> GetPatientIdByPersonId(int personId)
        {
            try
            {
                return _mgr.GetPatientIdByPersonId(personId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static string CalculateYourAge(DateTime Dob)
        {
            DateTime Now = DateTime.Now;
            int Years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
            DateTime PastYearDate = Dob.AddYears(Years);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == Now)
                {
                    Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= Now)
                {
                    Months = i - 1;
                    break;
                }
            }
            
            return String.Format("Age: {0} Year(s) {1} Month(s)",
            Years, Months);
        }

        public int GetPersonId(int patientId)
        {
            return _mgr.GetPersonId(patientId);
        }
    }
}

using Application.Presentation;
using Entities.CCC.Enrollment;
using Interface.CCC.Enrollment;
using System;
using System.Collections.Generic;

namespace IQCare.CCC.UILogic.Enrollment
{
    public class PatientIdentifierManager
    {
        IPatientIdentifierManager _mgr = (IPatientIdentifierManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Enrollment.BPatientIdentifier, BusinessProcess.CCC");

        public int addPatientIdentifier(PatientEntityIdentifier patientIdentifier)
        {
            int returnValue;
            returnValue = _mgr.AddPatientIdentifier(patientIdentifier);
            return returnValue;
        }

        public int UpdatePatientIdentifier(PatientEntityIdentifier patientIdentifier)
        {
            try
            {
                return _mgr.UpdatePatientIdentifier(patientIdentifier);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<PatientEntityIdentifier> GetPatientEntityIdentifiers(int patientId, int patientEnrollmentId,
            int identifierTypeId)
        {
            return _mgr.GetPatientEntityIdentifiers(patientId, patientEnrollmentId, identifierTypeId);
        }

        public List<PatientEntityIdentifier> CheckIfIdentifierNumberIsUsed(string identifierValue, int identifierTypeId)
        {
            try
            {
                return _mgr.CheckIfIdentifierNumberIsUsed(identifierValue, identifierTypeId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}

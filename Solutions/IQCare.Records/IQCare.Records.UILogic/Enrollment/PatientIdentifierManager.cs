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
   public class PatientIdentifierManager
    {
        IPatientIdentifierManager _mgr = (IPatientIdentifierManager)ObjectFactory.CreateInstance("BusinessProcess.Records.Enrollment.BPatientIdentifier, BusinessProcess.Records");

        public int addPatientIdentifier(int patientId, int patientEnrollmentId, int identifierId, string enrollmentNo, int facilityId, bool sendEvent = true)
        {
            try
            {
                PatientEntityIdentifier patientidentifier = new PatientEntityIdentifier()
                {
                    PatientId = patientId,
                    PatientEnrollmentId = patientEnrollmentId,
                    IdentifierTypeId = identifierId,
                    IdentifierValue = enrollmentNo
                };

                int returnValue = _mgr.AddPatientIdentifier(patientidentifier);

               

                return returnValue;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int UpdatePatientIdentifier(PatientEntityIdentifier patientIdentifier, int facilityId, bool sendEvent = true)
        {
            try
            {


                int x = _mgr.UpdatePatientIdentifier(patientIdentifier);
               

                return x;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PatientEntityIdentifier> GetPatientEntityIdentifiers(int patientId, int patientEnrollmentId,
            int identifierTypeId)
        {
            try
            {
                return _mgr.GetPatientEntityIdentifiers(patientId, patientEnrollmentId, identifierTypeId);
            }
            catch (Exception  e)
            {
                throw new Exception(e.Message);
            }
       
        }

        public List<PatientEntityIdentifier> GetEntityIdentifiersByPatientIdEnrollmentId(int patientId,
            int patientEnrollmentId)
        {
            try
            {
                return _mgr.GetEntityIdentifiersByPatientIdEnrollmentId(patientId, patientEnrollmentId);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PatientEntityIdentifier> CheckIfIdentifierNumberIsUsed(string identifierValue, int identifierTypeId)
        {
            try
            {
                return _mgr.CheckIfIdentifierNumberIsUsed(identifierValue, identifierTypeId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PatientEntityIdentifier> GetPatientEntityIdentifiersByPatientId(int patientId, int identifierTypeId)
        {
            try
            {
                return _mgr.GetPatientEntityIdentifiersByPatientId(patientId, identifierTypeId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PatientEntityIdentifier> GetAllPatientEntityIdentifiers(int patientId)
        {
            try
            {
                return _mgr.GetAllPatientEntityIdentifiers(patientId);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}


using Application.Presentation;
using Entities.CCC.Enrollment;
using Interface.CCC.Enrollment;
using IQCare.Events;
using System;
using System.Collections.Generic;

namespace IQCare.CCC.UILogic.Enrollment
{
    public class PatientIdentifierManager
    {
        IPatientIdentifierManager _mgr = (IPatientIdentifierManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Enrollment.BPatientIdentifier, BusinessProcess.CCC");
        
        public int addPatientIdentifier(int patientId, int patientEnrollmentId, int identifierId, string enrollmentNo)
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

                if (returnValue > 1)
                {
                    MessageEventArgs args = new MessageEventArgs()
                    {
                        PatientId = patientId,
                        EntityId = patientidentifier.PatientEnrollmentId,
                        MessageType = MessageType.NewClientRegistration,
                        EventOccurred = "Patient Enrolled Identifier = "
                    };

                    Publisher.RaiseEventAsync(this, args).ConfigureAwait(false);
                }
                
                return returnValue;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int UpdatePatientIdentifier(PatientEntityIdentifier patientIdentifier, int facilityId)
        {
            try
            {
                

                int x=  _mgr.UpdatePatientIdentifier(patientIdentifier);
                if (x > 0)
                {
                    MessageEventArgs args = new MessageEventArgs()
                    {
                        PatientId = patientIdentifier.PatientId,
                        EntityId = patientIdentifier.PatientEnrollmentId,
                        MessageType = MessageType.UpdatedClientInformation,
                        EventOccurred = "Patient Enrolled Identifier = ",
                        FacilityId = facilityId
                    };

                    Publisher.RaiseEventAsync(this, args).ConfigureAwait(false);
                }

                return x;
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

        public List<PatientEntityIdentifier> GetEntityIdentifiersByPatientIdEnrollmentId(int patientId,
            int patientEnrollmentId)
        {
            return _mgr.GetEntityIdentifiersByPatientIdEnrollmentId(patientId, patientEnrollmentId);
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
            return _mgr.GetAllPatientEntityIdentifiers(patientId);
        }
    }
}

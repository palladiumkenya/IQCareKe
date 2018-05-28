using Application.Presentation;
using Entities.CCC.Enrollment;
using Entities.CCC.Lookup;
using Interface.CCC.Enrollment;
using Interface.CCC.Lookup;
using Interface.CCC.Patient;
using IQCare.CCC.UILogic.Helpers;
using IQCare.Events;
using System;
using System.Collections.Generic;

namespace IQCare.CCC.UILogic.Enrollment
{
    
    public class PatientIdentifierManager
    {
        IPatientIdentifierManager _mgr = (IPatientIdentifierManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Enrollment.BPatientIdentifier, BusinessProcess.CCC");

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

                if (returnValue > 1 && sendEvent)
                {
                    MessageEventArgs args = new MessageEventArgs()
                    {
                        PatientId = patientId,
                        EntityId = patientidentifier.PatientEnrollmentId,
                        MessageType = MessageType.NewClientRegistration,
                        EventOccurred = "Patient Enrolled Identifier = ",
                        FacilityId = facilityId
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

        public int UpdatePatientIdentifier(PatientEntityIdentifier patientIdentifier, int facilityId, bool sendEvent = true)
        {
            try
            {


                int x = _mgr.UpdatePatientIdentifier(patientIdentifier);
                if (x > 0 && sendEvent)
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

        //public PatientEntityIdentifier GetPatientByCardSerialNumber(string cardSerialNumber)
        //{
        //    return _mgr.GetPatientByCardSerialNumber(cardSerialNumber);
        //}
        public PatientEntity GetPatientEntityByIdentifier(string identifierCode, string identifierValue)
        {
            IIdentifiersManager _idMgr = (IIdentifiersManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Enrollment.BIdentifier, BusinessProcess.CCC");
            IPersonIdentifierManager _personmgr = (IPersonIdentifierManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Enrollment.BPersonIdentifier, BusinessProcess.CCC");
            PatientEntity patient = null;
            IPatientManager _patMgr = (IPatientManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Patient.BPatient, BusinessProcess.CCC");
            var identifier = _idMgr.GetIdentifierByCode(identifierCode);
            if (identifier == null) return patient;
            if ((IdentifierType)identifier.IdentifierType == IdentifierType.Patient)
            {
                var retVal = CheckIfIdentifierNumberIsUsed(identifierValue, identifier.Id);
                if (retVal != null && retVal.Count > 0)
                {
                    patient = PatientEntityHelper.MapFromPatientPersonView(_patMgr.GetPatient(retVal[0].PatientId));

                }
            }
            else if ((IdentifierType)identifier.IdentifierType == IdentifierType.Person)
            {
                var retVal = _personmgr.CheckIfPersonIdentifierExists(identifierValue, identifier.Id);
                if (retVal != null && retVal.Count > 0)
                {
                    patient = PatientEntityHelper.MapFromPatientPersonView(_patMgr.GetPatientEntityByPersonId(retVal[0].PersonId));

                }
            }
            return patient;
        }
        public PatientEntity GetPatientByCardSerialNumber(string cardSerialNumber)
        {
            IIdentifiersManager _idMgr = (IIdentifiersManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Enrollment.BIdentifier, BusinessProcess.CCC");
            IPersonIdentifierManager _personmgr = (IPersonIdentifierManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Enrollment.BPersonIdentifier, BusinessProcess.CCC");
            //  IPatientLookupmanager _patientLookupmanager = (IPatientLookupmanager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientLookupManager, BusinessProcess.CCC");
            PatientEntity patient = null;
            IPatientManager _patMgr = (IPatientManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Patient.BPatient, BusinessProcess.CCC");

            var identifier = _idMgr.GetIdentifierByCode("CARD_SERIAL_NUMBER");
            if (identifier != null)
            {
                var retVal = _personmgr.CheckIfPersonIdentifierExists(cardSerialNumber, identifier.Id);
                if (retVal != null && retVal.Count > 0)
                {
                    patient = PatientEntityHelper.MapFromPatientPersonView(_patMgr.GetPatientEntityByPersonId(retVal[0].PersonId));
                   
                }
            }

            return patient;
        }
    }
}

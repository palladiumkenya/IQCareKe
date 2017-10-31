using Entities.CCC.Enrollment;
using Entities.CCC.Interoperability;
using IQCare.CCC.UILogic.Enrollment;
using IQCare.DTO;
using IQCare.DTO.CommonEntities;
using IQCare.DTO.PatientRegistration;
using System;
using System.Collections.Generic;

namespace IQCare.CCC.UILogic.Interoperability
{
    public class ProcessPatient
    {
        public string Update(int patientId, int? ptn_pk, DateTime dateOfBirth, string nationalId, int facilityId, string auditData, PatientEntryPoint entryPoints, int entryPointId, string entryPointAuditData, DateTime enrollmentDate, List<DTOIdentifier> internalPatientIdentifiers)
        {
            try
            {
                var patientManager = new PatientManager();
                var patientEntryPointManager = new PatientEntryPointManager();
                var patientIdentifierManager = new PatientIdentifierManager();
                var patientEnrollmentManager = new PatientEnrollmentManager();

                PatientEntity updatePatient = new PatientEntity();
                updatePatient.ptn_pk = ptn_pk;
                updatePatient.DateOfBirth = dateOfBirth;
                updatePatient.NationalId = nationalId;
                updatePatient.FacilityId = facilityId;
                updatePatient.AuditData = auditData;

                patientManager.UpdatePatient(updatePatient, patientId);

                if (entryPoints == null)
                {
                    patientEntryPointManager.addPatientEntryPoint(patientId, entryPointId, 1);
                }
                else
                {
                    entryPoints.EntryPointId = entryPointId;
                    entryPoints.AuditData = entryPointAuditData;
                    patientEntryPointManager.UpdatePatientEntryPoint(entryPoints);   
                }

                foreach (var item in internalPatientIdentifiers)
                {
                    if (item.IdentifierType == "CCC_NUMBER" && item.AssigningAuthority == "CCC")
                    {
                        var identifiersByPatientId = patientIdentifierManager.GetPatientEntityIdentifiersByPatientId(patientId, 1);

                        if (identifiersByPatientId.Count > 0)
                        {
                            foreach (var entityIdentifier in identifiersByPatientId)
                            {
                                int enrollmentId = entityIdentifier.PatientEnrollmentId;

                                PatientEntityEnrollment entityEnrollment = patientEnrollmentManager.GetPatientEntityEnrollment(enrollmentId);
                                List<PatientEntityEnrollment> listEnrollment = new List<PatientEntityEnrollment>();
                                listEnrollment.Add(entityEnrollment);
                                var enrollmentAuditData = AuditDataUtility.AuditDataUtility.Serializer(listEnrollment);

                                entityEnrollment.EnrollmentDate = enrollmentDate;
                                entityEnrollment.AuditData = enrollmentAuditData;

                                patientEnrollmentManager.updatePatientEnrollment(entityEnrollment);

                                var entityIdentifierAuditData = AuditDataUtility.AuditDataUtility.Serializer(identifiersByPatientId);
                                entityIdentifier.IdentifierValue = item.IdentifierValue;
                                entityIdentifier.AuditData = entityIdentifierAuditData;
                                //patientIdentifierManager.UpdatePatientIdentifier(entityIdentifier);
                            }
                        }
                        else
                        {
                            int patientEnrollmentId = patientEnrollmentManager.addPatientEnrollment(patientId, enrollmentDate.ToString(), 1);
                            int patientEntryPointId = patientEntryPointManager.addPatientEntryPoint(patientId, entryPointId, 1);
                            //int patientIdentifierId = patientIdentifierManager.addPatientIdentifier(patientId, patientEnrollmentId, 1, item.IdentifierValue);
                        }
                    }
                }

                return "Successfully updated patient";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static PatientRegistrationDTO Get(int patientId)
        {
            PatientMessageManager patientMessageManager = new PatientMessageManager();
            PatientMessage patientMessage = patientMessageManager.GetPatientMessageByEntityId(patientId);
            PatientRegistrationDTO registration = new PatientRegistrationDTO();

            if (patientMessage != null)
            {
                INTERNALPATIENTID internalPatientId = new INTERNALPATIENTID();
                internalPatientId.ID = patientMessage.IdentifierValue;
                internalPatientId.IDENTIFIER_TYPE = "CCC_NUMBER";
                internalPatientId.ASSIGNING_AUTHORITY = "CCC";
                

                registration.PATIENT_IDENTIFICATION = registration.PATIENT_IDENTIFICATION == null ? new PATIENTIDENTIFICATION() : registration.PATIENT_IDENTIFICATION;
                registration.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID = registration.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID == null ? new List<INTERNALPATIENTID>() : registration.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID;
                registration.PATIENT_IDENTIFICATION.PATIENT_NAME = registration.PATIENT_IDENTIFICATION.PATIENT_NAME == null ? new PATIENTNAME() : registration.PATIENT_IDENTIFICATION.PATIENT_NAME;
                registration.PATIENT_IDENTIFICATION.MOTHER_NAME = registration.PATIENT_IDENTIFICATION.MOTHER_NAME == null ? new PATIENTNAME() : registration.PATIENT_IDENTIFICATION.MOTHER_NAME;
                registration.PATIENT_IDENTIFICATION.PATIENT_ADDRESS = registration.PATIENT_IDENTIFICATION.PATIENT_ADDRESS == null ? new PATIENTADDRESS() : registration.PATIENT_IDENTIFICATION.PATIENT_ADDRESS;
                registration.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS = registration.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS == null ? new PHYSICAL_ADDRESS() : registration.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS;
                registration.VISIT = registration.VISIT == null ? new VISIT() : registration.VISIT;
                registration.NEXT_OF_KIN = registration.NEXT_OF_KIN == null ? new List<NEXTOFKIN>() : registration.NEXT_OF_KIN;

                //Start setting values
                registration.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID.Add(internalPatientId);
                if (patientMessage.NATIONAL_ID != null && patientMessage.NATIONAL_ID != "99999999")
                {
                    INTERNALPATIENTID internalNationalId = new INTERNALPATIENTID();
                    internalNationalId.ID = patientMessage.NATIONAL_ID;
                    internalNationalId.IDENTIFIER_TYPE = "NATIONAL_ID";
                    internalNationalId.ASSIGNING_AUTHORITY = "GOK";

                    registration.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID.Add(internalNationalId);
                }
                //set names
                registration.PATIENT_IDENTIFICATION.PATIENT_NAME.FIRST_NAME = !string.IsNullOrWhiteSpace(patientMessage.FIRST_NAME) ? patientMessage.FIRST_NAME: "";
                registration.PATIENT_IDENTIFICATION.PATIENT_NAME.MIDDLE_NAME = !string.IsNullOrWhiteSpace(patientMessage.MIDDLE_NAME) ? patientMessage.MIDDLE_NAME : "";
                registration.PATIENT_IDENTIFICATION.PATIENT_NAME.LAST_NAME = !string.IsNullOrWhiteSpace(patientMessage.LAST_NAME) ? patientMessage.LAST_NAME : "";
                //set DOB
                registration.PATIENT_IDENTIFICATION.DATE_OF_BIRTH = !string.IsNullOrWhiteSpace(patientMessage.DATE_OF_BIRTH) ? patientMessage.DATE_OF_BIRTH : "";
                registration.PATIENT_IDENTIFICATION.DATE_OF_BIRTH_PRECISION = !string.IsNullOrWhiteSpace(patientMessage.DATE_OF_BIRTH_PRECISION) ? patientMessage.DATE_OF_BIRTH_PRECISION : "";
                //set sex
                registration.PATIENT_IDENTIFICATION.SEX = !string.IsNullOrWhiteSpace(patientMessage.SEX) ? patientMessage.SEX : "";
                //set phone number
                registration.PATIENT_IDENTIFICATION.PHONE_NUMBER = !string.IsNullOrWhiteSpace(patientMessage.MobileNumber) ? patientMessage.MobileNumber : "";
                //set marital status
                registration.PATIENT_IDENTIFICATION.MARITAL_STATUS = !string.IsNullOrWhiteSpace(patientMessage.MARITAL_STATUS) ? patientMessage.MARITAL_STATUS : "";
                //set patient address variables
                registration.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.POSTAL_ADDRESS = !string.IsNullOrWhiteSpace(patientMessage.PhysicalAddress) ? patientMessage.PhysicalAddress : "";
                registration.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.VILLAGE = !string.IsNullOrWhiteSpace(patientMessage.Village) ? patientMessage.Village : "";
                registration.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.WARD = !string.IsNullOrWhiteSpace(patientMessage.WardName) ? patientMessage.WardName : "";
                registration.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.SUB_COUNTY = !string.IsNullOrWhiteSpace(patientMessage.Subcountyname) ? patientMessage.Subcountyname : "";
                registration.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.COUNTY = !string.IsNullOrWhiteSpace(patientMessage.CountyName) ? patientMessage.CountyName : "";
                registration.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.NEAREST_LANDMARK = !string.IsNullOrWhiteSpace(patientMessage.Landmark) ? patientMessage.Landmark : "";
                //set visit variables
                registration.VISIT.HIV_CARE_INITIATION_DATE = !string.IsNullOrWhiteSpace(patientMessage.DateOfEnrollment) ? patientMessage.DateOfEnrollment : "";
                registration.VISIT.PATIENT_SOURCE = !string.IsNullOrWhiteSpace(patientMessage.EntryPoint) ? patientMessage.EntryPoint : "";
                registration.VISIT.PATIENT_TYPE = !string.IsNullOrWhiteSpace(patientMessage.PatientType) ? patientMessage.PatientType : "";
                registration.VISIT.VISIT_DATE = !string.IsNullOrWhiteSpace(patientMessage.DateOfRegistration) ? patientMessage.DateOfRegistration : "";
                //set if patient is dead
                registration.PATIENT_IDENTIFICATION.DEATH_DATE = !string.IsNullOrWhiteSpace(patientMessage.DateOfDeath) ? patientMessage.DateOfDeath : "";
                registration.PATIENT_IDENTIFICATION.DEATH_INDICATOR = !string.IsNullOrWhiteSpace(patientMessage.DeathIndicator) ? patientMessage.DeathIndicator : "";

                if (!string.IsNullOrWhiteSpace(patientMessage.TFIRST_NAME) && !string.IsNullOrWhiteSpace(patientMessage.TLAST_NAME))
                {
                    NEXTOFKIN treatmentSupporter = new NEXTOFKIN();
                    treatmentSupporter.NOK_NAME.FIRST_NAME = !string.IsNullOrWhiteSpace(patientMessage.TFIRST_NAME) ? patientMessage.TFIRST_NAME : "";
                    treatmentSupporter.NOK_NAME.MIDDLE_NAME = !string.IsNullOrWhiteSpace(patientMessage.TMIDDLE_NAME) ? patientMessage.TMIDDLE_NAME : "";
                    treatmentSupporter.NOK_NAME.LAST_NAME = !string.IsNullOrWhiteSpace(patientMessage.TLAST_NAME) ? patientMessage.TLAST_NAME : "";
                    treatmentSupporter.DATE_OF_BIRTH = !string.IsNullOrWhiteSpace(patientMessage.TDATE_OF_BIRTH) ? patientMessage.TDATE_OF_BIRTH : "";
                    treatmentSupporter.PHONE_NUMBER = !string.IsNullOrWhiteSpace(patientMessage.TPHONE_NUMBER) ? patientMessage.TPHONE_NUMBER : "";
                    treatmentSupporter.ADDRESS = !string.IsNullOrWhiteSpace(patientMessage.TADDRESS) ? patientMessage.TADDRESS : "";
                    treatmentSupporter.CONTACT_ROLE = "T";
                    treatmentSupporter.RELATIONSHIP = String.Empty;
                    treatmentSupporter.SEX = !string.IsNullOrWhiteSpace(patientMessage.TSEX) ? patientMessage.TSEX : "";

                    registration.NEXT_OF_KIN.Add(treatmentSupporter);
                }
            }

            return registration;
        }
    }
}

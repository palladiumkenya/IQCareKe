using Entities.CCC.Enrollment;
using Entities.CCC.Interoperability;
using IQCare.CCC.UILogic.Enrollment;
using IQCare.DTO;
using IQCare.DTO.CommonEntities;
using IQCare.DTO.PatientRegistration;
using System;
using System.Collections.Generic;
using Entities.CCC.Lookup;
using IQCare.CCC.UILogic.Visit;

namespace IQCare.CCC.UILogic.Interoperability
{
    public class ProcessPatient
    {
        public static string Update(int patientId, int? ptn_pk, DateTime dateOfBirth, string nationalId, int facilityId, int entryPointId, DateTime enrollmentDate, string cccNumber, PatientLookup patient)
        {
            try
            {
                var patientManager = new PatientManager();
                var patientEntryPointManager = new PatientEntryPointManager();
                var patientIdentifierManager = new PatientIdentifierManager();
                var patientEnrollmentManager = new PatientEnrollmentManager();

                List<PatientLookup> patientLookups = new List<PatientLookup>();
                patientLookups.Add(patient);
                var entity = patientLookups.ConvertAll(x => new PatientEntity { Id = x.Id, Active = x.Active, DateOfBirth = x.DateOfBirth, ptn_pk = x.ptn_pk, PatientType = x.PatientType, PatientIndex = x.PatientIndex, NationalId = x.NationalId, FacilityId = x.FacilityId });
                var patientAuditData = AuditDataUtility.AuditDataUtility.Serializer(entity);

                PatientEntity updatePatient = new PatientEntity();
                updatePatient.ptn_pk = ptn_pk;
                updatePatient.DateOfBirth = dateOfBirth;
                updatePatient.NationalId = nationalId;
                updatePatient.FacilityId = facilityId;
                updatePatient.AuditData = patientAuditData;

                patientManager.UpdatePatient(updatePatient, patientId);

                string entryPointAuditData = null;
                List<PatientEntryPoint> entryPoints = patientEntryPointManager.GetPatientEntryPoints(patient.Id, 1);
                if (entryPoints.Count > 0)
                {
                    entryPointAuditData = AuditDataUtility.AuditDataUtility.Serializer(entryPoints);

                    entryPoints[0].EntryPointId = entryPointId;
                    entryPoints[0].AuditData = entryPointAuditData;
                    patientEntryPointManager.UpdatePatientEntryPoint(entryPoints[0]);
                }
                else
                {
                    patientEntryPointManager.addPatientEntryPoint(patientId, entryPointId, 1);
                }

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
                        entityIdentifier.IdentifierValue = cccNumber;
                        entityIdentifier.AuditData = entityIdentifierAuditData;
                        patientIdentifierManager.UpdatePatientIdentifier(entityIdentifier, facilityId, false);
                    }
                }
                else
                {
                    int patientEnrollmentId = patientEnrollmentManager.addPatientEnrollment(patientId, enrollmentDate.ToString(), 1);
                    int patientEntryPointId = patientEntryPointManager.addPatientEntryPoint(patientId, entryPointId, 1);
                    int patientIdentifierId = patientIdentifierManager.addPatientIdentifier(patientId, patientEnrollmentId, 1, cccNumber, facilityId, false);
                }

                return "Successfully updated patient";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static string Add(string firstName, string middleName, string lastName, int sex, int userId, DateTime dob, bool dobPrecision, int facilityId, int patientType, string nationalId, int visitType, DateTime dateOfEnrollment, string cccNumber, int entryPointId)
        {
            try
            {
                PersonManager personManager = new PersonManager();
                PatientManager patientManager = new PatientManager();
                PatientMasterVisitManager patientMasterVisitManager = new PatientMasterVisitManager();
                PatientEnrollmentManager patientEnrollmentManager = new PatientEnrollmentManager();
                PatientEntryPointManager patientEntryPointManager = new PatientEntryPointManager();
                PersonLookUpManager personLookUp = new PersonLookUpManager();
                PersonContactLookUpManager personContactLookUpManager = new PersonContactLookUpManager();
                MstPatientLogic mstPatientLogic = new MstPatientLogic();
                PatientIdentifierManager patientIdentifierManager = new PatientIdentifierManager();

                var personContacts = new List<PersonContactLookUp>();
                int ptn_Pk = 0;

                //Start Saving
                int personId = personManager.AddPersonUiLogic(firstName, middleName, lastName, sex, userId, dob, dobPrecision);
                String sDate = DateTime.Now.ToString();
                DateTime datevalue = Convert.ToDateTime(sDate);
                var patientIndex = datevalue.Year.ToString() + '-' + personId;

                PatientEntity patientEntity = new PatientEntity();
                patientEntity.PersonId = personId;
                patientEntity.ptn_pk = 0;
                patientEntity.FacilityId = facilityId;
                patientEntity.PatientType = patientType;
                patientEntity.PatientIndex = patientIndex;
                patientEntity.DateOfBirth = dob;
                patientEntity.NationalId = (nationalId);
                patientEntity.Active = true;
                patientEntity.CreatedBy = 1;
                patientEntity.CreateDate = DateTime.Now;
                patientEntity.DeleteFlag = false;
                patientEntity.DobPrecision = dobPrecision;

                int patientId = patientManager.AddPatient(patientEntity);

                //Add enrollment visit
                int patientMasterVisitId = patientMasterVisitManager.AddPatientMasterVisit(patientId, userId, visitType);
                //Enroll Patient to service
                int patientEnrollmentId = patientEnrollmentManager.addPatientEnrollment(patientId, dateOfEnrollment.ToString(), userId);
                //Add enrollment entry point
                int patientEntryPointId = patientEntryPointManager.addPatientEntryPoint(patientId, entryPointId, userId);

                //Get User Details to be used in BLUE CARD
                var patient_person_details = personLookUp.GetPersonById(personId);
                var greencardlookup = new PersonGreenCardLookupManager();
                var greencardptnpk = greencardlookup.GetPtnPkByPersonId(personId);

                if (patient_person_details != null)
                {
                    var maritalStatus = new PersonMaritalStatusManager().GetCurrentPatientMaritalStatus(personId);
                    personContacts = personContactLookUpManager.GetPersonContactByPersonId(personId);
                    var address = "";
                    var phone = "";

                    if (personContacts.Count > 0)
                    {
                        address = personContacts[0].PhysicalAddress;
                        phone = personContacts[0].MobileNumber;
                    }

                    var MaritalStatusId = 0;
                    if (maritalStatus != null)
                    {
                        MaritalStatusId = maritalStatus.MaritalStatusId;
                    }

                    var sexBluecard = 0;
                    var enrollmentBlueCardId = "";

                    if (LookupLogic.GetLookupNameById(patient_person_details.Sex) == "Male")
                    {
                        sexBluecard = 16;
                    }
                    else if (LookupLogic.GetLookupNameById(patient_person_details.Sex) == "Female")
                    {
                        sexBluecard = 17;
                    }

                    enrollmentBlueCardId = cccNumber;

                    if (greencardptnpk.Count == 0)
                    {
                        ptn_Pk = mstPatientLogic.InsertMstPatient((patient_person_details.FirstName), (patient_person_details.LastName), (patient_person_details.MiddleName), facilityId, enrollmentBlueCardId, entryPointId, dateOfEnrollment, sexBluecard, dob, 1, MaritalStatusId, address, phone, 1, facilityId.ToString(), 203, dateOfEnrollment, DateTime.Now);

                        patientEntity.ptn_pk = ptn_Pk;
                        patientManager.UpdatePatient(patientEntity, patientId);
                    }
                    else
                    {
                        ptn_Pk = greencardptnpk[0].Ptn_Pk;
                        patientEntity.ptn_pk = greencardptnpk[0].Ptn_Pk;
                        patientManager.UpdatePatient(patientEntity, patientId);
                    }
                }

                if (patientMasterVisitId > 0)
                {
                    int patientIdentifierId = patientIdentifierManager.addPatientIdentifier(patientId, patientEnrollmentId, 1, cccNumber, facilityId);

                    if (greencardptnpk.Count == 0)
                    {
                        mstPatientLogic.AddOrdVisit(ptn_Pk, facilityId, DateTime.Now, 110, 1, DateTime.Now, 203);
                    }
                }

                return "successfully saved";
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
                registration.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID = registration.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID == null ? new EXTERNALPATIENTID() : registration.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID;
                registration.PATIENT_IDENTIFICATION.PATIENT_NAME = registration.PATIENT_IDENTIFICATION.PATIENT_NAME == null ? new PATIENTNAME() : registration.PATIENT_IDENTIFICATION.PATIENT_NAME;
                registration.PATIENT_IDENTIFICATION.MOTHER_NAME = registration.PATIENT_IDENTIFICATION.MOTHER_NAME == null ? null : registration.PATIENT_IDENTIFICATION.MOTHER_NAME;
                registration.PATIENT_IDENTIFICATION.PATIENT_ADDRESS = registration.PATIENT_IDENTIFICATION.PATIENT_ADDRESS == null ? new PATIENTADDRESS() : registration.PATIENT_IDENTIFICATION.PATIENT_ADDRESS;
                registration.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS = registration.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS == null ? new PHYSICAL_ADDRESS() : registration.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS;
                registration.PATIENT_VISIT = registration.PATIENT_VISIT == null ? new VISIT() : registration.PATIENT_VISIT;
                registration.NEXT_OF_KIN = registration.NEXT_OF_KIN == null ? new List<NEXTOFKIN>() : registration.NEXT_OF_KIN;

                //External Patient Id
                registration.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.ID = String.Empty;
                registration.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.ASSIGNING_AUTHORITY = "MPI";
                registration.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.IDENTIFIER_TYPE = "GODS_NUMBER";
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
                registration.PATIENT_VISIT.HIV_CARE_INITIATION_DATE = !string.IsNullOrWhiteSpace(patientMessage.DateOfEnrollment) ? patientMessage.DateOfEnrollment : "";
                registration.PATIENT_VISIT.PATIENT_SOURCE = !string.IsNullOrWhiteSpace(patientMessage.EntryPoint) ? patientMessage.EntryPoint : "";
                registration.PATIENT_VISIT.PATIENT_TYPE = !string.IsNullOrWhiteSpace(patientMessage.PatientType) ? patientMessage.PatientType : "";
                registration.PATIENT_VISIT.VISIT_DATE = !string.IsNullOrWhiteSpace(patientMessage.DateOfRegistration) ? patientMessage.DateOfRegistration : "";
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

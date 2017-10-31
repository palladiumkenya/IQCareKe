using IQCare.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Application.Presentation;
using Entities.CCC.Baseline;
using Entities.CCC.Enrollment;
using Entities.CCC.Lookup;
using Interface.CCC.Baseline;
using IQCare.CCC.UILogic.Enrollment;
using IQCare.CCC.UILogic.Visit;
using Interface.Security;
using IQCare.DTO.PatientRegistration;
using Newtonsoft.Json;

namespace IQCare.CCC.UILogic.Interoperability
{
    public class ExMessage
    {
        public string Msg { get; set; }
        public int Code { get; set; }
    }

    public class ProcessRegistration : IInteropDTO<PatientRegistrationDTO>
    {
        private readonly IPatientLinkageManager linkageManager = (IPatientLinkageManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Baseline.BPatientLinkageManager, BusinessProcess.CCC");
        private readonly IPatientHivTestingManager _hivTestingManager = (IPatientHivTestingManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Baseline.BPatientHivTestingManager, BusinessProcess.CCC");
        IUser LoginManager = (IUser)ObjectFactory.CreateInstance("BusinessProcess.Security.BUser, BusinessProcess.Security");

        public string msg { get; set; }

        public PatientRegistrationDTO Get(int entityId)
        {
            return ProcessPatient.Get(entityId);
        }

        public string Save(PatientRegistrationDTO registration)
        {
            try
            {
                PersonManager personManager = new PersonManager();
                PatientManager patientManager = new PatientManager();
                LookupLogic lookupLogic = new LookupLogic();
                PatientMasterVisitManager patientMasterVisitManager = new PatientMasterVisitManager();
                PatientEnrollmentManager patientEnrollmentManager = new PatientEnrollmentManager();
                PatientEntryPointManager patientEntryPointManager = new PatientEntryPointManager();
                PersonLookUpManager personLookUp = new PersonLookUpManager();
                PersonContactLookUpManager personContactLookUpManager = new PersonContactLookUpManager();
                MstPatientLogic mstPatientLogic = new MstPatientLogic();
                PatientIdentifierManager patientIdentifierManager = new PatientIdentifierManager();


                var personContacts = new List<PersonContactLookUp>();
                //Get FacilityId
                int facilityId = Convert.ToInt32(registration.MESSAGE_HEADER.SENDING_FACILITY);
                //Get Gender
                string gender = registration.PATIENT_IDENTIFICATION.SEX == "F" ? "Female" : "Male";
                String sDate = DateTime.Now.ToString();
                DateTime datevalue = Convert.ToDateTime(sDate);
                //IQCare Sex
                int sex = lookupLogic.GetItemIdByGroupAndItemName("Gender", gender)[0].ItemId;
                //Assume this is a new patient
                int patientType = lookupLogic.GetItemIdByGroupAndItemName("PatientType", "New")[0].ItemId;
                //Get Enrollment Id Type
                int visitType = lookupLogic.GetItemIdByGroupAndItemName("VisitType", "Enrollment")[0].ItemId;
                //Get DOB
                DateTime DOB;
                DateTime.TryParse(registration.PATIENT_IDENTIFICATION.DATE_OF_BIRTH, out DOB);
                bool DOB_Precision = true;
                //Get Enrollment Date
                DateTime dateOfEnrollment = DateTime.MinValue;
                //Get Patient Names
                string firstName = registration.PATIENT_IDENTIFICATION.PATIENT_NAME.FIRST_NAME;
                string middleName = registration.PATIENT_IDENTIFICATION.PATIENT_NAME.MIDDLE_NAME;
                string lastName = registration.PATIENT_IDENTIFICATION.PATIENT_NAME.LAST_NAME;
                string nationalId = String.Empty;

                
                foreach (var internalpatientid in registration.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID)
                {
                    if (internalpatientid.IDENTIFIER_TYPE == "NATIONAL_ID")
                    {
                        nationalId = internalpatientid.ID;
                    }                  
                }

                //bool dobPrecision = registration.Patient.DobPrecision;
                int entryPointId = 0;
                int ptn_Pk = 0;

                //Start Saving
                int personId = personManager.AddPersonUiLogic(firstName, middleName, lastName, sex, 1, DOB, DOB_Precision);
                var patientIndex = datevalue.Year.ToString() + '-' + personId;

                PatientEntity patientEntity = new PatientEntity();
                patientEntity.PersonId = personId;
                patientEntity.ptn_pk = 0;
                patientEntity.FacilityId = facilityId;
                patientEntity.PatientType = patientType;
                patientEntity.PatientIndex = patientIndex;
                patientEntity.DateOfBirth = DOB;
                patientEntity.NationalId = (nationalId);
                patientEntity.Active = true;
                patientEntity.CreatedBy = 1;
                patientEntity.CreateDate = DateTime.Now;
                patientEntity.DeleteFlag = false;
                patientEntity.DobPrecision = DOB_Precision;

                int patientId = patientManager.AddPatient(patientEntity);

                //Add enrollment visit
                int patientMasterVisitId = patientMasterVisitManager.AddPatientMasterVisit(patientId, 1, visitType);
                //Enroll Patient to service
                int patientEnrollmentId = patientEnrollmentManager.addPatientEnrollment(patientId, dateOfEnrollment.ToString(), 1);
                //Add enrollment entry point
                int patientEntryPointId = patientEntryPointManager.addPatientEntryPoint(patientId, entryPointId, 1);

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
                    var facility = lookupLogic.GetFacility();

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

                    foreach (var item in registration.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID)
                    {
                        if (item.IDENTIFIER_TYPE == "CCC_NUMBER")
                        {
                            enrollmentBlueCardId = item.ID;
                        }
                    }


                    if (greencardptnpk.Count == 0)
                    {
                        ptn_Pk = mstPatientLogic.InsertMstPatient((patient_person_details.FirstName), (patient_person_details.LastName), (patient_person_details.MiddleName), facility.FacilityID, enrollmentBlueCardId, entryPointId, dateOfEnrollment, sexBluecard, DOB, 1, MaritalStatusId, address, phone, 1, facilityId.ToString(), 203, dateOfEnrollment, DateTime.Now);

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

                    foreach (var item in registration.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID)
                    {
                        if (item.IDENTIFIER_TYPE == "CCC_NUMBER" && item.ASSIGNING_AUTHORITY == "CCC")
                        {
                            int patientIdentifierId = patientIdentifierManager.addPatientIdentifier(patientId, patientEnrollmentId, 1, item.ID, facilityId);
                        }
                    }

                    if (greencardptnpk.Count == 0)
                    {
                        mstPatientLogic.AddOrdVisit(ptn_Pk, facilityId, DateTime.Now, 110, 1, DateTime.Now, 203);
                    }
                }

                msg = "success";
            }
            catch (Exception)
            {
                msg = "error";
            }

            return msg;
        }

        public string Update(PatientRegistrationDTO registration)
        {
            ExMessage message = new ExMessage();
            //try
            //{
            //    PatientRegistrationValidation validation = new PatientRegistrationValidation();
            //    ProcessPatient processPatient = new ProcessPatient();
            //    PatientLookupManager patientLookup = new PatientLookupManager();
            //    var patientEntryPointManager = new PatientEntryPointManager();

            //    LookupLogic lookupLogic = new LookupLogic();

            //    string cccNumber = null;
            //    PatientLookup patient = new PatientLookup();
            //    int entryPointId = 0;
            //    string entryPointAuditData = null;

            //    msg = validation.ValidateInterOperabilityRegistration(registration);

            //    var lookupEntryPoints = lookupLogic.GetItemIdByGroupAndDisplayName("Entrypoint", registration.EntryPoint);
            //    if (lookupEntryPoints.Count > 0)
            //    {
            //        entryPointId = lookupEntryPoints[0].ItemId;
            //    }
                
            //    DateTime DOB = !string.IsNullOrWhiteSpace(registration.Patient.DateOfBirth) ? DateTime.Parse(registration.Patient.DateOfBirth) : DateTime.MinValue;
            //    DateTime enrollmentDate = !string.IsNullOrWhiteSpace(registration.DateOfEnrollment) ? DateTime.Parse(registration.DateOfEnrollment) : DateTime.MinValue;
            //    DataSet ds = LoginManager.GetFacilitySettings();
            //    int facilityId = Convert.ToInt32(ds.Tables[0].Rows[0]["PosID"]);

            //    foreach (var item in registration.InternalPatientIdentifiers)
            //    {
            //        if (item.IdentifierType == "CCC_NUMBER" && item.AssigningAuthority== "CCC")
            //        {
            //            cccNumber = item.IdentifierValue;
            //        }
            //    }

            //    List<PatientLookup> patientLookups = new List<PatientLookup>();
            //    patientLookups.Add(patient);
            //    var entity = patientLookups.ConvertAll(x => new PatientEntity { Id = x.Id, Active = x.Active, DateOfBirth = x.DateOfBirth, ptn_pk = x.ptn_pk, PatientType = x.PatientType, PatientIndex = x.PatientIndex, NationalId = x.NationalId, FacilityId = x.FacilityId });
            //    var patientAuditData = AuditDataUtility.AuditDataUtility.Serializer(entity);
            //    patient = patientLookup.GetPatientByCccNumber(cccNumber);
            //    List<PatientEntryPoint> entryPoints = patientEntryPointManager.GetPatientEntryPoints(patient.Id, 1);
            //    if (entryPoints.Count > 0)
            //    {
            //        entryPointAuditData = AuditDataUtility.AuditDataUtility.Serializer(entryPoints);
            //    }
            //    else
            //    {
            //        entryPoints.Add(new PatientEntryPoint());
            //    }

            //    msg = processPatient.Update(patient.Id, patient.ptn_pk, DOB, registration.Patient.NationalId, facilityId, patientAuditData, entryPoints[0], entryPointId, entryPointAuditData, enrollmentDate, registration.InternalPatientIdentifiers);

            //    message.Msg = msg;
            //    message.Code = 0;
            //}
            //catch (Exception e)
            //{
            //    message.Msg = e.Message;
            //    message.Code = 1;
            //}

            return JsonConvert.SerializeObject(message);
        }
    }
}

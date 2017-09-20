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
using Newtonsoft.Json;

namespace IQCare.CCC.UILogic.Interoperability
{
    public class ExMessage
    {
        public string Msg { get; set; }
        public int Code { get; set; }
    }

    public class ProcessRegistration : IInteropDTO<DTO.Registration>
    {
        private readonly IPatientLinkageManager linkageManager = (IPatientLinkageManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Baseline.BPatientLinkageManager, BusinessProcess.CCC");
        private readonly IPatientHivTestingManager _hivTestingManager = (IPatientHivTestingManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Baseline.BPatientHivTestingManager, BusinessProcess.CCC");
        IUser LoginManager = (IUser)ObjectFactory.CreateInstance("BusinessProcess.Security.BUser, BusinessProcess.Security");

        public string msg { get; set; }
        public Registration Get(int entityId)
        {
            throw new NotImplementedException();
        }

        public string Save(Registration registration)
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

                DataSet ds = LoginManager.GetFacilitySettings();
                int facilityId = Convert.ToInt32(ds.Tables[0].Rows[0]["PosID"]);
                string gender;
                switch (registration.Patient.Sex)
                {
                    case "F":
                        gender = "Female";
                        break;
                    case "M":
                        gender = "Male";
                        break;
                    default:
                        gender = "";
                        break;
                }

                String sDate = DateTime.Now.ToString();
                DateTime datevalue = Convert.ToDateTime(sDate);
                int sex = lookupLogic.GetItemIdByGroupAndItemName("Gender", gender)[0].ItemId;
                int patientType = lookupLogic.GetItemIdByGroupAndItemName("PatientType", "New")[0].ItemId;
                int visitType = lookupLogic.GetItemIdByGroupAndItemName("VisitType", "Enrollment")[0].ItemId;
                DateTime DOB = registration.Patient.DateOfBirth.HasValue ? registration.Patient.DateOfBirth.Value : DateTime.MinValue;
                DateTime DateOfEnrollment = registration.DateOfEnrollment.HasValue ? registration.DateOfEnrollment.Value : DateTime.MinValue;

                int personId = personManager.AddPersonUiLogic(registration.Patient.FirstName,
                    registration.Patient.MiddleName,
                    registration.Patient.LastName, sex, 1, DOB,
                    registration.Patient.DobPrecision);
                var patientIndex = datevalue.Year.ToString() + '-' + personId;
                string nationalId = registration.Patient.NationalId;
                bool dobPrecision = registration.Patient.DobPrecision;
                int entryPointId = Convert.ToInt32(registration.EntryPoint);
                int ptn_Pk = 0;

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
                patientEntity.DobPrecision = dobPrecision;

                int patientId = patientManager.AddPatient(patientEntity);

                //Add enrollment visit
                int patientMasterVisitId = patientMasterVisitManager.AddPatientMasterVisit(patientId, 1, visitType);
                //Enroll Patient to service
                int patientEnrollmentId = patientEnrollmentManager.addPatientEnrollment(patientId, DateOfEnrollment.ToString(), 1);
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

                    foreach (var item in registration.InternalPatientIdentifiers)
                    {
                        if (item.IdentifierType == "CCC_NUMBER")
                        {
                            enrollmentBlueCardId = item.IdentifierValue;
                        }
                    }


                    if (greencardptnpk.Count == 0)
                    {
                        ptn_Pk = mstPatientLogic.InsertMstPatient(
                            (patient_person_details.FirstName),
                            (patient_person_details.LastName),
                            (patient_person_details.MiddleName),
                            facility.FacilityID, enrollmentBlueCardId, entryPointId,
                            DateOfEnrollment, sexBluecard,
                            DOB,
                            1, MaritalStatusId,
                            address, phone, 1, facilityId.ToString(),
                            203, DateOfEnrollment, DateTime.Now);

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

                    foreach (var item in registration.InternalPatientIdentifiers)
                    {
                        if (item.IdentifierType == "CCC_NUMBER" && item.AssigningAuthority == "CCC")
                        {
                            int patientIdentifierId = patientIdentifierManager.addPatientIdentifier(patientId,
                                patientEnrollmentId, 1, item.IdentifierValue);

                            var identifierManager = new IdentifierManager();
                            var identifierList = identifierManager.GetIdentifiersById(1);
                            var hivtesting = _hivTestingManager.GetAll().OrderByDescending(y => y.Id)
                                .FirstOrDefault(n => n.PersonId == personId);
                            if (identifierList.Count > 0)
                            {
                                if (identifierList[0].Code == "CCCNumber")
                                {
                                    if (hivtesting != null)
                                    {
                                        hivtesting.ReferredToCare = true;
                                        _hivTestingManager.UpdatePatientHivTesting(hivtesting);

                                        PatientLinkage patLinkage = new PatientLinkage();
                                        patLinkage.LinkageDate = DateOfEnrollment;
                                        patLinkage.CCCNumber = item.IdentifierValue;
                                        patLinkage.PersonId = personId;
                                        patLinkage.CreatedBy = 1;
                                        patLinkage.Enrolled = true;
                                        patLinkage.PatientId = patientId;

                                        linkageManager.AddPatientLinkage(patLinkage);
                                    }
                                }
                            }
                        }
                    }

                    if (greencardptnpk.Count == 0)
                    {
                        mstPatientLogic.AddOrdVisit(ptn_Pk, facilityId, DateTime.Now, 110,
                            1, DateTime.Now, 203);
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

        public string Update(Registration registration)
        {
            ExMessage message = new ExMessage();
            try
            {
                PatientRegistrationValidation validation = new PatientRegistrationValidation();
                ProcessPatient processPatient = new ProcessPatient();
                PatientLookupManager patientLookup = new PatientLookupManager();
                var patientEntryPointManager = new PatientEntryPointManager();

                LookupLogic lookupLogic = new LookupLogic();

                string cccNumber = null;
                PatientLookup patient = new PatientLookup();
                int entryPointId = 0;
                string entryPointAuditData = null;

                msg = validation.ValidateInterOperabilityRegistration(registration);

                var lookupEntryPoints = lookupLogic.GetItemIdByGroupAndDisplayName("Entrypoint", registration.EntryPoint);
                if (lookupEntryPoints.Count > 0)
                {
                    entryPointId = lookupEntryPoints[0].ItemId;
                }
                
                DateTime DOB = registration.Patient.DateOfBirth.HasValue ? registration.Patient.DateOfBirth.Value : DateTime.MinValue;
                DateTime enrollmentDate = registration.DateOfEnrollment.HasValue ? registration.DateOfEnrollment.Value : DateTime.MinValue;
                DataSet ds = LoginManager.GetFacilitySettings();
                int facilityId = Convert.ToInt32(ds.Tables[0].Rows[0]["PosID"]);

                foreach (var item in registration.InternalPatientIdentifiers)
                {
                    if (item.IdentifierType == "CCC_NUMBER" && item.AssigningAuthority== "CCC")
                    {
                        cccNumber = item.IdentifierValue;
                    }
                }

                List<PatientLookup> patientLookups = new List<PatientLookup>();
                patientLookups.Add(patient);
                var entity = patientLookups.ConvertAll(x => new PatientEntity { Id = x.Id, Active = x.Active, DateOfBirth = x.DateOfBirth, ptn_pk = x.ptn_pk, PatientType = x.PatientType, PatientIndex = x.PatientIndex, NationalId = x.NationalId, FacilityId = x.FacilityId });
                var patientAuditData = AuditDataUtility.AuditDataUtility.Serializer(entity);
                patient = patientLookup.GetPatientByCccNumber(cccNumber);
                List<PatientEntryPoint> entryPoints = patientEntryPointManager.GetPatientEntryPoints(patient.Id, 1);
                if (entryPoints.Count > 0)
                {
                    entryPointAuditData = AuditDataUtility.AuditDataUtility.Serializer(entryPoints);
                }
                else
                {
                    entryPoints.Add(new PatientEntryPoint());
                }

                msg = processPatient.Update(patient.Id, patient.ptn_pk, DOB, registration.Patient.NationalId, facilityId, patientAuditData, entryPoints[0], entryPointId, entryPointAuditData, enrollmentDate, registration.InternalPatientIdentifiers);

                message.Msg = msg;
                message.Code = 0;
            }
            catch (Exception e)
            {
                message.Msg = e.Message;
                message.Code = 1;
            }

            return JsonConvert.SerializeObject(message);
        }
    }
}

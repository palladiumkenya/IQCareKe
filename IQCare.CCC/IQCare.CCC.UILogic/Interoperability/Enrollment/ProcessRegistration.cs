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
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;
using IQCare.CCC.UILogic.Interoperability.DTOValidator;

namespace IQCare.CCC.UILogic.Interoperability.Enrollment
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
                LookupLogic lookupLogic = new LookupLogic();
                PatientLookupManager patientLookup = new PatientLookupManager();
                PatientLookup patient = new PatientLookup();
                //Get FacilityId
                int facilityId = Convert.ToInt32(registration.MESSAGE_HEADER.SENDING_FACILITY);
                //Get Gender
                string gender = registration.PATIENT_IDENTIFICATION.SEX == "F" ? "Female" : "Male";
                //IQCare Sex
                int sex = lookupLogic.GetItemIdByGroupAndItemName("Gender", gender)[0].ItemId;
                //Assume this is a new patient
                int patientType = lookupLogic.GetItemIdByGroupAndItemName("PatientType", registration.PATIENT_VISIT.PATIENT_TYPE)[0].ItemId;
                //Get Enrollment Id Type
                int visitType = lookupLogic.GetItemIdByGroupAndItemName("VisitType", "Enrollment")[0].ItemId;
                //Get DOB
                DateTime DOB = DateTime.ParseExact(registration.PATIENT_IDENTIFICATION.DATE_OF_BIRTH, "yyyyMMdd", null);
                bool DOB_Precision = true;
                switch (registration.PATIENT_IDENTIFICATION.DATE_OF_BIRTH_PRECISION)
                {
                    case "ESTIMATED":
                        DOB_Precision = false;
                        break;
                    case "EXACT":
                        DOB_Precision = true;
                        break;
                    default:
                        DOB_Precision = true;
                        break;
                }
                //Get Enrollment Date
                DateTime dateOfEnrollment = DateTime.ParseExact(registration.PATIENT_VISIT.HIV_CARE_ENROLLMENT_DATE, "yyyyMMdd", null);
                //Get Patient Names
                string firstName = registration.PATIENT_IDENTIFICATION.PATIENT_NAME.FIRST_NAME;
                string middleName = registration.PATIENT_IDENTIFICATION.PATIENT_NAME.MIDDLE_NAME;
                string lastName = registration.PATIENT_IDENTIFICATION.PATIENT_NAME.LAST_NAME;
                //Get gods number
                string godsNumber = registration.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.ID;
                //get marital status
                string maritalStatusString = String.Empty;
                int maritalStatusId = 0;
                switch (registration.PATIENT_IDENTIFICATION.MARITAL_STATUS)
                {
                    case "S":
                        maritalStatusString = "Single";
                        break;
                    case "W":
                        maritalStatusString = "Widowed";
                        break;
                    case "D":
                        maritalStatusString = "Divorced";
                        break;
                    case "MP":
                        maritalStatusString = "Married Polygamous";
                        break;
                    case "C":
                        maritalStatusString = "Cohabiting";
                        break;
                    case "MM":
                        maritalStatusString = "Married Monogamous";
                        break;
                    default:
                        maritalStatusString = "Unknown";
                        break;
                }

                maritalStatusId = lookupLogic.GetItemIdByGroupAndItemName("MaritalStatus", maritalStatusString)[0].ItemId;
                //Get patient Address
                string village = String.Empty;
                string ward = String.Empty;
                string sub_county = String.Empty;
                string county = String.Empty;
                string nearestLandMark =String.Empty;
                string postalAdress = String.Empty;
                string phoneNumber = String.Empty;
                string deathDate = String.Empty;
                var patient_address = registration.PATIENT_IDENTIFICATION.PATIENT_ADDRESS;
                if(patient_address != null)
                {
                    var physicalAddress = registration.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS;
                    if (physicalAddress != null)
                    {
                        village = registration.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.VILLAGE;
                        ward = registration.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.WARD;
                        sub_county = registration.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.SUB_COUNTY;
                        county = registration.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.COUNTY;
                        nearestLandMark = registration.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.NEAREST_LANDMARK;
                    }
                    postalAdress = registration.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.POSTAL_ADDRESS;
                }
                //Lookup Ward, sub_county_county
                int wardId = 0;
                int subCountyId = 0;
                int countyId = 0;
                var countyDetails = lookupLogic.GetCountyDetailsByWardName(ward);
                if (countyDetails != null)
                {
                    wardId = countyDetails.WardId;
                    subCountyId = countyDetails.SubcountyId;
                    countyId = countyDetails.CountyId;
                }
                //Get Phone Number
                phoneNumber = registration.PATIENT_IDENTIFICATION.PHONE_NUMBER;
                deathDate = registration.PATIENT_IDENTIFICATION.DEATH_DATE;
                DateTime? DateOfDeath = DateTime.ParseExact(deathDate, "yyyyMMdd", null);
                //GET NEXT OF KIN
                var nextOfKin = registration.NEXT_OF_KIN;
                //Get CCCNumber and NationalId
                string nationalId = String.Empty;
                string cccNumber = String.Empty;
                int entryPointId = 0;
                var lookupEntryPoints = lookupLogic.GetItemIdByGroupAndItemName("Entrypoint", registration.PATIENT_VISIT.PATIENT_SOURCE);
                if (lookupEntryPoints.Count > 0)
                {
                    entryPointId = lookupEntryPoints[0].ItemId;
                }
                else
                {
                    entryPointId = lookupLogic.GetItemIdByGroupAndDisplayName("Entrypoint", "Other")[0].ItemId;
                }


                foreach (var internalpatientid in registration.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID)
                {
                    if (internalpatientid.IDENTIFIER_TYPE == "NATIONAL_ID" && internalpatientid.ASSIGNING_AUTHORITY == "GOK")
                    {
                        nationalId = internalpatientid.ID;
                    }

                    if (internalpatientid.IDENTIFIER_TYPE == "CCC_NUMBER" && internalpatientid.ASSIGNING_AUTHORITY == "CCC")
                    {
                        cccNumber = internalpatientid.ID;
                    }
                }

                if (!String.IsNullOrWhiteSpace(cccNumber))
                {
                    patient = patientLookup.GetPatientByCccNumber(cccNumber);
                    if (patient == null)
                    {
                        msg = ProcessPatient.Add(firstName, middleName, lastName, sex, 1, DOB, DOB_Precision, facilityId, 
                            patientType, nationalId, visitType, dateOfEnrollment, cccNumber, entryPointId, godsNumber, 
                            maritalStatusId, village, wardId, subCountyId, countyId, nearestLandMark, postalAdress, phoneNumber,
                            DateOfDeath, nextOfKin);
                    }
                    else
                    {
                        msg = ProcessPatient.Update(patient.PersonId, patient.Id, patient.ptn_pk, DOB, nationalId, facilityId, 
                            entryPointId, dateOfEnrollment, cccNumber, patient, godsNumber, maritalStatusId, village, 
                            wardId, subCountyId, countyId, nearestLandMark, postalAdress, phoneNumber, DateOfDeath,
                            nextOfKin);
                    }

                }

                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return msg;
        }

        public string Update(PatientRegistrationDTO registration)
        {
            ExMessage message = new ExMessage();
            try
            {
                PatientLookupManager patientLookup = new PatientLookupManager();
                PatientEntryPointManager patientEntryPointManager = new PatientEntryPointManager();
                PatientRegistrationValidation registrationValidation = new PatientRegistrationValidation();
                LookupLogic lookupLogic = new LookupLogic();

                string cccNumber = String.Empty;
                string nationalId = String.Empty;
                PatientLookup patient = new PatientLookup();

                List<ValidationResult> results = registrationValidation.ValidateDTO(registration);
                if (results.Count > 0)
                {
                    throw new Exception(results.ToString());
                }

                foreach (var item in registration.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID)
                {
                    if (item.IDENTIFIER_TYPE == "CCC_NUMBER" && item.ASSIGNING_AUTHORITY == "CCC")
                    {
                        cccNumber = item.ID;
                    }

                    if (item.IDENTIFIER_TYPE == "NATIONAL_ID" && item.ASSIGNING_AUTHORITY == "GOK")
                    {
                        nationalId = item.ID;
                    }
                }

                if (!String.IsNullOrWhiteSpace(cccNumber))
                {
                    patient = patientLookup.GetPatientByCccNumber(cccNumber);

                    int entryPointId = 0;
                    //Get Gender
                    string gender = registration.PATIENT_IDENTIFICATION.SEX == "F" ? "Female" : "Male";
                    //IQCare Sex
                    int sex = lookupLogic.GetItemIdByGroupAndItemName("Gender", gender)[0].ItemId;
                    //Assume this is a new patient
                    int patientType = lookupLogic.GetItemIdByGroupAndItemName("PatientType", "New")[0].ItemId;
                    //Get Enrollment Id Type
                    int visitType = lookupLogic.GetItemIdByGroupAndItemName("VisitType", "Enrollment")[0].ItemId;
                    //Get DOB
                    DateTime DOB;
                    bool DOB_Precision;
                    switch (registration.PATIENT_IDENTIFICATION.DATE_OF_BIRTH_PRECISION)
                    {
                        case "ESTIMATED":
                            DOB_Precision = false;
                            break;
                        case "EXACT":
                            DOB_Precision = true;
                            break;
                        default:
                            DOB_Precision = true;
                            break;
                    }
                    //Get Patient Names
                    string firstName = registration.PATIENT_IDENTIFICATION.PATIENT_NAME.FIRST_NAME;
                    string middleName = registration.PATIENT_IDENTIFICATION.PATIENT_NAME.MIDDLE_NAME;
                    string lastName = registration.PATIENT_IDENTIFICATION.PATIENT_NAME.LAST_NAME;
                    string godsNumber = registration.PATIENT_IDENTIFICATION.EXTERNAL_PATIENT_ID.ID;
                    //Marital Status
                    string maritalStatusString = String.Empty;
                    int maritalStatusId = 0;
                    switch (registration.PATIENT_IDENTIFICATION.MARITAL_STATUS)
                    {
                        case "S":
                            maritalStatusString = "Single";
                            break;
                        case "W":
                            maritalStatusString = "Widowed";
                            break;
                        case "D":
                            maritalStatusString = "Divorced";
                            break;
                        case "MP":
                            maritalStatusString = "Married Polygamous";
                            break;
                        case "C":
                            maritalStatusString = "Cohabiting";
                            break;
                        case "MM":
                            maritalStatusString = "Married Monogamous";
                            break;
                        default:
                            maritalStatusString = "Unknown";
                            break;
                    }

                    maritalStatusId = lookupLogic.GetItemIdByGroupAndItemName("MaritalStatus", maritalStatusString)[0].ItemId;
                    //Get patient Address
                    string village = String.Empty;
                    string ward = String.Empty;
                    string sub_county = String.Empty;
                    string county = String.Empty;
                    string nearestLandMark = String.Empty;
                    string postalAdress = String.Empty;
                    string phoneNumber = String.Empty;
                    string deathDate = String.Empty;
                    var patient_address = registration.PATIENT_IDENTIFICATION.PATIENT_ADDRESS;
                    if (patient_address != null)
                    {
                        var physicalAddress = registration.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS;
                        if (physicalAddress != null)
                        {
                            village = registration.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.VILLAGE;
                            ward = registration.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.WARD;
                            sub_county = registration.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.SUB_COUNTY;
                            county = registration.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.COUNTY;
                            nearestLandMark = registration.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.NEAREST_LANDMARK;
                        }
                        postalAdress = registration.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.POSTAL_ADDRESS;
                    }
                    //Lookup Ward, sub_county_county
                    int wardId = 0;
                    int subCountyId = 0;
                    int countyId = 0;
                    var countyDetails = lookupLogic.GetCountyDetailsByWardName(ward);
                    if (countyDetails != null)
                    {
                        wardId = countyDetails.WardId;
                        subCountyId = countyDetails.SubcountyId;
                        countyId = countyDetails.CountyId;
                    }
                    //Get Phone Number
                    phoneNumber = registration.PATIENT_IDENTIFICATION.PHONE_NUMBER;
                    deathDate = registration.PATIENT_IDENTIFICATION.DEATH_DATE;
                    DateTime? DateOfDeath = DateTime.ParseExact(deathDate, "yyyyMMdd", null);
                    //GET NEXT OF KIN
                    var nextOfKin = registration.NEXT_OF_KIN;
                    var lookupEntryPoints =
                        lookupLogic.GetItemIdByGroupAndDisplayName("Entrypoint",
                            registration.PATIENT_VISIT.PATIENT_SOURCE);
                    if (lookupEntryPoints.Count > 0)
                    {
                        entryPointId = lookupEntryPoints[0].ItemId;
                    }
                    else
                    {
                        entryPointId = lookupLogic.GetItemIdByGroupAndDisplayName("Entrypoint", "Other")[0].ItemId;
                    }

                    DOB = DateTime.ParseExact(registration.PATIENT_IDENTIFICATION.DATE_OF_BIRTH, "yyyyMMdd", null);
                    DateTime enrollmentDate = DateTime.ParseExact(registration.PATIENT_VISIT.HIV_CARE_ENROLLMENT_DATE,
                        "yyyyMMdd", null);
                    int facilityId = Convert.ToInt32(registration.MESSAGE_HEADER.SENDING_FACILITY);

                    if (patient != null)
                    {
                        msg = ProcessPatient.Update(patient.PersonId, patient.Id, patient.ptn_pk, DOB, nationalId, facilityId,
                            entryPointId, enrollmentDate, cccNumber, patient, godsNumber, maritalStatusId, 
                            village, wardId, subCountyId, countyId, nearestLandMark, postalAdress, phoneNumber,
                            DateOfDeath, nextOfKin);
                    }
                    else
                    {
                        msg = ProcessPatient.Add(firstName, middleName, lastName, sex, 1, DOB, DOB_Precision,
                            patientType, facilityId, nationalId, visitType, enrollmentDate, cccNumber, entryPointId, 
                            godsNumber, maritalStatusId, village, wardId, subCountyId, countyId, nearestLandMark, postalAdress, phoneNumber,
                            DateOfDeath, nextOfKin);
                    }
                }
                else
                {
                    throw new Exception("Message without ccc number");
                }

                message.Msg = msg;
                message.Code = 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return new JavaScriptSerializer().Serialize(message);
        }
    }
}

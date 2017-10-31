using System;
using System.Collections.Generic;
using IQCare.DTO;

namespace IQCare.WebApi.Logic.MappingEntities
{
    public class MESSAGEHEADER
    {
        public string SENDING_APPLICATION { get; set; }
        public string SENDING_FACILITY { get; set; }
        public string RECEIVING_APPLICATION { get; set; }
        public string RECEIVING_FACILITY { get; set; }
        public string MESSAGE_DATETIME { get; set; }
        public string SECURITY { get; set; }
        public string MESSAGE_TYPE { get; set; }
        public string PROCESSING_ID { get; set; }
    }

    public class VISIT
    {
        public string VISIT_DATE { get; set; }
        public string PATIENT_TYPE { get; set; }
        public string PATIENT_SOURCE { get; set; }
        public string HIV_CARE_INITIATION_DATE { get; set; }

        public static VISIT GetVisit(Registration entity)
        {
            VISIT visit = new VISIT();
            visit.VISIT_DATE = entity.DateOfEnrollment;

            return visit;
        }
    }

    public class EXTERNALPATIENTID
    {
        public string ID { get; set; }
        public string IDENTIFIER_TYPE { get; set; }
        public string ASSIGNING_AUTHORITY { get; set; }

        public static EXTERNALPATIENTID GetExternalpatientid(Registration entity)
        {
            EXTERNALPATIENTID externalpatientid = new EXTERNALPATIENTID();
            externalpatientid.IDENTIFIER_TYPE = "GODS_NUMBER";
            externalpatientid.ASSIGNING_AUTHORITY = "MPI";
            externalpatientid.ID = entity.Patient.GODS_NUMBER;

            return externalpatientid;
        }
    }

    public class INTERNALPATIENTID
    {
        public string ID { get; set; }
        public string IDENTIFIER_TYPE { get; set; }
        public string ASSIGNING_AUTHORITY { get; set; }

        public static List<INTERNALPATIENTID> GetInternalPatientIds(Registration entity)
        {
            List<INTERNALPATIENTID> internalpatientids = new List<INTERNALPATIENTID>();
            foreach (var id in entity.InternalPatientIdentifiers)
            {
                var identifier = new INTERNALPATIENTID()
                {
                    ID = id.IdentifierValue,
                    IDENTIFIER_TYPE = id.IdentifierType,
                    ASSIGNING_AUTHORITY = id.AssigningAuthority
                };
                internalpatientids.Add(identifier);
            }

            return internalpatientids;
        }
    }

    public class PATIENTNAME
    {
        public string FIRST_NAME { get; set; }
        public string MIDDLE_NAME { get; set; }
        public string LAST_NAME { get; set; }

        public static PATIENTNAME GetPatientName(Registration entity)
        {
            PATIENTNAME patientName = new PATIENTNAME();
            patientName.FIRST_NAME = !string.IsNullOrWhiteSpace(entity.Patient.FirstName) ? entity.Patient.FirstName : "";
            patientName.MIDDLE_NAME = !string.IsNullOrWhiteSpace(entity.Patient.MiddleName) ? entity.Patient.MiddleName : "";
            patientName.LAST_NAME = !string.IsNullOrWhiteSpace(entity.Patient.LastName) ? entity.Patient.LastName : "";

            return patientName;
        }
    }

    public class PHYSICALADDRESS
    {
        public string VILLAGE { get; set; }
        public string WARD { get; set; }
        public string SUB_COUNTY { get; set; }
        public string COUNTY { get; set; }
        public string NEAREST_LANDMARK { get; set; }
        public string GPS_LOCATION { get; set; }

        public static PHYSICALADDRESS GetPhysicalAddress(Registration entity)
        {
            PHYSICALADDRESS physicaladdress = new PHYSICALADDRESS();
            physicaladdress.COUNTY = !string.IsNullOrWhiteSpace(entity.County) ? entity.County : "";
            physicaladdress.SUB_COUNTY = !string.IsNullOrWhiteSpace(entity.SubCounty) ? entity.SubCounty : "";
            physicaladdress.WARD = !string.IsNullOrWhiteSpace(entity.Ward) ? entity.Ward : "";
            physicaladdress.VILLAGE = !string.IsNullOrWhiteSpace(entity.Village) ? entity.Village : "";

            return physicaladdress;
        }
    }

    public class PATIENTADDRESS
    {
        public PHYSICALADDRESS PHYSICAL_ADDRESS { get; set; }
        public string POSTAL_ADDRESS { get; set; }

        public static PATIENTADDRESS PatientAddress(Registration entity)
        {
            PATIENTADDRESS patientaddress = new PATIENTADDRESS();
            patientaddress.POSTAL_ADDRESS = !string.IsNullOrWhiteSpace(entity.Patient.PhysicalAddress) ? entity.Patient.PhysicalAddress : "";
            patientaddress.PHYSICAL_ADDRESS = PHYSICALADDRESS.GetPhysicalAddress(entity);
            return patientaddress;
        }
    }

    public class PATIENTIDENTIFICATION
    {
        public EXTERNALPATIENTID EXTERNAL_PATIENT_ID { get; set; }
        public List<INTERNALPATIENTID> INTERNAL_PATIENT_ID { get; set; }
        public PATIENTNAME PATIENT_NAME { get; set; }
        public PATIENTNAME MOTHER_NAME { get; set; }
        public string DATE_OF_BIRTH { get; set; }
        public string DATE_OF_BIRTH_PRECISION { get; set; }
        public string SEX { get; set; }
        public PATIENTADDRESS PATIENT_ADDRESS { get; set; }
        public string PHONE_NUMBER { get; set; }
        public string MARITAL_STATUS { get; set; }
        public string DEATH_DATE { get; set; }
        public string DEATH_INDICATOR { get; set; }

        public static PATIENTIDENTIFICATION GetPatientidentification(Registration entity)
        {
            PATIENTIDENTIFICATION patientidentification = new PATIENTIDENTIFICATION();
            patientidentification.EXTERNAL_PATIENT_ID = EXTERNALPATIENTID.GetExternalpatientid(entity);
            patientidentification.INTERNAL_PATIENT_ID = INTERNALPATIENTID.GetInternalPatientIds(entity);
            patientidentification.PATIENT_NAME = PATIENTNAME.GetPatientName(entity);
            //patientidentification.MOTHER_MAIDEN_NAME = !string.IsNullOrWhiteSpace(entity.MotherMaidenName) ? entity.MotherMaidenName : "";
            patientidentification.DATE_OF_BIRTH = !string.IsNullOrWhiteSpace(entity.Patient.DateOfBirth) ? entity.Patient.DateOfBirth : "";
            patientidentification.SEX = !string.IsNullOrWhiteSpace(entity.Patient.Sex) ? entity.Patient.Sex : "";
            patientidentification.PATIENT_ADDRESS = PATIENTADDRESS.PatientAddress(entity);
            patientidentification.PHONE_NUMBER = !string.IsNullOrWhiteSpace(entity.Patient.DateOfBirth) ? entity.Patient.MobileNumber : "";
            patientidentification.MARITAL_STATUS = !string.IsNullOrWhiteSpace(entity.MaritalStatus) ? entity.MaritalStatus : "";
            patientidentification.DEATH_DATE = !string.IsNullOrWhiteSpace(entity.DateOfDeath) ? entity.DateOfDeath : "";
            patientidentification.DEATH_INDICATOR = !string.IsNullOrWhiteSpace(entity.DeathIndicator) ? entity.DeathIndicator : "";

            return patientidentification;
        }
    }

    public class NOKNAME
    {
        public string FIRST_NAME { get; set; }
        public string MIDDLE_NAME { get; set; }
        public string LAST_NAME { get; set; }

        public static NOKNAME GetNokName(DTONextOfKin kin)
        {
            NOKNAME nokname = new NOKNAME();
            nokname.FIRST_NAME = !string.IsNullOrWhiteSpace(kin.FirstName) ? kin.FirstName : "";
            nokname.MIDDLE_NAME = !string.IsNullOrWhiteSpace(kin.MiddleName) ? kin.MiddleName : "";
            nokname.LAST_NAME = !string.IsNullOrWhiteSpace(kin.LastName) ? kin.LastName : "";

            return nokname;
        }
    }

    public class NEXTOFKIN
    {
        public NEXTOFKIN()
        {
            NOK_NAME = new NOKNAME();
        }

        public NOKNAME NOK_NAME { get; set; }
        public string RELATIONSHIP { get; set; }
        public string ADDRESS { get; set; }
        public string PHONE_NUMBER { get; set; }
        public string SEX { get; set; }
        public string DATE_OF_BIRTH { get; set; }
        public string CONTACT_ROLE { get; set; }


        public static List<NEXTOFKIN> GetNextOfKins(Registration entity)
        {
            List<NEXTOFKIN> nextofkins = new List<NEXTOFKIN>();
            if (entity.NextOfKin.Count > 0)
            {
                foreach (var kin in entity.NextOfKin)
                {
                    NEXTOFKIN nextofkin = new NEXTOFKIN();
                    nextofkin.NOK_NAME = NOKNAME.GetNokName(kin);
                    nextofkin.CONTACT_ROLE = kin.PatientType;
                    nextofkin.RELATIONSHIP = !string.IsNullOrWhiteSpace(kin.RelationshipType) ? kin.RelationshipType : "";
                    nextofkin.PHONE_NUMBER = !string.IsNullOrWhiteSpace(kin.MobileNumber) ? kin.MobileNumber : "";
                    nextofkin.SEX = !string.IsNullOrWhiteSpace(kin.Sex) ? kin.Sex : "";
                    nextofkin.DATE_OF_BIRTH = !String.IsNullOrWhiteSpace(kin.DateOfBirth) ? kin.DateOfBirth : "";
                    nextofkin.ADDRESS = !string.IsNullOrWhiteSpace(kin.PhysicalAddress) ? kin.PhysicalAddress : "";

                    nextofkins.Add(nextofkin);
                }
            }
            return nextofkins;
        }
    }
}
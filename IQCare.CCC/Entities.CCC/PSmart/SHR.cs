using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Linq;


namespace Entities.CCC.PSmart
{
    [Serializable]
    public class SHR
    {
        public SHR()
        {
            PATIENT_IDENTIFICATION = new PATIENTIDENTIFICATION();
            NEXT_OF_KIN = new List<NEXTOFKIN>();
            HIV_TEST = new List<HIVTEST>();
            IMMUNIZATION = new List<IMMUNIZATION>();
            CARD_DETAILS = new CARDDETAILS();            
        }
        [Key]
        public int Id { get; set; }
        public PATIENTIDENTIFICATION PATIENT_IDENTIFICATION { get; set; }
        public List<NEXTOFKIN> NEXT_OF_KIN { get; set; }
        public List<HIVTEST> HIV_TEST { get; set; }
        public List<IMMUNIZATION> IMMUNIZATION { get; set; }
        public CARDDETAILS CARD_DETAILS { get; set; }
    }

    [Serializable][Table("psmart_ExternalPatientId")]
    public class EXTERNALPATIENTID
    {
        [Key]
        public int PersonId { get; set; }
        public string CardSerialNumber { get; set; }
        public string ID { get; set; }
        public string IDENTIFIER_TYPE { get; set; }
        public string ASSIGNING_AUTHORITY { get; set; }
        public string ASSIGNING_FACILITY { get; set; }
    }

    [Serializable][Table("psmart_InternalPatientId")]
    public class INTERNALPATIENTID
    {
        [Key]
        public int PatientId { get; set; }
        public int personId { get; set; }
        public string CardSerialNumber { get; set; }
        public string ID { get; set; }
        public string IDENTIFIER_TYPE { get; set; }
        public string ASSIGNING_AUTHORITY { get; set; }
        public string ASSIGNING_FACILITY { get; set; }
    }

    [Serializable][Table("psmart_PatientName")]
    public class PATIENTNAME
    {
        [Key]
        public int PersonId { get; set; }
        public int PatientId { get; set; }
        public string CardSerialNumber { get; set; }
        public string FIRST_NAME { get; set; }
        public string MIDDLE_NAME { get; set; }
        public string LAST_NAME { get; set; }
    }

    [Serializable][Table("psmart_PhysicalAddress")]
    public class PHYSICALADDRESS
    {
        [Key]
        public int PersonId { get; set; }
        public int PatientId { get; set; }
        public string CardSerialNumber { get; set; }
        public string VILLAGE { get; set; }
        public string WARD { get; set; }
        public string SUB_COUNTY { get; set; }
        public string COUNTY { get; set; }
        public string NEAREST_LANDMARK { get; set; }
    }

    [Serializable][Table("psmart_PatientAddress")]
    public class PATIENTADDRESS
    {
        [Key]
        public int PersonId { get; set; }
        public string CardSerialNumber { get; set; }
        public PHYSICALADDRESS PHYSICAL_ADDRESS { get; set; }
        public string POSTAL_ADDRESS { get; set; }
    }

    [Serializable][Table("psmart_MotherName")]
    public class MOTHERNAME
    {
        [Key]
        public int PersonId { get; set; }
        public string CardSerialNumber { get; set; }
        public string FIRST_NAME { get; set; }
        public string MIDDLE_NAME { get; set; }
        public string LAST_NAME { get; set; }
    }

    [Serializable][Table("psmart_MotherIdentifier")]
    public class MOTHERIDENTIFIER
    {
        [Key]
        public int PersonId { get; set; }
        public string CardSerialNumber { get; set; }
        public string ID { get; set; }
        public string IDENTIFIER_TYPE { get; set; }
        public string ASSIGNING_AUTHORITY { get; set; }
        public string ASSIGNING_FACILITY { get; set; }
    }

    [Serializable][Table("psmart_MotherDetails")]
    public class MOTHERDETAILS
    {
        [Key]
        public int PersonId { get; set; }

        public string CardSerialNumber { get; set; }

        [NotMapped]
        public MOTHERNAME MOTHER_NAME { get; set; }
        [NotMapped]
        public List<MOTHERIDENTIFIER> MOTHER_IDENTIFIER { get; set; }
    }

    [Serializable][Table("psmart_PatientIdentification")]
    public class PATIENTIDENTIFICATION
    {
        [Key]
        public int PersonId { get; set; }
        public int PatientId { get; set; }
        public string CardSerialNumber { get; set; }
        [NotMapped]
        public EXTERNALPATIENTID EXTERNAL_PATIENT_ID { get; set; }

        [NotMapped]
        public List<INTERNALPATIENTID> INTERNAL_PATIENT_ID { get; set; }

        [NotMapped]
        public PATIENTNAME PATIENT_NAME { get; set; }
      
        public string DATE_OF_BIRTH { get; set; }
      

        public string DATE_OF_BIRTH_PRECISION { get; set; }
        public string SEX { get; set; }
        public string DEATH_DATE { get; set; }
        public string DEATH_INDICATOR { get; set; }

        [NotMapped]
        public PATIENTADDRESS PATIENT_ADDRESS { get; set; }

        public string PHONE_NUMBER { get; set; }
        public string MARITAL_STATUS { get; set; }

        [NotMapped]
        public MOTHERDETAILS MOTHER_DETAILS { get; set; }
    }

    [Serializable][Table("psmart_NokName")]
    public class NOKNAME
    {
        [Key]
        public int PersonId { get; set; }
        public string CardSerialNumber { get; set; }
        public string FIRST_NAME { get; set; }
        public string MIDDLE_NAME { get; set; }
        public string LAST_NAME { get; set; }
    }

    [Serializable][Table("psmart_NextOfKin")]
    public class NEXTOFKIN
    {
        [Key]
        public int PersonId { get; set; }
        public string CardSerialNumber { get; set; }
        [NotMapped]
        public NOKNAME NOK_NAME { get; set; }

        public string RELATIONSHIP { get; set; }
        public string ADDRESS { get; set; }
        public string PHONE_NUMBER { get; set; }
        public string SEX { get; set; }
        public string DATE_OF_BIRTH { get; set; }
        public string CONTACT_ROLE { get; set; }
    }

    [Serializable][Table("psmart_ProviderDetails")]
    public class PROVIDERDETAILS
    {
        [Key]
        public int PersonId { get; set; }
        public int PatientId { get; set; }
        public string CardSerialNumber { get; set; }
        public string NAME { get; set; }
        public string ID { get; set; }
    }

    [Serializable][Table("psmartHIVTest")]
    public class HIVTEST
    {
        [Key]
        public int PersonId { get; set; }
        public int PatientId { get; set; }
        public string CardSerialNumber { get; set; }
        public string DATE { get; set; }
        public string RESULT { get; set; }
        public string TYPE { get; set; }
        public string FACILITY { get; set; }
        public string STRATEGY { get; set; }

        [NotMapped]
        public PROVIDERDETAILS PROVIDER_DETAILS { get; set; }
    }

    [Serializable][Table("psmart_Immunization")]
    public class IMMUNIZATION
    {
        [Key]
        public int PersonId { get; set; }
        public int PatientId { get; set; }
        public string CardSerialNumber { get; set; }
        public string NAME { get; set; }
        public string DATE_ADMINISTERED { get; set; }
    }

    [Serializable] [Table("psmart_CardDetails")]
    public class CARDDETAILS
    {
        [Key]
        public int PersonId { get; set; }
        public int PatientId { get; set; }
        public string CardSerialNumber { get; set; }
        public string STATUS { get; set; }
        public string REASON { get; set; }
        public string LAST_UPDATED { get; set; }
        public string LAST_UPDATED_FACILITY { get; set; }
    }
}
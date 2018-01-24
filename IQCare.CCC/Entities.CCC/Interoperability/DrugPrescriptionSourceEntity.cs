using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace Entities.CCC.Interoperability
{
    [Serializable]
    [System.ComponentModel.DataAnnotations.Schema.Table("API_DrugPrescription")]

    public class DrugPrescriptionSourceEntity
    {
        //public DrugPrescriptionEntity()
        //{
        //    MESSAGE_HEADER=new MESSAGE_HEADER();
        //    PATIENT_IDENTIFICATION=new PATIENT_IDENTIFICATION();
        //    COMMON_ORDER_DETAILS=new COMMON_ORDER_DETAILS();
        //    PHARMACY_ENCODED_ORDER=new List<PHARMACY_ENCODED_ORDER>();
        //}
        
        [Key]
        public int ptnpk { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int ptn_pharmacy_pk { get; set; }
        public string Id { get; set; }
        public string Id2 { get; set; }
        public string SENDING_FACILITY { get; set; }
        public string EXT_ID { get; set; }
        public string EXT_IDENTIFIER_TYPE { get; set; }
        public string EXT_ASSIGNING_AUTHOURITY { get; set; }
        public string IDENTIFIER_TYPE { get; set; }
        public string ASSIGNING_AUTHORITY { get; set; }
        // public string ID2 { get; set; }
        public string IDENTIFIER_TYPE2 { get; set; }
        public string ASSIGNING_AUTHORITY2 { get; set; }
        public string FIRST_NAME { get; set; }
        public string MIDDLE_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public DateTime TRANSACTION_DATETIME { get; set; }
        public string ORDER_CONTROL { get; set; }
        public int NUMBER { get; set; }
        public string ENTITY { get; set; }
        public string ORDER_STATUS { get; set; }
        public string ORDERING_PHYSICIAN_FIRST_NAME { get; set; }
        public string ORDERING_PHYSICIAN_LAST_NAME { get; set; }
        public string DRUG_NAME { get; set; }
        public string CODING_SYSTEM { get; set; }
        public string STRENGTH { get; set; }
        public Decimal DOSAGE { get; set; }
        public string FREQUENCY { get; set; }
        public Decimal DURATION { get; set; }
        public Decimal QUANTITY_PRESCRIBED { get; set; }
        public string PRESCRIPTION_NOTES { get; set; }
        public string NOTES { get; set; }
        public string INDICATION { get; set; }
        public DateTime PHARMACY_ORDER_DATE { get; set; }
        public string TREATMENT_INSTRUCTION { get; set; }

        //public MESSAGE_HEADER MESSAGE_HEADER { get; set; }
        //public PATIENT_IDENTIFICATION PATIENT_IDENTIFICATION { get; set; }
        //public COMMON_ORDER_DETAILS COMMON_ORDER_DETAILS { get; set; }
        //public List<PHARMACY_ENCODED_ORDER> PHARMACY_ENCODED_ORDER { get; set; }


        //public class PATIENT_IDENTIFICATION
        //{
        //    public PATIENT_IDENTIFICATION()
        //    {
        //        EXTERNAL_PATIENT_ID=new INTERNAL_PATIENT_ID();
        //        INTERNAL_PATIENT_ID=new List<INTERNAL_PATIENT_ID>();
        //        PATIENT_NAME=new PATIENT_NAME();
        //    }
        //    [Key]
        //    public int ptnpk { get; set; }
        //    public INTERNAL_PATIENT_ID EXTERNAL_PATIENT_ID { get; set; }
        //    public List<INTERNAL_PATIENT_ID> INTERNAL_PATIENT_ID { get; set; }
        //    public PATIENT_NAME PATIENT_NAME { get; set; }
        //}

        //public class COMMON_ORDER_DETAILS
        //{
        //    public COMMON_ORDER_DETAILS()
        //    {
        //        PLACER_ORDER_NUMBER=new PLACER_ORDER_NUMBER();
        //        ORDERING_PYSICIAN=new ORDERING_PHYSICIAN();
        //    }
        //    [Key]
        //    public int ptnpk { get; set; }
        //    public string ORDER_CONTROL { get; set; }
        //    public PLACER_ORDER_NUMBER PLACER_ORDER_NUMBER { get; set; }
        //    public string ORDER_STATUS { get; set; }
        //    public ORDERING_PHYSICIAN ORDERING_PYSICIAN { get; set; }
        //    public DateTime TRANSACTION_DATETIME { get; set; }
        //    public int NOTES { get; set; }
        //}

        //public class PHARMACY_ENCODED_ORDER
        //{
        //    public PHARMACY_ENCODED_ORDER()
        //    {

        //    }
        //    [Key]
        //    public int ptnpk { get; set; }
        //    public string DRUG_NAME { get; set; }
        //    public string CODING_SYSTEM { get; set; }
        //    public string STRENGTH { get; set; }
        //    public string DOSAGE { get; set; }
        //    public string FREQUENCY { get; set; }
        //    public int DURATION { get; set; }
        //    public string QUANTITY_PRESCRIBED { get; set; }
        //    public string TREATMENT_INSTRUCTION { get; set; }
        //    public string INDICATION { get; set; }
        //    public DateTime PHARMACY_ORDER_DATE { get; set; }
        //}

        //public class PLACER_ORDER_NUMBER
        //{
        //    [Key]
        //    public int ptnpk { get; set; }
        //    public string NUMBER { get; set; }
        //    public string ENTITY { get; set; }
        //}

        //public class ORDERING_PHYSICIAN
        //{
        //    [Key]
        //    public int ptnpk { get; set; }
        //    public string FIRST_NAME { get; set; }
        //    public string MIDDLE_NAME { get; set; }
        //    public string LAST_NAME { get; set; }
        //}

        //public class INTERNAL_PATIENT_ID
        //{
        //    public INTERNAL_PATIENT_ID()
        //    {

        //    }
        //    [Key]
        //    public int ptnpk { get; set; }
        //    public string ID { get; set; }
        //    public string IDENTIFIER_TYPE { get; set; }
        //    public string ASSIGNING_AUTHORITY { get; set; }
        //}

        //public class PATIENT_NAME
        //{
        //    [Key]
        //    public int ptnpk { get; set; }
        //    public string FIRST_NAME { get; set; }
        //    public string MIDDLE_NAME { get; set; }
        //    public string LAST_NAME { get; set; }
        //}

        //public class MESSAGE_HEADER
        //{
        //    [Key]
        //    public int ptnpk { get; set; }
        //    public string SENDING_APPLICATION { get; set; }
        //    public string SENDING_FACILITY { get; set; }
        //    public string RECEIVING_APPLICATION { get; set; }
        //    public string RECEIVING_FACILITY { get; set; }
        //    public DateTime MESSAGE_DATETIME { get; set; }
        //    public string SECURITY { get; set; }
        //    public string MESSAGE_TYPE { get; set; }
        //    public string PROCESSING_ID { get; set; }
    }

}

//}

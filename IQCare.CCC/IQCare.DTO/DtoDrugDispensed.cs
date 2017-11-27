using System;
using System.Collections.Generic;

namespace IQCare.DTO
{

    public class DtoDrugDispensed
    {
        public DtoDrugDispensed()
        {
            MESSAGE_HEADER = new MessageHeaderDispense();
            PATIENT_IDENTIFICATION = new DtoPatientIdentification();
            COMMON_ORDER_DETAILS = new CommonOrderDetailsDispenseDto();
            PHARMACY_DISPENSE = new List<PharmacyDispensedDrugs>();
        }

        public MessageHeaderDispense MESSAGE_HEADER { get; set; }
        public DtoPatientIdentification PATIENT_IDENTIFICATION { get; set; }
        public CommonOrderDetailsDispenseDto COMMON_ORDER_DETAILS { get; set; }
        public List<PharmacyEncodededOrderDispenseDto> PHARMACY_ENCODED_ORDER { get; set; }
        public List<PharmacyDispensedDrugs> PHARMACY_DISPENSE { get; set; }
    }

    public class PharmacyDispensedDrugs
    {
        public string DRUG_NAME { get; set; }
        public string CODING_SYSTEM { get; set; }
        public string ACTUAL_DRUGS { get; set; }
        public string STRENGTH { get; set; }
        public string DOSAGE { get; set; }
        public string FREQUENCY { get; set; }
        public int DURATION { get; set; }
        public int QUANTITY_DISPENSED { get; set; }
       public string DISPENSING_NOTES { get; set; }
    }

    public class PharmacyEncodededOrderDispenseDto
    {
        public string DRUG_NAME { get; set; }
        public string CODING_SYSTEM { get; set; }
        public string STRENGTH { get; set; }
        public string DOSAGE { get; set; }
        public string FREQUENCY { get; set; }
        public decimal DURATION { get; set; }
        public decimal QUANTITY_PRESCRIBED { get; set; } 
        public string PRESCRIPTION_NOTES { get; set; }
    }

    public class CommonOrderDetailsDispenseDto
    {
        public CommonOrderDetailsDispenseDto()
        {
            PLACER_ORDER_NUMBER=new PlacerOrderNumberDto();
            FILLER_ORDER_NUMBER=new PlacerOrderNumberDto();
            ORDERING_PHYSICIAN=new OrderingPysicianDto();
        }

        public string OrderControl { get; set; }
        public PlacerOrderNumberDto PLACER_ORDER_NUMBER { get; set; }
        public PlacerOrderNumberDto FILLER_ORDER_NUMBER { get; set; }
        public string ORDER_STATUS { get; set; }
        public OrderingPysicianDto ORDERING_PHYSICIAN { get; set; }
        public DateTime TRANSACTION_DATETIME { get; set; }
        public string NOTES { get; set; }
    }

    public class MessageHeaderDispense
    {
        public string SENDING_APPLICATION { get; set; }
        public string SENDING_FACILITY { get; set; }
        public string RECEIVING_APPLICATION { get; set; }
        public string RECEIVING_FACILITY { get; set; }
        public DateTime MESSAGE_DATETIME { get; set; }
        public string SECURITY { get; set; }
        public string MESSAGE_TYPE { get; set; }
        public string PROCESSING_ID { get; set; }
    }

}

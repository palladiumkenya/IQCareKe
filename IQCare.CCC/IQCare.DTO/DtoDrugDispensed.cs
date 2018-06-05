using IQCare.DTO.CommonEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IQCare.DTO
{

    public class DtoDrugDispensed
    {
        public DtoDrugDispensed()
        {
            MESSAGE_HEADER = new MESSAGE_HEADER();
            PATIENT_IDENTIFICATION = new APPOINTMENTPATIENTIDENTIFICATION();
            COMMON_ORDER_DETAILS = new CommonOrderDetailsDispenseDto();
            PHARMACY_DISPENSE = new List<PHARMACY_DISPENSE>();
        }

        [Required, ValidateObject]
        public MESSAGE_HEADER MESSAGE_HEADER { get; set; }
        public APPOINTMENTPATIENTIDENTIFICATION PATIENT_IDENTIFICATION { get; set; }
        public CommonOrderDetailsDispenseDto COMMON_ORDER_DETAILS { get; set; }
        [Required, ValidateObject]
        public List<PHARMACY_ENCODED_ORDER> PHARMACY_ENCODED_ORDER { get; set; }
        [Required, ValidateObject]
        public List<PHARMACY_DISPENSE> PHARMACY_DISPENSE { get; set; }
    }

    public class PHARMACY_DISPENSE
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

    //public class PharmacyEncodededOrderDispenseDto
    //{
    //    public string DRUG_NAME { get; set; }
    //    public string CODING_SYSTEM { get; set; }
    //    public string STRENGTH { get; set; }
    //    public string DOSAGE { get; set; }
    //    public string FREQUENCY { get; set; }
    //    public decimal DURATION { get; set; }
    //    public decimal QUANTITY_PRESCRIBED { get; set; } 
    //    public string PRESCRIPTION_NOTES { get; set; }
    //}

   
    public class CommonOrderDetailsDispenseDto
    {
        public CommonOrderDetailsDispenseDto()
        {
            PLACER_ORDER_NUMBER=new PlacerOrderNumberDto();
            FILLER_ORDER_NUMBER=new PlacerOrderNumberDto();
            ORDERING_PHYSICIAN=new OrderingPysicianDto();
        }

        public string ORDER_CONTROL { get; set; }
        public PlacerOrderNumberDto PLACER_ORDER_NUMBER { get; set; }
        public PlacerOrderNumberDto FILLER_ORDER_NUMBER { get; set; }
        public string ORDER_STATUS { get; set; }
        public OrderingPysicianDto ORDERING_PHYSICIAN { get; set; }
        [Required, ValidateObject]
        public DateTime TRANSACTION_DATETIME { get; set; }
        public string NOTES { get; set; }
    }

    //public class MessageHeaderDispense
    //{
    //    public string SENDING_APPLICATION { get; set; }
    //    public string SENDING_FACILITY { get; set; }
    //    public string RECEIVING_APPLICATION { get; set; }
    //    public string RECEIVING_FACILITY { get; set; }
    //    public DateTime MESSAGE_DATETIME { get; set; }
    //    public string SECURITY { get; set; }
    //    public string MESSAGE_TYPE { get; set; }
    //    public string PROCESSING_ID { get; set; }
    //}

}

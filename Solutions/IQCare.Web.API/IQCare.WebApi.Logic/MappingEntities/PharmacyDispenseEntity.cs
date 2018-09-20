using System.Collections.Generic;
using IQCare.WebApi.Logic.MappingEntities.drugs;
using System;

namespace IQCare.WebApi.Logic.MappingEntities
{
    public class PharmacyDispenseEntity
    {
        public PharmacyDispenseEntity()
        {
            MESSAGE_HEADER = new MESSAGEHEADER();
            PATIENT_IDENTIFICATION = new APPOINTMENTPATIENTIDENTIFICATION();
            COMMON_ORDER_DETAILS = new COMMONORDERDETAILS();
            PHARMACY_ENCODED_ORDER = new List<PHARMACYENCODEDORDER>();
            PHARMACY_DISPENSE = new List<PHARMACYDISPENSE>();
        }

        public MESSAGEHEADER MESSAGE_HEADER { get; set; }
        public APPOINTMENTPATIENTIDENTIFICATION PATIENT_IDENTIFICATION { get; set; }
        public COMMONORDERDETAILS COMMON_ORDER_DETAILS { get; set; }
        public List<PHARMACYENCODEDORDER> PHARMACY_ENCODED_ORDER { get; set; }
        public List<PHARMACYDISPENSE> PHARMACY_DISPENSE { get; set; }
    }

    public class PHARMACYDISPENSE
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

    public class PHARMACY_ENCODED_ORDER_DISPENSE
    {
        public string DRUG_NAME { get; set; }
        public string CODING_SYSTEM { get; set; }
        public string STRENGTH { get; set; }
        public string DOSAGE { get; set; }
        public string FREQUENCY { get; set; }
        public string DURATION { get; set; }
        public decimal QUANTITY_PRESCRIBED { get; set; }
        public string PRESCRIPTION_NOTES { get; set; }
    }

    public class CommonOrderDetailsDispenseEntity
    {
        public CommonOrderDetailsDispenseEntity()
        {
            PLACER_ORDER_NUMBER = new PLACERORDERNUMBER();
            FILLER_ORDER_NUMBER = new FILLERORDERNUMBER();
            ORDERING_PHYSICIAN = new ORDERINGPHYSICIAN();
        }

        public string ORDER_CONTROL { get; set; }
        public PLACERORDERNUMBER PLACER_ORDER_NUMBER { get; set; }
        public FILLERORDERNUMBER FILLER_ORDER_NUMBER { get; set; }
        public string ORDER_STATUS { get; set; }
        public ORDERINGPHYSICIAN ORDERING_PHYSICIAN { get; set; }
        public string TRANSACTION_DATETIME { get; set; }
        public string NOTES { get; set; }
    }
}

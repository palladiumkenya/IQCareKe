using System;
using System.Collections.Generic;

namespace IQCare.WebApi.Logic.MappingEntities
{

    public class PLACERORDERNUMBER
    {
        public string NUMBER { get; set; }
        public string ENTITY { get; set; }
    }

    public class ORDERINGPHYSICIAN
    {
        public string FIRST_NAME { get; set; }
        public string MIDDLE_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string PREFIX { get; set; }
    }

    public class COMMONORDERDETAILS
    {
        public string ORDER_CONTROL { get; set; }
        public PLACERORDERNUMBER PLACER_ORDER_NUMBER { get; set; }
        public string ORDER_STATUS { get; set; }
        public ORDERINGPHYSICIAN ORDERING_PHYSICIAN { get; set; }
        public string TRANSACTION_DATETIME { get; set; }
        public string NOTES { get; set; }
    }

    public class PHARMACYENCODEDORDER
    {
        public string DRUG_NAME { get; set; }
        public string CODING_SYSTEM { get; set; }
        public string STRENGTH { get; set; }
        public string DOSAGE { get; set; }
        public string FREQUENCY { get; set; }
        public string DURATION { get; set; }
        public string QUANTITY_PRESCRIBED { get; set; }
        public string PRESCRIPTION_NOTES { get; set; }
    }

    public class DrugPrescriptionEntity
    {
        public MESSAGEHEADER MESSAGE_HEADER { get; set; }
        public PATIENTIDENTIFICATION PATIENT_IDENTIFICATION { get; set; }
        public COMMONORDERDETAILS COMMON_ORDER_DETAILS { get; set; }
        public List<PHARMACYENCODEDORDER> PHARMACY_ENCODED_ORDER { get; set; }
    }
}

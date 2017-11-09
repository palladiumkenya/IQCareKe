using System.Collections.Generic;
using IQCare.WebApi.Logic.MappingEntities.drugs;

namespace IQCare.WebApi.Logic.MappingEntities
{
    public class DrugDispenseEntity
    {
        public MESSAGEHEADER MESSAGE_HEADER { get; set; }
        public PATIENTIDENTIFICATION PATIENT_IDENTIFICATION { get; set; }
        public COMMONORDERDETAILS COMMON_ORDER_DETAILS { get; set; }
        public List<PHARMACYENCODEDORDER> PHARMACY_ENCODED_ORDER { get; set; }
        public List<PHARMACYDISPENSE> PHARMACY_DISPENSE { get; set; }
    }

    public class PHARMACYDISPENSE
    {
       public string DRUG_NAME { get; set; }
       public string CODING_SYSTEM { get; set; }
      public string  ACTUAL_DRUGS { get; set; }
       public string STRENGTH { get; set; }
       public string DOSAGE { get; set; }
       public string FREQUENCY { get; set; } 
       public int  DURATION { get; set; }
       public int QUANTITY_DISPENSED { get; set; }
       public string DISPENSING_NOTES { get; set; }
		
    }
}

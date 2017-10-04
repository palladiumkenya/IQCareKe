using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.CCC.Interoperability
{
    [Serializable]
    [System.ComponentModel.DataAnnotations.Schema.Table("API_DrugPrescription")]

    public class DrugPrescriptionEntity
    {
        
        [Key]
        public int ptnpk { get; set; }
        public string Id { get; set; }
        public string SENDING_FACILITY { get; set; }
       public string IDENTIFIER_TYPE { get; set; }
       public string ASSIGNING_AUTHORITY { get; set; }
       public string ID2 { get; set; }
       public string IDENTIFIER_TYPE2 { get; set; }
       public string ASSIGNING_AUTHORITY2 { get; set; }
       public int ptn_pharmacy_pk { get; set; }
       public string FIRST_NAME { get; set; }
       public string MIDDLE_NAME { get; set; }
       public string LAST_NAME { get; set; }
       public DateTime TRANSACTION_DATETIME { get; set; }
       public string ORDER_CONTROL { get; set; }
       public int NUMBER { get; set; }
       public string ENTITY { get; set; }
       public string ORDER_STATUS { get; set; }
       public string ORDERING_PHYSICIAN { get; set; }
       public string NOTES { get; set; }
       //public List<PrescribedDrug>  PrescribedDrugs { get; set; }

   
    }
    public class PrescribedDrug
    {
        public string DRUG_NAME { get; set; }
        public string CODING_SYSTEM { get; set; }
        public string STRENGTH { get; set; }
        public string DOSAGE { get; set; }
        public string FREQUENCY { get; set; }
        public int DURATION { get; set; }
        public string QUANTITY_PRESCRIBED { get; set; }
        public string PRESCRIPTION_NOTES { get; set; }
    }
    public class PharmacyEncodedOrder
    {
        public string NOTES { get; set; }
        public string DRUG_NAME { get; set; }
        public string CODING_SYSTEM { get; set; }
        public string STRENGTH { get; set; }
        public string DOSAGE { get; set; }
        public string FREQUENCY { get; set; }
        public int DURATION { get; set; }
        public string QUANTITY_PRESCRIBED { get; set; }
        public string PRESCRIPTION_NOTES { get; set; }

    }
}
  
//}

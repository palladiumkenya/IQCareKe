using System.Collections.Generic;

namespace IQCare.DTO
{

    public class DtoDrugDispensed
    {
        public DtoDrugDispensed()
        {
            MessageHeader=new MessageHeader();
            PatientIdentification=new DtoPatientIdentification();
            CommonOrderDetails=new CommonOrderDetails();
            PharmacyEncodedOrder=new List<PharmacyEncodedOrder>();
            PharmacyDispense= new List<PharmacyDispensedDrugs>();
        }

        public DtoPatientIdentification PatientIdentification { get; set; }
        public MessageHeader MessageHeader { get; set; }
        public CommonOrderDetails CommonOrderDetails { get; set; }
        public List<PharmacyEncodedOrder>  PharmacyEncodedOrder { get; set; }
        public List<PharmacyDispensedDrugs> PharmacyDispense { get; set; }
    }

    public class PharmacyDispensedDrugs
    {
        public string  DrugName { get; set; }
        public string CodingSystem { get; set; }
        public string ActualDrugs { get; set; }
        public string Strength { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public int Duration { get; set; }
        public int QuantityDispensed { get; set; }
       public string DispensingNotes { get; set; }

    }
  
}

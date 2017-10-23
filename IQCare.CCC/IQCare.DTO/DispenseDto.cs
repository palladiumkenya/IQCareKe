using System.Collections.Generic;

namespace IQCare.DTO
{

    public class DispenseDto
    {
        public DispenseDto()
        {
            MessageHeader=new MessageHeader();
            CommonOrderDetails=new CommonOrderDetails();
            PharmacyEncodedOrder=new PharmacyEncodedOrder();
            PharmacyDispense= new List<PharmacyDispense>();
        }

    public DtoPatientIdentification PatientIdentification { get; set; }
        public MessageHeader MessageHeader { get; set; }
        public CommonOrderDetails CommonOrderDetails { get; set; }
        public PharmacyEncodedOrder PharmacyEncodedOrder { get; set; }
        public List<PharmacyDispense> PharmacyDispense { get; set; }
    }

    public class PharmacyDispense
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

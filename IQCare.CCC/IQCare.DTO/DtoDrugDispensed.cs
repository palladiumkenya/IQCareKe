using System;
using System.Collections.Generic;

namespace IQCare.DTO
{

    public class DtoDrugDispensed
    {
        public DtoDrugDispensed()
        {
            MESSAGE_HEADER = new MessageHeader();
            PATIENT_IDENTIFICATION = new DtoPatientIdentification();
            COMMON_ORDER_DETAILS = new CommonOrderDetailsDispenseDto();
            PHARMACY_DISPENSE = new List<PharmacyDispensedDrugs>();
        }

        public MessageHeader MESSAGE_HEADER { get; set; }
        public DtoPatientIdentification PATIENT_IDENTIFICATION { get; set; }
        public CommonOrderDetailsDispenseDto COMMON_ORDER_DETAILS { get; set; }
        public List<PharmacyEncodededOrderDispenseDto> PHARMACY_ENCODED_ORDER { get; set; }
        public List<PharmacyDispensedDrugs> PHARMACY_DISPENSE { get; set; }
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

    public class PharmacyEncodededOrderDispenseDto
    {
        public string DrugName { get; set; }
        public string CodingSystem { get; set; }
        public string Strength { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public decimal Duration { get; set; }
        public decimal QuantityPrescribed { get; set; } 
        public string PrescriptionNotes { get; set; }
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
        public string OrderStatus { get; set; }
        public OrderingPysicianDto ORDERING_PHYSICIAN { get; set; }
        public DateTime TransactionDatetime { get; set; }
        public string PrescriptionNotes { get; set; }
    }
  
}

using System;
using System.Collections.Generic;

namespace IQCare.DTO
{
    public class PrescriptionDto
    {
        public DTOIdentifier InternalPatientIdentifier { get; set; }
        public PatientNameDto Patientname { get; set; }
        public CommonOrderDetails CommonOrderDetails { get; set; }
        public List<PharmacyEncodedOrder> PharmacyEncodedOrder  { get; set; }
    }

   public class CommonOrderDetails
    {
       public string OrderControl { get; set; }
       public PlacerOrderNumber PlacerOrderNumber { get; set; }
       public string OrderStatus { get; set; }
       public OrderingPhysician OrderingPhysician { get; set; }
       public DateTime TransactionDatetime { get; set; }
       public string Notes { get; set; }
    }

    public class OrderingPhysician
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }

    public class PlacerOrderNumber
    {
        public int Number { get; set; }
        public string Entity { get; set; }
    }

   public class PharmacyEncodedOrder
    {
        public string DrugName { get; set; }
        public string CodingSystem { get; set; }
        public string Strength { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public int Duration { get; set; }
        public int QuantityPrescribed { get; set; }
        public string PrescriptionNotes { get; set; }
    }
}

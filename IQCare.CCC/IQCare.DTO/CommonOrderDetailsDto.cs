using System;

namespace IQCare.DTO
{
    public class CommonOrderDetailsDto
    {
        public int ptnpk { get; set; }
        public int PatientId { get; set; }
        public string OrderControl { get; set; }
        public PlacerOrderNumberDto PlacerOrderNumberDto { get; set; }
        public string OrderStatus { get; set; }
        public OrderingPysicianDto OrderingPhysicianDto { get; set; }
        public DateTime TransactionDatetime { get; set; }
        public string Notes { get; set; }
    }
}

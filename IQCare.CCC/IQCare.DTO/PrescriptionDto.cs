using System;
using System.Collections.Generic;

namespace IQCare.DTO
{
    public class PrescriptionDto
    {
        public PrescriptionDto()
        {
            MesssageHeader = new MessageHeader();
            PatientIdentification = new DtoPatientIdentification();
            CommonOrderDetails = new CommonOrderDetailsDto();
            PharmacyEncodedOrderDto = new List<PharmacyEncorderOrderDto>();
        }

        public MessageHeader MesssageHeader { get; set; }

        //public DTOIdentifier InternalPatientIdentifier { get; set; }
        public DtoPatientIdentification PatientIdentification { get; set; }

        // public PatientNameDto Patientname { get; set; }
        public CommonOrderDetailsDto CommonOrderDetails { get; set; }

        public List<PharmacyEncorderOrderDto> PharmacyEncodedOrderDto { get; set; }
    }
}


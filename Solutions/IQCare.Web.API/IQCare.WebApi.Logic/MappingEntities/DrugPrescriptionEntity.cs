using System;
using System.Collections.Generic;
using IQCare.DTO;
using IQCare.WebApi.Logic.MappingEntities.drugs;

namespace IQCare.WebApi.Logic.MappingEntities
{
    public class DrugPrescriptionEntity
    {
        public DrugPrescriptionEntity()
        {
            MESSAGE_HEADER = new MessageHeaderEntity(); 
            PATIENT_IDENTIFICATION=new PrescriptionIdentification();;
            COMMON_ORDER_DETAILS = new CommonOrderDetailsEntity();
            PHARMACY_ENCODED_ORDER = new List<PharmacyEncorderOrderEntity>();
        }

        public MessageHeaderEntity MESSAGE_HEADER { get; set; }
        public PrescriptionIdentification PATIENT_IDENTIFICATION { get; set; }
        public CommonOrderDetailsEntity COMMON_ORDER_DETAILS { get; set; }
        public List<PharmacyEncorderOrderEntity> PHARMACY_ENCODED_ORDER { get; set; }
    }
}

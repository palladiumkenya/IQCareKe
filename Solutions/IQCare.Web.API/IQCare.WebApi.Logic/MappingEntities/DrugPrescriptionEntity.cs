using System;
using System.Collections.Generic;
using IQCare.WebApi.Logic.MappingEntities.drugs;

namespace IQCare.WebApi.Logic.MappingEntities
{
    public class DrugPrescriptionEntity
    {
        public DrugPrescriptionEntity()
        {
            MESSAGE_HEADER = new MESSAGE_HEADER(); 
            PATIENT_IDENTIFICATION=new PATIENT_IDENTIFICATION();
            COMMON_ORDER_DETAILS = new COMMON_ORDER_DETAILS();
            PHARMACY_ENCODED_ORDER = new List<PHARMACY_ENCODED_ORDER>();
        }

        public MESSAGE_HEADER MESSAGE_HEADER { get; set; }
        public PATIENT_IDENTIFICATION PATIENT_IDENTIFICATION { get; set; }
        public COMMON_ORDER_DETAILS COMMON_ORDER_DETAILS { get; set; }
        public List<PHARMACY_ENCODED_ORDER> PHARMACY_ENCODED_ORDER { get; set; }
    }
}

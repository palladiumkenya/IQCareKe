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
           MessageHeaderEntity=new MessageHeaderEntity(); 
            Patientidentification=new PATIENTIDENTIFICATION();
            COMMONORDERDETAILS =new COMMONORDERDETAILS();
            PharmacyEncorderEntity =new List<PHARMACYENCODEDORDER>();
        }

        public MessageHeaderEntity MessageHeaderEntity { get; set; }
        public PATIENTIDENTIFICATION Patientidentification { get; set; }
        public COMMONORDERDETAILS COMMONORDERDETAILS { get; set; }
        public List<PHARMACYENCODEDORDER> PharmacyEncorderEntity { get; set; }
    }
}

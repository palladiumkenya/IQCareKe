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
            CommonOrderDetailsEntity =new CommonOrderDetailsEntity();
            PharmacyEncorderEntity =new List<PharmacyEncorderOrderEntity>();
        }

        public MessageHeaderEntity MessageHeaderEntity { get; set; }
        public PATIENTIDENTIFICATION Patientidentification { get; set; }
        public CommonOrderDetailsEntity CommonOrderDetailsEntity { get; set; }
        public List<PharmacyEncorderOrderEntity> PharmacyEncorderEntity { get; set; }
    }
}

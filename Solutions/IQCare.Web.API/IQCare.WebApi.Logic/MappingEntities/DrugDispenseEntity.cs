﻿using System.Collections.Generic;
using IQCare.WebApi.Logic.MappingEntities.drugs;

namespace IQCare.WebApi.Logic.MappingEntities
{
    public class DrugDispenseEntity
    {
        public MESSAGEHEADER Messageheader { get; set; }
        public PATIENTIDENTIFICATION Patientidentification { get; set; }
        public CommonOrderDetailsEntity Commonorderdetails { get; set; }
        public List<PharmacyEncorderOrderEntity> Pharmacyencodedorder { get; set; }
        public List<PharmacyDispense> PharmacyDispense { get; set; }
    }

    public class PharmacyDispense
    {
       public string DrugName { get; set; }
       public string CodingSystem { get; set; }
      public string  ActualDrugs { get; set; }
       public string Strength { get; set; }
       public string Dosage { get; set; }
       public string Frequency { get; set; } 
       public int  Duration { get; set; }
       public int QuantityDispensed { get; set; }
       public string DispensingNotes { get; set; }
		
    }
}

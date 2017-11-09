using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.WebApi.Logic.MappingEntities.drugs
{
   public class PharmacyEncorderOrderEntity
    {
        public string DRUG_NAME { get; set; }
        public string CODING_SYSTEM { get; set; }
        public string STRENGTH { get; set; }
        public Decimal DOSAGE { get; set; }
        public string FREQUENCY { get; set; }
        public Decimal DURATION { get; set; }
        public Decimal QUANTITY_PRESCRIBED { get; set; }
        public string TREATMENT_INSTRUCTION { get; set; }
        public string INDICATION { get; set; }
       public DateTime PHARMACY_ORDER_DATE { get; set; }
        public string PrescriptionNotes { get; set; }

    }
}

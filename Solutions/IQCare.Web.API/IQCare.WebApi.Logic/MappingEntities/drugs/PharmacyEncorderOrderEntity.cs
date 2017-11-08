using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.WebApi.Logic.MappingEntities.drugs
{
   public class PharmacyEncorderOrderEntity
    {
        public string DrugName { get; set; }
        public string CodingSystem { get; set; }
        public string Strength { get; set; }
        public Decimal Dosage { get; set; }
        public string Frequency { get; set; }
        public Decimal Duration { get; set; }
        public Decimal QuantityPrescribed { get; set; }
        public string PrescriptionNotes { get; set; }

    }
}

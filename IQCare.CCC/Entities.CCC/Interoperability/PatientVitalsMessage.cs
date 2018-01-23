using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Entities.CCC.Interoperability
{
    [Serializable]
    [Table("API_PatientVitalsView")]
    public class PatientVitalsMessage
    {
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public string WeightUnits { get; set; }
        public string HeightUnits { get; set; }
        public string VisitDate { get; set; }
    }
}

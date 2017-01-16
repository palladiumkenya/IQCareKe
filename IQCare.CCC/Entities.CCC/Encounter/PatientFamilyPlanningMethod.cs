using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Entities.CCC.Encounter
{
    [Serializable]
    [Table("PatientFamilyPlanningMethod")]


   public class PatientFamilyPlanningMethod
    {
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public int PatientFpId { get; set; }
        public int FPMethodId { get; set; }
        public int DeleteFlag { get; set; }
    }
}

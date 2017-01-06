using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Entities.CCC.Encounter
{
    [Serializable]
    [Table("PregnancyIndicator")]

    public class PregnancyIndicator :BaseObject
    {        
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public DateTime Lmp { get; set; }
        public DateTime Edd { get; set; }
        public int PregnancyStatusId { get; set; }
        public bool AncProfile { get; set; }
        public DateTime AncProfileDate { get; set; }
    }
}

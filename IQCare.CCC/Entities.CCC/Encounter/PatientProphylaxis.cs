using Entities.CCC.Enrollment;
using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Entities.CCC.Encounter
{
    [Serializable]
    [Table("PatientProphylaxis")]

    public class PatientProphylaxis :BaseObject
    {
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual PatientEntity Patient { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int ProphylaxisTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public bool complete { get; set; }
        public DateTime CompletionDate { get; set; }

    }
}

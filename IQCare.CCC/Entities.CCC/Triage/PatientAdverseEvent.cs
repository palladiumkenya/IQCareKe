using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Entities.CCC.Triage
{
    [Serializable]
    [Table("AdverseEvent")]
    public class PatientAdverseEvent
    {
        [Column]

        public virtual int PatientId { get; set; }
        [ForeignKey("patientId")]
        public virtual int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public string EventName { get; set; }
        public string EventCause { get; set; }
        public string Severity { get; set; }
        public string Action { get; set; }
    }
}

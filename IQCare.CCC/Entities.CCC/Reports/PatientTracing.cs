using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.CCC.Enrollment;
using Entities.CCC.Visit;
using Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Reports
{
    [Serializable]
    [Table("PatientTracing")]
    public class PatientTracing
    {
        [Column]
        [Key]
        public int TracingId { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public DateTime TracingDate { get; set; }
        public int TracingMethod { get; set; }
        public int TracingOutcome { get; set; }
        public string TracingOutcomeOther { get; set; }
        public DateTime? TracingDateOfDeath { get; set; }
        public string TracingTransferFacility { get; set; }
        public DateTime? TracingTransferDate { get; set; }
        public string TracingNotes { get; set; }
        public int TracingStatus { get; set; }
        public DateTime CreateDate { get; set; }
        public int CraetedBy{get; set;}
    }
}

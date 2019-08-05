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
using System.Xml.Serialization;

namespace Entities.CCC.Reports
{
    [Serializable]
    [Table("Tracing")]
    public class Tracing
    {
        public int Id { get; set; }
        public int PersonID { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int TracingType { get; set; }
        public DateTime DateTracingDone { get; set; }
        public int Mode { get; set; }
        public int Outcome { get; set; }
        public string Remarks { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatedBy { get; set; }
        public bool DeleteFlag { get; set; }
        [XmlIgnore]
        public string AuditData { get; set; }

        public int? Consent { get; set; }
        public DateTime? ReminderDate { get; set; }
        public DateTime? DateBookedTesting { get; set; }
        public DateTime? TracingDateOfDeath { get; set; }
        public string TracingTransferFacility { get; set; }
        public DateTime? TracingTransferDate { get; set; }
    }
}

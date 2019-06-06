using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace IQCare.Prep.Core.Models
{
  public class PatientMasterVisit
        {
            public int Id { get; set; }
            public int PatientId { get; set; }
            public int ServiceId { get; set; }
            public DateTime Start { get; set; }
            public DateTime? End { get; set; }
            public int? VisitScheduled { get; set; }
            public int? VisitBy { get; set; }
            public int? VisitType { get; set; }
            public DateTime? VisitDate { get; set; }
            public bool Active { get; set; }
            public int? Status { get; set; }
            [XmlIgnore]
            public string AuditData { get; set; }
            public DateTime CreateDate { get; set; }
            public int CreatedBy { get; set; }
            public bool DeleteFlag { get; set; }
        }
}


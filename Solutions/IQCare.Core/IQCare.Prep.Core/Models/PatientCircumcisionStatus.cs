using System;
using System.Reflection.Metadata;

namespace IQCare.Prep.Core.Models
{
    public class PatientCircumcisionStatus
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int ClientCircumcised { get; set; }
        public int? ReferredToVMMC  { get; set; }
        public bool DeleteFlag { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string AuditData { get; set; }
    }
}
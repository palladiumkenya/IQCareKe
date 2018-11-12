using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Core.Models
{
    public class PatientAllergy
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public string Allergen { get; set; }
        public Boolean DeleteFlag { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string AuditData { get; set; }
        public int Reaction { get; set; }
        public int Severity { get; set; }
        public DateTime OnsetDate { get; set; }
    }
}

using System;

namespace IQCare.Common.Core.Models
{
    public class PersonTreatmentSupporter
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int SupporterId { get; set; }
        //public string MobileContact { get; set; }
        public bool DeleteFlag { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string AuditData { get; set; }
        public int ContactCategory { get; set; }
    }
}
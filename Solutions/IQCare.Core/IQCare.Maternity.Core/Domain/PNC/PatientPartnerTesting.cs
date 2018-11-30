using System;
using System.ComponentModel.DataAnnotations;

namespace IQCare.Maternity.Core.Domain.PNC
{
    public class PatientPartnerTesting
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int PartnerTested { get; set; }
        public int PartnerHIVResult { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatedBy { get; set; }
        public Boolean DeleteFlag { get; set; }
        public string AuditData { get; set; }
    }
}

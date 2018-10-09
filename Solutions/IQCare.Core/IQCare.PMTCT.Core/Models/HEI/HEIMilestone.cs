using System;
using System.ComponentModel.DataAnnotations;

namespace IQCare.PMTCT.Core.Models.HEI
{
    public class HEIMilestone
    {
        [Key]
        public int Id { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int PatientId { get; set; }
        public int? TypeAssessed { get; set; }
        public bool? Achieved { get; set; }
        public int? Status { get; set; }
        public string Comment { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public bool DeleteFlag { get; set; }
    }
}

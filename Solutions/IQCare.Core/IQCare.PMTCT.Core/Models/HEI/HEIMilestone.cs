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
        public int? TypeAssessedId { get; set; }
        public bool? AchievedId { get; set; }
        public int? StatusId { get; set; }
        public string Comment { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public bool DeleteFlag { get; set; }
        public DateTime DateAssessed { get; set; }
    }
}

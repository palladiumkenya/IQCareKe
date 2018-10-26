using System;
using System.ComponentModel.DataAnnotations;

namespace IQCare.PMTCT.Core.Models
{
    public class PatientProfile
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public decimal AgeMenarche { get; set; }
        public int PregnancyId { get; set; }
        public int VisitNumber { get; set; }
        public int VisitType { get; set; }
        public int? TreatedForSyphilis { get; set; }
        public int? CreatedBy { get; set; }
        public bool DeleteFlag{get; set; }
        public DateTime CreateDate { get; set;}
        public int? DaysPostpartum { get; set; }

    }
}
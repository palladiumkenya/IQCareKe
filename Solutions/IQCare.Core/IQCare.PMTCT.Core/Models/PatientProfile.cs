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
        public Decimal AgeMenarche { get; set; }
        public int PregnancyId { get; set; }
        public int VisitNumber { get; set; }
        public int VisitType { get; set; }
        public int? TreatedForSyphilis { get; set; }
        public int? CreatedBy { get; set; }
        public Boolean DeleteFlag{get; set; }
        public DateTime CreateDate { get; set;} 
        
    }
}
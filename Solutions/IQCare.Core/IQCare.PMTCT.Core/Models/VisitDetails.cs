using System;
using System.ComponentModel.DataAnnotations;

namespace IQCare.PMTCT.Core.Models
{
    public class VisitDetails
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int? VisitNumber { get; set; }
        public int? DaysPostPartum { get; set; }
        public int ? VisitType { get; set; }
        public Boolean DeleteFlag { get; set; }
        public int  CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
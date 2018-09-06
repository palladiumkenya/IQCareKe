using System;
using System.ComponentModel.DataAnnotations;
using IQCare.Common.Core.Models;

namespace IQCare.PMTCT.Core.Models
{
    public class PatientPregnancy
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public DateTime? Lmp { get; set; }
        public DateTime? Edd { get; set; }
        public decimal? Gestation { get; set; }
        public int? Gravidae { get; set; }
        public int? Parity { get; set; }
        public int? Parity2 { get; set; }
        public int? Outcome { get; set; }
        public DateTime? DateOfOutcome { get; set; }
        public Patient Patient { get; set; }
        public PatientMasterVisit PatientMasterVisit { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
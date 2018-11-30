using System;
using System.ComponentModel.DataAnnotations;
using IQCare.Common.Core.Models;

namespace IQCare.PMTCT.Core.Models
{
    public class PreventiveService
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int PreventiveServiceId { get; set; }
        public DateTime? PreventiveServiceDate { get; set; }
        public string Description { get; set; }
        public DateTime? NextSchedule { get; set; }
        public Patient Patient { get; set; }
        public PatientMasterVisit PatientMasterVisit { get; set; }
        public int CreatedBy { get; set; }
        public Boolean DeleteFlag { get; set; }
    }
}
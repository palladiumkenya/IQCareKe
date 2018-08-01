using System;
using System.ComponentModel.DataAnnotations;
using IQCare.Common.Core.Models;

namespace IQCare.PMTCT.Core.Models
{
    public class PatientScreening
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int ScreeningTypeId { get; set; }
        public bool ScreeningDone { get; set; }
        public DateTime ScreeningDate { get; set; }
        public int ScreeningCategoryId { get; set; }
        public string Comment { get; set; }

        public PatientMasterVisit PatientMasterVisit { get; set; }
        public Patient Patient { get; set; }
    }
}
using System;

namespace IQCare.PMTCT.Core.Models.Views
{
    public class PmtctPatientScreeningView
    {
         public int Id { get; set;}
         public int PatientId { get; set; }
        public int ScreeningTypeId { get; set; }
        public string ScreeningType { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int ScreeningDone { get; set; }
        public DateTime? ScreeningDate { get; set; }
        public int ScreeningCategoryId { get; set; }
        public string ScreeningCategory { get; set; }
        public int ScreeningValueId { get; set; }
        public string ScreeningValue { get; set; }
        public string Comment { get; set; }
        public Boolean DeleteFlag { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
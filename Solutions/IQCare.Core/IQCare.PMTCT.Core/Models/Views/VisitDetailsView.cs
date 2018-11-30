using System;

namespace IQCare.PMTCT.Core.Models.Views
{
    public class VisitDetailsView
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int pregnancyId { get; set; }
        public int serviceAreaId { get; set; }
       public string ServiceAreaName { get; set; }
        public DateTime VisitDate { get; set; }
         public int? VisitNumber { get; set; }
        public int? DaysPostPartum { get; set; }
        public int?  VisitType { get; set; }
        public string  VisitTypeName { get; set; }
        public Boolean DeleteFlag { get; set; }
        public int CreatedBy { get; set; }
    }
}
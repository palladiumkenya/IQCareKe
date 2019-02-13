using System;

namespace IQCare.PMTCT.Core.Models.Views
{
    public class PatientPreventiveServiceView
    {
       public int Id { get; set; }
         public int    PatientId { get; set; }
         public int  PatientMasterVisitId { get; set; }
        public int PreventiveServiceId { get; set; }
         public string    PreventiveService { get; set; }
         public DateTime?   PreventiveServiceDate { get; set; }
         public string    Description { get; set; }
         public Boolean       DeleteFlag { get; set; }
         public int   CreatedBy { get; set; }
          public DateTime?      CreateDate { get; set; }
          public DateTime?  NextSchedule { get; set; }
    }
}
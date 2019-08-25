using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Prep.Core.Models
{
    public class PatientScreening
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public DateTime? VisitDate { get; set; }
        public int? ScreeningTypeId { get; set; }
        public bool ScreeningDone { get; set; }
        public DateTime? ScreeningDate { get; set; }
        public int? ScreeningCategoryId { get; set; }
        public int ScreeningValueId { get; set; }
        public string Comment { get; set; }

        public bool Active { get; set; }

        public bool DeleteFlag { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string AuditData { get; set; }
    }
}

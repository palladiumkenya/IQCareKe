using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Pharm.Core.Models
{
  public  class ARVTreatmentTracker
    {
        public int Id { get; set; }
        public int PatientId { get; set; }

        public int ServiceAreaId { get; set; }

        public int PatientMasterVisitId { get; set; }

        public int RegimenId { get; set; }

        public int RegimenLineId { get; set; }

        public int TreatmentStatusId { get; set; }

        public int? TreatmentStatusReasonId { get; set; }

        public bool DeleteFlag { get; set; }

        public int CreateBy { get; set; }

        public DateTime CreateDate { get; set; }

    }
}

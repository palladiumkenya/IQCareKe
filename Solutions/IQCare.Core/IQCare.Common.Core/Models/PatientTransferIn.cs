using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Core.Models
{
    public class PatientTransferIn
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int ServiceAreaId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public DateTime TransferInDate { get; set; }
        public DateTime TreatmentStartDate { get; set; }
        public string CurrentTreatment { get; set; }
        public string FacilityFrom { get; set; }
        public int MflCode { get; set; }
        public string CountyFrom { get; set; }
        public string TransferInNotes { get; set; }

        public DateTime CreateDate { get; set; }

        public int CreatedBy { get; set; }

        public bool DeleteFlag { get; set; }
    }
}

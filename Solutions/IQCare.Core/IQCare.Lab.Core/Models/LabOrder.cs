using System;

namespace IQCare.Lab.Core.Models
{
    public class LabOrder
    {
        public int Id { get; set; }

        public int Ptn_Pk { get; set; }

        public int LocationId { get; set; }

        public int VisitId { get; set; }

        public int ModuleId { get; set; }

        public int OrderedBy { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? PreClinicLabDate { get; set; }

        public string ClinicalOrderNotes { get; set; }

        public string OrderNumber { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreateDate { get; set; }

        public string OrderStatus { get; set; }

        public int UserId { get; set; }

        public DateTime UpdateDate { get; set; }

        public bool DeleteFlag { get; set; }

        public int DeletedBy { get; set; }

        public DateTime DeleteDate { get; set; }

        public string DeleteReason { get; set; }

        public int PatientId { get; set; }

        public int PatientMasterVisitId { get; set; }

        public string AuditData { get; set; }
    }
}
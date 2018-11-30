using System;

namespace IQCare.Lab.Core.Models
{
    public enum LabOrderStatus
    {
        Pending,
        Complete
    }

    public class LabOrder
    {

        public LabOrder()
        {

        }
        public LabOrder(int ptn_Pk, int patientId, int locationId, int visitId, int moduleId, int orderdBy,
            int userId,string clincalOrderNotes,string orderStatus, int patientMasterId)
        {
            Ptn_Pk = ptn_Pk;
            PatientId = patientId;
            LocationId = locationId;
            VisitId = visitId;
            ModuleId = moduleId;
            OrderedBy = orderdBy;
            UserId = userId;
            ClinicalOrderNotes = clincalOrderNotes;
            OrderStatus = orderStatus;
            PatientMasterVisitId = patientMasterId;
        }

        public int Id { get; private set; }

        public int Ptn_Pk { get; private set; }

        public int LocationId { get; private set; }

        public int VisitId { get; private set; }

        public int ModuleId { get; private set; }

        public int OrderedBy { get; private set; }

        public DateTime OrderDate { get; private set; }

        public DateTime? PreClinicLabDate { get; private set; }

        public string ClinicalOrderNotes { get; private set; }

        public string OrderNumber { get; private set; }

        public int CreatedBy { get; private set; }

        public DateTime CreateDate { get; private set; }

        public string OrderStatus { get; private set; }

        public int UserId { get; private set; }

        public DateTime ? UpdateDate { get; private set; }

        public bool DeleteFlag { get; private set; }

        public int DeletedBy { get; private set; }

        public DateTime? DeleteDate { get; private set; }

        public string DeleteReason { get; private set; }

        public int PatientId { get; private set; }

        public int PatientMasterVisitId { get; private set; }

        public string AuditData { get; private set; }

        public  void CompleteOrder()
        {
            OrderStatus = LabOrderStatus.Complete.ToString();
        }
    }
}
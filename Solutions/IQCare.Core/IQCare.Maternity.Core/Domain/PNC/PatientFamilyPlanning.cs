using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.Core.Domain.PNC
{
    public class PatientFamilyPlanning
    {
        public PatientFamilyPlanning(){}
        public PatientFamilyPlanning(int patientId, int patientMasterVisitId, int familyPlanningStatusId, int reasonNotOnFamilyPlanning,
            DateTime visitDate, int createdBy)
        {
            PatientId = patientId;
            PatientMasterVisitId = patientMasterVisitId;
            FamilyPlanningStatusId = familyPlanningStatusId;
            ReasonNotOnFPId = reasonNotOnFamilyPlanning;
            VisitDate = visitDate;
            CreateDate = DateTime.Now;
            CreatedBy = createdBy;
        }
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int FamilyPlanningStatusId { get; set; }
        public int ReasonNotOnFPId { get; set; }
        public bool DeleteFlag { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime VisitDate { get; set; }
        public string AuditData { get; set; }

    }
}

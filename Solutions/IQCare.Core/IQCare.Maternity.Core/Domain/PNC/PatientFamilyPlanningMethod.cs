using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.Core.Domain.PNC
{
    public class PatientFamilyPlanningMethod
    {
        public PatientFamilyPlanningMethod()
        {

        }

        public PatientFamilyPlanningMethod(int patientId, int patientFamilyPlanningId, int familyPlanningMethodId,int createdBy)
        {
            PatientId = patientId;
            PatientFamilyPlanningId = patientFamilyPlanningId;
            FamilyPlanningMethodId = familyPlanningMethodId;
            Active = true;
            CreatedBy = createdBy;
            CreateDate = DateTime.Now;
        }

        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientFamilyPlanningId { get; set; }
        public int FamilyPlanningMethodId { get; set; }
        public bool Active { get; set; }
        public bool DeleteFlag { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string AuditData { get; set; }

    }
}

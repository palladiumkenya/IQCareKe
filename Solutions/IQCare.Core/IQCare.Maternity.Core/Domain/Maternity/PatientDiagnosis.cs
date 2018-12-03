using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.Core.Domain.Maternity
{
    public class PatientDiagnosis
    {

        public PatientDiagnosis()
        {
                
        }

        public PatientDiagnosis(int patientId, int masterVisitId, string diagnosis, string managementPlan, int createdBy)
        {
            PatientId = patientId;
            PatientMasterVisitId = masterVisitId;
            Diagnosis = diagnosis;
            ManagementPlan = diagnosis;
            ManagementPlan = managementPlan;
            CreatedBy = createdBy;
            DateCreated = DateTime.Now;
        }
        public int Id { get; private set; }
        public int PatientId { get; private set; }
        public int PatientMasterVisitId { get; private set; }
        public string Diagnosis { get; private set; }
        public string ManagementPlan { get; private set; }
        public int CreatedBy { get; private set; }
        public DateTime DateCreated { get; private set; }
        public bool DeleteFlag { get; private set; }
    }
}

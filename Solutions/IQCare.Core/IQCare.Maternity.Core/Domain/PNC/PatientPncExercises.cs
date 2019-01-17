using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.Core.Domain.PNC
{
    public class PatientPncExercises
    {
        public PatientPncExercises()
        {

        }

        public PatientPncExercises(int patientId, int patientMasterVisitId, int pncExercisesDone, int createdBy, DateTime createDate)
        {
            PatientId = patientId;
            PatientMasterVisitId = patientMasterVisitId;
            PncExercisesDone = pncExercisesDone;
            CreatedBy = createdBy;
            CreateDate = createDate;
        }

        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int PncExercisesDone { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string AuditData { get; set; }
    }
}

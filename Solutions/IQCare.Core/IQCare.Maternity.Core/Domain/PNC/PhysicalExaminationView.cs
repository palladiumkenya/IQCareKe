using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace IQCare.Maternity.Core.Domain.PNC
{
    public class PhysicalExaminationView
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public int ExamId { get; set; }
        public int ExaminationTypeId { get; set; }
        public int FindingId { get; set; }
        public string FindingsNotes { get; set; }
        public int PatientMasterVisitId { get; set; }
        public string ExamDisplayName { get; set; }
        public string ExamName { get; set; }
    }
}

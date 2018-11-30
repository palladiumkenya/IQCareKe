using System;
using System.ComponentModel.DataAnnotations;

namespace IQCare.PMTCT.Core.Models
{
    public class PhysicalExamination
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int ExaminationTypeId { get; set; }
        public int ExamId { get; set; }
        public int DeleteFlag { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public int FindingId { get; set; }
        public string FindingsNotes { get; set; }
    }
}

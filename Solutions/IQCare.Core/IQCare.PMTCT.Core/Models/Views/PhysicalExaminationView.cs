using System;
using System.ComponentModel.DataAnnotations;

namespace IQCare.PMTCT.Core.Models.Views
{
    public class PhysicalExaminationView
    {
        [Key]
        public int Id { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int PatientId { get; set; }
        public int ExaminationTypeId { get; set; }
        public string ExaminationType { get; set; }
        public int ExamId { get; set; }
        public string Exam { get; set; }
        public Boolean DeleteFlag { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public int FindingId { get; set; }
        public string Findings { get; set; }
        public string FindingsNotes { get; set; }
    }
}
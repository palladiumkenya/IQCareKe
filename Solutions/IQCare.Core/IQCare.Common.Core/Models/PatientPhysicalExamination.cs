using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Core.Models
{
   public class PatientPhysicalExamination
    {
      public int Id { get; set; }
      public int PatientId { get; set; }
      public int PatientMasterVisitId { get; set; }
      public int ExaminationTypeId { get; set; }
      public int ExamId { get; set; }
      public int DeleteFlag { get; set; }
      public int? FindingId { get; set; }
      public string FindingsNotes { get; set; }
    }
}

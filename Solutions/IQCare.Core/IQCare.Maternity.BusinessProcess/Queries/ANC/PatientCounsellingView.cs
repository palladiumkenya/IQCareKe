using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace IQCare.Maternity.BusinessProcess.Queries.ANC
{
    public class PatientCounsellingView
    {
        [Key]
       public int Id { get; set; }
       public int PatientMasterVisitId { get; set; }
       public int PatientId { get; set; }
        public int CounsellingTopicId { get; set; }
        public string CounsellingTopic { get; set; }
        public DateTime CounsellingDate { get; set; }
         public string Description { get; set; }
         public Boolean DeleteFlag { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatedBy { get; set; }
    }
}
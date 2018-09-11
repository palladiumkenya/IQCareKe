using IQCare.Common.Core.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IQCare.PMTCT.Core.Models
{
    public class PatientEducation
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int CounsellingTopicId { get; set; }
        [NotMapped]
        public string counsellingTopic { get; set; }
        public DateTime CounsellingDate { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }

        public PatientMasterVisit PatientMasterVisit { get; set; }
        public Patient Patient { get; set; }
    
    }
}
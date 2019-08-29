using IQCare.Common.Core.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IQCare.PMTCT.Core.Models
{
    public class PatientEducation
    {
        public PatientEducation()
        {

        }
        public PatientEducation(int patientId, int masterVisitId, int counsellingTopicId, 
           DateTime ? counsellingDate, bool counsellingDone, int createdBy)
        {
            PatientId = patientId;
            PatientMasterVisitId = masterVisitId;
            CounsellingTopicId = counsellingTopicId;
            CounsellingDate = counsellingDate;
            Description = counsellingDone ? "N/A" : "Counselling not done";
            CreatedBy = createdBy;
            CreateDate = DateTime.Now;
            
        }

        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int CounsellingTopicId { get; set; }
        public DateTime ? CounsellingDate { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }

        public Boolean? DeleteFlag { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual PatientMasterVisit PatientMasterVisit { get; set; }
        public virtual Patient Patient { get; set; }
    
    }
}
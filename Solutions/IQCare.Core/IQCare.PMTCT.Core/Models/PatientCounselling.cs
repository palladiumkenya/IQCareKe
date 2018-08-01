using IQCare.Common.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IQCare.PMTCT.Core.Models
{
    public class PatientCounselling
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int CounsellingTopicId { get; set; }
        public DateTime CounsellingDate { get; set; }
        public string Description { get; set; }

        public PatientMasterVisit PatientMasterVisit { get; set; }
        public Patient Patient { get; set; }
    
    }
}
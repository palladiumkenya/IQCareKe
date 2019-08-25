using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Common.Core.Models
{
    public class PatientCareEnding
    { 
        
        public int Id { get; set; }
        public int PatientId { get; set; }
       
      
        public int PatientMasterVisitId { get; set; }
   
   
        public int PatientEnrollmentId { get; set; }
        public int ExitReason { get; set; }
        public DateTime ExitDate { get; set; }
        public string TransferOutFacility { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public string CareEndingNotes { get; set; }
        public bool Active { get; set; }

        public bool DeleteFlag { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreateDate { get; set; }
    }
}

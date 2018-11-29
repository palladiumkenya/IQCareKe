using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.PMTCT.Core.Models.HEI
{
    public class HeiFeeding
    {
        public int Id { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int PatientId { get; set; }
        public int FeedingModeId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public Boolean DeleteFlag { get; set; }
    }
}

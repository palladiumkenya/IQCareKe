using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.PMTCT.Core.Models.HEI
{
    public class PatientHeiProfile
    {
        public int Id { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int PatientId { get; set; }
        public DateTime VisitDate { get; set; }
        public int VisitType { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Boolean DeleteFlag { get; set; }
    }
}

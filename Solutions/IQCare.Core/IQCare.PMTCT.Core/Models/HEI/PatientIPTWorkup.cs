using System;

namespace IQCare.PMTCT.Core.Models.HEI
{
    public class PatientIptWorkup
    {
        public int Id { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int PatientId { get; set; }
        public bool YellowColouredUrine { get; set; }
        public bool Numbness { get; set; }
        public bool YellownessOfEyes { get; set; }
        public bool AbdominalTenderness { get; set; }
        public string LiverFunctionTests { get; set; }
        public bool DeleteFlag { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public bool StartIpt { get; set; }
        public DateTime IptStartDate { get; set; }
        public int IptRegimen { get; set; }
    }
}
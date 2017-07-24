using Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Tb
{
    [Serializable]
    [Table("PatientIptWorkup")]
    public class PatientIptWorkup : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int PatientId { get; set; }
        public bool YellowColouredUrine { get; set; }
        public bool Numbness { get; set; }
        public bool YellownessOfEyes { get; set; }
        public bool AbdominalTenderness { get; set; }
        public string  LiverFunctionTests { get; set; }
        public bool ? StartIpt { get; set; }
        public DateTime ? IptStartDate { get; set; }
    }
}
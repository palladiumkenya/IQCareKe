using Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Tb
{
    [Serializable]
    [Table("PatientIcf")]
    public class PatientIcf : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int PatientId { get; set; }
        public bool Cough { get; set; }
        public bool Fever { get; set; }
        public bool WeightLoss { get; set; }
        public bool NightSweats { get; set; }
    }
}
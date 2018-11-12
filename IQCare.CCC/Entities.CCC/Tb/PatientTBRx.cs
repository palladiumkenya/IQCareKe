using Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Tb
{
    [Serializable]
    [Table("TuberclosisTreatment")]
    public class PatientTBRx : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int PatientId { get; set; }
        public DateTime? TBRxStartDate { get; set; }
        public DateTime? TBRxEndDate { get; set; }
        public int? RegimenId { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;

namespace Entities.CCC.Visit
{
    [Serializable]
    [Table("PatientMasterVisit")]

    public class PatientMasterVisit :BaseObject
    {
        [Column]

        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public int FacilityId { get; set; }
        public DateTime VisitDate { get; set; }
        public bool Schedule { get; set; }
        public int VisitBy { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ServiceAreaId { get; set; }
    }
}

using Entities.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Encounter
{
    [Serializable]
    [Table("PatientFamilyPlanning")]

    public class PatientFamilyPlanning:BaseObject
    {
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public int PatientMasterVisitId { get; set; }
        [ForeignKey("PatientMasterVisitId")]
        public int FpStatusId { get; set; }
        public int ReasonNotOnFpId { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Encounter
{
    [Serializable]
    [Table("PatientWHOStage")]
    public class PatientWhoStage
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int WHOStage { get; set; }
    }
}

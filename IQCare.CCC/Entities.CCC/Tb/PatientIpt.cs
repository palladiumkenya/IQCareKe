using Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.CCC.Tb
{
    [Serializable]
    [Table("PatientIpt")]
    public class PatientIpt : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int PatientId { get; set; }
        public DateTime IptDueDate { get; set; }
        public DateTime IptDateCollected { get; set; }
        public int Weight { get; set; }
        public bool Hepatotoxicity { get; set; }
        public bool Peripheralneoropathy { get; set; }
        public bool Rash { get; set; }
        public int AdheranceMeasurement { get; set; }
        public string HepatotoxicityAction { get; set; }
        public string PeripheralneoropathyAction { get; set; }
        public string RashAction { get; set; }
        public string AdheranceMeasurementAction { get; set; }

    }
}
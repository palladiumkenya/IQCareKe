using System;
using System.ComponentModel.DataAnnotations;

namespace IQCare.PMTCT.Core.Models.HEI
{
    public class PatientIpt
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
        public bool DeleteFlag { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string HepatotoxicityAction { get; set; }
        public string PeripheralneoropathyAction { get; set; }
        public string  RashAction { get; set; }
        public string AdheranceMeasurementAction { get; set; }
    }
}
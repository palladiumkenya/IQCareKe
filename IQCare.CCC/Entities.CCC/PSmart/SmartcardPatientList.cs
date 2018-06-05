using System;

namespace Entities.CCC.PSmart
{
    [Serializable]
    [System.ComponentModel.DataAnnotations.Schema.Table("vw_SmartcardPatientList")]
    public class SmartcardPatientListX
    {
        public int PatientId { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public bool CardProcessed { get; set; }
    }
}
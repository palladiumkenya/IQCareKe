using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IQCare.PMTCT.Core.Models.HEI
{
    public class HeiPatientIcf
    {
        [Key]
        public int Id { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int PatientId { get; set; }
        public bool? Cough { get; set; }
        public bool? Fever { get; set; }
        public bool? WeightLoss { get; set; }
        public bool? NightSweats { get; set; }
        public bool DeleteFlag { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public bool OnAntiTbDrugs { get; set; }
        public bool? OnIpt { get; set; }
        public bool? EverBeenOnIpt { get; set; }
        public bool? ContactWithTb { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.PMTCT.Core.Models
{
    public class PatientVital
    {
        public PatientVital()
        {

        }

        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public decimal? Temperature { get; set; }
        public decimal? RespiratoryRate { get; set; }
        public decimal? HeartRate { get; set; }
        public int? BpDiastolic { get; set; }
        public int? BpSystolic { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Spo2 { get; set; }
        public decimal? Bmi { get; set; }
        public decimal? HeadCircumference { get; set; }
        public string BmiZ { get; set; }
        public string WeightForAge { get; set; }
        public string WeightForHeight { get; set; }
        public bool Active { get; set; }
        public DateTime? VisitDate { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
    }
}

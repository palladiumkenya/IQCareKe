using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public decimal? Muac { get; set; }
        public string BmiZ { get; set; }
        public string WeightForAge { get; set; }
        public string WeightForHeight { get; set; }
        [NotMapped]
        public string HeightForAge { get; set; }
        public bool Active { get; set; }
        public DateTime? VisitDate { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }

        public void UpdateVitalsInfo(dynamic vitalsInfo)
        {
            Weight = vitalsInfo.Weight;
            Height = vitalsInfo.Height;
            Temperature = vitalsInfo.Temperature;
            Bmi = vitalsInfo.Bmi;
            BmiZ = vitalsInfo.BmiZ.ToString();
            RespiratoryRate = vitalsInfo.RespiratoryRate;
            HeartRate = vitalsInfo.HeartRate;
            BpDiastolic = vitalsInfo.BpDiastolic;
            BpSystolic = vitalsInfo.BpSystolic;
            Spo2 = vitalsInfo.Spo2;
            HeadCircumference = vitalsInfo.HeadCircumference;
            Muac = vitalsInfo.Muac;
            WeightForAge = vitalsInfo.WeightForAge;
            WeightForHeight = vitalsInfo.WeightForHeight;
            VisitDate = vitalsInfo.VisitDate;
        }
    }
}

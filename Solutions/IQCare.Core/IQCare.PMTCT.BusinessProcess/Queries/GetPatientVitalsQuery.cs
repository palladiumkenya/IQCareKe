using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using IQCare.Library;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Queries
{
    public class GetPatientVitalsQuery : IRequest<Result<List<PatientVitalViewModel>>>
    {
        public int MasterVisitId { get; set; }
        
    }

    public class PatientVitalViewModel
    {
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
        public decimal? BmiZ { get; set; }
        public string WeightForAge { get; set; }
        public string WeightForHeight { get; set; }
        public string HeightForAge { get; set; }
        public decimal ? Muac { get; set; }
        public bool Active { get; set; }
        public DateTime? VisitDate { get; set; }
    }
}

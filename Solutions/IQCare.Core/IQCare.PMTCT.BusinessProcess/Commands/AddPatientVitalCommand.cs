using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;
using IQCare.PMTCT.Services.Interface.Triage;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands
{
    public class AddPatientVitalCommand : IRequest<Result<object>>
    {
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
        public decimal? BmiZ { get; set; }
        public string WeightForAge { get; set; }
        public decimal? Muac { get; set; }
        public string WeightForHeight { get; set; }
        public bool Active { get; set; }
        public DateTime? VisitDate { get; set; }
        public string Comment { get; set; }
        public int CreatedBy { get; set; }
    }

    public class UpdatePatientVitalCommand :IRequest<Result<object>>
    {
        public AddPatientVitalCommand PatientVitalInfo { get; set; }

    }

    public class CalculateZscoreCommand : IRequest<Result<ZscoreCalculationResult>>
    {
        public DateTime DateOfBirth { get; set; }
        public Sex Sex { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
    }


    public class ZscoreCalculationResult
    {
        public double WeightForAge { get; set; }
        public double WeightForHeight { get; set; }
        public double Bmiz { get; set; }
        public double HeightForAge { get; set; }
    }
   

}

using System;
using System.ComponentModel.DataAnnotations;
using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.Pregnancy
{
    public class PregnancyViewModel
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public DateTime? Lmp { get; set; }
        public DateTime? Edd { get; set; }
        public decimal? Gestation { get; set; }
        public int? Gravidae { get; set; }
        public int? Parity { get; set; }
        public int? Parity2 { get; set; }
        public int? Outcome { get; set; }
        public DateTime? DateOfOutcome { get; set; }
        public decimal? AgeAtMenarche { get; set; }
    }

    public class GetPregnancyCommand: IRequest<Result<PregnancyViewModel>>
    {
        public int PatientId { get; set; }
    }



}

using System;
using System.ComponentModel.DataAnnotations;
using IQCare.Library;
using IQCare.PMTCT.Core.Models;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.PatientHeiFeeding
{
    public class HeiFeedingViewModel
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int FeedingModeId { get; set; }
    }

    public class GetHeiFeedingCommand: IRequest<Result<HeiFeedingViewModel>>
    {
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
    }

}

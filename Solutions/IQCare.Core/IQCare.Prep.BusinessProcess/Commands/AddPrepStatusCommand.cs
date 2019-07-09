using IQCare.Library;
using IQCare.Prep.Core.Models;
using MediatR;

namespace IQCare.Prep.BusinessProcess.Commands
{
    public class AddPrepStatusCommand : IRequest<Result<PatientPrEPStatus>>
    {
        public int? Id { get; set; }
        public int PatientId { get; set; }
        public int PatientEncounterId { get; set; }
        public int SignsOrSymptomsHIV { get; set; }
        public int AdherenceCounsellingDone { get; set; }
        public int ContraindicationsPrepPresent { get; set; }
        public int PrepStatusToday { get; set; }
        public int CreatedBy { get; set; }
        public int? CondomsIssued { get; set; }
        public int? NoOfCondoms { get; set; }
    }
}
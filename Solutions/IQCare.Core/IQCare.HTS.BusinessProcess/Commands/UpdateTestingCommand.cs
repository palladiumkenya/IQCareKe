using IQCare.Library;
using MediatR;

namespace IQCare.HTS.BusinessProcess.Commands
{
    public class UpdateTestingCommand : IRequest<Result<UpdateTestingResponse>>
    {
        public int HtsEncounterId { get; set; }
        public int? CoupleDiscordant { get; set; }
        public int? FinalResultGiven { get; set; }
        public int RoundOneTestResult { get; set; }
        public int? RoundTwoTestResult { get; set; }
        public int? FinalResult { get; set; }
        public int AcceptedPartnerListing { get; set; }
        public int? ReasonsDeclinePartnerListing { get; set; }
        public string FinalResultsRemarks { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int ProviderId { get; set; }
        public int ServiceAreaId { get; set; }
    }

    public class UpdateTestingResponse
    {
        public string Message { get; set; }
    }
}
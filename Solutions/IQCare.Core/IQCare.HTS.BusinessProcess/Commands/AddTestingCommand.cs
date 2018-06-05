using System;
using System.Collections.Generic;
using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.HTS.BusinessProcess.Commands
{
    public class AddTestingCommand : IRequest<Result<AddTestingResponse>>
    {
        // Testing
        public List<Testing> Testing { get; set; }

        // HtsEncounter Result
        public FinalTestingResult FinalTestingResult { get; set; }

        public int HtsEncounterId { get; set; }
        public int ProviderId { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int ServiceAreaId { get; set; }
    }

    public class Testing
    {
        public int KitId { get; set; }
        public string KitLotNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Outcome { get; set; }
        public int TestRound { get; set; }
    }

    public class FinalTestingResult
    {
        public int FinalResultHiv1 { get; set; }
        public int? FinalResultHiv2 { get; set; }
        public int FinalResult { get; set; }
        public int FinalResultGiven { get; set; }
        public int? CoupleDiscordant { get; set; }
        public string FinalResultsRemarks { get; set; }
        public int AcceptedPartnerListing { get; set; }
        public int? ReasonsDeclinePartnerListing { get; set; }
    }

    public class AddTestingResponse
    {
        public bool AddedSuccessfully { get; set; }
    }
}
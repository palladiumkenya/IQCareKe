using System;
using IQCare.Common.Core.Models;
using MediatR;

namespace IQCare.HTS.BusinessProcess.Commands
{
    public class AddPnsScreeningCommand : IRequest<Result<AddPnsScreeningResponse>>
    {
        public DateTime TyScreeningDatepe { get; set; }
        public int PnsAccepted { get; set; }
        public int IpvScreeningDone { get; set; }

        public int PartnerPhysicallyHurt { get; set; }

        public int PartnerThreatenedHurt { get; set; }

        public int ForcedSexualUncomfortable { get; set; }

        public int IpvOutcome { get; set; }

        public int PnsRelationship { get; set; }

        public int LivingWithClient { get; set; }

        public int HivStatus { get; set; }

        public int PnsApproach { get; set; }

        public int EligibleTesting { get; set; }

        public DateTime BookingDate { get; set; }
    }

    public class AddPnsScreeningResponse
    {
        public int ScreeningId { get; set; }
    }
}
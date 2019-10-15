using System.Collections.Generic;
using IQCare.Library;
using IQCare.PMTCT.Core.Models.HEI;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.HeiDeliveryMaternalHistory
{
    public class GetHeiDeliveryCommand : IRequest<Result<List<HEIEncounter>>>
    {
        public int PatientId { get; set; }
    }
}
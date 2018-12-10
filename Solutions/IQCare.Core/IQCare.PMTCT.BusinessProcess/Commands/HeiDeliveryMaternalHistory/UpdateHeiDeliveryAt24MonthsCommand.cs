using IQCare.Library;
using IQCare.PMTCT.Core.Models.HEI;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.HeiDeliveryMaternalHistory
{
    public class UpdateHeiDeliveryAt24MonthsCommand : IRequest<Result<HEIEncounter>>
    {
        public int HeiEncounterId { get; set; }
        public int OutcomeAt24MonthsId { get; set; }
    }
}
using IQCare.Library;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.HeiDeliveryMaternalHistory
{
    public class HEIDeliveryCommand : IRequest<Result<AddHeiDeliveryCommandResponse>>
    {
        public int PlaceOfDelivery { get; set; }
        public int ModeOfDelivery { get; set; }
        public float BirthWeight { get; set; }
        public int ProphylaxisReceived { get; set; }
        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
    }

    public class AddHeiDeliveryCommandResponse
    {
        public int HeiEncounterId { get; set; }
    }
}
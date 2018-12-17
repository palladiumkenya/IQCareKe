using IQCare.Library;
using MediatR;

namespace IQCare.PMTCT.BusinessProcess.Commands.HeiDeliveryMaternalHistory
{
    public class UpdateHeiDeliveryCommand : IRequest<Result<UpdateHeiDeliveryResponse>>
    {
        public int Id { get; set; }
        public int PlaceOfDeliveryId { get; set; }
        public int ModeOfDeliveryId { get; set; }
        public double BirthWeight { get; set; }
        public int ArvProphylaxisId { get; set; }
        public string ArvProphylaxisOther { get; set; }
        public bool MotherIsRegistered { get; set; }
        public int MotherArtInfantEnrolRegimenId { get; set; }
        public int? MotherPersonId { get; set; }
        public int MotherStatusId { get; set; }
        public int PrimaryCareGiverID { get; set; }
        public string MotherName { get; set; }
        public string MotherCCCNumber { get; set; }
        public int MotherPMTCTDrugsId { get; set; }
        public int? MotherPMTCTRegimenId { get; set; }
        public string MotherPMTCTRegimenOther { get; set; }
        public int MotherArtInfantEnrolId { get; set; }
    }

    public class UpdateHeiDeliveryResponse
    {
        public string Message { get; set; }
    }
}
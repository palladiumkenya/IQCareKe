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
        public string ProphylaxisReceivedOther { get; set; }

        public bool MotherIsRegistered { get; set; }
        public int? MotherPersonId { get; set; }
        public int MotherStatusId { get; set; }
        public int PrimaryCareGiverID { get; set; }
        public string MotherName { get; set; }
        public string MotherCCCNumber { get; set; }
        public int MotherPMTCTDrugsId { get; set; }
        public int? MotherPMTCTRegimenId { get; set; }
        public string MotherPMTCTRegimenOther { get; set; }
        public int MotherArtInfantEnrolId { get; set; }
        public int MotherArtInfantEnrolRegimenId { get; set; }

        public int PatientId { get; set; }
        public int PatientMasterVisitId { get; set; }
        public int CreatedBy { get; set; }
    }

    public class AddHeiDeliveryCommandResponse
    {
        public int HeiEncounterId { get; set; }
    }
}
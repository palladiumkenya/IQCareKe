using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.HeiDeliveryMaternalHistory;
using IQCare.PMTCT.Core.Models.HEI;
using IQCare.PMTCT.Infrastructure;
using IQCare.PMTCT.Services;
using MediatR;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.HeiDeliveryMaternalHistory
{
    public class HEIDeliveryCommandHandler: IRequestHandler<HEIDeliveryCommand, Result<AddHeiDeliveryCommandResponse>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public HEIDeliveryCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<AddHeiDeliveryCommandResponse>> Handle(HEIDeliveryCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    HEIEncounterService heiEncounterService = new HEIEncounterService(_unitOfWork);
                    HEIEncounter heiEncounter = new HEIEncounter()
                    {
                        PatientId = request.PatientId,
                        PatientMasterVisitId = request.PatientMasterVisitId,
                        PlaceOfDeliveryId = request.PlaceOfDelivery,
                        ModeOfDeliveryId = request.ModeOfDelivery,
                        BirthWeight = request.BirthWeight,
                        ArvProphylaxisId = request.ProphylaxisReceived,
                        ArvProphylaxisOther = request.ProphylaxisReceivedOther,
                        MotherRegisteredId = request.MotherIsRegistered,
                        MotherPersonId = request.MotherPersonId,
                        MotherStatusId = request.MotherStatusId,
                        PrimaryCareGiverID = request.PrimaryCareGiverID,
                        MotherName = request.MotherName,
                        MotherCCCNumber = request.MotherCCCNumber,
                        MotherPMTCTDrugsId = request.MotherPMTCTDrugsId,
                        MotherPMTCTRegimenId = request.MotherPMTCTRegimenId,
                        MotherPMTCTRegimenOther = request.MotherPMTCTRegimenOther,
                        MotherArtInfantEnrolId = request.MotherArtInfantEnrolId,
                        MotherArtInfantEnrolRegimenId = request.MotherArtInfantEnrolRegimenId,
                        DeleteFlag = false,
                        CreateDate = DateTime.Now,
                        CreatedBy = request.CreatedBy,
                    };

                    HEIEncounter heiEncounterResult = await heiEncounterService.AddHeiEncounter(heiEncounter);

                    return Result<AddHeiDeliveryCommandResponse>.Valid(new AddHeiDeliveryCommandResponse()
                    {
                        HeiEncounterId = heiEncounterResult.Id
                    });
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<AddHeiDeliveryCommandResponse>.Invalid(e.Message);
                }
            }
        }
    }
}
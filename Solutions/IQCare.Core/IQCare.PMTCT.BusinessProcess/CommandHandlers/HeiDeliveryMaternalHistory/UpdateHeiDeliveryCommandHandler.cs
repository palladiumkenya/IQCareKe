using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.HeiDeliveryMaternalHistory;
using IQCare.PMTCT.Core.Models.HEI;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.HeiDeliveryMaternalHistory
{
    public class UpdateHeiDeliveryCommandHandler : IRequestHandler<UpdateHeiDeliveryCommand, Result<UpdateHeiDeliveryResponse>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;
        public UpdateHeiDeliveryCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<UpdateHeiDeliveryResponse>> Handle(UpdateHeiDeliveryCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var heiEncounter = await _unitOfWork.Repository<HEIEncounter>().FindByIdAsync(request.Id);
                    if (heiEncounter != null)
                    {
                        heiEncounter.PlaceOfDeliveryId = request.PlaceOfDeliveryId;
                        heiEncounter.ModeOfDeliveryId = request.ModeOfDeliveryId;
                        heiEncounter.BirthWeight = request.BirthWeight;
                        heiEncounter.ArvProphylaxisId = request.ArvProphylaxisId;
                        heiEncounter.ArvProphylaxisOther = request.ArvProphylaxisOther;
                        heiEncounter.MotherRegisteredId = request.MotherIsRegistered;
                        heiEncounter.MotherPersonId = request.MotherPersonId;
                        heiEncounter.MotherStatusId = request.MotherStatusId;
                        heiEncounter.PrimaryCareGiverID = request.PrimaryCareGiverID;
                        heiEncounter.MotherName = request.MotherName;
                        heiEncounter.MotherCCCNumber = request.MotherCCCNumber;
                        heiEncounter.MotherPMTCTDrugsId = request.MotherPMTCTDrugsId;
                        heiEncounter.MotherPMTCTRegimenId = request.MotherPMTCTRegimenId;
                        heiEncounter.MotherPMTCTRegimenOther = request.MotherPMTCTRegimenOther;
                        heiEncounter.MotherArtInfantEnrolId = request.MotherArtInfantEnrolId;
                        heiEncounter.MotherArtInfantEnrolRegimenId = request.MotherArtInfantEnrolRegimenId;

                        _unitOfWork.Repository<HEIEncounter>().Update(heiEncounter);
                        await _unitOfWork.SaveAsync();

                        return Result<UpdateHeiDeliveryResponse>.Valid(new UpdateHeiDeliveryResponse()
                        {
                            Message = "Successfully updated heidelivery"
                        });
                    }
                    else
                    {
                        return Result<UpdateHeiDeliveryResponse>.Invalid($"Could not find heiencounter for Id: {request.Id}");
                    }
                }
                catch (Exception e)
                {
                    Log.Error($"Error updating heidelivery for Id: {request.Id}, Message: {e.Message}");
                    return Result<UpdateHeiDeliveryResponse>.Invalid($"Error updating heidelivery for Id: {request.Id}, Message: {e.Message}");
                }
            }
        }
    }
}
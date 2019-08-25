using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.HTS.BusinessProcess.Commands;
using IQCare.HTS.Core.Model;
using IQCare.HTS.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.HTS.BusinessProcess.CommandHandlers
{
    public class UpdateTestingCommandHandler : IRequestHandler<UpdateTestingCommand, Result<UpdateTestingResponse>>
    {
        private readonly IHTSUnitOfWork _unitOfWork;

        public UpdateTestingCommandHandler(IHTSUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<UpdateTestingResponse>> Handle(UpdateTestingCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var htsEncounter = await _unitOfWork.Repository<HtsEncounter>().FindByIdAsync(request.HtsEncounterId);
                    if (htsEncounter != null)
                    {
                        htsEncounter.CoupleDiscordant = request.CoupleDiscordant;
                        htsEncounter.FinalResultGiven = request.FinalResultGiven;

                        _unitOfWork.Repository<HtsEncounter>().Update(htsEncounter);
                        await _unitOfWork.SaveAsync();

                        var htsEncounterResults = await _unitOfWork.Repository<HtsEncounterResult>()
                            .Get(x => x.HtsEncounterId == request.HtsEncounterId).ToListAsync();

                        if (htsEncounterResults.Count > 0)
                        {
                            htsEncounterResults[0].RoundOneTestResult = request.RoundOneTestResult;
                            htsEncounterResults[0].RoundTwoTestResult = request.RoundTwoTestResult;
                            htsEncounterResults[0].FinalResult = request.FinalResult;
                            htsEncounterResults[0].EncounterResultRemarks = request.FinalResultsRemarks;

                            _unitOfWork.Repository<HtsEncounterResult>().Update(htsEncounterResults[0]);
                            await _unitOfWork.SaveAsync();
                        }
                        else
                        {
                            HtsEncounterResult htsEncounterResult = new HtsEncounterResult()
                            {
                                EncounterResultRemarks = request.FinalResultsRemarks,
                                FinalResult = request.FinalResult,
                                HtsEncounterId = request.HtsEncounterId,
                                RoundOneTestResult = request.RoundOneTestResult,
                                RoundTwoTestResult = request.RoundTwoTestResult
                            };
                            await _unitOfWork.Repository<HtsEncounterResult>().AddAsync(htsEncounterResult);
                            await _unitOfWork.SaveAsync();
                        }

                        return Result<UpdateTestingResponse>.Valid(new UpdateTestingResponse()
                        {
                            Message = "Successfully updated HtsEncounter"
                        });
                    }
                    else
                    {
                        return Result<UpdateTestingResponse>.Invalid($"Could not find Encounter with Id: {request.HtsEncounterId}");
                    }
                }
                catch (Exception e)
                {
                    Log.Error($"Error updating HIV Testing for HtsEncounterId: {request.HtsEncounterId}", e);
                    return Result<UpdateTestingResponse>.Invalid(e.Message);
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.HTS.BusinessProcess.Commands;
using IQCare.HTS.Core.Model;
using IQCare.HTS.Infrastructure;
using IQCare.Library;
using MediatR;

namespace IQCare.HTS.BusinessProcess.CommandHandlers
{
    public class AddTestingCommandHandler : IRequestHandler<AddTestingCommand, Result<AddTestingResponse>>
    {
        private readonly IHTSUnitOfWork _unitOfWork;
        public AddTestingCommandHandler(IHTSUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<Result<AddTestingResponse>> Handle(AddTestingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (_unitOfWork)
                {
                    HtsEncounter encounter = await _unitOfWork.Repository<HtsEncounter>().FindByIdAsync(request.HtsEncounterId);
                    encounter.CoupleDiscordant = request.FinalTestingResult.CoupleDiscordant;
                    encounter.FinalResultGiven = request.FinalTestingResult.FinalResultGiven;

                    _unitOfWork.Repository<HtsEncounter>().Update(encounter);
                    await _unitOfWork.SaveAsync();

                    // Create Testing instances
                    List<Core.Model.Testing> testings = new List<Core.Model.Testing>();
                    request.Testing.ForEach(t => testings.Add(new Core.Model.Testing
                    {
                        ExpiryDate = t.ExpiryDate,
                        HtsEncounterId = request.HtsEncounterId,
                        KitId = t.KitId,
                        KitLotNumber = t.KitLotNumber,
                        Outcome = t.Outcome,
                        ProviderId = request.ProviderId,
                        TestRound = t.TestRound,
                        CreateDate = DateTime.Now,
                        DeleteFlag = false
                    }));

                    await _unitOfWork.Repository<Core.Model.Testing>().AddRangeAsync(testings);
                    await _unitOfWork.SaveAsync();


                    // Create HtsEncounterResult instance
                    HtsEncounterResult hTSEncounterResult = new HtsEncounterResult
                    {
                        FinalResult = request.FinalTestingResult.FinalResult,
                        HtsEncounterId = request.HtsEncounterId,
                        RoundOneTestResult = request.FinalTestingResult.FinalResultHiv1,
                        RoundTwoTestResult = request.FinalTestingResult.FinalResultHiv2,
                        EncounterResultRemarks = request.FinalTestingResult.FinalResultsRemarks
                    };

                    await _unitOfWork.Repository<HtsEncounterResult>().AddAsync(hTSEncounterResult);
                    await _unitOfWork.SaveAsync();

                    _unitOfWork.Dispose();

                    return Result<AddTestingResponse>.Valid(new AddTestingResponse()
                    {
                        AddedSuccessfully = true
                    });
                }
            }
            catch (Exception e)
            {
                return Result<AddTestingResponse>.Invalid(e.Message);
            }
        }
    }
}
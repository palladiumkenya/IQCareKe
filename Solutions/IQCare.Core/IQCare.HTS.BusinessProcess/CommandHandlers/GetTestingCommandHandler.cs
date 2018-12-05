using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.HTS.BusinessProcess.Commands;
using IQCare.HTS.Core.Model;
using IQCare.HTS.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Testing = IQCare.HTS.Core.Model.Testing;

namespace IQCare.HTS.BusinessProcess.CommandHandlers
{
    public class GetTestingCommandHandler : IRequestHandler<GetTestingCommand, Result<GetTestingResultsResponse>>
    {
        private readonly IHTSUnitOfWork _unitOfWork;

        public GetTestingCommandHandler(IHTSUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<GetTestingResultsResponse>> Handle(GetTestingCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    List<Core.Model.Testing> testings = new List<Testing>();
                    List<HtsEncounterResult> encounterResults = new List<HtsEncounterResult>();

                    var encounter = await _unitOfWork.Repository<HtsEncounter>()
                        .Get(x => x.PatientEncounterID == request.PatientEncounterId).ToListAsync();
                    

                    if (encounter.Count > 0)
                    {
                        testings = await _unitOfWork.Repository<Core.Model.Testing>().Get(x => x.HtsEncounterId == encounter[0].Id)
                            .ToListAsync();
                        encounterResults = await _unitOfWork.Repository<HtsEncounterResult>()
                            .Get(x => x.HtsEncounterId == encounter[0].Id).ToListAsync();
                    }

                    return Result<GetTestingResultsResponse>.Valid(new GetTestingResultsResponse()
                    {
                        Encounter = encounter,
                        Testing = testings,
                        EncounterResults = encounterResults
                    });
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message + " " + ex.InnerException);
                    return Result<GetTestingResultsResponse>.Invalid(ex.Message);
                }
            }
        }
    }
}
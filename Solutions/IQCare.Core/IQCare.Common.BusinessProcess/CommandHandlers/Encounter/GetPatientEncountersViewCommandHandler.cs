using IQCare.Common.BusinessProcess.Commands.Encounter;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Encounter
{
    public class GetPatientEncountersViewCommandHandler : IRequestHandler<GetPatientEncounterCommand, Result<List<PatientEncounterView>>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public GetPatientEncountersViewCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<PatientEncounterView>>> Handle(GetPatientEncounterCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    List<PatientEncounterView> patientEncounterViews = await _unitOfWork
                        .Repository<PatientEncounterView>().Get(x => x.PatientId == request.PatientId && x.EncounterTypeId==request.EncounterTypeId && !x.DeleteFlag)
                        .ToListAsync();
                    return Result<List<PatientEncounterView>>.Valid(patientEncounterViews);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<List<PatientEncounterView>>.Invalid(e.Message);
                }
            }
        }
    }
}
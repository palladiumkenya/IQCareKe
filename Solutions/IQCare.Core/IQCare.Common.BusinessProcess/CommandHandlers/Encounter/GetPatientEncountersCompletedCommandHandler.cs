using IQCare.Common.BusinessProcess.Commands.Encounter;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Encounter
{
    public class GetPatientEncountersCompletedCommandHandler : IRequestHandler<GetPatientEncountersCompletedCommand, Result<List<PatientEncountersCompletedPoco>>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public GetPatientEncountersCompletedCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<List<PatientEncountersCompletedPoco>>> Handle(GetPatientEncountersCompletedCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append($"select Id, PatientId, EncounterType = (SELECT ItemName FROM LookupItemView WHERE ItemId = EncounterTypeId AND MasterName = 'EncounterType'), PatientMasterVisitId, EncounterStartTime from PatientEncounter WHERE PatientId = {request.PatientId}  ORDER BY Id ASC;");
                    var patientEncountersList = await _unitOfWork.Context.Query<PatientEncountersCompletedPoco>().FromSql(sql.ToString()).ToListAsync();
                    return Result<List<PatientEncountersCompletedPoco>>.Valid(patientEncountersList);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, $"An error occured while trying to fetch patient encounters");
                    return Result<List<PatientEncountersCompletedPoco>>.Invalid($"An error occured while trying to fetch patient encounters");
                }
            }
        }
    }
}

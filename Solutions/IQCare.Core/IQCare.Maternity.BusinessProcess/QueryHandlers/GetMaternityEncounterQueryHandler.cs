using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.Maternity.BusinessProcess.Queries.Maternity;
using IQCare.Maternity.Core.Domain.Maternity;
using IQCare.Maternity.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IQCare.Maternity.BusinessProcess.QueryHandlers
{
    public class GetMaternityEncounterQueryHandler: IRequestHandler<GetMaternityEncountersQuery, Result<List<MaternityEncounter>>>
    {
        IMaternityUnitOfWork _unitOfWork;

        public GetMaternityEncounterQueryHandler(IMaternityUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<MaternityEncounter>>> Handle(GetMaternityEncountersQuery request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    List<MaternityEncounter> maternityEncounters = await _unitOfWork.Repository<MaternityEncounter>()
                        .Get(x => x.PatientId == request.PatientId).ToListAsync();
                    return Result<List<MaternityEncounter>>.Valid(maternityEncounters);
                }
                catch (Exception ex)
                {
                    //Logger.Error(ex,
                    //    $"An error occured while fetching maternity Encounters info for patientId{request.PatientId}");
                    return Result<List<MaternityEncounter>>.Invalid(ex.Message);
                }
            }
        }
    }
}
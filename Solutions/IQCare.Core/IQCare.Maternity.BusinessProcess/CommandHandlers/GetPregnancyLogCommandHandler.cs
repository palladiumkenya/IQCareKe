using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IQCare.Library;
using IQCare.Maternity.BusinessProcess.Commands.Maternity;
using IQCare.Maternity.Core.Domain.Maternity;
using IQCare.Maternity.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Maternity.BusinessProcess.CommandHandlers
{
    public class GetPregnancyLogCommandHandler : IRequestHandler<GetPregnancyLogCommand, Result<List<PregnancyLog>>>
    {
        IMaternityUnitOfWork _maternityUnitOfWork;
        IMapper _mapper;

        public GetPregnancyLogCommandHandler(IMaternityUnitOfWork maternityUnitOfWork, IMapper mapper)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<PregnancyLog>>> Handle(GetPregnancyLogCommand request, CancellationToken cancellationToken)
        {
            using (_maternityUnitOfWork)
            {
                try
                {
                    var result = await _maternityUnitOfWork.Repository<PregnancyLog>().Get(x =>
                        x.PatientId == request.PatientId &&
                        x.PatientMasterVisitId == request.PatientMasterVisitId).ToListAsync();

                    return Result<List<PregnancyLog>>.Valid(result);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, $"An error occured while tryng to fetch patientPregnancyLog for patientId: {request.PatientId} patientMasterVisitId {request.PatientMasterVisitId}");
                    return Result<List<PregnancyLog>>.Invalid($"An error occured while tryng to fetch patientPregnancyLog for patientId: {request.PatientId} patientMasterVisitId {request.PatientMasterVisitId}");
                }
            }
        }
    }
}
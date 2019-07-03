using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IQCare.Library;
using IQCare.Prep.BusinessProcess.Commands;
using IQCare.Prep.Core.Models;
using IQCare.Prep.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Prep.BusinessProcess.CommandHandlers
{
    public class GetPrepStatusCommandHandler : IRequestHandler<GetPrepStatusCommand, Result<List<PatientPrEPStatus>>>
    {
        private readonly IPrepUnitOfWork _prepUnitOfWork;
        private readonly IMapper _mapper;

        public GetPrepStatusCommandHandler(IPrepUnitOfWork prepUnitOfWork, IMapper mapper)
        {
            _prepUnitOfWork = prepUnitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<PatientPrEPStatus>>> Handle(GetPrepStatusCommand request, CancellationToken cancellationToken)
        {
            using (_prepUnitOfWork)
            {
                try
                {
                    var prepstatus = await _prepUnitOfWork.Repository<PatientPrEPStatus>().Get(x =>
                        x.PatientId == request.PatientId && x.PatientEncounterId == request.PatientEncounterId &&
                        x.DeleteFlag == false).ToListAsync();

                    return Result<List<PatientPrEPStatus>>.Valid(prepstatus);
                }
                catch (Exception ex)
                {
                    Log.Error($"An error occured while trying to get prep status for PatientId: {request.PatientId} and PatientEncounterId: {request.PatientEncounterId}, exception: {ex.Message} {ex.InnerException}");
                    return Result<List<PatientPrEPStatus>>.Invalid($"An error occured while trying to get prep status for PatientId: {request.PatientId} and PatientEncounterId: {request.PatientEncounterId}");
                }
            }
        }
    }
}
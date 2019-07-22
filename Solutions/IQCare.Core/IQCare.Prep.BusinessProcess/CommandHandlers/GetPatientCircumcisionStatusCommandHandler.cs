using System;
using System.Linq;
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
    public class GetPatientCircumcisionStatusCommandHandler : IRequestHandler<GetPatientCircumcisionStatusCommand, Result<PatientCircumcisionStatus>>
    {
        private readonly IPrepUnitOfWork _prepUnitOfWork;
        private readonly IMapper _mapper;

        public GetPatientCircumcisionStatusCommandHandler(IPrepUnitOfWork prepUnitOfWork, IMapper mapper)
        {
            _prepUnitOfWork = prepUnitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PatientCircumcisionStatus>> Handle(GetPatientCircumcisionStatusCommand request, CancellationToken cancellationToken)
        {
            using (_prepUnitOfWork)
            {
                try
                {
                    var result = await _prepUnitOfWork.Repository<PatientCircumcisionStatus>()
                        .Get(x => x.PatientId == request.PatientId && x.DeleteFlag == false)
                        .OrderByDescending(x => x.Id).FirstOrDefaultAsync();

                    return Result<PatientCircumcisionStatus>.Valid(result);
                }
                catch (Exception e)
                {
                    Log.Error($"Could not fetch client circumcision status Exception: {e.Message}, InnerException {e.InnerException}");
                    return Result<PatientCircumcisionStatus>.Invalid($"Could not fetch client circumcision status exception: {e.Message}");
                }
            }
        }
    }
}
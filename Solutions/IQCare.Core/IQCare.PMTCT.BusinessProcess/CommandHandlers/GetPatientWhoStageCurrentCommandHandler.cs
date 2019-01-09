using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers
{
    public class GetPatientWhoStageCurrentCommandHandler : IRequestHandler<GetPatientWhoStageCurrentCommand, Result<PatientWhoStage>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public GetPatientWhoStageCurrentCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
        }

        public async Task<Result<PatientWhoStage>> Handle(GetPatientWhoStageCurrentCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    PatientWhoStage patientWhoStage = await _unitOfWork.Repository<PatientWhoStage>().Get(x => x.PatientId == request.PatientId).OrderByDescending(x => x.Id)
                        .FirstOrDefaultAsync();
                    return Result<PatientWhoStage>.Valid(patientWhoStage);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Library.Result<PatientWhoStage>.Invalid(e.Message);
                }
            }
        }
    }
}
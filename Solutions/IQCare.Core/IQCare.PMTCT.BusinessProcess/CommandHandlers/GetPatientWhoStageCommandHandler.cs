using System;
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
    public class GetPatientWhoStageCommandHandler: IRequestHandler<GetPatientWhoStageCommand, Result<PatientWhoStage>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public GetPatientWhoStageCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
        }

        public async Task<Result<PatientWhoStage>> Handle(GetPatientWhoStageCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    PatientWhoStage patientWhoStage = await _unitOfWork.Repository<PatientWhoStage>().Get(x => x.PatientId == request.PatientId && x.PatientMasterVisitId==request.PatientMasterVisitId)
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
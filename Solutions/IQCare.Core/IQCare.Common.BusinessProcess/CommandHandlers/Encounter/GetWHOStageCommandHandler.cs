using IQCare.Common.BusinessProcess.Commands.Encounter;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Encounter
{
   public class GetWHOStageCommandHandler:IRequestHandler<GetWHOStageCommand,Result<PatientWHOStage>>
    {
        private ICommonUnitOfWork _unitOfWork;

        public GetWHOStageCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PatientWHOStage>> Handle(GetWHOStageCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var result = await _unitOfWork.Repository<PatientWHOStage>().FindAsync(x => x.PatientId == request.PatientId && x.Id == request.Id);
                    return Result<PatientWHOStage>.Valid(result);
                }
                catch (Exception e)
                {

                    Log.Error(e.Message);
                    return Result<PatientWHOStage>.Invalid(e.Message);
                }
            }
        }
    }
}

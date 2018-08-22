using IQCare.Common.BusinessProcess.Commands.Encounter;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using MediatR;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Encounter
{
    public class UpdateWHOStageCommandHandler : IRequestHandler<EditWHOStageCommand, Result<EditWHOStageCommandResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public UpdateWHOStageCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<EditWHOStageCommandResponse>> Handle(EditWHOStageCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    PatientWHOStage patientWHOStage = await _unitOfWork.Repository<PatientWHOStage>().FindAsync(x => x.PatientId == request.PatientWHOStage.PatientId && x.Id == request.PatientWHOStage.Id);
                    if (patientWHOStage != null)
                    {
                        patientWHOStage.WHOStage = request.PatientWHOStage.WHOStage;
                    }
                    int result = await _unitOfWork.SaveChangesAsync();

                    _unitOfWork.Dispose();
                    return Result<EditWHOStageCommandResponse>.Valid(new EditWHOStageCommandResponse()
                    {
                       WHOStageId  = result

                    });
                }
                catch (Exception e)
                {

                    Log.Error(e.Message);
                    return Result<EditWHOStageCommandResponse>.Invalid(e.Message);
                }
            }
        }
    }
}

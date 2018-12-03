using IQCare.Common.BusinessProcess.Commands.Encounter;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using MediatR;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Encounter
{
    public class AddWHOStageCommandHandler : IRequestHandler<AddWHOStageCommand, Result<AddWHOStageCommandResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public AddWHOStageCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<AddWHOStageCommandResponse>> Handle(AddWHOStageCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    PatientWHOStage patientWHOStage = new PatientWHOStage()
                    {
                        PatientId=request.PatientWHOStage.PatientId,
                        PatientMasterVisitId=request.PatientWHOStage.PatientMasterVisitId,
                        WHOStage=request.PatientWHOStage.WHOStage
                    };

                    await _unitOfWork.Repository<PatientWHOStage>().AddAsync(patientWHOStage);
                    await _unitOfWork.SaveAsync();

                    _unitOfWork.Dispose();

                    return Result<AddWHOStageCommandResponse>.Valid(new AddWHOStageCommandResponse()
                    {

                        WhoStageId = patientWHOStage.Id
                    });

                }
                catch (Exception e)
                {

                   Log.Error(e.Message);
                    return Result<AddWHOStageCommandResponse>.Invalid(e.Message);
                }
            }
        }
    }
}

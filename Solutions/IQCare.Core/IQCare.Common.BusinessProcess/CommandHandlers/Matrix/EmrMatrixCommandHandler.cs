using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands;
using IQCare.Common.BusinessProcess.Commands.Matrix;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using Serilog.Core;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Matrix
{
    public class EmrMatrixCommandHandler : IRequestHandler<MatrixCommand, Result<EmrMatrixResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public EmrMatrixCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<EmrMatrixResponse>> Handle(MatrixCommand request, CancellationToken cancellationToken)
        {
                try
                {
                    EmrMatrixService emrMatrixService = new EmrMatrixService(_unitOfWork);
                    EmrMatrix emrMatrix = await emrMatrixService.GetCurrentEmrMatrix();
                    return Result<EmrMatrixResponse>.Valid(new EmrMatrixResponse(){EmrName = emrMatrix.EmrName,EmrVersion = emrMatrix.EmrVersion, LastMoH731RunDate = emrMatrix.LastMoH731RunDate, LastLoginDate = emrMatrix.LastLoginDate});
                }
                catch (Exception e)
                {
                  //  Logger.Error($"An error occured while getting patient allergies method");
                    return Result<EmrMatrixResponse>.Invalid(e.Message);
                }
        }
    }
}
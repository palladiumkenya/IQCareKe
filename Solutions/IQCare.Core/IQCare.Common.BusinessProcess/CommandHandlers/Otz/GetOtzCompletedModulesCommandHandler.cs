using IQCare.Common.BusinessProcess.Commands.Otz;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Otz
{
    class GetOtzCompletedModulesCommandHandler : IRequestHandler<GetOtzCompletedModulesCommand, Result<List<OtzCompletedModulesView>>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public GetOtzCompletedModulesCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<List<OtzCompletedModulesView>>> Handle(GetOtzCompletedModulesCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var result = await _unitOfWork.Repository<OtzCompletedModulesView>().Get(x => x.PatientId == request.PatientId).ToListAsync();
                    return Result<List<OtzCompletedModulesView>>.Valid(result);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, $"An error occured while trying to fetching otz completed modules");
                    return Result<List<OtzCompletedModulesView>>.Invalid(ex.Message);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Setup;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Serilog;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Setup
{
    public class GetIqcareVersionCommandHandler : IRequestHandler<GetIqcareVersionCommand, Result<List<AppAdmin>>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public GetIqcareVersionCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<List<AppAdmin>>> Handle(GetIqcareVersionCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var result = await _unitOfWork.Repository<AppAdmin>().GetAllAsync();
                    return Result<List<AppAdmin>>.Valid(result.ToList());
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    return Result<List<AppAdmin>>.Invalid(ex.Message);
                }
            }
        }
    }
}
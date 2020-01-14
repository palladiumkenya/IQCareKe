using IQCare.Common.BusinessProcess.Commands.Matrix;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Matrix
{
    public class GetILMessageStatsCommandHandler : IRequestHandler<GetILMessageStatsCommand, Result<List<ILMessageStats>>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public GetILMessageStatsCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<List<ILMessageStats>>> Handle(GetILMessageStatsCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var result = await _unitOfWork.Repository<ILMessageStats>().GetAllAsync();
                    return Result<List<ILMessageStats>>.Valid(result.ToList());
                }
                catch (Exception ex)
                {
                    Log.Error(ex, $"An error occured while fetching facility Il message stats");
                    return Result<List<ILMessageStats>>.Invalid(ex.Message);
                }
            }
        }
    }
}

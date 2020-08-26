using IQCare.Common.BusinessProcess.Commands.Matrix;
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

namespace IQCare.Common.BusinessProcess.CommandHandlers.Matrix
{
    public class GetILStatisticsCommandHandler : IRequestHandler<GetILStatisticsCommand, Result<ILStatisticsResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public GetILStatisticsCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<ILStatisticsResponse>> Handle(GetILStatisticsCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var result = await _unitOfWork.Repository<IlStatistics>().Get(x => x.Id > 0).ToListAsync();
                    return Result<ILStatisticsResponse>.Valid(new ILStatisticsResponse()
                    {
                        Outbox = result[0].Outbox,
                        Inbox = result[0].Inbox
                    });
                }
                catch (Exception ex)
                {
                    Log.Error(ex, $"An error occured while fetching IL statistics");
                    return Result<ILStatisticsResponse>.Invalid(ex.Message);
                }
            }
        }
    }
}

using IQCare.HTS.BusinessProcess.Commands;
using IQCare.HTS.Core.Model;
using IQCare.HTS.Infrastructure;
using IQCare.Library;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.HTS.BusinessProcess.CommandHandlers
{
    class TestingSummaryStatisticsQueryHandler : IRequestHandler<TestingSummaryStatisticsQuery, Result<List<TestingSummaryStatistics>>>
    {
        private readonly IHTSUnitOfWork _unitOfWork;
        public TestingSummaryStatisticsQueryHandler(IHTSUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<List<TestingSummaryStatistics>>> Handle(TestingSummaryStatisticsQuery request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var result = await _unitOfWork.Repository<TestingSummaryStatistics>().GetAllAsync();
                    return Result<List<TestingSummaryStatistics>>.Valid(result.ToList());
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    return Result<List<TestingSummaryStatistics>>.Invalid(ex.Message);
                }
            }
        }
    }
}

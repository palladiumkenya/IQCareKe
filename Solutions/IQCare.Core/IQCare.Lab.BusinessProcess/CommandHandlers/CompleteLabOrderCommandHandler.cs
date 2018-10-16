using IQCare.Lab.BusinessProcess.Commands;
using IQCare.Lab.Core.Models;
using IQCare.Lab.Infrastructure.Interface;
using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Lab.BusinessProcess.CommandHandlers
{
    public class CompleteLabOrderCommandHandler : IRequestHandler<CompleteLabOrderCommand, Result<CompleteLabOrderResponse>>
    {
        ILabUnitOfWork _labUnitOfwork;
        public CompleteLabOrderCommandHandler(ILabUnitOfWork labUnitOfwork)
        {
            _labUnitOfwork = labUnitOfwork;
        }

        public Task<Result<CompleteLabOrderResponse>> Handle(CompleteLabOrderCommand request, CancellationToken cancellationToken)
        {
            var totalParameterCount = _labUnitOfwork.Repository<LabTestParameter>().Get(x => x.LabTestId == request.LabTestId).Count();

            var labOrderTestResults = new List<LabOrderTestResult>();

            foreach (var labTestResult in request.LabTestResults)
            {
                var labOrderTestResult = new LabOrderTestResult(request.LabOrderTestId, request.LabTestId, labTestResult.ParameterId, labTestResult.ResultValue, labTestResult.ResultOptionId, labTestResult.ResultOption, labTestResult.ResultUnit, labTestResult.ResultOptionId, request.UserId, labTestResult.Undetectable, labTestResult.DetectionLimit);

                labOrderTestResults.Add(labOrderTestResult);
            }
            return Task.FromResult(Result<CompleteLabOrderResponse>(new CompleteLabOrderResponse { }));
        }
    }
}

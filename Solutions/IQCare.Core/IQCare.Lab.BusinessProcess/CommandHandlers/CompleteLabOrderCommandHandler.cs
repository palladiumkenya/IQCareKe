using IQCare.Lab.BusinessProcess.Commands;
using IQCare.Lab.Core.Models;
using IQCare.Lab.Infrastructure.Interface;
using IQCare.Library;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Lab.BusinessProcess.CommandHandlers
{
    public class CompleteLabOrderCommandHandler : IRequestHandler<CompleteLabOrderCommand, Result<CompleteLabOrderResponse>>
    {
        private readonly ILabUnitOfWork _labUnitOfwork;
        public CompleteLabOrderCommandHandler(ILabUnitOfWork labUnitOfwork)
        {
            _labUnitOfwork = labUnitOfwork;
        }

        public async Task<Result<CompleteLabOrderResponse>> Handle(CompleteLabOrderCommand request, CancellationToken cancellationToken)
        {
            using (var transaction = _labUnitOfwork.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    var submittedLabOrderTest = _labUnitOfwork.Repository<LabOrderTest>()
                        .Get(x => x.Id == request.LabOrderTestId).FirstOrDefault();
                    if (submittedLabOrderTest == null)
                      return Result<CompleteLabOrderResponse>.Invalid($"Lab order request with Id {request.LabOrderTestId} not found");

                    var labTestParameters = _labUnitOfwork.Repository<LabTestParameter>()
                        .Get(x => x.LabTestId == request.LabTestId && x.DeleteFlag == false).ToList();

                    var totalLabTestParameterCount = labTestParameters.Count;

                    var submittedLabOrderTestResultsCount = _labUnitOfwork.Repository<LabOrderTestResult>()
                        .Get(x => x.LabOrderTestId == request.LabOrderTestId).Count();

                    var labOrderTestResults = new List<LabOrderTestResult>();

                    foreach (var labTestResult in request.LabTestResults)
                    {
                        var resultUnit = GetResultUnitDetails(labTestResult.ParameterId);

                        var labOrderTestResult = new LabOrderTestResult(request.LabOrderId, request.LabOrderTestId, request.LabTestId, labTestResult.ParameterId, labTestResult.ResultValue, labTestResult.ResultOptionId, labTestResult.ResultOption, resultUnit.Item2, resultUnit.Item1, request.UserId, labTestResult.Undetectable, labTestResult.DetectionLimit);

                        labOrderTestResults.Add(labOrderTestResult);
                    }

                    await _labUnitOfwork.Repository<LabOrderTestResult>().AddRangeAsync(labOrderTestResults);

                    // PatientLabTracker is updated only for LabTests with only one parameter count
                    UpdatePatientLabTestTracker(labTestParameters[0].Id, request.LabOrderId, totalLabTestParameterCount, request.LabTestResults[0]);

                    submittedLabOrderTest.ReceiveResult(request.UserId, DateTime.Now);
                    submittedLabOrderTestResultsCount += labOrderTestResults.Count;

                    if(submittedLabOrderTestResultsCount >= totalLabTestParameterCount)
                       submittedLabOrderTest.MarkAsReceived();
                    
                    _labUnitOfwork.Repository<LabOrderTest>().Update(submittedLabOrderTest);

                    await _labUnitOfwork.SaveAsync();

                    var labOrderTestPendingSubmission = _labUnitOfwork.Repository<LabOrderTest>()
                        .Get(x => x.LabOrderId == request.LabOrderId && x.ResultStatus != ResultStatusEnum.Received.ToString()).Any();

                    if(!labOrderTestPendingSubmission)
                    {
                        var labOrder = _labUnitOfwork.Repository<LabOrder>().Get(x => x.Id == request.LabOrderId).SingleOrDefault();
                        labOrder?.CompleteOrder();
                        _labUnitOfwork.Repository<LabOrder>().Update(labOrder);
                       await _labUnitOfwork.SaveAsync();
                    }

                    transaction.Commit();

                    return Result<CompleteLabOrderResponse>.Valid(new CompleteLabOrderResponse { LabOrderId = request.LabOrderId });
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
          
        }

        private Tuple<int?, string> GetResultUnitDetails(int parameterId)

        {
            var unitId = _labUnitOfwork.Repository<LabTestParameterConfig>().Get(x => x.ParameterId == parameterId && x.DeleteFlag == false)
                           .SingleOrDefault()?.UnitId;   
                var parameterUnit = _labUnitOfwork.Repository<LabTestParameterUnit>().Get(x => x.UnitId == unitId)
                    .SingleOrDefault();

                return new Tuple<int?, string>(parameterUnit?.UnitId, parameterUnit?.UnitName);         
        }

        private void UpdatePatientLabTestTracker(int parameterId, int labOrderId, int parameterCount, AddLabTestResultCommand labOrderTestResult)
        {
            if (parameterCount > 1)
                return;

            var resultUnit = GetResultUnitDetails(parameterId);

            var patientLabTracker = _labUnitOfwork.Repository<PatientLabTracker>().Get(x => x.LabOrderId == labOrderId).SingleOrDefault();
            if(patientLabTracker == null)
                return;
            patientLabTracker.UpdateResults(DateTime.Now, labOrderTestResult.ResultText, resultUnit.Item2, labOrderTestResult.ResultValue);
            _labUnitOfwork.Repository<PatientLabTracker>().Update(patientLabTracker);
        }
    }
}

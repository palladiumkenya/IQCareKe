using IQCare.Lab.BusinessProcess.Commands;
using IQCare.Lab.Core.Models;
using IQCare.Lab.Infrastructure.Interface;
using IQCare.Library;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
                    DateTime resultDate = request.DateResultsCollected.HasValue
                        ? request.DateResultsCollected.Value
                        : DateTime.Now;

                    request.LabTestResults = string.IsNullOrEmpty(request.StrLabTestResults)
                        ? request.LabTestResults
                        : BuildLabTestResultCommandCollection(request.StrLabTestResults);

                    var submittedLabOrderTest = _labUnitOfwork.Repository<LabOrderTest>()
                        .Get(x => x.Id == request.LabOrderTestId).FirstOrDefault();
                    if (submittedLabOrderTest == null)
                        return Result<CompleteLabOrderResponse>.Invalid($"Lab order request with Id {request.LabOrderTestId} not found");

                    if (!request.LabTestResults.Any())
                        return Result<CompleteLabOrderResponse>.Invalid($"Submit atleast one lab result to complete the lab order");

                   var labOrderTestResults = new List<LabOrderTestResult>();

                    foreach (var labTestResult in request.LabTestResults)
                    {
                        var parameterConfig = GetParamterConfigDetails(labTestResult.ParameterId);

                        var resultUnit = parameterConfig?.LabTestParameterConfig?.Unit;

                        var resultOption =
                            parameterConfig?.LabTestParameterResultOptions?.SingleOrDefault(x =>
                                x.Id == labTestResult.ResultOptionId)?.Value;

                        var labOrderTestResult = new LabOrderTestResult(request.LabOrderId, request.LabOrderTestId,
                            request.LabTestId, labTestResult.ParameterId, labTestResult.ResultValue,labTestResult.ResultText,
                            labTestResult.ResultOptionId, resultOption, resultUnit?.UnitName, resultUnit?.UnitId,
                            request.UserId, labTestResult.Undetectable, resultDate, labTestResult.DetectionLimit);

                        labOrderTestResults.Add(labOrderTestResult);
                    }


                    await _labUnitOfwork.Repository<LabOrderTestResult>().AddRangeAsync(labOrderTestResults);

                    // PatientLabTracker is updated only for LabTests with only one parameter count
                    var labTestParameters = _labUnitOfwork.Repository<LabTestParameter>()
                        .Get(x => x.LabTestId == request.LabTestId && x.DeleteFlag == false).ToList();

                    var totalLabTestParameterCount = labTestParameters.Count;

                    UpdatePatientLabTestTracker(labTestParameters[0].Id, request.LabOrderId, totalLabTestParameterCount,
                        request.LabOrderTestId, labOrderTestResults.FirstOrDefault());
                    submittedLabOrderTest.ReceiveResult(request.UserId, resultDate);
                     submittedLabOrderTest.MarkAsReceived();

                    _labUnitOfwork.Repository<LabOrderTest>().Update(submittedLabOrderTest);

                    await _labUnitOfwork.SaveAsync();

                    var labOrderTestPendingSubmission = _labUnitOfwork.Repository<LabOrderTest>()
                        .Get(x => x.LabOrderId == request.LabOrderId && x.ResultStatus != ResultStatusEnum.Received.ToString()).Any();

                    if (!labOrderTestPendingSubmission)
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



        private LabTestParameter GetParamterConfigDetails(int parameterId)
        {
            var parameterConfig = _labUnitOfwork.Repository<LabTestParameter>()
                .Get(x => x.Id == parameterId && x.DeleteFlag == false)
                .Include(x => x.LabTestParameterResultOptions)
                .Include(x => x.LabTestParameterConfig.Unit).Distinct().FirstOrDefault();

            return parameterConfig;
        }

        private void UpdatePatientLabTestTracker(int parameterId, int labOrderId, int parameterCount, int labOrderTestId, LabOrderTestResult labOrderTestResult)
        {
            var patientLabTracker = _labUnitOfwork.Repository<PatientLabTracker>()
                .Get(x => x.LabOrderId == labOrderId && x.LabOrderTestId == labOrderTestId).SingleOrDefault();

            if (patientLabTracker == null)
                return;
            if (parameterCount > 1)
            {
                patientLabTracker.SetAsComplete();
            }
            else
            {
                var resultText = labOrderTestResult.Undetectable
                    ? $"Undetectable (Detection limit = {labOrderTestResult.DetectionLimit})": labOrderTestResult.ResultText;

                var parameterConfig = GetParamterConfigDetails(parameterId);
                patientLabTracker.UpdateResults(DateTime.Now, resultText,
                    parameterConfig?.LabTestParameterConfig?.Unit?.UnitName, labOrderTestResult.ResultValue, labOrderTestResult.ResultOption);
            }

            _labUnitOfwork.Repository<PatientLabTracker>().Update(patientLabTracker);
        }

        private List<AddLabTestResultCommand> BuildLabTestResultCommandCollection(string parameterResultJson)
        {
            var labTestResultCommandType = typeof(AddLabTestResultCommand);
            var properties = labTestResultCommandType.GetProperties();


            var paramaterResultDictionary = JsonConvert
                .DeserializeObject<Dictionary<string, string>>(parameterResultJson)
                .Where(x => !string.IsNullOrWhiteSpace(x.Value) &&
                            !x.Value.Equals("No Units", StringComparison.OrdinalIgnoreCase))
                .ToDictionary(r => r.Key, r => r.Value);

            var labTestParameterIds = paramaterResultDictionary.Keys.Select(x => x.Split('_')[1]).Distinct().ToList();

            var labTestResultCommandCollection = new List<AddLabTestResultCommand>();

            foreach (var paramId in labTestParameterIds)
            {
                var testResultCommand = new AddLabTestResultCommand();
                labTestResultCommandType.GetProperty("ParameterId").SetValue(testResultCommand, Convert.ToInt32(paramId));

                var parameterSubmittedValuesDict = paramaterResultDictionary.Where(x => string.Equals(x.Key.Split('_')[1], paramId))
                    .ToDictionary(p => p.Key.Split('_')[0], x => x.Value);

                foreach (var paramDictItem in parameterSubmittedValuesDict)
                {
                    var propertyInfo = properties.SingleOrDefault(x =>
                        x.Name.Equals(paramDictItem.Key, StringComparison.OrdinalIgnoreCase));

                    if (propertyInfo != null)
                    {
                        var typeCode = Type.GetTypeCode(propertyInfo.PropertyType);
                        switch (typeCode)
                        {
                            case TypeCode.Int32:
                                propertyInfo.SetValue(testResultCommand, Convert.ToInt32(paramDictItem.Value));
                                break;
                            case TypeCode.Boolean:
                                propertyInfo.SetValue(testResultCommand, Convert.ToBoolean(paramDictItem.Value));
                                break;
                            case TypeCode.Decimal:
                                propertyInfo.SetValue(testResultCommand, Convert.ToDecimal(paramDictItem.Value));
                                break;

                            case TypeCode.Object:
                                if (propertyInfo.PropertyType == typeof(decimal?))
                                    propertyInfo.SetValue(testResultCommand, decimal.Parse(paramDictItem.Value));
                                else if (propertyInfo.PropertyType == typeof(int?))
                                    propertyInfo.SetValue(testResultCommand, int.Parse(paramDictItem.Value));
                                break;

                            default:
                                propertyInfo.SetValue(testResultCommand, paramDictItem.Value);
                                break;
                        }

                    }
                }

                if (testResultCommand.ResultText != null || testResultCommand.ResultOptionId.HasValue ||
                    testResultCommand.ResultValue.HasValue || testResultCommand.DetectionLimit.HasValue)
                    labTestResultCommandCollection.Add(testResultCommand);
            }

            return labTestResultCommandCollection;
        }
    }
}

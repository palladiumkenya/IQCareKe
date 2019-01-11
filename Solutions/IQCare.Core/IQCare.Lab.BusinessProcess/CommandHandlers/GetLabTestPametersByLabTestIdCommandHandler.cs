using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Lab.BusinessProcess.Commands;
using IQCare.Lab.Core.Models;
using IQCare.Lab.Infrastructure.Interface;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Lab.BusinessProcess.CommandHandlers
{
    public class GetLabTestPametersByLabTestIdCommandHandler : IRequestHandler<GetLabTestPametersByLabTestIdCommand, Result<List<LabTestParamaterViewModel>>>
    {
        private readonly ILabUnitOfWork _labUnitOfWork;

        public GetLabTestPametersByLabTestIdCommandHandler(ILabUnitOfWork labUnitOfWork)
        {
            _labUnitOfWork = labUnitOfWork;
        }

        public async Task<Result<List<LabTestParamaterViewModel>>> Handle(GetLabTestPametersByLabTestIdCommand request, CancellationToken cancellationToken)
        {

            using (_labUnitOfWork)
            {
                try
                {
                    var labTestParams = await _labUnitOfWork.Repository<LabTestParameter>()
                        .Get(x => x.LabTestId == request.LabTestId && x.DeleteFlag == false)
                        .Include(x=>x.LabTestParameterResultOptions)
                        .Include(x=>x.LabTestParameterConfig.Unit)
                        .ToListAsync();


                    var viewModel = labTestParams.Select(x => new LabTestParamaterViewModel
                    {
                        Id = x.Id, LabTestId = x.LabTestId, ParameterName = x.ParameterName,
                        UnitId = x.LabTestParameterConfig != null ? x.LabTestParameterConfig.UnitId :default(int),
                        DataType = x.DataType,
                        UnitName = x.LabTestParameterConfig != null ? x.LabTestParameterConfig.Unit.UnitName : "No Units",
                        ResultOptions = x.LabTestParameterResultOptions.Any()
                            ? x.LabTestParameterResultOptions.Select(p =>
                                new
                                {
                                    Key = p.Id.ToString(),
                                    Value = p.Value.ToString()
                                }).ToList()
                            : null
                    }).ToList();

                    return Result<List<LabTestParamaterViewModel>>.Valid(viewModel);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<List<LabTestParamaterViewModel>>.Invalid(e.Message);
                }
            }
        }
    }
}
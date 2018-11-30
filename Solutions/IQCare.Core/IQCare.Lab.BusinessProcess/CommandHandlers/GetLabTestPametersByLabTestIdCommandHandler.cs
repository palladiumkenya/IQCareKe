using System;
using System.Collections.Generic;
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
    public class GetLabTestPametersByLabTestIdCommandHandler : IRequestHandler<GetLabTestPametersByLabTestIdCommand, Result<List<LabTestParameter>>>
    {
        ILabUnitOfWork _labUnitOfWork;

        public GetLabTestPametersByLabTestIdCommandHandler(ILabUnitOfWork labUnitOfWork)
        {
            _labUnitOfWork = labUnitOfWork;
        }

        public async Task<Result<List<LabTestParameter>>> Handle(GetLabTestPametersByLabTestIdCommand request, CancellationToken cancellationToken)
        {
            using (_labUnitOfWork)
            {
                try
                {
                    var result = await _labUnitOfWork.Repository<LabTestParameter>()
                        .Get(x => x.LabTestId == request.LabTestId && x.DeleteFlag == false).ToListAsync();
                    return Result<List<LabTestParameter>>.Valid(result);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<List<LabTestParameter>>.Invalid(e.Message);
                }
            }
        }
    }
}
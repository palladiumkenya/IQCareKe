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
    public class GetLabTestsCommandHandler : IRequestHandler<GetLabTestsCommand, Result<GetLabTestsResponse>>
    {
        private readonly ILabUnitOfWork _labUnitOfwork;

        public GetLabTestsCommandHandler(ILabUnitOfWork labUnitOfwork)
        {
            _labUnitOfwork = labUnitOfwork;
        }

        public async Task<Result<GetLabTestsResponse>> Handle(GetLabTestsCommand request, CancellationToken cancellationToken)
        {
            using (_labUnitOfwork)
            {
                try
                {
                    var labTestsList = new List<KeyValuePair<string, List<LabTest>>>();
                    for (int i = 0; i < request.LabTests.Length; i++)
                    {
                        var items = await _labUnitOfwork.Repository<LabTest>()
                            .Get(c => c.Name == request.LabTests[i]).OrderByDescending(y => y.Id).ToListAsync();

                        labTestsList.Add(new KeyValuePair<string, List<LabTest>>(request.LabTests[i], items));
                    }

                    return Result<GetLabTestsResponse>.Valid(new GetLabTestsResponse()
                    {
                        LabTestsList = labTestsList
                    });

                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<GetLabTestsResponse>.Invalid(e.Message);
                }
            }
        }
    }
}
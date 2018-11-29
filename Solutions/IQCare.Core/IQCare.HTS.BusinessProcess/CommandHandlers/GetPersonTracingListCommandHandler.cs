using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.HTS.BusinessProcess.Commands;
using IQCare.HTS.Core.Model;
using IQCare.HTS.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.HTS.BusinessProcess.CommandHandlers
{
    public class GetPersonTracingListCommandHandler : IRequestHandler<GetPersonTracingListCommand, Result<List<PersonTracingView>>>
    {
        private readonly ICommonUnitOfWork _commonUnitOfWork;
        public GetPersonTracingListCommandHandler(IHTSUnitOfWork unitOfWork, ICommonUnitOfWork commonUnitOfWork)
        {
            _commonUnitOfWork = commonUnitOfWork ?? throw new ArgumentNullException(nameof(commonUnitOfWork));
        }

        public async Task<Result<List<PersonTracingView>>> Handle(GetPersonTracingListCommand request, CancellationToken cancellationToken)
        {
            using (_commonUnitOfWork)
            {
                try
                {
                    var result = await _commonUnitOfWork.Repository<PersonTracingView>()
                        .Get(x => x.PersonID == request.PersonId && x.DeleteFlag == false).ToListAsync();

                    return Result<List<PersonTracingView>>.Valid(result);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<List<PersonTracingView>>.Invalid(e.Message);
                }
            }
        }
    }
}
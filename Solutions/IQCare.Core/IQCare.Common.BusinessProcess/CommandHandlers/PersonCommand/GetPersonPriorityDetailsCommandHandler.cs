using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.PersonCommand;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Common.BusinessProcess.CommandHandlers.PersonCommand
{
    public class GetPersonPriorityDetailsCommandHandler : IRequestHandler<GetPersonPriorityDetailsCommand, Result<List<PersonPriority>>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public GetPersonPriorityDetailsCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }


        public async Task<Result<List<PersonPriority>>> Handle(GetPersonPriorityDetailsCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var result = await _unitOfWork.Repository<PersonPriority>()
                        .Get(x => x.PersonId == request.PersonId && x.DeleteFlag == false).ToListAsync();

                    return Result<List<PersonPriority>>.Valid(result);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<List<PersonPriority>>.Invalid(e.Message);
                }
            }
        }
    }
}
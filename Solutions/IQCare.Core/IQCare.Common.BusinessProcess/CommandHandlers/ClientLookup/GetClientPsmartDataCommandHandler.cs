using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.ClientLookup;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IQCare.Common.BusinessProcess.CommandHandlers.ClientLookup
{
    public class GetClientPsmartDataCommandHandler : IRequestHandler<GetClientPsmartDataCommand, Result<List<HivTestTracker>>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public GetClientPsmartDataCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<List<HivTestTracker>>> Handle(GetClientPsmartDataCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var result = await _unitOfWork.Repository<HivTestTracker>()
                        .Get(x => x.PersonId == request.PersonId && x.DeleteFlag == false).ToListAsync();

                    return Result<List<HivTestTracker>>.Valid(result);
                }
                catch (Exception e)
                {
                    return Result<List<HivTestTracker>>.Invalid(e.Message);
                }
            }
        }
    }
}
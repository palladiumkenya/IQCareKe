using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Lookup;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Lookup
{
    public class GetOptionsByGroupAndItemNameFilterCommandHandler : IRequestHandler<GetOptionsByGroupAndItemNameFilterCommand, Result<List<LookupItemView>>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public GetOptionsByGroupAndItemNameFilterCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<List<LookupItemView>>> Handle(GetOptionsByGroupAndItemNameFilterCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var results = await _unitOfWork.Repository<LookupItemView>()
                        .Get(x => x.MasterName == request.GroupName && x.ItemName.Contains(request.ItemName))
                        .ToListAsync();

                    return Result<List<LookupItemView>>.Valid(results);
                }
                catch (Exception ex)
                {
                    Log.Error($"Error occured while trying to filter Lookupitems");
                    return Result<List<LookupItemView>>.Invalid($"Error occured while trying to filter Lookupitems");
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Lookup;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Lookup
{
    public class GetAfyaMobileLookupsCommandHandler : IRequestHandler<GetAfyaMobileLookupsCommand, Result<List<LookupItemView>>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public GetAfyaMobileLookupsCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<List<LookupItemView>>> Handle(GetAfyaMobileLookupsCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    List<LookupItemView> lookupItemViews = new List<LookupItemView>();
                    for (int i = 0; i < request.options.Length; i++)
                    {
                        var options = await _unitOfWork.Repository<LookupItemView>()
                            .Get(x => x.MasterName == request.options[i]).ToListAsync();
                        lookupItemViews.AddRange(options);
                    }
                    return Result<List<LookupItemView>>.Valid(lookupItemViews);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<List<LookupItemView>>.Invalid(e.Message);
                }
            }
        }
    }
}
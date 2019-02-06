using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using IQCare.Records.BusinessProcess.Command.Lookup;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using LookupItemView = IQCare.Common.Core.Models.LookupItemView;

namespace IQCare.Records.BusinessProcess.CommandHandlers.Lookup
{
    public class GetOptionsByMasterNameCommandHandler : IRequestHandler<GetOptionsByMasterNameCommand, Result<List<LookupItemView>>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public GetOptionsByMasterNameCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<List<LookupItemView>>> Handle(GetOptionsByMasterNameCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var options = await _unitOfWork.Repository<LookupItemView>()
                        .Get(x => x.MasterName == request.MasterName).ToListAsync();
                    return Result<List<LookupItemView>>.Valid(options);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<List<LookupItemView>>.Invalid(e.Message);
                }
            }
        }
    }
}
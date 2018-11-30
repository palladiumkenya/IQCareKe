using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Lookup;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Lookup
{
    public class GetOptionsByGroupNameCommandHandler : IRequestHandler<GetOptionsByGroupNameCommand, Result<GetOptionsByGroupNameResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public GetOptionsByGroupNameCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<GetOptionsByGroupNameResponse>> Handle(GetOptionsByGroupNameCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var results = await _unitOfWork.Repository<LookupItemView>().Get(x => x.MasterName == request.GroupName)
                    .OrderBy(y => y.OrdRank).ToListAsync();

                _unitOfWork.Dispose();

                return Result<GetOptionsByGroupNameResponse>.Valid(new GetOptionsByGroupNameResponse
                {
                    LookupItems = results
                });
            }
            catch (Exception ex)
            {
                return Result<GetOptionsByGroupNameResponse>.Invalid(ex.Message);
            }
        }
    }
}
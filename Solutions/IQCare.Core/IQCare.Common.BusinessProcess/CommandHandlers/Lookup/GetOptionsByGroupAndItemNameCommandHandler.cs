using System;
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
    public class GetOptionsByGroupAndItemNameCommandHandler : IRequestHandler<GetOptionsByGroupAndItemNameCommand, Result<GetOptionsByGroupAndItemNameResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public GetOptionsByGroupAndItemNameCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<Result<GetOptionsByGroupAndItemNameResponse>> Handle(GetOptionsByGroupAndItemNameCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var results = await _unitOfWork.Repository<LookupItemView>()
                    .Get(x => x.MasterName == request.GroupName && x.ItemName == request.ItemName)
                    .FirstOrDefaultAsync();

                _unitOfWork.Dispose();

                return Result<GetOptionsByGroupAndItemNameResponse>.Valid(
                    new GetOptionsByGroupAndItemNameResponse {ItemId = results.ItemId});
            }
            catch (Exception ex)
            {
                return Result<GetOptionsByGroupAndItemNameResponse>.Invalid(ex.Message);
            }
        }
    }
}
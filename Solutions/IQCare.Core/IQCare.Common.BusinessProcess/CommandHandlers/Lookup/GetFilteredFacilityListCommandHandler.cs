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
    public class GetFilteredFacilityListCommandHandler : IRequestHandler<GetFilteredFacilityListCommand, Result<GetFilteredFacilityListResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public GetFilteredFacilityListCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<GetFilteredFacilityListResponse>> Handle(GetFilteredFacilityListCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (_unitOfWork)
                {
                    var result = await _unitOfWork.Repository<FacilityList>().Get(x => x.Name.ToLower().Contains(request.searchString.ToLower())).ToListAsync();

                    _unitOfWork.Dispose();

                    return Result<GetFilteredFacilityListResponse>.Valid(new GetFilteredFacilityListResponse()
                    {
                        FacilityList = result
                    });
                }
            }
            catch (Exception e)
            {
                return Result<GetFilteredFacilityListResponse>.Invalid(e.Message);
            }
        }
    }
}
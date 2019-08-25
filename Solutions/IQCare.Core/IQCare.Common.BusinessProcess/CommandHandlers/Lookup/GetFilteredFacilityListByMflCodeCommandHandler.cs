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
{  public  class GetFilteredFacilityListByMflCodeCommandHandler : IRequestHandler<GetFilteredFacilityListByMflCodeCommand, Result<GetFilteredFacilityListMflCodeResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public GetFilteredFacilityListByMflCodeCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<GetFilteredFacilityListMflCodeResponse>> Handle(GetFilteredFacilityListByMflCodeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (_unitOfWork)
                {
                    var result = await _unitOfWork.Repository<FacilityList>().Get(x => x.MFLCode.Contains(request.searchString.ToString())).ToListAsync();

                    _unitOfWork.Dispose();

                    return Result<GetFilteredFacilityListMflCodeResponse>.Valid(new GetFilteredFacilityListMflCodeResponse()
                    {
                        FacilityList = result
                    });
                }
            }
            catch (Exception e)
            {
                return Result<GetFilteredFacilityListMflCodeResponse>.Invalid(e.Message);
            }
        }
    }
}

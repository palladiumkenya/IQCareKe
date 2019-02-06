using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Lookup;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Serilog;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Lookup
{
    public class GetFacilityListCommandHandler : IRequestHandler<GetFacilityListCommand, Result<GetFacilityListResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public GetFacilityListCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<GetFacilityListResponse>> Handle(GetFacilityListCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (_unitOfWork)
                {
                    var result = await _unitOfWork.Repository<FacilityList>().GetAllAsync();

                    _unitOfWork.Dispose();

                    return Result<GetFacilityListResponse>.Valid(new GetFacilityListResponse()
                    {
                        FacilityList = result.ToList()
                    });
                }
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return Result<GetFacilityListResponse>.Invalid(e.Message);
            }
        }
    }

    public class GetActiveFaciltyCommandHandler : IRequestHandler<GetActiveFaciltyCommand, Result<FacilityViewModel>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public GetActiveFaciltyCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<Result<FacilityViewModel>> Handle(GetActiveFaciltyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var activeFacility = _unitOfWork.Repository<Facility>().Get(x => x.DeleteFlag == 0).Select(x =>
                       new FacilityViewModel
                       {
                           Id = x.FacilityID,
                           Name = x.FacilityName,
                           PositionId = x.PosID
                       }).SingleOrDefault();

                return Task.FromResult(Result<FacilityViewModel>.Valid(activeFacility));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while getting active facility");
                return Task.FromResult(Result<FacilityViewModel>.Invalid(ex.Message));
            }
        }
    }
}
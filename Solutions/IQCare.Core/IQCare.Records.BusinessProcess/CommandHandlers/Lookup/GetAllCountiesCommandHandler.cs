using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.Infrastructure;
using IQCare.Common.Services;
using IQCare.Library;
using IQCare.Records.BusinessProcess.Command.Lookup;
using MediatR;
using County = IQCare.Common.Core.Models.County;

namespace IQCare.Records.BusinessProcess.CommandHandlers.Lookup
{
    public class GetAllCountiesCommandHandler : IRequestHandler<GetAllCountiesCommand, Result<List<County>>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public GetAllCountiesCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<List<County>>> Handle(GetAllCountiesCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    CountyService countyService = new CountyService(_unitOfWork);
                    var counties = await countyService.GetCounties();
                    return Result<List<County>>.Valid(counties);
                }
                catch (Exception e)
                {
                    return Result<List<County>>.Invalid(e.Message);
                }
            }
        }
    }
}
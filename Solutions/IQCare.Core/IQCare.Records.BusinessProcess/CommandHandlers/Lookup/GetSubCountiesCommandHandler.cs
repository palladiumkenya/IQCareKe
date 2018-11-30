using IQCare.Common.Infrastructure;
using IQCare.Common.Services;
using IQCare.Library;
using IQCare.Records.BusinessProcess.Command.Lookup;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using County = IQCare.Common.Core.Models.County;
using SubCountyLookup = IQCare.Common.Core.Models.SubCountyLookup;

namespace IQCare.Records.BusinessProcess.CommandHandlers.Lookup
{
    public class GetSubCountiesCommandHandler:IRequestHandler<GetSubCountiesCommand,Result<List<SubCountyLookup>>>
    {

        private readonly ICommonUnitOfWork _unitOfWork;

        public GetSubCountiesCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<List<SubCountyLookup>>> Handle(GetSubCountiesCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    CountyService countyService = new CountyService(_unitOfWork);
                    var subcounties = await countyService.GetSubCountyList(request.CountyId);

                    return Result<List<SubCountyLookup>>.Valid(subcounties);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<List<SubCountyLookup>>.Invalid(e.Message);
                }
            }
        }
    }
}

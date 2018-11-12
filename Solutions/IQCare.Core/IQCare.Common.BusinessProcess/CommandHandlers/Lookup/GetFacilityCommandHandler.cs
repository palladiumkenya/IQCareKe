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

namespace IQCare.Common.BusinessProcess.CommandHandlers.Lookup
{
    public class GetFacilityCommandHandler : IRequestHandler<GetFacilityCommand, Result<List<FacilityList>>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public GetFacilityCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<List<FacilityList>>> Handle(GetFacilityCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var facility = await _unitOfWork.Repository<FacilityList>().Get(x => x.MFLCode == request.MflCode)
                        .ToListAsync();

                    return Result<List<FacilityList>>.Valid(facility);
                }
                catch (Exception e)
                {
                    return Result<List<FacilityList>>.Invalid(e.Message);
                }
            }
        }
    }
}
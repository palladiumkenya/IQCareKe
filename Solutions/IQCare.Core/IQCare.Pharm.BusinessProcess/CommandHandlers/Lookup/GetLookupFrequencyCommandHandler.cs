using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Pharm.Core.Models;
using IQCare.Pharm.BusinessProcess.Commands.Lookup;
using IQCare.Pharm.Infrastructure;

namespace IQCare.Pharm.BusinessProcess.CommandHandlers.Lookup
{
    public class GetLookupFrequencyCommandHandler : IRequestHandler<GetLookupFrequencyCommand, Result<GetLookupFrequencyResponse>>
    {
        private readonly IPharmUnitOfWork _unitOfWork;
        public GetLookupFrequencyCommandHandler(IPharmUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new System.ArgumentNullException(nameof(unitOfWork));

        }

        public async Task<Result<GetLookupFrequencyResponse>> Handle(GetLookupFrequencyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var results = await _unitOfWork
                    .Repository<Frequency>()
                    .Get(x =>  x.DeleteFlag == 0)
                    .ToListAsync();

                _unitOfWork.Dispose();

                return Result<GetLookupFrequencyResponse>.Valid(new GetLookupFrequencyResponse
                {
                    FrequencyItems = results
                });
            }
            catch (Exception ex)
            {
                return Result<GetLookupFrequencyResponse>.Invalid(ex.Message);
            }
        }
    }
}

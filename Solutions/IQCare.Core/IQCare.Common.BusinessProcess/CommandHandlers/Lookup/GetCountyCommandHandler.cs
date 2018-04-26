using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Lookup;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Lookup
{
    public class GetCountyCommandHandler : IRequestHandler<GetCountyCommand, Result<GetCountyResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public GetCountyCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<GetCountyResponse>> Handle(GetCountyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (_unitOfWork)
                {
                    Expression<Func<County, bool>> expressionFinal = county => county.Id > 0;

                    if (request.CountyId != null)
                    {
                        Expression<Func<County, bool>> expressionCountyId = c => c.CountyId == request.CountyId;

                        expressionFinal = PredicateBuilder.And(expressionFinal, expressionCountyId);
                    }

                    if (request.SubcountyId != null)
                    {
                        Expression<Func<County, bool>> expressionSubCountyId = c => c.SubcountyId == request.SubcountyId;

                        expressionFinal = PredicateBuilder.And(expressionFinal, expressionSubCountyId);
                    }

                    if (request.WardId != null)
                    {
                        Expression<Func<County, bool>> expressionWardId = c => c.WardId == request.WardId;

                        expressionFinal = PredicateBuilder.And(expressionFinal, expressionWardId);
                    }

                    var result = await _unitOfWork.Repository<County>().Get(expressionFinal).ToListAsync();

                    _unitOfWork.Dispose();

                    return Result<GetCountyResponse>.Valid(new GetCountyResponse()
                    {
                        County = result
                    });
                }
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return Result<GetCountyResponse>.Invalid(e.Message);
            }
        }
    }
}
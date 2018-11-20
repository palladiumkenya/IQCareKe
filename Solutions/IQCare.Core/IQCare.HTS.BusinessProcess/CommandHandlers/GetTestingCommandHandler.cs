using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.HTS.BusinessProcess.Commands;
using IQCare.HTS.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.HTS.BusinessProcess.CommandHandlers
{
    public class GetTestingCommandHandler : IRequestHandler<GetTestingCommand, Result<GetTestingResultsResponse>>
    {
        private readonly IHTSUnitOfWork _unitOfWork;

        public GetTestingCommandHandler(IHTSUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<GetTestingResultsResponse>> Handle(GetTestingCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {

                    var testings = await _unitOfWork.Repository<Core.Model.Testing>().Get(x => x.HtsEncounterId == 1)
                        .ToListAsync();

                    return Result<GetTestingResultsResponse>.Valid(new GetTestingResultsResponse());
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message + " " + ex.InnerException);
                    return Result<GetTestingResultsResponse>.Invalid(ex.Message);
                }
            }
        }
    }
}
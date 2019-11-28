using IQCare.Common.BusinessProcess.Commands.Setup;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Setup
{
    public class GetIdentifiersCommandHandler : IRequestHandler<GetIdentifiersCommand, Result<List<Identifier>>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public GetIdentifiersCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<List<Identifier>>> Handle(GetIdentifiersCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var result = await _unitOfWork.Repository<Identifier>().GetAllAsync();
                    return Result<List<Identifier>>.Valid(result.ToList());
                }
                catch (Exception ex)
                {
                    Log.Error(ex, $"An error occured while trying to fetch identifiers");
                    return Result<List<Identifier>>.Invalid(ex.Message);
                }
            }
        }
    }
}

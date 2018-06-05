using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.HTS.BusinessProcess.Commands;
using IQCare.HTS.Core.Model;
using IQCare.HTS.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IQCare.HTS.BusinessProcess.CommandHandlers
{
    public class GetClientDisabilityCommandHandler : IRequestHandler<GetClientDisabilityCommand, Result<List<ClientDisability>>>
    {
        private readonly IHTSUnitOfWork _unitOfWork;

        public GetClientDisabilityCommandHandler(IHTSUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<List<ClientDisability>>> Handle(GetClientDisabilityCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var result = await _unitOfWork.Repository<ClientDisability>()
                        .Get(x => x.PersonId == request.PersonId).ToListAsync();

                    return Result<List<ClientDisability>>.Valid(result);
                }
                catch (Exception e)
                {
                    return Result<List<ClientDisability>>.Invalid(e.Message);
                }
            }
        }
    }
}
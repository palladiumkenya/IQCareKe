using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using IQCare.Records.BusinessProcess.Command.Registration;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using PersonIdentifier = IQCare.Common.Core.Models.PersonIdentifier;

namespace IQCare.Records.BusinessProcess.CommandHandlers.Registration
{
    public class GetPersonIdentifiersCommandHandler : IRequestHandler<GetPersonIdentifiersCommand, Result<List<PersonIdentifier>>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public GetPersonIdentifiersCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<List<PersonIdentifier>>> Handle(GetPersonIdentifiersCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var result = await _unitOfWork.Repository<PersonIdentifier>()
                        .Get(x => x.DeleteFlag == false && x.PersonId == request.PersonId).ToListAsync();
                    return Result<List<PersonIdentifier>>.Valid(result);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<List<PersonIdentifier>>.Invalid(e.Message);
                }
            }
        }
    }
}
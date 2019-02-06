using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.PersonCommand;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Common.BusinessProcess.CommandHandlers.PersonCommand
{
    public class GetPersonPopulationCommandHandler : IRequestHandler<GetPersonPopulationCommand, Result<List<PersonPopulation>>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public GetPersonPopulationCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<List<PersonPopulation>>> Handle(GetPersonPopulationCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var result = await _unitOfWork.Repository<PersonPopulation>()
                        .Get(x => x.PersonId == request.PersonId && x.DeleteFlag == false).ToListAsync();

                    return Result<List<PersonPopulation>>.Valid(result);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<List<PersonPopulation>>.Invalid(e.Message);
                }
            }
        }
    }
}
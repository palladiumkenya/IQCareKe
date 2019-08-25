using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.PersonCommand;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Serilog;

namespace IQCare.Common.BusinessProcess.CommandHandlers.PersonCommand
{
    public class GetPersonQueryCommandHandler : IRequestHandler<GetPersonQueryCommand, Result<PersonView>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public GetPersonQueryCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<PersonView>> Handle(GetPersonQueryCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var result = await _unitOfWork.Repository<PersonView>().FindByIdAsync(request.PersonId);
                    return Result<PersonView>.Valid(result);
                }
                catch (Exception ex)
                {
                    Log.Error($"An error occured which fetching a person with PersonId: {request.PersonId}, ErrorMessage: {ex.Message}, InnerException: {ex.InnerException}");
                    return Result<PersonView>.Invalid($"An error occured which fetching a person with PersonId: {request.PersonId}, ErrorMessage: {ex.Message}");
                }
            }
        }
    }
}
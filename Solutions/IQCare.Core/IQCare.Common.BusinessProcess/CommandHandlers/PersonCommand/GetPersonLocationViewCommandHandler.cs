using IQCare.Common.BusinessProcess.Commands.PersonCommand;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;

namespace IQCare.Common.BusinessProcess.CommandHandlers.PersonCommand
{
    public class GetPersonLocationViewCommandHandler : IRequestHandler<GetPersonLocationViewCommand, Result<PersonLocationView>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public GetPersonLocationViewCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<PersonLocationView>> Handle(GetPersonLocationViewCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var result = await _unitOfWork.Repository<PersonLocationView>()
                       .Get(x => x.PersonId == request.personId).FirstOrDefaultAsync();

                    return Result<PersonLocationView>.Valid(result);
                }
                catch (Exception e)
                {

                    Log.Error(e.Message);
                    return Result<PersonLocationView>.Invalid(e.Message);
                }
            }         
            
        }
    }
}

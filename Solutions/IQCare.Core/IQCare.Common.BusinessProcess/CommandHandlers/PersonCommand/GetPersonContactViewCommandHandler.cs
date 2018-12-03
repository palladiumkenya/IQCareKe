using System;
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
    public class GetPersonContactViewCommandHandler : IRequestHandler<GetPersonContactViewCommand, Result<PersonContactView>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public GetPersonContactViewCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<PersonContactView>> Handle(GetPersonContactViewCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var result = await _unitOfWork.Repository<PersonContactView>()
                        .Get(x => x.PersonId == request.personId ).FirstOrDefaultAsync();

                    return Result<PersonContactView>.Valid(result) ;
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<PersonContactView>.Invalid(e.Message);
                }
            }
        }
    }
}

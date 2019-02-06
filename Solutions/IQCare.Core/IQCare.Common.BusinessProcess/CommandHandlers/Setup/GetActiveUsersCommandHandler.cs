using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Setup;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Serilog;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Setup
{
    public class GetActiveUsersCommandHandler : IRequestHandler<GetActiveUsersCommand, Result<List<User>>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public GetActiveUsersCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<List<User>>> Handle(GetActiveUsersCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    Services.Setup setup = new Services.Setup(_unitOfWork);
                    var users = await setup.GetActiveUsers();
                    return Result<List<User>>.Valid(users);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<List<User>>.Invalid(e.Message);
                }
            }
        }
    }
}
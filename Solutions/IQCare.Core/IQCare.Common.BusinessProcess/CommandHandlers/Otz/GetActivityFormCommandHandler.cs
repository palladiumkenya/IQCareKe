using IQCare.Common.BusinessProcess.Commands.Otz;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Otz
{
    public class GetActivityFormCommandHandler : IRequestHandler<GetActivityFormCommand, Result<OtzActivityFormsView>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public GetActivityFormCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<OtzActivityFormsView>> Handle(GetActivityFormCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var result = await _unitOfWork.Repository<OtzActivityFormsView>().FindByIdAsync(request.Id);
                    return Result<OtzActivityFormsView>.Valid(result);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, $"an error occured while trying to fetching activity form");
                    return Result<OtzActivityFormsView>.Invalid(ex.Message);
                }
            }
        }
    }
}

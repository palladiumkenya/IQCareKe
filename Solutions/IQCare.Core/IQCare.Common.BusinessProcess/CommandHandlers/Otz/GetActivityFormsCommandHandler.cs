using IQCare.Common.BusinessProcess.Commands.Otz;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Otz
{
    public class GetActivityFormsCommandHandler : IRequestHandler<GetActivityFormsCommand, Result<List<OtzActivityFormsView>>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public GetActivityFormsCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<List<OtzActivityFormsView>>> Handle(GetActivityFormsCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var result = await _unitOfWork.Repository<OtzActivityFormsView>().Get(x => x.PatientId == request.PatientId).ToListAsync();
                    return Result<List<OtzActivityFormsView>>.Valid(result);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, $"An error while fetching otz activity forms");
                    return Result<List<OtzActivityFormsView>>.Invalid(ex.Message);
                }
            }
        }
    }
}

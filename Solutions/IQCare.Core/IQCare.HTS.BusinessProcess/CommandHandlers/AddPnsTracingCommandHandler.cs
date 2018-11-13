using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.HTS.BusinessProcess.Commands;
using IQCare.HTS.Core.Model;
using IQCare.HTS.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IQCare.HTS.BusinessProcess.CommandHandlers
{
    public class AddPnsTracingCommandHandler : IRequestHandler<AddPnsTracingCommand, Result<AddPnsTracingResponse>>
    {
        private readonly IHTSUnitOfWork _unitOfWork;

        public AddPnsTracingCommandHandler(IHTSUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<AddPnsTracingResponse>> Handle(AddPnsTracingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (_unitOfWork)
                {
                    Tracing pnstrace = new Tracing()
                    {
                        PersonID = request.PersonId,
                        TracingType = request.TracingType,
                        DateTracingDone = request.TracingDate,
                        Mode = request.TracingMode,
                        Outcome = request.TracingOutcome,
                        Remarks = null,
                        DeleteFlag = false,
                        CreatedBy = request.UserId,
                        CreateDate = DateTime.Now,
                        Consent = request.Consent,
                        DateBookedTesting = request.DateBookedTesting,
                        ReminderDate = request.DateReminded
                    };

                    await _unitOfWork.Repository<Tracing>().AddAsync(pnstrace);
                    await _unitOfWork.SaveAsync();

                    _unitOfWork.Dispose();

                    return Result<AddPnsTracingResponse>.Valid(new AddPnsTracingResponse()
                    {
                        TracingId = pnstrace.Id
                    });
                }
            }
            catch (Exception e)
            {
                return Result<AddPnsTracingResponse>.Invalid(e.Message);
            }
        }
    }
}
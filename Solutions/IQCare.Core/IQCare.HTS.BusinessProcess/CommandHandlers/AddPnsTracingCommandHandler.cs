using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.HTS.BusinessProcess.Commands;
using IQCare.HTS.Core.Model;
using IQCare.HTS.Infrastructure;
using MediatR;

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
                        CreateDate = DateTime.Now
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
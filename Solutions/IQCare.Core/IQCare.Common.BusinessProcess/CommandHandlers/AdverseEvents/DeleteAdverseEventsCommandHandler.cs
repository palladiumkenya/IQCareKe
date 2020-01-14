using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.AdverseEvents;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;


namespace IQCare.Common.BusinessProcess.CommandHandlers.AdverseEvents
{
   public  class DeleteAdverseEventsCommandHandler :IRequestHandler<DeleteAdverseEventsCommand,Result<DeleteAdverseEventResponse>>
    {
        ICommonUnitOfWork _commontUnitOfWork;

        public string message { get; set; }

        public int Id { get; set; }

        public DeleteAdverseEventsCommandHandler(ICommonUnitOfWork commonUnitOfWork)
        {
            _commontUnitOfWork = commonUnitOfWork;
        }

        public async Task<Result<DeleteAdverseEventResponse>> Handle (DeleteAdverseEventsCommand request,CancellationToken cancellationToken)
        {
            using (_commontUnitOfWork)
            {
                try
                {
                    var adverseevents = await _commontUnitOfWork.Repository<PatientAdverseEvent>().Get(x => x.Id == request.Id && x.DeleteFlag == false).FirstOrDefaultAsync();

                    if(adverseevents != null)
                    {
                        adverseevents.DeleteFlag = true;
                        _commontUnitOfWork.Repository<PatientAdverseEvent>().Update(adverseevents);
                       await  _commontUnitOfWork.SaveAsync();
                        message += "Deleted Successfully";
                        Id = adverseevents.Id;
                            

                    }
                    else
                    {
                        message += "Not Deleted Successfully";
                        Id = 0;
                    }

                    return Result<DeleteAdverseEventResponse>.Valid(new DeleteAdverseEventResponse
                    {
                        Message = message,
                        ResultOutcome = Id
                    });


                }

                catch(Exception e)
                {
                    Log.Error($"An error occured while delete the adverseEvents. Exception: {e.Message} {e.InnerException}");
                    return Result<DeleteAdverseEventResponse>.Invalid($"An error occured while deleting adverseEvents for patient");

                }
            }
        }
    }
}

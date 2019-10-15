using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Allergies;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;


namespace IQCare.Common.BusinessProcess.CommandHandlers.Allergies
{
   public  class DeleteAllergiesCommandHandler : IRequestHandler<DeleteAllergiesCommand, Result<DeleteAllergiesResponse>>
    {
        ICommonUnitOfWork _commontUnitOfWork;

        public string message { get; set; }

        public int Id { get; set; }

        public DeleteAllergiesCommandHandler(ICommonUnitOfWork commonUnitOfWork)
        {
            _commontUnitOfWork = commonUnitOfWork;
        }

        public async Task<Result<DeleteAllergiesResponse>> Handle(DeleteAllergiesCommand request, CancellationToken cancellationToken)
        {
            using (_commontUnitOfWork)
            {
                try
                {
                    var allergy = await _commontUnitOfWork.Repository<PatientAllergy>().Get(x => x.Id == request.Id && x.DeleteFlag == false).FirstOrDefaultAsync();

                    if (allergy != null)
                    {
                        allergy.DeleteFlag = true;
                        _commontUnitOfWork.Repository<PatientAllergy>().Update(allergy);
                        await _commontUnitOfWork.SaveAsync();
                        message += "Deleted Successfully";
                        Id = allergy.Id;


                    }
                    else
                    {
                        message += "Not Deleted Successfully";
                        Id = 0;
                    }

                    return Result<DeleteAllergiesResponse>.Valid(new DeleteAllergiesResponse
                    {
                        Message = message,
                        ResultOutcome = Id
                    });


                }

                catch (Exception e)
                {
                    Log.Error($"An error occured while delete the Patient allergy. Exception: {e.Message} {e.InnerException}");
                    return Result<DeleteAllergiesResponse>.Invalid($"An error occured while deleting allergy for patient");

                }
            }
        }
    }
}

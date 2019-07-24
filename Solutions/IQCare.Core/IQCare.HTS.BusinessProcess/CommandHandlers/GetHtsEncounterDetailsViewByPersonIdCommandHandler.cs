using System;
using System.Collections.Generic;
using System.Text;
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
   public  class GetHtsEncounterDetailsViewByPersonIdCommandHandler :IRequestHandler<GetHtsEncounterDetailsViewByPersonIdCommand,Result<List<EncountersDetailView>>>
    {
         
      private readonly IHTSUnitOfWork _unitOfWork;
    public GetHtsEncounterDetailsViewByPersonIdCommandHandler (IHTSUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result<List<EncountersDetailView>>> Handle(GetHtsEncounterDetailsViewByPersonIdCommand request, CancellationToken cancellationToken)
    {
        using (_unitOfWork)
        {
            try
            {

                    var result = await _unitOfWork.Repository<EncountersDetailView>()
                    .Get(x => x.PersonId == request.personId && x.EncounterDate.Date == DateTime.Now.Date).ToListAsync();

                return Result<List<EncountersDetailView>>.Valid(result);
            }
            catch (Exception e)
            {
                return Result<List<EncountersDetailView>>.Invalid(e.Message);
            }
        }
    }
}
}

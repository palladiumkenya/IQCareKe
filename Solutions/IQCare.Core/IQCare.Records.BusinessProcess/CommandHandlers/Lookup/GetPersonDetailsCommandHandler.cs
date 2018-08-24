using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCareRecords.Common.BusinessProcess.Command;
using IQCare.Records.BusinessProcess.Command;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using IQCare.Common.Services;
using Serilog;

namespace IQCare.Records.BusinessProcess.CommandHandlers
{
    public  class GetPersonDetailsCommandHandler:IRequestHandler<GetPersonDetailsCommand,Result<List<PersonDetailsView>>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public GetPersonDetailsCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<List<PersonDetailsView>>> Handle(GetPersonDetailsCommand request,CancellationToken cancellationToken)
        {
            try
            {
                PersonDetailsViewService personDetailsViewService = new PersonDetailsViewService(_unitOfWork);
                var personDetail = await personDetailsViewService.GetPersonDetails(request.PersonId);

                return Result<List<PersonDetailsView>>.Valid(personDetail);
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message + " " + ex.InnerException);
                return Result<List<PersonDetailsView>>.Invalid(ex.Message);
            }
        }
    }
}

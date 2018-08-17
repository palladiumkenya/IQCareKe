using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCareRecords.Common.BusinessProcess.Command;
using MediatR;
namespace IQCareRecords.Common.BusinessProcess.CommandHandlers.Lookup
{
    public class GetConsentTypeCommandHandler:IRequestHandler<GetConsentTypeCommand,Result<GetConsentTypeResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public GetConsentTypeCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }


        public async Task<Result<GetConsentTypeResponse>> Handle(GetConsentTypeCommand request,CancellationToken cancellationToken)
        {

            try
            {  
                LookupLogic rs = new LookupLogic(_unitOfWork);
                int res = await Task.Run(() => rs.GetLookupIdbyName(request.ItemName));
                return Result<GetConsentTypeResponse>.Valid(new GetConsentTypeResponse()
                {
                    ConsentType = res
                });
            
            }
            catch(Exception e)
            {
                return Result<GetConsentTypeResponse>.Invalid(e.Message);

            }
        }
    }
}

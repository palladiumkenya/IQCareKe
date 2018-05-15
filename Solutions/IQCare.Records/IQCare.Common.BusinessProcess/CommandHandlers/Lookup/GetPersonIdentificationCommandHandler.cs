using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.BusinessProcess.Commands;
using MediatR;
using Entities.Records;
using System.Threading.Tasks;
using IQCare.Records.UILogic;
using IQCare.Records.UILogic.Enrollment;
using Entities.Records.Enrollment;
using System.Threading;

namespace IQCare.Common.BusinessProcess.CommandHandlers
{
   public  class GetPersonIdentificationCommandHandler:IRequestHandler<GetPersonIdentificationCommand,Result<GetPersonIdentificationResponse>>
    {
        IdentifierManager idm = new IdentifierManager();

        public async Task<Result<GetPersonIdentificationResponse>> Handle(GetPersonIdentificationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                List<Identifier> result = new List<Identifier>();
                result = await Task.Run(() => idm.GetMultipleIdentifierByCode(request.CodeName));
                

                return Result<GetPersonIdentificationResponse>.Valid(new GetPersonIdentificationResponse()
                {
                    Identifers = result
                });  
                  
            }
            catch(Exception ex)
            {
                return Result<GetPersonIdentificationResponse>.Invalid(ex.Message);
            }

        }
    }
}

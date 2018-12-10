using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCareRecords.Common.BusinessProcess.Command;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;

namespace IQCareRecords.Common.BusinessProcess.CommandHandlers.Lookup
{


    public class GetPersonIdentificationCommandHandler : IRequestHandler<GetPersonIdentificationCommand, Result<GetPersonIdentificationResponse>>
    {

        private readonly ICommonUnitOfWork _unitOfWork;
        public GetPersonIdentificationCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }


        public async Task<Result<GetPersonIdentificationResponse>> Handle(GetPersonIdentificationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                RegisterPersonService rs = new RegisterPersonService(_unitOfWork);
                List<Identifier> result = new List<Identifier>();
                result = await Task.Run(() => rs.GetPersonIdentifierType(request.CodeName));


                return Result<GetPersonIdentificationResponse>.Valid(new GetPersonIdentificationResponse()
                {
                    Identifers = result
                });

            }
            catch (Exception ex)
            {
                return Result<GetPersonIdentificationResponse>.Invalid(ex.Message);
            }

        }
    }
}

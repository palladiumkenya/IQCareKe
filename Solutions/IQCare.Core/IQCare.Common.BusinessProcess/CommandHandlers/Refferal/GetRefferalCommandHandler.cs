using IQCare.Common.BusinessProcess.Commands.Refferal;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Serilog;
using System;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Refferal
{

    public class GetRefferalCommandHandler :IRequestHandler<GetRefferalCommand, Result<List<PatientRefferal>>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public GetRefferalCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<PatientRefferal>>> Handle(GetRefferalCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    List<PatientRefferal> result = await _unitOfWork.Repository<PatientRefferal>().Get(x => x.PatientId == request.PatientId && x.PatientMasterVisitId == request.PatientMasterVisitId).ToListAsync();
                    return Result<List<PatientRefferal>>.Valid(result);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<List<PatientRefferal>>.Invalid(e.Message);
                }
                
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Enrollment;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Linq;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Enrollment
{
    public  class GetLatestCareEndingDetailsCommandHandler: IRequestHandler<GetLatestCareEndingDetailsCommand,Result<PatientCareEnding>>
    {

        private readonly ICommonUnitOfWork _unitOfWork;

        public GetLatestCareEndingDetailsCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<PatientCareEnding>> Handle(GetLatestCareEndingDetailsCommand request,CancellationToken cancellationToken)
        {
            try
            {

                var result = await _unitOfWork.Repository<PatientCareEnding>().Get(x => x.PatientId == request.PatientId && !x.Active).OrderByDescending(x=>x.Id).FirstOrDefaultAsync();


                return Result<PatientCareEnding>.Valid(result);
            }
            catch(Exception ex)
            {
                return Result<PatientCareEnding>.Invalid(ex.Message);

            }
        }
    }
}

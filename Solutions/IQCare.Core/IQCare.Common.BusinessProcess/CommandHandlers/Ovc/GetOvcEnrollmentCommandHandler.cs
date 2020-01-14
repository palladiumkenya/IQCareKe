using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Ovc;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Ovc
{
    public class GetOvcEnrollmentCommandHandler : IRequestHandler<GetOvcEnrollmentCommand, Result<OvcEnrollmentForm>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public GetOvcEnrollmentCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<Result<OvcEnrollmentForm>> Handle(GetOvcEnrollmentCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var result = await _unitOfWork.Repository<OvcEnrollmentForm>().Get(x => x.PersonId == request.PersonId && x.DeleteFlag != true)
                    .OrderByDescending(x => x.EnrollmentDate).FirstOrDefaultAsync();

                return Result<OvcEnrollmentForm>.Valid(result);
            }
            catch (Exception ex)
            {
                return Result<OvcEnrollmentForm>.Invalid(ex.Message);

            }

        }

    }
}
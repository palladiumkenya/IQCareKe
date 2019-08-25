
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
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Enrollment
{
    public class GetEnrollmentMasterVisitCommandHandler : IRequestHandler<GetEnrollmentMasterVisitCommand, Result<List<IQCare.Common.Core.Models.PatientMasterVisit>>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public GetEnrollmentMasterVisitCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<List<IQCare.Common.Core.Models.PatientMasterVisit>>> Handle(GetEnrollmentMasterVisitCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (_unitOfWork)
                {
                    int PatientMasterVisitId = 0;
                    RegisterPersonService registerPersonService = new RegisterPersonService(_unitOfWork);
                    var enrollmentVisitType = await _unitOfWork.Repository<LookupItemView>().Get(x => x.MasterName == "VisitType" && x.ItemName == "Enrollment").FirstOrDefaultAsync();
                    int? visitType = enrollmentVisitType != null ? enrollmentVisitType.ItemId : 0;

                    var enrollmentPatientMasterVisit =
                               await _unitOfWork.Repository<Core.Models.PatientMasterVisit>().Get(x =>
                               x.PatientId == request.PatientId && x.ServiceId == request.ServiceAreaId && x.VisitType == visitType && x.DeleteFlag == false).OrderByDescending(x=>x.Id).ToListAsync();

                    return Result<List<IQCare.Common.Core.Models.PatientMasterVisit>>.Valid(enrollmentPatientMasterVisit);
                }

            }
            catch(Exception ex)
            {
                return Result<List<IQCare.Common.Core.Models.PatientMasterVisit>>.Invalid(ex.Message);
            }
        }

    }
}

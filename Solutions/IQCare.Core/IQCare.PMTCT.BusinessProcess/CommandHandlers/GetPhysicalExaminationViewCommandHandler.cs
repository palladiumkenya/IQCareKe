using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands;
using IQCare.PMTCT.BusinessProcess.Commands.Education;
using IQCare.PMTCT.Core.Models.Views;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers
{
    public class GetPhysicalExaminationViewCommandHandler: IRequestHandler<GetPhysicalExaminationViewCommand, Result<List<PhysicalExaminationView>>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public GetPhysicalExaminationViewCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
        }

        public async Task<Result<List<PhysicalExaminationView> >> Handle(GetPhysicalExaminationViewCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                   List<PhysicalExaminationView>  physicalExamination = await _unitOfWork.Repository<PhysicalExaminationView>()
                        .Get(x => x.PatientId == request.PatientId &&
                                  x.PatientMasterVisitId == request.PatientMasterVisitId).ToListAsync();
                    return Result<List<PhysicalExaminationView> >.Valid(physicalExamination);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Library.Result<List<PhysicalExaminationView> >.Invalid(e.Message);
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Common.BusinessProcess.Commands.Encounter;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Encounter
{
    public class GetPatientMasterVisitCommandHandler : IRequestHandler<GetPatientMasterVisitCommand, Result<List<IQCare.Common.Core.Models.PatientMasterVisit>>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public GetPatientMasterVisitCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<IQCare.Common.Core.Models.PatientMasterVisit>>> Handle(GetPatientMasterVisitCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    List<IQCare.Common.Core.Models.PatientMasterVisit> patientvisits = await _unitOfWork
                        .Repository<IQCare.Common.Core.Models.PatientMasterVisit>().Get(x => x.PatientId == request.PatientId && x.Id == request.PatientMasterVisitId && !x.DeleteFlag)
                        .ToListAsync();
                    return Result<List<IQCare.Common.Core.Models.PatientMasterVisit>>.Valid(patientvisits);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<List<IQCare.Common.Core.Models.PatientMasterVisit>>.Invalid(e.Message);
                }
            }
        }

    }
}

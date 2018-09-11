using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.Education;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.Education
{
    public class GetPatientEducationCommandHandler: IRequestHandler<GetPatientEducationCommand,Result<List<PatientEducation>>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public GetPatientEducationCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<PatientEducation>>> Handle(GetPatientEducationCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var result =await _unitOfWork.Repository<PatientEducation>().Get(x => x.PatientId == request.PatientId).ToListAsync();
                    return Result<List<PatientEducation>>.Valid(result);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<List<PatientEducation>>.Invalid(e.Message);
                }
            }
        }
    }
}

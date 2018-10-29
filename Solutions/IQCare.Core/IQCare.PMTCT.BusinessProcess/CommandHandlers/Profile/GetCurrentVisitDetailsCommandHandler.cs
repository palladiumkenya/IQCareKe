using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.Profile;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Expressions;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.Profile
{
    public class GetCurrentVisitDetailsCommandHandler : IRequestHandler<GetCurrentVisitDetailsCommand, Result<PatientProfile>>
    {

        private readonly IPmtctUnitOfWork _unitOfWork;

        public GetCurrentVisitDetailsCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PatientProfile>> Handle(GetCurrentVisitDetailsCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    PatientPregnancy pregnancy = _unitOfWork.Repository<PatientPregnancy>()
                        .Get(x => x.Outcome == null && !x.DateOfOutcome.HasValue && !x.DeleteFlag).FirstOrDefault();

                    PatientProfile patientProfile= new PatientProfile();



                    if (pregnancy != null)
                    {
                         patientProfile = await _unitOfWork.Repository<PatientProfile>()
                            .Get(x => x.PatientId == request.PatientId && !x.DeleteFlag && x.PregnancyId == pregnancy.Id)
                            .FirstOrDefaultAsync();
                    }
                    else
                    {
                         patientProfile = await _unitOfWork.Repository<PatientProfile>()
                            .Get(x => x.PatientId == request.PatientId && !x.DeleteFlag)
                            .FirstOrDefaultAsync();
                    }
                    
                    return Result<PatientProfile>.Valid(patientProfile);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
           
        }
    }
}
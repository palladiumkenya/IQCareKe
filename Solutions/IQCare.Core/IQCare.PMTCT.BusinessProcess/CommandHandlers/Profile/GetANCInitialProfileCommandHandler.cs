using IQCare.Library;
using IQCare.PMTCT.Infrastructure;
using IQCare.PMTCT.Core.Models;
using MediatR;
using System;
using System.Threading;
using IQCare.PMTCT.BusinessProcess.Commands.Profile;
using System.Threading.Tasks;
using Serilog;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.Profile
{
    public class GetANCInitialProfileCommandHandler:IRequestHandler<GetANCInitialProfileCommand,Result<PatientProfile>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public GetANCInitialProfileCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PatientProfile>> Handle(GetANCInitialProfileCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    PatientProfile result =await  _unitOfWork.Repository<PatientProfile>().Get(x => x.PatientId == request.PatientId & x.PregnancyId==request.PregnancyId & !x.DeleteFlag).OrderBy(x=>x.Id).FirstOrDefaultAsync();
                    if(result==null)
                    {
                        result = new PatientProfile();
                    }
                    return Result<PatientProfile>.Valid(result);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<PatientProfile>.Invalid(e.Message);
                }
            }
        }
    }
}

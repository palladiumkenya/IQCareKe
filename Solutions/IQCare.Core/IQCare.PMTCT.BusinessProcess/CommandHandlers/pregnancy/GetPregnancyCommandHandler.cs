using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.Pregnancy;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using MediatR;
using Serilog;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.pregnancy
{
    public class GetPregnancyCommandHandler: IRequestHandler<GetPregnancyCommand,Result<PregnancyViewModel>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public GetPregnancyCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PregnancyViewModel>> Handle(GetPregnancyCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    PatientPregnancy result = await _unitOfWork.Repository<PatientPregnancy>().Get(x => x.PatientId == request.PatientId).OrderByDescending(x=>x.Id) .FirstOrDefaultAsync();
                    PregnancyViewModel pregnancyView = new PregnancyViewModel();;
                    if (result != null)
                    {
                        pregnancyView.Id = result.Id;
                        pregnancyView.PatientId = result.PatientId;
                        pregnancyView.PatientMasterVisitId = result.PatientMasterVisitId;
                        pregnancyView.Lmp = result.Lmp;
                        pregnancyView.Edd = result.Edd;
                        pregnancyView.Gestation = result.Gestation;
                        pregnancyView.Gravidae = result.Gravidae;
                        pregnancyView.Parity = result.Parity;
                        pregnancyView.Parity2 = result.Parity2;
                        pregnancyView.Outcome = result.Outcome;
                        pregnancyView.DateOfOutcome = result.DateOfOutcome;
                        pregnancyView.AgeAtMenarche = result.AgeAtMenarche;
                    }

                   return   Result<PregnancyViewModel>.Valid(pregnancyView);
                                   
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<PregnancyViewModel>.Invalid(e.Message);
                }
            }
        }
    }
}

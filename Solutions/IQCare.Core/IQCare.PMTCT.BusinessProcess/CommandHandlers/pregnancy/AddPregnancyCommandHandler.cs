using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands.Pregnancy;
using IQCare.PMTCT.Core.Models;
using IQCare.PMTCT.Infrastructure;
using IQCare.PMTCT.Services;
using MediatR;
using Serilog;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers.pregnancy
{
    public class AddPregnancyCommandHandler: IRequestHandler<AddPregnancyCommand, Result<AddPregnancyCommandResult>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;

        public AddPregnancyCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<AddPregnancyCommandResult>> Handle(AddPregnancyCommand request, CancellationToken cancellationToken)
        {
           

                try
                {
                    VisitDetailsService visitDetailsService = new VisitDetailsService(_unitOfWork);

                PatientPregnancy pregnancy = new PatientPregnancy()
                    {
                        PatientId = request.PatientId,
                        PatientMasterVisitId = request.PatientMasterVisitId,
                        CreateDate = DateTime.Now,
                        CreatedBy = request.CreatedBy,
                        DeleteFlag = false,
                        Lmp = request.Lmp,
                        Edd = request.Edd,
                        Gestation = request.Gestation,
                        Gravidae = request.Gravidae,
                        Parity = request.Parity,
                        Parity2 = request.Parity2,
                    };

                    var _preganancy = await visitDetailsService.AddPatientPregnancy(pregnancy);

                    return Result<AddPregnancyCommandResult>.Valid(new AddPregnancyCommandResult()
                    {
                        PregnancyId = _preganancy.Id
                            
                    });
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.InnerException);
                    return Result<AddPregnancyCommandResult>.Invalid(e.Message);
                }
        }
    }
}
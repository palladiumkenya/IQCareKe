using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Encounter;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Serilog;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Encounter
{
    public class EditEncounterVisitCommandHandler : IRequestHandler<EditEncounterVisitCommand, Result<EditEncounterVisitCommandResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public EditEncounterVisitCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<EditEncounterVisitCommandResponse>> Handle(EditEncounterVisitCommand request, CancellationToken cancellationToken)
        {
            using (var trans = _unitOfWork.Context.Database.BeginTransaction())
            {
                try
                {
                    var patientMasterVisit = await _unitOfWork.Repository<Core.Models.PatientMasterVisit>()
                        .FindByIdAsync(request.PatientMasterVisitId);
                    var patientEncounter = await _unitOfWork.Repository<PatientEncounter>().FindByIdAsync(request.Id);

                    if (patientMasterVisit != null)
                    {
                        patientMasterVisit.Start = request.EncounterDate;
                        patientMasterVisit.VisitDate = request.EncounterDate;

                        _unitOfWork.Repository<Core.Models.PatientMasterVisit>().Update(patientMasterVisit);
                        await _unitOfWork.SaveAsync();
                    }

                    if (patientEncounter != null)
                    {
                        patientEncounter.EncounterStartTime = request.EncounterDate;
                        patientEncounter.EncounterEndTime = request.EncounterDate;

                        _unitOfWork.Repository<PatientEncounter>().Update(patientEncounter);
                        await _unitOfWork.SaveAsync();
                    }

                    trans.Commit();

                    return Result<EditEncounterVisitCommandResponse>.Valid(new EditEncounterVisitCommandResponse()
                    {
                        Message = $"Successfully updated patient encounter visit"
                    });
                }
                catch (Exception ex)
                {
                    Log.Error($"Error updating Encounter Visit for PatientEncounterId: {request.Id} ", ex.Message);
                    trans.Rollback();
                    return Result<EditEncounterVisitCommandResponse>.Invalid($"Error updating Encounter Visit for PatientEncounterId: {request.Id} {ex.Message} ");
                }
            }
        }
    }
}
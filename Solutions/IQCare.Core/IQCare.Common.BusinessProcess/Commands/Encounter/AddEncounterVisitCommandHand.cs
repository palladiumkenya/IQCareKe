using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Encounter;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using MediatR;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Encounter
{
    public class AddEncounterVisitCommandHand : IRequestHandler<AddEncounterVisitCommand, Result<AddEncounterVisitResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public AddEncounterVisitCommandHand(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<AddEncounterVisitResponse>> Handle(AddEncounterVisitCommand request, CancellationToken cancellationToken)
        {
            using (var trans = _unitOfWork.Context.Database.BeginTransaction())
            {
                try
                {
                    PatientMasterVisit patientMasterVisit = new PatientMasterVisit()
                    {
                        PatientId = request.PatientId,
                        ServiceId = request.ServiceAreaId,
                        Start = request.EncounterDate,
                        Active = true,
                        CreateDate = DateTime.Now,
                        DeleteFlag = false,
                        VisitDate = request.EncounterDate,
                        CreatedBy = request.UserId,
                        VisitType = 0
                    };

                    await _unitOfWork.Repository<PatientMasterVisit>().AddAsync(patientMasterVisit);
                    await _unitOfWork.SaveAsync();

                    PatientEncounter patientEncounter = new PatientEncounter()
                    {
                        PatientId = request.PatientId,
                        EncounterTypeId = request.EncounterType,
                        Status = 0,
                        PatientMasterVisitId = patientMasterVisit.Id,
                        EncounterStartTime = request.EncounterDate,
                        EncounterEndTime = request.EncounterDate,
                        ServiceAreaId = request.ServiceAreaId,
                        CreatedBy = request.UserId,
                        CreateDate = DateTime.Now
                    };

                    await _unitOfWork.Repository<PatientEncounter>().AddAsync(patientEncounter);
                    await _unitOfWork.SaveAsync();

                    trans.Commit();

                    //_unitOfWork.Dispose();

                    return Result<AddEncounterVisitResponse>.Valid(new AddEncounterVisitResponse
                    {
                        PatientMasterVisitId = patientMasterVisit.Id,
                        PatientEncounterId = patientEncounter.Id
                    });
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return Result<AddEncounterVisitResponse>.Invalid(ex.Message);
                }
            }
                
        }
    }
}
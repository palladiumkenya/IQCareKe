using IQCare.Library;
using IQCare.Maternity.BusinessProcess.Commands.Maternity;
using IQCare.Maternity.Core.Domain.Maternity;
using IQCare.Maternity.Infrastructure.UnitOfWork;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Maternity.BusinessProcess.CommandHandlers
{
    public class DischargePatientCommandHandler : IRequestHandler<DischargePatientCommand, Result<DischargePatientResponse>>
    {
        IMaternityUnitOfWork _maternityUnitOfWork;
        ILogger logger = Log.ForContext<DischargePatientCommandHandler>();

        public DischargePatientCommandHandler(IMaternityUnitOfWork maternityUnitOfWork)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
        }
        public async Task<Result<DischargePatientResponse>> Handle(DischargePatientCommand request, CancellationToken cancellationToken)
        {
            using (_maternityUnitOfWork)
            {
                try
            {
                var patientDischarge = new MaternalPatientDischargeInformation(request.PatientMasterVisitId, request.OutcomeStatus, request.OutcomeDescription, request.CreatedBy, request.DateDischarged);

                await _maternityUnitOfWork.Repository<MaternalPatientDischargeInformation>().AddAsync(patientDischarge);
                await _maternityUnitOfWork.SaveAsync();

                return Result<DischargePatientResponse>.Valid(new DischargePatientResponse { PatientDischargeId = patientDischarge.Id });
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"An error occured while discharging patient with masterId {request.PatientMasterVisitId}");
                return Result<DischargePatientResponse>.Invalid(ex.Message);
            }
          }
            
        }
    }

    public class UpdatePatientDischargeCommandHandler : IRequestHandler<UpdatePatientDischargeCommand, Result<DischargePatientResponse>>
    {
        private readonly IMaternityUnitOfWork _maternityUnitOfWork;
        private readonly ILogger _logger = Log.ForContext<UpdatePatientDischargeCommandHandler>();

        public UpdatePatientDischargeCommandHandler(IMaternityUnitOfWork maternityUnitOfWork)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
        }
        public Task<Result<DischargePatientResponse>> Handle(UpdatePatientDischargeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var patientDischarge = _maternityUnitOfWork.Repository<MaternalPatientDischargeInformation>()
                       .Get(x => x.Id == request.Id).SingleOrDefault();
                if (patientDischarge == null)
                    return Task.FromResult(Result<DischargePatientResponse>.Invalid($"Patient discharge information with Id {request.Id} not found"));

                patientDischarge.Update(request.OutcomeStatus, request.DateDischarged, request.OutcomeDescription);

                _maternityUnitOfWork.Repository<MaternalPatientDischargeInformation>().Update(patientDischarge);
                _maternityUnitOfWork.Save();

                return Task.FromResult(Result<DischargePatientResponse>.Valid(new DischargePatientResponse
                {
                    Message = "Patient discharge information updated succesfully",
                    PatientDischargeId = request.Id
                }));
            }
            catch (Exception ex)
            {
                string message = $"An error occured while updating patient discharge information for Id {request.Id}";
                _logger.Error(ex, message);
                return Task.FromResult(Result<DischargePatientResponse>.Invalid(message));
            }
        }
    }
}

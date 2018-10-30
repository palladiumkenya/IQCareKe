using IQCare.Library;
using IQCare.Maternity.BusinessProcess.Commands.Maternity;
using IQCare.Maternity.Core.Domain.Maternity;
using IQCare.Maternity.Infrastructure.UnitOfWork;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
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

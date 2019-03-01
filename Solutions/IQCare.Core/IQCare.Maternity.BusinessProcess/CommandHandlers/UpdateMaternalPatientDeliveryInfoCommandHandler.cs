using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.Maternity.BusinessProcess.Commands.Maternity;
using IQCare.Maternity.Core.Domain.Maternity;
using IQCare.Maternity.Infrastructure.UnitOfWork;
using MediatR;
using Serilog;

namespace IQCare.Maternity.BusinessProcess.CommandHandlers
{
    public class UpdateMaternalPatientDeliveryInfoCommandHandler : IRequestHandler<UpdateMaternalPatientDeliveryInfoCommand, Result<UpdatePatientDeliveryInfoResponse>>
    {
        private readonly  IMaternityUnitOfWork _maternityUnitOfWork;
        private readonly ILogger _logger = Log.ForContext<UpdateMaternalPatientDeliveryInfoCommandHandler>();
        public UpdateMaternalPatientDeliveryInfoCommandHandler(IMaternityUnitOfWork maternityUnitOfWork)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
        }
        public Task<Result<UpdatePatientDeliveryInfoResponse>> Handle(UpdateMaternalPatientDeliveryInfoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var patientDeliveryInfo = _maternityUnitOfWork.Repository<PatientDeliveryInformation>()
                        .Get(x => x.Id == request.PatientDeliveryInfoId).SingleOrDefault();

                if (patientDeliveryInfo == null)
                    return Task.FromResult(Result<UpdatePatientDeliveryInfoResponse>.Invalid(
                        $"Patient Delivery information with Id {request.PatientDeliveryInfoId} not found"));

                patientDeliveryInfo.Update(request.MaternalPatientDeliveryInfoCommand);
                _maternityUnitOfWork.Repository<PatientDeliveryInformation>().Update(patientDeliveryInfo);

                _maternityUnitOfWork.Save();

                return Task.FromResult(Result<UpdatePatientDeliveryInfoResponse>.Valid(new UpdatePatientDeliveryInfoResponse
                {
                    Message = "Patient delivery info updated succesfully",
                    PatientMasterVisitId = request.MaternalPatientDeliveryInfoCommand.PatientMasterVisitId,
                    PatientDeliveryId = request.PatientDeliveryInfoId
                }));
            }
            catch (Exception ex)
            {
                string message = $"An error occured while updating patient delivery info with Id {request.PatientDeliveryInfoId}";
                _logger.Error(ex, message);

                return Task.FromResult(Result<UpdatePatientDeliveryInfoResponse>.Invalid(message));

            }

        }
    }
}

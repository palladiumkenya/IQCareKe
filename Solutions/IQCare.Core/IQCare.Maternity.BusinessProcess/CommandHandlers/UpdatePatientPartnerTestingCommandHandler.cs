using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Library;
using IQCare.Maternity.BusinessProcess.Commands.PNC;
using IQCare.Maternity.Core.Domain.PNC;
using IQCare.Maternity.Infrastructure.UnitOfWork;
using MediatR;
using Serilog;

namespace IQCare.Maternity.BusinessProcess.CommandHandlers
{
    public class UpdatePatientPartnerTestingCommandHandler : IRequestHandler<UpdatePatientPartnerTestingCommand, Result<UpdatePatientPartnerTestingResponse>>
    {

        IMaternityUnitOfWork _maternityUnitOfWork;
        public UpdatePatientPartnerTestingCommandHandler(IMaternityUnitOfWork maternityUnitOfWork)
        {
            _maternityUnitOfWork = maternityUnitOfWork;
        }


        public async Task<Result<UpdatePatientPartnerTestingResponse>> Handle(UpdatePatientPartnerTestingCommand request, CancellationToken cancellationToken)
        {
            using (_maternityUnitOfWork)
            {
                try
                {
                    var partnerTesting = await _maternityUnitOfWork.Repository<PatientPartnerTesting>().FindByIdAsync(request.Id);
                    if(partnerTesting==null)
                        return Result<UpdatePatientPartnerTestingResponse>.Invalid($"Partner testing for Id: {request.Id} could not be found");

                    partnerTesting.PartnerHIVResult = request.PartnerHIVResult;
                    partnerTesting.PartnerTested = request.PartnerTested;

                    _maternityUnitOfWork.Repository<PatientPartnerTesting>().Update(partnerTesting);
                    await _maternityUnitOfWork.SaveAsync();

                    return Result<UpdatePatientPartnerTestingResponse>.Valid(new UpdatePatientPartnerTestingResponse()
                    {
                        Message = "Successfully updated partnertesting"
                    });
                }
                catch (Exception e)
                {
                    Log.Error($"Error updating patient partner testing for Id: {request.Id}");
                    return Result<UpdatePatientPartnerTestingResponse>.Invalid($"Error updating patient partner testing for Id: {request.Id} " + e.Message);
                }
            }
        }
    }
}
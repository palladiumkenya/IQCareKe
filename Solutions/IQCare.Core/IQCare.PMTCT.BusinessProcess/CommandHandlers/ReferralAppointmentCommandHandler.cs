using IQCare.Library;
using IQCare.PMTCT.BusinessProcess.Commands;
using IQCare.PMTCT.Infrastructure;
using IQCare.PMTCT.Services.Interface;
using MediatR;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers
{
    public class ReferralAppointmentCommandHandler : IRequestHandler<ReferralAppointmentServiceCommand, Result<ReferralAppointmentCommandResponse>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;
        private IReferralAppointmentService _service;
        private int result = 0;

        public ReferralAppointmentCommandHandler(IPmtctUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<ReferralAppointmentCommandResponse>> Handle(ReferralAppointmentServiceCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                   int result1= await _service.AddPatientAppointment(request.patientAppointment);
                   int result2= await _service.AddPatientReferral(request.patientReferral);
                    if(result1>0 & result2>0)
                    {
                        result = 1;
                    }
                    return Result<ReferralAppointmentCommandResponse>.Valid(new ReferralAppointmentCommandResponse() { ReferralAppointmentId = result });
                }
                catch (Exception e)
                {

                    Log.Error(e.Message);
                    throw e;
                }
            }
        }
    }
}

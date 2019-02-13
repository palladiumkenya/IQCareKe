using IQCare.PMTCT.BusinessProcess.Commands;
using IQCare.PMTCT.Infrastructure;
using IQCare.PMTCT.Services.Interface;
using MediatR;
using Serilog;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.PMTCT.Services;
using IQCare.PMTCT.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using PatientAppointment = IQCare.PMTCT.Core.Models.PatientAppointment;

namespace IQCare.PMTCT.BusinessProcess.CommandHandlers
{
    public class ReferralAppointmentCommandHandler : IRequestHandler<ReferralAppointmentServiceCommand, Library.Result<ReferralAppointmentCommandResponse>>
    {
        private readonly IPmtctUnitOfWork _unitOfWork;
        private readonly ICommonUnitOfWork _commonUnitOfWork;
        private int result = 0;

        public ReferralAppointmentCommandHandler(IPmtctUnitOfWork unitOfWork, ICommonUnitOfWork commonUnitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _commonUnitOfWork = commonUnitOfWork ?? throw new ArgumentNullException(nameof(commonUnitOfWork));
        }

        public async Task<Library.Result<ReferralAppointmentCommandResponse>> Handle(ReferralAppointmentServiceCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    int statusId = _commonUnitOfWork.Repository<LookupItem>().Get(x => x.Name == "pending")
                        .Select(x => x.Id).FirstOrDefault();
                    int reasonId = _commonUnitOfWork.Repository<LookupItem>().Get(x => x.Name == "ANC Follow-up")
                        .Select(x => x.Id).FirstOrDefault();

                    PatientReferral referralData = new PatientReferral()
                    {
                        PatientMasterVisitId = request.PatientReferral.PatientMasterVisitId,
                        ReferredFrom = request.PatientReferral.ReferredFrom,
                        ReferredTo = request.PatientReferral.ReferredTo,
                        ReferralReason = request.PatientReferral.ReferralReason,
                        ReferralDate = request.PatientReferral.ReferralDate,
                        ReferredBy = request.PatientReferral.ReferredBy,
                        CreateBy = request.CreatedBy,
                        PatientId = request.PatientReferral.PatientId                        
                    };

                    PatientAppointment appointmentData = new PatientAppointment()
                    {
                        PatientMasterVisitId = request.PatientAppointment.PatientMasterVisitId,
                        PatientId = request.PatientAppointment.PatientId,
                        ServiceAreaId = request.PatientAppointment.ServiceAreaId,
                        AppointmentDate = request.PatientAppointment.AppointmentDate,
                        ReasonId = reasonId,
                        Description = request.PatientAppointment.Description,
                        StatusDate = DateTime.Now,
                        StatusId = statusId,
                        CreatedBy = request.CreatedBy
                    };

                    ReferralAppointmentService _service=new ReferralAppointmentService(_unitOfWork);
                   int result1= await _service.AddPatientAppointment(appointmentData);
                   int result2= await _service.AddPatientReferral(referralData);
                    if(result1>0 & result2>0)
                    {
                        result = 1;
                    }
                    return Library.Result<ReferralAppointmentCommandResponse>.Valid(new ReferralAppointmentCommandResponse() { ReferralAppointmentId = result });
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

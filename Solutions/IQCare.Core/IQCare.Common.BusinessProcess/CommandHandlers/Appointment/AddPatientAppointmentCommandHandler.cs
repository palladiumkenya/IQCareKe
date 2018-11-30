using IQCare.Common.BusinessProcess.Commands.Appointment;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Serilog;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Appointment
{
    public class AddPatientAppointmentCommandHandler : IRequestHandler<AddPatientAppointmentCommand, Result<AddPatientAppointmentResponse>>
    {
        ICommonUnitOfWork _commontUnitOfWork;
        public AddPatientAppointmentCommandHandler(ICommonUnitOfWork commontUnitOfWork)
        {
            _commontUnitOfWork = commontUnitOfWork; 
        }
        public async Task<Result<AddPatientAppointmentResponse>> Handle(AddPatientAppointmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var appointmentStatusId = _commontUnitOfWork.Repository<LookupItem>()
                       .Get(x => x.Name == "Pending").SingleOrDefault()?.Id;

                int? appointmentReasonId = _commontUnitOfWork.Repository<LookupItem>()
                    .Get(x => x.Name == request.AppointmentReason).SingleOrDefault()?.Id;

                var patientAppointment = new PatientAppointment(request.PatientId, request.PatientMasterVisitId, request.ServiceAreaId, request.AppointmentDate, (int)appointmentReasonId, (int)appointmentStatusId, request.DifferentiatedCareId, request.StatusDate, request.CreatedBy, request.Description);

                await _commontUnitOfWork.Repository<PatientAppointment>().AddAsync(patientAppointment);
                await _commontUnitOfWork.SaveAsync();

                return Result<AddPatientAppointmentResponse>.Valid(new AddPatientAppointmentResponse
                {
                    AppointmentId = patientAppointment.Id
                });
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"An error occured while adding patient referral information for patientId {request.PatientId}");
                return Result<AddPatientAppointmentResponse>.Invalid(ex.Message);
            }
        }
    }
}

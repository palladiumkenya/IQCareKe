using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Appointment;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Serilog;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Appointment
{
    public class EditAppointmentCommandHandler:IRequestHandler<EditAppointmentCommand, Result<EditAppointmentCommandResponse>>
    {
        ICommonUnitOfWork _commontUnitOfWork;

        public EditAppointmentCommandHandler(ICommonUnitOfWork commontUnitOfWork)
        {
            _commontUnitOfWork = commontUnitOfWork;
        }

        public async Task<Result<EditAppointmentCommandResponse>> Handle(EditAppointmentCommand request, CancellationToken cancellationToken)
        {
            using (_commontUnitOfWork)
            {
                try
                {
                    if (request.AppointmentId.HasValue)
                    {
                        var appointment = await _commontUnitOfWork.Repository<PatientAppointment>()
                            .FindByIdAsync(request.AppointmentId.Value);
                        if (appointment != null)
                        {
                            appointment.AppointmentDate = request.AppointmentDate;
                            appointment.Description = request.Description;

                            _commontUnitOfWork.Repository<PatientAppointment>().Update(appointment);
                            await _commontUnitOfWork.SaveAsync();

                            return Result<EditAppointmentCommandResponse>.Valid(new EditAppointmentCommandResponse()
                            {
                                Message = "Appointment updated successfully"
                            });
                        }
                        else
                        {
                            return Result<EditAppointmentCommandResponse>.Invalid("Error updating appointment for appointmentid: " + request.AppointmentId);
                        }
                    }
                    else
                    {
                        PatientAppointment patientAppointment = new PatientAppointment()
                        {
                            PatientId = request.PatientId,
                            PatientMasterVisitId = request.PatientMasterVisitId,
                            AppointmentDate = request.AppointmentDate,
                            DeleteFlag = false,
                            CreateDate = DateTime.Now,
                            CreatedBy = request.UserId,
                            Description = request.Description,
                            DifferentiatedCareId = request.DifferentiatedCareId,
                            ReasonId = request.ReasonId,
                            ServiceAreaId = request.ServiceAreaId,
                            StatusDate = null,
                            StatusId = request.StatusId
                        };

                        await _commontUnitOfWork.Repository<PatientAppointment>().AddAsync(patientAppointment);
                        await _commontUnitOfWork.SaveAsync();

                        return Result<EditAppointmentCommandResponse>.Valid(new EditAppointmentCommandResponse()
                        {
                            Message = "Appointment updated successfully"
                        });
                    }

                    //var appointment = await _commontUnitOfWork.Repository<PatientAppointment>().FindByIdAsync(request.AppointmentId);
                    //if (appointment != null)
                    //{
                    //    appointment.AppointmentDate = request.AppointmentDate;
                    //    appointment.Description = request.Description;

                    //    _commontUnitOfWork.Repository<PatientAppointment>().Update(appointment);
                    //    await _commontUnitOfWork.SaveAsync();

                    //    return Result<EditAppointmentCommandResponse>.Valid(new EditAppointmentCommandResponse()
                    //    {
                    //        Message = "Appointment updated successfully"
                    //    });
                    //}
                    //else
                    //{
                    //    return Result<EditAppointmentCommandResponse>.Invalid("Error updating appointment for appointmentid: " + request.AppointmentId);
                    //}
                }
                catch (Exception e)
                {
                    Log.Error("Error updating patient appointment " + e.Message + " " + e.InnerException);
                    return Result<EditAppointmentCommandResponse>.Invalid("Error updating patient appointment " + e.Message);
                }
            }
        }
    }
}
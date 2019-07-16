using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Appointment;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Appointment
{
    public class AddReasonAppoinmentNotGivenCommandHandler : IRequestHandler<AddReasonAppoinmentNotGivenCommand, Result<AddReasonAppointmentNotGivenResponse>>
    {
        ICommonUnitOfWork _commontUnitOfWork;
        public AddReasonAppoinmentNotGivenCommandHandler(ICommonUnitOfWork commontUnitOfWork)
        {
            _commontUnitOfWork = commontUnitOfWork;
        }

        public async Task<Result<AddReasonAppointmentNotGivenResponse>> Handle(AddReasonAppoinmentNotGivenCommand request, CancellationToken cancellationToken)
        {
            using (_commontUnitOfWork)
            {
                try
                {
                    var result = await _commontUnitOfWork.Repository<PatientAppointmentReasons>().Get(x =>
                            x.PatientId == request.PatientId && x.PatientMasterVisitId == request.PatientMasterVisitId)
                        .ToListAsync();

                    if (result.Count > 0)
                    {
                        result[0].ReasonAppointmentNotGiven = request.ReasonAppointmentNotGiven;

                        _commontUnitOfWork.Repository<PatientAppointmentReasons>().Update(result[0]);
                        await _commontUnitOfWork.SaveAsync();
                    }
                    else
                    {
                        PatientAppointmentReasons patientAppointmentReasons = new PatientAppointmentReasons()
                        {
                            PatientId = request.PatientId,
                            PatientMasterVisitId = request.PatientMasterVisitId,
                            ReasonAppointmentNotGiven = request.ReasonAppointmentNotGiven
                        };

                        await _commontUnitOfWork.Repository<PatientAppointmentReasons>()
                            .AddAsync(patientAppointmentReasons);
                        await _commontUnitOfWork.SaveAsync();
                    }

                    return Result<AddReasonAppointmentNotGivenResponse>.Valid(new AddReasonAppointmentNotGivenResponse()
                    {
                        Message = "Successfully added patient appointment not given reason"
                    });
                }
                catch (Exception e)
                {
                    Log.Error(e, $"An error occured while adding patient appointment reasons information for patientId {request.PatientId}");
                    return Result<AddReasonAppointmentNotGivenResponse>.Invalid($"An error occured while adding patient appointment reasons information for patientId {request.PatientId}");
                }
            }
        }
    }
}
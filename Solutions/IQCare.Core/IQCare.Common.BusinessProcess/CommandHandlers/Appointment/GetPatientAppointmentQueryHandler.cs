using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using IQCare.Common.BusinessProcess.Commands.Allergies;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Serilog;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Appointment;
using Microsoft.EntityFrameworkCore;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Appointment
{
    public class GetPatientAppointmentQueryHandler : IRequestHandler<GetPatientAppointmentViewQuery, Result<List<PatientAppointmentMethodViewModel>>>
    {
        ICommonUnitOfWork _commontUnitOfWork;
        IMapper _mapper;
        ILogger logger = Log.ForContext<GetPatientAppointmentQueryHandler>();
        public GetPatientAppointmentQueryHandler(ICommonUnitOfWork commontUnitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _commontUnitOfWork = commontUnitOfWork;
        }
        public Task<Result<List<PatientAppointmentMethodViewModel>>> Handle(GetPatientAppointmentViewQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var patientAppoitment = _commontUnitOfWork.Repository<Api_PatientAppointmentsView>().Get(x => x.PatientId == request.PatientId);
                patientAppoitment.ToList().ForEach(item => item.ConvertToDate());
                var patientAppointmentViewModel = _mapper.Map<List<PatientAppointmentMethodViewModel>>(patientAppoitment);
                return Task.FromResult(Result<List<PatientAppointmentMethodViewModel>>.Valid(patientAppointmentViewModel));
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"An error occured while getting patient appointment details{ request.PatientId}");
                return Task.FromResult(Result<List<PatientAppointmentMethodViewModel>>.Invalid(ex.Message));
            }

        }
    }
}
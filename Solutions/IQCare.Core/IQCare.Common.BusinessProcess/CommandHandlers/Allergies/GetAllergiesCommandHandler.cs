using AutoMapper;
using IQCare.Common.BusinessProcess.Commands.Allergies;
using IQCare.Common.BusinessProcess.Commands.Appointment;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Allergies
{
    public class GetAllergiesCommandHandler : IRequestHandler<GetPatientAllergies, Result<List<PatientAllergiesViewModel>>>
    {
        ICommonUnitOfWork _commontUnitOfWork;
        IMapper _mapper;
        ILogger logger = Log.ForContext<GetAllergiesCommandHandler>();
        public GetAllergiesCommandHandler(ICommonUnitOfWork commontUnitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _commontUnitOfWork = commontUnitOfWork;
        }
        public Task<Result<List<PatientAllergiesViewModel>>> Handle(GetPatientAllergies request, CancellationToken cancellationToken)
        {
            try
            {
                var patientAllergies = _commontUnitOfWork.Repository<PatientAllergy>().Get(x => x.PatientId == request.PatientId);
                var allergiesViewModel = _mapper.Map<List<PatientAllergiesViewModel>>(patientAllergies);

                return Task.FromResult(Result<List<PatientAllergiesViewModel>>.Valid(allergiesViewModel));
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"An error occured while getting patient allergies methods{ request.PatientId}");
                return Task.FromResult(Result<List<PatientAllergiesViewModel>>.Invalid(ex.Message));
            }

        }
    }
}
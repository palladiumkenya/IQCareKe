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
using Microsoft.EntityFrameworkCore;

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
        public async Task<Result<List<PatientAllergiesViewModel>>> Handle(GetPatientAllergies request, CancellationToken cancellationToken)
        {
            try
            {
                var patientAllergies = await _commontUnitOfWork.Repository<PatientAllergyView>()
                    .Get(x => x.PatientId == request.PatientId).ToListAsync();
                var allergiesViewModel = _mapper.Map<List<PatientAllergiesViewModel>>(patientAllergies);

                return Result<List<PatientAllergiesViewModel>>.Valid(allergiesViewModel);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"An error occured while getting patient allergies methods{ request.PatientId}");
                return Result<List<PatientAllergiesViewModel>>.Invalid(ex.Message);
            }

        }
    }
}
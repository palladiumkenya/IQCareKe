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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace IQCare.Common.BusinessProcess.CommandHandlers.Allergies
{
    public class AddAllergiesCommandHandler : IRequestHandler<AddAllergiesCommand, Result<AddPatientAllergiesResponse>>
    {
        ICommonUnitOfWork _commontUnitOfWork;
        IMapper _mapper;
        ILogger logger = Log.ForContext<AddAllergiesCommandHandler>();
        public AddAllergiesCommandHandler(ICommonUnitOfWork commontUnitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _commontUnitOfWork = commontUnitOfWork;
        }
        public async Task<Result<AddPatientAllergiesResponse>> Handle(AddAllergiesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.PatientAllergies.Any())
                {

                    List<PatientAllergy> patientAllergies =
                        _mapper.Map<List<PatientAllergy>>(request.PatientAllergies);

                    await _commontUnitOfWork.Repository<PatientAllergy>().AddRangeAsync(patientAllergies);
                    await _commontUnitOfWork.SaveAsync();
                }

                return Result<AddPatientAllergiesResponse>.Valid(new AddPatientAllergiesResponse
                {
                    Message = "Successfully added patient allergies"
                });
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"An error occured while adding patient Allergy information");
                return Result<AddPatientAllergiesResponse>.Invalid(ex.Message);
            }
        }
    }
}

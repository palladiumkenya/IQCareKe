using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.PersonCommand;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using MediatR;

namespace IQCare.Common.BusinessProcess.CommandHandlers.PersonCommand
{
    public class AddPatientPopulationCommandHandler : IRequestHandler<AddPatientPopulationCommand, Result<AddPatientPopulationResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public AddPatientPopulationCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<AddPatientPopulationResponse>> Handle(AddPatientPopulationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (_unitOfWork)
                {
                    var patientPopulation = new PatientPopulation()
                    {
                        PersonId = request.PersonId,
                        PopulationType = "Key Population",
                        PopulationCategory = request.KeyPopulation,
                        Active = true,
                        DeleteFlag = false,
                        CreatedBy = request.UserId,
                        CreateDate = DateTime.Now
                    };

                    await _unitOfWork.Repository<PatientPopulation>().AddAsync(patientPopulation);
                    await _unitOfWork.SaveAsync();

                    _unitOfWork.Dispose();

                    return Result<AddPatientPopulationResponse>.Valid(new AddPatientPopulationResponse()
                    {
                        PatientPopulationId = patientPopulation.Id
                    });
                }
            }
            catch (Exception e)
            {
                return Result<AddPatientPopulationResponse>.Invalid(e.Message);
            }
        }
    }
}
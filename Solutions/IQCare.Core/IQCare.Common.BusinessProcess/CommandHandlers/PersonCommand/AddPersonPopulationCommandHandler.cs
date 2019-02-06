using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.PersonCommand;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;

namespace IQCare.Common.BusinessProcess.CommandHandlers.PersonCommand
{
    public class AddPersonPopulationCommandHandler : IRequestHandler<AddPersonPopulationCommand, Result<AddPersonPopulationResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public AddPersonPopulationCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<AddPersonPopulationResponse>> Handle(AddPersonPopulationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (_unitOfWork)
                {
                    // Create Person Population
                    List<PersonPopulation> personPopulations = new List<PersonPopulation>();
                    request.Population.ForEach(t => personPopulations.Add(new PersonPopulation
                    {
                        PersonId = request.PersonId,
                        PopulationType = t.PopulationType,
                        PopulationCategory = t.PopulationCategory,
                        Active = true,
                        DeleteFlag = false,
                        CreatedBy = request.UserId,
                        CreateDate = DateTime.Now
                    }));

                    await _unitOfWork.Repository<PersonPopulation>().AddRangeAsync(personPopulations);
                    await _unitOfWork.SaveAsync();

                    // Create Person Priorities
                    List<PersonPriority> personPriorities = new List<PersonPriority>();
                    request.Priority.ForEach(x=>personPriorities.Add(new PersonPriority()
                    {
                        PersonId = request.PersonId,
                        PriorityId = x.PriorityId,
                        DeleteFlag = false,
                        CreatedBy = request.UserId,
                        CreateDate = DateTime.Now
                    }));

                    await _unitOfWork.Repository<PersonPriority>().AddRangeAsync(personPriorities);
                    await _unitOfWork.SaveAsync();

                    _unitOfWork.Dispose();

                    return Result<AddPersonPopulationResponse>.Valid(new AddPersonPopulationResponse()
                    {
                        isSuccessful = true
                    });
                }
            }
            catch (Exception e)
            {
                return Result<AddPersonPopulationResponse>.Invalid(e.Message);
            }
        }
    }
}
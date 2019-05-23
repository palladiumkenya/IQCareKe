using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.PersonCommand;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
                    // Check if Person Population exists
                    var existingPersonPopulations = await _unitOfWork.Repository<PersonPopulation>()
                        .Get(x => x.PersonId == request.PersonId && x.DeleteFlag == false).ToListAsync();
                    if (existingPersonPopulations.Count > 0)
                    {
                        existingPersonPopulations.ForEach(u => u.DeleteFlag = true);

                        _unitOfWork.Repository<PersonPopulation>().UpdateRange(existingPersonPopulations);
                        await _unitOfWork.SaveAsync();

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
                    }
                    else
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
                    }

                    //Check if Person Priorities exists
                    var existingPersonPriorities = await _unitOfWork.Repository<PersonPriority>()
                        .Get(x => x.PersonId == request.PersonId && x.DeleteFlag == false).ToListAsync();
                    if (existingPersonPriorities.Count > 0)
                    {
                        existingPersonPriorities.ForEach(u => u.DeleteFlag = true);

                        _unitOfWork.Repository<PersonPriority>().UpdateRange(existingPersonPriorities);
                        await _unitOfWork.SaveAsync();

                        // Create Person Priorities
                        List<PersonPriority> personPriorities = new List<PersonPriority>();
                        request.Priority.ForEach(x => personPriorities.Add(new PersonPriority()
                        {
                            PersonId = request.PersonId,
                            PriorityId = x.PriorityId,
                            DeleteFlag = false,
                            CreatedBy = request.UserId,
                            CreateDate = DateTime.Now
                        }));

                        await _unitOfWork.Repository<PersonPriority>().AddRangeAsync(personPriorities);
                        await _unitOfWork.SaveAsync();
                    }
                    else
                    {
                        // Create Person Priorities
                        List<PersonPriority> personPriorities = new List<PersonPriority>();
                        request.Priority.ForEach(x => personPriorities.Add(new PersonPriority()
                        {
                            PersonId = request.PersonId,
                            PriorityId = x.PriorityId,
                            DeleteFlag = false,
                            CreatedBy = request.UserId,
                            CreateDate = DateTime.Now
                        }));

                        await _unitOfWork.Repository<PersonPriority>().AddRangeAsync(personPriorities);
                        await _unitOfWork.SaveAsync();
                    }

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
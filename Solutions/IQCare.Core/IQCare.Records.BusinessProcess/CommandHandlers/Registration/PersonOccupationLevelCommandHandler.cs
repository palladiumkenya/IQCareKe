using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Common.Services;
using IQCare.Library;
using IQCareRecords.Common.BusinessProcess.Command;
using MediatR;
namespace IQCareRecords.Common.BusinessProcess.CommandHandlers.Registration
{
    public class PersonOccupationLevelCommandHandler:IRequestHandler<PersonOccupationLevelCommand,Result<AddPersonOccupationLevelResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public PersonOccupationLevelCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<AddPersonOccupationLevelResponse>>Handle(PersonOccupationLevelCommand request,CancellationToken cancellationToken)
        {
            try
            {
                PersonOccupationService personOccupationService = new PersonOccupationService(_unitOfWork);
                List<PersonOccupation> personOccupations = await personOccupationService.GetCurrentOccupation(request.PersonId);
                PersonOccupation personOccupation = new PersonOccupation();
                if (personOccupations.Count > 0)
                {
                    personOccupations[0].Occupation = request.Occupation;
                    personOccupation = await personOccupationService.Update(personOccupations[0]);
                }
                else
                {
                    PersonOccupation pc = new PersonOccupation()
                    {
                        PersonId = request.PersonId,
                        Occupation = request.Occupation,
                        CreateDate = DateTime.Now,
                        CreatedBy = request.UserId,
                        Active = false,
                        DeleteFlag = false
                    };
                    personOccupation = await personOccupationService.Add(pc);
                }

                return Result<AddPersonOccupationLevelResponse>.Valid(new AddPersonOccupationLevelResponse()
                {
                    Message = "Success",
                    OccupationId = personOccupation.Id
                });
            }
            catch(Exception e)
            {
                return Result<AddPersonOccupationLevelResponse>.Invalid(e.Message);
            }
        }
    }   
}

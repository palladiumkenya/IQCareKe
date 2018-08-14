using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Common.Services;
using IQCareRecords.Common.BusinessProcess.Command;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IQCareRecords.Common.BusinessProcess.CommandHandlers.Registration
{
    public class PersonEducationLevelCommandHandler:IRequestHandler<PersonEducationLevelCommand,Result<AddPersonEducationalLevelResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public PersonEducationLevelCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<AddPersonEducationalLevelResponse>>Handle(PersonEducationLevelCommand request,CancellationToken cancellationToken)
        {
            try
            {
                EducationLevelService educationLevelService = new EducationLevelService(_unitOfWork);
                List<PersonEducation> personEducations = await educationLevelService.GetCurrentPersonEducation(request.PersonId);
                PersonEducation personEducation = new PersonEducation();
                if (personEducations.Count > 0)
                {
                    personEducations[0].EducationLevel = request.EducationalLevel;
                    personEducation = await educationLevelService.UpdatePersonEducation(personEducations[0]);
                }
                else
                {
                    PersonEducation ped = new PersonEducation();
                    ped.PersonId = request.PersonId;
                    ped.CreatedBy = request.UserId;
                    ped.EducationLevel = request.EducationalLevel;
                    ped.CreateDate = DateTime.Now;
                    personEducation = await educationLevelService.AddPersonEducation(ped);
                }


                return Result<AddPersonEducationalLevelResponse>.Valid(new AddPersonEducationalLevelResponse()
                {
                    Message = "Success",
                    EducationLevelId = personEducation.Id
                });
            }

            catch (Exception e) {

                return Result<AddPersonEducationalLevelResponse>.Invalid(e.Message);
            }

        }
  }
}
    
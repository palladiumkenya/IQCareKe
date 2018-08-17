using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using IQCareRecords.Common.BusinessProcess.Command;
using IQCare.Common.Core.Models;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.Infrastructure;
using IQCare.Common.BusinessProcess.Services;

namespace IQCareRecords.Common.BusinessProcess.CommandHandlers.Registration
{
  public   class PersonEducationLevelCommandHandler:IRequestHandler<PersonEducationLevelCommand,Result<AddPersonEducationalLevelResponse>>

    {
        public int res;
        public string msg;

        private readonly ICommonUnitOfWork _unitOfWork;
        public PersonEducationLevelCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<Result<AddPersonEducationalLevelResponse>>Handle(PersonEducationLevelCommand request,CancellationToken cancellationToken)
        {
            try {
                RegisterPersonService rs = new RegisterPersonService(_unitOfWork);
            if (request.PersonId > 0)
                {
                    PersonEducation pme = new PersonEducation();
                    pme = await rs.GetCurrentPersonEducation(request.PersonId);
                    if (pme != null)
                    {
                        pme.EducationLevel = request.EducationalLevel;
                        pme.CreatedBy = request.UserId;
                        await rs.UpdatePersonEducation(pme);

                        msg += "Person Educatin updated successfully";
                    }
                    else
                    {
                        PersonEducation ped = new PersonEducation();
                        ped.PersonId = request.PersonId;
                        ped.CreatedBy = request.UserId;
                        ped.EducationLevel = request.EducationalLevel;
                        ped.CreateDate = DateTime.Now;
                        var peducation = await rs.AddPersonEducation(ped);

                        if (peducation != null)
                        {
                            msg = "PersonEducationalLevel added successfully for personId" + request.PersonId;
                        }
                    }

                }


                return Result<AddPersonEducationalLevelResponse>.Valid(new AddPersonEducationalLevelResponse()
                {
                    Message = msg

                });
            }

            catch (Exception e) {

                return Result<AddPersonEducationalLevelResponse>.Invalid(e.Message);
            }

        }
  }
}
    
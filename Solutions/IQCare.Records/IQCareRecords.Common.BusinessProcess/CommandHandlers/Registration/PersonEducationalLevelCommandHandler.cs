using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Entities.Records;
using Entities.Records.Enrollment;
using IQCareRecords.Common.BusinessProcess.Commands;
using IQCare.Records.UILogic;
using MediatR;
namespace IQCareRecords.Common.BusinessProcess.CommandHandlers
{
   public class PersonEducationalLevelCommandHandler:IRequestHandler<PersonEducationLevelCommand,Result<AddPersonEducationalLevelResponse>>
    {
        public int res;
        public string msg;
        public async Task<Result<AddPersonEducationalLevelResponse>>Handle(PersonEducationLevelCommand request,CancellationToken cancellationToken)
        {
            PersonEducation plast = new PersonEducation();
            try
            {
                if(request.PersonId > 0)
                {
                    PersonEducationManager pme = new PersonEducationManager();
                    plast = pme.GetCurrentPersonEducation(request.PersonId);
                    if (plast !=null)
                    {
                        
                      res= await Task.Run(() => pme.UpdatePersonEducationalLevel(request.PersonId, request.UserId, request.EducationalLevel));
    
                        if(res> 0)
                        {
                            msg = "PersonEducationLevel updated successfully";
                        }
                    }
                    else
                    {
                        res = await Task.Run(() => pme.AddPersonEducationLevel(request.PersonId, request.UserId, request.EducationalLevel));

                        if(res > 0)
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
            catch (Exception e){

                return Result<AddPersonEducationalLevelResponse>.Invalid(e.Message);
            }

            

        }

    }
}

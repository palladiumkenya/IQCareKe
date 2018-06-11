using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCareRecords.Common.BusinessProcess.Command;
using MediatR;
namespace IQCareRecords.Common.BusinessProcess.CommandHandlers.Registration
{
    public class PersonOccupationLevelCommandHandler:IRequestHandler<PersonOccupationLevelCommand,Result<AddPersonOccupationLevelResponse>>
    {
        public int res;
        public string msg;
        private readonly ICommonUnitOfWork _unitOfWork;
        public PersonOccupationLevelCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<AddPersonOccupationLevelResponse>>Handle(PersonOccupationLevelCommand request,CancellationToken cancellationToken)
        {
            try
            {
                RegisterPersonService sc = new RegisterPersonService(_unitOfWork);
                if (request.PersonId > 0)
                {
                    PersonOccupation pmo = new PersonOccupation();
                    pmo = await  sc.GetCurrentOccupation(request.PersonId);
                    if (pmo != null)
                    {
                      pmo.DeleteFlag = true;
                        var pm =await sc.UpdateOccupation(pmo);

                      
                        if (pm != null)
                        {
                            msg = "PersonOccupation Updated successfully";
                        }
                    }
                    else
                    {
                        var AddedPersonOcc = await  sc.AddPersonOccupation( request.UserId, request.Occupation, request.PersonId);
                        if (AddedPersonOcc != null)
                        {
                            msg = "PersonOccupation Added  successfully for personId" + request.PersonId;
                        }
                    }

                }
                return Result<AddPersonOccupationLevelResponse>.Valid(new AddPersonOccupationLevelResponse()
                {

                    Message = msg
                });
            }
            catch(Exception e)
            {
                return Result<AddPersonOccupationLevelResponse>.Invalid(e.Message);
            }
            }

        }   
    }

using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCareRecords.Common.BusinessProcess.Command;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IQCareRecords.Common.BusinessProcess.CommandHandlers.Registration
{
    public class AddUpdatePersonContactCommandHandler : IRequestHandler<AddUpdatePersonContactCommand, Result<AddUpdatePersonContactResponse>>
    {
        public int res;
        public string msg;

        private readonly ICommonUnitOfWork _unitOfWork;
        public AddUpdatePersonContactCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<Result<AddUpdatePersonContactResponse>> Handle(AddUpdatePersonContactCommand request, CancellationToken cancellationToken)
        {
            try
            {
                RegisterPersonService rs = new RegisterPersonService(_unitOfWork);
                if(request.PersonId > 0)
                {
                    PersonContact pc = new PersonContact();
                     pc = await rs.GetPersonContactByPersonId(request.PersonId);
                    if (pc != null)
                    {
                        pc.MobileNumber = request.MobileNumber;
                        pc.AlternativeNumber = request.AlternativeNumber;
                        pc.EmailAddress = request.EmailAddress;
                        pc.PersonId = request.PersonId;

                        var personcontact = await rs.UpdatePersonContact(pc.PersonId, pc.MobileNumber, pc.AlternativeNumber, pc.Id);
                        if(personcontact !=null)
                        {
                            res = personcontact.Id;
                            msg += "Person Contact Updated Successfully";
                        }
                    }
                    else
                    {
                        var personcontact = await rs.addPersonContact(request.PersonId, request.PhysicalAddress, request.MobileNumber, request.AlternativeNumber, request.EmailAddress, request.UserId);
                        if (personcontact != null)
                        {
                            res = personcontact.Id;
                            msg += "Person Contact Added Successfully";
                        }
                    }


                }
                else
                {
                  var personcontact=  await rs.addPersonContact(request.PersonId,request.PhysicalAddress,request.MobileNumber,request.AlternativeNumber,request.EmailAddress,request.UserId);
                      if(personcontact !=null)
                    {
                        res = personcontact.Id;
                        msg += "Person Contact Added Successfully";
                    }
                }


                return Result<AddUpdatePersonContactResponse>.Valid(new AddUpdatePersonContactResponse()
                {
                    PersonContactId=res,
                    Message = msg

                });

            }
            catch(Exception e)
            {
                return Result<AddUpdatePersonContactResponse>.Invalid(e.Message);
            }


        }
    }
}

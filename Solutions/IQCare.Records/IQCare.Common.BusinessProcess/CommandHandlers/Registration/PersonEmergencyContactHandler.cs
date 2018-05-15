using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using IQCare.Records.UILogic;
using IQCare.Common.BusinessProcess.Commands;
using System.Threading;
using System.Threading.Tasks;
using Entities.Records;

namespace IQCare.Common.BusinessProcess.CommandHandlers.Registration
{
    public class PersonEmergencyContactHandler : IRequestHandler<PersonEmergencyContactCommand, Result<AddPersonEmergencyContactResponse>>
    {
        public string msg;
        PersonManager pma = new PersonManager();

        public async Task<Result<AddPersonEmergencyContactResponse>> Handle(PersonEmergencyContactCommand request, CancellationToken cancellationToken)
        {

            try
            {
                PersonEmergencyContact pme = new PersonEmergencyContact();

                if (request.PersonId > 0)
                {

                    PersonEmergencyContactManager pmme = new PersonEmergencyContactManager();
                    if (request.Id != null || request.Id != 0)
                    {
                        PersonEmergencyContact listEmergencyContact = await Task.Run(() => pmme.GetSpecificEmergencyContact(Convert.ToInt32(request.Id), request.PersonId));
                        if (listEmergencyContact != null)
                        {
                            pma.UpdatePerson(request.firstname, request.middlename, request.lastname, request.gender, request.CreatedBy, listEmergencyContact.EmergencyContactPersonId);
                            PersonEmergencyContact supporter = new PersonEmergencyContact()
                            {
                                Id = listEmergencyContact.Id,
                                PersonId = request.PersonId,
                                EmergencyContactPersonId = request.EmergencyContactPersonId,
                                MobileContact = request.MobileContact,
                                CreatedBy = request.CreatedBy,
                                DeleteFlag = listEmergencyContact.DeleteFlag

                            };
                            pmme.UpdatePersonEmergencyContact(supporter);

                            msg += "Person Emergency Contact Updated Successfully";


                        }
                    }

                    else
                    {
                        int personEmergencyContactId = await Task.Run(() => pma.AddPersonEmergencySupporterUiLogic(request.firstname, request.middlename, request.lastname, request.gender, request.CreatedBy));
                        if (personEmergencyContactId > 0)
                        {
                            msg += "New Person EmergencyContact Added successfully";
                        }
                    }

                    
                }


                return Result<AddPersonEmergencyContactResponse>.Valid(new AddPersonEmergencyContactResponse()
                {

                    Message = msg

                });
            }
            catch (Exception e)
            {

                return Result<AddPersonEmergencyContactResponse>.Invalid(e.Message);
            }

        }

    }
}
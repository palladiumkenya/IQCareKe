using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using IQCareRecords.Common.BusinessProcess.Command;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Services;
using IQCare.Library;
using Microsoft.EntityFrameworkCore;

namespace IQCareRecords.Common.BusinessProcess.CommandHandlers.Registration
{
    public class PersonEmergencyContactCommandHandler : IRequestHandler<PersonEmergencyContactCommand, Result<AddPersonEmergencyContactResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public PersonEmergencyContactCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<AddPersonEmergencyContactResponse>> Handle(PersonEmergencyContactCommand request, CancellationToken cancellationToken)
        {
            try
            {
                RegisterPersonService registerPersonService = new RegisterPersonService(_unitOfWork);
                PersonContactsService personContactsService = new PersonContactsService(_unitOfWork);

                for (int i = 0; i < request.Emergencycontact.Count; i++)
                {
                    Facility clientFacility = await _unitOfWork.Repository<Facility>().Get(x => x.PosID == request.Emergencycontact[i].PosId.ToString()).FirstOrDefaultAsync();
                    if (clientFacility == null)
                    {
                        clientFacility = await _unitOfWork.Repository<Facility>().Get(x => x.DeleteFlag == 0).FirstOrDefaultAsync();
                    }

                    int emergencyPersonId = 0;
                    if (request.Emergencycontact[i].RegisteredPersonId > 0)
                    {
                        emergencyPersonId = request.Emergencycontact[i].RegisteredPersonId;
                    }
                    else
                    {
                        //add new person 
                        var contactPerson = await registerPersonService.RegisterPerson(request.Emergencycontact[i].Firstname, request.Emergencycontact[i].Middlename,
                            request.Emergencycontact[i].Lastname, request.Emergencycontact[i].Gender, request.Emergencycontact[i].CreatedBy, clientFacility.FacilityID, null, DateTime.Now);
                        emergencyPersonId = contactPerson.Id;
                    }
                    

                    //make the person an emergency contact
                    await personContactsService.Add(request.Emergencycontact[i].PersonId, emergencyPersonId,
                        request.Emergencycontact[i].CreatedBy, request.Emergencycontact[i].ContactCategory,
                        request.Emergencycontact[i].RelationshipType);

                    //add the person mobile contact
                    await registerPersonService.addPersonContact(emergencyPersonId, "", request.Emergencycontact[i].MobileContact,
                        "", "", request.Emergencycontact[i].CreatedBy);

                    var consentTypeList = await _unitOfWork.Repository<LookupItemView>()
                        .Get(x => x.MasterName == "ConsentType" && x.ItemName == "ConsentToSendSMS").ToListAsync();

                    int consentType = 0;
                    if (consentTypeList.Count > 0)
                    {
                        consentType = consentTypeList[0].ItemId;
                    }

                    //add person consent to sms
                    PatientConsent patientConsent = new PatientConsent()
                    {
                        PatientMasterVisitId = 0,
                        PatientId = 0,
                        ServiceAreaId = 0,
                        ConsentType = consentType,
                        ConsentValue = request.Emergencycontact[i].Consent,
                        ConsentDate = DateTime.Now,
                        DeclineReason = null,
                        DeleteFlag = false,
                        CreatedBy = request.Emergencycontact[i].CreatedBy,
                        CreateDate = DateTime.Now,
                        PersonId = emergencyPersonId,
                        Comments = request.Emergencycontact[i].ConsentDecline
                    };

                    await registerPersonService.AddPatientConsent(patientConsent);
                }

                return Result<AddPersonEmergencyContactResponse>.Valid(new AddPersonEmergencyContactResponse()
                {
                    Message = "Successfully registered emergency contact",
                    PersonEmergencyContactId = 1
                });
            }
            catch (Exception e)
            {
                return Result<AddPersonEmergencyContactResponse>.Invalid(e.Message);
            }
        }
    }
}

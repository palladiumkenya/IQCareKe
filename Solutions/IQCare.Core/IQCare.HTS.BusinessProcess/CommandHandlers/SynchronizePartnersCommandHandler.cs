using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.HTS.BusinessProcess.Commands;
using IQCare.HTS.Infrastructure;
using MediatR;

namespace IQCare.HTS.BusinessProcess.CommandHandlers
{
    public class SynchronizePartnersCommandHandler : IRequestHandler<SynchronizePartnersCommand, Result<SynchronizePartnersResponse>>
    {

        private readonly ICommonUnitOfWork _unitOfWork;
        private readonly IHTSUnitOfWork _htsUnitOfWork;

        public SynchronizePartnersCommandHandler(ICommonUnitOfWork commonUnitOfWork, IHTSUnitOfWork htsUnitOfWork)
        {
            _unitOfWork = commonUnitOfWork ?? throw new ArgumentNullException(nameof(commonUnitOfWork));
            _htsUnitOfWork = htsUnitOfWork ?? throw new ArgumentNullException(nameof(htsUnitOfWork));
        }
        public async Task<Result<SynchronizePartnersResponse>> Handle(SynchronizePartnersCommand request, CancellationToken cancellationToken)
        {
            using (_htsUnitOfWork)
            using (_unitOfWork)
            {
                try
                {
                    RegisterPersonService registerPersonService = new RegisterPersonService(_unitOfWork);

                    var facilityId = request.MESSAGE_HEADER.SENDING_FACILITY;
                    for (int i = 0; i < request.PARTNERS.Count; i++)
                    {
                        string afyaMobileId = string.Empty;
                        string indexClientAfyaMobileId = string.Empty;

                        for (int j = 0; j < request.PARTNERS[i].PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID.Count; j++)
                        {
                            if (request.PARTNERS[i].PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID[j].IDENTIFIER_TYPE ==
                                "AFYA_MOBILE_ID")
                            {
                                afyaMobileId = request.PARTNERS[i].PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID[j].ID;
                            }

                            if (request.PARTNERS[i].PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID[j].IDENTIFIER_TYPE ==
                                "INDEX_CLIENT_AFYAMOBILE_ID")
                            {
                                indexClientAfyaMobileId =
                                    request.PARTNERS[i].PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID[j].ID;
                            }
                        }

                        string firstName = request.PARTNERS[i].PATIENT_IDENTIFICATION.PATIENT_NAME.FIRST_NAME;
                        string middleName = request.PARTNERS[i].PATIENT_IDENTIFICATION.PATIENT_NAME.MIDDLE_NAME;
                        string lastName = request.PARTNERS[i].PATIENT_IDENTIFICATION.PATIENT_NAME.LAST_NAME;
                        int sex = request.PARTNERS[i].PATIENT_IDENTIFICATION.SEX;
                        DateTime dateOfBirth = DateTime.ParseExact(request.PARTNERS[i].PATIENT_IDENTIFICATION.DATE_OF_BIRTH, "yyyyMMdd", null);
                        int providerId = Int32.MinValue;
                        int maritalStatusId = request.PARTNERS[i].PATIENT_IDENTIFICATION.MARITAL_STATUS;
                        string mobileNumber = request.PARTNERS[i].PATIENT_IDENTIFICATION.PHONE_NUMBER;
                        string landmark = request.PARTNERS[i].PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS
                            .LANDMARK;
                        int relationshipType = request.PARTNERS[i].PATIENT_IDENTIFICATION.RELATIONSHIP_TYPE;

                        var indexClientIdentifiers = await registerPersonService.getPersonIdentifiers(indexClientAfyaMobileId, 10);
                        if (indexClientIdentifiers.Count > 0)
                        {
                            //Get Index client
                            var indexClient =
                                await registerPersonService.GetPatientByPersonId(indexClientIdentifiers[0].PersonId);
                            //Register Partner
                            var person = await registerPersonService.RegisterPerson(firstName, middleName, lastName, sex, dateOfBirth, providerId);
                            //Add afyamobile Id as an Id of the partner
                            var personIdentifier = await registerPersonService.addPersonIdentifiers(person.Id, 10, afyaMobileId, providerId);
                            //Add partner marital status
                            var partnerMaritalStatus = await registerPersonService.AddMaritalStatus(person.Id, maritalStatusId, providerId);
                            //add partner contacts
                            var partnerContacts = await registerPersonService.addPersonContact(person.Id, null,
                                mobileNumber, null, null, providerId);
                            //add partner location
                            var partnerLocation =
                                await registerPersonService.addPersonLocation(person.Id, 0, 0, 0, null, landmark,
                                    providerId);
                            //Add PersonRelationship
                            var personRelationship = await registerPersonService.addPersonRelationship(person.Id, indexClient.Id, relationshipType, providerId);


                            /***
                             *Encounter
                             */

                        }
                        
                    }

                    return Result<SynchronizePartnersResponse>.Valid(new SynchronizePartnersResponse()
                    {

                    });
                }
                catch (Exception e)
                {
                    return Result<SynchronizePartnersResponse>.Invalid(e.Message);
                }
            }
        }
    }
}
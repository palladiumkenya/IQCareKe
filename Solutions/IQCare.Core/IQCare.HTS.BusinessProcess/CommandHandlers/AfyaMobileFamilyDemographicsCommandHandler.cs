using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.HTS.BusinessProcess.Commands;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;

namespace IQCare.HTS.BusinessProcess.CommandHandlers
{
    public class AfyaMobileFamilyDemographicsCommandHandler: IRequestHandler<AfyaMobileFamilyDemographicsCommand, Result<string>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public AfyaMobileFamilyDemographicsCommandHandler(ICommonUnitOfWork commonUnitOfWork)
        {
            _unitOfWork = commonUnitOfWork ?? throw new ArgumentNullException(nameof(commonUnitOfWork));
        }

        public async Task<Result<string>> Handle(AfyaMobileFamilyDemographicsCommand request, CancellationToken cancellationToken)
        {
            string afyaMobileId = string.Empty;
            string indexClientAfyaMobileId = string.Empty;

            using (var trans = _unitOfWork.Context.Database.BeginTransaction())
            {
                RegisterPersonService registerPersonService = new RegisterPersonService(_unitOfWork);
                var facilityId = request.MESSAGE_HEADER.SENDING_FACILITY;

                try
                {
                    for (int i = 0; i < request.FAMILY.Count; i++)
                    {
                        for (int j = 0; j < request.FAMILY[i].PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID.Count; j++)
                        {
                            if (request.FAMILY[i].PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID[j].IDENTIFIER_TYPE == "AFYA_MOBILE_ID")
                            {
                                afyaMobileId = request.FAMILY[i].PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID[j].ID;
                            }

                            if (request.FAMILY[i].PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID[j].IDENTIFIER_TYPE == "INDEX_CLIENT_AFYAMOBILE_ID")
                            {
                                indexClientAfyaMobileId = request.FAMILY[i].PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID[j].ID;
                            }
                        }

                        var afyaMobileMessage = await registerPersonService.AddAfyaMobileInbox(DateTime.Now, request.MESSAGE_HEADER.MESSAGE_TYPE, afyaMobileId, JsonConvert.SerializeObject(request), false);

                        string firstName = request.FAMILY[i].PATIENT_IDENTIFICATION.PATIENT_NAME.FIRST_NAME;
                        string middleName = request.FAMILY[i].PATIENT_IDENTIFICATION.PATIENT_NAME.MIDDLE_NAME;
                        string lastName = request.FAMILY[i].PATIENT_IDENTIFICATION.PATIENT_NAME.LAST_NAME;
                        int sex = request.FAMILY[i].PATIENT_IDENTIFICATION.SEX;
                        DateTime dateOfBirth = DateTime.ParseExact(request.FAMILY[i].PATIENT_IDENTIFICATION.DATE_OF_BIRTH, "yyyyMMdd", null);
                        int providerId = request.FAMILY[i].PATIENT_IDENTIFICATION.USER_ID;
                        int maritalStatusId = request.FAMILY[i].PATIENT_IDENTIFICATION.MARITAL_STATUS;
                        string mobileNumber = request.FAMILY[i].PATIENT_IDENTIFICATION.PHONE_NUMBER;

                        string landmark = request.FAMILY[i].PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.LANDMARK;
                        int countyId = request.FAMILY[i].PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.COUNTY;
                        int subCountyId = request.FAMILY[i].PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.SUB_COUNTY;
                        int wardId = request.FAMILY[i].PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.WARD;


                        int relationshipType = request.FAMILY[i].PATIENT_IDENTIFICATION.RELATIONSHIP_TYPE;

                        Facility clientFacility = await _unitOfWork.Repository<Facility>().Get(x => x.PosID == facilityId).FirstOrDefaultAsync();
                        if (clientFacility == null)
                        {
                            clientFacility = await _unitOfWork.Repository<Facility>().Get(x => x.DeleteFlag == 0).FirstOrDefaultAsync();
                        }

                        var indexClientIdentifiers = await registerPersonService.getPersonIdentifiers(indexClientAfyaMobileId, 10);
                        if (indexClientIdentifiers.Count > 0)
                        {
                            //Get Index client
                            var indexClient = await registerPersonService.GetPatientByPersonId(indexClientIdentifiers[0].PersonId);
                            var partnetPersonIdentifiers = await registerPersonService.getPersonIdentifiers(afyaMobileId, 10);
                            if (partnetPersonIdentifiers.Count > 0)
                            {
                                await registerPersonService.UpdatePerson(partnetPersonIdentifiers[0].PersonId, firstName, middleName, lastName, sex, dateOfBirth, clientFacility.FacilityID);
                                //update maritalstatus id
                                await registerPersonService.UpdateMaritalStatus(partnetPersonIdentifiers[0].PersonId, maritalStatusId);

                                if (!string.IsNullOrWhiteSpace(mobileNumber))
                                    await registerPersonService.UpdatePersonContact(partnetPersonIdentifiers[0].PersonId, null, mobileNumber);
                                if (!string.IsNullOrWhiteSpace(landmark))
                                    await registerPersonService.UpdatePersonLocation(partnetPersonIdentifiers[0].PersonId, landmark);

                                var getPersonRelationship = await registerPersonService.GetPersonRelationshipByPatientIdPersonId(indexClient.Id, partnetPersonIdentifiers[0].PersonId);
                                if (getPersonRelationship != null)
                                {
                                    getPersonRelationship.RelationshipTypeId = relationshipType;
                                    var updatedRelationship = await registerPersonService.UpdatePersonRelationship(getPersonRelationship);
                                }
                                else
                                {
                                    //Add PersonRelationship
                                    var personRelationship = await registerPersonService.addPersonRelationship(partnetPersonIdentifiers[0].PersonId, indexClient.Id, relationshipType, providerId);
                                }
                            }
                            else
                            {
                                //Register Partner
                                var person = await registerPersonService.RegisterPerson(firstName, middleName, lastName, sex, providerId, clientFacility.FacilityID, dateOfBirth);
                                //Add afyamobile Id as an Id of the partner
                                var personIdentifier = await registerPersonService.addPersonIdentifiers(person.Id, 10, afyaMobileId, providerId);
                                //Add partner marital status
                                var partnerMaritalStatus = await registerPersonService.AddMaritalStatus(person.Id, maritalStatusId, providerId);
                                //add partner contacts
                                if (!string.IsNullOrWhiteSpace(mobileNumber))
                                {
                                    var partnerContacts = await registerPersonService.addPersonContact(person.Id, null, mobileNumber, null, null, providerId);
                                }

                                //add partner location
                                if (!string.IsNullOrWhiteSpace(landmark) || (countyId > 0) || (subCountyId > 0) || (wardId > 0))
                                {
                                    landmark = string.IsNullOrWhiteSpace(landmark) ? "" : landmark;
                                    var partnerLocation = await registerPersonService.addPersonLocation(person.Id, countyId, subCountyId, wardId, " ", landmark, providerId);
                                }

                                //Add PersonRelationship
                                var personRelationship = await registerPersonService.addPersonRelationship(person.Id, indexClient.Id, relationshipType, providerId);
                            }
                        }
                        else
                        {
                            //update message has been processed
                            await registerPersonService.UpdateAfyaMobileInbox(afyaMobileMessage.Id, afyaMobileId, true, DateTime.Now, $"Index clientid: {indexClientAfyaMobileId} for familyid: {afyaMobileId} not found", false);
                            return Result<string>.Invalid($"Index clientid: {indexClientAfyaMobileId} for familyid: {afyaMobileId} not found");
                        }

                        //update message has been processed
                        await registerPersonService.UpdateAfyaMobileInbox(afyaMobileMessage.Id, afyaMobileId, true, DateTime.Now, "success", true);
                    }

                    trans.Commit();
                    return Result<string>.Valid($"Successfully synchronized family: {afyaMobileId}");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    Log.Error($"Failed to synchronize family: {afyaMobileId} for clientid: {indexClientAfyaMobileId} " + ex.Message + " " + ex.InnerException);
                    return Result<string>.Invalid($"Failed to synchronize family: {afyaMobileId} for clientid: {indexClientAfyaMobileId} " + ex.Message + " " + ex.InnerException);
                }
            }
        }
    }
}
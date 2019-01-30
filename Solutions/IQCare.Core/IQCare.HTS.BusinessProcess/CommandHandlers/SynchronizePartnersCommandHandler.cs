using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Encounter;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.HTS.BusinessProcess.Commands;
using IQCare.HTS.BusinessProcess.Services;
using IQCare.HTS.Infrastructure;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;

namespace IQCare.HTS.BusinessProcess.CommandHandlers
{
    public class SynchronizePartnersCommandHandler : IRequestHandler<SynchronizePartnersCommand, Result<string>>
    {

        private readonly ICommonUnitOfWork _unitOfWork;
        private readonly IHTSUnitOfWork _htsUnitOfWork;

        public SynchronizePartnersCommandHandler(ICommonUnitOfWork commonUnitOfWork, IHTSUnitOfWork htsUnitOfWork)
        {
            _unitOfWork = commonUnitOfWork ?? throw new ArgumentNullException(nameof(commonUnitOfWork));
            _htsUnitOfWork = htsUnitOfWork ?? throw new ArgumentNullException(nameof(htsUnitOfWork));
        }
        public async Task<Result<string>> Handle(SynchronizePartnersCommand request, CancellationToken cancellationToken)
        {
            using (_htsUnitOfWork)
            using (_unitOfWork)
            {
                string afyaMobileId = string.Empty;
                string indexClientAfyaMobileId = string.Empty;

                RegisterPersonService registerPersonService = new RegisterPersonService(_unitOfWork);
                EncounterTestingService encounterTestingService = new EncounterTestingService(_unitOfWork, _htsUnitOfWork);

                var facilityId = request.MESSAGE_HEADER.SENDING_FACILITY;
                for (int i = 0; i < request.PARTNERS.Count; i++)
                {

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

                    var afyaMobileMessage = await registerPersonService.AddAfyaMobileInbox(DateTime.Now, request.MESSAGE_HEADER.MESSAGE_TYPE, indexClientAfyaMobileId, JsonConvert.SerializeObject(request), false);
                    try
                    {
                        string firstName = request.PARTNERS[i].PATIENT_IDENTIFICATION.PATIENT_NAME.FIRST_NAME;
                        string middleName = request.PARTNERS[i].PATIENT_IDENTIFICATION.PATIENT_NAME.MIDDLE_NAME;
                        string lastName = request.PARTNERS[i].PATIENT_IDENTIFICATION.PATIENT_NAME.LAST_NAME;
                        int sex = request.PARTNERS[i].PATIENT_IDENTIFICATION.SEX;
                        DateTime dateOfBirth = DateTime.ParseExact(request.PARTNERS[i].PATIENT_IDENTIFICATION.DATE_OF_BIRTH, "yyyyMMdd", null);
                        int providerId = request.PARTNERS[i].PATIENT_IDENTIFICATION.USER_ID;
                        int maritalStatusId = request.PARTNERS[i].PATIENT_IDENTIFICATION.MARITAL_STATUS;
                        string mobileNumber = request.PARTNERS[i].PATIENT_IDENTIFICATION.PHONE_NUMBER;
                        string landmark = request.PARTNERS[i].PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS
                            .LANDMARK;
                        int relationshipType = request.PARTNERS[i].PATIENT_IDENTIFICATION.RELATIONSHIP_TYPE;

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

                                /***
                                    *Encounter
                                    */

                                if (request.PARTNERS[i].ENCOUNTER != null)
                                {
                                    if (request.PARTNERS[i].ENCOUNTER.PARTNER_SCREENING != null)
                                    {
                                        int pnsAccepted = request.PARTNERS[i].ENCOUNTER.PARTNER_SCREENING.PNS_ACCEPTED;
                                        DateTime screeningDate = DateTime.ParseExact(request.PARTNERS[i].ENCOUNTER.PARTNER_SCREENING.SCREENING_DATE, "yyyyMMdd", null);
                                        int ipvScreeningDone = request.PARTNERS[i].ENCOUNTER.PARTNER_SCREENING.IPV_SCREENING_DONE;
                                        int hurtByPartner = request.PARTNERS[i].ENCOUNTER.PARTNER_SCREENING.HURT_BY_PARTNER;
                                        int threatByPartner = request.PARTNERS[i].ENCOUNTER.PARTNER_SCREENING.THREAT_BY_PARTNER;
                                        int sexualAbuseByPartner = request.PARTNERS[i].ENCOUNTER.PARTNER_SCREENING.SEXUAL_ABUSE_BY_PARTNER;
                                        int ipvOutcome = request.PARTNERS[i].ENCOUNTER.PARTNER_SCREENING.IPV_OUTCOME;
                                        string partnerOccupation = request.PARTNERS[i].ENCOUNTER.PARTNER_SCREENING.PARTNER_OCCUPATION;
                                        int partnerRelationship = request.PARTNERS[i].ENCOUNTER.PARTNER_SCREENING.PARTNER_RELATIONSHIP;
                                        int livingWithClient = request.PARTNERS[i].ENCOUNTER.PARTNER_SCREENING.LIVING_WITH_CLIENT;
                                        int hivStatus = request.PARTNERS[i].ENCOUNTER.PARTNER_SCREENING.HIV_STATUS;
                                        int pnsApproach = request.PARTNERS[i].ENCOUNTER.PARTNER_SCREENING.PNS_APPROACH;
                                        int eligibleForHts = request.PARTNERS[i].ENCOUNTER.PARTNER_SCREENING.ELIGIBLE_FOR_HTS;
                                        DateTime bookingDate = DateTime.ParseExact(request.PARTNERS[i].ENCOUNTER.PARTNER_SCREENING.BOOKING_DATE, "yyyyMMdd", null);

                                        var pnsScreeningOptions = await _unitOfWork.Repository<LookupItemView>()
                                            .Get(x => x.MasterName == "PnsScreening").ToListAsync();

                                        List<Screening> newScreenings = new List<Screening>();
                                        for (int j = 0; j < pnsScreeningOptions.Count; j++)
                                        {
                                            if (pnsScreeningOptions[j].ItemName == "EligibleTesting")
                                            {
                                                Screening screening = new Screening()
                                                {
                                                    ScreeningCategoryId = pnsScreeningOptions[j].ItemId,
                                                    ScreeningTypeId = pnsScreeningOptions[j].MasterId,
                                                    ScreeningValueId = eligibleForHts
                                                };
                                                newScreenings.Add(screening);
                                            }
                                            else if (pnsScreeningOptions[j].ItemName == "PNSApproach")
                                            {
                                                Screening screening = new Screening()
                                                {
                                                    ScreeningCategoryId = pnsScreeningOptions[j].ItemId,
                                                    ScreeningTypeId = pnsScreeningOptions[j].MasterId,
                                                    ScreeningValueId = pnsApproach
                                                };
                                                newScreenings.Add(screening);
                                            }
                                            else if (pnsScreeningOptions[j].ItemName == "HIVStatus")
                                            {
                                                Screening screening = new Screening()
                                                {
                                                    ScreeningCategoryId = pnsScreeningOptions[j].ItemId,
                                                    ScreeningTypeId = pnsScreeningOptions[j].MasterId,
                                                    ScreeningValueId = hivStatus
                                                };
                                                newScreenings.Add(screening);
                                            }
                                            else if (pnsScreeningOptions[j].ItemName == "LivingWithClient")
                                            {
                                                Screening screening = new Screening()
                                                {
                                                    ScreeningCategoryId = pnsScreeningOptions[j].ItemId,
                                                    ScreeningTypeId = pnsScreeningOptions[j].MasterId,
                                                    ScreeningValueId = livingWithClient
                                                };
                                                newScreenings.Add(screening);
                                            }
                                            else if (pnsScreeningOptions[j].ItemName == "PnsRelationship")
                                            {
                                                Screening screening = new Screening()
                                                {
                                                    ScreeningCategoryId = pnsScreeningOptions[j].ItemId,
                                                    ScreeningTypeId = pnsScreeningOptions[j].MasterId,
                                                    ScreeningValueId = partnerRelationship
                                                };
                                                newScreenings.Add(screening);
                                            }
                                            else if (pnsScreeningOptions[j].ItemName == "IPVOutcome")
                                            {
                                                Screening screening = new Screening()
                                                {
                                                    ScreeningCategoryId = pnsScreeningOptions[j].ItemId,
                                                    ScreeningTypeId = pnsScreeningOptions[j].MasterId,
                                                    ScreeningValueId = ipvOutcome
                                                };
                                                newScreenings.Add(screening);
                                            }
                                            else if (pnsScreeningOptions[j].ItemName == "PnsForcedSexual")
                                            {
                                                Screening screening = new Screening()
                                                {
                                                    ScreeningCategoryId = pnsScreeningOptions[j].ItemId,
                                                    ScreeningTypeId = pnsScreeningOptions[j].MasterId,
                                                    ScreeningValueId = sexualAbuseByPartner
                                                };
                                                newScreenings.Add(screening);
                                            }
                                            else if (pnsScreeningOptions[j].ItemName == "PnsThreatenedHurt")
                                            {
                                                Screening screening = new Screening()
                                                {
                                                    ScreeningCategoryId = pnsScreeningOptions[j].ItemId,
                                                    ScreeningTypeId = pnsScreeningOptions[j].MasterId,
                                                    ScreeningValueId = threatByPartner
                                                };
                                                newScreenings.Add(screening);
                                            }
                                            else if (pnsScreeningOptions[j].ItemName == "PnsPhysicallyHurt")
                                            {
                                                Screening screening = new Screening()
                                                {
                                                    ScreeningCategoryId = pnsScreeningOptions[j].ItemId,
                                                    ScreeningTypeId = pnsScreeningOptions[j].MasterId,
                                                    ScreeningValueId = hurtByPartner
                                                };
                                                newScreenings.Add(screening);
                                            }
                                        }

                                        var patientMasterVisitEntity = await _unitOfWork.Repository<PatientMasterVisit>()
                                            .Get(x => x.PatientId == indexClient.Id && x.ServiceId == 2).ToListAsync();

                                        int patientMasterVisitId = patientMasterVisitEntity.OrderBy(x => x.Id).FirstOrDefault().Id;

                                        var partnHtsScreenings = await encounterTestingService.AddPartnerScreening(partnetPersonIdentifiers[0].PersonId, indexClient.Id, patientMasterVisitId, partnerOccupation,
                                            screeningDate, bookingDate, newScreenings, providerId);
                                    }

                                    var tracingLookup = await _unitOfWork.Repository<LookupItemView>()
                                        .Get(x => x.MasterName == "TracingType" && x.ItemName == "Family").ToListAsync();

                                    int tracingType = tracingLookup.FirstOrDefault().ItemId;

                                    for (int j = 0; j < request.PARTNERS[i].ENCOUNTER.TRACING.Count; j++)
                                    {
                                        DateTime tracingDate = DateTime.ParseExact(request.PARTNERS[i].ENCOUNTER.TRACING[j].TRACING_DATE, "yyyyMMdd", null);
                                        int mode = request.PARTNERS[i].ENCOUNTER.TRACING[j].TRACING_MODE;
                                        int outcome = request.PARTNERS[i].ENCOUNTER.TRACING[j].TRACING_OUTCOME;
                                        int consent = request.PARTNERS[i].ENCOUNTER.TRACING[j].CONSENT;
                                        DateTime? tracingBookingDate = null;
                                        if (!string.IsNullOrWhiteSpace(request.PARTNERS[i].ENCOUNTER.TRACING[j].BOOKING_DATE))
                                            tracingBookingDate = DateTime.ParseExact(request.PARTNERS[i].ENCOUNTER.TRACING[j].BOOKING_DATE, "yyyyMMdd", null);

                                        var tracingOutcome = await encounterTestingService.addTracing(partnetPersonIdentifiers[0].PersonId, tracingType,
                                            tracingDate, mode, outcome, providerId, null, consent, tracingBookingDate, null);
                                    }
                                }

                                // update message as processed
                                await registerPersonService.UpdateAfyaMobileInbox(afyaMobileMessage.Id, indexClientAfyaMobileId, true, DateTime.Now, "success");
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
                                    var partnerContacts = await registerPersonService.addPersonContact(person.Id, null,
                                        mobileNumber, null, null, providerId);
                                }
                                //add partner location
                                if (!string.IsNullOrWhiteSpace(landmark))
                                {
                                    var partnerLocation =
                                        await registerPersonService.addPersonLocation(person.Id, 0, 0, 0, " ", landmark,
                                            providerId);
                                }

                                //Add PersonRelationship
                                var personRelationship = await registerPersonService.addPersonRelationship(person.Id, indexClient.Id, relationshipType, providerId);


                                /***
                                    *Encounter
                                    */

                                if (request.PARTNERS[i].ENCOUNTER != null)
                                {
                                    if (request.PARTNERS[i].ENCOUNTER.PARTNER_SCREENING != null)
                                    {
                                        int pnsAccepted = request.PARTNERS[i].ENCOUNTER.PARTNER_SCREENING.PNS_ACCEPTED;
                                        DateTime screeningDate = DateTime.ParseExact(request.PARTNERS[i].ENCOUNTER.PARTNER_SCREENING.SCREENING_DATE, "yyyyMMdd", null);
                                        int ipvScreeningDone = request.PARTNERS[i].ENCOUNTER.PARTNER_SCREENING.IPV_SCREENING_DONE;
                                        int hurtByPartner = request.PARTNERS[i].ENCOUNTER.PARTNER_SCREENING.HURT_BY_PARTNER;
                                        int threatByPartner = request.PARTNERS[i].ENCOUNTER.PARTNER_SCREENING.THREAT_BY_PARTNER;
                                        int sexualAbuseByPartner = request.PARTNERS[i].ENCOUNTER.PARTNER_SCREENING.SEXUAL_ABUSE_BY_PARTNER;
                                        int ipvOutcome = request.PARTNERS[i].ENCOUNTER.PARTNER_SCREENING.IPV_OUTCOME;
                                        string partnerOccupation = request.PARTNERS[i].ENCOUNTER.PARTNER_SCREENING.PARTNER_OCCUPATION;
                                        int partnerRelationship = request.PARTNERS[i].ENCOUNTER.PARTNER_SCREENING.PARTNER_RELATIONSHIP;
                                        int livingWithClient = request.PARTNERS[i].ENCOUNTER.PARTNER_SCREENING.LIVING_WITH_CLIENT;
                                        int hivStatus = request.PARTNERS[i].ENCOUNTER.PARTNER_SCREENING.HIV_STATUS;
                                        int pnsApproach = request.PARTNERS[i].ENCOUNTER.PARTNER_SCREENING.PNS_APPROACH;
                                        int eligibleForHts = request.PARTNERS[i].ENCOUNTER.PARTNER_SCREENING.ELIGIBLE_FOR_HTS;
                                        DateTime bookingDate = DateTime.ParseExact(request.PARTNERS[i].ENCOUNTER.PARTNER_SCREENING.BOOKING_DATE, "yyyyMMdd", null);

                                        var pnsScreeningOptions = await _unitOfWork.Repository<LookupItemView>()
                                            .Get(x => x.MasterName == "PnsScreening").ToListAsync();

                                        List<Screening> newScreenings = new List<Screening>();
                                        for (int j = 0; j < pnsScreeningOptions.Count; j++)
                                        {
                                            if (pnsScreeningOptions[j].ItemName == "EligibleTesting")
                                            {
                                                Screening screening = new Screening()
                                                {
                                                    ScreeningCategoryId = pnsScreeningOptions[j].ItemId,
                                                    ScreeningTypeId = pnsScreeningOptions[j].MasterId,
                                                    ScreeningValueId = eligibleForHts
                                                };
                                                newScreenings.Add(screening);
                                            }
                                            else if (pnsScreeningOptions[j].ItemName == "PNSApproach")
                                            {
                                                Screening screening = new Screening()
                                                {
                                                    ScreeningCategoryId = pnsScreeningOptions[j].ItemId,
                                                    ScreeningTypeId = pnsScreeningOptions[j].MasterId,
                                                    ScreeningValueId = pnsApproach
                                                };
                                                newScreenings.Add(screening);
                                            }
                                            else if (pnsScreeningOptions[j].ItemName == "HIVStatus")
                                            {
                                                Screening screening = new Screening()
                                                {
                                                    ScreeningCategoryId = pnsScreeningOptions[j].ItemId,
                                                    ScreeningTypeId = pnsScreeningOptions[j].MasterId,
                                                    ScreeningValueId = hivStatus
                                                };
                                                newScreenings.Add(screening);
                                            }
                                            else if (pnsScreeningOptions[j].ItemName == "LivingWithClient")
                                            {
                                                Screening screening = new Screening()
                                                {
                                                    ScreeningCategoryId = pnsScreeningOptions[j].ItemId,
                                                    ScreeningTypeId = pnsScreeningOptions[j].MasterId,
                                                    ScreeningValueId = livingWithClient
                                                };
                                                newScreenings.Add(screening);
                                            }
                                            else if (pnsScreeningOptions[j].ItemName == "PnsRelationship")
                                            {
                                                Screening screening = new Screening()
                                                {
                                                    ScreeningCategoryId = pnsScreeningOptions[j].ItemId,
                                                    ScreeningTypeId = pnsScreeningOptions[j].MasterId,
                                                    ScreeningValueId = partnerRelationship
                                                };
                                                newScreenings.Add(screening);
                                            }
                                            else if (pnsScreeningOptions[j].ItemName == "IPVOutcome")
                                            {
                                                Screening screening = new Screening()
                                                {
                                                    ScreeningCategoryId = pnsScreeningOptions[j].ItemId,
                                                    ScreeningTypeId = pnsScreeningOptions[j].MasterId,
                                                    ScreeningValueId = ipvOutcome
                                                };
                                                newScreenings.Add(screening);
                                            }
                                            else if (pnsScreeningOptions[j].ItemName == "PnsForcedSexual")
                                            {
                                                Screening screening = new Screening()
                                                {
                                                    ScreeningCategoryId = pnsScreeningOptions[j].ItemId,
                                                    ScreeningTypeId = pnsScreeningOptions[j].MasterId,
                                                    ScreeningValueId = sexualAbuseByPartner
                                                };
                                                newScreenings.Add(screening);
                                            }
                                            else if (pnsScreeningOptions[j].ItemName == "PnsThreatenedHurt")
                                            {
                                                Screening screening = new Screening()
                                                {
                                                    ScreeningCategoryId = pnsScreeningOptions[j].ItemId,
                                                    ScreeningTypeId = pnsScreeningOptions[j].MasterId,
                                                    ScreeningValueId = threatByPartner
                                                };
                                                newScreenings.Add(screening);
                                            }
                                            else if (pnsScreeningOptions[j].ItemName == "PnsPhysicallyHurt")
                                            {
                                                Screening screening = new Screening()
                                                {
                                                    ScreeningCategoryId = pnsScreeningOptions[j].ItemId,
                                                    ScreeningTypeId = pnsScreeningOptions[j].MasterId,
                                                    ScreeningValueId = hurtByPartner
                                                };
                                                newScreenings.Add(screening);
                                            }
                                        }

                                        var patientMasterVisitEntity = await _unitOfWork.Repository<PatientMasterVisit>()
                                            .Get(x => x.PatientId == indexClient.Id && x.ServiceId == 2).ToListAsync();

                                        int patientMasterVisitId = patientMasterVisitEntity.OrderBy(x => x.Id).FirstOrDefault().Id;

                                        var partnHtsScreenings = await encounterTestingService.AddPartnerScreening(person.Id, indexClient.Id, patientMasterVisitId, partnerOccupation,
                                            screeningDate, bookingDate, newScreenings, providerId);
                                    }

                                    var tracingLookup = await _unitOfWork.Repository<LookupItemView>()
                                        .Get(x => x.MasterName == "TracingType" && x.ItemName == "Family").ToListAsync();

                                    int tracingType = tracingLookup.FirstOrDefault().ItemId;

                                    for (int j = 0; j < request.PARTNERS[i].ENCOUNTER.TRACING.Count; j++)
                                    {
                                        DateTime tracingDate = DateTime.ParseExact(request.PARTNERS[i].ENCOUNTER.TRACING[j].TRACING_DATE, "yyyyMMdd", null);
                                        int mode = request.PARTNERS[i].ENCOUNTER.TRACING[j].TRACING_MODE;
                                        int outcome = request.PARTNERS[i].ENCOUNTER.TRACING[j].TRACING_OUTCOME;
                                        int consent = request.PARTNERS[i].ENCOUNTER.TRACING[j].CONSENT;
                                        DateTime? tracingBookingDate = null;
                                        if (!string.IsNullOrWhiteSpace(request.PARTNERS[i].ENCOUNTER.TRACING[j].BOOKING_DATE))
                                            tracingBookingDate = DateTime.ParseExact(request.PARTNERS[i].ENCOUNTER.TRACING[j].BOOKING_DATE, "yyyyMMdd", null);

                                        var tracingOutcome = await encounterTestingService.addTracing(person.Id, tracingType,
                                            tracingDate, mode, outcome, providerId, null, consent, tracingBookingDate, null);
                                    }
                                }
                            }
                        }

                        // update message as processed
                        await registerPersonService.UpdateAfyaMobileInbox(afyaMobileMessage.Id, indexClientAfyaMobileId, true, DateTime.Now, "success");
                        return Result<string>.Valid(afyaMobileId);
                    }
                    catch (Exception e)
                    {
                        Log.Error(e.Message);
                        Log.Error(e.InnerException.ToString());
                        // update message as processed
                        await registerPersonService.UpdateAfyaMobileInbox(afyaMobileMessage.Id, indexClientAfyaMobileId, false, DateTime.Now, e.Message + " " + e.InnerException.ToString());
                        return Result<string>.Invalid(e.Message);
                    }
                        
                }

                return Result<string>.Valid(afyaMobileId);
            }
        }
    }
}
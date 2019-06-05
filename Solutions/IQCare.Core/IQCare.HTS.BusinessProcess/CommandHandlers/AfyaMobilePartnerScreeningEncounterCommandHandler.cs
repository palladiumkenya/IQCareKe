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
    public class AfyaMobilePartnerScreeningEncounterCommandHandler : IRequestHandler<AfyaMobilePartnerScreeningEncounterCommand, Result<string>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        private readonly IHTSUnitOfWork _htsUnitOfWork;

        public AfyaMobilePartnerScreeningEncounterCommandHandler(IHTSUnitOfWork htsUnitOfWork, ICommonUnitOfWork commonUnitOfWork)
        {
            _htsUnitOfWork = htsUnitOfWork ?? throw new ArgumentNullException(nameof(htsUnitOfWork));
            _unitOfWork = commonUnitOfWork ?? throw new ArgumentNullException(nameof(commonUnitOfWork));
        }

        public async Task<Result<string>> Handle(AfyaMobilePartnerScreeningEncounterCommand request, CancellationToken cancellationToken)
        {
            string afyaMobileId = string.Empty;
            string indexClientAfyaMobileId = string.Empty;

            using (var trans = _htsUnitOfWork.Context.Database.BeginTransaction())
            {
                try
                {
                    RegisterPersonService registerPersonService = new RegisterPersonService(_unitOfWork);
                    EncounterTestingService encounterTestingService = new EncounterTestingService(_unitOfWork, _htsUnitOfWork);

                    for (int j = 0; j < request.INTERNAL_PATIENT_ID.Count; j++)
                    {
                        if (request.INTERNAL_PATIENT_ID[j].IDENTIFIER_TYPE == "AFYA_MOBILE_ID")
                        {
                            afyaMobileId = request.INTERNAL_PATIENT_ID[j].ID;
                        }

                        if (request.INTERNAL_PATIENT_ID[j].IDENTIFIER_TYPE == "INDEX_CLIENT_AFYAMOBILE_ID")
                        {
                            indexClientAfyaMobileId = request.INTERNAL_PATIENT_ID[j].ID;
                        }
                    }

                    var afyaMobileMessage = await registerPersonService.AddAfyaMobileInbox(DateTime.Now, request.MESSAGE_HEADER.MESSAGE_TYPE, afyaMobileId, JsonConvert.SerializeObject(request), false);

                    int pnsAccepted = request.SCREENING.PARTNER_SCREENING.PNS_ACCEPTED;
                    DateTime screeningDate = DateTime.Now;
                    try
                    {
                        screeningDate = DateTime.ParseExact(request.SCREENING.PARTNER_SCREENING.SCREENING_DATE, "yyyyMMdd", null);
                    }
                    catch (Exception e)
                    {
                        Log.Error($"Could not parse partner screening SCREENING_DATE: {request.SCREENING.PARTNER_SCREENING.SCREENING_DATE} as a valid date: Incorrect format, date should be in the following format yyyyMMdd");
                        throw new Exception($"Could not parse partner screening SCREENING_DATE: {request.SCREENING.PARTNER_SCREENING.SCREENING_DATE} as a valid date: Incorrect format, date should be in the following format yyyyMMdd");
                    }
                    
                    int ipvScreeningDone = request.SCREENING.PARTNER_SCREENING.IPV_SCREENING_DONE;
                    int hurtByPartner = request.SCREENING.PARTNER_SCREENING.HURT_BY_PARTNER;
                    int threatByPartner = request.SCREENING.PARTNER_SCREENING.THREAT_BY_PARTNER;
                    int sexualAbuseByPartner = request.SCREENING.PARTNER_SCREENING.SEXUAL_ABUSE_BY_PARTNER;
                    int ipvOutcome = request.SCREENING.PARTNER_SCREENING.IPV_OUTCOME;
                    string partnerOccupation = request.SCREENING.PARTNER_SCREENING.PARTNER_OCCUPATION;
                    int partnerRelationship = request.SCREENING.PARTNER_SCREENING.PARTNER_RELATIONSHIP;
                    int livingWithClient = request.SCREENING.PARTNER_SCREENING.LIVING_WITH_CLIENT;
                    int hivStatus = request.SCREENING.PARTNER_SCREENING.HIV_STATUS;
                    int pnsApproach = request.SCREENING.PARTNER_SCREENING.PNS_APPROACH;
                    int eligibleForHts = request.SCREENING.PARTNER_SCREENING.ELIGIBLE_FOR_HTS;
                    DateTime bookingDate = DateTime.Now;
                    try
                    {
                        bookingDate = DateTime.ParseExact(request.SCREENING.PARTNER_SCREENING.BOOKING_DATE, "yyyyMMdd", null);
                    }
                    catch (Exception e)
                    {
                        Log.Error($"Could not parse partner screening BOOKING_DATE: {request.SCREENING.PARTNER_SCREENING.BOOKING_DATE} as a valid date: Incorrect format, date should be in the following format yyyyMMdd");
                        throw new Exception($"Could not parse partner screening BOOKING_DATE: {request.SCREENING.PARTNER_SCREENING.BOOKING_DATE} as a valid date: Incorrect format, date should be in the following format yyyyMMdd");
                    }
                    
                    var pnsScreeningOptions = await _unitOfWork.Repository<LookupItemView>().Get(x => x.MasterName == "PnsScreening").ToListAsync();
                    int providerId = 1;

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

                    var indexClientIdentifiers = await registerPersonService.getPersonIdentifiers(indexClientAfyaMobileId, 10);
                    if (indexClientIdentifiers.Count > 0)
                    {
                        //Get Index client
                        var indexClient = await registerPersonService.GetPatientByPersonId(indexClientIdentifiers[0].PersonId);
                        var partnetPersonIdentifiers = await registerPersonService.getPersonIdentifiers(afyaMobileId, 10);
                        if (partnetPersonIdentifiers.Count > 0)
                        {
                            var patientMasterVisitEntity = await _unitOfWork.Repository<PatientMasterVisit>()
                                .Get(x => x.PatientId == indexClient.Id && x.ServiceId == 2).ToListAsync();
                            int patientMasterVisitId = patientMasterVisitEntity.OrderBy(x => x.Id).FirstOrDefault().Id;



                            var partnHtsScreenings = await encounterTestingService.AddPartnerScreening(
                                partnetPersonIdentifiers[0].PersonId, indexClient.Id, patientMasterVisitId,
                                partnerOccupation,
                                screeningDate, bookingDate, newScreenings, providerId);
                        }
                        else
                        {
                            //update message has been processed
                            await registerPersonService.UpdateAfyaMobileInbox(afyaMobileMessage.Id, afyaMobileId, true, DateTime.Now, $"Partner with afyamobileid: {afyaMobileId} could not be found", false);
                            return Result<string>.Invalid($"Partner with afyamobileid: {afyaMobileId} could not be found");
                        }
                    }
                    else
                    {
                        //update message has been processed
                        await registerPersonService.UpdateAfyaMobileInbox(afyaMobileMessage.Id, afyaMobileId, true, DateTime.Now, $"Index clientid: {indexClientAfyaMobileId} for partnerid: {afyaMobileId} not found", false);
                        return Result<string>.Invalid($"Index clientid: {indexClientAfyaMobileId} for partnerid: {afyaMobileId} not found");
                    }

                    //update message has been processed
                    await registerPersonService.UpdateAfyaMobileInbox(afyaMobileMessage.Id, afyaMobileId, true, DateTime.Now, $"Successfully synchronized partner screening for afyamobileid: {afyaMobileId}", true);
                    trans.Commit();
                    return Result<string>.Valid($"Successfully synchronized partner screening for afyamobileid: {afyaMobileId}");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    Log.Error($"Failed to synchronize partner screening for clientId: {afyaMobileId} " + ex.Message + " " + ex.InnerException);
                    return Result<string>.Invalid($"Failed to synchronize partner screening for clientId: {afyaMobileId} " + ex.Message + " " + ex.InnerException);
                }
            }
        }
    }
}
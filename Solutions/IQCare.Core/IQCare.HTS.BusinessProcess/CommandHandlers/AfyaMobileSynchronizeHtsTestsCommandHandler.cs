using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Setup;
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
    public class AfyaMobileSynchronizeHtsTestsCommandHandler: IRequestHandler<AfyaMobileSynchronizeHtsTestsCommand, Result<string>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        private readonly IHTSUnitOfWork _htsUnitOfWork;

        public AfyaMobileSynchronizeHtsTestsCommandHandler(IHTSUnitOfWork htsUnitOfWork, ICommonUnitOfWork unitOfWork)
        {
            _htsUnitOfWork = htsUnitOfWork ?? throw new ArgumentNullException(nameof(htsUnitOfWork));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<string>> Handle(AfyaMobileSynchronizeHtsTestsCommand request, CancellationToken cancellationToken)
        {
            string afyaMobileId = String.Empty;

            using (var trans = _htsUnitOfWork.Context.Database.BeginTransaction())
            {
                RegisterPersonService registerPersonService = new RegisterPersonService(_unitOfWork);
                EncounterTestingService encounterTestingService = new EncounterTestingService(_unitOfWork, _htsUnitOfWork);

                try
                {
                    //Person Identifier
                    for (int j = 0; j < request.INTERNAL_PATIENT_ID.Count; j++)
                    {
                        if (request.INTERNAL_PATIENT_ID[j].IDENTIFIER_TYPE == "AFYA_MOBILE_ID" && request.INTERNAL_PATIENT_ID[j].ASSIGNING_AUTHORITY == "AFYAMOBILE")
                        {
                            afyaMobileId = request.INTERNAL_PATIENT_ID[j].ID;
                        }
                    }
                    var afyaMobileMessage = await registerPersonService.AddAfyaMobileInbox(DateTime.Now, request.MESSAGE_HEADER.MESSAGE_TYPE, afyaMobileId, JsonConvert.SerializeObject(request), false);
                    
                    //check if person already exists
                    var identifiers = await registerPersonService.getPersonIdentifiers(afyaMobileId, 10);
                    if (identifiers.Count > 0)
                    {
                        var person = await registerPersonService.GetPerson(identifiers[0].PersonId);
                        var patient = await registerPersonService.GetPatientByPersonId(identifiers[0].PersonId);

                        int pnsAccepted = request.HIV_TESTS.SUMMARY.PNS_ACCEPTED;
                        int pnsDeclineReason = request.HIV_TESTS.SUMMARY.PNS_DECLINE_REASON;
                        List<NewTests> screeningTests = request.HIV_TESTS.SCREENING;
                        List<NewTests> confirmatoryTests = request.HIV_TESTS.CONFIRMATORY;
                        int coupleDiscordant = request.HIV_TESTS.SUMMARY.COUPLE_DISCORDANT;
                        int finalResultGiven = request.HIV_TESTS.SUMMARY.FINAL_RESULT_GIVEN;
                        int roundOneTestResult = request.HIV_TESTS.SUMMARY.SCREENING_RESULT;
                        int? roundTwoTestResult = request.HIV_TESTS.SUMMARY.CONFIRMATORY_RESULT;
                        int? finalResult = request.HIV_TESTS.SUMMARY.FINAL_RESULT;
                        var encounterNumber = request.PLACER_DETAIL.ENCOUNTER_NUMBER;
                        int providerId = request.PLACER_DETAIL.PROVIDER_ID;

                        //Get Consent to screen partners itemId
                        var consentPartnerType = await _unitOfWork.Repository<LookupItemView>()
                            .Get(x => x.MasterName == "ConsentType" && x.ItemName == "ConsentToListPartners")
                            .FirstOrDefaultAsync();
                        int consentListPartnersTypeId = consentPartnerType != null ? consentPartnerType.ItemId : 0;

                        var resultPlacerGet = await registerPersonService.GetInteropPlacerValue(7, 4, encounterNumber);
                        if (resultPlacerGet.Count > 0)
                        {
                            var getHtsEncounter = await encounterTestingService.GetHtsEncounter(resultPlacerGet[0].EntityId);
                            var getPatientEncounter = await encounterTestingService.GetPatientEncounterById(getHtsEncounter.PatientEncounterID);
                            var getPatientConsents = await encounterTestingService.GetPatientConsent(patient.Id, getPatientEncounter.PatientMasterVisitId, 2, consentListPartnersTypeId);
                            if (getPatientConsents.Count > 0)
                            {
                                getPatientConsents[0].ConsentValue = pnsAccepted;
                                getPatientConsents[0].ConsentDate = getPatientEncounter.EncounterStartTime;
                                getPatientConsents[0].DeclineReason = pnsDeclineReason;

                                await encounterTestingService.UpdatePatientConsent(getPatientConsents[0]);


                                var hasConsentedToListPartners = await _unitOfWork.Repository<LookupItemView>()
                                    .Get(x => x.ItemId == pnsAccepted && x.MasterName == "YesNoNA").ToListAsync();
                                if (hasConsentedToListPartners.Count > 0)
                                {
                                    if (hasConsentedToListPartners[0].ItemName == "Yes")
                                    {
                                        var listPartners = await registerPersonService.AddAppStateStore(person.Id, patient.Id, 3,
                                            getPatientEncounter.PatientMasterVisitId, getPatientEncounter.Id, null);
                                    }
                                }
                            }
                            else
                            {
                                //add consent to list partners
                                var partnersConsent = await encounterTestingService.addPatientConsent(patient.Id,
                                    getPatientEncounter.PatientMasterVisitId, 2, pnsAccepted, consentListPartnersTypeId, getPatientEncounter.EncounterStartTime, providerId,
                                    pnsDeclineReason);

                                var hasConsentedToListPartners = await _unitOfWork.Repository<LookupItemView>()
                                    .Get(x => x.ItemId == pnsAccepted && x.MasterName == "YesNoNA").ToListAsync();
                                if (hasConsentedToListPartners.Count > 0)
                                {
                                    if (hasConsentedToListPartners[0].ItemName == "Yes")
                                    {
                                        var listPartners = await registerPersonService.AddAppStateStore(person.Id, patient.Id, 3,
                                            getPatientEncounter.PatientMasterVisitId, getPatientEncounter.Id, null);
                                    }
                                }
                            }

                            //Screening Tests
                            var updatedScreeningTests = await encounterTestingService.UpdateTesting(getHtsEncounter.Id, screeningTests, providerId, 1);
                            //Confirmatory Tests
                            var updatedConfirmatoryTests = await encounterTestingService.UpdateTesting(getHtsEncounter.Id, confirmatoryTests, providerId, 2);

                            getHtsEncounter.CoupleDiscordant = coupleDiscordant;
                            getHtsEncounter.FinalResultGiven = finalResultGiven;

                            await encounterTestingService.updateHtsEncounter(getHtsEncounter.Id, getHtsEncounter);
                            var getHtsEncounterResults = await encounterTestingService.GetHtsEncounterResultByEncounterId(getHtsEncounter.Id);
                            if (getHtsEncounterResults.Count > 0)
                            {
                                getHtsEncounterResults[0].RoundOneTestResult = roundOneTestResult;
                                getHtsEncounterResults[0].RoundTwoTestResult = roundTwoTestResult;
                                getHtsEncounterResults[0].FinalResult = finalResult;

                                var updatedHtsEncounterResult = await encounterTestingService.UpdateHtsEncounterResult(getHtsEncounterResults[0]);

                                // add state for positive person
                                var clientFinalResultsList = await _unitOfWork.Repository<LookupItemView>()
                                    .Get(x => x.ItemId == finalResult && x.MasterName == "HIVFinalResults").ToListAsync();
                                if (clientFinalResultsList.Count > 0 &&
                                    clientFinalResultsList[0].ItemName == "Positive")
                                {
                                    var isClientPositiveState = await registerPersonService.AddAppStateStore(person.Id, patient.Id, 4,
                                        getPatientEncounter.PatientMasterVisitId, getPatientEncounter.Id, null);
                                }
                            }
                            else
                            {
                                var htsEncounterResult = await encounterTestingService.addHtsEncounterResult(getHtsEncounter.Id, roundOneTestResult, roundTwoTestResult, finalResult);


                                // add state for positive person
                                var clientFinalResultsList = await _unitOfWork.Repository<LookupItemView>()
                                    .Get(x => x.ItemId == finalResult && x.MasterName == "HIVFinalResults").ToListAsync();
                                if (clientFinalResultsList.Count > 0 &&
                                    clientFinalResultsList[0].ItemName == "Positive")
                                {
                                    var isClientPositiveState = await registerPersonService.AddAppStateStore(person.Id, patient.Id, 4,
                                        getPatientEncounter.PatientMasterVisitId, getPatientEncounter.Id, null);
                                }
                            }
                        }
                        else
                        {
                            //update message has been processed
                            await registerPersonService.UpdateAfyaMobileInbox(afyaMobileMessage.Id, afyaMobileId, true, DateTime.Now, $"HTS PRE-TEST with encounter number: {encounterNumber} could not be found", false);
                            Result<string>.Invalid($"HTS PRE-TEST with encounter number: {encounterNumber} could not be found");
                        }
                    }
                    else
                    {
                        //update message has been processed
                        await registerPersonService.UpdateAfyaMobileInbox(afyaMobileMessage.Id, afyaMobileId, true, DateTime.Now, $"Person with afyaMobileId: {afyaMobileId} could not be found", false);
                        return Result<string>.Invalid($"Person with afyaMobileId: {afyaMobileId} could not be found");
                    }

                    //update message has been processed
                    await registerPersonService.UpdateAfyaMobileInbox(afyaMobileMessage.Id, afyaMobileId, true, DateTime.Now, $"Successfully synchronized HTS tests for afyamobileid: {afyaMobileId}", true);
                    trans.Commit();
                    return Result<string>.Valid($"Successfully synchronized HTS tests for afyamobileid: {afyaMobileId}");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    Log.Error($"Failed to synchronize Hts tests for clientid: {afyaMobileId} " + ex.Message + " " + ex.InnerException);
                    return Result<string>.Invalid($"Failed to synchronize Hts tests for clientid: {afyaMobileId} " + ex.Message + " " + ex.InnerException);
                }
            }
        }
    }
}
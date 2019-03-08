using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
    public class AfyaMobileSynchronizeEncounterCommandHandler : IRequestHandler<AfyaMobileSynchronizeEncounterCommand, Result<string>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        private readonly IHTSUnitOfWork _htsUnitOfWork;

        public AfyaMobileSynchronizeEncounterCommandHandler(IHTSUnitOfWork htsUnitOfWork, ICommonUnitOfWork unitOfWork)
        {
            _htsUnitOfWork = htsUnitOfWork ?? throw new ArgumentNullException(nameof(htsUnitOfWork));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<string>> Handle(AfyaMobileSynchronizeEncounterCommand request, CancellationToken cancellationToken)
        {
            string afyaMobileId = String.Empty;

            using (var trans = _htsUnitOfWork.Context.Database.BeginTransaction())
            {
                RegisterPersonService registerPersonService = new RegisterPersonService(_unitOfWork);
                EncounterTestingService encounterTestingService = new EncounterTestingService(_unitOfWork, _htsUnitOfWork);

                //Person Identifier
                for (int j = 0; j < request.INTERNAL_PATIENT_ID.Count; j++)
                {
                    if (request.INTERNAL_PATIENT_ID[j].IDENTIFIER_TYPE == "AFYA_MOBILE_ID" && request.INTERNAL_PATIENT_ID[j].ASSIGNING_AUTHORITY == "AFYAMOBILE")
                    {
                        afyaMobileId = request.INTERNAL_PATIENT_ID[j].ID;
                    }
                }
                var afyaMobileMessage = await registerPersonService.AddAfyaMobileInbox(DateTime.Now, request.MESSAGE_HEADER.MESSAGE_TYPE, afyaMobileId, JsonConvert.SerializeObject(request), false);

                try
                {
                    //check if person already exists
                    var identifiers = await registerPersonService.getPersonIdentifiers(afyaMobileId, 10);
                    //Encounter
                    DateTime encounterDate = DateTime.ParseExact(request.PRE_TEST.ENCOUNTER_DATE, "yyyyMMdd", null);
                    string htsEncounterRemarks = request.PRE_TEST.REMARKS;
                    int clientEverTested = request.PRE_TEST.EVER_TESTED;
                    int clientEverSelfTested = request.PRE_TEST.SELF_TEST_12_MONTHS;
                    int testEntryPoint = request.PRE_TEST.SERVICE_POINT;
                    int htsencounterType = request.PRE_TEST.ENCOUNTER_TYPE;
                    int testingStrategy = request.PRE_TEST.STRATEGY;
                    int clientTestedAs = request.PRE_TEST.TESTED_AS;
                    int monthsSinceLastTest = request.PRE_TEST.MONTHS_SINCE_LAST_TEST;
                    List<int> clientDisabilities = request.PRE_TEST.DISABILITIES;
                    int providerId = request.PLACER_DETAIL.PROVIDER_ID;
                    var encounterNumber = request.PLACER_DETAIL.ENCOUNTER_NUMBER;

                    //HTS Encounter types
                    var emrEncounterTypes = await _unitOfWork.Repository<LookupItemView>()
                        .Get(x => x.MasterName == "EncounterType" && x.ItemName == "Hts-encounter")
                        .FirstOrDefaultAsync();
                    int encounterTypeId = emrEncounterTypes.ItemId;

                    //Get consent to testing
                    int consentValue = request.PRE_TEST.CONSENT;
                    var consentType = await _unitOfWork.Repository<LookupItemView>().Get(x => x.MasterName == "ConsentType" && x.ItemName == "ConsentToBeTested").FirstOrDefaultAsync();
                    int consentTypeId = consentType != null ? consentType.ItemId : 0;

                    //Get TBStatus masterId
                    var screeningType = await _unitOfWork.Repository<LookupItemView>().Get(x => x.MasterName == "TbScreening").FirstOrDefaultAsync();
                    int screeningTypeId = screeningType != null ? screeningType.MasterId : 0;
                    int tbStatus = request.PRE_TEST.TB_SCREENING;

                    if (identifiers.Count > 0)
                    {
                        var person = await registerPersonService.GetPerson(identifiers[0].PersonId);
                        var patient = await registerPersonService.GetPatientByPersonId(identifiers[0].PersonId);
                        var resultPlacerGet = await registerPersonService.GetInteropPlacerValue(7, 4, encounterNumber);
                        if (resultPlacerGet.Count > 0)
                        {
                            var getHtsEncounter = await encounterTestingService.GetHtsEncounter(resultPlacerGet[0].EntityId);
                            var getPatientEncounter = await encounterTestingService.GetPatientEncounterById(getHtsEncounter.PatientEncounterID);

                            getHtsEncounter.EverTested = clientEverTested;
                            getHtsEncounter.MonthsSinceLastTest = monthsSinceLastTest;
                            getHtsEncounter.MonthSinceSelfTest = null;
                            getHtsEncounter.TestedAs = clientTestedAs;
                            getHtsEncounter.TestingStrategy = testingStrategy;
                            getHtsEncounter.EncounterRemarks = htsEncounterRemarks;
                            //getHtsEncounter.FinalResultGiven = ;
                            //getHtsEncounter.CoupleDiscordant = 1;
                            getHtsEncounter.TestEntryPoint = testEntryPoint;
                            getHtsEncounter.EverSelfTested = clientEverSelfTested;
                            getHtsEncounter.EncounterType = htsencounterType;

                            await encounterTestingService.UpdateHtsEncounter(getHtsEncounter);

                            var getPatientScreenings = await encounterTestingService.GetPatientScreening(patient.Id, getPatientEncounter.PatientMasterVisitId, screeningTypeId, null);
                            if (getPatientScreenings.Count > 0)
                            {
                                getPatientScreenings[0].ScreeningValueId = tbStatus;
                                await encounterTestingService.UpdatePatientScreening(getPatientScreenings[0]);
                            }
                            else
                            {
                                //add patient screening
                                var patientScreening = await encounterTestingService.addPatientScreening(patient.Id,
                                    getPatientEncounter.PatientMasterVisitId, screeningTypeId, encounterDate, tbStatus, providerId);
                            }

                            await encounterTestingService.UpdateClientDisabilities(identifiers[0].PersonId, clientDisabilities, getPatientEncounter.Id, providerId);
                        }
                        else
                        {
                            //add patient master visit
                            var patientMasterVisit = await encounterTestingService.AddPatientMasterVisit(patient.Id, 2, encounterDate, providerId);
                            //add patient encounter
                            var patientEncounter = await encounterTestingService.AddPatientEncounter(patient.Id,
                                encounterTypeId, patientMasterVisit.Id, encounterDate, 2, providerId);
                            //add patient HTS encounter
                            var htsEncounter = await encounterTestingService.addHtsEncounter(htsEncounterRemarks,
                                clientEverSelfTested, clientEverTested,
                                patientEncounter.Id, person.Id, providerId, testEntryPoint, htsencounterType,
                                testingStrategy, clientTestedAs, monthsSinceLastTest, null);
                            //add afya mobile placer value
                            var addplacerHtsPlacer = await registerPersonService.AddInteropPlacerValue(htsEncounter.Id, 4, 7, encounterNumber);
                            //add patient consent
                            var consent = await encounterTestingService.addPatientConsent(patient.Id, patientMasterVisit.Id,
                                2, consentValue, consentTypeId, encounterDate, providerId, null);
                            //add patient screening
                            var patientScreening = await encounterTestingService.addPatientScreening(patient.Id,
                                patientMasterVisit.Id, screeningTypeId, encounterDate, tbStatus, providerId);

                            //add disabilities
                            var disabilities = await encounterTestingService.addDisabilities(clientDisabilities,
                                patientEncounter.Id, identifiers[0].PersonId, providerId);
                        }
                    }
                    else
                    {
                        //update message has been processed
                        await registerPersonService.UpdateAfyaMobileInbox(afyaMobileMessage.Id, afyaMobileId, true, DateTime.Now, $"Person with afyaMobileId: {afyaMobileId} could not be found", false);
                        return Result<string>.Invalid($"Person with afyaMobileId: {afyaMobileId} could not be found");
                    }

                    //update message has been processed
                    await registerPersonService.UpdateAfyaMobileInbox(afyaMobileMessage.Id, afyaMobileId, true, DateTime.Now, "success", true);
                    trans.Commit();
                    return Result<string>.Valid($"Successfully synchronized HTS encounter for afyamobileid: {afyaMobileId}");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    Log.Error($"Failed to synchronize encounter for clientid: {afyaMobileId} " + ex.Message + " " + ex.InnerException);
                    return Result<string>.Invalid($"Failed to synchronize encounter for clientid: {afyaMobileId} " + ex.Message + " " + ex.InnerException);
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using IQCare.Common.BusinessProcess.Commands.Setup;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.HTS.BusinessProcess.Commands;
using IQCare.HTS.BusinessProcess.Services;
using IQCare.HTS.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using Serilog;

namespace IQCare.HTS.BusinessProcess.CommandHandlers
{
    public class SynchronizeClientsCommandHandler : IRequestHandler<SynchronizeClientsCommand, Result<string>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        private readonly IHTSUnitOfWork _htsUnitOfWork;
        public SynchronizeClientsCommandHandler(ICommonUnitOfWork unitOfWork, IHTSUnitOfWork htsUnitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _htsUnitOfWork = htsUnitOfWork ?? throw new ArgumentNullException(nameof(htsUnitOfWork));
        }

        public async Task<Result<string>> Handle(SynchronizeClientsCommand request, CancellationToken cancellationToken)
        {
            string afyaMobileId = String.Empty;

            using (_unitOfWork)
            using (_htsUnitOfWork)
            {
                LookupLogic lookupLogic = new LookupLogic(_unitOfWork);
                RegisterPersonService registerPersonService = new RegisterPersonService(_unitOfWork);
                EncounterTestingService encounterTestingService = new EncounterTestingService(_unitOfWork, _htsUnitOfWork);

                for (int i = 0; i < request.CLIENTS.Count; i++)
                {
                    for (int j = 0; j < request.CLIENTS[i].PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID.Count; j++)
                    {
                        if (request.CLIENTS[i].PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID[j].IDENTIFIER_TYPE ==
                            "AFYA_MOBILE_ID" &&
                            request.CLIENTS[i].PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID[j].ASSIGNING_AUTHORITY ==
                            "AFYAMOBILE")
                        {
                            afyaMobileId = request.CLIENTS[i].PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID[j].ID;
                        }
                    }
                }
                var afyaMobileMessage = await registerPersonService.AddAfyaMobileInbox(DateTime.Now, afyaMobileId, JsonConvert.SerializeObject(request), false);

                try
                {
                    var facilityId = request.MESSAGE_HEADER.SENDING_FACILITY;

                    for (int i = 0; i < request.CLIENTS.Count; i++)
                    {
                        string firstName = request.CLIENTS[i].PATIENT_IDENTIFICATION.PATIENT_NAME.FIRST_NAME;
                        string middleName = string.IsNullOrWhiteSpace(request.CLIENTS[i].PATIENT_IDENTIFICATION.PATIENT_NAME.MIDDLE_NAME) ? "" : request.CLIENTS[i].PATIENT_IDENTIFICATION.PATIENT_NAME.MIDDLE_NAME;
                        string lastName = request.CLIENTS[i].PATIENT_IDENTIFICATION.PATIENT_NAME.LAST_NAME;
                        int sex = request.CLIENTS[i].PATIENT_IDENTIFICATION.SEX;
                        DateTime dateOfBirth = DateTime.ParseExact(request.CLIENTS[i].PATIENT_IDENTIFICATION.DATE_OF_BIRTH, "yyyyMMdd", null);
                        string dobPrecision = request.CLIENTS[i].PATIENT_IDENTIFICATION.DATE_OF_BIRTH_PRECISION;
                        DateTime dateEnrollment = DateTime.ParseExact(request.CLIENTS[i].PATIENT_IDENTIFICATION.REGISTRATION_DATE, "yyyyMMdd", null);
                        int maritalStatusId = request.CLIENTS[i].PATIENT_IDENTIFICATION.MARITAL_STATUS;
                        string landmark = request.CLIENTS[i].PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS
                            .LANDMARK;
                        string physicalAddress = request.CLIENTS[i].PATIENT_IDENTIFICATION.PATIENT_ADDRESS.POSTAL_ADDRESS;
                        string mobileNumber = request.CLIENTS[i].PATIENT_IDENTIFICATION.PHONE_NUMBER;
                        string enrollmentNo = string.Empty;
                        int userId = request.CLIENTS[i].PATIENT_IDENTIFICATION.USER_ID;

                        string maritalStatusName = String.Empty;
                        string gender = String.Empty;

                        var maritalStatusList = await lookupLogic.GetLookupNameByGroupNameItemId(maritalStatusId, "HTSMaritalStatus");
                        var genderList = await lookupLogic.GetLookupNameByGroupNameItemId(sex, "Gender");
                        if (maritalStatusList.Count > 0)
                        {
                            maritalStatusName = maritalStatusList[0].ItemName;
                        }
                        if (genderList.Count > 0)
                        {
                            gender = genderList[0].ItemName;
                        }

                        //Tracing
                        var enrollmentTracing = await _unitOfWork.Repository<LookupItemView>()
                            .Get(x => x.MasterName == "TracingType" && x.ItemName == "Enrolment").FirstOrDefaultAsync();
                        int tracingType = enrollmentTracing.ItemId;
                        string tracingRemarks = String.Empty;

                        for (int j = 0; j < request.CLIENTS[i].PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID.Count; j++)
                        {
                            if (request.CLIENTS[i].PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID[j].ASSIGNING_AUTHORITY ==
                                "HTS" && request.CLIENTS[i].PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID[j]
                                    .IDENTIFIER_TYPE == "HTS_SERIAL")
                            {
                                enrollmentNo = request.CLIENTS[i].PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID[j].ID;
                            }

                            if (request.CLIENTS[i].PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID[j].IDENTIFIER_TYPE ==
                                "AFYA_MOBILE_ID" &&
                                request.CLIENTS[i].PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID[j].ASSIGNING_AUTHORITY ==
                                "AFYAMOBILE")
                            {
                                afyaMobileId = request.CLIENTS[i].PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID[j].ID;
                            }
                        }

                        //var afyaMobileMessage = await registerPersonService.AddAfyaMobileInbox(DateTime.Now, afyaMobileId, JsonConvert.SerializeObject(request), false);

                        //check if person already exists
                        var identifiers = await registerPersonService.getPersonIdentifiers(afyaMobileId, 10);
                        if (identifiers.Count > 0)
                        {
                            var registeredPerson = await registerPersonService.GetPerson(identifiers[0].PersonId);
                            if (registeredPerson != null)
                            {
                                var updatedPerson = await registerPersonService.UpdatePerson(identifiers[0].PersonId,
                                    firstName, middleName, lastName, sex, dateOfBirth);
                            }
                            else
                            {
                                var person = await registerPersonService.RegisterPerson(firstName, middleName, lastName,
                                    sex, userId, dateOfBirth);
                            }

                            var patient = await registerPersonService.GetPatientByPersonId(identifiers[0].PersonId);
                            if (patient != null)
                            {
                                var updatedPatient = await registerPersonService.UpdatePatient(patient.Id, dateOfBirth, facilityId);
                            }
                            else
                            {
                                //Add Person to mst_patient
                                var mstResult = await registerPersonService.InsertIntoBlueCard(firstName, lastName,
                                    middleName, dateEnrollment, maritalStatusName, physicalAddress, mobileNumber, gender, dobPrecision, dateOfBirth, userId, facilityId);
                                if (mstResult.Count > 0)
                                {
                                    patient = await registerPersonService.AddPatient(identifiers[0].PersonId, userId, facilityId);
                                    // Person is enrolled state
                                    var enrollmentAppState = await registerPersonService.AddAppStateStore(identifiers[0].PersonId, patient.Id, 7, null, null);
                                    // Enroll patient
                                    var patientIdentifier = await registerPersonService.EnrollPatient(enrollmentNo, patient.Id, 2, userId, dateEnrollment);
                                    //Add PersonIdentifiers
                                    var personIdentifier = await registerPersonService.addPersonIdentifiers(identifiers[0].PersonId, 10, afyaMobileId, userId);
                                }
                            }

                            var updatedPersonPopulations = await registerPersonService.UpdatePersonPopulation(identifiers[0].PersonId,
                                request.CLIENTS[i].PATIENT_IDENTIFICATION.KEY_POP, userId);
                            if (!string.IsNullOrWhiteSpace(landmark))
                            {
                                var updatedLocation = await registerPersonService.UpdatePersonLocation(identifiers[0].PersonId, landmark);
                            }

                            if (!string.IsNullOrWhiteSpace(mobileNumber) || !string.IsNullOrWhiteSpace(physicalAddress))
                            {
                                //add Person Contact
                                var personContact =
                                    await registerPersonService.UpdatePersonContact(identifiers[0].PersonId,
                                        physicalAddress, mobileNumber);
                            }

                            /**
                             * Encounter
                             * 
                             */

                            if (request.CLIENTS[i].ENCOUNTER != null)
                            {
                                if (request.CLIENTS[i].ENCOUNTER.PRE_TEST != null)
                                {
                                    DateTime encounterDate = DateTime.ParseExact(request.CLIENTS[i].ENCOUNTER.PRE_TEST.ENCOUNTER_DATE, "yyyyMMdd", null);
                                    var emrEncounterTypes = await _unitOfWork.Repository<LookupItemView>()
                                        .Get(x => x.MasterName == "EncounterType" && x.ItemName == "Hts-encounter")
                                        .FirstOrDefaultAsync();

                                    int encounterTypeId = emrEncounterTypes.ItemId;

                                    //Get consent to testing
                                    int consentValue = request.CLIENTS[i].ENCOUNTER.PRE_TEST.CONSENT;
                                    var consentType = await _unitOfWork.Repository<LookupItemView>().Get(x => x.MasterName == "ConsentType" && x.ItemName == "ConsentToBeTested").FirstOrDefaultAsync();
                                    int consentTypeId = consentType != null ? consentType.ItemId : 0;

                                    //Get TBStatus masterId
                                    var screeningType = await _unitOfWork.Repository<LookupItemView>().Get(x => x.MasterName == "TbScreening").FirstOrDefaultAsync();
                                    int screeningTypeId = screeningType != null ? screeningType.MasterId : 0;
                                    int tbStatus = request.CLIENTS[i].ENCOUNTER.PRE_TEST.TB_SCREENING;

                                    //Get Consent to screen partners itemId
                                    var consentPartnerType = await _unitOfWork.Repository<LookupItemView>()
                                        .Get(x => x.MasterName == "ConsentType" && x.ItemName == "ConsentToListPartners")
                                        .FirstOrDefaultAsync();
                                    int consentListPartnersTypeId = consentPartnerType != null ? consentPartnerType.ItemId : 0;

                                    string htsEncounterRemarks = request.CLIENTS[i].ENCOUNTER.PRE_TEST.REMARKS;
                                    int clientEverTested = request.CLIENTS[i].ENCOUNTER.PRE_TEST.EVER_TESTED;
                                    int clientEverSelfTested = request.CLIENTS[i].ENCOUNTER.PRE_TEST.SELF_TEST_12_MONTHS;
                                    int testEntryPoint = request.CLIENTS[i].ENCOUNTER.PRE_TEST.SERVICE_POINT;
                                    int htsencounterType = request.CLIENTS[i].ENCOUNTER.PRE_TEST.ENCOUNTER_TYPE;
                                    int testingStrategy = request.CLIENTS[i].ENCOUNTER.PRE_TEST.STRATEGY;
                                    int clientTestedAs = request.CLIENTS[i].ENCOUNTER.PRE_TEST.TESTED_AS;
                                    int monthsSinceLastTest = request.CLIENTS[i].ENCOUNTER.PRE_TEST.MONTHS_SINCE_LAST_TEST;
                                    List<int> clientDisabilities = request.CLIENTS[i].ENCOUNTER.PRE_TEST.DISABILITIES;
                                    int providerId = request.CLIENTS[i].ENCOUNTER.PLACER_DETAIL.PROVIDER_ID;
                                    string encounterNumber = request.CLIENTS[i].ENCOUNTER.PLACER_DETAIL.ENCOUNTER_NUMBER;


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

                                        //check for hiv tests
                                        if (request.CLIENTS[i].ENCOUNTER.HIV_TESTS != null)
                                        {
                                            int pnsAccepted = request.CLIENTS[i].ENCOUNTER.HIV_TESTS.SUMMARY.PNS_ACCEPTED;
                                            int pnsDeclineReason = request.CLIENTS[i].ENCOUNTER.HIV_TESTS.SUMMARY.PNS_DECLINE_REASON;
                                            List<NewTests> screeningTests = request.CLIENTS[i].ENCOUNTER.HIV_TESTS.SCREENING;
                                            List<NewTests> confirmatoryTests = request.CLIENTS[i].ENCOUNTER.HIV_TESTS.CONFIRMATORY;
                                            int coupleDiscordant = request.CLIENTS[i].ENCOUNTER.HIV_TESTS.SUMMARY.COUPLE_DISCORDANT;
                                            int finalResultGiven = request.CLIENTS[i].ENCOUNTER.HIV_TESTS.SUMMARY.FINAL_RESULT_GIVEN;
                                            int roundOneTestResult = request.CLIENTS[i].ENCOUNTER.HIV_TESTS.SUMMARY.SCREENING_RESULT;
                                            int? roundTwoTestResult = request.CLIENTS[i].ENCOUNTER.HIV_TESTS.SUMMARY.CONFIRMATORY_RESULT;
                                            int? finalResult = request.CLIENTS[i].ENCOUNTER.HIV_TESTS.SUMMARY.FINAL_RESULT;

                                            var getPatientConsents = await encounterTestingService.GetPatientConsent(patient.Id, getPatientEncounter.PatientMasterVisitId, 2, consentListPartnersTypeId);

                                            if (getPatientConsents.Count > 0)
                                            {
                                                getPatientConsents[0].ConsentValue = pnsAccepted;
                                                getPatientConsents[0].ConsentDate = encounterDate;
                                                getPatientConsents[0].DeclineReason = pnsDeclineReason;

                                                await encounterTestingService.UpdatePatientConsent(getPatientConsents[0]);
                                            }
                                            else
                                            {
                                                //add consent to list partners
                                                var partnersConsent = await encounterTestingService.addPatientConsent(patient.Id,
                                                    getPatientEncounter.PatientMasterVisitId, 2, pnsAccepted, consentListPartnersTypeId, encounterDate, providerId,
                                                    pnsDeclineReason);
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
                                            }
                                            else
                                            {
                                                var htsEncounterResult = await encounterTestingService.addHtsEncounterResult(getHtsEncounter.Id, roundOneTestResult, roundTwoTestResult, finalResult);
                                            }
                                        }

                                        for (int j = 0; request.CLIENTS[i].ENCOUNTER != null  && request.CLIENTS[i].ENCOUNTER.TRACING != null && j < request.CLIENTS[i].ENCOUNTER.TRACING.Count; j++)
                                        {
                                            DateTime tracingDate = DateTime.ParseExact(request.CLIENTS[i].ENCOUNTER.TRACING[j].TRACING_DATE, "yyyyMMdd", null);
                                            int mode = request.CLIENTS[i].ENCOUNTER.TRACING[j].TRACING_MODE;
                                            int outcome = request.CLIENTS[i].ENCOUNTER.TRACING[j].TRACING_OUTCOME;

                                            //add Client Tracing
                                            var clientTracing = await encounterTestingService.addTracing(identifiers[0].PersonId, tracingType, tracingDate, mode, outcome,
                                                providerId, tracingRemarks, null, null, null);
                                        }

                                        //check for linkage
                                        if (request.CLIENTS[i].ENCOUNTER != null && request.CLIENTS[i].ENCOUNTER.LINKAGE != null)
                                        {
                                            DateTime dateLinkageEnrolled = DateTime.ParseExact(request.CLIENTS[i].ENCOUNTER.LINKAGE.DATE_ENROLLED, "yyyyMMdd", null);
                                            string linkageCCCNumber = request.CLIENTS[i].ENCOUNTER.LINKAGE.CCC_NUMBER;
                                            string linkageFacility = request.CLIENTS[i].ENCOUNTER.LINKAGE.FACILITY;
                                            string healthWorker = request.CLIENTS[i].ENCOUNTER.LINKAGE.HEALTH_WORKER;
                                            string carde = request.CLIENTS[i].ENCOUNTER.LINKAGE.CARDE;
                                            string ARTStartDate = request.CLIENTS[i].ENCOUNTER.LINKAGE.ARTStartDate;
                                            string remarks = request.CLIENTS[i].ENCOUNTER.LINKAGE.REMARKS;

                                            DateTime? artstartDate = null;
                                            if (!string.IsNullOrWhiteSpace(ARTStartDate))
                                            {
                                                artstartDate = DateTime.ParseExact(ARTStartDate, "yyyyMMdd", null);
                                            }

                                            var previousLinkage = await encounterTestingService.GetPersonLinkage(identifiers[0].PersonId);
                                            if (previousLinkage.Count > 0)
                                            {
                                                previousLinkage[0].ArtStartDate = artstartDate;
                                                previousLinkage[0].LinkageDate = dateLinkageEnrolled;
                                                previousLinkage[0].CCCNumber = linkageCCCNumber;
                                                previousLinkage[0].Facility = linkageFacility;
                                                previousLinkage[0].HealthWorker = healthWorker;
                                                previousLinkage[0].Cadre = carde;
                                                previousLinkage[0].Comments = remarks;

                                                await encounterTestingService.UpdatePersonLinkage(previousLinkage[0]);
                                            }
                                            else
                                            {
                                                //add Client Linkage
                                                var clientLinkage = await encounterTestingService.AddLinkage(identifiers[0].PersonId, dateLinkageEnrolled,
                                                    linkageCCCNumber, linkageFacility, providerId, healthWorker, carde, remarks, artstartDate);
                                            }
                                        }

                                        if (request.CLIENTS[i].ENCOUNTER != null && request.CLIENTS[i].ENCOUNTER.REFERRAL != null)
                                        {
                                            //add referral
                                            var facility = await encounterTestingService.GetCurrentFacility();
                                            if (facility.Count > 0)
                                            {
                                                DateTime dateToBeEnrolled = DateTime.ParseExact(request.CLIENTS[i].ENCOUNTER.REFERRAL.DATE_TO_BE_ENROLLED, "yyyyMMdd", null);
                                                string facilityReferred = request.CLIENTS[i].ENCOUNTER.REFERRAL.REFERRED_TO;

                                                var referralReason = await _unitOfWork.Repository<LookupItemView>()
                                                    .Get(x => x.MasterName == "ReferralReason" &&
                                                              x.ItemName == "CCCEnrollment").ToListAsync();
                                                var searchFacility = await encounterTestingService.SearchFacility(facilityReferred);
                                                var previousReferrals = await encounterTestingService.GetReferralByPersonId(identifiers[0].PersonId);
                                                if (searchFacility.Count > 0)
                                                {
                                                    if (previousReferrals.Count > 0)
                                                    {
                                                        previousReferrals[0].ToFacility =
                                                            Convert.ToInt32(searchFacility[0].MFLCode);
                                                        previousReferrals[0].OtherFacility = "";
                                                        previousReferrals[0].ExpectedDate = dateToBeEnrolled;

                                                        await encounterTestingService.UpdateReferral(
                                                            previousReferrals[0]);
                                                    }
                                                    else
                                                    {
                                                        await encounterTestingService.AddReferral(identifiers[0].PersonId,
                                                            facility[0].FacilityID, 2,
                                                            Convert.ToInt32(searchFacility[0].MFLCode),
                                                            referralReason[0].ItemId, userId, dateToBeEnrolled, "");
                                                    }
                                                }
                                                else
                                                {
                                                    if (previousReferrals.Count > 0)
                                                    {
                                                        previousReferrals[0].ToFacility =
                                                            Convert.ToInt32(searchFacility[0].MFLCode);
                                                        previousReferrals[0].OtherFacility = "";
                                                        previousReferrals[0].ExpectedDate = dateToBeEnrolled;

                                                        await encounterTestingService.UpdateReferral(
                                                            previousReferrals[0]);
                                                    }
                                                    else
                                                    {
                                                        await encounterTestingService.AddReferral(identifiers[0].PersonId,
                                                            facility[0].FacilityID, 2, 99999,
                                                            referralReason[0].ItemId, userId, dateToBeEnrolled, facilityReferred);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //add patient master visit
                                        var patientMasterVisit = await encounterTestingService.AddPatientMasterVisit(patient.Id, 2, encounterDate, providerId);
                                        //add patient encounter
                                        var patientEncounter = await encounterTestingService.AddPatientEncounter(patient.Id,
                                            encounterTypeId, patientMasterVisit.Id, encounterDate, 2, providerId);
                                        //add patient encounter
                                        var htsEncounter = await encounterTestingService.addHtsEncounter(htsEncounterRemarks,
                                            clientEverSelfTested, clientEverTested,
                                            patientEncounter.Id, identifiers[0].PersonId, providerId, testEntryPoint, htsencounterType,
                                            testingStrategy, clientTestedAs, monthsSinceLastTest, null);

                                        //add patient consent
                                        var consent = await encounterTestingService.addPatientConsent(patient.Id, patientMasterVisit.Id,
                                            2, consentValue, consentTypeId, encounterDate, providerId, null);
                                        //add patient screening
                                        var patientScreening = await encounterTestingService.addPatientScreening(patient.Id,
                                            patientMasterVisit.Id, screeningTypeId, encounterDate, tbStatus, providerId);
                                        
                                        //add disabilities
                                        var disabilities = await encounterTestingService.addDisabilities(clientDisabilities,
                                            patientEncounter.Id, identifiers[0].PersonId, providerId);
                                        //add afya mobile placer value
                                        var addplacerHtsPlacer = await registerPersonService.AddInteropPlacerValue(htsEncounter.Id, 4, 7, encounterNumber);

                                        //check for hiv tests
                                        if (request.CLIENTS[i].ENCOUNTER.HIV_TESTS != null)
                                        {
                                            int pnsAccepted = request.CLIENTS[i].ENCOUNTER.HIV_TESTS.SUMMARY.PNS_ACCEPTED;
                                            int pnsDeclineReason = request.CLIENTS[i].ENCOUNTER.HIV_TESTS.SUMMARY.PNS_DECLINE_REASON;
                                            List<NewTests> screeningTests = request.CLIENTS[i].ENCOUNTER.HIV_TESTS.SCREENING;
                                            List<NewTests> confirmatoryTests = request.CLIENTS[i].ENCOUNTER.HIV_TESTS.CONFIRMATORY;
                                            int coupleDiscordant = request.CLIENTS[i].ENCOUNTER.HIV_TESTS.SUMMARY.COUPLE_DISCORDANT;
                                            int finalResultGiven = request.CLIENTS[i].ENCOUNTER.HIV_TESTS.SUMMARY.FINAL_RESULT_GIVEN;
                                            int roundOneTestResult = request.CLIENTS[i].ENCOUNTER.HIV_TESTS.SUMMARY.SCREENING_RESULT;
                                            int? roundTwoTestResult = request.CLIENTS[i].ENCOUNTER.HIV_TESTS.SUMMARY.CONFIRMATORY_RESULT;
                                            int? finalResult = request.CLIENTS[i].ENCOUNTER.HIV_TESTS.SUMMARY.FINAL_RESULT;


                                            //add consent to list partners
                                            var partnersConsent = await encounterTestingService.addPatientConsent(patient.Id,
                                                patientMasterVisit.Id, 2, pnsAccepted, consentListPartnersTypeId, encounterDate, providerId,
                                                pnsDeclineReason);

                                            //add screening tests for client
                                            var clientScreeningTesting =
                                                await encounterTestingService.addTesting(screeningTests, htsEncounter.Id, providerId);

                                            //add confirmatory tests for client
                                            var clientConfirmatoryTesting = await encounterTestingService.addTesting(confirmatoryTests, htsEncounter.Id, providerId);

                                            //update testing for client
                                            htsEncounter.CoupleDiscordant = coupleDiscordant;
                                            htsEncounter.FinalResultGiven = finalResultGiven;

                                            await encounterTestingService.updateHtsEncounter(htsEncounter.Id, htsEncounter);
                                            var htsEncounterResult = await encounterTestingService.addHtsEncounterResult(htsEncounter.Id, roundOneTestResult, roundTwoTestResult, finalResult);
                                        }
                                    }
                                }
                            }

                            //check for tracing
                            for (int j = 0; request.CLIENTS[i].ENCOUNTER != null && request.CLIENTS[i].ENCOUNTER.TRACING != null && j < request.CLIENTS[i].ENCOUNTER.TRACING.Count; j++)
                            {
                                DateTime tracingDate = DateTime.ParseExact(request.CLIENTS[i].ENCOUNTER.TRACING[j].TRACING_DATE, "yyyyMMdd", null);
                                int mode = request.CLIENTS[i].ENCOUNTER.TRACING[j].TRACING_MODE;
                                int outcome = request.CLIENTS[i].ENCOUNTER.TRACING[j].TRACING_OUTCOME;

                                //add Client Tracing
                                var clientTracing = await encounterTestingService.addTracing(identifiers[0].PersonId, tracingType, tracingDate, mode, outcome,
                                    userId, tracingRemarks, null, null, null);
                            }

                            //check for linkage
                            if (request.CLIENTS[i].ENCOUNTER != null && request.CLIENTS[i].ENCOUNTER.LINKAGE != null)
                            {
                                DateTime dateLinkageEnrolled = DateTime.ParseExact(request.CLIENTS[i].ENCOUNTER.LINKAGE.DATE_ENROLLED, "yyyyMMdd", null);
                                string linkageCCCNumber = request.CLIENTS[i].ENCOUNTER.LINKAGE.CCC_NUMBER;
                                string linkageFacility = request.CLIENTS[i].ENCOUNTER.LINKAGE.FACILITY;
                                string healthWorker = request.CLIENTS[i].ENCOUNTER.LINKAGE.HEALTH_WORKER;
                                string carde = request.CLIENTS[i].ENCOUNTER.LINKAGE.CARDE;
                                string ARTStartDate = request.CLIENTS[i].ENCOUNTER.LINKAGE.ARTStartDate;
                                string remarks = request.CLIENTS[i].ENCOUNTER.LINKAGE.REMARKS;

                                DateTime? artstartDate = null;
                                if (!string.IsNullOrWhiteSpace(ARTStartDate))
                                {
                                    artstartDate = DateTime.ParseExact(ARTStartDate, "yyyyMMdd", null);
                                }
                                var previousLinkage = await encounterTestingService.GetPersonLinkage(identifiers[0].PersonId);
                                if (previousLinkage.Count > 0)
                                {
                                    previousLinkage[0].ArtStartDate = artstartDate;
                                    previousLinkage[0].LinkageDate = dateLinkageEnrolled;
                                    previousLinkage[0].CCCNumber = linkageCCCNumber;
                                    previousLinkage[0].Facility = linkageFacility;
                                    previousLinkage[0].HealthWorker = healthWorker;
                                    previousLinkage[0].Cadre = carde;
                                    previousLinkage[0].Comments = remarks;

                                    await encounterTestingService.UpdatePersonLinkage(previousLinkage[0]);
                                }
                                else
                                {
                                    //add Client Linkage
                                    var clientLinkage = await encounterTestingService.AddLinkage(identifiers[0].PersonId, dateLinkageEnrolled,
                                        linkageCCCNumber, linkageFacility, userId, healthWorker, carde, remarks, artstartDate);
                                }
                            }

                            //check for referral
                            if (request.CLIENTS[i].ENCOUNTER != null && request.CLIENTS[i].ENCOUNTER.REFERRAL != null)
                            {
                                //add referral
                                var facility = await encounterTestingService.GetCurrentFacility();
                                if (facility.Count > 0)
                                {
                                    DateTime dateToBeEnrolled = DateTime.ParseExact(request.CLIENTS[i].ENCOUNTER.REFERRAL.DATE_TO_BE_ENROLLED, "yyyyMMdd", null);
                                    string facilityReferred = request.CLIENTS[i].ENCOUNTER.REFERRAL.REFERRED_TO;

                                    var referralReason = await _unitOfWork.Repository<LookupItemView>()
                                        .Get(x => x.MasterName == "ReferralReason" &&
                                                  x.ItemName == "CCCEnrollment").ToListAsync();
                                    var searchFacility = await encounterTestingService.SearchFacility(facilityReferred);
                                    var previousReferrals = await encounterTestingService.GetReferralByPersonId(identifiers[0].PersonId);
                                    if (searchFacility.Count > 0)
                                    {
                                        if (previousReferrals.Count > 0)
                                        {
                                            previousReferrals[0].ToFacility =
                                                Convert.ToInt32(searchFacility[0].MFLCode);
                                            previousReferrals[0].OtherFacility = "";
                                            previousReferrals[0].ExpectedDate = dateToBeEnrolled;

                                            await encounterTestingService.UpdateReferral(previousReferrals[0]);
                                        }
                                        else
                                        {
                                            await encounterTestingService.AddReferral(identifiers[0].PersonId,
                                                facility[0].FacilityID, 2, Convert.ToInt32(searchFacility[0].MFLCode),
                                                referralReason[0].ItemId, userId, dateToBeEnrolled, "");
                                        }
                                    }
                                    else
                                    {
                                        if (previousReferrals.Count > 0)
                                        {
                                            previousReferrals[0].ToFacility =
                                                Convert.ToInt32(searchFacility[0].MFLCode);
                                            previousReferrals[0].OtherFacility = "";
                                            previousReferrals[0].ExpectedDate = dateToBeEnrolled;

                                            await encounterTestingService.UpdateReferral(previousReferrals[0]);
                                        }
                                        else
                                        {
                                            await encounterTestingService.AddReferral(identifiers[0].PersonId,
                                                facility[0].FacilityID, 2, Convert.ToInt32(searchFacility[0].MFLCode),
                                                referralReason[0].ItemId, userId, dateToBeEnrolled, facilityReferred);
                                        }
                                    }
                                }
                            }

                            // update message as processed
                            await registerPersonService.UpdateAfyaMobileInbox(afyaMobileMessage.Id, afyaMobileId, true, DateTime.Now);
                        }
                        else
                        {
                            // Add Person
                            var person = await registerPersonService.RegisterPerson(firstName, middleName, lastName, sex,
                                userId, dateOfBirth);
                            //Add Person to mst_patient
                            var mstResult = await registerPersonService.InsertIntoBlueCard(firstName, lastName,
                                middleName, dateEnrollment, maritalStatusName, physicalAddress, mobileNumber, gender, dobPrecision, dateOfBirth, userId, facilityId);
                            if (mstResult.Count > 0)
                            {
                                //Add PersonIdentifiers
                                var personIdentifier = await registerPersonService.addPersonIdentifiers(person.Id, 10, afyaMobileId, userId);
                                // Add Patient
                                var patient = await registerPersonService.AddPatient(person.Id, userId, mstResult[0].Ptn_Pk, facilityId);
                                // Person is enrolled state
                                var enrollmentAppState = await registerPersonService.AddAppStateStore(person.Id, patient.Id, 7, null, null);
                                // Enroll patient
                                var patientIdentifier = await registerPersonService.EnrollPatient(enrollmentNo, patient.Id, 2, userId, dateEnrollment);
                                // Add Marital Status
                                var maritalStatus = await registerPersonService.AddMaritalStatus(person.Id, maritalStatusId, userId);
                                // Add Person Key pop
                                var population = await registerPersonService.addPersonPopulation(person.Id,
                                    request.CLIENTS[i].PATIENT_IDENTIFICATION.KEY_POP, userId);
                                // Add Person Location
                                if (!string.IsNullOrWhiteSpace(landmark))
                                {
                                    var personLocation = await registerPersonService.addPersonLocation(person.Id, 0, 0, 0, "", landmark, userId);
                                }

                                if (!string.IsNullOrWhiteSpace(mobileNumber) || !string.IsNullOrWhiteSpace(physicalAddress))
                                {
                                    //add Person Contact
                                    var personContact = await registerPersonService.addPersonContact(person.Id, physicalAddress,
                                    mobileNumber, string.Empty, string.Empty, userId);
                                }

                                /***
                                 * Encounter
                                 */

                                if (request.CLIENTS[i].ENCOUNTER != null)
                                {
                                    if (request.CLIENTS[i].ENCOUNTER.PRE_TEST != null)
                                    {
                                        DateTime encounterDate = DateTime.ParseExact(request.CLIENTS[i].ENCOUNTER.PRE_TEST.ENCOUNTER_DATE, "yyyyMMdd", null);
                                        var emrEncounterTypes = await _unitOfWork.Repository<LookupItemView>()
                                            .Get(x => x.MasterName == "EncounterType" && x.ItemName == "Hts-encounter")
                                            .FirstOrDefaultAsync();

                                        int encounterTypeId = emrEncounterTypes.ItemId;

                                        //Get consent to testing
                                        int consentValue = request.CLIENTS[i].ENCOUNTER.PRE_TEST.CONSENT;
                                        var consentType = await _unitOfWork.Repository<LookupItemView>().Get(x => x.MasterName == "ConsentType" && x.ItemName == "ConsentToBeTested").FirstOrDefaultAsync();
                                        int consentTypeId = consentType != null ? consentType.ItemId : 0;

                                        //Get TBStatus masterId
                                        var screeningType = await _unitOfWork.Repository<LookupItemView>().Get(x => x.MasterName == "TbScreening").FirstOrDefaultAsync();
                                        int screeningTypeId = screeningType != null ? screeningType.MasterId : 0;
                                        int tbStatus = request.CLIENTS[i].ENCOUNTER.PRE_TEST.TB_SCREENING;

                                        //Get Consent to screen partners itemId
                                        var consentPartnerType = await _unitOfWork.Repository<LookupItemView>()
                                            .Get(x => x.MasterName == "ConsentType" && x.ItemName == "ConsentToListPartners")
                                            .FirstOrDefaultAsync();
                                        int consentListPartnersTypeId = consentPartnerType != null ? consentPartnerType.ItemId : 0;

                                        string htsEncounterRemarks = request.CLIENTS[i].ENCOUNTER.PRE_TEST.REMARKS;
                                        int clientEverTested = request.CLIENTS[i].ENCOUNTER.PRE_TEST.EVER_TESTED;
                                        int clientEverSelfTested = request.CLIENTS[i].ENCOUNTER.PRE_TEST.SELF_TEST_12_MONTHS;
                                        int testEntryPoint = request.CLIENTS[i].ENCOUNTER.PRE_TEST.SERVICE_POINT;
                                        int htsencounterType = request.CLIENTS[i].ENCOUNTER.PRE_TEST.ENCOUNTER_TYPE;
                                        int testingStrategy = request.CLIENTS[i].ENCOUNTER.PRE_TEST.STRATEGY;
                                        int clientTestedAs = request.CLIENTS[i].ENCOUNTER.PRE_TEST.TESTED_AS;
                                        int monthsSinceLastTest = request.CLIENTS[i].ENCOUNTER.PRE_TEST.MONTHS_SINCE_LAST_TEST;
                                        List<int> clientDisabilities = request.CLIENTS[i].ENCOUNTER.PRE_TEST.DISABILITIES;
                                        int providerId = request.CLIENTS[i].ENCOUNTER.PLACER_DETAIL.PROVIDER_ID;
                                        string encounterNumber = request.CLIENTS[i].ENCOUNTER.PLACER_DETAIL.ENCOUNTER_NUMBER;

                                        //add patient master visit
                                        var patientMasterVisit = await encounterTestingService.AddPatientMasterVisit(patient.Id, 2, encounterDate, providerId);
                                        //add patient encounter
                                        var patientEncounter = await encounterTestingService.AddPatientEncounter(patient.Id,
                                            encounterTypeId, patientMasterVisit.Id, encounterDate, 2, providerId);
                                        //add patient consent
                                        var consent = await encounterTestingService.addPatientConsent(patient.Id, patientMasterVisit.Id,
                                            2, consentValue, consentTypeId, encounterDate, providerId, null);
                                        //add patient screening
                                        var patientScreening = await encounterTestingService.addPatientScreening(patient.Id,
                                            patientMasterVisit.Id, screeningTypeId, encounterDate, tbStatus, providerId);
                                        //add patient encounter
                                        var htsEncounter = await encounterTestingService.addHtsEncounter(htsEncounterRemarks,
                                            clientEverSelfTested, clientEverTested,
                                            patientEncounter.Id, person.Id, providerId, testEntryPoint, htsencounterType,
                                            testingStrategy, clientTestedAs, monthsSinceLastTest, null);
                                        //add disabilities
                                        var disabilities = await encounterTestingService.addDisabilities(clientDisabilities,
                                            patientEncounter.Id, person.Id, providerId);
                                        //add afya mobile placer value
                                        var addplacerHtsPlacer = await registerPersonService.AddInteropPlacerValue(htsEncounter.Id, 4, 7, encounterNumber);

                                        // Person is tested as state
                                        var testedAsAppState = await registerPersonService.AddAppStateStore(person.Id, patient.Id, 6, patientMasterVisit.Id, htsEncounter.Id);
                                        // Person is consent to testing state
                                        var consentintToTestingAppState = await registerPersonService.AddAppStateStore(person.Id, patient.Id, 1, patientMasterVisit.Id, htsEncounter.Id);

                                        //check for hiv tests
                                        if (request.CLIENTS[i].ENCOUNTER.HIV_TESTS != null)
                                        {
                                            int pnsAccepted = request.CLIENTS[i].ENCOUNTER.HIV_TESTS.SUMMARY.PNS_ACCEPTED;
                                            int pnsDeclineReason = request.CLIENTS[i].ENCOUNTER.HIV_TESTS.SUMMARY.PNS_DECLINE_REASON;
                                            List<NewTests> screeningTests = request.CLIENTS[i].ENCOUNTER.HIV_TESTS.SCREENING;
                                            List<NewTests> confirmatoryTests = request.CLIENTS[i].ENCOUNTER.HIV_TESTS.CONFIRMATORY;
                                            int coupleDiscordant = request.CLIENTS[i].ENCOUNTER.HIV_TESTS.SUMMARY.COUPLE_DISCORDANT;
                                            int finalResultGiven = request.CLIENTS[i].ENCOUNTER.HIV_TESTS.SUMMARY.FINAL_RESULT_GIVEN;
                                            int roundOneTestResult = request.CLIENTS[i].ENCOUNTER.HIV_TESTS.SUMMARY.SCREENING_RESULT;
                                            int? roundTwoTestResult = request.CLIENTS[i].ENCOUNTER.HIV_TESTS.SUMMARY.CONFIRMATORY_RESULT;
                                            int? finalResult = request.CLIENTS[i].ENCOUNTER.HIV_TESTS.SUMMARY.FINAL_RESULT;


                                            //add consent to list partners
                                            var partnersConsent = await encounterTestingService.addPatientConsent(patient.Id,
                                                patientMasterVisit.Id, 2, pnsAccepted, consentListPartnersTypeId, encounterDate, providerId,
                                                pnsDeclineReason);

                                            // Person is consent to list partners
                                            var consentToListPartnersAppState = await registerPersonService.AddAppStateStore(person.Id, patient.Id, 3, patientMasterVisit.Id, htsEncounter.Id);

                                            //add screening tests for client
                                            var clientScreeningTesting =
                                                await encounterTestingService.addTesting(screeningTests, htsEncounter.Id, providerId);

                                            //add confirmatory tests for client
                                            var clientConfirmatoryTesting = await encounterTestingService.addTesting(confirmatoryTests, htsEncounter.Id, providerId);

                                            //update testing for client
                                            htsEncounter.CoupleDiscordant = coupleDiscordant;
                                            htsEncounter.FinalResultGiven = finalResultGiven;

                                            await encounterTestingService.updateHtsEncounter(htsEncounter.Id, htsEncounter);
                                            var htsEncounterResult = await encounterTestingService.addHtsEncounterResult(htsEncounter.Id, roundOneTestResult, roundTwoTestResult, finalResult);

                                            // Person is positive
                                            var partnerIsPositiveAppState = await registerPersonService.AddAppStateStore(person.Id, patient.Id, 4, patientMasterVisit.Id, htsEncounter.Id);
                                        }


                                    }
                                }

                                //check for client tracing
                                for (int j = 0; (request.CLIENTS[i].ENCOUNTER != null && request.CLIENTS[i].ENCOUNTER.TRACING != null && j < request.CLIENTS[i].ENCOUNTER.TRACING.Count); j++)
                                {
                                    DateTime tracingDate = DateTime.ParseExact(request.CLIENTS[i].ENCOUNTER.TRACING[j].TRACING_DATE, "yyyyMMdd", null);
                                    int mode = request.CLIENTS[i].ENCOUNTER.TRACING[j].TRACING_MODE;
                                    int outcome = request.CLIENTS[i].ENCOUNTER.TRACING[j].TRACING_OUTCOME;

                                    //add Client Tracing
                                    var clientTracing = await encounterTestingService.addTracing(person.Id, tracingType, tracingDate, mode, outcome,
                                        userId, tracingRemarks, null, null, null);
                                }

                                //check for linkage
                                if (request.CLIENTS[i].ENCOUNTER != null && request.CLIENTS[i].ENCOUNTER.LINKAGE != null)
                                {
                                    DateTime dateLinkageEnrolled = DateTime.ParseExact(request.CLIENTS[i].ENCOUNTER.LINKAGE.DATE_ENROLLED, "yyyyMMdd", null);
                                    string linkageCCCNumber = request.CLIENTS[i].ENCOUNTER.LINKAGE.CCC_NUMBER;
                                    string linkageFacility = request.CLIENTS[i].ENCOUNTER.LINKAGE.FACILITY;
                                    string healthWorker = request.CLIENTS[i].ENCOUNTER.LINKAGE.HEALTH_WORKER;
                                    string carde = request.CLIENTS[i].ENCOUNTER.LINKAGE.CARDE;
                                    string ARTStartDate = request.CLIENTS[i].ENCOUNTER.LINKAGE.ARTStartDate;
                                    string remarks = request.CLIENTS[i].ENCOUNTER.LINKAGE.REMARKS;

                                    DateTime? artstartDate = null;
                                    if (!string.IsNullOrWhiteSpace(ARTStartDate))
                                    {
                                        artstartDate = DateTime.ParseExact(ARTStartDate, "yyyyMMdd", null);
                                    }

                                    var previousLinkage = await encounterTestingService.GetPersonLinkage(person.Id);
                                    if (previousLinkage.Count > 0)
                                    {
                                        previousLinkage[0].ArtStartDate = artstartDate;
                                        previousLinkage[0].LinkageDate = dateLinkageEnrolled;
                                        previousLinkage[0].CCCNumber = linkageCCCNumber;
                                        previousLinkage[0].Facility = linkageFacility;
                                        previousLinkage[0].HealthWorker = healthWorker;
                                        previousLinkage[0].Cadre = carde;
                                        previousLinkage[0].Comments = remarks;

                                        await encounterTestingService.UpdatePersonLinkage(previousLinkage[0]);
                                    }
                                    else
                                    {
                                        //add Client Linkage
                                        var clientLinkage = await encounterTestingService.AddLinkage(person.Id, dateLinkageEnrolled,
                                            linkageCCCNumber, linkageFacility, userId, healthWorker, carde, remarks, artstartDate);
                                    }
                                }

                                //check for referral
                                if (request.CLIENTS[i].ENCOUNTER != null && request.CLIENTS[i].ENCOUNTER.REFERRAL != null)
                                {
                                    //add referral
                                    var facility = await encounterTestingService.GetCurrentFacility();
                                    if (facility.Count > 0)
                                    {
                                        DateTime dateToBeEnrolled = DateTime.ParseExact(request.CLIENTS[i].ENCOUNTER.REFERRAL.DATE_TO_BE_ENROLLED, "yyyyMMdd", null);
                                        string facilityReferred = request.CLIENTS[i].ENCOUNTER.REFERRAL.REFERRED_TO;

                                        var referralReason = await _unitOfWork.Repository<LookupItemView>()
                                            .Get(x => x.MasterName == "ReferralReason" &&
                                                      x.ItemName == "CCCEnrollment").ToListAsync();

                                        var searchFacility = await encounterTestingService.SearchFacility(facilityReferred);
                                        var previousReferrals = await encounterTestingService.GetReferralByPersonId(identifiers[0].PersonId);
                                        if (searchFacility.Count > 0)
                                        {
                                            if (previousReferrals.Count > 0)
                                            {
                                                previousReferrals[0].ToFacility = Convert.ToInt32(searchFacility[0].MFLCode);
                                                previousReferrals[0].OtherFacility = "";
                                                previousReferrals[0].ExpectedDate = dateToBeEnrolled;

                                                await encounterTestingService.UpdateReferral(previousReferrals[0]);
                                            }
                                            else
                                            {
                                                await encounterTestingService.AddReferral(identifiers[0].PersonId,
                                                    facility[0].FacilityID, 2,
                                                    Convert.ToInt32(searchFacility[0].MFLCode),
                                                    referralReason[0].ItemId, userId, dateToBeEnrolled, "");
                                            }
                                        }
                                        else
                                        {
                                            if (previousReferrals.Count > 0)
                                            {
                                                previousReferrals[0].ToFacility = 99999;
                                                previousReferrals[0].OtherFacility = "";
                                                previousReferrals[0].ExpectedDate = dateToBeEnrolled;

                                                await encounterTestingService.UpdateReferral(previousReferrals[0]);
                                            }
                                            else
                                            {
                                                await encounterTestingService.AddReferral(identifiers[0].PersonId,
                                                    facility[0].FacilityID, 2, 99999,
                                                    referralReason[0].ItemId, userId, dateToBeEnrolled, facilityReferred);
                                            }
                                        }
                                    }
                                }

                                //update message has been processed
                                await registerPersonService.UpdateAfyaMobileInbox(afyaMobileMessage.Id, afyaMobileId, true, DateTime.Now);
                            }
                        }
                    }

                    //update message has been processed
                    await registerPersonService.UpdateAfyaMobileInbox(afyaMobileMessage.Id, afyaMobileId, true, DateTime.Now, "success");
                    return Result<string>.Valid(afyaMobileId);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    Log.Error(e.InnerException.ToString());
                    // update message as processed
                    await registerPersonService.UpdateAfyaMobileInbox(afyaMobileMessage.Id, afyaMobileId, false, DateTime.Now, e.Message);
                    return Result<string>.Invalid(e.Message);
                }
            }
        }
    }
}
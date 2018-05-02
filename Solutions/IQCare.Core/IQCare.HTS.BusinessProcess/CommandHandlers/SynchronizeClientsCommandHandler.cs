using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.HTS.BusinessProcess.Commands;
using IQCare.HTS.BusinessProcess.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IQCare.HTS.BusinessProcess.CommandHandlers
{
    public class SynchronizeClientsCommandHandler : IRequestHandler<SynchronizeClientsCommand, Result<SynchronizeClientsResponse>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public SynchronizeClientsCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<SynchronizeClientsResponse>> Handle(SynchronizeClientsCommand request, CancellationToken cancellationToken)
        {
            using (_unitOfWork)
            {
                try
                {
                    var facilityId = request.MESSAGE_HEADER.SENDING_FACILITY;
                    for (int i = 0; i < request.CLIENTS.Count; i++)
                    {
                        RegisterPersonService registerPersonService = new RegisterPersonService(_unitOfWork);
                        EncounterTestingService encounterTestingService = new EncounterTestingService(_unitOfWork);

                        string firstName = request.CLIENTS[i].PATIENT_IDENTIFICATION.PATIENT_NAME.FIRST_NAME;
                        string middleName = request.CLIENTS[i].PATIENT_IDENTIFICATION.PATIENT_NAME.MIDDLE_NAME;
                        string lastName = request.CLIENTS[i].PATIENT_IDENTIFICATION.PATIENT_NAME.LAST_NAME;
                        int sex = request.CLIENTS[i].PATIENT_IDENTIFICATION.SEX;
                        DateTime dateOfBirth = DateTime.ParseExact(request.CLIENTS[i].PATIENT_IDENTIFICATION.DATE_OF_BIRTH, "yyyyMMdd", null);
                        DateTime dateEnrollment = DateTime.ParseExact(request.CLIENTS[i].PATIENT_IDENTIFICATION.REGISTRATION_DATE, "yyyyMMdd", null);
                        int maritalStatusId = request.CLIENTS[i].PATIENT_IDENTIFICATION.MARITAL_STATUS;
                        string landmark = request.CLIENTS[i].PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS
                            .LANDMARK;
                        string physicalAddress = request.CLIENTS[i].PATIENT_IDENTIFICATION.PATIENT_ADDRESS.POSTAL_ADDRESS;
                        string mobileNumber = request.CLIENTS[i].PATIENT_IDENTIFICATION.PHONE_NUMBER;

                        string enrollmentNo = string.Empty;
                        for (int j = 0; j < request.CLIENTS[j].PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID.Count; j++)
                        {
                            if (request.CLIENTS[i].PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID[j].ASSIGNING_AUTHORITY ==
                                "HTS" && request.CLIENTS[i].PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID[j]
                                    .IDENTIFIER_TYPE == "HTS_SERIAL")
                            {
                                enrollmentNo = request.CLIENTS[i].PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID[j].ID;
                            }
                        }


                        // Add Person
                        var person = await registerPersonService.RegisterPerson(firstName, middleName, lastName, sex,
                            dateOfBirth, 1);
                        // Add Patient
                        var patient = await registerPersonService.AddPatient(person.Id, dateOfBirth);
                        // Enroll patient
                        var patientIdentifier = await registerPersonService.EnrollPatient(enrollmentNo, patient.Id, 2, 1, dateEnrollment);
                        // Add Marital Status
                        var maritalStatus = await registerPersonService.AddMaritalStatus(person.Id, maritalStatusId, 1);
                        // Add Person Key pop
                        var population = await registerPersonService.addPersonPopulation(person.Id,
                            request.CLIENTS[i].PATIENT_IDENTIFICATION.KEY_POP, 1);
                        // Add Person Location
                        var personLocation =
                            await registerPersonService.addPersonLocation(person.Id, 0, 0, 0, "", landmark, 1);
                        //add Person Contact
                        var personContact = await registerPersonService.addPersonContact(person.Id, physicalAddress,
                            mobileNumber, string.Empty, string.Empty, 1);


                        /***
                         * Encounter
                         */

                        DateTime encounterDate = DateTime.ParseExact(request.CLIENTS[i].ENCOUNTER.PRE_TEST.ENCOUNTER_DATE, "yyyyMMdd", null);
                        var emrEncounterTypes = await _unitOfWork.Repository<LookupItemView>()
                            .Get(x => x.MasterName == "EncounterType" && x.ItemName == "Hts-encounter")
                            .FirstOrDefaultAsync();

                        int encounterTypeId = emrEncounterTypes.ItemId;

                        int consentValue = request.CLIENTS[i].ENCOUNTER.PRE_TEST.CONSENT;

                        var consentType = await _unitOfWork.Repository<LookupItemView>().Get(x => x.MasterName == "ConsentType" && x.ItemName == "ConsentToListPartners").FirstOrDefaultAsync();
                        int consentTypeId = consentType != null ? consentType.ItemId : 0;

                        var screeningType = await _unitOfWork.Repository<LookupItemView>().Get(x => x.MasterName == "TBStatus").FirstOrDefaultAsync();
                        int screeningTypeId = screeningType != null ? screeningType.MasterId : 0;

                        int tbStatus = request.CLIENTS[i].ENCOUNTER.PRE_TEST.TB_SCREENING;

                        string htsEncounterRemarks = request.CLIENTS[i].ENCOUNTER.PRE_TEST.REMARKS;
                        int clientEverTested = request.CLIENTS[i].ENCOUNTER.PRE_TEST.EVER_TESTED;
                        int clientEverSelfTested = request.CLIENTS[i].ENCOUNTER.PRE_TEST.SELF_TEST_12_MONTHS;
                        int providerId = request.CLIENTS[i].ENCOUNTER.PLACER_DETAIL.PROVIDER_ID;
                        int testEntryPoint = request.CLIENTS[i].ENCOUNTER.PRE_TEST.SERVICE_POINT;
                        int htsencounterType= request.CLIENTS[i].ENCOUNTER.PRE_TEST.ENCOUNTER_TYPE;
                        int testingStrategy = request.CLIENTS[i].ENCOUNTER.PRE_TEST.STRATEGY;
                        int clientTestedAs = request.CLIENTS[i].ENCOUNTER.PRE_TEST.TESTED_AS;
                        int monthsSinceLastTest = request.CLIENTS[i].ENCOUNTER.PRE_TEST.MONTHS_SINCE_LAST_TEST;
                        List<int> clientDisabilities = request.CLIENTS[i].ENCOUNTER.PRE_TEST.DISABILITIES;

                        //add patient master visit
                        var patientMasterVisit = await encounterTestingService.AddPatientMasterVisit(patient.Id, 2, encounterDate, 1);
                        //add patient encounter
                        var patientEncounter = await encounterTestingService.AddPatientEncounter(patient.Id,
                            encounterTypeId, patientMasterVisit.Id, encounterDate, 2, 1);
                        //add patient consent
                        var consent = await encounterTestingService.addPatientConsent(patient.Id, patientMasterVisit.Id,
                            2, consentValue, consentTypeId, encounterDate, 1, null);
                        //add patient screening
                        var patientScreening = await encounterTestingService.addPatientScreening(patient.Id,
                            patientMasterVisit.Id, screeningTypeId, encounterDate, tbStatus, 1);
                        //add patient encounter
                        var htsEncounter = await encounterTestingService.addHtsEncounter(htsEncounterRemarks,
                            clientEverSelfTested, clientEverTested, null,
                            patientEncounter.Id, person.Id, providerId, testEntryPoint, htsencounterType,
                            testingStrategy, clientTestedAs, monthsSinceLastTest, null);
                        //add disabilities
                        var disabilities = await encounterTestingService.addDisabilities(clientDisabilities,
                            patientEncounter.Id, person.Id, providerId);
                    }

                    return Result<SynchronizeClientsResponse>.Valid(new SynchronizeClientsResponse()
                    {

                    });
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    return Result<SynchronizeClientsResponse>.Invalid(e.Message);
                }
            }
        }
    }
}
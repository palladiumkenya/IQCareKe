using System;
using System.Threading;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Services;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Common.Services;
using IQCare.HTS.BusinessProcess.Commands;
using IQCare.Library;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;

namespace IQCare.HTS.BusinessProcess.CommandHandlers
{
    public class AfyaMobileSynchronizeClientsCommandHandler : IRequestHandler<AfyaMobileSynchronizeClientsCommand, Result<string>>
    {
        private readonly ICommonUnitOfWork _unitOfWork;

        public AfyaMobileSynchronizeClientsCommandHandler(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<string>> Handle(AfyaMobileSynchronizeClientsCommand request, CancellationToken cancellationToken)
        {
            string afyaMobileId = String.Empty;

            using (var trans = _unitOfWork.Context.Database.BeginTransaction())
            {
                RegisterPersonService registerPersonService = new RegisterPersonService(_unitOfWork);
                LookupLogic lookupLogic = new LookupLogic(_unitOfWork);
                PersonOccupationService pocc = new PersonOccupationService(_unitOfWork);
                EducationLevelService educationLevelService = new EducationLevelService(_unitOfWork);

                for (int i = 0; i < request.CLIENTS.Count; i++)
                {
                    for (int j = 0; j < request.CLIENTS[i].PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID.Count; j++)
                    {
                        if (request.CLIENTS[i].PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID[j].IDENTIFIER_TYPE == "AFYA_MOBILE_ID" &&
                            request.CLIENTS[i].PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID[j].ASSIGNING_AUTHORITY == "AFYAMOBILE")
                        {
                            afyaMobileId = request.CLIENTS[i].PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID[j].ID;
                        }
                    }
                }
                var afyaMobileMessage = await registerPersonService.AddAfyaMobileInbox(DateTime.Now, request.MESSAGE_HEADER.MESSAGE_TYPE, afyaMobileId, JsonConvert.SerializeObject(request), false);
                try
                {
                    var facilityId = request.MESSAGE_HEADER.SENDING_FACILITY;

                    for (int i = 0; i < request.CLIENTS.Count; i++)
                    {
                        string firstName = request.CLIENTS[i].PATIENT_IDENTIFICATION.PATIENT_NAME.FIRST_NAME;
                        string middleName = string.IsNullOrWhiteSpace(request.CLIENTS[i].PATIENT_IDENTIFICATION.PATIENT_NAME.MIDDLE_NAME) ? "" : request.CLIENTS[i].PATIENT_IDENTIFICATION.PATIENT_NAME.MIDDLE_NAME;
                        string lastName = request.CLIENTS[i].PATIENT_IDENTIFICATION.PATIENT_NAME.LAST_NAME;
                        string nickName = (request.CLIENTS[i].PATIENT_IDENTIFICATION.PATIENT_NAME.NICK_NAME == null) ? "" : request.CLIENTS[i].PATIENT_IDENTIFICATION.PATIENT_NAME.NICK_NAME.ToString();
                        int sex = request.CLIENTS[i].PATIENT_IDENTIFICATION.SEX;

                        //Try to parse dateOfBirth
                        DateTime dateOfBirth = DateTime.Now;
                        try
                        {
                            dateOfBirth = DateTime.ParseExact(request.CLIENTS[i].PATIENT_IDENTIFICATION.DATE_OF_BIRTH, "yyyyMMdd", null);
                        }
                        catch (Exception e)
                        {
                            Log.Error($"Could not parse DateOfBirth: {request.CLIENTS[i].PATIENT_IDENTIFICATION.DATE_OF_BIRTH} as a valid date. Incorrect format, date should be in the following format yyyyMMdd");
                            throw new Exception($"Could not parse DateOfBirth: {request.CLIENTS[i].PATIENT_IDENTIFICATION.DATE_OF_BIRTH} as a valid date. Incorrect format, date should be in the following format yyyyMMdd");
                        }
                        string dobPrecision = request.CLIENTS[i].PATIENT_IDENTIFICATION.DATE_OF_BIRTH_PRECISION;

                        //Try to parse DateOfEnrollment
                        DateTime dateEnrollment = DateTime.Now;
                        try
                        {
                            dateEnrollment = DateTime.ParseExact(request.CLIENTS[i].PATIENT_IDENTIFICATION.REGISTRATION_DATE, "yyyyMMdd", null);
                        }
                        catch (Exception e)
                        {
                            Log.Error($"Could not parse DateOfEnrollment: {request.CLIENTS[i].PATIENT_IDENTIFICATION.REGISTRATION_DATE} as a valid date: Incorrect format, date should be in the following format yyyyMMdd");
                            throw new Exception($"Could not parse DateOfEnrollment: {request.CLIENTS[i].PATIENT_IDENTIFICATION.REGISTRATION_DATE} as a valid date: Incorrect format, date should be in the following format yyyyMMdd");
                        }

                        int maritalStatusId = request.CLIENTS[i].PATIENT_IDENTIFICATION.MARITAL_STATUS;
                        string landmark = request.CLIENTS[i].PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS
                            .LANDMARK;

                        int ward = request.CLIENTS[i].PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.WARD;
                        int county = request.CLIENTS[i].PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.COUNTY;
                        int subcounty = request.CLIENTS[i].PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.SUB_COUNTY;

                        string educationlevel = (request.CLIENTS[i].PATIENT_IDENTIFICATION.EDUCATIONLEVEL == null) ? "" : request.CLIENTS[i].PATIENT_IDENTIFICATION.EDUCATIONLEVEL.ToString();
                        string educationoutcome = (request.CLIENTS[i].PATIENT_IDENTIFICATION.EDUCATIONOUTCOME == null) ? "" : request.CLIENTS[i].PATIENT_IDENTIFICATION.EDUCATIONOUTCOME.ToString();
                        string occupation = (request.CLIENTS[i].PATIENT_IDENTIFICATION.OCCUPATION == null) ? "" : request.CLIENTS[i].PATIENT_IDENTIFICATION.OCCUPATION.ToString();
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

                        for (int j = 0; j < request.CLIENTS[i].PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID.Count; j++)
                        {
                            if (request.CLIENTS[i].PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID[j].ASSIGNING_AUTHORITY ==
                                "HTS" && request.CLIENTS[i].PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID[j].IDENTIFIER_TYPE == "HTS_SERIAL")
                            {
                                enrollmentNo = request.CLIENTS[i].PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID[j].ID;
                            }

                            if (request.CLIENTS[i].PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID[j].IDENTIFIER_TYPE == "AFYA_MOBILE_ID" &&
                                request.CLIENTS[i].PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID[j].ASSIGNING_AUTHORITY == "AFYAMOBILE")
                            {
                                afyaMobileId = request.CLIENTS[i].PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID[j].ID;
                            }
                        }

                        Facility clientFacility = await _unitOfWork.Repository<Facility>().Get(x => x.PosID == facilityId).FirstOrDefaultAsync();
                        if (clientFacility == null)
                        {
                            clientFacility = await _unitOfWork.Repository<Facility>().Get(x => x.DeleteFlag == 0).FirstOrDefaultAsync();
                        }

                        //check if person already exists
                        var identifiers = await registerPersonService.getPersonIdentifiers(afyaMobileId, 10);
                        if (identifiers.Count > 0)
                        {
                            var registeredPerson = await registerPersonService.GetPerson(identifiers[0].PersonId);

                            if (registeredPerson != null)
                            {
                                var updatedPerson = await registerPersonService.UpdatePerson(identifiers[0].PersonId,
                                    firstName, middleName, lastName, sex, dateOfBirth, clientFacility.FacilityID, registrationDate: dateEnrollment,  NickName: nickName);
                            }
                            else
                            {
                                var person = await registerPersonService.RegisterPerson(firstName, middleName, lastName,
                                    sex, userId, clientFacility.FacilityID, dateOfBirth, nickName: nickName);
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
                                    middleName, dateEnrollment," ", 283, maritalStatusName, physicalAddress, mobileNumber, gender, dobPrecision, dateOfBirth, userId, facilityId);
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
                             
                            //Location
                            if (!string.IsNullOrWhiteSpace(landmark) || (county > 0) || (subcounty > 0) || (ward > 0))
                            {
                                var updatedLocation = await registerPersonService.UpdatePersonLocation(identifiers[0].PersonId, landmark, ward, county, subcounty, userId);
                            }
                            
                            if (!string.IsNullOrWhiteSpace(educationlevel))
                            {
                                var personeducation = await educationLevelService.UpdatePersonEducation(identifiers[0].PersonId, educationlevel, educationoutcome, userId);
                            }
                            if (!string.IsNullOrWhiteSpace(occupation))
                            {
                                var personoccupation = await pocc.Update(identifiers[0].PersonId, occupation, userId);
                            }

                            if (!string.IsNullOrWhiteSpace(mobileNumber) || !string.IsNullOrWhiteSpace(physicalAddress))
                            {
                                //add Person Contact
                                var personContact =
                                    await registerPersonService.UpdatePersonContact(identifiers[0].PersonId,
                                        physicalAddress, mobileNumber);
                            }

                            // update message as processed
                            await registerPersonService.UpdateAfyaMobileInbox(afyaMobileMessage.Id, afyaMobileId, true, DateTime.Now, "success", true);
                        }
                        else
                        {
                            // Add Person
                            var person = await registerPersonService.RegisterPerson(firstName, middleName, lastName, sex,
                                userId, clientFacility.FacilityID, dateOfBirth, nickName: nickName);
                            //Add Person to mst_patient
                            var mstResult = await registerPersonService.InsertIntoBlueCard(firstName, lastName, middleName, dateEnrollment, " ", 283, maritalStatusName, physicalAddress, mobileNumber, gender, dobPrecision, dateOfBirth, userId, facilityId);
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
                                var population = await registerPersonService.addPersonPopulation(person.Id, request.CLIENTS[i].PATIENT_IDENTIFICATION.KEY_POP, userId);

                                // Add Person Location
                                if (!string.IsNullOrWhiteSpace(landmark) || (county > 0) || (subcounty > 0) || (ward > 0))
                                {
                                    var personLocation = await registerPersonService.UpdatePersonLocation(person.Id, landmark, ward, county, subcounty, userId);
                                }

                                if (!string.IsNullOrWhiteSpace(educationlevel))
                                {
                                    var personeducation = await educationLevelService.UpdatePersonEducation(person.Id, educationlevel, educationoutcome, userId);
                                }
                                if (!string.IsNullOrWhiteSpace(occupation))
                                {
                                    var personoccupation = await pocc.Update(person.Id, occupation, userId);
                                }


                                if (!string.IsNullOrWhiteSpace(mobileNumber) || !string.IsNullOrWhiteSpace(physicalAddress))
                                {
                                    //add Person Contact
                                    var personContact = await registerPersonService.addPersonContact(person.Id, physicalAddress,
                                    mobileNumber, string.Empty, string.Empty, userId);
                                }

                                //update message has been processed
                                await registerPersonService.UpdateAfyaMobileInbox(afyaMobileMessage.Id, afyaMobileId, true, DateTime.Now, $"Successfully synchronized demographics for afyamobileid: {afyaMobileId}", true);
                            }
                        }
                    }

                    //update message has been processed
                    await registerPersonService.UpdateAfyaMobileInbox(afyaMobileMessage.Id, afyaMobileId, true, DateTime.Now, $"Successfully synchronized demographics for afyamobileid: {afyaMobileId}", true);
                    trans.Commit();
                    return Result<string>.Valid($"Successfully synchronized demographics for afyamobileid: {afyaMobileId}");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    Log.Error($"Error syncronizing afyamobileid: {afyaMobileId}. Exception Message: {ex.Message},  Inner Exception {ex.InnerException}");
                    return Result<string>.Invalid($"Failed to synchronize clientId: {afyaMobileId} " + ex.Message + " " + ex.InnerException);
                }
            }
        }
    }
}
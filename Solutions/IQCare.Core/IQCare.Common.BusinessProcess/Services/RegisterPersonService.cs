using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using IQCare.Library;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.Common.BusinessProcess.Services
{
    public class RegisterPersonService
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public RegisterPersonService(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<PatientConsent> AddPatientConsent(PatientConsent patientConsent)
        {
            try
            {
                await _unitOfWork.Repository<PatientConsent>().AddAsync(patientConsent);
                await _unitOfWork.SaveAsync();

                return patientConsent;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw e;
            }
        }

        public async Task<List<Identifier>> GetPersonIdentifierType(string codeName)
        {
            try
            {
                var Identifiers = await _unitOfWork.Repository<Identifier>().Get(x => x.Code == codeName && !x.DeleteFlag).ToListAsync();
                return Identifiers;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<PersonContact> GetPersonContactByPersonId(int personId)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("exec pr_OpenDecryptedSession;");
                sql.Append($"SELECT top 1 [Id], [PersonId], CAST(DECRYPTBYKEY([PhysicalAddress]) AS VARCHAR(50)) [PhysicalAddress], CAST(DECRYPTBYKEY([MobileNumber]) AS VARCHAR(50)) [MobileNumber], CAST(DECRYPTBYKEY([AlternativeNumber]) AS VARCHAR(50)) [AlternativeNumber], CAST(DECRYPTBYKEY([EmailAddress]) AS VARCHAR(50)) [EmailAddress], [Active], [DeleteFlag], [CreatedBy]," +
                           $" [CreateDate], AuditData FROM [dbo].[PersonContact] WHERE (DeleteFlag=0 or DeleteFlag is null) and  PersonId ='{personId}'  order by Id desc;");
                sql.Append("exec [dbo].[pr_CloseDecryptedSession];");
                var personContactList = await _unitOfWork.Repository<PersonContact>().FromSql(sql.ToString());

                return personContactList.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<ApiInbox> UpdateAfyaMobileInbox(int id, string afyamobileId = null, bool processed = false, DateTime? dateProcessed = null, string logMessage = null, bool isSuccess = false)
        {
            try
            {
                var afyaMobileMessage = await _unitOfWork.Repository<ApiInbox>().FindByIdAsync(id);
                // afyaMobileMessage.AfyamobileId = afyamobileId;
                afyaMobileMessage.Processed = processed;
                afyaMobileMessage.DateProcessed = dateProcessed;
                afyaMobileMessage.LogMessage = logMessage;
                afyaMobileMessage.IsSuccess = isSuccess;

                _unitOfWork.Repository<ApiInbox>().Update(afyaMobileMessage);
                await _unitOfWork.SaveAsync();

                return afyaMobileMessage;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw e;
            }
        }

        public async Task<ApiInbox> AddAfyaMobileInbox(DateTime dateReceived, string messageType, string afyaMobileId = null, string message = null, bool processed = false, DateTime? dateProcessed = null, string logMessage = null, bool isSuccess = false)
        {
            try
            {
                ApiInbox afyaMobileInbox = new ApiInbox()
                {
                    DateReceived = dateReceived,
                    uid = afyaMobileId,
                    SenderId = 7,
                    Message = message,
                    Processed = processed,
                    DateProcessed = dateProcessed,
                    LogMessage = logMessage,
                    MessageType = messageType,
                    IsSuccess = isSuccess
                };
                await _unitOfWork.Repository<ApiInbox>().AddAsync(afyaMobileInbox);
                await _unitOfWork.SaveAsync();
                return afyaMobileInbox;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw e;
            }
        }

        public async Task<AppStateStore> AddAppStateStore(int personId, int patientId, int appStateId, int? patientMasterVisitId, int? encounterId, string appStateStoreObjects = null)
        {
            try
            {
                AppStateStore appStateStore = new AppStateStore()
                {
                    AppStateId = appStateId,
                    EncounterId = encounterId,
                    PatientId = patientId,
                    PatientMasterVisitId = patientMasterVisitId,
                    DeleteFlag = false,
                    PersonId = personId,
                    StatusDate = DateTime.Now
                };

                await _unitOfWork.Repository<AppStateStore>().AddAsync(appStateStore);
                await _unitOfWork.SaveAsync();

                if (!string.IsNullOrWhiteSpace(appStateStoreObjects))
                {
                    await _unitOfWork.Repository<AppStateStoreObjects>().AddAsync(new AppStateStoreObjects()
                    {
                        AppStateStoreId = appStateStore.Id,
                        AppStateObject = appStateStoreObjects

                    });
                    await _unitOfWork.SaveAsync();
                }

                return appStateStore;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<InteropPlacerValue> AddInteropPlacerValue(int entityId, int identifierType, int interopPlacerTypeId, string placerValue)
        {
            try
            {
                InteropPlacerValue interopPlacerValue = new InteropPlacerValue()
                {
                    EntityId = entityId,
                    IdentifierType = identifierType,
                    InteropPlacerTypeId = interopPlacerTypeId,
                    PlacerValue = placerValue
                };

                await _unitOfWork.Repository<InteropPlacerValue>().AddAsync(interopPlacerValue);
                await _unitOfWork.SaveAsync();

                return interopPlacerValue;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<InteropPlacerValue>> GetInteropPlacerValue(int interopPlacerTypeId, int identifierType, string placerValue)
        {
            try
            {
                var result = await _unitOfWork.Repository<InteropPlacerValue>().Get(x =>
                    x.InteropPlacerTypeId == interopPlacerTypeId && x.IdentifierType == identifierType &&
                    x.PlacerValue == placerValue).ToListAsync();
                
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<PatientARVHistory> GetPatientARVHistory(int patientId, int patientMasterId)
        {
            try
            {
                var result = await _unitOfWork.Repository<PatientARVHistory>().
                    Get(x => x.PatientId == patientId && x.PatientMasterVisitId == x.PatientMasterVisitId).ToListAsync();
                return result.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<PatientARVHistory> AddPatientARVHistory(PatientARVHistory po)
        {
            try
            {


                await _unitOfWork.Repository<PatientARVHistory>().AddAsync(po);
                await _unitOfWork.SaveAsync();

                return po;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<PatientARVHistory> UpdatePatientARVHistory(PatientARVHistory po)
        {
            try
            {
                _unitOfWork.Repository<PatientARVHistory>().Update(po);
                await _unitOfWork.SaveAsync();

                return po;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }

        }




        public async Task<PatientTransferIn> GetPatientTransferIn(int patientId, int patientMasterId)
        {
            try
            {
                var result = await _unitOfWork.Repository<PatientTransferIn>().
                    Get(x => x.PatientId == patientId && x.PatientMasterVisitId == x.PatientMasterVisitId).ToListAsync();
                return result.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<PatientTransferIn> AddPatientTransferIn(PatientTransferIn po)
        {
            try
            {


                await _unitOfWork.Repository<PatientTransferIn>().AddAsync(po);
                await _unitOfWork.SaveAsync();

                return po;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<PatientTransferIn> UpdatePatientTransferIn(PatientTransferIn po)
        {
            try
            {
                _unitOfWork.Repository<PatientTransferIn>().Update(po);
                await _unitOfWork.SaveAsync();

                return po;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }

        }


        public async Task<PatientOVCStatus> GetPatientOVCStatusByPersonId(int personId)
        {
            try
            {
                var result = await _unitOfWork.Repository<PatientOVCStatus>()
                    .Get(x => x.PersonId == personId).ToListAsync();

                return result.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<PatientOVCStatus> AddPatientOVCStatus(PatientOVCStatus po)
        {
            try
            {
                

                await _unitOfWork.Repository<PatientOVCStatus>().AddAsync(po);
                await _unitOfWork.SaveAsync();

                return po;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<PatientOVCStatus> UpdatePatientOVCStauts(PatientOVCStatus po )
        {
            try
            {
                _unitOfWork.Repository<PatientOVCStatus>().Update(po);
                await _unitOfWork.SaveAsync();

                return po;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }

        }
        public async Task<PersonRelationship> GetPersonRelationshipByPatientIdPersonId(int patientId, int personId)
        {
            try
            {
                var result = await _unitOfWork.Repository<PersonRelationship>()
                    .Get(x => x.PersonId == personId && x.PatientId == patientId).ToListAsync();

                return result.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<PersonRelationship> UpdatePersonRelationship(PersonRelationship personRelationship)
        {
            try
            {
                _unitOfWork.Repository<PersonRelationship>().Update(personRelationship);
                await _unitOfWork.SaveAsync();

                return personRelationship;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }

        public async Task<PersonRelationship> addPersonRelationship(int personId, int patientId, int relationshipTypeId, int userId)
        {
            try
            {
                PersonRelationship personRelationship = new PersonRelationship()
                {
                    PersonId = personId,
                    PatientId = patientId,
                    RelationshipTypeId = relationshipTypeId,
                    DeleteFlag = false,
                    CreatedBy = userId,
                    CreateDate = DateTime.Now
                };

                await _unitOfWork.Repository<PersonRelationship>().AddAsync(personRelationship);
                await _unitOfWork.SaveAsync();

                return personRelationship;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<PersonContact>> GetPersonContact(int personId)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("exec pr_OpenDecryptedSession;");
                sql.Append($"SELECT [Id] ,[PersonId], CAST(DECRYPTBYKEY([PhysicalAddress]) AS VARCHAR(50)) [PhysicalAddress]," +
                           $"CAST(DECRYPTBYKEY([MobileNumber]) AS VARCHAR(50)) [MobileNumber]," +
                           $"CAST(DECRYPTBYKEY([AlternativeNumber]) AS VARCHAR(50)) [AlternativeNumber]," +
                           $"CAST(DECRYPTBYKEY([EmailAddress]) AS VARCHAR(50)) [EmailAddress],[Active]," +
                           $"[DeleteFlag],[CreatedBy],[CreateDate],[AuditData] FROM [dbo].[PersonContact] WHERE PersonId = '{personId}' AND DeleteFlag = 0;");
                sql.Append("exec [dbo].[pr_CloseDecryptedSession];");

                var personContact = await _unitOfWork.Repository<PersonContact>().FromSql(sql.ToString());
                return personContact;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<PersonContact> UpdatePersonContact(int personId, string physicalAddress, string mobileNumber, string emailAddress = "", string alternativeNumber = "")
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("exec pr_OpenDecryptedSession;");
                sql.Append($"UPDATE PersonContact SET PhysicalAddress = ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '{physicalAddress}'), " +
                           $"MobileNumber = ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '{mobileNumber}')," +
                           $"AlternativeNumber = ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '{alternativeNumber}')," +
                           $"EmailAddress = ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '{emailAddress}')" +
                           $" WHERE PersonId = {personId} AND DeleteFlag = 0;");

                sql.Append($"SELECT [Id] ,[PersonId], CAST(DECRYPTBYKEY([PhysicalAddress]) AS VARCHAR(50)) [PhysicalAddress]," +
                           $"CAST(DECRYPTBYKEY([MobileNumber]) AS VARCHAR(50)) [MobileNumber]," +
                           $"CAST(DECRYPTBYKEY([AlternativeNumber]) AS VARCHAR(50)) [AlternativeNumber]," +
                           $"CAST(DECRYPTBYKEY([EmailAddress]) AS VARCHAR(50)) [EmailAddress],[Active]," +
                           $"[DeleteFlag],[CreatedBy],[CreateDate],[AuditData] FROM [dbo].[PersonContact] WHERE PersonId = '{personId}' AND DeleteFlag = 0;");
                sql.Append("exec [dbo].[pr_CloseDecryptedSession];");

                var personContactInsert = await _unitOfWork.Repository<PersonContact>().FromSql(sql.ToString());

                return personContactInsert.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<PersonContact> addPersonContact(int personId, string physicalAddress, string mobileNumber, string alternativeNumber, string emailAddress, int userId)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("exec pr_OpenDecryptedSession;");
                sql.Append("INSERT INTO PersonContact(PersonId,PhysicalAddress,MobileNumber,AlternativeNumber,EmailAddress,Active,DeleteFlag,CreateDate,CreatedBy, AuditData)");
                sql.Append($"VALUES('{personId}', ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '{physicalAddress}'), ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '{mobileNumber}'), ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '{alternativeNumber}'), ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '{emailAddress}'), 1, 0, GETDATE(), '{userId}', NULL);");
                sql.Append("SELECT [Id], [PersonId], CAST(DECRYPTBYKEY([PhysicalAddress]) AS VARCHAR(50)) [PhysicalAddress], CAST(DECRYPTBYKEY([MobileNumber]) AS VARCHAR(50)) [MobileNumber], CAST(DECRYPTBYKEY([AlternativeNumber]) AS VARCHAR(50)) [AlternativeNumber], CAST(DECRYPTBYKEY([EmailAddress]) AS VARCHAR(50)) [EmailAddress], [Active], [DeleteFlag], [CreatedBy], [CreateDate], AuditData FROM [dbo].[PersonContact] WHERE Id = SCOPE_IDENTITY();");
                sql.Append("exec [dbo].[pr_CloseDecryptedSession];");

                var personContactInsert = await _unitOfWork.Repository<PersonContact>().FromSql(sql.ToString());

                return personContactInsert.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<PersonPriority>> addPersonPriority(int personId, List<int> priorities, int userid)
        {
            try
            {
                List<PersonPriority> personPriorities = new List<PersonPriority>();
                priorities.ForEach(x => personPriorities.Add(new PersonPriority()
                {
                    PersonId = personId,
                    PriorityId = x,
                    DeleteFlag = false,
                    CreatedBy = userid,
                    CreateDate = DateTime.Now
                }));

                await _unitOfWork.Repository<PersonPriority>().AddRangeAsync(personPriorities);
                await _unitOfWork.SaveAsync();

                return personPriorities;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<PersonPopulation>> UpdatePersonPopulation(int personId, List<int> populations, int userId)
        {
            try
            {
                var personPopulations = await _unitOfWork.Repository<PersonPopulation>()
                    .Get(x => x.PersonId == personId && x.DeleteFlag == false).ToListAsync();
                foreach (var population in personPopulations)
                {
                    population.DeleteFlag = true;
                    _unitOfWork.Repository<PersonPopulation>().Update(population);
                    await _unitOfWork.SaveAsync();
                }

                await addPersonPopulation(personId, populations, userId);
                await _unitOfWork.SaveAsync();

                return personPopulations;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<PersonPopulation>> addPersonPopulation(int personId, List<int> populations, int userId)
        {
            try
            {
                List<PersonPopulation> personPopulations = new List<PersonPopulation>();
                var populationType = "Key Population";
                for (int i = 0; i < populations.Count; i++)
                {
                    var keyPop = await _unitOfWork.Repository<LookupItemView>()
                        .Get(x => x.MasterName == "HTSKeyPopulation" && x.ItemId == populations[i])
                        .FirstOrDefaultAsync();

                    if (keyPop !=null && keyPop.ItemName == "Not Applicable")
                    {
                        populationType = "General Population";
                    }
                }
                
                populations.ForEach(t => personPopulations.Add(new PersonPopulation
                {
                    PersonId = personId,
                    PopulationType = populationType,
                    PopulationCategory = t,
                    Active = true,
                    DeleteFlag = false,
                    CreatedBy = userId,
                    CreateDate = DateTime.Now
                }));

                await _unitOfWork.Repository<PersonPopulation>().AddRangeAsync(personPopulations);
                await _unitOfWork.SaveAsync();

                return personPopulations;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

      
        public async Task<List<PersonLocation>> GetPersonLocation(int personId)
        {
            try
            {
                var result = await _unitOfWork.Repository<PersonLocation>()
                    .Get(x => x.PersonId == personId && x.DeleteFlag == false).ToListAsync();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public async Task<PersonLocation> UpdatePersonLocation(int personId, string landmark, int ward = 0, int county = 0, int subcounty = 0, int userid = 1)
        {
            try
            {
                var location = await _unitOfWork.Repository<PersonLocation>().Get(x => x.PersonId == personId)
                    .FirstOrDefaultAsync();
                if (location != null)
                {

                    if (!string.IsNullOrEmpty(landmark))
                    {
                        location.LandMark = landmark;
                    }
                    location.Ward = ward;
                    location.County = county;
                    location.SubCounty = subcounty;

                    _unitOfWork.Repository<PersonLocation>().Update(location);
                    await _unitOfWork.SaveAsync();
                }
                else
                {
                    landmark = string.IsNullOrWhiteSpace(landmark) ? "n/a" : landmark;
                    int user;
                    if (userid > 0)
                    {
                        user = userid;
                    }
                    else
                    {
                        user = 1;
                    }

                    location = await addPersonLocation(personId, county, subcounty, ward, "", landmark, user);
                }
                return location;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<PersonLocation> addPersonLocation(int personId, int countyId, int subCountyId, int wardId, string village, string landmark, int userId)
        {
            try
            {
                PersonLocation personLocation = new PersonLocation()
                {
                    PersonId = personId,
                    County = countyId,
                    SubCounty = subCountyId,
                    Ward = wardId,
                    Village = village,
                    Location = "",
                    SubLocation = "",
                    LandMark = landmark,
                    NearestHealthCentre = "",
                    Active = false,
                    DeleteFlag = false,
                    CreateDate = DateTime.Now,
                    CreatedBy = userId
                };

                await _unitOfWork.Repository<PersonLocation>().AddAsync(personLocation);
                await _unitOfWork.SaveAsync();

                return personLocation;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<PersonMaritalStatus>> GetPersonMaritalStatus(int personId)
        {
            try
            {
                var result = await _unitOfWork.Repository<PersonMaritalStatus>()
                    .Get(x => x.PersonId == personId && x.DeleteFlag == false).ToListAsync();

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<PersonMaritalStatus> UpdateMaritalStatus(int personId, int maritalStatusId)
        {
            try
            {
                var maritalStatus = await _unitOfWork.Repository<PersonMaritalStatus>().Get(x => x.PersonId == personId && x.DeleteFlag == false)
                    .FirstOrDefaultAsync();

                maritalStatus.MaritalStatusId = maritalStatusId;
                _unitOfWork.Repository<PersonMaritalStatus>().Update(maritalStatus);

                return maritalStatus;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<PersonMaritalStatus> AddMaritalStatus(int personId, int maritalStatusId, int userId)
        {
            try
            {
                PersonMaritalStatus personMaritalStatus = new PersonMaritalStatus()
                {
                    PersonId = personId,
                    MaritalStatusId = maritalStatusId,
                    Active = true,
                    DeleteFlag = false,
                    CreatedBy = userId,
                    CreateDate = DateTime.Now
                };

                await _unitOfWork.Repository<PersonMaritalStatus>().AddAsync(personMaritalStatus);
                await _unitOfWork.SaveAsync();

                return personMaritalStatus;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<PersonIdentifier>> getPersonIdentifiers(string identifierValue, int identifierId)
        {
            try
            {
                var identifiers = await _unitOfWork.Repository<PersonIdentifier>().Get(x =>
                    x.IdentifierValue == identifierValue && x.IdentifierId == identifierId).ToListAsync();

                return identifiers;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<PersonIdentifier> addPersonIdentifiers(int personId, int identifierId, string identifierValue, int userId)
        {
            try
            {
                PersonIdentifier personIdentifier = new PersonIdentifier()
                {
                    PersonId = personId,
                    IdentifierId = identifierId,
                    IdentifierValue = identifierValue,
                    DeleteFlag = false,
                    CreatedBy = userId,
                    CreateDate = DateTime.Now
                };

                await _unitOfWork.Repository<PersonIdentifier>().AddAsync(personIdentifier);
                await _unitOfWork.SaveAsync();

                return personIdentifier;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task DynamicEnrollment(int patientId, int serviceAreaId, int createdBy, DateTime dateOfEnrollment, List<ServiceIdentifiersList> serviceIdentifiersList, bool transferIn)
        {
            try
            {
                for (int i = 0; i < serviceIdentifiersList.Count; i++)
                {
                    var previouslyIdentifiers = await _unitOfWork.Repository<PatientIdentifier>().Get(y =>
                        y.IdentifierValue == serviceIdentifiersList[i].IdentifierValue &&
                        y.IdentifierTypeId == serviceIdentifiersList[i].IdentifierId && y.PatientId != patientId).ToListAsync();

                    if (previouslyIdentifiers.Count > 0)
                    {
                        var exception = new Exception("No: " + serviceIdentifiersList[i].IdentifierValue + " already exists");
                        throw exception;
                    }
                }


                var enrollmentVisitType = await _unitOfWork.Repository<LookupItemView>().Get(x => x.MasterName == "VisitType" && x.ItemName == "Enrollment").FirstOrDefaultAsync();
                int? visitType = enrollmentVisitType != null ? enrollmentVisitType.ItemId : 0;

                var enrollmentPatientMasterVisit = await _unitOfWork.Repository<PatientMasterVisit>().Get(x =>
                    x.PatientId == patientId && x.ServiceId == serviceAreaId && x.VisitType == visitType).ToListAsync();

                if (enrollmentPatientMasterVisit.Count == 0)
                {
                    var patientMasterVisit = new PatientMasterVisit()
                    {
                        PatientId = patientId,
                        ServiceId = serviceAreaId,
                        Start = DateTime.Now,
                        End = null,
                        Active = false,
                        VisitDate = DateTime.Now,
                        VisitType = visitType,
                        Status = 1,
                        CreateDate = DateTime.Now,
                        DeleteFlag = false,
                        CreatedBy = createdBy
                    };


                    await _unitOfWork.Repository<PatientMasterVisit>().AddAsync(patientMasterVisit);
                    await _unitOfWork.SaveAsync();
                }


                var previousPatientEnrollment = await _unitOfWork.Repository<PatientEnrollment>().Get(x =>
                        x.PatientId == patientId && x.ServiceAreaId == serviceAreaId && x.DeleteFlag == false)
                    .ToListAsync();
                if (previousPatientEnrollment.Count == 0)
                {
                    var patientEnrollment = new PatientEnrollment()
                    {
                        PatientId = patientId,
                        ServiceAreaId = serviceAreaId,
                        EnrollmentDate = dateOfEnrollment,
                        EnrollmentStatusId = 0,
                        TransferIn = false,
                        CareEnded = false,
                        DeleteFlag = false,
                        CreatedBy = createdBy,
                        CreateDate = DateTime.Now
                    };


                    await _unitOfWork.Repository<PatientEnrollment>().AddAsync(patientEnrollment);
                    await _unitOfWork.SaveAsync();

                    if (serviceIdentifiersList.Any())
                    {
                        List<PatientIdentifier> patientIdentifierList = new List<PatientIdentifier>();
                        foreach (var x in serviceIdentifiersList)
                        {
                            if (x.IdentifierValue != null)
                            {
                                patientIdentifierList.Add(new PatientIdentifier()
                                {
                                    PatientId = patientId,
                                    PatientEnrollmentId = patientEnrollment.Id,
                                    IdentifierTypeId = x.IdentifierId,
                                    IdentifierValue = x.IdentifierValue,
                                    DeleteFlag = false,
                                    CreatedBy = createdBy,
                                    CreateDate = DateTime.Now,
                                    Active = true
                                });
                            }

                            await _unitOfWork.Repository<PatientIdentifier>().AddRangeAsync(patientIdentifierList);
                            await _unitOfWork.SaveAsync();
                        }
                    }
                }
                else
                {
                    if (serviceIdentifiersList.Any())
                    {
                        foreach (var x in serviceIdentifiersList)
                        {
                            if (x.IdentifierValue != null)
                            {
                                var patientIdentifiers = await _unitOfWork.Repository<PatientIdentifier>().Get(y =>
                                    y.PatientId == patientId && y.IdentifierTypeId == x.IdentifierId &&
                                    y.DeleteFlag == false).ToListAsync();

                                if (patientIdentifiers.Count > 0)
                                {
                                    patientIdentifiers[0].IdentifierValue = x.IdentifierValue;

                                    _unitOfWork.Repository<PatientIdentifier>().Update(patientIdentifiers[0]);
                                    await _unitOfWork.SaveAsync();
                                }
                                else
                                {
                                    var patientIdentifier = new PatientIdentifier()
                                    {
                                        PatientId = patientId,
                                        PatientEnrollmentId = previousPatientEnrollment[0].Id,
                                        IdentifierTypeId = x.IdentifierId,
                                        IdentifierValue = x.IdentifierValue,
                                        DeleteFlag = false,
                                        CreatedBy = createdBy,
                                        CreateDate = DateTime.Now,
                                        Active = true
                                    };

                                    await _unitOfWork.Repository<PatientIdentifier>().AddAsync(patientIdentifier);
                                    await _unitOfWork.SaveAsync();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e.Message + " " + e.InnerException);
                throw e;
            }
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<PatientIdentifier> EnrollPatient(string enrollmentNo, int patientId, int serviceAreaId, int createdBy, DateTime dateOfEnrollment)
        {
            try
            {
                // generate a random alphabet prefix for hts
                string originalEnrollmentNo = enrollmentNo;
                if(serviceAreaId == 2)
                    enrollmentNo = RandomString(6) + "-" + enrollmentNo;

                var previouslyIdentifiers = await _unitOfWork.Repository<PatientIdentifier>().Get(y =>
                        y.IdentifierValue == enrollmentNo && y.IdentifierTypeId == 8)
                    .ToListAsync();

                if (previouslyIdentifiers.Count > 0)
                {
                    var exception = new Exception("No: " + originalEnrollmentNo + " already exists");
                    throw exception;
                }

                var enrollmentVisitType = await _unitOfWork.Repository<LookupItemView>().Get(x => x.MasterName == "VisitType" && x.ItemName == "Enrollment").FirstOrDefaultAsync();
                int? visitType = enrollmentVisitType != null ? enrollmentVisitType.ItemId : 0;
                var patientMasterVisit = new PatientMasterVisit()
                {
                    PatientId = patientId,
                    ServiceId = serviceAreaId,
                    Start = DateTime.Now,
                    End = null,
                    Active = false,
                    VisitDate = DateTime.Now,
                    VisitType = visitType,
                    Status = 1,
                    CreateDate = DateTime.Now,
                    DeleteFlag = false,
                    CreatedBy = createdBy
                };

                await _unitOfWork.Repository<PatientMasterVisit>().AddAsync(patientMasterVisit);
                await _unitOfWork.SaveAsync();

                var patientEnrollment = new PatientEnrollment()
                {
                    PatientId = patientId,
                    ServiceAreaId = serviceAreaId,
                    EnrollmentDate = dateOfEnrollment,
                    EnrollmentStatusId = 0,
                    TransferIn = false,
                    CareEnded = false,
                    DeleteFlag = false,
                    CreatedBy = createdBy,
                    CreateDate = DateTime.Now
                };

                await _unitOfWork.Repository<PatientEnrollment>().AddAsync(patientEnrollment);
                await _unitOfWork.SaveAsync();

                var patientIdentifier = new PatientIdentifier()
                {
                    PatientId = patientId,
                    PatientEnrollmentId = patientEnrollment.Id,
                    IdentifierTypeId = 8,
                    IdentifierValue = enrollmentNo,
                    DeleteFlag = false,
                    CreatedBy = createdBy,
                    CreateDate = DateTime.Now,
                    Active = true

                };

                await _unitOfWork.Repository<PatientIdentifier>().AddAsync(patientIdentifier);
                await _unitOfWork.SaveAsync();

                return patientIdentifier;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<MstPatient>> InsertIntoBlueCard(string firstName, string lastName, string midName, DateTime dateOfEnrollment, string patientEnrollmentID, int moduleId,
            string maritalStatusName, string physicalAddress, string mobileNumber, string sex, string isDobPrecision, DateTime dob, int createdBy, string posId)
        {
            try
            {
                firstName = string.IsNullOrWhiteSpace(firstName) ? "" : firstName.Replace("'", "''");
                midName = string.IsNullOrWhiteSpace(midName) ? "" : midName.Replace("'", "''");
                lastName = string.IsNullOrWhiteSpace(lastName) ? "" : lastName.Replace("'", "''");

                LookupLogic lookupLogic = new LookupLogic(_unitOfWork);
                Facility facility = await _unitOfWork.Repository<Facility>().Get(x => x.PosID == posId).FirstOrDefaultAsync();
                if (facility == null)
                {
                    facility = await _unitOfWork.Repository<Facility>().Get(x => x.DeleteFlag == 0).FirstOrDefaultAsync();
                }

                var referralId = await lookupLogic.GetDecodeIdByName("VCT", 17);

                var maritalStatusId = await lookupLogic.GetDecodeIdByName(maritalStatusName, 17);
                var address = physicalAddress == null ? " " : physicalAddress;
                var phone = mobileNumber == null ? " " : mobileNumber;
                var dobPrecision = isDobPrecision == "ESTIMATED" ? 1 : 0;

                var gender = 0;
                if (sex == "Male")
                {
                    gender = 16;
                }
                else if (sex == "Female")
                {
                    gender = 17;
                }

                //string dateOfBirth = dob.ToString("yyyy-MM-dd");

                StringBuilder sql = new StringBuilder();
                sql.Append("exec pr_OpenDecryptedSession;");
                sql.Append("Insert Into mst_Patient(FirstName, LastName, MiddleName, LocationID, PatientEnrollmentID, ReferredFrom, RegistrationDate, Sex, DOB, DobPrecision, MaritalStatus, Address, Phone, UserID, PosId, Status, DeleteFlag, CreateDate,MovedToPatientTable)");
                sql.Append("Values(");
                sql.Append($"ENCRYPTBYKEY(KEY_GUID('Key_CTC'), @firstName),");
                sql.Append($"ENCRYPTBYKEY(KEY_GUID('Key_CTC'), @lastName),");
                sql.Append($"ENCRYPTBYKEY(KEY_GUID('Key_CTC'), @midName),");
                sql.Append($"@FacilityID,");
                sql.Append($"@patientEnrollmentID,");
                sql.Append($"@referralId,");
                sql.Append($"@dateOfEnrollment,");
                sql.Append($"@gender,");
                sql.Append($"@dateOfBirth,");
                sql.Append($"@dobPrecision,");
                sql.Append($"@maritalStatusId,");
                sql.Append($"ENCRYPTBYKEY(KEY_GUID('Key_CTC'), @address),");
                sql.Append($"ENCRYPTBYKEY(KEY_GUID('Key_CTC'), @phone),");
                sql.Append($" @createdBy,");
                sql.Append($"@PosID,");
                sql.Append("0,");
                sql.Append("0,");
                sql.Append($"@dateOfEnrollment,");
                sql.Append("1");
                sql.Append(");");

                sql.Append("SELECT Ptn_Pk, CAST(DECRYPTBYKEY([FirstName]) AS VARCHAR(50)) AS FirstName, CAST(DECRYPTBYKEY([LastName]) AS VARCHAR(50)) AS LastName, LocationID FROM [dbo].[mst_Patient] WHERE [Ptn_Pk] = SCOPE_IDENTITY();");
                sql.Append("exec [dbo].[pr_CloseDecryptedSession];");

                var firstNameParameter = new SqlParameter();
                firstNameParameter.SqlDbType = SqlDbType.VarChar;
                firstNameParameter.ParameterName = "@firstName";
                firstNameParameter.Size = -1;
                firstNameParameter.Value = firstName;

                var lastNameParameter = new SqlParameter();
                lastNameParameter.SqlDbType = SqlDbType.VarChar;
                lastNameParameter.ParameterName = "@lastName";
                lastNameParameter.Size = -1;
                lastNameParameter.Value = lastName;

                var midNameParameter = new SqlParameter();
                midNameParameter.SqlDbType = SqlDbType.VarChar;
                midNameParameter.ParameterName = "@midName";
                midNameParameter.Size = -1;
                midNameParameter.Value = midName;

                var addressParameter = new SqlParameter();
                addressParameter.SqlDbType = SqlDbType.VarChar;
                addressParameter.ParameterName = "@address";
                addressParameter.Size = -1;
                addressParameter.Value = address;

                var phoneParameter = new SqlParameter();
                phoneParameter.SqlDbType = SqlDbType.VarChar;
                phoneParameter.ParameterName = "@phone";
                phoneParameter.Size = -1;
                phoneParameter.Value = phone;

                var facilityIDParameter = new SqlParameter("@FacilityID", facility.FacilityID);
                var referralIdParameter = new SqlParameter("@referralId", referralId);
                var dateOfEnrollmentParameter = new SqlParameter("@dateOfEnrollment", dateOfEnrollment);
                var patientEnrollmentIdParameter = new SqlParameter("@patientEnrollmentID", patientEnrollmentID);
                var genderParameter = new SqlParameter("@gender", gender);
                var dateOfBirthParameter = new SqlParameter("@dateOfBirth", dob);
                var dobPrecisionParameter = new SqlParameter("@dobPrecision", dobPrecision);
                var maritalStatusIdParameter = new SqlParameter("@maritalStatusId", maritalStatusId);
                var createdByParameter = new SqlParameter("@createdBy", createdBy);
                var posIdParameter = new SqlParameter("@PosID", facility.PosID);
                var moduleIdParameter = new SqlParameter("@moduleId", moduleId);

                var result = await _unitOfWork.Repository<MstPatient>().FromSql(sql.ToString(), parameters:new []
                {
                    firstNameParameter,
                    lastNameParameter,
                    midNameParameter,
                    facilityIDParameter,
                    referralIdParameter,
                    patientEnrollmentIdParameter,
                    dateOfEnrollmentParameter,
                    genderParameter,
                    dateOfBirthParameter,
                    dobPrecisionParameter,
                    maritalStatusIdParameter,
                    addressParameter,
                    phoneParameter,
                    createdByParameter,
                    posIdParameter
                });

                StringBuilder sqlBuilder = new StringBuilder();
                sqlBuilder.Append("Insert Into Lnk_PatientProgramStart(Ptn_pk, ModuleId, StartDate, UserID, CreateDate)");
                sqlBuilder.Append("Values(");
                sqlBuilder.Append($"@ptn_pk,");
                sqlBuilder.Append($"@moduleId,");
                sqlBuilder.Append($"@dateOfEnrollment,");
                sqlBuilder.Append($"@createdBy,");
                sqlBuilder.Append($"@dateOfEnrollment");
                sqlBuilder.Append(");");

                var ptn_pkParameter = new SqlParameter("@ptn_pk", result[0].Ptn_Pk);

                var insertResult = await _unitOfWork.Context.Database.ExecuteSqlCommandAsync(sqlBuilder.ToString(), parameters:new []
                {
                    ptn_pkParameter,
                    moduleIdParameter,
                    dateOfEnrollmentParameter,
                    createdByParameter
                });

                return result;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                Log.Error(e.InnerException.ToString());
                throw e;
            }
        }



        public async Task<List<MstPatient>> UpdateBlueCard(int? ptn_pk, string patientEnrollmentID, int moduleId)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("exec pr_OpenDecryptedSession;");
                sql.Append($"UPDATE mst_Patient SET PatientEnrollmentID = {patientEnrollmentID} WHERE Ptn_Pk = {ptn_pk};");
                sql.Append($"UPDATE Lnk_PatientProgramStart SET ModuleId = {moduleId} WHERE Ptn_Pk = {ptn_pk};");

                sql.Append($"SELECT Ptn_Pk, CAST(DECRYPTBYKEY([FirstName]) AS VARCHAR(50)) AS FirstName, CAST(DECRYPTBYKEY([LastName]) AS VARCHAR(50)) AS LastName, LocationID FROM [dbo].[mst_Patient] WHERE [Ptn_Pk] = {ptn_pk};");
                sql.Append("exec [dbo].[pr_CloseDecryptedSession];");

                var patientUpdate = await _unitOfWork.Repository<MstPatient>().FromSql(sql.ToString());

                return patientUpdate;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Patient> UpdatePatient(int patientId, DateTime dateOfBirth, string facilityId)
        {
            try
            {
                var dateOfBirthParameter = new SqlParameter("@dateOfBirth", dateOfBirth);
                var facilityIdParameter = new SqlParameter("@facilityId", facilityId);
                var patientIdParameter = new SqlParameter("@patientId", patientId);

                StringBuilder sql = new StringBuilder();
                sql.Append("exec pr_OpenDecryptedSession;");
                sql.Append($"UPDATE Patient SET DateOfBirth = @dateOfBirth, FacilityId = @facilityId WHERE Id = @patientId;");
                sql.Append($"SELECT [Id],[ptn_pk],[PersonId],[PatientIndex],[PatientType],[FacilityId],[Active],[DateOfBirth]," +
                           $"[DobPrecision],CAST(DECRYPTBYKEY(NationalId) AS VARCHAR(50)) [NationalId],[DeleteFlag],[CreatedBy]," +
                           $"[CreateDate],[AuditData],[RegistrationDate] FROM Patient WHERE Id = @patientId;");
                sql.Append("exec [dbo].[pr_CloseDecryptedSession];");


                var patientUpdate = await _unitOfWork.Repository<Patient>().FromSql(sql.ToString(), parameters:new []
                {
                    dateOfBirthParameter,
                    facilityIdParameter,
                    patientIdParameter
                });
                return patientUpdate.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Patient> AddPatient(int personID, int userId, int ptn_pk, string facilityId = "")
        {
            try
            {
                var facility = await _unitOfWork.Repository<Facility>().Get(x => x.DeleteFlag == 0).FirstOrDefaultAsync();
                var patientType = await _unitOfWork.Repository<LookupItemView>()
                    .Get(x => x.MasterName == "PatientType" && x.ItemName == "New").FirstOrDefaultAsync();

                var patient = await this.GetPatientByPersonId(personID);
                if (patient == null)
                {
                    var person = await this.GetPerson(personID);
                    DateTime dateOfBirth = DateTime.Now;
                    if (person != null)
                    {
                        dateOfBirth = person.DateOfBirth.HasValue? person.DateOfBirth.Value:DateTime.Now;
                    }

                    if (string.IsNullOrWhiteSpace(facilityId))
                    {
                        facilityId = facility.PosID;
                    }

                    var sqlPatient = "exec pr_OpenDecryptedSession;" +
                                     "Insert Into  Patient(ptn_pk,PersonId,PatientIndex,PatientType,FacilityId,Active,DateOfBirth,NationalId,DeleteFlag,CreatedBy,CreateDate,AuditData,DobPrecision)" +
                                     $"Values(@ptn_pk, @personID, @PatientIndex, @PatientType, @facilityId, 1," +
                                     $" @dateOfBirth, ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '99999999'), 0, @userId, GETDATE()," +
                                     $"NULL, 1);" +
                                     $"SELECT [Id],[ptn_pk],[PersonId],[PatientIndex],[PatientType],[FacilityId],[Active],[DateOfBirth]," +
                                     $"[DobPrecision],CAST(DECRYPTBYKEY(NationalId) AS VARCHAR(50)) [NationalId],[DeleteFlag],[CreatedBy]," +
                                     $"[CreateDate],[AuditData],[RegistrationDate] FROM [dbo].[Patient] WHERE Id = SCOPE_IDENTITY();" +
                                     $"exec [dbo].[pr_CloseDecryptedSession];";

                    var ptn_pkParameter = new SqlParameter("@ptn_pk", ptn_pk);
                    var personIDParameter = new SqlParameter("@personID", personID);
                    var patientIndexParameter = new SqlParameter("@PatientIndex", DateTime.Now.Year + '-' + personID);
                    var patientTypeParameter = new SqlParameter("@PatientType", patientType.ItemId);
                    var facilityIdParameter = new SqlParameter("@facilityId", facilityId);
                    var userIdParameter = new SqlParameter("@userId", userId);
                    var dateOfBirthParameter = new SqlParameter("@dateOfBirth", dateOfBirth);

                    var patientInsert = await _unitOfWork.Repository<Patient>().FromSql(sqlPatient, parameters:new []
                    {
                        ptn_pkParameter,
                        personIDParameter,
                        patientIndexParameter,
                        patientTypeParameter,
                        facilityIdParameter,
                        userIdParameter,
                        dateOfBirthParameter
                    });

                    return patientInsert.FirstOrDefault();
                }
                else
                {
                    return patient;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Patient> AddPatient(int personID, int userId, string facilityId = "")
        {
            try
            {
                var facility = await _unitOfWork.Repository<Facility>().Get(x => x.DeleteFlag == 0).FirstOrDefaultAsync();
                var patientType = await _unitOfWork.Repository<LookupItemView>()
                    .Get(x => x.MasterName == "PatientType" && x.ItemName == "New").FirstOrDefaultAsync();

                var patient = await this.GetPatientByPersonId(personID);
                if (patient == null)
                {
                    var person = await this.GetPerson(personID);
                    DateTime dateOfBirth = DateTime.Now;
                    if (person != null)
                    {
                        dateOfBirth = person.DateOfBirth.HasValue?person.DateOfBirth.Value:DateTime.Now;
                    }

                    if (string.IsNullOrWhiteSpace(facilityId))
                    {
                        facilityId = facility.PosID;
                    }

                    var sqlPatient = "exec pr_OpenDecryptedSession;" +
                                     "Insert Into  Patient(ptn_pk,PersonId,PatientIndex,PatientType,FacilityId,Active,DateOfBirth,NationalId,DeleteFlag,CreatedBy,CreateDate,AuditData,DobPrecision)" +
                                     $"Values(0, @personID, @PatientIndex, @patientType, @facilityId, 1," +
                                     $"@dateOfBirth, ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '99999999'), 0, @userId, GETDATE()," +
                                     $"NULL, 1);" +
                                     $"SELECT [Id],[ptn_pk],[PersonId],[PatientIndex],[PatientType],[FacilityId],[Active],[DateOfBirth]," +
                                     $"[DobPrecision],CAST(DECRYPTBYKEY(NationalId) AS VARCHAR(50)) [NationalId],[DeleteFlag],[CreatedBy]," +
                                     $"[CreateDate],[AuditData],[RegistrationDate] FROM [dbo].[Patient] WHERE Id = SCOPE_IDENTITY();" +
                                     $"exec [dbo].[pr_CloseDecryptedSession];";

                    var personIDParameter = new SqlParameter("@personID", personID);
                    var patientIndexParameter = new SqlParameter("@PatientIndex", DateTime.Now.Year + '-' + personID);
                    var patientTypeParameter = new SqlParameter("@patientType", patientType.ItemId);
                    var facilityIdParameter = new SqlParameter("@facilityId", facilityId);
                    var dateOfBirthParameter = new SqlParameter("@dateOfBirth", dateOfBirth);
                    var userIdParameter = new SqlParameter("@userId", userId);

                    var patientInsert = await _unitOfWork.Repository<Patient>().FromSql(sqlPatient, parameters:new []
                    {
                        personIDParameter,
                        patientIndexParameter,
                        patientTypeParameter,
                        facilityIdParameter,
                        dateOfBirthParameter,
                        userIdParameter
                    });

                    return patientInsert.FirstOrDefault();
                }
                else
                {
                    return patient;
                }
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw e;
            }
        }

        public async Task<Patient> GetPatientByPersonId(int personId)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("exec pr_OpenDecryptedSession;");
                sql.Append(
                    "SELECT [Id],[ptn_pk],[PersonId],[PatientIndex],[PatientType],[FacilityId],[Active],[DateOfBirth]," +
                    "[DobPrecision],CAST(DECRYPTBYKEY(NationalId) AS VARCHAR(50)) [NationalId],[DeleteFlag],[CreatedBy]," +
                    $"[CreateDate],[AuditData],[RegistrationDate] FROM [dbo].[Patient] WHERE Personid = '{personId}';");
                sql.Append("exec [dbo].[pr_CloseDecryptedSession];");

                var patient = await _unitOfWork.Repository<Patient>().FromSql(sql.ToString());

                return patient.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Patient> GetPatient(int patientId)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("exec pr_OpenDecryptedSession;");
                sql.Append(
                    "SELECT [Id],[ptn_pk],[PersonId],[PatientIndex],[PatientType],[FacilityId],[Active],[DateOfBirth]," +
                    "[DobPrecision],CAST(DECRYPTBYKEY(NationalId) AS VARCHAR(50)) [NationalId],[DeleteFlag],[CreatedBy]," +
                    $"[CreateDate],[AuditData],[RegistrationDate] FROM [dbo].[Patient] WHERE Id = '{patientId}';");
                sql.Append("exec [dbo].[pr_CloseDecryptedSession];");

                var patient = await _unitOfWork.Repository<Patient>().FromSql(sql.ToString());

                return patient.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Person> GetPerson(int personId)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("exec pr_OpenDecryptedSession;");
                sql.Append($"SELECT [Id] , CAST(DECRYPTBYKEY(FirstName) AS VARCHAR(50)) [FirstName] ,CAST(DECRYPTBYKEY(MidName) AS VARCHAR(50)) MidName" +
                           $",CAST(DECRYPTBYKEY(LastName) AS VARCHAR(50)) [LastName],CAST(DECRYPTBYKEY(NickName) AS VARCHAR(50)) [NickName] ,[Sex] ,[Active] ,[DeleteFlag] ,[CreateDate] " +
                           $",[CreatedBy] ,[AuditData] ,[DateOfBirth] ,[DobPrecision],FacilityId ,[RegistrationDate] FROM [dbo].[Person] WHERE Id = '{personId}';");
                sql.Append("exec [dbo].[pr_CloseDecryptedSession];");

                var person = await _unitOfWork.Repository<Person>().FromSql(sql.ToString());

                return person.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Person> UpdatePerson(int personId, string firstName, string middleName, string lastName, int sex, DateTime dateOfBirth, int facilityId, DateTime? registrationDate = null, bool dobPrecision = true,string NickName="")
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                firstName = string.IsNullOrWhiteSpace(firstName) ? "" : firstName.Replace("'", "''");
                middleName = string.IsNullOrWhiteSpace(middleName) ? "" : middleName.Replace("'", "''");
                lastName = string.IsNullOrWhiteSpace(lastName) ? "" : lastName.Replace("'", "''");
                if (string.IsNullOrEmpty(NickName))
                {
                    var registeredPerson = await this.GetPerson(personId);
                    if (registeredPerson != null)
                    {
                        if (!string.IsNullOrEmpty(registeredPerson.NickName))
                        {
                            NickName = registeredPerson.NickName;
                        }
                        else
                        {
                            NickName = "";
                        }
                    }
                }
                NickName = string.IsNullOrWhiteSpace(NickName) ? "" : NickName.Replace("'", "''");
                sql.Append("exec pr_OpenDecryptedSession;");
                sql.Append($"UPDATE Person SET FirstName = ENCRYPTBYKEY(KEY_GUID('Key_CTC'), @firstName), " +
                           $"MidName = ENCRYPTBYKEY(KEY_GUID('Key_CTC'), @middleName), " +
                           $"LastName = ENCRYPTBYKEY(KEY_GUID('Key_CTC'), @lastName), " +
                           $"NickName=ENCRYPTBYKEY(KEY_GUID('Key_CTC'), @nickName), " +
                           $"Sex = @sex, DateOfBirth = @dateOfBirth, " +
                           $"RegistrationDate = @registrationDate, FacilityId = @facilityId, " +
                           $"[DobPrecision] = @dobPrecision WHERE Id = @personId; ");
                sql.Append($"SELECT [Id] , CAST(DECRYPTBYKEY(FirstName) AS VARCHAR(50)) [FirstName] ,CAST(DECRYPTBYKEY(MidName) AS VARCHAR(50)) MidName" +
                           $",CAST(DECRYPTBYKEY(LastName) AS VARCHAR(50)) [LastName],CAST(DECRYPTBYKEY(NickName) AS VARCHAR(50)) [NickName]  ,[Sex] ,[Active] ,[DeleteFlag] ,[CreateDate] " +
                           $",[CreatedBy], [AuditData], [DateOfBirth], [DobPrecision], FacilityId, RegistrationDate FROM Person WHERE Id = @personId;");
                sql.Append("exec [dbo].[pr_CloseDecryptedSession];");

                var firstNameParameter = new SqlParameter();
                firstNameParameter.SqlDbType = SqlDbType.VarChar;
                firstNameParameter.ParameterName = "@firstName";
                firstNameParameter.Size = -1;
                firstNameParameter.Value = firstName;


                var middleNameParameter = new SqlParameter();
                middleNameParameter.SqlDbType = SqlDbType.VarChar;
                middleNameParameter.ParameterName = "@middleName";
                middleNameParameter.Size = -1;
                middleNameParameter.Value = middleName;


                var lastNameParameter = new SqlParameter();
                lastNameParameter.SqlDbType = SqlDbType.VarChar;
                lastNameParameter.ParameterName = "@lastName";
                lastNameParameter.Size = -1;
                lastNameParameter.Value = lastName;


                var nickNameParameter = new SqlParameter();
                nickNameParameter.SqlDbType = SqlDbType.VarChar;
                nickNameParameter.ParameterName = "@nickName";
                nickNameParameter.Size = -1;
                nickNameParameter.Value = NickName;

                var sexParameter = new SqlParameter("@sex", sex);
                var dateOfBirthParameter = new SqlParameter("@dateOfBirth", dateOfBirth);
                //var registrationDateParameter = new SqlParameter("@registrationDate", registrationDate);

                var registrationDateParameter = new SqlParameter();
                registrationDateParameter.ParameterName = "@registrationDate";
                registrationDateParameter.IsNullable = true;
                registrationDateParameter.SqlDbType = SqlDbType.DateTime;
                registrationDateParameter.Value = !registrationDate.HasValue ? (object)DBNull.Value : registrationDate.Value;

                var facilityIdParameter = new SqlParameter("@facilityId", facilityId);
                var personIdParameter = new SqlParameter("@personId", personId);
                var dobPrecisionParameter = new SqlParameter("@dobPrecision", dobPrecision);


                var personInsert = await _unitOfWork.Repository<Person>().FromSql(sql.ToString(), parameters:new []
                {
                    firstNameParameter,
                    middleNameParameter,
                    lastNameParameter,
                    nickNameParameter,
                    sexParameter,
                    dateOfBirthParameter,
                    registrationDateParameter,
                    facilityIdParameter,
                    personIdParameter,
                    dobPrecisionParameter
                });
                var patient = await GetPatientByPersonId(personInsert.FirstOrDefault().Id);
                if (patient != null)
                {
                    var mstFacility = await _unitOfWork.Repository<Facility>().Get(x => x.FacilityID == facilityId)
                        .ToListAsync();
                    if (mstFacility.Count == 0)
                    {
                        mstFacility = await _unitOfWork.Repository<Facility>().Get(x => x.DeleteFlag == 0)
                            .ToListAsync();
                    }
                    
                    await UpdatePatient(patient.Id, dateOfBirth, mstFacility[0].PosID);

                    if (patient.Ptn_pk > 0)
                    {
                        var gender = 0;
                        var lookupGender = await _unitOfWork.Repository<LookupItemView>().Get(x => x.ItemId == sex).ToListAsync();
                        if (lookupGender.Count > 0)
                        {
                            if (lookupGender[0].ItemName == "Male")
                            {
                                gender = 16;
                            }
                            else if (lookupGender[0].ItemName == "Female")
                            {
                                gender = 17;
                            }

                            StringBuilder stringBuilder = new StringBuilder();
                            stringBuilder.Append("exec pr_OpenDecryptedSession;");
                            stringBuilder.Append($"UPDATE mst_Patient SET " +
                                                 $"FirstName = ENCRYPTBYKEY(KEY_GUID('Key_CTC'), @firstName)," +
                                                 $"MiddleName = ENCRYPTBYKEY(KEY_GUID('Key_CTC'), @middleName)," +
                                                 $"LastName = ENCRYPTBYKEY(KEY_GUID('Key_CTC'), @lastName)," +
                                                 $"Sex = @gender," +
                                                 $"DOB = @dateOfBirth " +
                                                 $"where Ptn_Pk = @ptn_pk;");
                            stringBuilder.Append("exec [dbo].[pr_CloseDecryptedSession];");
                            stringBuilder.Append($"SELECT Ptn_Pk, " +
                                                 $"CAST(DECRYPTBYKEY(FirstName) AS VARCHAR(50)) FirstName, " +
                                                 $"CAST(DECRYPTBYKEY(LastName) AS VARCHAR(50)) LastName, LocationID FROM mst_Patient WHERE Ptn_Pk = @ptn_pk;");

                            var ptnpkParameter = new SqlParameter("@ptn_pk", patient.Ptn_pk);
                            var dobParameter = new SqlParameter("@dateOfBirth", dateOfBirth);
                            var genderParameter = new SqlParameter("@gender", gender);

                            await _unitOfWork.Repository<MstPatient>().FromSql(stringBuilder.ToString(), parameters:new []
                            {
                                firstNameParameter,
                                middleNameParameter,
                                lastNameParameter,
                                genderParameter,
                                ptnpkParameter,
                                dobParameter
                            });
                        }
                    }
                }

                return personInsert.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Person> RegisterPerson(string firstName, string middleName, string lastName, int sex, int createdBy, int facilityId, DateTime? dateOfBirth, DateTime? registrationDate = null, string nickName="", bool dobPrecison = true)
        {
            try
            {
                firstName = string.IsNullOrWhiteSpace(firstName) ? "" : firstName.Replace("'", "''");
                middleName = string.IsNullOrWhiteSpace(middleName) ? "" : middleName.Replace("'", "''");
                lastName = string.IsNullOrWhiteSpace(lastName) ? "" : lastName.Replace("'", "''");
                nickName = string.IsNullOrWhiteSpace(nickName) ? "" : nickName.Replace("'", "''");
                string dob = dateOfBirth.HasValue ? dateOfBirth.Value.ToString("yyyy-MM-dd") : null;
                string regDate = registrationDate.HasValue ? registrationDate.Value.ToString("yyyy-MM-dd") : null;

                StringBuilder sql = new StringBuilder();
                sql.Append("exec pr_OpenDecryptedSession;");
                if (!string.IsNullOrEmpty(dob))
                {
                    sql.Append("Insert Into Person(FirstName, MidName, LastName,NickName, " +
                               "Sex, DateOfBirth, DobPrecision, Active, DeleteFlag, CreateDate, " +
                               "CreatedBy, RegistrationDate, FacilityId)" +
                               $"Values(ENCRYPTBYKEY(KEY_GUID('Key_CTC'), @firstName), ENCRYPTBYKEY(KEY_GUID('Key_CTC'), @middleName)," +
                               $"ENCRYPTBYKEY(KEY_GUID('Key_CTC'), @lastName),ENCRYPTBYKEY(KEY_GUID('Key_CTC'), @nickName) , @sex, @dob, @dobPrecison," +
                               $"1,0,GETDATE(), @createdBy, @regDate, @facilityId);");
                }
                else
                {
                    sql.Append("Insert Into Person(FirstName, MidName, LastName,NickName, " +
                               "Sex,DateOfBirth, DobPrecision, Active, DeleteFlag, CreateDate, " +
                               "CreatedBy, RegistrationDate, FacilityId)" +
                               $"Values(ENCRYPTBYKEY(KEY_GUID('Key_CTC'), @firstName), ENCRYPTBYKEY(KEY_GUID('Key_CTC'), @middleName)," +
                               $"ENCRYPTBYKEY(KEY_GUID('Key_CTC'), @lastName),ENCRYPTBYKEY(KEY_GUID('Key_CTC'), @nickName) , @sex," +
                               $"@dob, @dobPrecison,1,0,GETDATE(), @createdBy, @regDate, @facilityId);");
                }
                
                sql.Append("SELECT [Id] , CAST(DECRYPTBYKEY(FirstName) AS VARCHAR(50)) [FirstName] ,CAST(DECRYPTBYKEY(MidName) AS VARCHAR(50)) MidName" +
                           ",CAST(DECRYPTBYKEY(LastName) AS VARCHAR(50)) [LastName], CAST(DECRYPTBYKEY(NickName) AS VARCHAR(50)) [NickName],[Sex] ,[Active] ,[DeleteFlag] ,[CreateDate] " +
                           ",[CreatedBy] ,[AuditData] ,[DateOfBirth] ,[DobPrecision], RegistrationDate, FacilityId FROM [dbo].[Person] WHERE Id = SCOPE_IDENTITY();" +
                           "exec [dbo].[pr_CloseDecryptedSession];");

                var firstNameParameter = new SqlParameter();
                firstNameParameter.SqlDbType = SqlDbType.VarChar;
                firstNameParameter.ParameterName = "@firstName";
                firstNameParameter.Size = -1;
                firstNameParameter.Value = firstName;

                var midNameParameter = new SqlParameter();
                midNameParameter.SqlDbType = SqlDbType.VarChar;
                midNameParameter.ParameterName = "@middleName";
                midNameParameter.Size = -1;
                midNameParameter.Value = middleName;

                var lastNameParameter = new SqlParameter();
                lastNameParameter.SqlDbType = SqlDbType.VarChar;
                lastNameParameter.ParameterName = "@lastName";
                lastNameParameter.Size = -1;
                lastNameParameter.Value = lastName;

                var nickNameParameter = new SqlParameter();
                nickNameParameter.SqlDbType = SqlDbType.VarChar;
                nickNameParameter.ParameterName = "@nickName";
                nickNameParameter.Size = -1;
                nickNameParameter.Value = nickName;

                var sexParameter = new SqlParameter("@sex", sex);
                var dobParameter = new SqlParameter();
                dobParameter.ParameterName = "@dob";
                dobParameter.IsNullable = true;
                dobParameter.SqlDbType = SqlDbType.DateTime;
                dobParameter.Value = string.IsNullOrWhiteSpace(dob) ? (object)DBNull.Value : dateOfBirth.Value;

                var dobPrecisionParameter = new SqlParameter("@dobPrecison", dobPrecison);
                var createdByParameter = new SqlParameter("@createdBy", createdBy);
                var regDateParameter = new SqlParameter();
                regDateParameter.ParameterName = "@regDate";
                regDateParameter.IsNullable = true;
                regDateParameter.SqlDbType = SqlDbType.DateTime;
                regDateParameter.Value = string.IsNullOrWhiteSpace(regDate) ? (object)DBNull.Value : registrationDate.Value;

                var facilityIdParameter = new SqlParameter("@facilityId", facilityId);

                var personInsert = await _unitOfWork.Repository<Person>().FromSql(sql.ToString(), parameters:new []
                {
                    firstNameParameter,
                    midNameParameter,
                    lastNameParameter,
                    nickNameParameter,
                    sexParameter,
                    dobParameter,
                    dobPrecisionParameter,
                    createdByParameter,
                    regDateParameter,
                    facilityIdParameter
                });
                return personInsert.FirstOrDefault();
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw e;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IQCare.Common.BusinessProcess.Commands.Enrollment;
using IQCare.Common.BusinessProcess.Commands.PersonCommand;
using IQCare.Common.Core.Models;
using IQCare.Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Parsing.Structure.IntermediateModel;
using Serilog;

namespace IQCare.Common.BusinessProcess.Services
{
    public class RegisterPersonService
    {
        private readonly ICommonUnitOfWork _unitOfWork;
        public RegisterPersonService(ICommonUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
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

        public async Task<PersonContact> GetPersonContactByPersonId(int personId)
        {
            try
            {
                var personContactList = await _unitOfWork.Repository<PersonContact>().Get(x => x.PersonId == personId && x.DeleteFlag == 0).OrderByDescending(x => x.Id).Take(1).FirstOrDefaultAsync();
                return personContactList;
            }
            catch(Exception e)
            {
                throw e;
            }
            }
        public async Task<PersonEducation> GetCurrentPersonEducation(int personId)
        {
            try
            {
                PersonEducation pe = await _unitOfWork.Repository<PersonEducation>().Get(x => x.PersonId == personId && x.DeleteFlag == false).OrderByDescending(x => x.Id).Take(1).FirstOrDefaultAsync();
                return pe;
            }
            catch(Exception e)
            {
                throw e;

            }
        }

        public async Task<PersonEducation> AddPersonEducation(PersonEducation pm)
        {
            try
            {
                await _unitOfWork.Repository<PersonEducation>().AddAsync(pm);
                await _unitOfWork.SaveAsync();
                return pm;
            }
            catch (Exception e)
            {
                throw e;
            }
        

        }

        public async Task<PersonEducation> UpdatePersonEducation(PersonEducation pm)
        {

            try
            {

                _unitOfWork.Repository<PersonEducation>().Update(pm);
                await _unitOfWork.SaveAsync();
                return pm;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<List<Identifier>> GetPersonIdentifierType (string codeName)
        {
            try
            {
                var Identifiers = await _unitOfWork.Repository<Identifier>().Get(x => x.Code == codeName && !x.DeleteFlag).ToListAsync();
                return Identifiers;
            }
            catch(Exception e)
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

        public async Task<PersonOccupation>  AddPersonOccupation(int userId,int occupation,int personId)
        {
            try
            {
                PersonOccupation pm = new PersonOccupation()
                {
                    PersonId = personId,
                    CreatedBy = userId,
                    Occupation = occupation,
                    CreateDate = DateTime.Now,
                    DeleteFlag = false,

                };
                await _unitOfWork.Repository<PersonOccupation>().AddAsync(pm);
                await _unitOfWork.SaveAsync();
                return pm;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<PersonOccupation> UpdateOccupation(PersonOccupation pm)
        {
            try { 
           
            _unitOfWork.Repository<PersonOccupation>().Update(pm);
            await _unitOfWork.SaveAsync();
            return pm;
            }
            catch(Exception e)
            {
                throw e;
            }
}
        public async Task<PersonOccupation> GetCurrentOccupation(int personId)
        {
            try
            {
                var occupation = await _unitOfWork.Repository<PersonOccupation>().Get(x => x.PersonId == personId & !x.DeleteFlag).OrderByDescending(o => o.CreateDate).FirstOrDefaultAsync();
                return occupation;
            }
            catch(Exception e)
            {
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
        public async Task<PersonContact> UpdatePersonContact(int personId, string mobileNumber,string alternativenumber,int id)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("exec pr_OpenDecryptedSession;");
                sql.Append($"UPDATE PersonContact SET  MobileNumber = ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '{mobileNumber}') ,AlternativeNumber=ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '{alternativenumber}') " +
                    $"WHERE PersonId = {personId} and Id={id};");
                sql.Append("exec [dbo].[pr_CloseDecryptedSession];");

                var personContactInsert = await _unitOfWork.Repository<PersonContact>().FromSql(sql.ToString());

                return personContactInsert.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public async Task<PersonContact> UpdatePersonContact(int personId, string physicalAddress, string mobileNumber)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("exec pr_OpenDecryptedSession;");
                sql.Append($"UPDATE PersonContact SET PhysicalAddress = ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '{physicalAddress}'), MobileNumber = ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '{mobileNumber}') WHERE PersonId = {personId};");
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
                    .Get(x => x.PersonId == personId).ToListAsync();
                foreach (var population in personPopulations)
                {
                    population.DeleteFlag = true;
                    _unitOfWork.Repository<PersonPopulation>().Update(population);
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

                    if (keyPop.ItemName == "Not Applicable")
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

        public async Task<PersonLocation> UpdatePersonLocation(int personId, string landmark)
        {
            try
            {
                var location = await _unitOfWork.Repository<PersonLocation>().Get(x => x.PersonId == personId)
                    .FirstOrDefaultAsync();

                location.LandMark = landmark;

                _unitOfWork.Repository<PersonLocation>().Update(location);
                await _unitOfWork.SaveAsync();

                return location;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> AddPersonLocation(int personId, int county, int subcounty, int ward, string village, string location, string sublocation, string landmark, string nearesthealthcentre, int userId)
        {
            PersonLocation personLocation = new PersonLocation()
            {
                PersonId = personId,
                County = county,
                SubCounty = subcounty,
                Ward = ward,
                Village = village,
                Location = location,
                SubLocation = sublocation,
                LandMark = landmark,
                NearestHealthCentre = nearesthealthcentre,
                Active = false,
                DeleteFlag = false,
                CreateDate = DateTime.Now,
                CreatedBy = userId
            };
            await _unitOfWork.Repository<PersonLocation>().AddAsync(personLocation);
           int res  =await _unitOfWork.SaveChangesAsync();

            return res;

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
        public  async Task<PersonMaritalStatus> GetFirstPatientMaritalStatus(int personId)
        {
            try
            {
                var maritalstatus = await _unitOfWork.Repository<PersonMaritalStatus>().Get(x => x.PersonId == personId & !x.DeleteFlag).OrderByDescending(o => o.CreateDate).FirstOrDefaultAsync();
                return maritalstatus;
            }
            catch(Exception e)
            {
                throw e;
            }

                
        }


        public async Task<PersonMaritalStatus> UpdateMaritalStatus(PersonMaritalStatus pms)
        {
            try
            {
                _unitOfWork.Repository<PersonMaritalStatus>().Update(pms);
                await _unitOfWork.SaveAsync();
                var maritalStatus = await _unitOfWork.Repository<PersonMaritalStatus>().Get(x => x.Id == pms.Id).FirstOrDefaultAsync();
                return maritalStatus;
            }
            catch(Exception e)
            {
                throw e;
            }
            }
        public async Task<PersonMaritalStatus> UpdateMaritalStatus(int personId, int maritalStatusId)
        {
            try
            {
                var maritalStatus = await _unitOfWork.Repository<PersonMaritalStatus>().Get(x => x.PersonId == personId)
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


        public async Task<PersonIdentifier> UpdatePersonIdentifier(PersonIdentifier pmi)
        {
            try {
                _unitOfWork.Repository<PersonIdentifier>().Update(pmi);
                await _unitOfWork.SaveAsync();
                return pmi;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<PersonIdentifier> GetCurrentPersonIdentifier(int identifierId,int PersonId)
        {
            try {

                var identifier= await _unitOfWork.Repository<PersonIdentifier>().Get(x=>x.IdentifierId == identifierId && x.PersonId==PersonId && !x.DeleteFlag).OrderByDescending(x => x.CreateDate).FirstOrDefaultAsync();
                return identifier;

            }
            catch(Exception e)
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


        public async Task<PatientIdentifier> EnrollPatient(string enrollmentNo, int patientId, int serviceAreaId, int createdBy, DateTime dateOfEnrollment)
        {
            using (var trans = _unitOfWork.Context.Database.BeginTransaction())
            {
                try
                {
                    var previouslyIdentifiers = await _unitOfWork.Repository<PatientIdentifier>().Get(y =>
                            y.IdentifierValue == enrollmentNo && y.IdentifierTypeId == 8)
                        .ToListAsync();

                    if (previouslyIdentifiers.Count > 0)
                    {
                        var exception = new Exception("No: " + enrollmentNo + " already exists");
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

                    GetPatientDetails patientDetails = new GetPatientDetails(_unitOfWork);
                    LookupLogic lookupLogic = new LookupLogic(_unitOfWork);

                    var patientLookup = await patientDetails.GetPatientByPatientId(patientId);

                    if (patientLookup.Count > 0)
                    {
                        Facility facility = await _unitOfWork.Repository<Facility>().Get(x => x.DeleteFlag == 0).FirstOrDefaultAsync();
                        var referralId = await lookupLogic.GetDecodeIdByName("VCT", 17);
                        var maritalStatusId = await lookupLogic.GetDecodeIdByName(patientLookup[0].MaritalStatusName, 17);
                        var address = patientLookup[0].PhysicalAddress == null ? " " : patientLookup[0].PhysicalAddress;
                        var phone = patientLookup[0].MobileNumber == null ? " " : patientLookup[0].MobileNumber;
                        var dobPrecision = patientLookup[0].DobPrecision ? 1 : 0;

                        var gender = 0;
                        if (patientLookup[0].Gender == "Male")
                        {
                            gender = 16;
                        }
                        else if (patientLookup[0].Gender == "Female")
                        {
                            gender = 17;
                        }

                        StringBuilder sql = new StringBuilder();
                        sql.Append("exec pr_OpenDecryptedSession;");
                        sql.Append("Insert Into mst_Patient(FirstName, LastName, MiddleName, LocationID, PatientEnrollmentID, ReferredFrom, RegistrationDate, Sex, DOB, DobPrecision, MaritalStatus, Address, Phone, UserID, PosId, Status, DeleteFlag, CreateDate,MovedToPatientTable)");
                        sql.Append("Values(");
                        sql.Append($"ENCRYPTBYKEY(KEY_GUID('Key_CTC'),'{patientLookup[0].FirstName}'),");
                        sql.Append($"ENCRYPTBYKEY(KEY_GUID('Key_CTC'),'{patientLookup[0].LastName}'),");
                        sql.Append($"ENCRYPTBYKEY(KEY_GUID('Key_CTC'),'{patientLookup[0].MidName}'),");
                        sql.Append($"'{facility.FacilityID}',");
                        sql.Append("' ',");
                        sql.Append($"'{referralId}',");
                        sql.Append($"'{dateOfEnrollment.ToString("yyyy-MM-dd")}',");
                        sql.Append($"'{gender}',");
                        sql.Append($"'{patientLookup[0].DateOfBirth.ToString("yyyy-MM-dd")}',");
                        sql.Append($"'{dobPrecision}',");
                        sql.Append($"'{maritalStatusId}',");
                        sql.Append($"ENCRYPTBYKEY(KEY_GUID('Key_CTC'),'{address}'),");
                        sql.Append($"ENCRYPTBYKEY(KEY_GUID('Key_CTC'),'{phone}'),");
                        sql.Append($"'{createdBy}',");
                        sql.Append($"'{facility.PosID}',");
                        sql.Append("0,");
                        sql.Append("0,");
                        sql.Append($"'{dateOfEnrollment.ToString("yyyy-MM-dd")}',");
                        sql.Append("1");
                        sql.Append(");");

                        sql.Append("SELECT Ptn_Pk, CAST(DECRYPTBYKEY([FirstName]) AS VARCHAR(50)) AS FirstName, CAST(DECRYPTBYKEY([LastName]) AS VARCHAR(50)) AS LastName, LocationID FROM [dbo].[mst_Patient] WHERE [Ptn_Pk] = SCOPE_IDENTITY();");
                        sql.Append("exec [dbo].[pr_CloseDecryptedSession];");

                        var result = await _unitOfWork.Repository<MstPatient>().FromSql(sql.ToString());

                        StringBuilder sqlBuilder = new StringBuilder();
                        sqlBuilder.Append("Insert Into Lnk_PatientProgramStart(Ptn_pk, ModuleId, StartDate, UserID, CreateDate)");
                        sqlBuilder.Append("Values(");
                        sqlBuilder.Append($"'{result[0].Ptn_Pk}',");
                        sqlBuilder.Append("283,");
                        sqlBuilder.Append($"'{dateOfEnrollment.ToString("yyyy-MM-dd")}',");
                        sqlBuilder.Append($"'{createdBy}',");
                        sqlBuilder.Append($"'{dateOfEnrollment.ToString("yyyy-MM-dd")}'");
                        sqlBuilder.Append(");");

                        var insertResult = await _unitOfWork.Context.Database.ExecuteSqlCommandAsync(sqlBuilder.ToString());

                        StringBuilder sqlPatient = new StringBuilder();
                        sqlPatient.Append($"UPDATE Patient SET ptn_pk = '{result[0].Ptn_Pk}' WHERE Id = '{patientId}';");
                        var updateResult = await _unitOfWork.Context.Database.ExecuteSqlCommandAsync(sqlPatient.ToString());
                    }

                    trans.Commit();

                    return patientIdentifier;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }

        public async Task<Patient> UpdatePatient(int patientId, DateTime dateOfBirth, string facilityId)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("exec pr_OpenDecryptedSession;");
                sql.Append($"UPDATE Patient SET DateOfBirth = '{dateOfBirth.ToString("yyyy-MM-dd")}', FacilityId = '{facilityId}' WHERE Id = {patientId};");
                sql.Append($"SELECT [Id],[ptn_pk],[PersonId],[PatientIndex],[PatientType],[FacilityId],[Active],[DateOfBirth]," +
                           $"[DobPrecision],CAST(DECRYPTBYKEY(NationalId) AS VARCHAR(50)) [NationalId],[DeleteFlag],[CreatedBy]," +
                           $"[CreateDate],[AuditData],[RegistrationDate] FROM Patient WHERE Id = '{patientId}';");
                sql.Append("exec [dbo].[pr_CloseDecryptedSession];");


                var patientUpdate = await _unitOfWork.Repository<Patient>().FromSql(sql.ToString());
                return patientUpdate.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Patient> AddPatient(int personID, DateTime dateOfBirth, int userId, string facilityId = "")
        {
            try
            {
                var facility = await _unitOfWork.Repository<Facility>().Get(x => x.DeleteFlag == 0).FirstOrDefaultAsync();
                var patientType = await _unitOfWork.Repository<LookupItemView>()
                    .Get(x => x.MasterName == "PatientType" && x.ItemName == "New").FirstOrDefaultAsync();

                if (string.IsNullOrWhiteSpace(facilityId))
                {
                    facilityId = facility.PosID;
                }

                var sqlPatient = "exec pr_OpenDecryptedSession;" +
                                 "Insert Into  Patient(ptn_pk,PersonId,PatientIndex,PatientType,FacilityId,Active,DateOfBirth,NationalId,DeleteFlag,CreatedBy,CreateDate,AuditData,DobPrecision)" +
                                 $"Values(0, {personID}, {DateTime.Now.Year + '-' + personID}, '{patientType.ItemId}', '{facilityId}', 1," +
                                 $"'{dateOfBirth.ToString("yyyy-MM-dd")}', ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '99999999'), 0, '{userId}', GETDATE()," +
                                 $"NULL, 1);" +
                                 $"SELECT [Id],[ptn_pk],[PersonId],[PatientIndex],[PatientType],[FacilityId],[Active],[DateOfBirth]," +
                                 $"[DobPrecision],CAST(DECRYPTBYKEY(NationalId) AS VARCHAR(50)) [NationalId],[DeleteFlag],[CreatedBy]," +
                                 $"[CreateDate],[AuditData],[RegistrationDate] FROM [dbo].[Patient] WHERE Id = SCOPE_IDENTITY();" +
                                 $"exec [dbo].[pr_CloseDecryptedSession];";

                var patientInsert = await _unitOfWork.Repository<Patient>().FromSql(sqlPatient);

                return patientInsert.FirstOrDefault();
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
                           $",CAST(DECRYPTBYKEY(LastName) AS VARCHAR(50)) [LastName] ,[Sex] ,[Active] ,[DeleteFlag] ,[CreateDate] " +
                           $",[CreatedBy] ,[AuditData] ,[DateOfBirth] ,[DobPrecision] FROM [dbo].[Person] WHERE Id = '{personId}';");
                sql.Append("exec [dbo].[pr_CloseDecryptedSession];");

                var person = await _unitOfWork.Repository<Person>().FromSql(sql.ToString());

                return person.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public async Task<Person> UpdatePerson(int personId, string firstName, string middleName, string lastName, int sex,int CreatedBy, DateTime dateOfBirth,int DobPrecision)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("exec pr_OpenDecryptedSession;");
                sql.Append($"UPDATE Person SET FirstName = ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '{firstName}'), " +
                           $"MidName = ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '{middleName}'), " +
                           $"LastName = ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '{lastName}'), " +
                           $"Sex = {sex}" +
                           $"CreatedBy={CreatedBy}" +
                           $"DobPrecision={DobPrecision}, DateOfBirth = '{dateOfBirth.ToString("yyyy-MM-dd")}' WHERE Id = {personId}; ");
                sql.Append($"SELECT [Id] , CAST(DECRYPTBYKEY(FirstName) AS VARCHAR(50)) [FirstName] ,CAST(DECRYPTBYKEY(MidName) AS VARCHAR(50)) MidName" +
                           $",CAST(DECRYPTBYKEY(LastName) AS VARCHAR(50)) [LastName] ,[Sex] ,[Active] ,[DeleteFlag] ,[CreateDate] " +
                           $",[CreatedBy] ,[AuditData] ,[DateOfBirth] ,[DobPrecision] FROM Person WHERE Id = '{personId}';");
                sql.Append("exec [dbo].[pr_CloseDecryptedSession];");

                var personInsert = await _unitOfWork.Repository<Person>().FromSql(sql.ToString());
                return personInsert.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
       
        public async Task<Person> UpdatePerson(int personId, string firstName, string middleName, string lastName, int sex, DateTime dateOfBirth)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("exec pr_OpenDecryptedSession;");
                sql.Append($"UPDATE Person SET FirstName = ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '{firstName}'), " +
                           $"MidName = ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '{middleName}'), " +
                           $"LastName = ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '{lastName}'), " +
                           $"Sex = {sex}, DateOfBirth = '{dateOfBirth.ToString("yyyy-MM-dd")}' WHERE Id = {personId}; ");
                sql.Append($"SELECT [Id] , CAST(DECRYPTBYKEY(FirstName) AS VARCHAR(50)) [FirstName] ,CAST(DECRYPTBYKEY(MidName) AS VARCHAR(50)) MidName" +
                           $",CAST(DECRYPTBYKEY(LastName) AS VARCHAR(50)) [LastName] ,[Sex] ,[Active] ,[DeleteFlag] ,[CreateDate] " +
                           $",[CreatedBy] ,[AuditData] ,[DateOfBirth] ,[DobPrecision] FROM Person WHERE Id = '{personId}';");
                sql.Append("exec [dbo].[pr_CloseDecryptedSession];");

                var personInsert = await _unitOfWork.Repository<Person>().FromSql(sql.ToString());
                return personInsert.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> UpdatePerson(Person person ,int id)
        {
            try
            {
                SqlParameter FirstNameParameter = new SqlParameter("@FirstName", SqlDbType.VarChar);
                FirstNameParameter.Value = person.FirstName;
                SqlParameter MidNameParameter = new SqlParameter("@MidName", SqlDbType.VarChar);
                MidNameParameter.Value = person.MidName;
                SqlParameter LastNameParameter = new SqlParameter("@LastName", SqlDbType.VarChar);
                LastNameParameter.Value = person.LastName;
                SqlParameter SexParameter = new SqlParameter("@Sex", SqlDbType.Int);
                SexParameter.Value = person.Sex;
                SqlParameter DateofBirthParameter=new SqlParameter("@DateOfBirth", SqlDbType.DateTime); new SqlParameter("@DateOfBirth", SqlDbType.DateTime);
                if (person.DateOfBirth != null)
                {
                     
                    DateofBirthParameter.Value = person.DateOfBirth;
                }
                SqlParameter idParameter = new SqlParameter("@Id", SqlDbType.Int);
                idParameter.Value = id;



                await _unitOfWork.Repository<Person>().FromSql("exec Person_Update @FirstName,@MidName,@LastName ,@Sex,@DateOfBirth,@Id",FirstNameParameter,MidNameParameter,LastNameParameter,SexParameter,
                   DateofBirthParameter, idParameter);
                int res = await _unitOfWork.SaveChangesAsync();
                return res;
            }
            catch(Exception e)
            {
                Log.Error(e.Message);
                throw e;
            }
        }
         public async Task<Person> RegisterPerson(string firstName, string middleName, string lastName, int sex ,DateTime dateOfBirth, int createdBy,int DobPrecision)
        {
            try
            {
                var sql =
                    "exec pr_OpenDecryptedSession;" +
                    "Insert Into Person(FirstName, MidName,LastName,Sex,DateOfBirth,DobPrecision,Active,DeleteFlag,CreateDate,CreatedBy)" +
                    $"Values(ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '{firstName}'), ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '{middleName}')," +
                    $"ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '{lastName}'), {sex}, '{dateOfBirth.ToString("yyyy-MM-dd")}',{DobPrecision}," +
                    $"1,0,GETDATE(), '{createdBy}');" +
                    "SELECT [Id] , CAST(DECRYPTBYKEY(FirstName) AS VARCHAR(50)) [FirstName] ,CAST(DECRYPTBYKEY(MidName) AS VARCHAR(50)) MidName" +
                    ",CAST(DECRYPTBYKEY(LastName) AS VARCHAR(50)) [LastName] ,[Sex] ,[Active] ,[DeleteFlag] ,[CreateDate] " +
                    ",[CreatedBy] ,[AuditData] ,[DateOfBirth] ,[DobPrecision] FROM [dbo].[Person] WHERE Id = SCOPE_IDENTITY();" +
                    "exec [dbo].[pr_CloseDecryptedSession];";

                var personInsert = await _unitOfWork.Repository<Person>().FromSql(sql.ToString());

                return personInsert.FirstOrDefault();
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw e;
            }
        }
        public async Task<Person> InsertPerson(string firstName,string middleName,string lastName,int sex,int createdBy)
        {
            try
            {
                var sql =
                    "exec pr_OpenDecryptedSession;" +
                    "Insert Into Person(FirstName, MidName,LastName,Sex,Active,DeleteFlag,CreateDate,CreatedBy)" +
                    $"Values(ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '{firstName}'), ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '{middleName}')," +
                    $"ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '{lastName}'), {sex}," +
                    $"1,0,GETDATE(), '{createdBy}');" +
                    "SELECT [Id] , CAST(DECRYPTBYKEY(FirstName) AS VARCHAR(50)) [FirstName] ,CAST(DECRYPTBYKEY(MidName) AS VARCHAR(50)) MidName" +
                    ",CAST(DECRYPTBYKEY(LastName) AS VARCHAR(50)) [LastName] ,[Sex] ,[Active] ,[DeleteFlag] ,[CreateDate] " +
                    ",[CreatedBy] ,[AuditData] ,[DateOfBirth] ,[DobPrecision] FROM [dbo].[Person] WHERE Id = SCOPE_IDENTITY();" +
                    "exec [dbo].[pr_CloseDecryptedSession];";

                var personInsert = await _unitOfWork.Repository<Person>().FromSql(sql);

                return personInsert.FirstOrDefault();
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw e;
            }
        }
       
        public async Task<Person> RegisterPerson(string firstName, string middleName, string lastName, int sex, DateTime dateOfBirth, int createdBy)
        {
            try
            {
                var sql =
                    "exec pr_OpenDecryptedSession;" +
                    "Insert Into Person(FirstName, MidName,LastName,Sex,DateOfBirth,DobPrecision,Active,DeleteFlag,CreateDate,CreatedBy)" +
                    $"Values(ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '{firstName}'), ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '{middleName}')," +
                    $"ENCRYPTBYKEY(KEY_GUID('Key_CTC'), '{lastName}'), {sex}, '{dateOfBirth.ToString("yyyy-MM-dd")}', 1," +
                    $"1,0,GETDATE(), '{createdBy}');" +
                    "SELECT [Id] , CAST(DECRYPTBYKEY(FirstName) AS VARCHAR(50)) [FirstName] ,CAST(DECRYPTBYKEY(MidName) AS VARCHAR(50)) MidName" +
                    ",CAST(DECRYPTBYKEY(LastName) AS VARCHAR(50)) [LastName] ,[Sex] ,[Active] ,[DeleteFlag] ,[CreateDate] " +
                    ",[CreatedBy] ,[AuditData] ,[DateOfBirth] ,[DobPrecision] FROM [dbo].[Person] WHERE Id = SCOPE_IDENTITY();" +
                    "exec [dbo].[pr_CloseDecryptedSession];";

                var personInsert = await _unitOfWork.Repository<Person>().FromSql(sql);

                return personInsert.FirstOrDefault();
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw e;
            }
        }

        public async Task<List<PersonEmergencyContact>> GetListEmergencyContacts(int PersonId)
        {

            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("exec pr_OpenDecryptedSession;");
                sql.Append($"Select[Id, PersonId, EmeregencyContactPersonId, CAST(DECRYPTBYKEY(MobileContact) as Varchar(100)) as MobileContact, DeleteFlag, CreatedBy, CreateDate, AuditData" +
                           $" FROM [dbo].[PersonEmergencyContact] WHERE DeleteFlag='0' and PersonId = '{PersonId}';");
                sql.Append("exec [dbo].[pr_CloseDecryptedSession];");
                var pec = await _unitOfWork.Repository<PersonEmergencyContact>().FromSql(sql.ToString());
                return pec;
            }
            catch(Exception e)
            {
                Log.Error(e.Message);
                throw e;

            }


        }


        public async Task<PersonEmergencyContact> GetSpecificEmergencyContacts(int EmergencyContactPersonId,int PersonId)
        {

            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("exec pr_OpenDecryptedSession;");
                sql.Append($"Select[Id, PersonId, EmergencyContactPersonId, CAST(DECRYPTBYKEY(MobileContact) as Varchar(100)) as MobileContact, DeleteFlag, CreatedBy, CreateDate, AuditData" +
                           $" FROM [dbo].[PersonEmergencyContact] WHERE DeleteFlag='0' and EmergencyContactPersonId='{EmergencyContactPersonId}' and  PersonId = '{PersonId}';");
                sql.Append("exec [dbo].[pr_CloseDecryptedSession];");
                var pec = await _unitOfWork.Repository<PersonEmergencyContact>().FromSql(sql.ToString());
                return pec.FirstOrDefault();
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw e;

            }


        }
        public async Task<int> UpdatePersonEmergencyContact(PersonEmergencyContact pmc)
        {
            try
            {
                SqlParameter personIdParameter = new SqlParameter("personIdParameter", SqlDbType.Int);
                personIdParameter.Value = pmc.PersonId;
                SqlParameter EmergencyContactPersonIdParameter = new SqlParameter("EmergencyContactPersonIdParameter", SqlDbType.Int);
                EmergencyContactPersonIdParameter.Value = pmc.EmergencyContactPersonId;
                SqlParameter MobileContactParameter = new SqlParameter("MobileContactParameter", SqlDbType.VarBinary);
                MobileContactParameter.Value = Encoding.ASCII.GetBytes(pmc.MobileContact);
                SqlParameter userId = new SqlParameter("UserId", SqlDbType.Int);
                userId.Value = pmc.CreatedBy;
                SqlParameter DeleteFlag = new SqlParameter("DeleteFlag", SqlDbType.Bit);
                if (pmc.DeleteFlag == true)
                {
                    DeleteFlag.Value = 1;
                }
                else if (pmc.DeleteFlag == false)
                {
                    DeleteFlag.Value = 0;
                }

                await  _unitOfWork.Repository<PersonEmergencyContact>().FromSql("exec PersonEmergencyContact_Update @personIdParameter,@EmergencyContactPersonIdParameter,@MobileContactParameter,@UserId,@DeleteFlag",
                    personIdParameter,EmergencyContactPersonIdParameter,MobileContactParameter,userId,DeleteFlag
                    );
               int res= await _unitOfWork.SaveChangesAsync();

                return res;
                    
            }
            catch(Exception e)
            {
                Log.Error(e.Message);
                throw e;

            }
        }

        public async Task<PersonEmergencyContact> GetCurrentLatestEmergencyContact(int personId)
        {
            try {

                var pme = await _unitOfWork.Repository<PersonEmergencyContact>().Get(x => x.PersonId == personId & x.DeleteFlag == false).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                return pme;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw e;
                   
            }
        }

        public async Task<PersonLocation> GetCurrentPersonLocation(int personId)
        {
            try
            {
                var pme = await _unitOfWork.Repository<PersonLocation>().Get(x => x.PersonId == personId & x.DeleteFlag == false).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                return pme;
            }
            catch(Exception e)
            {
                Log.Error(e.Message);
                throw e;
                   
            }
        }

        public async Task<int> UpdatePersonLocation(PersonLocation location)
        {
            try
            {
                
               _unitOfWork.Repository<PersonLocation>().Update(location);
                int res = await _unitOfWork.SaveChangesAsync();

                return res;
                
            }
            catch(Exception e)
            {
                Log.Error(e.Message);
                throw e;
            }
        }
        public async Task<int> UpdatePersonContact(PersonContact p)
        {
            try
            {
                SqlParameter personIdParameter = new SqlParameter("personIdParameter", SqlDbType.Int);
                personIdParameter.Value = p.PersonId;

                SqlParameter physicalAdressParameter = new SqlParameter("physicalAddressParameter", SqlDbType.VarBinary);
                physicalAdressParameter.Value = Encoding.ASCII.GetBytes(p.PhysicalAddress);

                SqlParameter mobileNumberParameter = new SqlParameter("mobileNumberParameter", SqlDbType.VarBinary);
                mobileNumberParameter.Value = Encoding.ASCII.GetBytes(p.MobileNumber);

                SqlParameter alternativeNumberParameter = new SqlParameter("alternativeNumberParameter",
                    SqlDbType.VarBinary);
                alternativeNumberParameter.Value = Encoding.ASCII.GetBytes(p.AlternativeNumber);

                SqlParameter emailAddressParameter = new SqlParameter("emailAddressParameter", SqlDbType.VarBinary);
                emailAddressParameter.Value = Encoding.ASCII.GetBytes(p.EmailAddress);

                SqlParameter id = new SqlParameter("Id", SqlDbType.Int);
                id.Value = p.Id;

                await _unitOfWork.Repository<PersonContact>().FromSql("exec PersonContact_Update @personIdParameter, @physicalAddressParameter, @mobileNumberParameter, @alternativeNumberParameter, @emailAddressParameter, @Id",
                    personIdParameter, physicalAdressParameter, mobileNumberParameter, alternativeNumberParameter,
                    emailAddressParameter, id);
                int res = await _unitOfWork.SaveChangesAsync();
                return res;

            }
            catch(Exception e)
            { 
            Log.Error(e.Message);
            throw e;

            }
        }

        public async Task<PersonRelation>   GetCurrentRelationShip(int PersonId,int RelatedToPersonId)
        {
            try
            {
                var pme = await _unitOfWork.Repository<PersonRelation>().Get(x => x.PersonId == RelatedToPersonId && x.IndexPersonId == PersonId && !x.DeleteFlag).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                return pme;
            }
            catch(Exception e)
            {
                Log.Error(e.Message);
                throw e;
            }
            
        }

        public async Task<PersonRelation> AddPersonRelationship(int PersonId,int RelatedToPersonId,int RelationshipType,int Createdby)
        {
            try
            {
                PersonRelation pl = new PersonRelation();
                pl.PersonId = RelatedToPersonId;
                pl.RelationshipTypeId = RelationshipType;
                pl.IndexPersonId = PersonId;
                pl.CreateDate = DateTime.Now;
                pl.DeleteFlag = false;
                pl.CreatedBy = Createdby;


                await _unitOfWork.Repository<PersonRelation>().AddAsync(pl);
                await _unitOfWork.SaveAsync();

                return pl;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw e;

            }

        }

        public async Task<PersonConsent> GetCurrentPersonConsent (int EmergencyContactId,int PersonId)
        {
            try
            {
                var consent = await _unitOfWork.Repository<PersonConsent>().Get(x => x.EmergencyContactId == EmergencyContactId && x.PersonId == PersonId && !x.DeleteFlag).OrderByDescending(x=>x.CreateDate).FirstOrDefaultAsync();
                return consent;
            }
            catch(Exception e)
            {
                Log.Error(e.Message);
                throw e;
            }
        }
        public async Task<PersonConsent> AddPersonConsent(PersonConsent pmc)
        {
            try
            {
                var sql = "Insert Into PersonConsent(PersonId, EmergencyContactId,ConsentType,ConsentDate,ConsentValue,ConsentReason,Active,DeleteFlag,CreateDate,CreatedBy)" +
                    $"Values( '{pmc.PersonId}', '{pmc.EmergencyContactId}'," +
                    $"'{pmc.ConsentType}', '{pmc.ConsentDate}', '{pmc.ConsentValue}', '{pmc.ConsentReason}," +
                    $"1,0,GETDATE(), '{pmc.CreatedBy}');" +
                    "SELECT PersonId, EmergencyContactId,ConsentType,ConsentDate" +
                    ",ConsentValue,ConsentReason,Active,DeleteFlag,CreateDate,CreatedBy " +
                    "FROM [dbo].[PersonConsent] WHERE Id = SCOPE_IDENTITY();";
                    

                var personConsent = await _unitOfWork.Repository<PersonConsent>().FromSql(sql);

                return personConsent.FirstOrDefault();
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw e;
            }

        }

        public async Task<int> UpdatePersonConsent(PersonConsent pmc)
        {
            try
            {
                _unitOfWork.Repository<PersonConsent>().Update(pmc);
                int res=await _unitOfWork.SaveChangesAsync();
                return res;
            }
            catch(Exception e)
            {
                Log.Error(e.Message);
                throw e;
            }
        }

       public async Task<PersonEmergencyContact> AddPersonEmergencyContact(PersonEmergencyContact pmc)
        {
            try
            {
                SqlParameter personIdParameter = new SqlParameter("personIdParameter", SqlDbType.Int);
                personIdParameter.Value = pmc.PersonId;
                SqlParameter EmergencyContactPersonIdParameter = new SqlParameter("EmergencyContactPersonIdParameter", SqlDbType.Int);
                EmergencyContactPersonIdParameter.Value = pmc.EmergencyContactPersonId;
                SqlParameter MobileContactParameter = new SqlParameter("MobileContactParameter", SqlDbType.VarBinary);
                MobileContactParameter.Value = Encoding.ASCII.GetBytes(pmc.MobileContact);
                SqlParameter userId = new SqlParameter("UserId", SqlDbType.Int);
                userId.Value = pmc.CreatedBy;
                
                  await _unitOfWork.Repository<PersonEmergencyContact>().ExecWithStoreProcedureAsync("PersonEmergencyContact_Insert @personIdParameter,@EmergencyContactPersonIdParameter,@MobileContactParameter,@UserId",
                     personIdParameter,EmergencyContactPersonIdParameter,MobileContactParameter,userId);
                 await _unitOfWork.SaveChangesAsync();
                return pmc;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw e;

            }
        }
    }
}
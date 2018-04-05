using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.SqlClient;
using System.Reflection;
using DataAccess.Context;
using DataAccess.WebApi.Interface;
using Entities.CCC.psmart;
using Entity.WebApi.PSmart;
using IQCare.DTO.PSmart;

namespace DataAccess.WebApi.Repository
{
    public class MstPatientRepository : BaseRepository<MstPatient>, IMstPatientReposiroty
    {
        public PsmartContext _context;
        public MstPatientRepository():this(new PsmartContext())
        {
        }

        public MstPatientRepository(PsmartContext context) : base(context)
        {
            _context = context;
        }

        public void InsertNewClients(string firstName, string middleName, string lastName, DateTime registrationDate, string dob, string dobPrecision, string phone, string gender, string landmark, string maritalStatus, string htsId, int moduleId, string cardSerial, string village, string ward, string subcounty, string heiId, string Address)
        {
            try
            {
                //var firstName = shr.PATIENT_IDENTIFICATION.PATIENT_NAME.FIRST_NAME.ToString();
                //var middleName = shr.PATIENT_IDENTIFICATION.PATIENT_NAME.MIDDLE_NAME.ToString();
                //var lastName = shr.PATIENT_IDENTIFICATION.PATIENT_NAME.LAST_NAME.ToString();
                //var registraionDate = DateTime.Now;
                //DateTime doB = Convert.ToDateTime(shr.PATIENT_IDENTIFICATION.DATE_OF_BIRTH);
                //string dobPrecision = (shr.PATIENT_IDENTIFICATION.DATE_OF_BIRTH_PRECISION == "ESTIMATED")? "1":"0";
                //string gender = shr.PATIENT_IDENTIFICATION.SEX;
                //string phone = shr.PATIENT_IDENTIFICATION.PHONE_NUMBER;
                //string landmark = shr.PATIENT_IDENTIFICATION.PATIENT_ADDRESS.PHYSICAL_ADDRESS.NEAREST_LANDMARK;
                //string htsId = "";
                //string maritalStatus = shr.PATIENT_IDENTIFICATION.MARITAL_STATUS;
                //foreach (var Item in shr.PATIENT_IDENTIFICATION.INTERNAL_PATIENT_ID)
                //{
                //    if (Item.IDENTIFIER_TYPE == "HTS_NUMBER")
                //    {
                //        htsId = Item.ID;
                //    }
                //}
             

                //int result = _context.Database.ExecuteSqlCommand(@"INSERT INTO
                //mst_Patient(
                //    Status, FirstName, MiddleName, LastName,
                //    LocationID, RegistrationDate, Sex, DOB, DobPrecision,
                //    CountryId, PosId, SatelliteId, UserID, CreateDate,
                //    Phone, Landmark, HTSID,MaritalStatus)
                //VALUES(
                //    '0', encryptbykey(key_guid('Key_CTC'), {firstname}), encryptbykey(key_guid('Key_CTC'), '{middleName}'), encryptbykey(key_guid('Key_CTC'), '{lastName}'),
                //    '{facilityId}', '{registraionDate:yyyy MMMM dd}', '{ (SELECT Id FROM mst_Decode WHERE Name='gender')}', '{doB:yyyy MMMM dd}', '{dobPrecision}',
                //    '{(SELECT CountryID FROM mst_facility WHERE deleteFlag=0)}', '{(SELECT PosID FROM mst_facility WHERE deleteFlag=0)}', '{0}', '{1}', GETDATE(),
                //    encryptbykey(key_guid('Key_CTC'), '{Phone}'), '{landmark}', '{htsId}', '{(SELECT Id FROM mst_decode WHERE Id=maritalStatus)}'); SELECT SCOPE_IDENTITY(); ");

                _context.Database.SqlQuery<EntityType>(
                    "EXEC Psmart_ProcessNewClientRegistration @param1,@param2,@param3,@param4,@param5,@param6,@param7,@param8,@param9,@param10,@param11,@param12,@param13,@param14,@param15,@param16,@param17,@param18,@param19",
                    new SqlParameter("param1", firstName),
                    new SqlParameter("param2", middleName),
                    new SqlParameter("param3", lastName),
                    new SqlParameter("param4", registrationDate),
                    new SqlParameter("param5", dob),
                    new SqlParameter("param6", dobPrecision),
                    new SqlParameter("param7", phone),
                    new SqlParameter("param8", gender),
                    new SqlParameter("param9", landmark),
                    new SqlParameter("param10", maritalStatus),
                    new SqlParameter("param11", htsId),
                    new SqlParameter("param12", cardSerial),
                    new SqlParameter("param13", 0),
                    new SqlParameter("param14", moduleId),
                    new SqlParameter("param15", village),
                    new SqlParameter("param16", ward),
                    new SqlParameter("param17", subcounty),
                    new SqlParameter("param18", heiId),
                    new SqlParameter("param19", Address)
                );

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        public int UpdateSerialNUmber(psmartCard psmartCard)
        {
          int result=  _context.Database.ExecuteSqlCommand($"UPDATE mst_Patient SET CardSerialNumber='{psmartCard.CARD_SERIAL_NO}' WHERE Ptn_Pk= {psmartCard.PATIENTID}");
            return result;
        }
    }
}
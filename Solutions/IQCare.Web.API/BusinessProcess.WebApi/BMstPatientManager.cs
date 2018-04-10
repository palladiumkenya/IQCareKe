using System;
using System.Data;
using System.Linq;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using DataAccess.WebApi;
using DataAccess.WebApi.Repository;
using Entities.CCC.PSmart;
using Entity.WebApi.PSmart;
using Interface.WebApi;


namespace BusinessProcess.WebApi
{
    public class BMstPatientManager:ProcessBase,IMstPatientManager
    {
        private int _result = 0;
        public int AddMstPatient(string firstName, string middleName, string lastName, DateTime registrationDate, string dob, string dobPrecision, string phone, string gender, string landmark, string maritalStatus, string htsId, string moduleId, string cardSerial, string village, string ward, string subcounty, string heiId, string Address)
        {
            try
            {
                //using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                //{
                //    unitOfWork.MstPatientReposiroty.Add(mstPatient);
                //    _result = unitOfWork.Complete();
                //    unitOfWork.Dispose();
                //    return _result;
                //}
                string _Address = $" Village = {village} ; Ward = {ward} ; Subcounty= {subcounty}; Address {Address}";
                int Id = -1;
                ClsObject obj = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddExtendedParameters("@FirstName", SqlDbType.VarChar, firstName);
                ClsUtility.AddExtendedParameters("@MiddleName", SqlDbType.VarChar, middleName);
                ClsUtility.AddExtendedParameters("@LastName", SqlDbType.VarChar, lastName);

               // ClsUtility.AddExtendedParameters("@facilityId", SqlDbType.VarChar, registrationDate);
                ClsUtility.AddExtendedParameters("@registrationDate", SqlDbType.VarChar, registrationDate);
                ClsUtility.AddExtendedParameters("@dob", SqlDbType.VarChar, dob);
                ClsUtility.AddExtendedParameters("@dobPrecision", SqlDbType.VarChar, dobPrecision);
                ClsUtility.AddExtendedParameters("@phone", SqlDbType.VarChar, phone);
                ClsUtility.AddExtendedParameters("@gender", SqlDbType.VarChar, gender=="F"? "Female" : "Male");
                ClsUtility.AddExtendedParameters("@landmark", SqlDbType.VarChar, landmark);
                ClsUtility.AddExtendedParameters("@maritalStatus", SqlDbType.VarChar, maritalStatus);
                ClsUtility.AddExtendedParameters("@htsId", SqlDbType.VarChar, htsId);
                ClsUtility.AddExtendedParameters("@moduleId", SqlDbType.VarChar, moduleId);
                ClsUtility.AddExtendedParameters("@serialNumber", SqlDbType.VarChar, cardSerial);
             // ClsUtility.AddExtendedParameters("@village", SqlDbType.VarChar, village);
               // ClsUtility.AddExtendedParameters("@ward", SqlDbType.VarChar, ward);
               // ClsUtility.AddExtendedParameters("@subcounty", SqlDbType.VarChar, subcounty);
                ClsUtility.AddExtendedParameters("@heiNumber", SqlDbType.VarChar, heiId);
                ClsUtility.AddExtendedParameters("@Address", SqlDbType.VarChar,_Address );

                DataTable dt = (DataTable)obj.ReturnObject(ClsUtility.theParams, "Psmart_ProcessNewClientRegistration", ClsUtility.ObjectEnum.DataTable);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                }
                // p.Add(person);
                obj = null;
                return Id;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public int ProcessMotherNames(string firstName, string middleName, string lastName, string cccNumber, int ptnpk)
        {
            try
            {
                int Id = -1;
                ClsObject obj = new ClsObject();
                ClsUtility.Init_Hashtable();
                ClsUtility.AddExtendedParameters("@firstName", SqlDbType.VarChar, firstName);
                ClsUtility.AddExtendedParameters("@middleName", SqlDbType.VarChar, middleName);
                ClsUtility.AddExtendedParameters("@lastName", SqlDbType.VarChar, lastName);
                ClsUtility.AddExtendedParameters("@cccNumber", SqlDbType.VarChar, cccNumber);
                ClsUtility.AddExtendedParameters("@ptnpk", SqlDbType.VarChar, ptnpk);

                DataTable dt = (DataTable)obj.ReturnObject(ClsUtility.theParams, "Psmart_ProcessMotherDetails", ClsUtility.ObjectEnum.DataTable);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                }
                // p.Add(person);
                obj = null;
                return Id;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public int EditMstPatient(MstPatient mstPatient)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    unitOfWork.MstPatientReposiroty.Update(mstPatient);
                    _result = unitOfWork.Complete();
                    unitOfWork.Dispose();
                    return mstPatient.Ptn_pk;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void InsertNewClient(string firstName, string middleName, string lastName, DateTime registrationDate, string dob, string dobPrecision, string phone, string gender, string landmark, string maritalStatus, string htsId, int moduleId, string cardSerial, string village, string ward, string subcounty, string heiId, string Address)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                   unitOfWork.MstPatientReposiroty.InsertNewClients( firstName,  middleName,  lastName,  registrationDate,  dob,  dobPrecision,  phone,  gender,  landmark,  maritalStatus,  htsId,moduleId,cardSerial,village,ward,subcounty,heiId,Address);
                   
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public int UpdateCardSerial(psmartCard psmartCard)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
            {
                return unitOfWork.MstPatientReposiroty.UpdateSerialNUmber(psmartCard);
            }
        }

        public int InsertPatientCardSerialNumber(psmartCard psmartCard)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new PsmartContext()))
                {
                    var mstPatient = unitOfWork.MstPatientReposiroty.FindBy(x => x.Ptn_pk == psmartCard.PATIENTID).Select(x=> new MstPatient()
                        {
                            CardSerialNumber = psmartCard.CARD_SERIAL_NO
                        })
                        .FirstOrDefault();
                    if (mstPatient != null)
                    {
                        mstPatient.CardSerialNumber = psmartCard.CARD_SERIAL_NO;
                    }
                    unitOfWork.MstPatientReposiroty.Update(mstPatient);
                    _result = unitOfWork.Complete();
                    unitOfWork.Dispose();
                    return _result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}
using System;
using System.Xml.XPath;
using Application.Presentation;
using Entities.CCC.psmart;
using Entity.WebApi.PSmart;
using Interface.WebApi;
using IQCare.DTO.PSmart;

namespace IQCare.WebApi.Logic.PSmart
{
    public class MstPatientManager
    {
        private readonly IMstPatientManager _mstPatientManager = (IMstPatientManager)ObjectFactory.CreateInstance("BusinessProcess.WebApi.BMstPatientManager, BusinessProcess.WebApi");
        private int result = 0;

        public int AddMstPatient(string firstName, string middleName, string lastName, DateTime registrationDate, string dob, string dobPrecision, string phone, string gender, string landmark, string maritalStatus, string htsId, string moduleId, string cardSerial, string village, string ward, string subcounty, string heiId, string Address)
        {
            return _mstPatientManager.AddMstPatient(firstName, middleName, lastName, registrationDate, dob,
                dobPrecision, phone, gender, landmark, maritalStatus, htsId, moduleId, cardSerial,
                village, ward, subcounty, heiId, Address);
        }

        public int EditMstPatient(MstPatient mstPatient)
        {
            return _mstPatientManager.EditMstPatient(mstPatient);
        }

        public int UpdatePatientCardSerial(psmartCard psmartCard)
        {
            return _mstPatientManager.UpdateCardSerial(psmartCard);
        }

        public void InsertNewClient(string firstName, string middleName, string lastName, DateTime registrationDate, string dob, string dobPrecision, string phone, string gender, string landmark, string maritalStatus, string htsId, int moduleId, string cardSerial, string village, string ward, string subcounty, string heiId, string Address)
        {
            MstPatient patient = new MstPatient()
            {
                //LocationID = shr.PA ,
                //GODS_NUMBER = ,
                //CardSerialNumber = ,
                //HTSID = ,
                //FirstName = ,
                //MIddleName = ,
                //LastName = ,
                //DOB=,
                //DobPrecision = ,
            };
             _mstPatientManager.InsertNewClient( firstName,  middleName,  lastName,  registrationDate,  dob,  dobPrecision,  phone,  gender,  landmark,  maritalStatus, htsId,moduleId,cardSerial,village,ward,subcounty,heiId,Address);
        }

        public int InsertPatientCardSerialNumber(psmartCard psmartCard)
        {
            result= _mstPatientManager.InsertPatientCardSerialNumber(psmartCard);
            return result;
            //if (result > 0)
            //{
            //    return "Card serial for " + psmartCard.PATIENTID + "updated successfully";
            //}else { return "Card serial for " + psmartCard.PATIENTID + "NOT updated"; }
        }


       public int ProcessMotherNames(string firstName, string middleName, string lastName, string cccNumber, int ptnpk)
        {
            return _mstPatientManager.ProcessMotherNames(firstName, middleName, lastName, cccNumber, ptnpk);
        }

    }
}
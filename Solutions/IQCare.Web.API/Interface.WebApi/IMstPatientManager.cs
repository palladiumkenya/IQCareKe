using Entities.CCC.PSmart;
using Entity.WebApi.PSmart;
using System;

namespace Interface.WebApi
{
    public interface IMstPatientManager
    {
        int AddMstPatient(string firstName, string middleName, string lastName, DateTime registrationDate, string dob, string dobPrecision, string phone, string gender, string landmark, string maritalStatus, string htsId, string moduleId, string cardSerial, string village, string ward, string subcounty, string heiId, string Address);
        int ProcessMotherNames(string firstName, string middleName, string lastName, string cccNumber, int ptnpk);
        int EditMstPatient(MstPatient mstPatient);
        int InsertPatientCardSerialNumber(psmartCard psmartCard);
        int UpdateCardSerial(psmartCard psmartCard);
        void InsertNewClient(string firstName, string middleName, string lastName, DateTime registrationDate, string dob, string dobPrecision, string phone, string gender, string landmark, string maritalStatus, string htsId, int moduleId, string cardSerial, string village, string ward, string subcounty, string heiId, string Address);
    }
}
using System;
using DataAccess.Context;
using Entities.CCC.PSmart;
using Entity.WebApi.PSmart;


namespace DataAccess.WebApi.Interface
{
    public interface IMstPatientReposiroty :IRepository<MstPatient>
    {
        int UpdateSerialNUmber(psmartCard psmartCard);
        void InsertNewClients(string firstName, string middleName, string lastName, DateTime registrationDate, string dob, string dobPrecision, string phone, string gender, string landmark, string maritalStatus, string htsId, int moduleId, string cardSerial, string village, string ward, string subcounty, string heiId, string Address,string _card_issuing_facility);
    }
}
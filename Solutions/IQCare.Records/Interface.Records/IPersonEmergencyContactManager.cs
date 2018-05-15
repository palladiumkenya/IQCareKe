using System.Collections.Generic;
using Entities.Records;

namespace Interface.Records
{
    public interface IPersonEmergencyContactManager
    {
        int AddPersonEmergencyContact(PersonEmergencyContact pmc);
        int DeletePersonEmergencyContact(int id);
        List<PersonEmergencyContact> GetAllEmergencyContact(int personId);

        List<PersonEmergencyContact> GetCurrentEmergencyContact(int personId);
        int UpdatePersonEmergencyContact(PersonEmergencyContact pmc);
        PersonEmergencyContact GetSpecificEmergencyContact(int id, int personId);
    }
}
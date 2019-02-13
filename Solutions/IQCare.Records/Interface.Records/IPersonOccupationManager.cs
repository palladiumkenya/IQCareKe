using System.Collections.Generic;
using Entities.Records.Enrollment;

namespace Interface.Records
{
    public interface IPersonOccupationManager
    {
        int AddPersonOccupation(PersonOccupation pated);
        int DeletePersonOccupation(int id);
        List<PersonOccupation> GetAllPersonOccupation(int personId);
        int UpdatePersonOccupation(PersonOccupation pe);

        PersonOccupation GetCurrentOccupation(int personId);
    }
}
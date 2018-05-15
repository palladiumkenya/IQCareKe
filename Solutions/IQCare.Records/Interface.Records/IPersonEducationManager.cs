using System.Collections.Generic;
using Entities.Records.Enrollment;

namespace Interface.Records
{
    public interface IPersonEducationManager
    {
        int AddPersonEducationLevel(PersonEducation pated);
        int DeletePersonEducationLevel(int id);
        List<PersonEducation> GetAllPersonEducationLevel(int personId);
       PersonEducation GetCurrentPersonEducation(int personId);
        int UpdatePersonEducation(PersonEducation pe);
    }
}
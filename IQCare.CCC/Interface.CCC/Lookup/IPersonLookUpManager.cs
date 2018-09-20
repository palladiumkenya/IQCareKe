using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Entities.CCC.Lookup;

namespace Interface.CCC.Lookup
{
    public interface IPersonLookUpManager
    {
        PersonLookUp GetPersonById(int id);
        List<PersonLookUp> GetPatientSearchresults(string firstName, string middleName, string lastName, string dob, int sex);
    }
}

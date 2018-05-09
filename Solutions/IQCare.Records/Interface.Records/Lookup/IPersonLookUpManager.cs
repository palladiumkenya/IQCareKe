using Entities.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Records.Lookup
{
   
        public interface IPersonLookUpManager
        {
            PersonLookUp GetPersonById(int id);
            List<PersonLookUp> GetPatientSearchresults(string firstName, string middleName, string lastName, string dob);
        }
    
}

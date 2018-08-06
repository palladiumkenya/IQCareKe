using Application.Common;
using Application.Presentation;
using Entities.Records;
using Interface.Records.Lookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.Records.UILogic
{
    public class PersonLookUpManager
    {
        readonly IPersonLookUpManager _personLookUpManager = (IPersonLookUpManager)ObjectFactory.CreateInstance("BusinessProcess.Records.BPersonLookUpManager, BusinessProcess.Records");
        Utility _utility = new Utility();

        public PersonLookUp GetPersonById(int id)
        {
            return _personLookUpManager.GetPersonById(id);
        }

        public List<PersonLookUp> GetPersonSearchResults(string firstName, string middleName, string lastName, string dob)
        {
            return _personLookUpManager.GetPatientSearchresults(firstName, middleName, lastName, dob);
        }
    }
}

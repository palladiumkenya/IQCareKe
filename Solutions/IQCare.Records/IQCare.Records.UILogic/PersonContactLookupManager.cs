using Application.Common;
using Application.Presentation;
using Entities.Records;
using Interface.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.Records.UILogic
{
    public class PersonContactLookupManager
    {
        readonly IPersonContactLookUpManager _personContactLookUpManager = (IPersonContactLookUpManager)ObjectFactory.CreateInstance("BusinessProcess.Records.BPersonContactLookUpManager, BusinessProcess.Records");
        Utility _utility = new Utility();

        public List<PersonContactLookUp> GetPersonContactByPersonId(int personId)
        {
            return _personContactLookUpManager.GetPersonContactByPersonId(personId);
        }
    }
}

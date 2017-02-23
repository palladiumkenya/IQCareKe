using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Presentation;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;

namespace IQCare.CCC.UILogic
{
    public class PersonContactLookUpManager
    {
        readonly IPersonContactLookUpManager _personContactLookUpManager = (IPersonContactLookUpManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Lookup.BPersonContactLookUpManager, BusinessProcess.CCC");
        Utility _utility = new Utility();

        public List<PersonContactLookUp> GetPersonContactByPersonId(int personId)
        {
            return _personContactLookUpManager.GetPersonContactByPersonId(personId);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface.CCC.Lookup;
using Application.Common;
using Application.Presentation;
using Entities.CCC.Lookup;

namespace IQCare.CCC.UILogic
{
    public class PersonLookUpManager
    {
        readonly IPersonLookUpManager _personLookUpManager = (IPersonLookUpManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPersonLookUpManager, BusinessProcess.CCC");
        Utility _utility = new Utility();

        public PersonLookUp GetPersonById(int id)
        {
            return _personLookUpManager.GetPersonById(id);
        }
    }
}

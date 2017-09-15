using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Presentation;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;

namespace IQCare.CCC.UILogic
{
    public class PersonGreenCardLookupManager
    {
        readonly IPersonGreenCardLookupManager _personLookUpManager = (IPersonGreenCardLookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Lookup.BPersonGreenCardLookupManager, BusinessProcess.CCC");

        public List<PersonGreenCardLookup> GetPtnPkByPersonId(int personId)
        {
            try
            {
                return _personLookUpManager.GetPtnPkByPersonId(personId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}

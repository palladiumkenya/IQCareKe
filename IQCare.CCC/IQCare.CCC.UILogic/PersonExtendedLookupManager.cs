using System;
using Application.Presentation;
using Entities.CCC.Lookup;
using Interface.CCC;
using Interface.CCC.Lookup;

namespace IQCare.CCC.UILogic
{
    public class PersonExtendedLookupManager
    {
        private readonly IPersonExtendedLookupManager _personExtendedManager =
            (IPersonExtendedLookupManager)
            ObjectFactory.CreateInstance("BusinessProcess.CCC.Lookup.BPersonExtendedLookupManager, BusinessProcess.CCC");

        public PersonExtLookup GetPatientDetailsByPersonId(int personId)
        {
            try
            {
                return _personExtendedManager.GetPatientDetailsByPersonId(personId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
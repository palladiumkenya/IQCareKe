using Entities.CCC.Lookup;

namespace Interface.CCC.Lookup
{
    public interface IPersonExtendedLookupManager
    {
        PersonExtLookup GetPatientDetailsByPersonId(int personId);
    }
}
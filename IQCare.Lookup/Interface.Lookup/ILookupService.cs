using System.Collections.Generic;
using Entities.Lookup;
namespace Interface.Lookup
{
    public interface ILookupService
    {
        /// <summary>
        /// Gets the lookup items.
        /// </summary>
        /// <param name="prefix">The prefix.</param>
        /// <param name="lookupName">Name of the lookup.</param>
        /// <param name="lookupcategory">The lookupcategory.</param>
        /// <returns></returns>
        List<Item> GetLookupItems(string prefix, string lookupName, string lookupcategory);
        /// <summary>
        /// Gets the look up item.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="lookupName">Name of the lookup.</param>
        /// <param name="lookupcategory">The lookupcategory.</param>
        /// <returns></returns>
        Item GetLookUpItem(int id, string lookupName, string lookupcategory);
    }
}

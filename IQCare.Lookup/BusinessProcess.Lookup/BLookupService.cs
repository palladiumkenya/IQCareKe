using System.Collections.Generic;
using System.Linq;
using DataAccess.Base;
using DataAccess.Lookup;
using Entities.Lookup;
using Interface.Lookup;
namespace BusinessProcess.Lookup
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="DataAccess.Base.ProcessBase" />
    /// <seealso cref="IQCare.Web.WS.ILookupService" />
    public class BLookup : ProcessBase, ILookupService
    {
        /// <summary>
        /// Gets the lookup items.
        /// </summary>
        /// <param name="prefix">The prefix.</param>
        /// <param name="lookupName">Name of the lookup.</param>
        /// <param name="lookupcategory">The lookupcategory.</param>
        /// <returns></returns>
        public List<Item> GetLookupItems(string prefix, string lookupName, string lookupcategory)
        {
            LookupRepository repo = new LookupRepository();
            if (!string.IsNullOrEmpty(prefix))
            {
                return repo.GetFiltered(prefix, lookupName, lookupcategory);
            }
            else
            {
                return repo.GetAll(lookupName, lookupcategory).ToList();
            }
        }

        /// <summary>
        /// Gets the look up item.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="lookupName">Name of the lookup.</param>
        /// <param name="lookupcategory">The lookupcategory.</param>
        /// <returns></returns>
        public Item GetLookUpItem(int id, string lookupName, string lookupcategory)
        {
            LookupRepository repo = new LookupRepository();
            return repo.Find(id, lookupName, lookupcategory);
        }
    }
}

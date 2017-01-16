using System;
using System.Collections.Generic;
using System.Linq;
using Entities.Lookup;
using DataAccess.Context;
using System.Linq.Expressions;

namespace DataAccess.Lookup
{
    public class LookupRepository : BaseRepository<LookupItem>
    {

        private readonly BaseContext _lookupContext;

        public LookupRepository() : base()
        {
        }
        //public LookupRepository(): this(new LookupContext())
        //{
        //    _lookupContext = new LookupContext();
        //}
        public LookupRepository(BaseContext context) : base(context)
        {
            _lookupContext = context;
        }
        public override IQueryable<LookupItem> Filter(Expression<Func<LookupItem, bool>> filter)
        {
            return base.Filter(filter);
        }
        public override IEnumerable<LookupItem> GetAll()
        {
            return base.GetAll();
        }
        public override void Add(LookupItem entity)
        {
            base.Add(entity);
        }
        public IEnumerable<LookupItem> GetAll(string lookname, string lookcategory)
        {
            return Filter(lk => lk.LookupName == lookname && lk.Category == lookcategory);
        }
        public LookupItem Find(int id, string lookupname, string lookupcategory)
        {

            return this.Filter(lk => lk.Id == id
               && lk.LookupName.ToLower() == lookupname.ToLower()
               && lk.Category.ToLower() == lookupcategory.ToLower())
                .FirstOrDefault();
        }
        public List<LookupItem> GetFiltered(string itemname, string lookname, string lookcategory)
        {
            var items = Filter(lk => lk.LookupName == lookname && lk.Category == lookcategory && lk.Name.ToLower().Contains(itemname.ToLower()));
            return items.ToList();
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="IQCare.Web.WS.Repository{IQCare.Web.WS.Item}" />
 /*   public class LookupRepository : Repository<Item>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LookupRepository"/> class.
        /// </summary>
        public LookupRepository()
        {

        }
        /// <summary>
        /// Finds the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="lookupname">The lookupname.</param>
        /// <param name="lookupcategory">The lookupcategory.</param>
        /// <returns></returns>
        public override Item Find(int id, string lookupname, string lookupcategory)
        {

            return this.Filter(lk => lk.Id == id
               && lk.LookupName.ToLower() == lookupname.ToLower()
               && lk.Category.ToLower() == lookupcategory.ToLower())
                .FirstOrDefault();
        }

        public override IEnumerable<Item> GetAll(string lookcategory)
        {
            return Filter(lk => lk.LookupName.ToLower() == lookcategory.ToLower());
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="lookname"></param>
        /// <param name="lookcategory"></param>
        /// <returns></returns>
        public override IEnumerable<Item> GetAll(string lookname, string lookcategory)
        {
            return Filter(lk => lk.LookupName == lookname && lk.Category == lookcategory);
        }

        /// <summary>
        /// Gets the filtered.
        /// </summary>
        /// <param name="itemname">The itemname.</param>
        /// <param name="lookname">The lookname.</param>
        /// <param name="lookcategory">The lookcategory.</param>
        /// <returns></returns>
        public List<Item> GetFiltered(string itemname, string lookname, string lookcategory)
        {
            var items = Filter(lk => lk.LookupName == lookname && lk.Category == lookcategory && lk.Name.ToLower().Contains(itemname.ToLower()));
            // var items = this.GetAll(lookname, lookcategory);
            // items = items.Where(it => it.Name.ToLower().Contains(itemname.ToLower()));

            return items.ToList();
        }
    }*/
}

using System.Collections.Generic;
using System.Data.Entity;
using Common.Core.Interfaces;
using Common.Core.Model;

namespace Common.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        internal readonly BaseContext Context;
        internal IDbSet<T> Dbset;

        public BaseRepository(BaseContext context)
        {
            Context = context;
            Dbset = Context.Set<T>();
        }

        public virtual T GetById(int id)
        {
            return Dbset.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Dbset;
        }
    }
}

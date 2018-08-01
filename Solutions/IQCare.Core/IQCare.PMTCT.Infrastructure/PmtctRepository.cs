using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IQCare.PMTCT.Infrastructure.Interface;

namespace IQCare.PMTCT.Infrastructure
{
    public class PmtctRepository<TEntity> : IPmtctRepository<TEntity> where TEntity : class
    {
        public TEntity FindById(object id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> FindByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, List<Expression<Func<TEntity, object>>> includeProperties = null, int? page = null,
            int? pageSize = null)
        {
            throw new NotImplementedException();
        }

        public Task<int> ExecWithStoreProcedureAsync(string query, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> FromSql(string query, params object[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
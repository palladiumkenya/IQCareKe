using IQCare.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.HTS.Core
{
    public interface IHTSRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
    }
}

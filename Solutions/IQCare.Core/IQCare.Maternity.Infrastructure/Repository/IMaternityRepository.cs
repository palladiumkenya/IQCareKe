using IQCare.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.Infrastructure.Repository
{
    public interface IMaternityRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {

    }
}

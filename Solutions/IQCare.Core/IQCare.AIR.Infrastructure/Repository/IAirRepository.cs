using System;
using System.Collections.Generic;
using System.Text;
using IQCare.SharedKernel.Interfaces;

namespace IQCare.AIR.Infrastructure.Repository
{
    public interface IAirRepository<TEntity> : IRepository<TEntity> where TEntity : class 
    {

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using IQCare.SharedKernel.Infrastructure;
using IQCare.SharedKernel.Interfaces;

namespace IQCare.Pharm.Infrastructure.Repository
{
   public interface IPharmRepository<TEntity>:IRepository<TEntity> where TEntity:class
    {
    }
}

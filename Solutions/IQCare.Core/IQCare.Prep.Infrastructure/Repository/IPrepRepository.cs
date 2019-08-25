using System;
using System.Collections.Generic;
using System.Text;
using IQCare.SharedKernel.Infrastructure;
using IQCare.SharedKernel.Interfaces;

namespace IQCare.Prep.Infrastructure.Repository
{
   public  interface IPrepRepository<TEntity>: IRepository<TEntity> where TEntity : class
    {
    }
}

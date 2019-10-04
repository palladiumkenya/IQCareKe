using System;
using System.Collections.Generic;
using System.Text;
using IQCare.SharedKernel.Interfaces;
namespace IQCare.Queue.Infrastructure.Repository
{
   public  interface IQueueRepository<TEntity>:IRepository<TEntity> where TEntity:class
    {
    }
   
}


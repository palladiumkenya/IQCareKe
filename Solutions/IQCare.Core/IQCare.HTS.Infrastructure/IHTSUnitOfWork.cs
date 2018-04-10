using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IQCare.Common.Infrastructure;
using IQCare.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IQCare.HTS.Infrastructure
{
    public interface IHTSUnitOfWork : IDisposable
    {
        void Save();
        Task SaveAsync();
        DbContext Context { get; }
        IRepository<T> Repository<T>() where T : class;
    }
}

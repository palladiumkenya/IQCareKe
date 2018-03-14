using IQCare.Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IQCare.SharedKernel.Interfaces;

namespace IQCare.Registration.Infrastructure
{
    public interface IRegistrationUnitOfWork
    {
        void Save();
        Task SaveAsync();
        DbContext Context { get; }
        IRepository<T> Repository<T>() where T : class;
    }
}

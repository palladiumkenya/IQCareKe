using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Queue.Infrastructure.Repository;
using IQCare.Queue.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace IQCare.Queue.Infrastructure.Installers
{
    public static class QueueDatabaseContextInstaller
    {
        public static void AddQueueDbContext(this IServiceCollection services,string connectionString)
        {
            services.AddDbContext<QueueDbContext>(opt => opt.UseSqlServer(connectionString));
            services.AddScoped(typeof(IQueueRepository<>), typeof(QueueRepository<>));
            services.AddScoped(typeof(IQueueUnitOfWork), typeof(QueueUnitOfWork));
        }
    }
}

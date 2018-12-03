using System;
using System.Collections.Generic;
using System.Text;
using IQCare.Lab.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IQCare.Lab.Infrastructure
{
    public static class LabDbContextInstaller
    {
        public static void AddLabDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<LabDbContext>(x => x.UseSqlServer(connectionString));
            services.AddScoped(typeof(ILabRepository<>), typeof(LabRepository<>));
            services.AddScoped(typeof(ILabUnitOfWork), typeof(LabUnitOfWork));
        }
    }
}

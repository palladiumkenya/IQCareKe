using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using IQCare.Common.Core.Interfaces.Repositories;

namespace IQCare.Common.Infrastructure.Installers
{
   public static class CommonDatabaseContextInstaller
    {
        public static void AddCommonDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<CommonDbContext>(config => config.UseSqlServer(connectionString));
            services.AddScoped(typeof(ICommonRepository<>), typeof(CommonRepository<>));
            services.AddScoped(typeof(ICommonUnitOfWork),typeof(CommonUnitOfWork));
        }
    }
}

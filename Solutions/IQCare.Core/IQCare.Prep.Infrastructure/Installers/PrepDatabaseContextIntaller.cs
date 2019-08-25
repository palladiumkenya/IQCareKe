using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using IQCare.Prep.Infrastructure.Repository;
using IQCare.Prep.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;


namespace IQCare.Prep.Infrastructure.Installers
{
   public static class PrepDatabaseContextIntaller
    {
        public static void AddPrepDbContext(this IServiceCollection services,string connectionString)
        {
            services.AddDbContext<PrepDbContext>(x => x.UseSqlServer(connectionString));
            services.AddScoped(typeof(IPrepRepository<>), typeof(PrepRepository<>));
            services.AddScoped(typeof(IPrepUnitOfWork), typeof(PrepUnitOfWork));
        }
    }
}

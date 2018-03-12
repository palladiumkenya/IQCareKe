using IQCare.HTS.Core;
using IQCare.HTS.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQCare.Common.Infrastructure;

namespace IQCare
{
    public static class BuilderExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var dbConnectionString = configuration.GetConnectionString("IQCareConnection");
            services.AddDbContext<HtsDbContext>(b => b.UseSqlServer(dbConnectionString));
            services.AddScoped(typeof(IHTSRepository<>), typeof(HTSRepository<>));
            services.AddScoped<IHTSUnitOfWork>(c => new HTSUnitOfWork(c.GetRequiredService<HtsDbContext>()));


            //Common
            services.AddDbContext<CommonDbContext>(x => x.UseSqlServer(dbConnectionString));
            return services;
        }
    }
}

using System.Data.Common;
using IQCare.Common.Core.Interfaces.Repositories;
using IQCare.Common.Infrastructure;
using IQCare.HTS.Core;
using IQCare.HTS.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            return services;
        }

        public static IServiceCollection AddPmtctDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var dbConnectionString = configuration.GetConnectionString("IQCareConnection");
            //services.AddDbContext<PmtctDbContext>(b => b.UseSqlServer(dbConnectionString));
            //services.AddScoped(typeof(IPmtctRepository<>), typeof(PmtctRepository<>));
            //services.AddScoped<IPmtctUnitOfWork>(c => new PmtctUnitOfWork(c.GetRequiredService<PmtctDbContext>()));

            return services;
        }

        public static IServiceCollection AddCommonDatabase(this IServiceCollection services,
            IConfiguration configuration)
        {
            var dbConnectionString = configuration.GetConnectionString("IQCareConnection");
            services.AddDbContext<CommonDbContext>(b => b.UseSqlServer(dbConnectionString));
            services.AddScoped(typeof(ICommonRepository<>), typeof(CommonRepository<>));
            services.AddScoped<ICommonUnitOfWork>(c => new CommonUnitOfWork(c.GetRequiredService<CommonDbContext>()));

            return services;
        }
    }
}

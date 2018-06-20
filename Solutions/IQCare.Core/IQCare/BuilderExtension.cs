using System.Data.Common;
using IQCare.Common.Core.Interfaces.Repositories;
using IQCare.Common.Infrastructure;
using IQCare.Helpers;
using IQCare.HTS.Core;
using IQCare.HTS.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IQCare
{
    public static class BuilderExtensions
    {
        public static string ConnectionString { get; set; }
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration, IConnectionString connectionString)
        {
            //var dbConnectionString = configuration.GetConnectionString("IQCareConnection");
            var iqcareuri = configuration.GetSection("IQCareUri").Get<string>();
            var db = connectionString.GetConnectionString(iqcareuri);
            ConnectionString = db.Result;

            services.AddDbContext<HtsDbContext>(b => b.UseSqlServer(ConnectionString));
            services.AddScoped(typeof(IHTSRepository<>), typeof(HTSRepository<>));
            services.AddScoped<IHTSUnitOfWork>(c => new HTSUnitOfWork(c.GetRequiredService<HtsDbContext>()));

            return services;
        }

        public static IServiceCollection AddCommonDatabase(this IServiceCollection services, IConfiguration configuration, IConnectionString connectionString)
        {
            //var dbConnectionString = configuration.GetConnectionString("IQCareConnection");
            //var iqcareuri = configuration.GetSection("IQCareUri").Get<string>();
            //var db = connectionString.GetConnectionString(iqcareuri);
            //var dbConnectionString = db.Result;

            services.AddDbContext<CommonDbContext>(b => b.UseSqlServer(ConnectionString));
            services.AddScoped(typeof(ICommonRepository<>), typeof(CommonRepository<>));
            services.AddScoped<ICommonUnitOfWork>(c => new CommonUnitOfWork(c.GetRequiredService<CommonDbContext>()));

            return services;
        }
    }
}

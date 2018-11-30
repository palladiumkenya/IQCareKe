using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using IQCare.Common.Core.Interfaces.Repositories;
using IQCare.Common.Infrastructure;
using IQCare.Helpers;
using IQCare.HTS.Core;
using IQCare.HTS.Infrastructure;
using IQCare.PMTCT.Infrastructure;
using IQCare.PMTCT.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace IQCare
{
    public static class BuilderExtensions
    {
        public static string _connectionString { get; set; }
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration, IConnectionString connectionString)
        {
            //var dbConnectionString = configuration.GetConnectionString("IQCareConnection");
            var iqcareuri = configuration.GetSection("IQCareUri").Get<string>();
            var db = connectionString.GetConnectionString(iqcareuri);

            _connectionString = db.Result.Replace("\"","").Replace("Application Name=IQCare_EMR;","").Replace("Server", "Data Source").
                Replace("Type System Version=SQL Data Source 2005;","").Replace("Database", "Initial Catalog")
                .Replace("Integrated Security=false;", "").Replace("packet size=4128;Min Pool Size=3;Max Pool Size=200;","");

            _connectionString = _connectionString.Replace(@"\\", @"\");
            StringBuilder conn = new StringBuilder();
            conn.Append(_connectionString);
            conn.Append("MultipleActiveResultSets=True;");

            _connectionString = conn.ToString();
            Log.Debug(_connectionString);

            services.AddDbContext<HtsDbContext>(b => b.UseSqlServer(_connectionString));
            services.AddScoped(typeof(IHTSRepository<>), typeof(HTSRepository<>));
            services.AddScoped<IHTSUnitOfWork>(c => new HTSUnitOfWork(c.GetRequiredService<HtsDbContext>()));

            return services;
        }

        public static IServiceCollection AddPmtctDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PmtctDbContext>(b => b.UseSqlServer(_connectionString));
            services.AddScoped(typeof(IPmtctRepository<>), typeof(PmtctRepository<>));
            services.AddScoped<PMTCT.Infrastructure.IPmtctUnitOfWork>(c => new PmtctUnitOfWork(c.GetRequiredService<PmtctDbContext>()));

            return services;
        }

        public static IServiceCollection AddCommonDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CommonDbContext>(b => b.UseSqlServer(_connectionString));
            services.AddScoped(typeof(ICommonRepository<>), typeof(CommonRepository<>));
            services.AddScoped<Common.Infrastructure.ICommonUnitOfWork>(c => new CommonUnitOfWork(c.GetRequiredService<CommonDbContext>()));

            return services;
        }

        public static void AddCommonDatabaseFunc(this IServiceCollection services)
        {
            services.AddSingleton<Func<SqlConnection>>(() => new SqlConnection(_connectionString));
        }
    }
}

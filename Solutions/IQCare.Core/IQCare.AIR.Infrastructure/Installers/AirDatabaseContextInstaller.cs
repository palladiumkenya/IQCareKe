using System;
using System.Collections.Generic;
using System.Text;
using IQCare.AIR.Infrastructure.Repository;
using IQCare.AIR.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IQCare.AIR.Infrastructure.Installers
{
    public static class AirDatabaseContextInstaller 
    {
        public static void AddAirDbContext(this IServiceCollection services,string connectionString)
        {
            services.AddDbContext<AirDbContext>(opt => opt.UseSqlServer(connectionString, 
                options =>
                {
                    options.EnableRetryOnFailure(3);                    
                }));
            services.AddScoped(typeof(IAirRepository<>), typeof(AirRepository<>));
            services.AddScoped(typeof(IAirUnitOfWork), typeof(AirUnitOfWork));

        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using IQCare.Pharm.Infrastructure.Repository;
using IQCare.Pharm.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;


namespace IQCare.Pharm.Infrastructure.Installers
{
   public  static class PharmDatabaseContextInstaller
    {
        public static void AddPharmDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<PharmDbContext>(x => x.UseSqlServer(connectionString));
            services.AddScoped(typeof(IPharmRepository<>), typeof(PharmRepository<>));
            services.AddScoped(typeof(IPharmUnitOfWork), typeof(PharmUnitOfWork));
        }
    }
}

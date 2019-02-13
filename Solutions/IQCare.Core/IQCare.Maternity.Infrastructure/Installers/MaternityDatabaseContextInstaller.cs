using IQCare.Maternity.Infrastructure.Repository;
using IQCare.Maternity.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.Infrastructure.Installers
{
    public static class MaternityDatabaseContextInstaller
    {
        public static void AddMaternityDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<MaternityDbContext>(x => x.UseSqlServer(connectionString));
            services.AddScoped(typeof(IMaternityRepository<>), typeof(MaternityRepository<>));
            services.AddScoped(typeof(IMaternityUnitOfWork), typeof(MaternityUnitOfWork));
        }
    }
}

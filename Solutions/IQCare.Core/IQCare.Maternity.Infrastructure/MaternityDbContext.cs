using IQCare.SharedKernel.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.Maternity.Infrastructure
{
    public class MaternityDbContext : BaseContext
    {
        public MaternityDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyEntityTypeConfigsFromAssembly(Assembly.GetAssembly(typeof(MaternityDbContext)));
        }
    }
}

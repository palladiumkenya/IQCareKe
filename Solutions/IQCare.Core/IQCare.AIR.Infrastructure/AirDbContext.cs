using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using IQCare.SharedKernel.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace IQCare.AIR.Infrastructure
{
    public class AirDbContext : BaseContext
    {
        public AirDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyEntityTypeConfigsFromAssembly(Assembly.GetAssembly(typeof(AirDbContext)));
        }
    }
}

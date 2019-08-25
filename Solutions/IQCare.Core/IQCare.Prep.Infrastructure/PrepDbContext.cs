using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using IQCare.SharedKernel.Infrastructure;
namespace IQCare.Prep.Infrastructure
{
   public  class PrepDbContext: BaseContext
    {
        public PrepDbContext(DbContextOptions options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyEntityTypeConfigsFromAssembly(Assembly.GetAssembly(typeof(PrepDbContext)));
        }
    }
}

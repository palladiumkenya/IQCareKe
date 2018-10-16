using System.Reflection;
using IQCare.SharedKernel.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace IQCare.Lab.Infrastructure
{
    public class LabDbContext : BaseContext
    {
        public LabDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyEntityTypeConfigsFromAssembly(Assembly.GetAssembly(typeof(LabDbContext)));
        }
    }
}
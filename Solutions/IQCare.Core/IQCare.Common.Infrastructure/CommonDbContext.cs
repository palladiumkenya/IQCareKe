using IQCare.Common.Core.Models;
using IQCare.SharedKernel.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace IQCare.Common.Infrastructure
{
    public class CommonDbContext : BaseContext
    {
        public DbSet<LookupItemView> LookupItemViews { get; set; }
        public CommonDbContext(DbContextOptions<CommonDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LookupItemView>().ToTable("LookupItemView").HasKey(x => x.RowID);

            base.OnModelCreating(modelBuilder);
        }
    }
}
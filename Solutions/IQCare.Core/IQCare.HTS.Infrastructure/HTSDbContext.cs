using IQCare.HTS.Core.Model;
using IQCare.SharedKernel.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace IQCare.HTS.Infrastructure
{
    public class HtsDbContext : BaseContext
    {
        public DbSet<Form> Forms { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<HtsEncounter> HtsEncounters { get; set; }
        public HtsDbContext(DbContextOptions<HtsDbContext> options) : base(options)
        {
        }
    }
}
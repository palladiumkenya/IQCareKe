using System.Data.Common;
using DataAccess.Base;
using System.Data.Entity;
using Entities.Pharmacy;

namespace DataAccess.Pharmacy
{
    public class PharmacyContext : DbContext
    {
        public PharmacyContext(): base((DbConnection)DataMgr.GetConnection(), true)
        {
            Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer<PharmacyContext>(null);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);        

        }      
        public DbSet<PharmacyItem> PharmacyItem { get; set; }

        public DbSet<PrescriptionItem> PrescriptionItem { get; set; }
       
        public DbSet<Prescription> Prescription { get; set; }
        
    }
}

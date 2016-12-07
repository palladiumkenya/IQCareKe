using System.Data.Entity;
using Common.Data;
using Config.Core.Model;

namespace Config.Data
{
    public class ConfigContext:BaseContext
    {
        public ConfigContext():base()
        {
        }

        public ConfigContext(string connection) : base(connection)
        {
        }

        public DbSet<ServiceArea> ServiceAreas { get; set; }
    }
}
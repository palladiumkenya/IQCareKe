using System.Data.Common;
using System.Data.Entity;
using DataAccess.Base;
using Entities.Common;

namespace DataAccess.Context
{



    public class PersonContext :BaseContext
    {
        public PersonContext() :  base((DbConnection)DataMgr.GetConnection(), true) {

            Configuration.ProxyCreationEnabled = false;
            // DataMgr.OpenDecryptedSession(base.Database.Connection);
            // Configuration.LazyLoadingEnabled = true;
            Database.SetInitializer<PersonContext>(null);
        }

        public DbSet<Person> Persons  { get; set; }
    }
}

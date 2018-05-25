using DataAccess.Base;
using DataAccess.Context;
using Entities.CCC;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CCC.Context
{
   public class ViewContext : BaseContext
    {
        public ViewContext() : base((DbConnection)DataMgr.GetConnection(), true)
        {
            Configuration.ProxyCreationEnabled = false;
            // DataMgr.OpenDecryptedSession(base.Database.Connection);
            Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer<ViewContext>(null);
        }

        public DbSet<PatientRelationshipDTO> PatientRelationshipList { get; set; }
    }
}

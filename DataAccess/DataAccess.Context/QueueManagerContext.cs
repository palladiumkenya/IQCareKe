using DataAccess.Base;
using Entities.Queue;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;


namespace DataAccess.Context
{
   public class QueueManagerContext:BaseContext
    {
        public QueueManagerContext() : base((DbConnection)DataMgr.GetConnection(), true)
        {
            Configuration.ProxyCreationEnabled = false;
            // DataMgr.OpenDecryptedSession(base.Database.Connection);
            Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer<QueueManagerContext>(null);
        }

      //  public DbSet<QueueManager> QueueManagers { get; set; }

        public DbSet<WaitingQueue> Queue { get; set; }
    }
}

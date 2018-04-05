using Entities.Queue;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Context.Repository
{
    public class QueueManagerRepository  :BaseRepository<QueueManager>, IDisposable
    {
        private readonly QueueManagerContext _context;
      

        public void Dispose()
        {
            _context.Dispose();
        }

        public QueueManagerRepository() : this(new QueueManagerContext()) => _context = new QueueManagerContext();
        public QueueManagerRepository(QueueManagerContext context) : base(context)
        {
            _context = context;
        }

        public override int Count()
        {
            return base.Count();
        }
        public override IQueryable<QueueManager> Filter(Expression<Func<QueueManager, bool>> filter)
        {
            return base.Filter(filter);
        }
        public  WaitingQueue QueueByName(string name)
        {
            return _context.Queue.Where(ws => ws.QueueName == name).FirstOrDefault();

        }
    }


}

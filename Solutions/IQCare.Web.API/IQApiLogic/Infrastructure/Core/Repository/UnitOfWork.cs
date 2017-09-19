using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using DataAccess.Context;
using DataAccess.Context.ModuleMaster;
using IQ.ApiLogic.Infrastructure.Context;
using IQ.ApiLogic.Infrastructure.Core.Interface;

namespace IQ.ApiLogic.Infrastructure.Core.Repository
{
   public class UnitOfWork:IUnitOfWork,IDisposable
    {
        private BaseContext _context;

        private IApiInboxRepository _apiInboxRepository;
        private IApiOutboxRepository _apiOutboxRepository;
        private IApiInteropSystemsRepository _apiInteropSystemsRepository;

        public UnitOfWork(BaseContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("missing context");
            }
            _context = context;
        }

        public DbContext Context { get { return _context; } }

        public IApiInboxRepository ApiInboxRepository
        {
            get
            {
                return _apiInboxRepository ?? (_apiInboxRepository = new ApiInboxRepository((ApiContext)_context));
            }
        }

        public IApiOutboxRepository ApiOutboxRepository
        {
            get
            {
                return _apiOutboxRepository ?? (_apiOutboxRepository=new ApiOutboxRepository((ApiContext)_context));
            }
        }

        public IApiInteropSystemsRepository ApiInteropSystemsRepository
        {
            get
            {
                return _apiInteropSystemsRepository ??
                       (_apiInteropSystemsRepository = new ApiInteropSystemRepository((ApiContext) _context));
            }
        }


        public int Complete()
        {
            return _context.SaveChanges();
        }


        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}

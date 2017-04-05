﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Encounter;
using DataAccess.Context;
using Entities.CCC.Encounter;

namespace DataAccess.CCC.Repository.Encounter
{
    public class PatientCareEndingRepository:BaseRepository<PatientCareEnding>, IPatientCareEndingRepository,IDisposable
    {
        private readonly GreencardContext _context;

        public PatientCareEndingRepository():base(new GreencardContext())
        {
            
        }

        public PatientCareEndingRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }


        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                _disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
            GC.SuppressFinalize(this);
        }

        ~PatientCareEndingRepository()
        {
            Dispose(false);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Lookup;
using DataAccess.Context;
using Entities.CCC.Lookup;

namespace DataAccess.CCC.Repository.Lookup
{
   public class LookupPreviousLabsRepository : BaseRepository<LookupPreviousLabs>, ILookupPreviousLabs,IDisposable
    {
        private readonly LookupContext _context;

        public LookupPreviousLabsRepository() : this(new LookupContext())
        {
        }

        public LookupPreviousLabsRepository(LookupContext context) : base(context)
       {
            _context = context;
        }

        public List<LookupPreviousLabs> FindBy(Func<LookupPreviousLabs, bool> p)
        {
            var results = _context.LookupPreviousLaboratories.Where(p);
            //  .Where(p).ToList<LookupCounty>();

            return results.ToList();
        }

        public List<LookupPreviousLabs> GetPreviousLabs(int patientId)
        {
            ILookupPreviousLabs previouslabsrepository = new LookupPreviousLabsRepository();
            // var list = previouslabsrepository.GetAll().GroupBy(x => x.Id).Select(x => x.First()).OrderBy(l => l.TestName);
            //return list.ToList();
            var vl = "Viral Load";
            var complete = "Complete";

            var myList = previouslabsrepository.FindBy(x => x.PatientId == patientId);
            var list = myList.GroupBy(x => x.Id).Select(x => x.First()).OrderBy(x => x.LabName).Where(x => x.LabName != vl).Where(x => x.Results == complete); 
            return list.Distinct().ToList();
        }

        public List<LookupPreviousLabs> GetPendingLabs(int patientId)
        {
            ILookupPreviousLabs previouslabsrepository = new LookupPreviousLabsRepository();
            // var list = previouslabsrepository.GetAll().GroupBy(x => x.Id).Select(x => x.First()).OrderBy(l => l.TestName);
            //return list.ToList();
            var vl = "Viral Load";
            var pending = "Pending";

            var myList = previouslabsrepository.FindBy(x => x.PatientId == patientId);
            var list = myList.GroupBy(x => x.Id).Select(x => x.First()).OrderBy(x => x.LabName).Where(x => x.LabName != vl).Where(x => x.Results == pending); 
            return list.Distinct().ToList();
        }
        public List<LookupPreviousLabs> GetVlLabs(int patientId)
        {
            ILookupPreviousLabs previouslabsrepository = new LookupPreviousLabsRepository();
            var vl = "Viral Load";
            var complete = "Complete";

            var myList = previouslabsrepository.FindBy(x => x.PatientId == patientId);
            var list = myList.GroupBy(x => x.Id).Select(x => x.First()).OrderBy(x => x.LabName).Where(x => x.LabName == vl).Where(x => x.Results == complete); 
            return list.Distinct().ToList();
        }

        public List<LookupPreviousLabs> GetPendingVlLabs(int patientId)
        {
            ILookupPreviousLabs previouslabsrepository = new LookupPreviousLabsRepository();
            var vl = "Viral Load";
            var pending = "Pending";
            var myList = previouslabsrepository.FindBy(x => x.PatientId == patientId);
            var list = myList.GroupBy(x => x.Id).Select(x => x.First()).OrderBy(x => x.LabName).Where(x => x.LabName == vl).Where(x => x.Results == pending);
            return list.Distinct().ToList();
            //.Where(x => x.Results == pending)
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

        ~LookupPreviousLabsRepository()
        {
            Dispose(false);
        }

    }
}

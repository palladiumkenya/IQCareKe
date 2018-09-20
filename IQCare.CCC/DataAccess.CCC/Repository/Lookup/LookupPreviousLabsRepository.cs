using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Lookup;
using DataAccess.Context;
using Entities.CCC.Lookup;

namespace DataAccess.CCC.Repository.Lookup
{
    public class LookupPreviousLabsRepository : BaseRepository<LookupPreviousLabs>, ILookupPreviousLabs
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
       
        public List<LookupPreviousLabs> GetExtruderCompleteLabs(int patientId)
        {
            ILookupPreviousLabs completelabsrepository = new LookupPreviousLabsRepository();
           
            var complete = "Complete";
            var myList = completelabsrepository.FindBy(
                x =>  x.PatientId == patientId && (x.Results.ToLower().Trim() == complete || x.Results.ToLower().Trim() == "completed"));

            myList = myList.OrderByDescending(x => x.Id).Take(5);

            //DateTime? maxLabDate = null;
            //if (myList.ToList().Count > 0) {

            //    maxLabDate = myList.Max(x => x.ResultDate);
            //}

            ////gets max laborder           
            //var list = myList.Where(x => x.ResultDate == maxLabDate);

            //return list.ToList();
            return myList.ToList();


        }


        public List<LookupPreviousLabs> GetExtruderPendingLabs(int patientId)
        {
            ILookupPreviousLabs previouslabsrepository = new LookupPreviousLabsRepository();
            // var list = previouslabsrepository.GetAll().GroupBy(x => x.Id).Select(x => x.First()).OrderBy(l => l.TestName);
            //return list.ToList();         
            var pending = "Pending";

            var myList = previouslabsrepository.FindBy(
                x =>
                x.PatientId == patientId &&              x.Results == pending);
            var list = myList.GroupBy(x => x.Id).Select(x => x.First());
            return list.Distinct().ToList();
        }
        public List<LookupPreviousLabs> GetPreviousLabs(int patientId)
        {
            ILookupPreviousLabs previouslabsrepository = new LookupPreviousLabsRepository();
            // var list = previouslabsrepository.GetAll().GroupBy(x => x.Id).Select(x => x.First()).OrderBy(l => l.TestName);
            //return list.ToList();
            //var vl = "Viral Load";
            var complete = "Complete";
            var myList = previouslabsrepository.FindBy(
                x =>
                x.PatientId == patientId && (x.Results.ToLower().Trim() == complete || x.Results.ToLower().Trim() == "completed"));
            var list = myList.GroupBy(x => x.Id).Select(x => x.First()).OrderBy(x => x.Id);
            return list.Distinct().ToList();
        }
        public List<LookupPreviousLabs> GetPendingLabs(int patientId)
        {
            ILookupPreviousLabs pendinglabsrepository = new LookupPreviousLabsRepository();
          // var vl = "Viral Load";
           var pending = "Pending";
           var myList = pendinglabsrepository.FindBy(
                x =>
                  x.PatientId == patientId &&
                  x.Results == pending);
            var list = myList.GroupBy(x => x.Id).Select(x => x.First()).OrderBy(x => x.Id);
            return list.Distinct().ToList();
        }
        public List<LookupPreviousLabs> GetVlLabs(int patientId)
        {
            ILookupPreviousLabs previouslabsvl = new LookupPreviousLabsRepository();
            var vl = "Viral Load";
            var complete = "Complete";

            var myList = previouslabsvl.FindBy(x => x.PatientId == patientId);
            var list = myList.GroupBy(x => x.Id).Select(x => x.First()).OrderBy(x => x.Id).Where(x => x.LabName == vl).Where(x => (x.Results.ToLower().Trim() == complete || x.Results.ToLower().Trim() == "completed"));
            return list.Distinct().ToList();
        }

        public List<LookupPreviousLabs> GetPendingVlLabs(int patientId)
        {
            ILookupPreviousLabs pendingvllabs = new LookupPreviousLabsRepository();
            var vl = "Viral Load";
            var pending = "Pending";
            var myList = pendingvllabs.FindBy(x => x.PatientId == patientId);
            var list = myList.GroupBy(x => x.Id).Select(x => x.First()).OrderBy(x => x.Id).Where(x => x.LabName == vl).Where(x => x.Results == pending);
            return list.Distinct().ToList();
            //.Where(x => x.Results == pending)
        }
    }
}

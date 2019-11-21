using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Entities.Lab;
using System.Linq.Expressions;

namespace DataAccess.Context
{
    public class LabOrderRepository : Repository<LabOrder>, IDisposable
    {
        public LabOrderRepository()
        {
           // OpenDecryptedSession();
        }
        public override LabOrder Find(object id)
        {
            return GetAll().FirstOrDefault(x => x.Id == (int)id);
        }
        public override IQueryable<LabOrder> Filter(Expression<Func<LabOrder, bool>> filter)
        {
            return labContext.LabOrder.Include(p=> p.Client).Where(filter);
        }
        public override IEnumerable<LabOrder> GetAll()
        {
            var orders = labContext.LabOrder
                .Include(s => s.Client).DefaultIfEmpty();
            return orders;
        }
        public override List<LabOrder> GetAllFilterd(Entities.Common.IFilter filter)
        {
            var laborders =  Filter(o => o.LocationId == filter.LocationId && o.DeleteFlag == filter.DeleteFlag);

            LabOrderFilter _filter = (LabOrderFilter)filter;

            if (_filter.PatientId.HasValue)
            {
                laborders = laborders.Where(o => o.Client.Id == _filter.PatientId.Value);
            }
            if (!string.IsNullOrEmpty(_filter.ReferenceNumber))
            {
                laborders = laborders.Where(o => o.OrderNumber == _filter.ReferenceNumber);
            }
            if (_filter.DateFrom.HasValue && _filter.DateTo.HasValue)
            {
                laborders = laborders.Where(o => o.OrderDate >= _filter.DateFrom.Value && o.OrderDate <= _filter.DateTo.Value);
            }
            if (!string.IsNullOrEmpty(_filter.OrderStatus))
            {
                if (_filter.OrderStatus == "Pending")
                {
                    laborders = laborders.Where(o => o.OrderStatus == _filter.OrderStatus || o.OrderStatus == "Partially Completed");
                }

            }
            return laborders.ToList();
            
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public override void Delete(LabOrder entityToDelete)
        {
            //using (var dbContextTransaction = labContext.Database.BeginTransaction())
            //{
            //    try
            //    {
            //        entityToDelete.DeletedBy = entityToDelete.DeletedBy;
            //        entityToDelete.DeleteFlag = true;
            //        var tests = labContext.LabOrderTests.Where(t => t.LabOrderId == entityToDelete.Id);
            //        foreach (LabOrderTest t in tests)
            //        {
            //            t.DeleteFlag = true;
            //        }
            //        //tests.ForEach(t => t.DeleteFlag = true);

            //        var results = labContext.ParameterResults.Where(p => p.LabOrderId == entityToDelete.Id);
            //        foreach (LabTestParameterResult r in results)
            //        {
            //            r.DeleteFlag = true;
            //        }
            //        //results.ForEach(r => r.DeleteFlag = true);

            //        //  entityToDelete.OrderedTest.ForEach(t => t.ParameterResults.ForEach(p => p.DeleteFlag = true));
            //        labContext.SaveChanges();
            //        dbContextTransaction.Commit();
            //    }
            //    catch
            //    {
            //        dbContextTransaction.Rollback();
            //    }

            //}
            labContext.Database.ExecuteSqlCommand("Exec Laboratory_DeleteLabOrder @LabOrderId ={0}, @DeletedBy = {1}, @DeleteReason = {2}", entityToDelete.Id, entityToDelete.DeletedBy,entityToDelete.DeleteReason);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    labContext.Dispose();
                }
            }
            this.disposed = true;
        }
        
        
    }
}

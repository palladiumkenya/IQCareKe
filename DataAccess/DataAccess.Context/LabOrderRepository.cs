using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Entities.Lab;
namespace DataAccess.Context
{
    public class LabOrderRepository : Repository<LabOrder>
    {
        public LabOrderRepository()
        {
           // OpenDecryptedSession();
        }
        public override LabOrder Find(object id)
        {
            return GetAll().FirstOrDefault(x => x.Id == (int)id);
        }
        public override IQueryable<LabOrder> Filter(System.Linq.Expressions.Expression<Func<LabOrder, bool>> filter)
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
            return laborders.ToList<LabOrder>();
            
        }
        public void Dispose()
        {
           // CloseDecryptedSession();
        }
    }
}

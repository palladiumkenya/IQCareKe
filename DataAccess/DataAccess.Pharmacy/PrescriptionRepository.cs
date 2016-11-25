using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.Pharmacy;
using System.Data.Entity;
namespace DataAccess.Pharmacy
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="DataAccess.Pharmacy.Repository{Entities.Pharmacy.Prescription}" />
    public class PrescriptionRepository : Repository<Prescription>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrescriptionRepository"/> class.
        /// </summary>
        public PrescriptionRepository()
        {
           
        }
        /// <summary>
        /// Finds the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public override Prescription Find(object id)
        {
            return GetAll().FirstOrDefault(x => x.Id == (int)id);
        }
        /// <summary>
        /// Filters the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public override IQueryable<Prescription> Filter(System.Linq.Expressions.Expression<Func<Prescription, bool>> filter)
        {
            return context.Prescription.Include(p => p.Client).Where(filter);
        }
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<Prescription> GetAll()
        {
            var orders = context.Prescription
                .Include(s => s.Client).DefaultIfEmpty();
            return orders;
        }

        /// <summary>
        /// Gets all filterd.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public override List<Prescription> GetAllFilterd(Entities.Common.IFilter filter)
        {
            var prescriptions = Filter(o => o.LocationId == filter.LocationId);

            PrescriptionFilter _filter = (PrescriptionFilter)filter;

            if (_filter.PatientId.HasValue)
            {
                prescriptions = prescriptions.Where(o => o.Client.Id == _filter.PatientId.Value);
            }
            if (!string.IsNullOrEmpty(_filter.ReferenceNumber))
            {
                prescriptions = prescriptions.Where(o => o.PrescriptionNumber == _filter.ReferenceNumber);
            }
            if (_filter.DateFrom.HasValue && _filter.DateTo.HasValue)
            {
                prescriptions = prescriptions.Where(o => o.PrescriptionDate >= _filter.DateFrom.Value && o.PrescriptionDate <= _filter.DateTo.Value);
            }
            //if (!string.IsNullOrEmpty(_filter.OrderStatus))
            //{
            //    prescriptions = prescriptions.Where(o => o.OrderStatus == _filter.OrderStatus);
            //}
            return prescriptions.ToList<Prescription>();
        }
    }
}

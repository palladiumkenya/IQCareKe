using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Base;
using Interface.Pharmacy;
using DataAccess.Pharmacy;
using Entities.Pharmacy;

namespace BusinessProcess.Pharmacy
{
    public class BPharmacyRequest : ProcessBase, IPharmacyRepo
    {
        public List<Entities.Pharmacy.Prescription> GetAll(Entities.Common.IFilter orderFilters)
        {
            PrescriptionRepository repo = new PrescriptionRepository();
            if (orderFilters != null)
                return repo.GetAllFilterd(orderFilters);
            else
            {
                return repo.GetAll().ToList<Prescription>();
            }
        }
    }
}

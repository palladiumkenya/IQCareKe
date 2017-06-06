using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;

namespace BusinessProcess.CCC.Lookup
{
    public class BFacilityListManager : ProcessBase, IFacilityList
    {
        public List<FacilityList> GetFacilitiesList()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var facilityList = unitOfWork.FacilityListRepository.GetAll().ToList();
                unitOfWork.Dispose();
                return facilityList;
            }
        }
    }
}

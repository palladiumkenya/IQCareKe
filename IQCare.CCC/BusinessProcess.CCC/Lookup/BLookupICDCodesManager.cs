using DataAccess.CCC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.CCC.Lookup;
using DataAccess.CCC.Context;
using DataAccess.Base;
using Interface.CCC.Lookup;

namespace BusinessProcess.CCC.Lookup
{
    public class BLookupICDCodesManager:ProcessBase, ILookupICDCodesManager
    {
        public List<ICDCodeList> GetICDCodeList()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var CodeList = unitOfWork.LookupICDCodesRepository.GetAll();
                unitOfWork.Dispose();
                return CodeList.ToList();
            }
        }

    }
}

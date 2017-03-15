using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;

namespace BusinessProcess.CCC
{
    public class BPersonLookUpManager : ProcessBase, IPersonLookUpManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext());
        public PersonLookUp GetPersonById(int id)
        {
            return _unitOfWork.PersonLookUpRepository.GetById(id);
        }
    }
}

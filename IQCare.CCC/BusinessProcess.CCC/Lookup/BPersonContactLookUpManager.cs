using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;

namespace BusinessProcess.CCC.LookupLogic
{
    public class BPersonContactLookUpManager : ProcessBase, IPersonContactLookUpManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext());
        public List<PersonContactLookUp> GetPersonContactByPersonId(int personId)
        {
            return _unitOfWork.PersonContactLookUpRepository.FindBy(x => x.PersonId == personId).ToList();
        }
    }
}

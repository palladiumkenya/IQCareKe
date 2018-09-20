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
    public class BPersonGreenCardLookupManager : ProcessBase, IPersonGreenCardLookupManager
    {
        public List<PersonGreenCardLookup> GetPtnPkByPersonId(int personId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var list = unitOfWork.PersonGreenCardLookupRepository.FindBy(x => x.PersonId == personId).ToList();
                unitOfWork.Dispose();
                return list;
            }
        }

        public PersonGreenCardLookup AddPersonToBlueCardLookup(PersonGreenCardLookup personGreenCardLookup)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PersonGreenCardLookupRepository.Add(personGreenCardLookup);
                unitOfWork.Complete();
                unitOfWork.Dispose();
                return personGreenCardLookup;
            }
        }
    }
}

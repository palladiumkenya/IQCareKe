using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;

namespace BusinessProcess.CCC.Lookup
{
    public class BPersonContactLookUpManager : ProcessBase, IPersonContactLookUpManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext());
        public List<PersonContactLookUp> GetPersonContactByPersonId(int personId)
        {
            try
            {
                return _unitOfWork.PersonContactLookUpRepository.FindBy(x => x.PersonId == personId).ToList();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }
    }
}

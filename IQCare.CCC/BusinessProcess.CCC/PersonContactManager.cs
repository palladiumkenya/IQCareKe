using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.CCC.Repository;
using DataAccess.Context;
using Entities.Common;
using Interface.CCC;

namespace BusinessProcess.CCC
{
    public class PersonContactManager : IPersonContactManager
    {
        private UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext());

        public void AddPersonContact(PersonContact p)
        {
            _unitOfWork.PersonContactRepository.Add(p);
            _unitOfWork.Complete();
        }

        public List<PersonContact> GetAllPersonContact(int personId)
        {
            var mylist=_unitOfWork.PersonContactRepository.GetAllPersonContact(personId);
            return mylist.ToList();
        }

        public List<PersonContact> GetLatespersonContact(int personId)
        {
            var mylist = _unitOfWork.PersonContactRepository.GetLatespersonContact(personId);
            return mylist.ToList();
        }

        public void UpdatePersonContact(PersonContact p,int id)
        {
            
        }

        public void VoidPersonContact(int personId, int id)
        {
            throw new NotImplementedException();
        }
    }
}

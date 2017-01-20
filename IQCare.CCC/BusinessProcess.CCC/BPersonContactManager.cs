using System.Collections.Generic;
using DataAccess.CCC.Repository;
using DataAccess.Context;
using Entities.Common;
using Interface.CCC;

namespace BusinessProcess.CCC
{
    public class BPersonContactManager : IPersonContactManager
    {
        private UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext());

        public void AddPersonContact(PersonContact p)
        {
            _unitOfWork.PersonContactRepository.Add(p);
            _unitOfWork.Complete();
        }

        public void DeletePersonContact(int id)
        {
           PersonContact personContact= _unitOfWork.PersonContactRepository.GetById(id);
            _unitOfWork.PersonContactRepository.Remove(personContact);
            _unitOfWork.Complete();
        }

        public List<PersonContact> GetAllPersonContact(int personId)
        {
            return _unitOfWork.PersonContactRepository.GetAllPersonContact(personId);
        }

        public List<PersonContact> GetCurrentPersonContacts(int personId)
        {
            var myList = _unitOfWork.PersonContactRepository.GetCurrentPersonContact(personId);
            return myList;
        }

        public void UpdatePersonContact(PersonContact p)
        {
            _unitOfWork.PersonContactRepository.Update(p);
        }
    }
}

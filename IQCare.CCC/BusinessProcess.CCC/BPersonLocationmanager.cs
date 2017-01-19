using System.Collections.Generic;
using System.Linq;
using DataAccess.Base;
using DataAccess.CCC.Repository;
using DataAccess.Context;
using Entities.Common;
using Interface.CCC;

namespace BusinessProcess.CCC
{
    public class BPersonLocationmanager : ProcessBase,IPersonLocationManager
    {
        private UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext());

        public void AddPersonLocation(PersonLocation location)
        {
            _unitOfWork.PersonLocationRepository.Add(location);
            _unitOfWork.Complete();
           // return location.Id;
        }

        public void DeletePersonLocation(int id)
        {
          PersonLocation location=  _unitOfWork.PersonLocationRepository.GetById(id);
          _unitOfWork.PersonLocationRepository.Remove(location);
            _unitOfWork.Complete();
        }

        public List<PersonLocation> GetCurrentPersonLocation(int persoId)
        {
            var myList = _unitOfWork.PersonLocationRepository.GetPersonCurrentLocation(persoId);
            return myList;
        }

        public List<PersonLocation> GetPersonLocationAll(int personId)
        {
            var mylist = _unitOfWork.PersonLocationRepository.FindBy(x => x.PersonId == personId);
            return mylist.ToList();
        }

        public void UpdatePersonLocation(PersonLocation location)
        {
           _unitOfWork.PersonLocationRepository.Update(location);
            _unitOfWork.Complete();
        }
    }
}

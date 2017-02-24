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
        private int _result;

        public int AddPersonLocation(PersonLocation location)
        {
            _unitOfWork.PersonLocationRepository.Add(location);
           _result= _unitOfWork.Complete();
            return _result;
            // return location.Id;
        }

        public int DeletePersonLocation(int id)
        {
          PersonLocation location=  _unitOfWork.PersonLocationRepository.GetById(id);
          _unitOfWork.PersonLocationRepository.Remove(location);
          _result=  _unitOfWork.Complete();
            return _result;
        }

        public List<PersonLocation> GetCurrentPersonLocation(int persoId)
        {
            return _unitOfWork.PersonLocationRepository.FindBy(x => x.PersonId == persoId && x.DeleteFlag == false).ToList();
                /*personLocation.FindBy(x => x.PersonId == personId & x.DeleteFlag == false)
                .OrderBy(x => x.Id)
                .FirstOrDefault();
            var myList = _unitOfWork.PersonLocationRepository.GetPersonCurrentLocation(persoId);
            return myList;*/
        }

        public List<PersonLocation> GetPersonLocationAll(int personId)
        {
            var mylist = _unitOfWork.PersonLocationRepository.FindBy(x => x.PersonId == personId);
            return mylist.ToList();
        }

        public  int UpdatePersonLocation(PersonLocation location)
        {
           _unitOfWork.PersonLocationRepository.Update(location);
           _result= _unitOfWork.Complete();
            return _result;
        }
    }
}

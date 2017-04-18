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
      //  private UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext());
        private int _result;

        public int AddPersonLocation(PersonLocation location)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext()))
            {
                _unitOfWork.PersonLocationRepository.Add(location);
                _result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return _result;
            }
        }

        public int DeletePersonLocation(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext()))
            {
                PersonLocation location = _unitOfWork.PersonLocationRepository.GetById(id);
                _unitOfWork.PersonLocationRepository.Remove(location);
                _result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return _result;
            }
        }

        public List<PersonLocation> GetCurrentPersonLocation(int persoId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext()))
            {
               var location= _unitOfWork.PersonLocationRepository.FindBy(x => x.PersonId == persoId && x.DeleteFlag == false)
                        .ToList();
                _unitOfWork.Dispose();
                return location;
            }
                /*personLocation.FindBy(x => x.PersonId == personId & x.DeleteFlag == false)
                .OrderBy(x => x.Id)
                .FirstOrDefault();
            var myList = _unitOfWork.PersonLocationRepository.GetPersonCurrentLocation(persoId);
            return myList;*/
        }

        public List<PersonLocation> GetPersonLocationAll(int personId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext()))
            {
                var mylist = _unitOfWork.PersonLocationRepository.FindBy(x => x.PersonId == personId).ToList();
                _unitOfWork.Dispose();
                return mylist;
            }
        }

        public  int UpdatePersonLocation(PersonLocation location)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext()))
            {
                _unitOfWork.PersonLocationRepository.Update(location);
                _result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return _result;
            }
        }
    }
}

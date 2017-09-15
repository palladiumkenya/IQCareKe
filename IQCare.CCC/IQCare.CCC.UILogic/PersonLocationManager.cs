using System.Collections.Generic;
using Application.Presentation;
using Entities.Common;
using Interface.CCC;

namespace IQCare.CCC.UILogic
{

    public class PersonLocationManager
    {
        private IPersonLocationManager _mgr = (IPersonLocationManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPersonLocationmanager, BusinessProcess.CCC");
        private int _result;

        public int AddPersonLocation(int personId,int county,int subcounty,int ward,string village,string location,string sublocation,string landmark,string nearesthealthcentre,int userId)
        {
            PersonLocation personLocation = new PersonLocation()
            {
                PersonId = personId,
                County = county,
                SubCounty = subcounty,
                Ward = ward,
                Village = village,
                Location = location,
                SubLocation = sublocation,
                LandMark = landmark,
                NearestHealthCentre = nearesthealthcentre,
                CreatedBy = userId
            };
          return _result=  _mgr.AddPersonLocation(personLocation);
        }

        public int UpdatePersonLocation(PersonLocation personLocation)
        {
            return _result= _mgr.UpdatePersonLocation(personLocation);
        }

        public int DeletePersonLocation(int id)
        {
           return _result= _mgr.DeletePersonLocation(id);
        }

        public List<PersonLocation> GetCurrentPersonLocation(int persoId)
        {
            var myList = _mgr.GetCurrentPersonLocation(persoId);
            return myList;
        }

        public List<PersonLocation> GetPersonLocationAll(int personId)
        {
            var myList = _mgr.GetPersonLocationAll(personId);
            return myList;
        }
    }
}

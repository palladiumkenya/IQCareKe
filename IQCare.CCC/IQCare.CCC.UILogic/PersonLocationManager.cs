using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Presentation;
using Entities.Common;
using Interface.CCC;

namespace IQCare.CCC.UILogic
{

    public class PersonLocationManager
    {
        private IPersonLocationManager _mgr = (IPersonLocationManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.PersonLocationManager, BusinessProcess.CCC");
        private int _result;

        public int AddPersonLocation(int personId,int county,int subcounty,int ward,string village,string estate,string landmark,string nearesthealthcentre)
        {
            PersonLocation personLocation = new PersonLocation()
            {
                PersonId = personId,
                County = county,
                SubCounty = subcounty,
                Ward = ward,
                Village = village,
                Estate = estate,
                LandMark = landmark,
                NearestHealthCentre = nearesthealthcentre
            };
          return _result=  _mgr.AddPersonLocation(personLocation);
        }

        public int UpdatePersonLocation(int county, int subcounty, int ward, string village, string estate, string landmark, string nearesthealthcentre)
        {
            PersonLocation personLocation = new PersonLocation()
            {
                County = county,
                SubCounty = subcounty,
                Ward = ward,
                Village = village,
                Estate = estate,
                LandMark = landmark,
                NearestHealthCentre = nearesthealthcentre
            };
            return _result= _mgr.AddPersonLocation(personLocation);

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

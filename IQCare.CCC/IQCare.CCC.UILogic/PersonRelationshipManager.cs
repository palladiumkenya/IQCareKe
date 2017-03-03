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
    public class PersonRelationshipManager
    {
        private IPersonRelationshipManager _mgr = (IPersonRelationshipManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.PatientMaritalStatusManager, BusinessProcess.CCC");
        private int _result;

        public int AddPersonRelationship(int personId, int relatedTo, int relationshipType)
        {
            PersonRelationship personRelatioship =new PersonRelationship()
            {
                PersonId = personId,
               RelatedTo = relatedTo,
               RelationshipTypeId = relationshipType

            };
           return _result=_mgr.AddPersonRelationship(personRelatioship);
        }

        public int UpdatePersonRelationship(int relatedTo, int relationshipType)
        {
            PersonRelationship personRelatioship = new PersonRelationship()
            {
                RelatedTo = relatedTo,
                RelationshipTypeId = relationshipType

            };
           return _result=  _mgr.UpdatePersonRelationship(personRelatioship);
        }

        public int DeletePersonRelationship(int id)
        {
          return _result=  _mgr.DeletePersonRelationship(id);
        }

        public List<PersonRelationship> GetAllPersonRelationship(int patientid)
        {
            var myList = _mgr.GetAllPersonRelationship(patientid);
            return myList;
        }
    }
}

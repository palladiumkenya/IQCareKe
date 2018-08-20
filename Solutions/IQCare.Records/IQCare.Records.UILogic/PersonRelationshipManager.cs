using Application.Presentation;
using Entities.Common;
using Interface.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.Records.UILogic
{
  
    public class PersonRelationshipManager
    {
        readonly IPersonRelationshipManager _mgr = (IPersonRelationshipManager)ObjectFactory.CreateInstance("BusinessProcess.Records.BPersonRelationshipManager, BusinessProcess.Records");
        private int _result;

        public int AddPersonRelationship(int personId, int relatedTo, int relationshipType)
        {
            PersonRelationship personRelatioship = new PersonRelationship()
            {
                PersonId = personId,
                //RelatedTo = relatedTo,
                RelationshipTypeId = relationshipType

            };
            return _result = _mgr.AddPersonRelationship(personRelatioship);
        }

        public int UpdatePersonRelationship(int relatedTo, int relationshipType)
        {
            PersonRelationship personRelatioship = new PersonRelationship()
            {
                //RelatedTo = relatedTo,
                RelationshipTypeId = relationshipType

            };
            return _result = _mgr.UpdatePersonRelationship(personRelatioship);
        }

        public int DeletePersonRelationship(int id)
        {
            return _result = _mgr.DeletePersonRelationship(id);
        }

        public List<PersonRelationship> GetAllPersonRelationship(int patientid)
        {
            var myList = _mgr.GetAllPersonRelationship(patientid);
            return myList;
        }

        public bool PersonLinkedToPatient(int personId, int patientId)
        {
            return _mgr.PersonLinkedToPatient(personId, patientId);
        }
    }
}

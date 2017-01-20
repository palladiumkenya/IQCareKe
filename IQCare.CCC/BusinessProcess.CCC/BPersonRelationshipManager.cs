using System.Collections.Generic;
using System.Linq;
using DataAccess.CCC.Repository;
using DataAccess.Context;
using Entities.Common;
using Interface.CCC;

namespace BusinessProcess.CCC
{
    public class BPersonRelationshipManager :IPersonRelationshipManager
    {
        private UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext());

        public void AddPersonRelationship(PersonRelationship personRelationship)
        {
           _unitOfWork.PersonRelationshipRepository.Add(personRelationship);
            _unitOfWork.Complete();
        }

        public void DeletePersonRelationship(int id)
        {
          var personRelation=  _unitOfWork.PersonRelationshipRepository.GetById(id);
            _unitOfWork.PersonRelationshipRepository.Remove(personRelation);
            _unitOfWork.Complete();
        }

        public List<PersonRelationship> GetAllPersonRelationship(int personId)
        {
           
            var myList =
                _unitOfWork.PersonRelationshipRepository.FindBy(x => x.PersonId == personId & x.DeleteFlag == false);
            return myList.ToList();
        }

        public void UpdatePersonRelationship(PersonRelationship personRelationship)
        {
            _unitOfWork.PersonRelationshipRepository.Update(personRelationship);
            _unitOfWork.Complete();
        }
    }
}

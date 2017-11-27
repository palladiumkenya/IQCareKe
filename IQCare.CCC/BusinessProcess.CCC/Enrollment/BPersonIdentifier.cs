using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Enrollment;
using Interface.CCC.Enrollment;

namespace BusinessProcess.CCC.Enrollment
{
    public class BPersonIdentifier : ProcessBase, IPersonIdentifierManager
    {
        public int AddPersonIdentifier(PersonIdentifier personIdentifier)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PersonIdentifierRepository.Add(personIdentifier);
                unitOfWork.Complete();
                unitOfWork.Dispose();
                return personIdentifier.Id;
            }
        }

        public List<PersonIdentifier> GetPersonIdenfiers(int personId, int identifierId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var identifierList = unitOfWork.PersonIdentifierRepository
                    .FindBy(x => x.PersonId == personId && x.IdentifierId == identifierId).ToList();
                unitOfWork.Dispose();
                return identifierList;
            }
        }
    }
}

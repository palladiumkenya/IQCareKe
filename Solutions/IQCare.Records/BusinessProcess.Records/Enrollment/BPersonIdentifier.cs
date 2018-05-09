using DataAccess.Base;
using DataAccess.Records;
using DataAccess.Records.Context;
using Entities.Records.Enrollment;
using Interface.Records.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessProcess.Records.Enrollment
{
    public class BPersonIdentifier : ProcessBase, IPersonIdentifierManager
    {
        public int AddPersonIdentifier(PersonIdentifier personIdentifier)
    {
        using (UnitOfWork unitOfWork = new UnitOfWork(new RecordContext()))
        {
            unitOfWork.PersonIdentifierRepository.Add(personIdentifier);
            unitOfWork.Complete();
            unitOfWork.Dispose();
            return personIdentifier.Id;
        }
    }

    public List<PersonIdentifier> GetPersonIdenfiers(int personId, int identifierId)
    {
        using (UnitOfWork unitOfWork = new UnitOfWork(new RecordContext()))
        {
            var identifierList = unitOfWork.PersonIdentifierRepository
                .FindBy(x => x.PersonId == personId && x.IdentifierId == identifierId).ToList();
            unitOfWork.Dispose();
            return identifierList;
        }
    }
}
}

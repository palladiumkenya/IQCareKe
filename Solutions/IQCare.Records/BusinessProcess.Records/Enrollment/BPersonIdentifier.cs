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
        internal int Result;
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

        public PersonIdentifier GetCurrentPersonIdentifier(int personId, int identifierId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new RecordContext()))
            {
                var identifierList = unitOfWork.PersonIdentifierRepository
                     .FindBy(x => x.PersonId == personId && x.IdentifierId == identifierId).OrderByDescending(o => o.CreateDate).FirstOrDefault();
                unitOfWork.Dispose();
                return identifierList;
            }
        }

        public int DeletePersonIdentifier(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new RecordContext()))
            {
                var identifier = _unitOfWork.PersonIdentifierRepository.GetById(id);
                _unitOfWork.PersonIdentifierRepository.Remove(identifier);
                Result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }
        }

        public int UpdatePersondentifier(PersonIdentifier personIdentifier)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new RecordContext()))
            {
                _unitOfWork.PersonIdentifierRepository.Update(personIdentifier);
                Result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }
        }
    }
}

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
    public class BIdentifier : ProcessBase, IIdentifierManager
    {
        public List<Identifier> GetAllIdentifiers()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new RecordContext()))
            {
                var identifiers = unitOfWork.IdentifierRepository.GetAll().ToList();
                unitOfWork.Dispose();
                return identifiers;
            }
        }

        public Identifier GetIdentifierByCode(string code)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new RecordContext()))
            {
                var identifier = unitOfWork.IdentifierRepository.FindBy(x => x.Code == code).FirstOrDefault();
                unitOfWork.Dispose();
                return identifier;
            }
        }

        public List<Identifier> GetIdentifiersById(int identifierId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new RecordContext()))
            {
                var identifiers = unitOfWork.IdentifierRepository.FindBy(x => x.Id == identifierId).ToList();
                unitOfWork.Dispose();
                return identifiers;
            }
        }
        public List<Identifier> GetMultipleIdentifierByCode(string code)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new RecordContext()))
            {
                List<Identifier> identifiers = unitOfWork.IdentifierRepository.FindBy(x => x.Code == code).ToList();
                unitOfWork.Dispose();
                return identifiers;
            }

        }
    }
}

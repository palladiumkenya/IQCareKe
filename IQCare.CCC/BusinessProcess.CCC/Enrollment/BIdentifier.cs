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
    public class BIdentifier : ProcessBase, IIdentifiersManager
    {
        public List<Identifier> GetAllIdentifiers()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var identifiers = unitOfWork.IdentifierRepository.GetAll().ToList();
                unitOfWork.Dispose();
                return identifiers;
            }
        }

        public Identifier GetIdentifierByName(string name)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var identifier = unitOfWork.IdentifierRepository.FindBy(x => x.Name==name && !x.DeleteFlag).FirstOrDefault();
                unitOfWork.Dispose();
                return identifier;
            }
        }
        public Identifier GetIdentifierByCode(string code)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var identifier = unitOfWork.IdentifierRepository.FindBy(x => x.Code == code).FirstOrDefault();
                unitOfWork.Dispose();
                return identifier;
            }
        }

        public List<Identifier> GetIdentifiersById(int identifierId)
        {
            using (UnitOfWork unitOfWork=new UnitOfWork(new GreencardContext()))
            {
                var identifiers = unitOfWork.IdentifierRepository.FindBy(x => x.Id == identifierId).ToList();
                unitOfWork.Dispose();
                return identifiers;
            }
        }
    }
}

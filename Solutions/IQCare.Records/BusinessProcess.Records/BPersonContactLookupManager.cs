using DataAccess.Base;
using DataAccess.Lookup;
using DataAccess.Records;
using Entities.Records;
using Interface.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessProcess.Records
{
    public class BPersonContactLookUpManager : ProcessBase, IPersonContactLookUpManager
    {
        //  private readonly UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext());

        public List<PersonContactLookUp> GetPersonContactByPersonId(int personId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new LookupContext()))
            {
                var personContactList = _unitOfWork.PersonContactLookUpRepository.FindBy(x => x.PersonId == personId).ToList();
                _unitOfWork.Dispose();
                return personContactList;
            }
        }
    }
}

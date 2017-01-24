using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccess.CCC.Repository;
using DataAccess.Context;
using Entities.Common;
using Interface.CCC;

namespace BusinessProcess.CCC
{
    public class BPersonContactManager : IPersonContactManager
    {
        private UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext());
        private int _result;

        public int AddPersonContact(PersonContact personContact)
        {
            SqlParameter personIdParameter =new SqlParameter("personIdParameter",SqlDbType.Int);
            personIdParameter.Value = personContact.PersonId;

            SqlParameter physicalAdressParameter =new SqlParameter("physicalAddressParameter",SqlDbType.VarBinary);
            physicalAdressParameter.Value = personContact.PhysicalAddress;

            SqlParameter mobileNumberParameter =new SqlParameter("mobileNumberParameter",SqlDbType.VarBinary);
            mobileNumberParameter.Value = personContact.MobileNumber;

            _unitOfWork.PersonContactRepository.ExecuteProcedure("exec PersonContact_Insert @personId,@physicalAddress,@mobileNumber",personIdParameter, physicalAdressParameter, mobileNumberParameter);
            //_unitOfWork.PersonContactRepository.Add(p);
            return _result = _unitOfWork.Complete();
        }

        public int DeletePersonContact(int id)
        {
            PersonContact personContact = _unitOfWork.PersonContactRepository.GetById(id);
            _unitOfWork.PersonContactRepository.Remove(personContact);
            return _result = _unitOfWork.Complete();
        }

        public List<PersonContact> GetAllPersonContact(int personId)
        {
            return _unitOfWork.PersonContactRepository.GetAllPersonContact(personId);
        }

        public List<PersonContact> GetCurrentPersonContacts(int personId)
        {
            var myList = _unitOfWork.PersonContactRepository.GetCurrentPersonContact(personId);
            return myList;
        }

        public int UpdatePersonContact(PersonContact p)
        {
            _unitOfWork.PersonContactRepository.Update(p);
            return _result = _unitOfWork.Complete();
        }
    }
}

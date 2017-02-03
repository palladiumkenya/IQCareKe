using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccess.Base;
using DataAccess.CCC.Repository;
using DataAccess.Context;
using Entities.Common;
using Interface.CCC;

namespace BusinessProcess.CCC
{
    public class BPersonContactManager :ProcessBase ,IPersonContactManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext());
        private int _result;

        public int AddPersonContact(PersonContact personContact)
        {
            SqlParameter personIdParameter =new SqlParameter("personIdParameter",SqlDbType.Int);
            personIdParameter.Value = personContact.PersonId;

            SqlParameter physicalAdressParameter =new SqlParameter("physicalAddressParameter",SqlDbType.VarChar);
            physicalAdressParameter.Value = personContact.PhysicalAddress;

            SqlParameter mobileNumberParameter =new SqlParameter("mobileNumberParameter",SqlDbType.VarChar);
            mobileNumberParameter.Value = personContact.MobileNumber;

            SqlParameter alternativeNumberParameter = new SqlParameter("alternativeNumberParameter", SqlDbType.VarChar);
            alternativeNumberParameter.Value = personContact.AlternativeNumber;

            SqlParameter emailAddressParameter = new SqlParameter("emailAddressParameter", SqlDbType.VarChar);
            emailAddressParameter.Value = personContact.EmailAddress;

            _unitOfWork.PersonContactRepository.ExecuteProcedure("exec PersonContact_Insert @personIdParameter,@physicalAddressParameter,@mobileNumberParameter,@alternativeNumberParameter,@emailAddressParameter", personIdParameter, physicalAdressParameter, mobileNumberParameter,alternativeNumberParameter,emailAddressParameter);
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

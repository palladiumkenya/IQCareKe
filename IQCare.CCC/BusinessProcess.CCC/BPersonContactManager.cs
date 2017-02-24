using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccess.Base;
using DataAccess.CCC.Repository;
using DataAccess.Context;
using Entities.Common;
using Interface.CCC;
using System.Text;

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

            SqlParameter physicalAdressParameter =new SqlParameter("physicalAddressParameter",SqlDbType.VarBinary);
            physicalAdressParameter.Value = Encoding.ASCII.GetBytes(personContact.PhysicalAddress);

            SqlParameter mobileNumberParameter =new SqlParameter("mobileNumberParameter",SqlDbType.VarBinary);
            mobileNumberParameter.Value = Encoding.ASCII.GetBytes(personContact.MobileNumber);

            SqlParameter alternativeNumberParameter = new SqlParameter("alternativeNumberParameter", SqlDbType.VarBinary);
            alternativeNumberParameter.Value = Encoding.ASCII.GetBytes(personContact.AlternativeNumber);

            SqlParameter emailAddressParameter = new SqlParameter("emailAddressParameter", SqlDbType.VarBinary);
            emailAddressParameter.Value = Encoding.ASCII.GetBytes(personContact.EmailAddress);

            SqlParameter userId = new SqlParameter("UserId", SqlDbType.Int);
            userId.Value = personContact.CreatedBy;

            _unitOfWork.PersonContactRepository.ExecuteProcedure("exec PersonContact_Insert @personIdParameter,@physicalAddressParameter,@mobileNumberParameter,@alternativeNumberParameter,@emailAddressParameter,@UserId", personIdParameter, physicalAdressParameter, mobileNumberParameter,alternativeNumberParameter,emailAddressParameter,userId);
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
            SqlParameter personIdParameter = new SqlParameter("personIdParameter", SqlDbType.Int);
            personIdParameter.Value = p.PersonId;

            SqlParameter physicalAdressParameter = new SqlParameter("physicalAddressParameter", SqlDbType.VarBinary);
            physicalAdressParameter.Value = Encoding.ASCII.GetBytes(p.PhysicalAddress);

            SqlParameter mobileNumberParameter = new SqlParameter("mobileNumberParameter", SqlDbType.VarBinary);
            mobileNumberParameter.Value = Encoding.ASCII.GetBytes(p.MobileNumber);

            SqlParameter alternativeNumberParameter = new SqlParameter("alternativeNumberParameter", SqlDbType.VarBinary);
            alternativeNumberParameter.Value = Encoding.ASCII.GetBytes(p.AlternativeNumber);

            SqlParameter emailAddressParameter = new SqlParameter("emailAddressParameter", SqlDbType.VarBinary);
            emailAddressParameter.Value = Encoding.ASCII.GetBytes(p.EmailAddress);

            SqlParameter Id = new SqlParameter("Id", SqlDbType.Int);
            Id.Value = p.Id;

            _unitOfWork.PersonContactRepository.ExecuteProcedure("exec PersonContact_Update @personIdParameter,@physicalAddressParameter,@mobileNumberParameter,@alternativeNumberParameter,@emailAddressParameter, @Id", personIdParameter, physicalAdressParameter, mobileNumberParameter, alternativeNumberParameter, emailAddressParameter, Id);
            //_unitOfWork.PersonContactRepository.Add(p);
            return _result = _unitOfWork.Complete();

            //_unitOfWork.PersonContactRepository.Update(p);
            //return _result = _unitOfWork.Complete();
        }
    }
}

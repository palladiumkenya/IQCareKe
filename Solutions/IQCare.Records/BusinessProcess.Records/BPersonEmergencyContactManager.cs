using DataAccess.Base;
using DataAccess.Records;
using DataAccess.Records.Context;
using Entities.Records;
using Interface.Records;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessProcess.Records
{
  public  class BPersonEmergencyContactManager : ProcessBase, IPersonEmergencyContactManager
    {
        private int _result;
        private PersonEmergencyContact _personEmergencyContact;
        public int AddPersonEmergencyContact(PersonEmergencyContact pmc) 
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new RecordContext()))
            {
                SqlParameter personIdParameter = new SqlParameter("personIdParameter", SqlDbType.Int);
                personIdParameter.Value = pmc.PersonId;
                SqlParameter EmergencyContactPersonIdParameter = new SqlParameter("EmergencyContactPersonIdParameter", SqlDbType.Int);
                EmergencyContactPersonIdParameter.Value = pmc.EmergencyContactPersonId;
                SqlParameter MobileContactParameter = new SqlParameter("MobileContactParameter", SqlDbType.VarBinary);
                MobileContactParameter.Value = Encoding.ASCII.GetBytes(pmc.MobileContact);
                SqlParameter userId = new SqlParameter("UserId", SqlDbType.Int);
                userId.Value = pmc.CreatedBy;
                _unitOfWork.PersonEmergencyContactRepository.ExecuteProcedure("exec PersonEmergencyContact_Insert @personIdParameter,EmergencyContactPersonIdParameter,@MobileContactParameter,@UserId",
                    personIdParameter, EmergencyContactPersonIdParameter, MobileContactParameter, userId);

                _result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return _result;



            }
        }

        public int UpdatePersonEmergencyContact(PersonEmergencyContact pmc)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new RecordContext()))
            {
                SqlParameter personIdParameter = new SqlParameter("personIdParameter", SqlDbType.Int);
                personIdParameter.Value = pmc.PersonId;
                SqlParameter EmergencyContactPersonIdParameter = new SqlParameter("EmergencyContactPersonIdParameter", SqlDbType.Int);
                EmergencyContactPersonIdParameter.Value = pmc.EmergencyContactPersonId;
                SqlParameter MobileContactParameter = new SqlParameter("MobileContactParameter", SqlDbType.VarBinary);
                MobileContactParameter.Value = Encoding.ASCII.GetBytes(pmc.MobileContact);
                SqlParameter userId = new SqlParameter("UserId", SqlDbType.Int);
                userId.Value = pmc.CreatedBy;
                _unitOfWork.PersonEmergencyContactRepository.ExecuteProcedure("exec PersonEmergencyContact_Update @personIdParameter,EmergencyContactPersonIdParameter,@MobileContactParameter,@UserId",
                    personIdParameter, EmergencyContactPersonIdParameter, MobileContactParameter, userId);

                _result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return _result;



            }
        }



        public int DeletePersonEmergencyContact(int id)
        {
            using (UnitOfWork _unitofwork = new UnitOfWork(new RecordContext()))
            {
                _personEmergencyContact = _unitofwork.PersonEmergencyContactRepository.GetById(id);
                _unitofwork.PersonEmergencyContactRepository.Remove(_personEmergencyContact);
                _result = _unitofwork.Complete();
                _unitofwork.Dispose();
                return _result;
            }
        }
        public List<PersonEmergencyContact> GetAllEmergencyContact(int personId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new RecordContext()))
            {
                List<PersonEmergencyContact> personEmergencyContacts = _unitOfWork.PersonEmergencyContactRepository
                    .FindBy(x => x.PersonId == personId & x.DeleteFlag == false).OrderByDescending(x => x.Id).ToList();
                _unitOfWork.Dispose();
                return personEmergencyContacts;
            }
        }

        public List<PersonEmergencyContact> GetCurrentEmergencyContact(int personId)
        {
            using(UnitOfWork _unitOfWork = new UnitOfWork(new RecordContext()))
            {
                List<PersonEmergencyContact> personEmergencyContacts = _unitOfWork.PersonEmergencyContactRepository
                    .FindBy(x => x.PersonId == personId & x.DeleteFlag == false).OrderByDescending(x => x.Id).Take(1).ToList();
                _unitOfWork.Dispose();
                return personEmergencyContacts;
            }

        }

    }
}

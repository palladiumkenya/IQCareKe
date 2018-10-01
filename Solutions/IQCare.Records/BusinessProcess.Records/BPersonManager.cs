using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Entities.Common;
using System.Data;
using DataAccess.Context;
using DataAccess.Records;
using Interface.Records;
namespace BusinessProcess.Records

{
    public class BPersonManager:ProcessBase,IPersonManager
    {


        private int result;


        public int AddPerson(Person  person)
        {
            int personId = -1;
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            ClsUtility.AddExtendedParameters("@FirstName", SqlDbType.VarChar, person.FirstName);
            ClsUtility.AddExtendedParameters("@MidName", SqlDbType.VarChar, person.MidName);
            ClsUtility.AddExtendedParameters("@LastName", SqlDbType.VarChar, person.LastName);
            ClsUtility.AddExtendedParameters("@Sex", SqlDbType.Int, person.Sex);

            if (person.DateOfBirth.HasValue)
            {
                ClsUtility.AddExtendedParameters("@DateOfBirth", SqlDbType.DateTime, person.DateOfBirth);
            }

            if (person.DobPrecision.HasValue)
            {
                ClsUtility.AddExtendedParameters("@DobPrecision", SqlDbType.Bit, person.DobPrecision);
            }
            //ClsUtility.AddExtendedParameters("@NationalId", SqlDbType.VarChar, person.NationalId);
            ClsUtility.AddExtendedParameters("@UserId", SqlDbType.Int, person.CreatedBy);

            DataTable dt = (DataTable)obj.ReturnObject(ClsUtility.theParams, "Person_Insert", ClsUtility.ObjectEnum.DataTable);
            if (dt != null && dt.Rows.Count > 0)
            {
                personId = Convert.ToInt32(dt.Rows[0]["PersonId"]);
            }
            // p.Add(person);
            obj = null;
            return personId;
        }

      

       

        public Person GetPerson(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext()))
            {
                var personInfo = _unitOfWork.PersonRepository.GetById(id);
                _unitOfWork.Dispose();
                return personInfo;
            }

        }

        public List<Person> GetPersonAll()
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext()))
            {
                var mylist = _unitOfWork.PersonRepository.GetAll().ToList();
                _unitOfWork.Dispose();
                return mylist;
            }

        }


        public int UpdatePerson(Person person, int id)
        {

            int personId = -1;
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            ClsUtility.AddExtendedParameters("@FirstName", SqlDbType.VarChar, person.FirstName);
            ClsUtility.AddExtendedParameters("@MidName", SqlDbType.VarChar, person.MidName);
            ClsUtility.AddExtendedParameters("@LastName", SqlDbType.VarChar, person.LastName);
            ClsUtility.AddExtendedParameters("@Sex", SqlDbType.Int, person.Sex);
            //ClsUtility.AddExtendedParameters("@NationalId", SqlDbType.VarChar, person.NationalId);
            if (person.DateOfBirth.HasValue)
            {
                ClsUtility.AddExtendedParameters("@DateOfBirth", SqlDbType.DateTime, person.DateOfBirth);
            }

            ClsUtility.AddExtendedParameters("@Id", SqlDbType.Int, id);

            DataTable dt = (DataTable)obj.ReturnObject(ClsUtility.theParams, "Person_Update", ClsUtility.ObjectEnum.DataTable);
            if (dt != null && dt.Rows.Count > 0)
            {
                personId = Convert.ToInt32(dt.Rows[0]["PersonId"]);
            }
            obj = null;
            return personId;

        }

        public void DeletePerson(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new PersonContext()))
            {
                Person personInfo = _unitOfWork.PersonRepository.GetById(id);
                _unitOfWork.PersonRepository.Remove(personInfo);
                result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
            }
        }
    }
}


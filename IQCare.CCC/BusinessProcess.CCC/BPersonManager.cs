using System.Collections.Generic;
using DataAccess.Base;
using DataAccess.CCC.Repository;
using DataAccess.Context;
using Entities.Common;
using Interface.CCC;
using System.Linq;
using System.Data;
using DataAccess.Common;
using DataAccess.Entity;
using System;

namespace BusinessProcess.CCC
{

    public class BPersonManager:ProcessBase, IPersonManager
    {
        private readonly UnitOfWork _unitOfWork=new UnitOfWork(new PersonContext());

       public int AddPerson(Person person)
       {
            //  _unitOfWork.PersonRepository.Add(person);

           int personId = -1;
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            ClsUtility.AddExtendedParameters("@FirstName", SqlDbType.VarChar, person.FirstName);
            ClsUtility.AddExtendedParameters("@MidName", SqlDbType.VarChar, person.MidName);
            ClsUtility.AddExtendedParameters("@LastName", SqlDbType.VarChar, person.LastName);
            ClsUtility.AddExtendedParameters("@Sex", SqlDbType.Int, person.Sex);
            ClsUtility.AddExtendedParameters("@NationalId", SqlDbType.VarChar, person.NationalId);
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
           var personInfo=  _unitOfWork.PersonRepository.GetById(id);
           return personInfo;
        }

        public List<Person> GetPersonAll()
        {
           var mylist= _unitOfWork.PersonRepository.GetAll();
            return mylist.ToList();
        }


        public int UpdatePerson(Person person,int id)
        {

            int personId = -1;
            ClsObject obj = new ClsObject();
            ClsUtility.Init_Hashtable();
            ClsUtility.AddExtendedParameters("@FirstName", SqlDbType.VarChar, person.FirstName);
            ClsUtility.AddExtendedParameters("@MidName", SqlDbType.VarChar, person.MidName);
            ClsUtility.AddExtendedParameters("@LastName", SqlDbType.VarChar, person.LastName);
            ClsUtility.AddExtendedParameters("@Sex", SqlDbType.Int, person.Sex);
            ClsUtility.AddExtendedParameters("@NationalId", SqlDbType.VarChar, person.NationalId);
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
            Person personInfo = _unitOfWork.PersonRepository.GetById(id);
            _unitOfWork.PersonRepository.Remove(personInfo);
            _unitOfWork.Complete();
        }
    }
}

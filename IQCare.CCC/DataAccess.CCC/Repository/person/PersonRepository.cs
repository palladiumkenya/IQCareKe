using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccess.CCC.Interface.person;
using DataAccess.Context;
using Entities.Common;

namespace DataAccess.CCC.Repository.person
{
    public class PersonRepository:BaseRepository<Person>,IPersonRepository
    {
        private readonly PersonContext _context;

        public PersonRepository() : this(new PersonContext())
        {

        }   

        public override void Add(Person entity)
        {

            SqlParameter firstName = new SqlParameter("FirstName", DbType.String)
            {
                Value = entity.FirstName ?? (object) DBNull.Value
            };

            SqlParameter pMiddleName = new SqlParameter("@MidName", DbType.String)
            {
                Value = entity.MidName ?? (object) DBNull.Value
            };

            SqlParameter pLastName = new SqlParameter("@LastName", DbType.String)
            {
                Value = entity.LastName ?? (object) DBNull.Value
            };

            SqlParameter pSex = new SqlParameter("@Sex", DbType.String) {Value = entity.Sex};

            SqlParameter pNationalId = new SqlParameter("@NationalId", DbType.String)
            {
                Value = entity.NationalId ?? (object) DBNull.Value
            };

            SqlParameter pUserId = new SqlParameter("@UserId", DbType.String) {Value = entity.CreatedBy};



            //DbParameter pFirstName = new EntityParameter("FirstName", DbType.String);
            //pFirstName.Value = entity.FirstName;

            //DbParameter pLastName = new EntityParameter("LastName", DbType.String);
            //pLastName.Value = entity.LastName;

            //DbParameter pMidName = new EntityParameter("MidName", DbType.String);
            //pMidName.Value = entity.MidName;

            //DbParameter pSex = new EntityParameter("Sex", DbType.Int32);
            //pSex.Value = entity.Sex;

            //DbParameter pNational = new EntityParameter("NationalId", DbType.String);
            //pNational.Value = entity.NationalId;

            //DbParameter pUserId = new EntityParameter("UserId", DbType.Int32);
            //pUserId.Value = entity.CreatedBy;

            //add more parameter
           base.ExecuteProcedure("exec Person_Insert @FirstName,@MidName,@LastName,@Sex,@NationalId,@UserId", firstName,pMiddleName,pLastName,pSex,pNationalId,pUserId);
        
        }

        public override IEnumerable<Person> GetAll()
        {
            return base.GetAll();
        }

        public PersonRepository(PersonContext context) : base(context)
        {
            _context = context;
        }

    }
}

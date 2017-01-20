using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
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
           DbParameter pFirstName = new EntityParameter("FirstName",DbType.String);
            pFirstName.Value = entity.FirstName;

            DbParameter pLastName = new EntityParameter("LastName", DbType.String);
            pLastName.Value = entity.LastName;

            DbParameter pMidName = new EntityParameter("MidName", DbType.String);
            pMidName.Value = entity.MidName;

            DbParameter pSex = new EntityParameter("Sex", DbType.Int32);
            pSex.Value = entity.Sex;

            DbParameter pNational = new EntityParameter("NationalId", DbType.String);
            pNational.Value = entity.NationalId;

            DbParameter pUserId = new EntityParameter("UserId", DbType.Int32);
            pUserId.Value = entity.CreatedBy;

            //add more parameter

            base.ExecuteProcedure("Patient_Insert",pFirstName,pLastName,pMidName,pSex,pNational,pUserId);
        
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

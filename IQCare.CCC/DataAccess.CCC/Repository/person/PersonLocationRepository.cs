using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.CCC.Interface.person;
using DataAccess.Context;
using Entities.Common;

namespace DataAccess.CCC.Repository.person
{
   public class PersonLocationRepository:BaseRepository<PersonLocation>,IPersonLocationRepository
   {
       private readonly PersonContext _context;

       public PersonLocationRepository() : this(new PersonContext())
       {
           
       }

       public PersonLocationRepository(PersonContext context) : base(context)
       {
           _context = context;
       }
    }
}

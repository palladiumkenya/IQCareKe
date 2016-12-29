using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data.Repository;
using PersonManagement.Core.Interfaces;
using PersonManagement.Core.Model;

namespace PersonManagement.Data.Repository
{
   public class PersonRelationshipRepository:BaseRepository<PersonRelationship>,IPersonRelationshipRepository
   {
       private readonly PersonContext _context;

       public PersonRelationshipRepository() : this(new PersonContext())
       {
       }

       public PersonRelationshipRepository(PersonContext context) : base(context)
       {
           _context = context;
       }
    }
}

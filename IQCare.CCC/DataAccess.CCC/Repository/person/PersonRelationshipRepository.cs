using DataAccess.CCC.Interface;
using DataAccess.CCC.Interface.person;
using DataAccess.Context;
using Entities.Common;

namespace DataAccess.CCC.Repository.person
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

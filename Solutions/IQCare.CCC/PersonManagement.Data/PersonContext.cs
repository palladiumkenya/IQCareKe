using System.Data.Entity;
using Common.Data;
using PersonManagement.Core.Model;

namespace PersonManagement.Data
{
    public class PersonContext:BaseContext
    {
        public PersonContext():base()
        {
            
        }
        public PersonContext(string connection) : base(connection)
        {

        }

       public DbSet<PersonContact> PersonContacts { get; set; }
        public DbSet<PersonLocation> PersonLocations { get; set; }
        public DbSet<PersonRelationship> PersonRelationships { get; set; }
    }
}

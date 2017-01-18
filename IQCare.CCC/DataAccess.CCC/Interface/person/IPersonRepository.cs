using DataAccess.Context;
using Entities.Common;

namespace DataAccess.CCC.Interface.person
{
    public interface IPersonRepository :IRepository<Person>
    {
        void Update(Person p);

    }
}

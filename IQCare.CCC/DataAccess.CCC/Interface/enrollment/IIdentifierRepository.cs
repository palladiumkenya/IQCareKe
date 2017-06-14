using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Context;
using Entities.CCC.Enrollment;

namespace DataAccess.CCC.Interface.enrollment
{
    public interface IIdentifierRepository : IRepository<Identifier>
    {
    }
}

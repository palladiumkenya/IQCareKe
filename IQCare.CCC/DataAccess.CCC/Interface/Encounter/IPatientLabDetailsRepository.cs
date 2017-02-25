using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Context;
using Entities.CCC.Encounter;

namespace DataAccess.CCC.Interface.Encounter
{
    interface IPatientLabDetailsRepository : IRepository<LabDetailsEntity>
    {
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Context;
using Entities.CCC.Triage;

namespace DataAccess.CCC.Interface.Encounter
{
    public interface IPatientLabDetailsRepository : IRepository<LabDetailsEntity>
    {

    }
}

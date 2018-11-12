using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.CCC.Adherence;
using DataAccess.Context;

namespace DataAccess.CCC.Interface.Adherence
{
    public interface IAdherenceScreeningRepository : IRepository<AdherenceScreening>
    {
        List<AdherenceScreening> getAdherenceScreening(int personId, int patientMasterVisitId);
        int updateAdherenceScreening(AdherenceScreening _AS);
    }
}

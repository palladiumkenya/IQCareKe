using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.CCC.Adherence;
using DataAccess.Context;

namespace DataAccess.CCC.Interface.Adherence
{
    public interface IAdherenceRepository: IRepository<HIVStatus>
    {
        List<HIVStatus> getHIVStatus(int personId, int patientMasterVisitId);
        int updateHIVStatus(HIVStatus SH);
    }
}

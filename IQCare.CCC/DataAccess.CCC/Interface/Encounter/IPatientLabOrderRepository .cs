using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Context;
using Entities.CCC.Encounter;

namespace DataAccess.CCC.Interface.Encounter
{
    public interface IPatientLabOrderRepository : IRepository<LabOrderEntity>
    {
        //List<LabOrderEntity> GetByPatientId(int facilityId);
        List<LabOrderEntity> GetVlPendingCount(int facilityId);
        List<LabOrderEntity> GetVlCompleteCount(int facilityId);

    }
}

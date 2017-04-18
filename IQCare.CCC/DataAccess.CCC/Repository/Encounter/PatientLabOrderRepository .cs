using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Encounter;
using DataAccess.Context;
using Entities.CCC.Encounter;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.CCC.Repository.Encounter
{
    public class PatientLabOrderRepository : BaseRepository<LabOrderEntity>, IPatientLabOrderRepository
    {
        private GreencardContext _context;

        public PatientLabOrderRepository() : this(new GreencardContext())
        {

        }

        public PatientLabOrderRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
        public List<LabOrderEntity> GetVlPendingCount(int facilityId)
        {
            var pending = "Pending";
            IPatientLabOrderRepository laborderRepository = new PatientLabOrderRepository();
            List<LabOrderEntity> pendingVLCount = laborderRepository.FindBy(p => p.LocationId == facilityId &
                                                                                 p.OrderStatus== pending &
                                                                                 p.LabTestId == 3).ToList();
            return pendingVLCount;
        }
        public List<LabOrderEntity> GetVlCompleteCount(int facilityId)
        {
            var complete = "Complete";
            IPatientLabOrderRepository laborderRepository = new PatientLabOrderRepository();
            List<LabOrderEntity> completeVLCount = laborderRepository.FindBy(p => p.LocationId == facilityId &
                                                                                 p.OrderStatus == complete &
                                                                                 p.LabTestId == 3).ToList();
            return completeVLCount;
        }
    }
}


//namespace DataAccess.CCC.Repository.Encounter
//{
//    public class PatientLabOrderRepository : BaseRepository<LabOrderEntity>, IPatientLabOrderRepository
//    {
//        private readonly GreencardContext _context;

//        public PatientLabOrderRepository() :base(new GreencardContext())
//        {

//        }

//        public PatientLabOrderRepository(GreencardContext context) : base(context)
//        {
//            _context = context;
//        }


//    }
//}

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
            List<LabOrderEntity> facilityVlPendingOrders = laborderRepository.FindBy(x => x.LocationId == facilityId &
                                                                                     x.LabTestId == 3 &
                                                                                     x.OrderStatus == pending)
                                                                                     .ToList();
            return facilityVlPendingOrders;
        }

        public List<LabOrderEntity> GetVlCompleteCount(int facilityId)
        {
            var complete = "Complete";
            IPatientLabOrderRepository laborderRepository = new PatientLabOrderRepository();
            List<LabOrderEntity> facilityVlPendingOrders = laborderRepository.FindBy(x => x.LocationId == facilityId &
                                                                                     x.LabTestId == 3 &
                                                                                     x.OrderStatus == complete)
                                                                                     .ToList();
            return facilityVlPendingOrders;
        }

    }
}




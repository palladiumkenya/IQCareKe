using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Encounter;
using DataAccess.Context;
using Entities.CCC.Encounter;

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

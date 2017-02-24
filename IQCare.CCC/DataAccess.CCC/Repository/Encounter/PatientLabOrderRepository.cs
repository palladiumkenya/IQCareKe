using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Encounter;
using DataAccess.Context;
using Entities.CCC.Encounter;

namespace DataAccess.CCC.Repository.Encounter
{
    class PatientLabOrderRepository : BaseRepository<LabOrderEntity>, IPatientLabOrderRepository
    {
    }
}


namespace DataAccess.CCC.Repository.visit
{
    public class PatientLabOrderRepository : BaseRepository<LabOrderEntity>, IPatientLabOrderRepository
    {
        private readonly GreencardContext _context;

        public PatientLabOrderRepository() : this(new GreencardContext())
        {

        }

        public PatientLabOrderRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }


    }
}

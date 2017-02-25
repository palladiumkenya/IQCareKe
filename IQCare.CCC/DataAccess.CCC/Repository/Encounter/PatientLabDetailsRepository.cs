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
    public class PatientLabdetailsRepository : BaseRepository<LabDetailsEntity>, IPatientLabDetailsRepository
    {
    }
}


namespace DataAccess.CCC.Repository.visit
{
    public class PatientLabdetailsRepository : BaseRepository<LabDetailsEntity>, IPatientLabDetailsRepository
    {
        private readonly GreencardContext _context;

        public PatientLabdetailsRepository() : this(new GreencardContext())
        {

        }

        public PatientLabdetailsRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }


    }
}

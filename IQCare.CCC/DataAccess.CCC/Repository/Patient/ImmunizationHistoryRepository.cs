using System;
using System.Linq;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Patient;
using DataAccess.Context;
using Entities.CCC.Neonatal;
using System.Collections.Generic;

namespace DataAccess.CCC.Repository.Patient
{
    public class ImmunizationHistoryRepository:BaseRepository<PatientImmunizationHistory>, IImmunizationHistoryRepository
    {
        private readonly GreencardContext _context;
        public ImmunizationHistoryRepository() : this(new GreencardContext())
        {

        }
        public ImmunizationHistoryRepository(GreencardContext context):base(context) 
        {
            _context = context;
        }
        public List<PatientImmunizationHistory> getPatientImmunization(int patientId)
        {
            IImmunizationHistoryRepository immunizationRepository = new ImmunizationHistoryRepository();
            var immunizationList = immunizationRepository.GetAll().Where(x => x.PatientId == patientId);
            return immunizationList.ToList();
        }
    }
}

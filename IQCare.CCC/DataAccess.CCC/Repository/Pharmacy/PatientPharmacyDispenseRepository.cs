using System.Collections.Generic;
using System.Linq;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Pharmacy;
using DataAccess.Context;
using Entities.CCC.pharmacy;

namespace DataAccess.CCC.Repository.Pharmacy
{
    public class PatientPharmacyDispenseRepository : BaseRepository<PatientPharmacyDispense>, IPatientPharmacyDispenseRepository
    {
        private readonly GreencardContext _context;

        public PatientPharmacyDispenseRepository() : this(new GreencardContext())
        {
        }

        public PatientPharmacyDispenseRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }

        public List<PatientPharmacyDispense> GetByPharmacyOrder(int pharmacyOrderId)
        {
            IPatientPharmacyDispenseRepository patientPharmacyDispenseRepository = new PatientPharmacyDispenseRepository();
            List<PatientPharmacyDispense> pharmacyDispenses = patientPharmacyDispenseRepository.FindBy(p => p.ptn_pharmacy_pk == pharmacyOrderId).ToList();
            return pharmacyDispenses;
        }
    }
}
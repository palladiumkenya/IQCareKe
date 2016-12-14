using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data.Repository;
using PatientManagement.Core.Interfaces;
using PatientManagement.Core.Model;

namespace PatientManagement.Data.Repository
{
    public class PatientTreatmentSupporterRepository :BaseRepository<PatientTreatmentSupporter>,IPatientTreatmentSupporterRepository
    {
        private readonly PatientContext _context;

        public PatientTreatmentSupporterRepository() : this(new PatientContext())
        {

        }

        public PatientTreatmentSupporterRepository(PatientContext context) : base(context)
        {
            _context = context;
        }
    }
}

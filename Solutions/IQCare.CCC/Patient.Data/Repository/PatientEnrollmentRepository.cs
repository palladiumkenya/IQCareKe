using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data;
using Common.Data.Repository;
using PatientManagement.Core.Interfaces;
using PatientManagement.Core.Model;

namespace PatientManagement.Data.Repository
{
    public class PatientEnrollmentRepository :BaseRepository<PatientEnrollment>,IPatientEnrollmentRepository
    {
        private readonly PatientContext _context;
        public PatientEnrollmentRepository() : this(new PatientContext())
        {

        }

        public PatientEnrollmentRepository(PatientContext context) : base(context)
        {
            _context = context;
        }
    }
}

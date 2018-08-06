using DataAccess.Context;
using DataAccess.Records.Context;
using DataAccess.Records.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Records.Repository.Patient
{
    public class PatientRepository : BaseRepository<Entities.Records.Enrollment.PatientEntity>, IPatientRepository
    {
        private   RecordContext _context;

        public PatientRepository() : this(new RecordContext())
        {

        }

        public PatientRepository(RecordContext context) : base(context)
        {
            _context = context;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Base;
using DataAccess.Context;
using Entities.Records;
using Entities.Records.Enrollment;
using DataAccess.Records.Context;
using DataAccess.Records.Interface;

namespace DataAccess.Records.Repository
{
   public class PatientEnrollmentRepository:BaseRepository<PatientEntityEnrollment>,IPatientEnrollmentRepository
    {
        private readonly RecordContext _context;

        public PatientEnrollmentRepository(): this(new RecordContext())
        {

        }

        public PatientEnrollmentRepository (RecordContext context):base(context)
        {
            _context = context;
        }
    }
}

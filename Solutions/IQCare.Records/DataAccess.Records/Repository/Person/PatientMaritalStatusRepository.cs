using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Records;
using DataAccess.Context;
using Entities.PatientCore;
using DataAccess.Records.Interface;

namespace DataAccess.Records.Repository
{
   public class PatientMaritalStatusRepository:BaseRepository<PatientMaritalStatus>,IPatientMaritalStatusRepository
    {
        private readonly PersonContext _context;

        public PatientMaritalStatusRepository():this(new PersonContext())
        { }

        public PatientMaritalStatusRepository(PersonContext context):base(context)
        {
            _context = context;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Records.Context;
using Entities.Records;
using DataAccess.Context;
using DataAccess.Records.Interface;
using System.Data.Entity;

namespace DataAccess.Records.Repository.Visit
{
   public class PatientMasterVisitRepository:BaseRepository<PatientMasterVisit>, IPatientMasterVisitRepository
    {
        private RecordContext _context;

        public PatientMasterVisitRepository():this(new RecordContext())
        {

        }
        public PatientMasterVisitRepository(RecordContext context):base(context)
        {
            _context = context;
        }

        public List<PatientMasterVisit> GetByDate(DateTime date)
        {
            IPatientMasterVisitRepository patientAppointmentRepository = new PatientMasterVisitRepository();
            List<PatientMasterVisit> patientAppointment = patientAppointmentRepository.FindBy(p => DbFunctions.TruncateTime(p.VisitDate) == DbFunctions.TruncateTime(date)).ToList();
            return patientAppointment;
        }

    }
}

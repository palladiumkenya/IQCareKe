using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.visit;
using DataAccess.Context;
using Entities.CCC.Visit;

namespace DataAccess.CCC.Repository.visit
{
    public class PatientMasterVisitRepository : BaseRepository<PatientMasterVisit>, IPatientMasterVisitRepository
    {
        private  GreencardContext _context;

        public PatientMasterVisitRepository() : this(new GreencardContext())
        {

        }

        public PatientMasterVisitRepository(GreencardContext context) : base(context)
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

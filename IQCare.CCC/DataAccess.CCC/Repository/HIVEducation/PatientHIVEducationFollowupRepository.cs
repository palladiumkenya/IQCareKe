using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.HIVEducation;
using DataAccess.Context;
using Entities.CCC.HIVEducation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CCC.Repository.HIVEducation
{
    public class PatientHIVEducationFollowupRepository : BaseRepository<HIVEducationFollowup>, IHIVEducationRepository
    {
        private readonly GreencardContext _context;
        public PatientHIVEducationFollowupRepository() : base(new GreencardContext())
        {

        }
        public PatientHIVEducationFollowupRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
        public List<HIVEducationFollowup> getPatientHIVEducation(int patientId)
        {
            throw new NotImplementedException();
        }

        public List<HIVEducationFollowup> getPatientHIVEducationByTopic(int patientId, int topic)
        {
            throw new NotImplementedException();
        }

        public int updatePatientClinicalNotes(HIVEducationFollowup HEF)
        {
            throw new NotImplementedException();
        }
    }
}

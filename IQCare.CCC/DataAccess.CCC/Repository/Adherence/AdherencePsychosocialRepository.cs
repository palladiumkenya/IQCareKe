using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.CCC.Adherence;
using DataAccess.CCC.Context;
using DataAccess.Context;
using DataAccess.CCC.Interface.Adherence;

namespace DataAccess.CCC.Repository.Adherence
{
    public class AdherencePsychosocialRepository : BaseRepository<PsychosocialCircumstances>, IAdherencePsychosocialRepository
    {
        private readonly GreencardContext _context;
        public AdherencePsychosocialRepository():base(new GreencardContext())
        {

        }
        public AdherencePsychosocialRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
        public List<PsychosocialCircumstances> getPsychosocialCircumstances(int personId, int patientMasterVisitId)
        {
            IAdherencePsychosocialRepository psychosocialRepository = new AdherencePsychosocialRepository();
            var psychosocialList = psychosocialRepository.GetAll().Where(x => x.PatientId == personId && x.PatientMasterVisitId == patientMasterVisitId).OrderBy(x => x.Id);
            return psychosocialList.ToList();
        }
        public int updatePsychosocialCircumstnces(PsychosocialCircumstances PC)
        {
            IAdherencePsychosocialRepository psychosocialRepository = new AdherencePsychosocialRepository();
            psychosocialRepository.updatePsychosocialCircumstnces(PC);
            return PC.Id;
        }
    }
}

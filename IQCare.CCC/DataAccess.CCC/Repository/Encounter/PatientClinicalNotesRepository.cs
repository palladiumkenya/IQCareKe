using System.Collections.Generic;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Encounter;
using DataAccess.Context;
using Entities.CCC.Encounter;
using System.Linq;
using Entities.CCC.Neonatal;

namespace DataAccess.CCC.Repository.Encounter
{
    public class PatientClinicalNotesRepository : BaseRepository<PatientClinicalNotes>, IPatientClinicalNotesRepository
    {
        private readonly GreencardContext _context;
        public PatientClinicalNotesRepository():base(new GreencardContext())
        {

        }
        public PatientClinicalNotesRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
        public List<PatientClinicalNotes> getPatientClinicalNotes(int patientId)
        {
            IPatientClinicalNotesRepository notesRepository = new PatientClinicalNotesRepository();
            var notesList = notesRepository.GetAll().Where(x => x.PatientId == patientId);
            return notesList.ToList();
        }

        public List<PatientClinicalNotes> getPatientClinicalNotesByCategory(int patientId, int categoryId)
        {
            IPatientClinicalNotesRepository notesRepository = new PatientClinicalNotesRepository();
            var notesList = notesRepository.GetAll().Where(x => x.PatientId == patientId & x.NotesCategoryId == categoryId);
            return notesList.ToList();
        }

        public int updatePatientClinicalNotes(PatientClinicalNotes PCN)
        {
            IPatientClinicalNotesRepository notesRepository = new PatientClinicalNotesRepository();
            notesRepository.updatePatientClinicalNotes(PCN);
            return PCN.Id;
        }
    }
}

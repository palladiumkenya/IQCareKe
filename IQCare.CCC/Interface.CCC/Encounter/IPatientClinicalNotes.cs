using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.CCC.Encounter;

namespace Interface.CCC.Encounter
{
    public interface IPatientClinicalNotes
    {
        int AddPatientClinicalNotes(PatientClinicalNotes PCN);
        int UpdatePatientClinicalNotes(PatientClinicalNotes PCN);
        List<PatientClinicalNotes> getPatientClinicalNotes(int patientId);
        List<PatientClinicalNotes> getPatientClinicalNotesByCategory(int patientId, int categoryId);
        int checkPatientNotesifExisting(int patientId, int categoryId);
    }
}

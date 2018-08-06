using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Context;
using Entities.CCC.Encounter;
namespace DataAccess.CCC.Interface.Encounter
{
    public interface IPatientClinicalNotesRepository : IRepository<PatientClinicalNotes>
    {
        List<PatientClinicalNotes> getPatientClinicalNotes(int patientId);
        List<PatientClinicalNotes> getPatientClinicalNotesByCategory(int patientId, int categoryId);
        int updatePatientClinicalNotes(PatientClinicalNotes PCN);
    }
}

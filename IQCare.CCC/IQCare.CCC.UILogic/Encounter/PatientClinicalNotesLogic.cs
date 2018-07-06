using System;
using System.Collections.Generic;
using Interface.CCC.Encounter;
using Application.Presentation;
using Entities.CCC.Encounter;

namespace IQCare.CCC.UILogic.Encounter
{
    public class PatientClinicalNotesLogic
    {
        private IPatientClinicalNotes _patientNotes = (IPatientClinicalNotes)ObjectFactory.CreateInstance("BusinessProcess.CCC.Encounters.BPatientClinicalNotes, BusinessProcess.CCC");
        public int addPatientClinicalNotes(int patientId, int patientMasterVisitId, int serviceAreaId, int notesCategoryId, string clinicalNotes, int userId)
        {
            try
            {
                int notesId = _patientNotes.checkPatientNotesifExisting(patientId, notesCategoryId);
                if (notesId > 0)
                {
                    var PCN = new PatientClinicalNotes()
                    {
                        PatientId = patientId,
                        PatientMasterVisitId = patientMasterVisitId,
                        ServiceAreaId = serviceAreaId,
                        ClinicalNotes = clinicalNotes,
                        CreatedBy = userId,
                        //VersionStamp = DateTime.Now,
                        NotesCategoryId = notesCategoryId,
                        Id = notesId
                    };
                    return _patientNotes.UpdatePatientClinicalNotes(PCN);
                }
                else
                {
                    var PCN = new PatientClinicalNotes()
                    {
                        PatientId = patientId,
                        PatientMasterVisitId = patientMasterVisitId,
                        ServiceAreaId = serviceAreaId,
                        ClinicalNotes = clinicalNotes,
                        CreatedBy = userId,
                        //VersionStamp = DateTime.UtcNow,
                        NotesCategoryId = notesCategoryId
                    };
                    return _patientNotes.AddPatientClinicalNotes(PCN);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<PatientClinicalNotes> getPatientClinicalNotesById(int PatientID, int PatientMasterVisitID)
        {
            List<PatientClinicalNotes> notesList = new List<PatientClinicalNotes>();
            try
            {
                notesList = _patientNotes.getPatientClinicalNotesByCategory(PatientID, PatientMasterVisitID);
            }
            catch (Exception)
            {
                throw;
            }
            return notesList;
        }
    }
}

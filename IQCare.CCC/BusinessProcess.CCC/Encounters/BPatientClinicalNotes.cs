using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Encounter;
using Interface.CCC.Encounter;

namespace BusinessProcess.CCC.Encounters
{
    public class BPatientClinicalNotes: ProcessBase, IPatientClinicalNotes
    {
        private int result;
        public int AddPatientClinicalNotes(PatientClinicalNotes PCN)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientClinicalNotesRepository.Add(PCN);
                unitOfWork.Complete();
                unitOfWork.Dispose();
                return PCN.Id;
            }
        }

        public int UpdatePatientClinicalNotes(PatientClinicalNotes PCN)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientClinicalNotesRepository.Update(PCN);
                result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return result;
            }
        }
        public List<PatientClinicalNotes> getPatientClinicalNotes(int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var clinicalNotes = unitOfWork.PatientClinicalNotesRepository.getPatientClinicalNotes(patientId);
                unitOfWork.Dispose();
                return clinicalNotes.ToList();
            }
        }
        public List<PatientClinicalNotes> getPatientClinicalNotesByCategory(int patientId, int categoryId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var clinicalNotes = unitOfWork.PatientClinicalNotesRepository.getPatientClinicalNotesByCategory(patientId, categoryId);
                unitOfWork.Dispose();
                return clinicalNotes.ToList();
            }
        }
        public int checkPatientNotesifExisting(int patientId, int categoryId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var PCN = unitOfWork.PatientClinicalNotesRepository.FindBy(x => x.PatientId == patientId & x.NotesCategoryId == categoryId)
                      .Select(x => x.Id)
                      .FirstOrDefault();
                unitOfWork.Dispose();
                return Convert.ToInt32(PCN);
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.CCC.Encounter;
using Interface.CCC.Encounter;
using Application.Presentation;
using Entities.CCC.Neonatal;

namespace IQCare.CCC.UILogic.Encounter
{
    public class NeonatalHistoryLogic
    {
        INeonatalHistory neonatatalHtx = (INeonatalHistory)ObjectFactory.CreateInstance("BusinessProcess.CCC.Encounters.BNeonatalHistory, BusinessProcess.CCC");
        public List<PatientNeonatalHistory> getNeonatalNotes(int PatientId, int PatientMasterVisitId)
        {
            List<PatientNeonatalHistory> notesList = new List<PatientNeonatalHistory>();
            try
            {
                notesList = neonatatalHtx.getNeonatalNotes(PatientId, PatientMasterVisitId);
            }
            catch(Exception)
            {
                throw;
            }
            return notesList;
        }

        public int updateNeonatalNotes(int PatientId, int PatientMasterVisitId, string notes,int recordNeonatalHistory, int notesid)
        {
            PatientNeonatalHistory nNotes = new PatientNeonatalHistory()
            {
                PatientId = PatientId,
                PatientMasterVisitId = PatientMasterVisitId,
                NeonatalHistoryNotes = notes,
                RecordNeonatalHistory = recordNeonatalHistory,
                Id = notesid
            };
            int neonatalNotesResult = 0;
            neonatalNotesResult = neonatatalHtx.updateNeonatalNotes(nNotes);
            return neonatalNotesResult;
        }

        public List<PatientMilestone> getPatientMilestones(int patientId)
        {
            List<PatientMilestone> neonatalList = new List<PatientMilestone>();
            try
            {
                neonatalList = neonatatalHtx.getPatientMilestones(patientId);
                //dailyRoutineList = _adherence.getDailyRoutine(patientId, patientMasterVisitId);
            }
            catch
            {
                throw;
            }
            return neonatalList;
        }
        public List<PatientMilestone> getMilestoneAssessed(int milestoneAssessed)
        {
            List<PatientMilestone> milestoneList = new List<PatientMilestone>();
            try
            {
                milestoneList = neonatatalHtx.getMilestoneAssessed(milestoneAssessed);
            }
            catch
            {
                throw;
            }
            return milestoneList;
        }
        public void DeleteMilestone(int Id)
        {
            neonatatalHtx.DeleteMilestone(Id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.CCC.Encounter;
using DataAccess.Base;
using DataAccess.CCC.Repository;
using Interface.CCC.Encounter;
using DataAccess.CCC.Context;
using Entities.CCC.Neonatal;

namespace BusinessProcess.CCC.Encounters
{
    public class BNeonatalHistory: ProcessBase, INeonatalHistory
    {
        private int result;
        public List<PatientNeonatalHistory> getNeonatalNotes(int PatientId, int PatientMasterVisitId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var neonatalNotes = unitOfWork.NeonatalRepository.getNeonatalNotes(PatientId, PatientMasterVisitId);
                unitOfWork.Dispose();
                return neonatalNotes.ToList();
            }
        }

        public int updateNeonatalNotes(PatientNeonatalHistory nn)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.NeonatalRepository.Update(nn);
                result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return result;
            }
        }
        public List<PatientMilestone> getPatientMilestones(int patientId)
        {
            using (UnitOfWork unitofwork = new UnitOfWork(new GreencardContext()))
            {
                var patientMilestones = unitofwork.MilestonesRepository.getPatientMilestones(patientId).OrderByDescending(x => x.Id);
                unitofwork.Dispose();
                return patientMilestones.ToList();
            }
        }
        public List<PatientMilestone> getMilestoneAssessed(int milestoneAssessed)
        {
            using (UnitOfWork unitofwork = new UnitOfWork(new GreencardContext()))
            {
                var patientMilestones = unitofwork.MilestonesRepository.getMilestoneAssessed(milestoneAssessed);
                unitofwork.Dispose();
                return patientMilestones.ToList();
            }
        }
        public List<PatientImmunizationHistory> getPatientImmunization(int patientId)
        {
            using (UnitOfWork unitofwork = new UnitOfWork(new GreencardContext()))
            {
                var patientImmunization = unitofwork.ImmunizationHistoryRepository.getPatientImmunization(patientId).OrderByDescending(x => x.Id);
                unitofwork.Dispose();
                return patientImmunization.ToList();
            }
        }
        public void DeleteMilestone(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                PatientMilestone patientMilestone = _unitOfWork.MilestonesRepository.GetById(id);
                _unitOfWork.MilestonesRepository.Remove(patientMilestone);
                _unitOfWork.Complete();
                _unitOfWork.Dispose();
            }

        }
    }
}

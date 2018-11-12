using DataAccess.Base;
using Entities.CCC.Neonatal;
using Interface.CCC;
using System.Collections.Generic;
using System.Linq;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using DataAccess.CCC.Repository.Encounter;
using DataAccess.Entity;
using DataAccess.Common;
using System.Data;
using System;
using Entities.CCC.Encounter;

namespace BusinessProcess.CCC
{
    public class BPatientNeonatal: ProcessBase, IPatientNeonatal
    {
        private int _result;
        public int AddPatientNeonatal(PatientMilestone p)

        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {

                unitOfWork.PatientNeonatalRepository.Add(p);
                _result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return p.Id;
            }

        }

        public int AddImmunizationHistory(PatientImmunizationHistory I)
        {
            using (UnitOfWork unitofwork = new UnitOfWork(new GreencardContext()))
            {
                unitofwork.ImmunizationHistoryRepository.Add(I);
                _result = unitofwork.Complete();
                unitofwork.Dispose();
                return I.Id;
            }
        }

        public int AddPatientNeonatalHistory(PatientNeonatalHistory n)
        {
            using (UnitOfWork unitofwork = new UnitOfWork(new GreencardContext()))
            {
                unitofwork.NeonatalRepository.Add(n);
                _result = unitofwork.Complete();
                unitofwork.Dispose();
                return n.Id;
            }
        }
        public List<PatientMilestone> getPatientMilestones(int patientId)
        {
            using (UnitOfWork unitofwork = new UnitOfWork(new GreencardContext()))
            {
                var patientMilestones = unitofwork.MilestonesRepository.getPatientMilestones(patientId);
                unitofwork.Dispose();
                return patientMilestones.ToList();
            }
        }
        public void DeleteImmunization(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                PatientImmunizationHistory immunization = _unitOfWork.ImmunizationHistoryRepository.GetById(id);
                _unitOfWork.ImmunizationHistoryRepository.Remove(immunization);
                _unitOfWork.Complete();
                _unitOfWork.Dispose();
            }

        }
    }
}

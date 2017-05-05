using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Visit;
using Interface.CCC.Visit;
using DataAccess.Base;

namespace BusinessProcess.CCC.visit
{
    public class BPatientmasterVisit : ProcessBase, IPatientMasterVisitManager
    {
       // private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        internal int Result;

        public int AddPatientmasterVisit(PatientMasterVisit patientMasterVisit)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientMasterVisitRepository.Add(patientMasterVisit);
                Result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return patientMasterVisit.Id;
            }

        }

        public int DeletePatientVisit(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var patientmasterVisit = unitOfWork.PatientMasterVisitRepository.GetById(id);
                unitOfWork.PatientMasterVisitRepository.Remove(patientmasterVisit);
                 Result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return Result;
            }

        }

        public List<PatientMasterVisit> GetPatientCurrentVisit(int patientId, DateTime visitDate)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                List<PatientMasterVisit> patientMasterVisitList =
                unitOfWork.PatientMasterVisitRepository.FindBy(
                        x =>
                            x.PatientId == patientId &
                            DbFunctions.TruncateTime(x.CreateDate) == DbFunctions.TruncateTime(visitDate) &
                            x.DeleteFlag)
                    .OrderByDescending(x => x.Id).Take(1).ToList();
                            unitOfWork.Dispose();
                            return patientMasterVisitList;
            }

        }

        public List<PatientMasterVisit> GetPatientVisits(int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                List<PatientMasterVisit> patientVisitList = unitOfWork.PatientMasterVisitRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag).OrderByDescending(x => x.Id).ToList();
                unitOfWork.Dispose();
                return patientVisitList;
            }

        }

        public int UpdatePatientMasterVisit(PatientMasterVisit patientMasterVisit)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientMasterVisitRepository.Update(patientMasterVisit);
                unitOfWork.Dispose();
                return Result = unitOfWork.Complete();
            }

        }

        public int PatientMasterVisitCheckin(int patientId,PatientMasterVisit patientMasterVisit)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                PatientMasterVisitAutoClosure(patientId);

                var visitId =
                    unitOfWork.PatientMasterVisitRepository.FindBy(
                        x =>
                                //x.PatientId == patientId & DbFunctions.AddHours(x.Start,-24) <= DateTime.Now &
                                x.PatientId == patientId & DbFunctions.DiffHours(x.Start, DateTime.Now) <= 24 &
                                x.End == null & !x.Active & x.Status == 1).Select(x => x.Id).FirstOrDefault();
                if (visitId == 0)
                {
                    unitOfWork.PatientMasterVisitRepository.Add(patientMasterVisit);
                    unitOfWork.Complete();
                    visitId = patientMasterVisit.Id;
                }

                unitOfWork.Dispose();
                return visitId;
            }
        }

        public int PatientMasterVisitCheckout(int patientId,PatientMasterVisit patientMasterVisit)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var patientVisit =
                    unitOfWork.PatientMasterVisitRepository.FindBy(
                            x => x.PatientId == patientId & x.Status == 1 & !x.DeleteFlag & x.End == null & !x.Active)
                        .FirstOrDefault();
                if (patientVisit != null)
                {
                    patientVisit.Status = 2;
                    patientVisit.End = patientMasterVisit.End;
                    patientVisit.VisitDate = patientMasterVisit.VisitDate;
                    patientVisit.VisitScheduled = patientMasterVisit.VisitScheduled;
                    patientVisit.VisitType = patientMasterVisit.VisitType;
                    patientVisit.VisitBy = patientMasterVisit.VisitBy;
                    patientVisit.Patient = null;
                    //call the update function here....
                    unitOfWork.PatientMasterVisitRepository.Update(patientMasterVisit);
                    Result = unitOfWork.Complete();
                }
                unitOfWork.Dispose();
                return Result;
            }

        }

        public int PatientMasterVisitCheckout(int patientId, int masterVisitId,int visitSchedule, int visitBy,int visitType,DateTime visitDate)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var patientVisit = unitOfWork.PatientMasterVisitRepository.GetById(masterVisitId);
                if (null != patientVisit)
                {
                    patientVisit.Status = 2;
                    patientVisit.End = DateTime.Now;
                    patientVisit.Active = true;
                    patientVisit.VisitDate = visitDate;
                    patientVisit.VisitScheduled = visitSchedule;
                    patientVisit.VisitBy = visitBy;
                    patientVisit.VisitType = visitType;

                    unitOfWork.PatientMasterVisitRepository.Update(patientVisit);
                    Result = unitOfWork.Complete();
                }
                unitOfWork.Dispose();
                return Result;
            }

        }

        public void PatientMasterVisitAutoClosure(int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                List<PatientMasterVisit> patientMasterVisits = unitOfWork.PatientMasterVisitRepository.FindBy(x => x.PatientId == patientId & x.End == null & DbFunctions.DiffHours(x.Start, DateTime.Now) > 24).OrderBy(x => x.Id).ToList();

                if (patientMasterVisits.Count > 0)
                {
                    foreach (var item in patientMasterVisits)
                    {
                        item.Status = 3;
                        item.End = DateTime.Now;
                        item.Active = true;
                        item.VisitDate = null;
                        item.VisitScheduled = null;
                        item.VisitBy = null;
                        item.VisitType = null;

                        unitOfWork.PatientMasterVisitRepository.Update(item);

                        Result = unitOfWork.Complete();
                        unitOfWork.Dispose();
                    }
                }
            }

        }

        public DateTime GetPatientLastVisitDate(int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {

                
              DateTime patientLastVisitDate=   Convert.ToDateTime(
                    unitOfWork.PatientMasterVisitRepository.FindBy(x => x.PatientId == patientId)
                        .OrderByDescending(x => x.Id)
                        .Select(x => x.Start));
                unitOfWork.Dispose();
                return patientLastVisitDate;
            }

        }

        public List<PatientMasterVisit> GetByDate(DateTime date)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                List<PatientMasterVisit> patientVisitList = unitOfWork.PatientMasterVisitRepository.GetByDate(date);
                unitOfWork.Dispose();
                return patientVisitList;
            }
        }
    }
}

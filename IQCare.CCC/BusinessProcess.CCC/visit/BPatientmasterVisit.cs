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
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientMasterVisitRepository.Add(patientMasterVisit);
                Result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return patientMasterVisit.Id;
            }

        }

        public int DeletePatientVisit(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var patientmasterVisit = _unitOfWork.PatientMasterVisitRepository.GetById(id);
                _unitOfWork.PatientMasterVisitRepository.Remove(patientmasterVisit);
                 Result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }

        }

        public List<PatientMasterVisit> GetPatientCurrentVisit(int patientId, DateTime visitDate)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                List<PatientMasterVisit> patientMasterVisitList =
                _unitOfWork.PatientMasterVisitRepository.FindBy(
                        x =>
                            x.PatientId == patientId &
                            DbFunctions.TruncateTime(x.CreateDate) == DbFunctions.TruncateTime(visitDate) &
                            x.DeleteFlag)
                    .OrderByDescending(x => x.Id).Take(1).ToList();
                            _unitOfWork.Dispose();
                            return patientMasterVisitList;
            }

        }

        public List<PatientMasterVisit> GetPatientVisits(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                List<PatientMasterVisit> patientVisitList =
    _unitOfWork.PatientMasterVisitRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag)
        .OrderByDescending(x => x.Id).ToList();
                _unitOfWork.Dispose();
                return patientVisitList;
            }

        }

        public int UpdatePatientMasterVisit(PatientMasterVisit patientMasterVisit)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientMasterVisitRepository.Update(patientMasterVisit);
                _unitOfWork.Dispose();
                return Result = _unitOfWork.Complete();
            }

        }

        public int PatientMasterVisitCheckin(int patientId,PatientMasterVisit patientMasterVisit)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                PatientMasterVisitAutoClosure(patientId);

                var visitId =
                    _unitOfWork.PatientMasterVisitRepository.FindBy(
                        x =>
                                //x.PatientId == patientId & DbFunctions.AddHours(x.Start,-24) <= DateTime.Now &
                                x.PatientId == patientId & DbFunctions.DiffHours(x.Start, DateTime.Now) <= 24 &
                                x.End == null & !x.Active & x.Status == 1).Select(x => x.Id).FirstOrDefault();
                if (visitId == 0)
                {
                    _unitOfWork.PatientMasterVisitRepository.Add(patientMasterVisit);
                    _unitOfWork.Complete();
                    visitId = patientMasterVisit.Id;
                }

                _unitOfWork.Dispose();
                return visitId;
            }
        }

        public int PatientMasterVisitCheckout(int patientId,PatientMasterVisit patientMasterVisit)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var patientVisit =
                    _unitOfWork.PatientMasterVisitRepository.FindBy(
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
                    _unitOfWork.PatientMasterVisitRepository.Update(patientMasterVisit);
                    Result = _unitOfWork.Complete();
                }
                _unitOfWork.Dispose();
                return Result;
            }

        }

        public int PatientMasterVisitCheckout(int patientId, int masterVisitId,int visitSchedule, int visitBy,int visitType,DateTime visitDate)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var patientVisit = _unitOfWork.PatientMasterVisitRepository.GetById(masterVisitId);
                if (null != patientVisit)
                {
                    patientVisit.Status = 2;
                    patientVisit.End = DateTime.Now;
                    patientVisit.Active = true;
                    patientVisit.VisitDate = visitDate;
                    patientVisit.VisitScheduled = visitSchedule;
                    patientVisit.VisitBy = visitBy;
                    patientVisit.VisitType = visitType;

                    _unitOfWork.PatientMasterVisitRepository.Update(patientVisit);
                    Result = _unitOfWork.Complete();
                }
                _unitOfWork.Dispose();
                return Result;
            }

        }

        public void PatientMasterVisitAutoClosure(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                List<PatientMasterVisit> patientMasterVisits = _unitOfWork.PatientMasterVisitRepository.FindBy(x => x.PatientId == patientId & x.End == null & DbFunctions.DiffHours(x.Start, DateTime.Now) > 24).OrderBy(x => x.Id).ToList();

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

                        _unitOfWork.PatientMasterVisitRepository.Update(item);

                        Result = _unitOfWork.Complete();
                        _unitOfWork.Dispose();
                    }
                }
            }

        }

        public DateTime GetPatientLastVisitDate(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {

                
              DateTime PatineLastVisitDate=   Convert.ToDateTime(
                    _unitOfWork.PatientMasterVisitRepository.FindBy(x => x.PatientId == patientId)
                        .OrderByDescending(x => x.Id)
                        .Select(x => x.Start));
                _unitOfWork.Dispose();
                return PatineLastVisitDate;
            }

        }

        public List<PatientMasterVisit> GetByDate(DateTime date)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                List<PatientMasterVisit> patientVisitList = _unitOfWork.PatientMasterVisitRepository.GetByDate(date);
                _unitOfWork.Dispose();
                return patientVisitList;
            }
        }
    }
}

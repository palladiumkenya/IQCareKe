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
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        internal int Result;

        public int AddPatientmasterVisit(PatientMasterVisit patientMasterVisit)
        {
            _unitOfWork.PatientMasterVisitRepository.Add(patientMasterVisit);
            Result = _unitOfWork.Complete();
            return patientMasterVisit.Id;
        }

        public int DeletePatientVisit(int id)
        {
            var patientmasterVisit = _unitOfWork.PatientMasterVisitRepository.GetById(id);
            _unitOfWork.PatientMasterVisitRepository.Remove(patientmasterVisit);
            return Result = _unitOfWork.Complete();
        }

        public List<PatientMasterVisit> GetPatientCurrentVisit(int patientId, DateTime visitDate)
        {
            List<PatientMasterVisit> patientMasterVisitList =
                _unitOfWork.PatientMasterVisitRepository.FindBy(
                        x =>
                            x.PatientId == patientId &
                            DbFunctions.TruncateTime(x.CreateDate) == DbFunctions.TruncateTime(visitDate) & x.DeleteFlag)
                    .OrderByDescending(x => x.Id).Take(1).ToList();
            return patientMasterVisitList;
        }

        public List<PatientMasterVisit> GetPatientVisits(int patientId)
        {
            List<PatientMasterVisit> patientVisitList =
                _unitOfWork.PatientMasterVisitRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag)
                    .OrderByDescending(x => x.Id).ToList();
            return patientVisitList;
        }

        public int UpdatePatientMasterVisit(PatientMasterVisit patientMasterVisit)
        {
            _unitOfWork.PatientMasterVisitRepository.Update(patientMasterVisit);
            return Result = _unitOfWork.Complete();
        }

        public int PatientMasterVisitCheckin(int patientId,PatientMasterVisit patientMasterVisit)
        {

            var visitId =
                _unitOfWork.PatientMasterVisitRepository.FindBy(
                    x =>
                          //x.PatientId == patientId & DbFunctions.AddHours(x.Start,-24) <= DateTime.Now &
                          x.PatientId == patientId & DbFunctions.DiffHours(x.Start, DateTime.Now)<=24 &
                        x.End==null & !x.Active & x.Status==1).Select(x=> x.Id).FirstOrDefault();
            if (visitId == 0)
            {
                _unitOfWork.PatientMasterVisitRepository.Add(patientMasterVisit);
                _unitOfWork.Complete();
                visitId = patientMasterVisit.Id;
            }
            return visitId;
        }

        public int PatientMasterVisitCheckout(int patientId,PatientMasterVisit patientMasterVisit)
        {
            var patientVisit =
                _unitOfWork.PatientMasterVisitRepository.FindBy(
                    x => x.PatientId == patientId & x.Status == 1 & !x.DeleteFlag & x.End == null & !x.Active).FirstOrDefault();
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
            return Result;
        }

        public int PatientMasterVisitCheckout(int patientId, int masterVisitId,int visitSchedule, int visitBy,int visitType,DateTime visitDate)
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
            return Result;

        }
    }
}

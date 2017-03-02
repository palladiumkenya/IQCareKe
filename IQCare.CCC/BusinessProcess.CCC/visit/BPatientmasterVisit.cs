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

        public int PatienMasterVisitCheckin(int patientId,PatientMasterVisit patientMasterVisit)
        {
            /* for status column 1=checkedin 2=checkedout 3=systemcheckout*/
            var visitId =
                _unitOfWork.PatientMasterVisitRepository.FindBy(
                    x =>
                        x.PatientId == patientId & DbFunctions.AddHours(x.Start,24) < DateTime.Now &
                        x.Status == 1 & !x.Active & !x.DeleteFlag).Select(x=> x.Id).FirstOrDefault();
            if (visitId < 1)
            {
                _unitOfWork.PatientMasterVisitRepository.Add(patientMasterVisit);
                _unitOfWork.Complete();
                visitId = patientMasterVisit.Id;
            }

            return visitId;
        }

        public int PatientMasterVisitCheckout(int patientId,PatientMasterVisit patientMasterVisit)
        {
            var visitId =
                _unitOfWork.PatientMasterVisitRepository.FindBy(
                    x => x.PatientId == patientId & x.Status == 1 & !x.DeleteFlag).Select(x => x.Id).SingleOrDefault();
            if (visitId > 0)
            {
                //var pmVisit=new PatientMasterVisit {Id = visitId,End =Convert.ToDateTime(DateTime.Now.TimeOfDay),ServiceId = 1,status = 2};
                _unitOfWork.PatientMasterVisitRepository.Update(patientMasterVisit);
                Result = _unitOfWork.Complete();
            }
            else
            {
                Result = 0;
            }
            return Result;
        }
    }
}
